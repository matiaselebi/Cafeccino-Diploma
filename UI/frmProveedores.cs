using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using BE;
using BLL;
using SERVICIOS;

namespace CAFECCINO_INGENIERIA_DE_SOFTWARE
{
    public partial class frmProveedores : Form, IObserver
    {
        private readonly BLLProveedor bllProveedor_389ME = new BLLProveedor();
        private readonly Traductor traductor_389ME = new Traductor();

        public frmProveedores()
        {
            InitializeComponent();
            AyudaGrilla.Preparar_389ME(dgvProveedores_389ME);
            txtCUIT_389ME.KeyPress += Validacion.SoloNumeros_389ME;
            txtTelefono_389ME.KeyPress += Validacion.SoloNumeros_389ME;
            txtCuentaBancaria_389ME.KeyPress += Validacion.SoloNumeros_389ME;
        }

        private void ActualizarGrilla_389ME()
        {
            dgvProveedores_389ME.DataSource = null;
            dgvProveedores_389ME.DataSource = bllProveedor_389ME.Listar_389ME();

            // La cuenta bancaria es un dato sensible: no se muestra en la grilla
            if (dgvProveedores_389ME.Columns.Contains("CuentaBancaria_389ME"))
                dgvProveedores_389ME.Columns["CuentaBancaria_389ME"].Visible = false;
        }

        private void dgvProveedores_SelectionChanged_389ME(object sender_389ME, EventArgs e_389ME)
        {
            if (!(AyudaGrilla.Seleccionado_389ME(dgvProveedores_389ME) is Proveedor proveedor_389ME)) return;

            txtCUIT_389ME.Text = proveedor_389ME.CUIT_389ME;
            txtNombre_389ME.Text = proveedor_389ME.Nombre_389ME;
            txtRazonSocial_389ME.Text = proveedor_389ME.RazonSocial_389ME;
            txtTelefono_389ME.Text = proveedor_389ME.Telefono_389ME;
            txtEmail_389ME.Text = proveedor_389ME.Email_389ME;
            txtDireccion_389ME.Text = proveedor_389ME.Direccion_389ME;
            txtCuentaBancaria_389ME.Text = proveedor_389ME.CuentaBancaria_389ME;
        }

        private Proveedor ArmarProveedor_389ME()
        {
            return new Proveedor
            {
                CUIT_389ME = txtCUIT_389ME.Text.Trim(),
                Nombre_389ME = txtNombre_389ME.Text.Trim(),
                RazonSocial_389ME = txtRazonSocial_389ME.Text.Trim(),
                Telefono_389ME = txtTelefono_389ME.Text.Trim(),
                Email_389ME = txtEmail_389ME.Text.Trim(),
                Direccion_389ME = txtDireccion_389ME.Text.Trim(),
                CuentaBancaria_389ME = txtCuentaBancaria_389ME.Text.Trim()
            };
        }

        private void btnPreRegistrar_Click_389ME(object sender_389ME, EventArgs e_389ME)
        {
            try
            {
                bllProveedor_389ME.PreRegistrar_389ME(ArmarProveedor_389ME());
                MessageBox.Show(string.IsNullOrEmpty(txtCuentaBancaria_389ME.Text)
                    ? "Proveedor pre-registrado. Para comprarle, complete su dirección y su cuenta bancaria."
                    : "Proveedor registrado correctamente.");
                LimpiarCampos_389ME();
                ActualizarGrilla_389ME();
            }
            catch (Exception ex_389ME)
            {
                MessageBox.Show("Error al pre-registrar el proveedor: " + ex_389ME.Message);
            }
        }

        private void btnModificar_Click_389ME(object sender_389ME, EventArgs e_389ME)
        {
            try
            {
                bllProveedor_389ME.Modificar_389ME(ArmarProveedor_389ME());
                MessageBox.Show("Proveedor modificado correctamente.");
                LimpiarCampos_389ME();
                ActualizarGrilla_389ME();
            }
            catch (Exception ex_389ME)
            {
                MessageBox.Show("Error al modificar el proveedor: " + ex_389ME.Message);
            }
        }

        private void LimpiarCampos_389ME()
        {
            txtCUIT_389ME.Clear();
            txtNombre_389ME.Clear();
            txtRazonSocial_389ME.Clear();
            txtTelefono_389ME.Clear();
            txtEmail_389ME.Clear();
            txtDireccion_389ME.Clear();
            txtCuentaBancaria_389ME.Clear();
        }

        public void ActualizarIdioma(string idioma_389ME)
        {
            traductor_389ME.TraducirFormulario(this, int.Parse(idioma_389ME));
        }

        private void frmProveedores_FormClosing_389ME(object sender_389ME, FormClosingEventArgs e_389ME)
        {
            if (e_389ME.CloseReason == CloseReason.UserClosing)
            {
                e_389ME.Cancel = true;
                Hide();
            }
        }

        private void frmProveedores_VisibleChanged_389ME(object sender_389ME, EventArgs e_389ME)
        {
            if (this.Visible)
            {
                LimpiarCampos_389ME();
                ActualizarGrilla_389ME();
                traductor_389ME.TraducirFormulario(this, GestorIdiomas.Instancia.IdiomaActual);
            }
        }
    }
}
