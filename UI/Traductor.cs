using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL;

namespace CAFECCINO_INGENIERIA_DE_SOFTWARE
{
    public class Traductor
    {
        private readonly BLLIdioma bllIdioma = new BLLIdioma();

        

        public void TraducirFormulario(Form formulario, int idIdioma)
        {
            TraducirControles(formulario.Controls, formulario.Name, idIdioma);

            
            foreach (Control control in formulario.Controls)
            {
                if (control is MenuStrip menuStrip)
                    TraducirMenuItems(menuStrip.Items, formulario.Name, idIdioma);
            }
        }

        private void TraducirControles(Control.ControlCollection controles,
                                       string nombreFormulario,
                                       int idIdioma)
        {
            foreach (Control control in controles)
            {
                string texto = bllIdioma.ObtenerTexto(idIdioma, nombreFormulario, control.Name);
                if (!string.IsNullOrEmpty(texto))
                    control.Text = texto;
                if (control.HasChildren)
                    TraducirControles(control.Controls, nombreFormulario, idIdioma);
            }
        }

        private void TraducirMenuItems(ToolStripItemCollection items,
                                       string nombreFormulario,
                                       int idIdioma)
        {
            foreach (ToolStripItem item in items)
            {
                string texto = bllIdioma.ObtenerTexto(idIdioma, nombreFormulario, item.Name);
                if (!string.IsNullOrEmpty(texto))
                    item.Text = texto;

                
                if (item is ToolStripMenuItem menuItem && menuItem.DropDownItems.Count > 0)
                    TraducirMenuItems(menuItem.DropDownItems, nombreFormulario, idIdioma);
            }
        }
    }
}
