using System;
using System.Collections.Generic;
using System.Linq;

namespace BE
{
    public class Proveedor
    {
        public string CUIT_389ME { get; set; }
        public string Nombre_389ME { get; set; }
        public string RazonSocial_389ME { get; set; }
        public string Telefono_389ME { get; set; }
        public string Email_389ME { get; set; }
        public string Direccion_389ME { get; set; }
        public string CuentaBancaria_389ME { get; set; }
        // false mientras esta pre-registrado: pasa a true cuando se completan los datos de pago
        public bool Registrado_389ME { get; set; }

        public override string ToString()
        {
            return Nombre_389ME + " - CUIT " + CUIT_389ME;
        }
    }
}
