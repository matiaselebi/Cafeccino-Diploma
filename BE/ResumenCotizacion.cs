using System;
using System.Collections.Generic;
using System.Linq;

namespace BE
{
    // Totales de lo que cotizo un proveedor para una solicitud. Sirve para comparar proveedores.
    public class ResumenCotizacion
    {
        public string CUIT_389ME { get; set; }
        public string NombreProveedor_389ME { get; set; }
        public int InsumosCotizados_389ME { get; set; }
        public double Total_389ME { get; set; }
        public bool Registrado_389ME { get; set; }
    }
}
