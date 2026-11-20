using System;
using System.Collections.Generic;
using System.Linq;

namespace BE
{
    // Precio y cantidad que ofrece un proveedor por un insumo de una solicitud
    public class Cotizacion
    {
        public int CodCotizacion_389ME { get; set; }
        public int CodSolicitud_389ME { get; set; }
        public string CUIT_389ME { get; set; }
        public string NombreProveedor_389ME { get; set; }
        public int CodInsumo_389ME { get; set; }
        public string NombreInsumo_389ME { get; set; }
        public double Precio_389ME { get; set; }
        public double Cantidad_389ME { get; set; }
        public DateTime FechaVencimiento_389ME { get; set; }

        public double Subtotal_389ME
        {
            get { return Precio_389ME * Cantidad_389ME; }
        }
    }
}
