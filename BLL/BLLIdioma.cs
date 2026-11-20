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

        private readonly BLLBitacoraEventos bllBitacora_389ME = new BLLBitacoraEventos();
        private readonly BLLDigitoVerificador bllDigito_389ME = new BLLDigitoVerificador();

        public void AgregarIdioma(string nombre)
        {
            foreach (DataRow fila_389ME in dal.ListarIdiomas().Rows)
                if (string.Equals(fila_389ME["Nombre"].ToString().Trim(), nombre.Trim(), StringComparison.OrdinalIgnoreCase))
                    throw new Exception("Ya existe un idioma con ese nombre.");

            var idioma = new Idioma { Nombre = nombre };
            dal.AgregarIdioma(idioma);
            bllDigito_389ME.Recalcular_389ME("Idioma");

            bllBitacora_389ME.RegistrarEvento_389ME("Alta de idioma", nombre, "Idiomas", "Baja");
        }

        public DataTable ObtenerTraducciones(string formulario, int idIdioma)
        {
            return dal.ObtenerTraducciones(formulario, idIdioma);
        }

    
        public void GuardarTraduccion(Traduccion traduccion)
        {
            dal.GuardarTraduccion(traduccion);
            bllDigito_389ME.Recalcular_389ME("Traduccion");
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
            bllDigito_389ME.Recalcular_389ME("ControlIdioma");
        }
        public DataTable ObtenerControles(string formulario)
        {
            return dal.ObtenerControles(formulario);
        }
    }
}
