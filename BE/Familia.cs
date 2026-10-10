using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Familia : Perfil
    {
        private IList<Perfil> hijos;

        public Familia()
        {
            hijos = new List<Perfil>();
        }

        public override IList<Perfil> Hijos
        {
            get
            {
                
                return hijos.ToList();
            }
        }

        public override void EliminarHijo(int id)
        {
            Perfil aEliminar = hijos.FirstOrDefault(hijo => hijo.Id == id);
            if (aEliminar != null)
                hijos.Remove(aEliminar);
        }
        public override void VaciarHijos()
        {
            hijos.Clear();
        }
        public override void AgregarHijo(Perfil perfil)
        {
            
            if (perfil != null && !hijos.Any(hijo => hijo.Id == perfil.Id))
            {
                hijos.Add(perfil);
            }
        }
    }
}
