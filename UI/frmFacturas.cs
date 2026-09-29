using System;
using BE;
using BLL;
using SERVICIOS;

namespace CAFECCINO_INGENIERIA_DE_SOFTWARE
{
    public partial class frmFacturas : System.Windows.Forms.Form, IObserver
    {
        private readonly BLLFactura bllFactura_389ME = new BLLFactura();
        Traductor traductor = new Traductor();

        public frmFacturas()
        {
            InitializeComponent();
        }

        public void ActualizarIdioma(string idioma)
        {
            traductor.TraducirFormulario(this, int.Parse(idioma));
        }

        private void frmFacturas_Load_389ME(object sender_389ME, EventArgs e_389ME)
        {
            ActualizarFacturas_389ME();
        }

        private void ActualizarFacturas_389ME()
        {
            dgvFacturas_389ME.DataSource = bllFactura_389ME.Listar_389ME();
            if (dgvFacturas_389ME.Columns.Contains("DVH_389ME"))
                dgvFacturas_389ME.Columns["DVH_389ME"].Visible = false;

            dgvDetalle_389ME.DataSource = null;
        }

        private void dgvFacturas_SelectionChanged_389ME(object sender_389ME, EventArgs e_389ME)
        {
            if (dgvFacturas_389ME.CurrentRow == null)
            {
                dgvDetalle_389ME.DataSource = null;
                return;
            }

            int codFactura_389ME = Convert.ToInt32(dgvFacturas_389ME.CurrentRow.Cells["CodFactura_389ME"].Value);
            dgvDetalle_389ME.DataSource = bllFactura_389ME.ObtenerDetalle_389ME(codFactura_389ME);

            if (dgvDetalle_389ME.Columns.Contains("CodFactura_389ME"))
                dgvDetalle_389ME.Columns["CodFactura_389ME"].Visible = false;
            if (dgvDetalle_389ME.Columns.Contains("CodProducto_389ME"))
                dgvDetalle_389ME.Columns["CodProducto_389ME"].Visible = false;
            if (dgvDetalle_389ME.Columns.Contains("DVH_389ME"))
                dgvDetalle_389ME.Columns["DVH_389ME"].Visible = false;
        }

        private void frmFacturas_FormClosing_389ME(object sender_389ME, System.Windows.Forms.FormClosingEventArgs e_389ME)
        {
            if (e_389ME.CloseReason == System.Windows.Forms.CloseReason.UserClosing)
            {
                e_389ME.Cancel = true;
                Hide();
            }
        }

        private void frmFacturas_VisibleChanged_389ME(object sender_389ME, EventArgs e_389ME)
        {
            if (this.Visible)
            {
                ActualizarFacturas_389ME();
                traductor.TraducirFormulario(this, GestorIdiomas.Instancia.IdiomaActual);
            }
        }
    }
}
