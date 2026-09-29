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
    public partial class frmVerificacionIntegridad : Form, IObserver
    {
        private readonly BLLUsuario bllUsuario = new BLLUsuario();
        private readonly BLLBackup bllBackup = new BLLBackup();
        private readonly BLLAuditoriaCambios bllBitacora = new BLLAuditoriaCambios();
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
            btnRestaurar.Enabled = esAdmin && bllBackup.ExisteBackupDisponible();

            lblAviso.Text = esAdmin
                ? ""
                : "Debe iniciar sesión como administrador para restaurar o recalcular.";

            try
            {
                bool integro = bllUsuario.ValidarIntegridad();
                List<int> filasAlteradas = bllUsuario.ObtenerFilasAlteradas().Where(id => id > 0).ToList();

                if (integro)
                {
                    lblEstado.Text = "Estado: OK. La tabla Usuario no presenta alteraciones.";
                }
                else if (filasAlteradas.Count > 0)
                {
                    lblEstado.Text = ArmarDetalleAlteraciones(filasAlteradas);
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

        private string ArmarDetalleAlteraciones(List<int> filasAlteradas)
        {
            var texto = new System.Text.StringBuilder();
            texto.AppendLine("Estado: ALTERADO.");
            texto.AppendLine("DNI afectados: " + string.Join(", ", filasAlteradas));
            texto.AppendLine();

            foreach (int dni in filasAlteradas)
            {
                Usuario actual = bllUsuario.ObtenerPorDNI(dni);
                texto.AppendLine($"--- DNI {dni} ---");

                if (actual == null)
                {
                    texto.AppendLine("Ya no existe en la tabla Usuario (pudo haber sido borrado directamente por fuera del sistema).");
                    texto.AppendLine();
                    continue;
                }

                texto.AppendLine("Datos actuales:");
                texto.AppendLine($"  Usuario: {actual.NombreUsuario} | Nombre: {actual.Nombre} | Apellido: {actual.Apellido} | Mail: {actual.Mail} | Tel: {actual.NumTelefono} | Rol: {actual.Rol}");

                AuditoriaCambios ultimo = bllBitacora.ObtenerUltimoPorDni(dni);
                if (ultimo == null || string.IsNullOrEmpty(ultimo.DatosNuevos))
                {
                    texto.AppendLine("No hay un registro previo en Auditoria de Cambios para este DNI - no se puede determinar que campo cambio, solo que el digito verificador no coincide.");
                }
                else
                {
                    Usuario ultimoRegistrado = bllBitacora.ParsearSnapshot(ultimo.DatosNuevos);
                    List<string> diferencias = CompararUsuarios(ultimoRegistrado, actual);

                    if (diferencias.Count == 0)
                    {
                        texto.AppendLine("Los datos actuales coinciden con el ultimo registro de auditoria - el digito verificador fue alterado sin que cambien los datos visibles (o el cambio no paso por el sistema).");
                    }
                    else
                    {
                        texto.AppendLine($"Campo(s) modificado(s) respecto al ultimo registro de auditoria ({ultimo.FechaHora:dd/MM/yyyy HH:mm}):");
                        foreach (string diferencia in diferencias)
                            texto.AppendLine("  " + diferencia);
                    }
                }
                texto.AppendLine();
            }

            return texto.ToString();
        }

        private List<string> CompararUsuarios(Usuario anterior, Usuario actual)
        {
            var diferencias = new List<string>();

            void Comparar(string campo, object valorAnterior, object valorActual)
            {
                string anteriorTexto = valorAnterior?.ToString() ?? "";
                string actualTexto = valorActual?.ToString() ?? "";
                if (anteriorTexto != actualTexto)
                    diferencias.Add($"{campo}: '{anteriorTexto}' -> '{actualTexto}'");
            }

            Comparar("Usuario", anterior.NombreUsuario, actual.NombreUsuario);
            Comparar("Nombre", anterior.Nombre, actual.Nombre);
            Comparar("Apellido", anterior.Apellido, actual.Apellido);
            Comparar("Mail", anterior.Mail, actual.Mail);
            Comparar("Tel", anterior.NumTelefono, actual.NumTelefono);
            Comparar("Rol", anterior.Rol, actual.Rol);

            return diferencias;
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
                bllBackup.Recalcular();
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

            if (!bllBackup.ExisteBackupDisponible())
            {
                MessageBox.Show("No hay ningún backup generado todavía. Use 'Recalcular' primero para generar uno.");
                return;
            }

            DialogResult confirm = MessageBox.Show(
                "Esto va a REEMPLAZAR toda la base de datos actual por el último backup generado (" + bllBackup.ObtenerRutaBackup() + ").\n" +
                "Se perderá cualquier cambio posterior a ese backup y se cerrará la sesión actual.\n\n¿Desea continuar?",
                "Confirmar restauración",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirm != DialogResult.Yes) return;

            try
            {
                bllBackup.Restaurar();
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
                parent.ventasToolStripMenuItem.Visible = false;
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
