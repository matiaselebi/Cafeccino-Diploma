using System;
using System.Linq;
using System.Windows.Forms;
using BE;
using BLL;
using SERVICIOS;

namespace CAFECCINO_INGENIERIA_DE_SOFTWARE
{
    public partial class frmGestionClientes : Form, IObserver
    {
        private readonly BLLCliente bllCliente_389ME = new BLLCliente();
        Traductor traductor = new Traductor();

        public frmGestionClientes()
        {
            InitializeComponent();
        }

        public void ActualizarIdioma(string idioma)
        {
            traductor.TraducirFormulario(this, int.Parse(idioma));
        }

        private void frmGestionClientes_Load_389ME(object sender_389ME, EventArgs e_389ME)
        {
            ActualizarGrilla_389ME();
        }

        private void ActualizarGrilla_389ME()
        {
            dgvClientes_389ME.DataSource = null;
            dgvClientes_389ME.DataSource = bllCliente_389ME.Listar_389ME();

            if (dgvClientes_389ME.Columns.Contains("DVH_389ME"))
                dgvClientes_389ME.Columns["DVH_389ME"].Visible = false;
        }

        private void btnAgregar_Click_389ME(object sender_389ME, EventArgs e_389ME)
        {
            if (!ValidarCampos_389ME(out int dni_389ME, out int telefono_389ME))
                return;

            try
            {
                bllCliente_389ME.Registrar_389ME(ArmarCliente_389ME(dni_389ME, telefono_389ME));

                MessageBox.Show("Cliente agregado correctamente.");
                LimpiarCampos_389ME();
                ActualizarGrilla_389ME();
            }
            catch (Exception ex_389ME)
            {
                MessageBox.Show("Error al agregar el cliente: " + ex_389ME.Message);
            }
        }

        private void btnModificar_Click_389ME(object sender_389ME, EventArgs e_389ME)
        {
            if (!ValidarCampos_389ME(out int dni_389ME, out int telefono_389ME))
                return;

            try
            {
                bllCliente_389ME.Modificar_389ME(ArmarCliente_389ME(dni_389ME, telefono_389ME));

                MessageBox.Show("Cliente modificado correctamente.");
                LimpiarCampos_389ME();
                ActualizarGrilla_389ME();
            }
            catch (Exception ex_389ME)
            {
                MessageBox.Show("Error al modificar el cliente: " + ex_389ME.Message);
            }
        }

        private void btnBloquear_Click_389ME(object sender_389ME, EventArgs e_389ME)
        {
            CambiarBloqueo_389ME(true);
        }

        private void btnDesbloquear_Click_389ME(object sender_389ME, EventArgs e_389ME)
        {
            CambiarBloqueo_389ME(false);
        }

        private void CambiarBloqueo_389ME(bool bloquear_389ME)
        {
            if (dgvClientes_389ME.CurrentRow == null)
            {
                MessageBox.Show("Seleccione un cliente de la grilla.");
                return;
            }

            try
            {
                int dni_389ME = Convert.ToInt32(dgvClientes_389ME.CurrentRow.Cells["DNI_389ME"].Value);
                bllCliente_389ME.CambiarBloqueo_389ME(dni_389ME, bloquear_389ME);

                MessageBox.Show(bloquear_389ME ? "Cliente bloqueado correctamente." : "Cliente desbloqueado correctamente.");
                LimpiarCampos_389ME();
                ActualizarGrilla_389ME();
            }
            catch (Exception ex_389ME)
            {
                MessageBox.Show("Error al cambiar el bloqueo: " + ex_389ME.Message);
            }
        }

        private void dgvClientes_CellClick_389ME(object sender_389ME, DataGridViewCellEventArgs e_389ME)
        {
            if (dgvClientes_389ME.CurrentRow == null) return;

            txtDNI_389ME.Text = dgvClientes_389ME.CurrentRow.Cells["DNI_389ME"].Value.ToString();
            txtNombre_389ME.Text = dgvClientes_389ME.CurrentRow.Cells["Nombre_389ME"].Value.ToString();
            txtApellido_389ME.Text = dgvClientes_389ME.CurrentRow.Cells["Apellido_389ME"].Value.ToString();
            txtDireccion_389ME.Text = dgvClientes_389ME.CurrentRow.Cells["Direccion_389ME"].Value.ToString();
            txtMail_389ME.Text = dgvClientes_389ME.CurrentRow.Cells["Mail_389ME"].Value.ToString();
            txtTelefono_389ME.Text = dgvClientes_389ME.CurrentRow.Cells["NumTelefono_389ME"].Value.ToString();
        }

        private Cliente ArmarCliente_389ME(int dni_389ME, int telefono_389ME)
        {
            return new Cliente
            {
                DNI_389ME = dni_389ME,
                Nombre_389ME = txtNombre_389ME.Text.Trim(),
                Apellido_389ME = txtApellido_389ME.Text.Trim(),
                Direccion_389ME = txtDireccion_389ME.Text.Trim(),
                Mail_389ME = txtMail_389ME.Text.Trim(),
                NumTelefono_389ME = telefono_389ME
            };
        }

        private bool ValidarCampos_389ME(out int dni_389ME, out int telefono_389ME)
        {
            dni_389ME = 0;
            telefono_389ME = 0;

            string textoDni_389ME = txtDNI_389ME.Text.Trim();
            if (textoDni_389ME.Length != 8 || !textoDni_389ME.All(char.IsDigit) || !int.TryParse(textoDni_389ME, out dni_389ME))
            {
                MessageBox.Show("El DNI debe tener 8 numeros.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtNombre_389ME.Text) || string.IsNullOrWhiteSpace(txtApellido_389ME.Text))
            {
                MessageBox.Show("Faltan datos del cliente.");
                return false;
            }

            string mail_389ME = txtMail_389ME.Text.Trim();
            if (!mail_389ME.Contains("@") || !mail_389ME.Contains("."))
            {
                MessageBox.Show("El mail debe tener una arroba y un punto.");
                return false;
            }

            string textoTelefono_389ME = txtTelefono_389ME.Text.Trim();
            if (textoTelefono_389ME.Length > 0 && !int.TryParse(textoTelefono_389ME, out telefono_389ME))
            {
                MessageBox.Show("El telefono debe tener solo numeros.");
                return false;
            }

            return true;
        }

        private void LimpiarCampos_389ME()
        {
            txtDNI_389ME.Clear();
            txtNombre_389ME.Clear();
            txtApellido_389ME.Clear();
            txtDireccion_389ME.Clear();
            txtMail_389ME.Clear();
            txtTelefono_389ME.Clear();
        }

        private void frmGestionClientes_FormClosing_389ME(object sender_389ME, FormClosingEventArgs e_389ME)
        {
            if (e_389ME.CloseReason == CloseReason.UserClosing)
            {
                e_389ME.Cancel = true;
                Hide();
            }
        }

        private void frmGestionClientes_VisibleChanged_389ME(object sender_389ME, EventArgs e_389ME)
        {
            if (this.Visible)
            {
                LimpiarCampos_389ME();
                ActualizarGrilla_389ME();
                traductor.TraducirFormulario(this, GestorIdiomas.Instancia.IdiomaActual);
            }
        }
    }
}
