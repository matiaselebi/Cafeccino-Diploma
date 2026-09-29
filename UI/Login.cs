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
        BLLBitacoraEventos bitacora = new BLLBitacoraEventos();
        Traductor traductor = new Traductor();
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


            Usuario userLogueado;
            try
            {
                userLogueado = NegociosUsuario.Login(userIntento);
            }
            catch (Exception ex)
            {
                bitacora.RegistrarEvento(txtNombre.Text, "Intento de inicio de sesión con usuario bloqueado", "Login", "Alta");
                MessageBox.Show(ex.Message, "Usuario bloqueado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

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
                bitacora.RegistrarEvento(txtNombre.Text, "Intento de inicio de sesión fallido", "Login", "Media");
                MessageBox.Show("Nombre de usuario o contraseña incorrectos.", "Error de autenticación", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void Login_Load(object sender, EventArgs e)
        {
            GestorIdiomas.Instancia.Suscribir(this);
        }

        private void Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();

            }
            GestorIdiomas.Instancia.Desuscribir(this);
        }
        public void ModificarMenu()
        {
            frmInicio parent = this.MdiParent as frmInicio;
            parent.iniciarSesionToolStripMenuItem.Visible = false;
            parent.cerrarSesionToolStripMenuItem.Visible = true;
            parent.administradorToolStripMenuItem.Visible = true;
            parent.cambiarContraseñaToolStripMenuItem.Visible = true;
            parent.ventasToolStripMenuItem.Visible = true;
            parent.verificacionIntegridadToolStripMenuItem.Visible = Singleton.Instancia.EsAdministrador();
        }


        private void Login_FormClosed(object sender, FormClosedEventArgs e)
        {
            GestorIdiomas.Instancia.Desuscribir(this);
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }



        private void Login_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                txtNombre.Clear();
                txtContraseña.Clear();

                traductor.TraducirFormulario(this, GestorIdiomas.Instancia.IdiomaActual);
            }
        }
    }
}

