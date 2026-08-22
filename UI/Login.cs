using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BE;
using BLL;
using SERVICIOS;

namespace CAFECCINO_INGENIERIA_DE_SOFTWARE
{
    public partial class Login : Form, IObserver
    {
        BLLUsuario NegociosUsuario = new BLLUsuario();
        BLLEvento bitacora = new BLLEvento();
        Traductor traductor = new Traductor();
        BLLIdioma iidioma = new BLLIdioma();
        private bool CambioManual = false; 
        public Login()
        {
            InitializeComponent();
        }

        public void ActualizarIdioma(string idioma)
        {
            traductor.TraducirFormulario(this, int.Parse(idioma));
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text) || string.IsNullOrWhiteSpace(txtContraseña.Text))
            {
                MessageBox.Show("Llene todos los espacios");
                return;
            }
            if (Singleton.Instancia.Usuario != null)
            {
                MessageBox.Show("Ya hay una sesión activa.");
                return;
            }

            Usuario userIntento = new Usuario
            {
                NombreUsuario = txtNombre.Text,
                Contraseña = txtContraseña.Text
            };

           
            Usuario userLogueado = NegociosUsuario.Login(userIntento);

            if (userLogueado != null)
            {
               
                Singleton.Instancia.IniciarSesion(userLogueado);

                bitacora.RegistrarEvento(txtNombre.Text, "Inicio de sesión exitoso", "Login", "Baja");

                frmInicio principal = Application.OpenForms.OfType<frmInicio>().FirstOrDefault();
                if (principal != null)
                {
                    principal.AplicarPermisosUsuario(userLogueado.Perfil.Hijos.ToList());
                }

                ModificarMenu();
                MessageBox.Show("Inicio de sesión exitoso.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Hide();
            }
            else
            {
                MessageBox.Show("Nombre de usuario o contraseña incorrectos.", "Error de autenticación", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        public void RecargarIdiomas()
        {
            CambioManual = false;
            cmbCambiarIdioma.DataSource = null;
            cmbCambiarIdioma.DataSource = iidioma.ListarIdiomas();
            cmbCambiarIdioma.DisplayMember = "Nombre";
            cmbCambiarIdioma.ValueMember = "IdIdioma";
            cmbCambiarIdioma.SelectedIndex = -1;
            CambioManual = true;
        }
        private void Login_Load(object sender, EventArgs e)
        {
            CambioManual = false; 
            cmbCambiarIdioma.DisplayMember = "Nombre";
            cmbCambiarIdioma.ValueMember = "IdIdioma";
            cmbCambiarIdioma.DataSource = iidioma.ListarIdiomas();
            cmbCambiarIdioma.SelectedIndex = -1;
            CambioManual = true; 
            Singleton.Instancia.Suscribir(this);
        }

        private void Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();

            }
            Singleton.Instancia.Desuscribir(this);
        }
        public void ModificarMenu()
        {
            frmInicio parent = this.MdiParent as frmInicio;
            parent.cerrarSesionToolStripMenuItem.Visible = true;
            parent.administradorToolStripMenuItem.Visible = true;
        }


        private void Login_FormClosed(object sender, FormClosedEventArgs e)
        {
            Singleton.Instancia.Desuscribir(this);
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       

        private void cmbCambiarIdioma_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (!CambioManual) return;
            if (cmbCambiarIdioma.SelectedValue == null) return;

            Singleton.Instancia.CambiarIdioma(
                Convert.ToInt32(cmbCambiarIdioma.SelectedValue));
        }

        private void Login_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                this.BeginInvoke(new Action(() =>
                {
                    CambioManual = false;
                    traductor.TraducirFormulario(this, Singleton.Instancia.IdiomaActual);
                    CambioManual = true;
                }));
            }
        }
    }
}

