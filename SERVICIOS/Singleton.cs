using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using BE;

namespace SERVICIOS
{
    public class Singleton: Subject
    {
        private static Singleton instance;
        private static Usuario _user;


        private int _idIdioma = 2;

        private Singleton() { }

        public static Singleton Instancia
        {
            get
            {
                if (instance == null)
                {
                    instance = new Singleton();
                }

                return instance;
            }
        }

        public void IniciarSesion(Usuario userNuevo)
        {
            _user = userNuevo;
        }

        public void CerrarSesion()
        {
            _user = null;
        }

        public Usuario Usuario
        {
            get { return _user; }
        }

        public bool EsAdministrador()
        {
            return _user != null && string.Equals(_user.Rol, "administrador", StringComparison.OrdinalIgnoreCase);
        }


        public int IdiomaActual
        {
            get { return _idIdioma; }
            set { _idIdioma = value; }
        }

        public void CambiarIdioma(int idIdioma)
        {
            _idIdioma = idIdioma;

            Notificar();
        }
    }
}
