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
        private readonly DALAuditoriaCambios _dal = new DALAuditoriaCambios();
        private readonly DALUsuario _dalUsuario = new DALUsuario();
        private string SerializarUsuario(Usuario u)
        {
            return $"DNI:{u.DNI} | Usuario:{u.NombreUsuario} | Contraseña:{u.Contraseña}| Nombre:{u.Nombre} | " +
                   $"Apellido:{u.Apellido} | Mail:{u.Mail} | Tel:{u.NumTelefono} | Rol:{u.Rol}";
        }

        public void RegistrarModificacion(Usuario anterior, Usuario nuevo, string usuarioEditor)
        {
            _dal.RegistrarCambio(new AuditoriaCambios
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
            return _dal.ObtenerCambios();
        }

        public void RestaurarUsuario(int idBitacora)
        {

            AuditoriaCambios registro = _dal.ObtenerPorId(idBitacora);

            if (registro == null)
                return;

            if (registro.Operacion != "Modificacion")
                return;

            Usuario usuarioAnterior = DesSerializarUsuario(registro.DatosAnteriores);

            _dalUsuario.ModificarUsuario(usuarioAnterior);

            _dal.RegistrarCambio(new AuditoriaCambios
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
            Usuario u = new Usuario();

            foreach (var parte in partes)
            {
                var kv = parte.Trim().Split(':');
                if (kv.Length < 2) continue;
                string clave = kv[0].Trim();
                string valor = string.Join(":", kv.Skip(1)).Trim();

                switch (clave)
                {
                    case "DNI": u.DNI = int.Parse(valor); break;
                    case "Usuario": u.NombreUsuario = valor; break;
                    case "Contraseña": u.Contraseña = valor; break;
                    case "Nombre": u.Nombre = valor; break;
                    case "Apellido": u.Apellido = valor; break;
                    case "Mail": u.Mail = valor; break;
                    case "Tel": u.NumTelefono = int.TryParse(valor, out int tel) ? tel : 0; break;
                    case "Rol": u.Rol = valor; break;
                }
            }
            return u;
        }
    }
}
