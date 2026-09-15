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
    public partial class frmVerificacionIntegridad : Form, IObserver
    {
        private readonly BLLUsuario _usuarioBLL = new BLLUsuario();
        private readonly BLLBackup _backupBLL = new BLLBackup();
        Traductor traductor = new Traductor();

        public frmVerificacionIntegridad()
        {
            InitializeComponent();
        }

        public void ActualizarIdioma(string idioma)
        {
            traductor.TraducirFormulario(this, int.Parse(idioma));
        }

        private void frmVerificacionIntegridad_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                ActualizarEstado();
            }
        }

        private void ActualizarEstado()
        {
            bool esAdmin = Singleton.Instancia.EsAdministrador();

            btnRecalcular.Enabled = esAdmin;
            btnRestaurar.Enabled = esAdmin && _backupBLL.ExisteBackupDisponible();

            lblAviso.Text = esAdmin
                ? ""
                : "Debe iniciar sesión como administrador para restaurar o recalcular.";

            try
            {
                bool integro = _usuarioBLL.ValidarIntegridad();
                List<int> filasAlteradas = _usuarioBLL.ObtenerFilasAlteradas().Where(id => id > 0).ToList();

                if (integro)
                {
                    lblEstado.Text = "Estado: OK. La tabla Usuario no presenta alteraciones.";
                }
                else if (filasAlteradas.Count > 0)
                {
                    lblEstado.Text = "Estado: ALTERADO.\nDNI afectados: " + string.Join(", ", filasAlteradas);
                }
                else
                {
                    lblEstado.Text = "Estado: ALTERADO.\nSe modificó la cantidad de filas de la tabla (alta o baja fuera del sistema).";
                }
            }
            catch (Exception ex)
            {
                lblEstado.Text = "No se pudo verificar el estado: " + ex.Message;
            }
        }

        private void btnRecalcular_Click(object sender, EventArgs e)
        {
            if (!Singleton.Instancia.EsAdministrador())
            {
                MessageBox.Show("Solo un administrador puede recalcular los dígitos verificadores.");
                return;
            }

            DialogResult confirm = MessageBox.Show(
                "Esto acepta el estado actual de la tabla Usuario como válido y genera un nuevo backup de la base de datos.\n\n¿Desea continuar?",
                "Confirmar recálculo",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes) return;

            try
            {
                _backupBLL.Recalcular();
                MessageBox.Show("Dígitos recalculados y backup generado correctamente.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al recalcular: " + ex.Message);
            }

            ActualizarEstado();
        }

        private void btnRestaurar_Click(object sender, EventArgs e)
        {
            if (!Singleton.Instancia.EsAdministrador())
            {
                MessageBox.Show("Solo un administrador puede restaurar la base de datos.");
                return;
            }

            if (!_backupBLL.ExisteBackupDisponible())
            {
                MessageBox.Show("No hay ningún backup generado todavía. Use 'Recalcular' primero para generar uno.");
                return;
            }

            DialogResult confirm = MessageBox.Show(
                "Esto va a REEMPLAZAR toda la base de datos actual por el último backup generado (" + _backupBLL.ObtenerRutaBackup() + ").\n" +
                "Se perderá cualquier cambio posterior a ese backup y se cerrará la sesión actual.\n\n¿Desea continuar?",
                "Confirmar restauración",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirm != DialogResult.Yes) return;

            try
            {
                _backupBLL.Restaurar();
                MessageBox.Show("Base de datos restaurada correctamente desde el backup. Debe iniciar sesión nuevamente.");
                CerrarSesionYPedirLogin();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al restaurar: " + ex.Message);
                ActualizarEstado();
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
                parent.verificacionIntegridadToolStripMenuItem.Visible = false;

                parent.gestionDeFamilias.Hide();
                parent.gestionDePerfiles.Hide();
                parent.administradorIdiomas.Hide();

                Hide();

                parent.login.MdiParent = parent;
                parent.login.Show();
            }
            else
            {
                Hide();
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Hide();
        }

        private void frmVerificacionIntegridad_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }

        private void frmVerificacionIntegridad_Load(object sender, EventArgs e)
        {

        }
    }
}
