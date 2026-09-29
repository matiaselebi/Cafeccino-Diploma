using System;
using System.Windows.Forms;
using BLL;
using SERVICIOS;

namespace CAFECCINO_INGENIERIA_DE_SOFTWARE
{
    public partial class frmCambiarIdioma : Form, IObserver
    {
        private readonly BLLIdioma bllIdioma_389ME = new BLLIdioma();
        Traductor traductor = new Traductor();

        // Mientras se llena el combo el evento SelectedIndexChanged se dispara solo,
        // y sin esta bandera el idioma se cambiaria sin que el usuario elija nada.
        private bool cambioManual_389ME = false;

        public frmCambiarIdioma()
        {
            InitializeComponent();
        }

        public void ActualizarIdioma(string idioma)
        {
            traductor.TraducirFormulario(this, int.Parse(idioma));
        }

        private void frmCambiarIdioma_Load_389ME(object sender_389ME, EventArgs e_389ME)
        {
            CargarIdiomas_389ME();
        }

        private void CargarIdiomas_389ME()
        {
            cambioManual_389ME = false;
            cmbIdiomas_389ME.DataSource = null;
            cmbIdiomas_389ME.DataSource = bllIdioma_389ME.ListarIdiomas();
            cmbIdiomas_389ME.DisplayMember = "Nombre";
            cmbIdiomas_389ME.ValueMember = "IdIdioma";
            cmbIdiomas_389ME.SelectedValue = GestorIdiomas.Instancia.IdiomaActual;
            cambioManual_389ME = true;
        }

        private void cmbIdiomas_SelectedIndexChanged_389ME(object sender_389ME, EventArgs e_389ME)
        {
            if (!cambioManual_389ME) return;
            if (cmbIdiomas_389ME.SelectedValue == null) return;

            GestorIdiomas.Instancia.CambiarIdioma(Convert.ToInt32(cmbIdiomas_389ME.SelectedValue));
        }

        private void frmCambiarIdioma_FormClosing_389ME(object sender_389ME, FormClosingEventArgs e_389ME)
        {
            if (e_389ME.CloseReason == CloseReason.UserClosing)
            {
                e_389ME.Cancel = true;
                Hide();
            }
        }

        private void frmCambiarIdioma_VisibleChanged_389ME(object sender_389ME, EventArgs e_389ME)
        {
            if (this.Visible)
            {
                CargarIdiomas_389ME();
                traductor.TraducirFormulario(this, GestorIdiomas.Instancia.IdiomaActual);
            }
        }
    }
}
