using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Factura
    {
        public int CodFactura_389ME { get; set; }
        public int DNICliente_389ME { get; set; }
        public DateTime Fecha_389ME { get; set; }
        public string MetodoPago_389ME { get; set; }
        public string Banco_389ME { get; set; }
        public string TipoTarjeta_389ME { get; set; }
        public string MarcaTarjeta_389ME { get; set; }
        public double MontoTotal_389ME { get; set; }
        public int DVH_389ME { get; set; }

        public List<DetalleFactura> Detalles_389ME { get; set; } = new List<DetalleFactura>();
    }
}
