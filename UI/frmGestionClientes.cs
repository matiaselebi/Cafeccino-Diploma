using System;
using System.Collections.Generic;
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
            AyudaGrilla.Preparar_389ME(dgvClientes_389ME);

            txtDNI_389ME.KeyPress += Validacion.SoloNumeros_389ME;
            txtTelefono_389ME.KeyPress += Validacion.SoloNumeros_389ME;
            txtNombre_389ME.KeyPress += Validacion.SoloLetras_389ME;
            txtApellido_389ME.KeyPress += Validacion.SoloLetras_389ME;
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
            MostrarClientes_389ME(bllCliente_389ME.Listar_389ME());
        }

        private void MostrarClientes_389ME(List<Cliente> clientes_389ME)
        {
            dgvClientes_389ME.DataSource = null;
            dgvClientes_389ME.DataSource = clientes_389ME;

            if (dgvClientes_389ME.Columns.Contains("DVH_389ME"))
                dgvClientes_389ME.Columns["DVH_389ME"].Visible = false;
        }

        private void btnAgregar_Click_389ME(object sender_389ME, EventArgs e_389ME)
        {
            if (!ValidarCampos_389ME(out int dni_389ME, out long telefono_389ME))
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
                MessageBox.Show("No se pudo agregar el cliente: " + ex_389ME.Message);
            }
        }

        private void btnModificar_Click_389ME(object sender_389ME, EventArgs e_389ME)
        {
            if (!ValidarCampos_389ME(out int dni_389ME, out long telefono_389ME))
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

        // Serializacion: los clientes que estan en la grilla se guardan en un archivo XML que elige el operador
        private void btnSerializar_Click_389ME(object sender_389ME, EventArgs e_389ME)
        {
            List<Cliente> clientes_389ME = dgvClientes_389ME.DataSource as List<Cliente>;
            if (clientes_389ME == null || clientes_389ME.Count == 0)
            {
                MessageBox.Show("No hay clientes en la grilla para serializar.");
                return;
            }

            using (SaveFileDialog dialogo_389ME = new SaveFileDialog())
            {
                dialogo_389ME.Filter = "Archivo XML (*.xml)|*.xml";
                dialogo_389ME.FileName = "Clientes.xml";
                if (dialogo_389ME.ShowDialog() != DialogResult.OK) return;

                try
                {
                    bllCliente_389ME.Serializar_389ME(clientes_389ME, dialogo_389ME.FileName);
                    txtArchivoXml_389ME.Text = dialogo_389ME.FileName;
                    MessageBox.Show("Se serializaron " + clientes_389ME.Count + " clientes.");
                }
                catch (Exception ex_389ME)
                {
                    MessageBox.Show("No se pudo serializar: " + ex_389ME.Message);
                }
            }
        }

        // Des-serializacion: el archivo XML elegido se muestra en la grilla (no se guarda en la base)
        private void btnDeserializar_Click_389ME(object sender_389ME, EventArgs e_389ME)
        {
            using (OpenFileDialog dialogo_389ME = new OpenFileDialog())
            {
                dialogo_389ME.Filter = "Archivo XML (*.xml)|*.xml";
                if (dialogo_389ME.ShowDialog() != DialogResult.OK) return;

                try
                {
                    List<Cliente> clientes_389ME = bllCliente_389ME.Deserializar_389ME(dialogo_389ME.FileName);
                    LimpiarCampos_389ME();
                    MostrarClientes_389ME(clientes_389ME);
                    txtArchivoXml_389ME.Text = dialogo_389ME.FileName;
                    MessageBox.Show("Se des-serializaron " + clientes_389ME.Count + " clientes.");
                }
                catch (Exception ex_389ME)
                {
                    MessageBox.Show("No se pudo des-serializar: " + ex_389ME.Message);
                }
            }
        }

        // Limpiar solo vacia la grilla; Actualizar la vuelve a cargar desde la base
        private void btnLimpiar_Click_389ME(object sender_389ME, EventArgs e_389ME)
        {
            dgvClientes_389ME.DataSource = null;
            LimpiarCampos_389ME();
            txtArchivoXml_389ME.Clear();
        }

        private void btnActualizar_Click_389ME(object sender_389ME, EventArgs e_389ME)
        {
            LimpiarCampos_389ME();
            ActualizarGrilla_389ME();
        }

        private void dgvClientes_CellClick_389ME(object sender_389ME, DataGridViewCellEventArgs e_389ME)
        {
            if (dgvClientes_389ME.CurrentRow == null) return;

            // Convert.ToString devuelve "" si el dato esta vacio (por ejemplo un cliente sin direccion)
            txtDNI_389ME.Text = Convert.ToString(dgvClientes_389ME.CurrentRow.Cells["DNI_389ME"].Value);
            txtNombre_389ME.Text = Convert.ToString(dgvClientes_389ME.CurrentRow.Cells["Nombre_389ME"].Value);
            txtApellido_389ME.Text = Convert.ToString(dgvClientes_389ME.CurrentRow.Cells["Apellido_389ME"].Value);
            txtDireccion_389ME.Text = Convert.ToString(dgvClientes_389ME.CurrentRow.Cells["Direccion_389ME"].Value);
            txtMail_389ME.Text = Convert.ToString(dgvClientes_389ME.CurrentRow.Cells["Mail_389ME"].Value);
            long telefono_389ME = Convert.ToInt64(dgvClientes_389ME.CurrentRow.Cells["NumTelefono_389ME"].Value);
            txtTelefono_389ME.Text = telefono_389ME == 0 ? "" : telefono_389ME.ToString();
        }

        private Cliente ArmarCliente_389ME(int dni_389ME, long telefono_389ME)
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

        // Aca solo se convierten los numeros; el resto de los datos los valida BLLCliente
        private bool ValidarCampos_389ME(out int dni_389ME, out long telefono_389ME)
        {
            dni_389ME = 0;
            telefono_389ME = 0;

            string textoDni_389ME = txtDNI_389ME.Text.Trim();
            if (textoDni_389ME.Length != 8 || !textoDni_389ME.All(char.IsDigit) || !int.TryParse(textoDni_389ME, out dni_389ME))
            {
                MessageBox.Show("El DNI debe tener 8 números.");
                return false;
            }

            string textoTelefono_389ME = txtTelefono_389ME.Text.Trim();
            if (textoTelefono_389ME.Length > 0 && !long.TryParse(textoTelefono_389ME, out telefono_389ME))
            {
                MessageBox.Show("El teléfono debe tener solo números.");
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
