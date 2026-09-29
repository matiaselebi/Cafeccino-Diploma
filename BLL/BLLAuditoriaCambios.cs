using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DAL;
using SERVICIOS;

namespace BLL
{
    public class BLLAuditoriaCambios
    {
        private readonly DALAuditoriaCambios dalAuditoriaCambios = new DALAuditoriaCambios();
        private readonly DALUsuario dalUsuario = new DALUsuario();
        private string SerializarUsuario(Usuario usuario)
        {
            return $"DNI:{usuario.DNI} | Usuario:{usuario.NombreUsuario} | Contraseña:{usuario.Contraseña}| Nombre:{usuario.Nombre} | " +
                   $"Apellido:{usuario.Apellido} | Mail:{usuario.Mail} | Tel:{usuario.NumTelefono} | Rol:{usuario.Rol}";
        }

        public void RegistrarModificacion(Usuario anterior, Usuario nuevo, string usuarioEditor)
        {
            dalAuditoriaCambios.RegistrarCambio(new AuditoriaCambios
            {
                UsuarioEditor = usuarioEditor,
                Operacion = "Modificacion",
                DNIAfectado = nuevo.DNI,
                DatosAnteriores = SerializarUsuario(anterior),
                DatosNuevos = SerializarUsuario(nuevo)
            });
        }

        public DataTable ObtenerCambios()
        {
            return dalAuditoriaCambios.ObtenerCambios();
        }

        public AuditoriaCambios ObtenerUltimoPorDni(int dni)
        {
            return dalAuditoriaCambios.ObtenerUltimoPorDni(dni);
        }

        public Usuario ParsearSnapshot(string datos)
        {
            return DesSerializarUsuario(datos);
        }

        public void RestaurarUsuario(int idBitacora)
        {

            AuditoriaCambios registro = dalAuditoriaCambios.ObtenerPorId(idBitacora);

            if (registro == null)
                return;

            if (registro.Operacion != "Modificacion")
                return;

            Usuario usuarioAnterior = DesSerializarUsuario(registro.DatosAnteriores);

            dalUsuario.ModificarUsuario(usuarioAnterior);

            dalAuditoriaCambios.RegistrarCambio(new AuditoriaCambios
            {
                UsuarioEditor = Singleton.Instancia.Usuario.NombreUsuario,
                Operacion = "Restauracion",
                DNIAfectado = registro.DNIAfectado,
                DatosAnteriores = registro.DatosNuevos,
                DatosNuevos = registro.DatosAnteriores
            });

            new BLLUsuario().SincronizarDigitos();
        }

        private Usuario DesSerializarUsuario(string datos)
        {

            var partes = datos.Split('|');
            Usuario usuario = new Usuario();

            foreach (var parte in partes)
            {
                var kv = parte.Trim().Split(':');
                if (kv.Length < 2) continue;
                string clave = kv[0].Trim();
                string valor = string.Join(":", kv.Skip(1)).Trim();

                switch (clave)
                {
                    case "DNI": usuario.DNI = int.Parse(valor); break;
                    case "Usuario": usuario.NombreUsuario = valor; break;
                    case "Contraseña": usuario.Contraseña = valor; break;
                    case "Nombre": usuario.Nombre = valor; break;
                    case "Apellido": usuario.Apellido = valor; break;
                    case "Mail": usuario.Mail = valor; break;
                    case "Tel": usuario.NumTelefono = int.TryParse(valor, out int tel) ? tel : 0; break;
                    case "Rol": usuario.Rol = valor; break;
                }
            }
            return usuario;
        }
    }
}
