using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    
    public class DigitoVerificador
    {
        public string Tabla { get; set; }
        public string DVH { get; set; }   
        public string DVV { get; set; } 
       
        public List<int> ObtenerListaDVH() =>
            DVH.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
               .Select(int.Parse).ToList();

        public int ObtenerDVV() =>
            int.TryParse(DVV, out int v) ? v : 0;
    }
}
