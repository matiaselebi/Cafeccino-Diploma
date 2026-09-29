using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Producto
    {
        public int CodProducto_389ME { get; set; }
        public string Nombre_389ME { get; set; }
        public string Titulo_389ME { get; set; }
        public double Precio_389ME { get; set; }
        public int Stock_389ME { get; set; }
        public int DVH_389ME { get; set; }

        public override string ToString()
        {
            return Nombre_389ME;
        }
    }
}
