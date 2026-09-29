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
            }


            return usuarioLogueado;
        }
        public void RegistrarUsuario(Usuario usuario, int idPerfil)
        {
            usuario.Contraseña = encriptacion.GenerarHash(usuario.Contraseña);
            dalUsuario.RegistrarUsuario(usuario, idPerfil);
        }
        public DataTable ListarUsuarios()
        {
            return dalUsuario.ListarUsuarios();
        }
        public void ModificarUsuario(Usuario usuario)
        {
            usuario.Contraseña = encriptacion.GenerarHash(usuario.Contraseña);
            dalUsuario.ModificarUsuario(usuario);
        }
        public void EliminarUsuario(int dni)
        {
            dalUsuario.EliminarUsuario(dni);
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

            string hash = encriptacion.GenerarHash(nuevaContraseña);
            dalUsuario.ActualizarContraseña(nombreUsuario, hash);

            SincronizarDigitos();
        }
    }
}

