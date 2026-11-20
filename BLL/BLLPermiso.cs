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
        private DALPermiso dalPermiso = new DALPermiso();

        public Perfil CargarArbolCompleto(Perfil perfil)
        {
            List<Perfil> hijos = dalPermiso.ObtenerHijos(perfil.Id);

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

            return dalPermiso.ObtenerTodosLosComponentes(soloFamilias: true, soloPerfiles: false);
        }
        public List<Perfil> ObtenerPermisosYPerfiles()
        {
            return dalPermiso.ObtenerPermisosYPerfiles();
        }
        public List<Perfil> ObtenerPermisosSimples()
        {

            return dalPermiso.ObtenerTodosLosComponentes(soloFamilias: false, soloPerfiles: false);
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

            var hijos = dalPermiso.ObtenerHijos(componentePadre.Id);
            foreach (var hijo in hijos)
            {
                componentePadre.AgregarHijo(hijo);

                if (hijo is Familia)
                    ArmarArbolComponente(hijo);
            }
        }

        private readonly BLLBitacoraEventos bllBitacora_389ME = new BLLBitacoraEventos();
        private readonly BLLDigitoVerificador bllDigito_389ME = new BLLDigitoVerificador();

        public void GuardarFamilia(Familia familia, bool esPerfil)
        {
            try
            {

                dalPermiso.EliminarRelacionesPadre(familia.Id);

                foreach (var hijo in familia.Hijos)
                {

                    dalPermiso.InsertarRelacion(familia.Id, hijo.Id);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error en BLL al guardar el árbol: " + ex.Message);
            }
            bllDigito_389ME.Recalcular_389ME("Permiso_Permiso");

            bllBitacora_389ME.RegistrarEvento_389ME("Modificación de permisos",
                $"{familia.Nombre}: {familia.Hijos.Count} permiso(s) asignado(s)", "Permisos", "Alta");
        }

        public void EliminarComponente(int idComponente)
        {
            if (dalPermiso.TieneUsuariosAsignados(idComponente))
                throw new Exception("No se puede eliminar este rol porque tiene usuarios asignados.");

            dalPermiso.EliminarRelacionesComponente(idComponente);
            dalPermiso.EliminarAsignacionesUsuario(idComponente);
            dalPermiso.EliminarComponenteBase(idComponente);
            bllDigito_389ME.Recalcular_389ME("Permiso", "Permiso_Permiso", "Usuario_Permiso");

            bllBitacora_389ME.RegistrarEvento_389ME("Baja de perfil o familia", $"Código {idComponente}", "Permisos", "Alta");
        }

        public void EliminarFamilia(int idFamilia)
        {
            if (dalPermiso.TieneUsuariosAsignados(idFamilia))
                throw new Exception("No se puede eliminar este rol porque tiene usuarios asignados.");

            dalPermiso.EliminarRelacionesComponente(idFamilia);
            dalPermiso.EliminarComponenteBase(idFamilia);
            bllDigito_389ME.Recalcular_389ME("Permiso", "Permiso_Permiso");

            bllBitacora_389ME.RegistrarEvento_389ME("Baja de perfil o familia", $"Código {idFamilia}", "Permisos", "Alta");
        }

        public List<Perfil> ObtenerPerfiles()
        {

            return dalPermiso.ObtenerTodosLosComponentes(soloFamilias: true, soloPerfiles: true);
        }

        public int InsertarComponente(string nombre, bool esFamilia, bool esPerfil)
        {
            if (dalPermiso.ExisteConNombre(nombre))
                throw new Exception($"Ya existe un componente con el nombre '{nombre}'.");

            int codigo_389ME = dalPermiso.InsertarComponente(nombre, esFamilia, esPerfil);
            bllDigito_389ME.Recalcular_389ME("Permiso");

            string tipo_389ME = esPerfil ? "perfil" : esFamilia ? "familia" : "permiso";
            bllBitacora_389ME.RegistrarEvento_389ME("Alta de " + tipo_389ME, $"{nombre} - Código {codigo_389ME}", "Permisos", "Media");
            return codigo_389ME;
        }
    }
}
