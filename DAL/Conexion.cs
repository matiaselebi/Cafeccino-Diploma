using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    internal static class Conexion
    {
        // La cadena se lee de UI.exe.config. El instalador escribe ahi el servidor elegido.
        internal static readonly string cadena = ConfigurationManager.ConnectionStrings["Cafeccino"].ConnectionString;
    }
}
