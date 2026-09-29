using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Permiso : Perfil
    {
  
        private static readonly IList<Perfil> hijosVacios = new List<Perfil>().AsReadOnly();

        public override IList<Perfil> Hijos
        {
            get
            {
                return hijosVacios;
            }
        }

        public override void AgregarHijo(Perfil perfil)
        {
            
            throw new InvalidOperationException("No se pueden agregar hijos a un permiso simple.");
        }
        public override void VaciarHijos()
        {
           
        }
        public override void EliminarHijo(int id)
        {
            throw new InvalidOperationException("Un permiso simple no tiene hijos.");
        }

    }
}
