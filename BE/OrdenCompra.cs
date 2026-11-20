using System;
using System.Collections.Generic;
using System.Linq;

namespace BE
{
    public class OrdenCompra
    {
        public int CodOrden_389ME { get; set; }
        public string CUIT_389ME { get; set; }
        public string NombreProveedor_389ME { get; set; }
        public int CodSolicitud_389ME { get; set; }
        public DateTime FechaCreacion_389ME { get; set; }
        public double PrecioTotal_389ME { get; set; }
        public string Estado_389ME { get; set; }
        public List<OrdenCompraDetalle> Detalles_389ME { get; set; } = new List<OrdenCompraDetalle>();

        public override string ToString()
        {
            return "Orden N° " + CodOrden_389ME + " - " + NombreProveedor_389ME;
        }
    }
}
