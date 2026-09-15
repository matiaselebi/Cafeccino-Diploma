using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class BitacoraEventos
    {
        public int CodEvento {  get; set; }
        public string NombreUsuario { get; set; }
        public DateTime Fecha { get; set; }
        public string Evento { get; set; }
        public string Modulo { get; set; }
        public string Criticidad { get; set; }
    }
}
