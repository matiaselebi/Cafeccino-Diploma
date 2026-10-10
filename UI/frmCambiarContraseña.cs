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
    public partial class frmCambiarContraseña : Form, IObserver
    {
        private BLLUsuario bllUsuario = new BLLUsuario();
        private BLLBitacoraEventos bllBitacora = new BLLBitacoraEventos();
        Traductor traductor = new Traductor();

        public frmCambiarContraseña()
        {
            InitializeComponent();
        }

        public void ActualizarIdioma(string idioma)
        {
            traductor.TraducirFormulario(this, int.Parse(idioma));
        }

        private void btnCambiar_Click(object sender, EventArgs e)
        {
            string usuario = txtUsuario.Text.Trim();
            string nuevaContraseña = txtNuevaContraseña.Text;
            string confirmarContraseña = txtConfirmarContraseña.Text;

            if (string.IsNullOrWhiteSpace(usuario) || string.IsNullOrWhiteSpace(nuevaContraseña) || string.IsNullOrWhiteSpace(confirmarContraseña))
            {
                MessageBox.Show("Por favor, complete todos los campos.");
                return;
            }

            if (nuevaContraseña != confirmarContraseña)
            {
                MessageBox.Show("La nueva contraseña y su confirmación no coinciden.");
                return;
            }

            try
            {
                bllUsuario.CambiarContraseña(usuario, nuevaContraseña);

                bllBitacora.RegistrarEvento(usuario, "Cambio de contraseña", "Usuario", "Modificación");

                MessageBox.Show("Contraseña cambiada con éxito. Debe iniciar sesión nuevamente.");

                CerrarSesionYPedirLogin();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cambiar la contraseña: " + ex.Message);
            }
        }

        private void CerrarSesionYPedirLogin()
        {
            Singleton.Instancia.CerrarSesion();

            frmInicio parent = this.MdiParent as frmInicio;
            if (parent != null)
            {
                parent.cerrarSesionToolStripMenuItem.Visible = false;
                parent.administradorToolStripMenuItem.Visible = false;
                parent.cambiarContraseñaToolStripMenuItem.Visible = false;
                parent.ventasToolStripMenuItem.Visible = false;
                parent.verificacionIntegridadToolStripMenuItem.Visible = false;

                parent.gestionDeFamilias.Hide();
                parent.gestionDePerfiles.Hide();
                parent.administradorIdiomas.Hide();
                parent.verificacionIntegridad.Hide();

                txtUsuario.Clear();
                txtNuevaContraseña.Clear();
                txtConfirmarContraseña.Clear();
                Hide();

                parent.login.MdiParent = parent;
                parent.login.Show();
            }
            else
            {
                txtUsuario.Clear();
                txtNuevaContraseña.Clear();
                txtConfirmarContraseña.Clear();
                Hide();
            }
        }

        private void frmCambiarContraseña_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }

        private void frmCambiarContraseña_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                txtUsuario.Clear();
                txtNuevaContraseña.Clear();
                txtConfirmarContraseña.Clear();
            }
        }
    }
}
