using System;
using System.Collections.Generic;
using System.Linq;

namespace BE
{
    // Un insumo pedido en una solicitud de cotizacion
    public class SolicitudInsumo
    {
        public int CodSolicitud_389ME { get; set; }
        public int CodInsumo_389ME { get; set; }
        public string NombreInsumo_389ME { get; set; }
        public string UnidadMedida_389ME { get; set; }
        public double Cantidad_389ME { get; set; }
        public double PrecioActual_389ME { get; set; }
    }
}
