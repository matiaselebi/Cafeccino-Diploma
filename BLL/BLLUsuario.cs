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
    public class BLLUsuario
    {
        BLLPermiso bllPermiso = new BLLPermiso();
        Encriptacion encriptacion = new Encriptacion();
        private readonly BLLBitacoraEventos bllBitacora_389ME = new BLLBitacoraEventos();
        private readonly BLLDigitoVerificador bllDigito_389ME = new BLLDigitoVerificador();
        private readonly DALUsuario dalUsuario = new DALUsuario();

        public const int IntentosParaBloquear = 3;

        public Usuario Login(Usuario user)
        {
            Usuario registrado = dalUsuario.ObtenerPorUsername(user.NombreUsuario);

            if (registrado != null && registrado.Bloqueado)
                throw new Exception("Usuario bloqueado.");

            user.Contraseña = encriptacion.GenerarHash(user.Contraseña);


            Usuario usuarioLogueado = dalUsuario.Login(user);


            if (usuarioLogueado != null)
            {
                dalUsuario.ReiniciarIntentos(usuarioLogueado.DNI);

                if (usuarioLogueado.Perfil != null)
                {
                    bllPermiso.ArmarArbolComponente(usuarioLogueado.Perfil);
                }
            }
            else if (registrado != null)
            {
                dalUsuario.RegistrarIntentoFallido(user.NombreUsuario, IntentosParaBloquear);

                // Con este intento llego al tope: el usuario acaba de quedar bloqueado.
                if (registrado.IntentosFallidos + 1 >= IntentosParaBloquear)
                    bllBitacora_389ME.RegistrarEvento_389ME("Bloqueo de usuario",
                        $"{IntentosParaBloquear} intentos fallidos de inicio de sesión", "Login", "Alta", user.NombreUsuario);
            }


            return usuarioLogueado;
        }
        public void RegistrarUsuario(Usuario usuario, int idPerfil)
        {
            Validar_389ME(usuario);
            ValidarContraseña_389ME(usuario.Contraseña);

            if (dalUsuario.ObtenerPorDNI(usuario.DNI) != null)
                throw new Exception("Ya existe un usuario con ese DNI.");
            if (dalUsuario.ObtenerPorUsername(usuario.NombreUsuario) != null)
                throw new Exception("Ya existe un usuario con ese nombre de usuario.");

            usuario.Contraseña = encriptacion.GenerarHash(usuario.Contraseña);
            dalUsuario.RegistrarUsuario(usuario, idPerfil);
            bllDigito_389ME.Recalcular_389ME("Usuario_Permiso");

            bllBitacora_389ME.RegistrarEvento_389ME("Alta de usuario", $"{usuario.NombreUsuario} - DNI {usuario.DNI} - Rol {usuario.Rol}", "Usuarios", "Media");
        }
        public DataTable ListarUsuarios()
        {
            return dalUsuario.ListarUsuarios();
        }
        public void ModificarUsuario(Usuario usuario, int idPerfil)
        {
            Validar_389ME(usuario);

            Usuario anterior_389ME = dalUsuario.ObtenerPorDNI(usuario.DNI);
            if (anterior_389ME == null)
                throw new Exception("No existe un usuario con ese DNI.");

            Usuario mismoNombre_389ME = dalUsuario.ObtenerPorUsername(usuario.NombreUsuario);
            if (mismoNombre_389ME != null && mismoNombre_389ME.DNI != usuario.DNI)
                throw new Exception("Ya existe un usuario con ese nombre de usuario.");

            // Si no se escribio una contraseña nueva, se mantiene la que tenia (ya guardada como hash)
            if (string.IsNullOrEmpty(usuario.Contraseña))
            {
                usuario.Contraseña = anterior_389ME.Contraseña;
            }
            else
            {
                ValidarContraseña_389ME(usuario.Contraseña);
                usuario.Contraseña = encriptacion.GenerarHash(usuario.Contraseña);
            }

            // El trigger TR_Usuario_Cambios guarda en AuditoriaCambios los datos anteriores y los nuevos
            dalUsuario.ModificarUsuario(usuario, Singleton.Instancia.Usuario?.NombreUsuario, "Modificacion");
            // El rol de la grilla es solo texto: los permisos salen del perfil asignado
            dalUsuario.CambiarPerfil_389ME(usuario.DNI, idPerfil);
            bllDigito_389ME.Recalcular_389ME("Usuario_Permiso", "AuditoriaCambios");

            bllBitacora_389ME.RegistrarEvento_389ME("Modificación de usuario", $"{usuario.NombreUsuario} - DNI {usuario.DNI}", "Usuarios", "Media");
        }
        public void EliminarUsuario(int dni)
        {
            if (Singleton.Instancia.Usuario != null && Singleton.Instancia.Usuario.DNI == dni)
                throw new Exception("No puede eliminar el usuario con el que inició sesión.");
            if (dalUsuario.ObtenerPorDNI(dni) == null)
                throw new Exception("No existe un usuario con ese DNI.");

            dalUsuario.EliminarUsuario(dni);
            bllDigito_389ME.Recalcular_389ME("Usuario_Permiso");

            bllBitacora_389ME.RegistrarEvento_389ME("Baja de usuario", $"DNI {dni}", "Usuarios", "Alta");
        }

        public void Bloquear_389ME(int dni_389ME)
        {
            CambiarBloqueo_389ME(dni_389ME, true);
        }

        public void Desbloquear_389ME(int dni_389ME)
        {
            CambiarBloqueo_389ME(dni_389ME, false);
        }

        // El bloqueo no cambia el DVH (no entra en el calculo) ni el registro de cambios (el trigger lo ignora)
        private void CambiarBloqueo_389ME(int dni_389ME, bool bloquear_389ME)
        {
            Usuario usuario_389ME = dalUsuario.ObtenerPorDNI(dni_389ME);
            if (usuario_389ME == null)
                throw new Exception("No existe un usuario con ese DNI.");
            if (bloquear_389ME && Singleton.Instancia.Usuario != null && Singleton.Instancia.Usuario.DNI == dni_389ME)
                throw new Exception("No puede bloquear el usuario con el que inició sesión.");
            if (usuario_389ME.Bloqueado == bloquear_389ME)
                throw new Exception(bloquear_389ME ? "El usuario ya está bloqueado." : "El usuario no está bloqueado.");

            string editor_389ME = Singleton.Instancia.Usuario?.NombreUsuario ?? "Sistema";
            dalUsuario.CambiarBloqueo_389ME(dni_389ME, bloquear_389ME, editor_389ME);

            bllBitacora_389ME.RegistrarEvento_389ME(bloquear_389ME ? "Bloqueo de usuario" : "Desbloqueo de usuario",
                $"{usuario_389ME.NombreUsuario} - DNI {dni_389ME} - {(bloquear_389ME ? "bloqueado" : "desbloqueado")} por {editor_389ME}",
                "Usuarios", bloquear_389ME ? "Alta" : "Media");
        }
        private readonly DALDigitoVerificador dalDigitoVerificador = new DALDigitoVerificador();


        public void SincronizarDigitos()
        {
            var usuarios = dalUsuario.ObtenerTodos();

            var dv = DigitVerifierService.Recalcular(usuarios);

            foreach (Usuario usuario in usuarios)
            {
                dalUsuario.ActualizarDVH(usuario.DNI, usuario.DVH);
            }

            dalDigitoVerificador.Guardar(dv);
        }


        public List<int> ObtenerFilasAlteradas()
        {
            List<Usuario> lista = dalUsuario.ObtenerTodos();
            DigitoVerificador dvGuardado = dalDigitoVerificador.ObtenerPorTabla("Usuario");
            return DigitVerifierService.ObtenerFilasAlteradas(lista, dvGuardado);
        }

        public bool ValidarIntegridad()
        {
            var usuarios = dalUsuario.ObtenerTodos();

            var dv = dalDigitoVerificador.ObtenerPorTabla("Usuario");

            return DigitVerifierService.Validar(usuarios, dv);
        }

        public Usuario ObtenerPorDNI(int dni)
        {
            return dalUsuario.ObtenerPorDNI(dni);
        }

        public void CambiarContraseña(string nombreUsuario, string nuevaContraseña)
        {
            Usuario usuario = dalUsuario.ObtenerPorUsername(nombreUsuario);
            if (usuario == null)
                throw new Exception("No existe un usuario con ese nombre de usuario.");

            ValidarContraseña_389ME(nuevaContraseña);

            string hash = encriptacion.GenerarHash(nuevaContraseña);
            if (hash == usuario.Contraseña)
                throw new Exception("La nueva contraseña debe ser distinta de la actual.");

            dalUsuario.ActualizarContraseña(nombreUsuario, hash);

            SincronizarDigitos();
            bllDigito_389ME.Recalcular_389ME("AuditoriaCambios");
        }

        private void Validar_389ME(Usuario usuario)
        {
            if (usuario.DNI < 10000000 || usuario.DNI > 99999999)
                throw new Exception("El DNI debe tener 8 números.");
            if (string.IsNullOrWhiteSpace(usuario.NombreUsuario) || usuario.NombreUsuario.Length < 4 || usuario.NombreUsuario.Contains(" "))
                throw new Exception("El nombre de usuario debe tener al menos 4 caracteres y no puede tener espacios.");
            if (!Validador.EsTexto_389ME(usuario.Nombre) || !Validador.EsTexto_389ME(usuario.Apellido))
                throw new Exception("El nombre y el apellido son obligatorios y solo pueden tener letras.");
            if (!Validador.EsMail_389ME(usuario.Mail))
                throw new Exception("El mail no es válido. Ejemplo: nombre@correo.com");
            // El telefono es opcional: 0 significa que no se cargo
            if (usuario.NumTelefono != 0 && (usuario.NumTelefono < 10000000 || usuario.NumTelefono > 9999999999))
                throw new Exception("El teléfono debe tener entre 8 y 10 números.");
            if (string.IsNullOrWhiteSpace(usuario.Rol))
                throw new Exception("Seleccione un rol.");
        }

        // Minimo 8 caracteres, con al menos una letra y un numero
        private void ValidarContraseña_389ME(string contraseña_389ME)
        {
            if (contraseña_389ME == null || contraseña_389ME.Length < 8 ||
                !contraseña_389ME.Any(char.IsLetter) || !contraseña_389ME.Any(char.IsDigit))
                throw new Exception("La contraseña debe tener al menos 8 caracteres, con letras y números.");
        }
    }
}

