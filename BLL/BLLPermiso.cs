using BE;
using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLLPermiso
    {
        private DALPermiso _permisoDAL = new DALPermiso();

        public Perfil CargarArbolCompleto(Perfil perfil)
        {
            List<Perfil> hijos = _permisoDAL.ObtenerHijos(perfil.Id);

            foreach (Perfil hijo in hijos)
            {

                if (hijo is Familia)
                    CargarArbolCompleto(hijo);

                perfil.AgregarHijo(hijo);
            }

            return perfil;
        }

        public List<Perfil> ObtenerFamilias()
        {

            return _permisoDAL.ObtenerTodosLosComponentes(soloFamilias: true, soloPerfiles: false);
        }
        public List<Perfil> ObtenerPermisosYPerfiles()
        {
            return _permisoDAL.ObtenerPermisosYPerfiles();
        }
        public List<Perfil> ObtenerPermisosSimples()
        {

            return _permisoDAL.ObtenerTodosLosComponentes(soloFamilias: false, soloPerfiles: false);
        }

        public bool ExisteEnArbol(Perfil raiz, int idBuscar)
        {
            if (raiz.Id == idBuscar) return true;

            foreach (Perfil hijo in raiz.Hijos)
            {
                if (ExisteEnArbol(hijo, idBuscar)) return true;
            }
            return false;
        }

        public bool AlgunComponenteYaExisteEnArbol(Perfil raiz, Perfil subArbolAAgregar)
        {
            if (ExisteEnArbol(raiz, subArbolAAgregar.Id)) return true;

            foreach (Perfil hijo in subArbolAAgregar.Hijos)
            {
                if (AlgunComponenteYaExisteEnArbol(raiz, hijo)) return true;
            }
            return false;
        }

        public void ArmarArbolComponente(Perfil componentePadre)
        {

            componentePadre.VaciarHijos();

            var hijos = _permisoDAL.ObtenerHijos(componentePadre.Id);
            foreach (var hijo in hijos)
            {
                componentePadre.AgregarHijo(hijo);

                if (hijo is Familia)
                    ArmarArbolComponente(hijo);
            }
        }

        public void GuardarFamilia(Familia familia, bool esPerfil)
        {
            try
            {

                _permisoDAL.EliminarRelacionesPadre(familia.Id);

                foreach (var hijo in familia.Hijos)
                {

                    _permisoDAL.InsertarRelacion(familia.Id, hijo.Id);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error en BLL al guardar el árbol: " + ex.Message);
            }
        }

        public void EliminarComponente(int idComponente)
        {
            if (_permisoDAL.TieneUsuariosAsignados(idComponente))
                throw new Exception("No se puede eliminar este rol porque tiene usuarios asignados.");

            _permisoDAL.EliminarRelacionesComponente(idComponente);
            _permisoDAL.EliminarAsignacionesUsuario(idComponente);
            _permisoDAL.EliminarComponenteBase(idComponente);
        }

        public void EliminarFamilia(int idFamilia)
        {
            if (_permisoDAL.TieneUsuariosAsignados(idFamilia))
                throw new Exception("No se puede eliminar este rol porque tiene usuarios asignados.");

            _permisoDAL.EliminarRelacionesComponente(idFamilia);
            _permisoDAL.EliminarComponenteBase(idFamilia);
        }

        public List<Perfil> ObtenerPerfiles()
        {

            return _permisoDAL.ObtenerTodosLosComponentes(soloFamilias: true, soloPerfiles: true);
        }

        public int InsertarComponente(string nombre, bool esFamilia, bool esPerfil)
        {
            if (_permisoDAL.ExisteConNombre(nombre))
                throw new Exception($"Ya existe un componente con el nombre '{nombre}'.");

            return _permisoDAL.InsertarComponente(nombre, esFamilia, esPerfil);
        }
    }
}
