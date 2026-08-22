using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL;
using SERVICIOS;

namespace CAFECCINO_INGENIERIA_DE_SOFTWARE
{
    public partial class frmBitacoraCambios : Form, IObserver
    {
        private BLLBitacoraUsuario _bll = new BLLBitacoraUsuario();
        Traductor traductor = new Traductor();
        public frmBitacoraCambios()
        {
            InitializeComponent();
        }
        public void ActualizarIdioma(string idioma)
        {


            traductor.TraducirFormulario(this, int.Parse(idioma));
        }
        private void frmBitacoraCambios_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = _bll.ObtenerCambios();

        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione un registro para restaurar.");
                return;
            }

            int idBitacora = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["IdBitacora"].Value);
            string operacion = dataGridView1.SelectedRows[0].Cells["Operacion"].Value.ToString();

            if (operacion == "Restauracion")
            {
                MessageBox.Show("No se puede restaurar una restauración.");
                return;
            }

            DialogResult confirm = MessageBox.Show(
                "¿Desea restaurar este registro al estado anterior?",
                "Confirmar restauración",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirm == DialogResult.Yes)
            {
                try
                {
                    _bll.RestaurarUsuario(idBitacora);
                    MessageBox.Show("Usuario restaurado correctamente.");
                    dataGridView1.DataSource = _bll.ObtenerCambios();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al restaurar: " + ex.Message);
                }
            }
        }

        private void frmBitacoraCambios_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = _bll.ObtenerCambios();
        }
    }
}
