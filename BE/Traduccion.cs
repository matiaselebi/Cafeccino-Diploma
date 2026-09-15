using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Traduccion
    {
        public int IdTraduccion { get; set; }

        public int Idioma { get; set; }

        public int Control { get; set; }

        public string Texto { get; set; }

        public Traduccion()
        {

        }      
    }
}
