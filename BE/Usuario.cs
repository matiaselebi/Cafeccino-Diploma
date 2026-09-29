using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Usuario
    {
        public int DNI { get; set; }
        public string NombreUsuario { get; set; }
        public string Contraseña { get; set; }

        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Mail { get; set; }
        public int NumTelefono { get; set; }
        public string Rol { get; set; }
        public int DVH { get; set; }
        public int IntentosFallidos { get; set; }
        public bool Bloqueado { get; set; }

       
        public Perfil Perfil { get; set; }

        public Usuario(int dni, string nombre, string apellido, string mail, int numtelefono, string username, string password, string rol, int dVH)
        {
            DNI = dni;
            Nombre = nombre;
            Apellido = apellido;
            Mail = mail;
            NumTelefono = numtelefono;
            NombreUsuario = username;
            Contraseña = password;
            Rol = rol;
            DVH = dVH;

        }

        public Usuario()
        {
        }
    }
}
