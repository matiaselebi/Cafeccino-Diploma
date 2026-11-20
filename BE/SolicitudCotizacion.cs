using System;
using System.Collections.Generic;
using System.Linq;

namespace BE
{
    public class SolicitudCotizacion
    {
        public int CodSolicitud_389ME { get; set; }
        public DateTime FechaEmision_389ME { get; set; }
        public string Estado_389ME { get; set; }
        public List<SolicitudInsumo> Insumos_389ME { get; set; } = new List<SolicitudInsumo>();
        public List<Proveedor> Proveedores_389ME { get; set; } = new List<Proveedor>();

        public override string ToString()
        {
            return "Solicitud N° " + CodSolicitud_389ME + " - " + FechaEmision_389ME.ToString("dd/MM/yyyy");
        }
    }
}
