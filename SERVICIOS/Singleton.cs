using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using BE;

namespace SERVICIOS
{
    public class Singleton
    {
        private static Singleton instance;
        private static Usuario usuario;

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
            usuario = userNuevo;
        }

        public void CerrarSesion()
        {
            usuario = null;
        }

        public Usuario Usuario
        {
            get { return usuario; }
        }

        public bool EsAdministrador()
        {
            return usuario != null && string.Equals(usuario.Rol, "administrador", StringComparison.OrdinalIgnoreCase);
        }
    }
}
