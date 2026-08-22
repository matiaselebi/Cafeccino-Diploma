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
    public partial class CerrarSesion : Form
    {
        BLLEvento bitacora = new BLLEvento();
        public CerrarSesion()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            string nombreUsuario = Singleton.Instancia.Usuario?.Nombre ?? "Desconocido";

            Singleton.Instancia.CerrarSesion();

            ModificarMenu();

            bitacora.RegistrarEvento(
                nombreUsuario,
                "Cierre de sesión",
                "Login",
                "Baja");

            MessageBox.Show("Sesion Cerrada");
            Hide();
        }
        public void ModificarMenu()
        {
            frmInicio parent = this.MdiParent as frmInicio;

            parent.cerrarSesionToolStripMenuItem.Visible = false;
            parent.administradorToolStripMenuItem.Visible = false;

            parent.login.Hide();
            parent.gestionDeFamilias.Hide();
            parent.gestionDePerfiles.Hide();
            parent.administradorIdiomas.Hide();
      
        }

        private void CerrrarSesion_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Hide();
        }

        private void CerrarSesion_Load(object sender, EventArgs e)
        {

        }
    }
}
