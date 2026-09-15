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
        BLLPermiso _permisoBLL = new BLLPermiso();
        Encriptacion encriptacion = new Encriptacion();
        DALUsuario DALUsuario = new DALUsuario();

        public Usuario Login(Usuario user)
        {

            user.Contraseña = encriptacion.GenerarHash(user.Contraseña);


            Usuario usuarioLogueado = DALUsuario.Login(user);


            if (usuarioLogueado != null)
            {

                if (usuarioLogueado.Perfil != null)
                {
                    _permisoBLL.ArmarArbolComponente(usuarioLogueado.Perfil);
                }
            }


            return usuarioLogueado;
        }
        public void RegistrarUsuario(Usuario usuario, int idPerfil)
        {
            usuario.Contraseña = encriptacion.GenerarHash(usuario.Contraseña);
            DALUsuario.RegistrarUsuario(usuario, idPerfil);
        }
        public DataTable ListarUsuarios()
        {
            return DALUsuario.ListarUsuarios();
        }
        public void ModificarUsuario(Usuario usuario)
        {
            usuario.Contraseña = encriptacion.GenerarHash(usuario.Contraseña);
            DALUsuario.ModificarUsuario(usuario);
        }
        public void EliminarUsuario(int dni)
        {
            DALUsuario.EliminarUsuario(dni);
        }
        private readonly DALUsuario _usuarioDAL = new DALUsuario();
        private readonly DALDigitoVerificador _dvDAL = new DALDigitoVerificador();


        public void SincronizarDigitos()
        {
            var usuarios = _usuarioDAL.ObtenerTodos();

            var dv = DigitVerifierService.Recalcular(usuarios);

            foreach (Usuario u in usuarios)
            {
                _usuarioDAL.ActualizarDVH(u.DNI, u.DVH);
            }

            _dvDAL.Guardar(dv);
        }


        public List<int> ObtenerFilasAlteradas()
        {
            List<Usuario> lista = _usuarioDAL.ObtenerTodos();
            DigitoVerificador dvGuardado = _dvDAL.ObtenerPorTabla("Usuario");
            return DigitVerifierService.ObtenerFilasAlteradas(lista, dvGuardado);
        }

        public bool ValidarIntegridad()
        {
            var usuarios = _usuarioDAL.ObtenerTodos();

            var dv = _dvDAL.ObtenerPorTabla("Usuario");

            return DigitVerifierService.Validar(usuarios, dv);
        }

        public Usuario ObtenerPorDNI(int dni)
        {
            return _usuarioDAL.ObtenerPorDNI(dni);
        }

        public void CambiarContraseña(string nombreUsuario, string nuevaContraseña)
        {
            Usuario usuario = _usuarioDAL.ObtenerPorUsername(nombreUsuario);
            if (usuario == null)
                throw new Exception("No existe un usuario con ese nombre de usuario.");

            string hash = encriptacion.GenerarHash(nuevaContraseña);
            _usuarioDAL.ActualizarContraseña(nombreUsuario, hash);

            SincronizarDigitos();
        }
    }
}

