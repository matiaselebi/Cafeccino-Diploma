using System;
using System.Collections.Generic;
using System.Linq;

namespace BE
{
    public class OrdenCompraDetalle
    {
        public int CodOrden_389ME { get; set; }
        public int CodInsumo_389ME { get; set; }
        public string NombreInsumo_389ME { get; set; }
        public double Cantidad_389ME { get; set; }
        public double PrecioUnitario_389ME { get; set; }
        public double CantidadRecibida_389ME { get; set; }

        public double Subtotal_389ME
        {
            get { return Cantidad_389ME * PrecioUnitario_389ME; }
        }
    }
}
