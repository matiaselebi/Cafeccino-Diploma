using System;
using System.Collections.Generic;
using System.Linq;

namespace BE
{
    // Materia prima con la que se elaboran los productos (harina, leche, cafe...)
    public class Insumo
    {
        public int CodInsumo_389ME { get; set; }
        public string Nombre_389ME { get; set; }
        public string Tipo_389ME { get; set; }
        public string UnidadMedida_389ME { get; set; }
        public double Precio_389ME { get; set; }
        public double Stock_389ME { get; set; }
        public double StockMinimo_389ME { get; set; }
        public double StockMaximo_389ME { get; set; }

        public override string ToString()
        {
            return Nombre_389ME + " (" + UnidadMedida_389ME + ")";
        }
    }
}
