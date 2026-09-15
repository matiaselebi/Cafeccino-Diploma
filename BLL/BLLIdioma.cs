using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DAL;
using SERVICIOS;

namespace BLL
{
    public class BLLIdioma
    {
        DALIdioma dal = new DALIdioma();
       
        public DataTable ListarIdiomas()
        {
            return dal.ListarIdiomas();
        }

        public void AgregarIdioma(string nombre)
        {
            var idioma = new Idioma { Nombre = nombre };
            dal.AgregarIdioma(idioma);
        }

        public DataTable ObtenerTraducciones(string formulario, int idIdioma)
        {
            return dal.ObtenerTraducciones(formulario, idIdioma);
        }

    
        public void GuardarTraduccion(Traduccion traduccion)
        {
            dal.GuardarTraduccion(traduccion);
        }

        public string ObtenerTexto(int idIdioma,
                                   string formulario,
                                   string control)
        {
            return dal.ObtenerTexto(idIdioma, formulario, control);
        }

        public void RegistrarControl(string formulario, string nombreControl, string textoOriginal)
        {
            dal.RegistrarControl(formulario, nombreControl, textoOriginal);
        }
        public DataTable ObtenerControles(string formulario)
        {
            return dal.ObtenerControles(formulario);
        }
    }
}
