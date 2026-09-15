using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public abstract class Perfil
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        
        public TipoPermiso? Permiso { get; set; }

        public abstract IList<Perfil> Hijos { get; }
        public abstract void AgregarHijo(Perfil p);
        public abstract void EliminarHijo(int id);
        public abstract void VaciarHijos();
    }
}
