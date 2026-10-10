using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class DetalleFactura
    {
        public int CodFactura_389ME { get; set; }
        public int CodProducto_389ME { get; set; }
        public string NombreProducto_389ME { get; set; }
        public int Cantidad_389ME { get; set; }
        public double PrecioUnitario_389ME { get; set; }
        public int DVH_389ME { get; set; }

        public double Subtotal_389ME
        {
            get { return Cantidad_389ME * PrecioUnitario_389ME; }
        }
    }
}
