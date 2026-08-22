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
    public partial class frmEventos : Form, IObserver
    {
        BLLEvento bllevento = new BLLEvento();
        Traductor traductor = new Traductor();
        public frmEventos()
        {
            InitializeComponent();
        }

        public void ActualizarIdioma(string idioma)
        {
            traductor.TraducirFormulario(this, int.Parse(idioma));
        }

        private void frmEventos_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = bllevento.ObtenerEventos();
            
            cmbModulo.Items.Add("Todos");
            cmbModulo.Items.Add("Login");
            cmbModulo.Items.Add("Usuarios");
            cmbModulo.Items.Add("Permisos");
            cmbEvento.Items.Add("Todos");
            cmbEvento.Items.Add("Inicio de sesión exitoso");
            cmbEvento.Items.Add("Cierre de sesión");

            cmbEvento.SelectedIndex = 0;
            cmbModulo.SelectedIndex = 0;

            cmbCriticidad.Items.Add("Todas");
            cmbCriticidad.Items.Add("Alta");
            cmbCriticidad.Items.Add("Media");
            cmbCriticidad.Items.Add("Baja");

            cmbCriticidad.SelectedIndex = 0;
        }

        private void Buscar_Click(object sender, EventArgs e)
        {
            string modulo = cmbModulo.Text;
            string criticidad = cmbCriticidad.Text;
            string fecha = dateTimePicker1.Value.ToString("yyyy-MM-dd");
          string evento = cmbEvento.Text;

            if (modulo == "Todos")
                modulo = "";

            if (criticidad == "Todas")
                criticidad = "";

            if (evento == "Todos")
                evento = "";

            BLLEvento bll = new BLLEvento();

            dataGridView1.DataSource = bll.FiltrarEventos(modulo, criticidad,  evento, fecha);
        }

        private void frmEventos_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();

            }
        }

        private void frmEventos_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
            
                traductor.TraducirFormulario(this, Singleton.Instancia.IdiomaActual);
            }
        }
    }
}
