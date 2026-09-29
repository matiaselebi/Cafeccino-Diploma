using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SERVICIOS
{
    public sealed class GestorIdiomas : Subject
    {
        private static GestorIdiomas instancia;

        public static GestorIdiomas Instancia
        {
            get
            {
                if (instancia == null)
                    instancia = new GestorIdiomas();

                return instancia;
            }
        }

        private GestorIdiomas()
        {

        }

        public int IdiomaActual { get; private set; } = 2;

        public void CambiarIdioma(int idIdioma)
        {
            IdiomaActual = idIdioma;

            Notificar();
        }
    }
}
