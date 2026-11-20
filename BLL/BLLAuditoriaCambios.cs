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
        private readonly BLLDigitoVerificador bllDigito_389ME = new BLLDigitoVerificador();
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

            // El trigger TR_Usuario_Cambios guarda la restauracion en AuditoriaCambios
            dalUsuario.ModificarUsuario(usuarioAnterior, Singleton.Instancia.Usuario.NombreUsuario, "Restauracion");
            bllDigito_389ME.Recalcular_389ME("AuditoriaCambios");

            new BLLUsuario().SincronizarDigitos();

            new BLLBitacoraEventos().RegistrarEvento_389ME("Restauración de usuario", $"DNI {registro.DNIAfectado} volvió a sus datos anteriores", "Usuarios", "Alta");
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
                    case "Tel": usuario.NumTelefono = long.TryParse(valor, out long tel) ? tel : 0; break;
                    case "Rol": usuario.Rol = valor; break;
                }
            }
            return usuario;
        }
    }
}
