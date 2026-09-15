using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class AuditoriaCambios
    {
        public int IdBitacora { get; set; }
        public DateTime FechaHora { get; set; }
        public string UsuarioEditor { get; set; }
        public string Operacion { get; set; }
        public int DNIAfectado { get; set; }
        public string DatosAnteriores { get; set; }
        public string DatosNuevos { get; set; }
    }
}
