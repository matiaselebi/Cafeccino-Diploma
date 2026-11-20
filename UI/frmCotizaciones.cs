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
    public partial class frmCotizaciones : Form, IObserver
    {
        private readonly BLLSolicitudCotizacion bllSolicitud_389ME = new BLLSolicitudCotizacion();
        private readonly BLLCotizacion bllCotizacion_389ME = new BLLCotizacion();
        private readonly Traductor traductor_389ME = new Traductor();

        public frmCotizaciones()
        {
            InitializeComponent();
            AyudaGrilla.Preparar_389ME(dgvInsumos_389ME, dgvCotizaciones_389ME);
            txtPrecio_389ME.KeyPress += Validacion.SoloDecimales_389ME;
            txtCantidad_389ME.KeyPress += Validacion.SoloDecimales_389ME;
            txtPrecioTotal_389ME.KeyPress += Validacion.SoloDecimales_389ME;
            txtPrecio_389ME.TextChanged += txtPrecio_TextChanged_389ME;
            txtCantidad_389ME.TextChanged += txtPrecio_TextChanged_389ME;
            txtPrecioTotal_389ME.TextChanged += txtPrecioTotal_TextChanged_389ME;
        }

        // Precio total = precio unitario x cantidad. Si el proveedor da el total, el unitario se calcula solo.
        // La bandera evita que un cuadro vuelva a recalcular al otro mientras se escribe el resultado.
        private bool calculando_389ME = false;

        private void txtPrecio_TextChanged_389ME(object sender_389ME, EventArgs e_389ME)
        {
            if (calculando_389ME) return;
            if (Validacion.LeerDecimal_389ME(txtPrecio_389ME.Text, out double precio_389ME) &&
                Validacion.LeerDecimal_389ME(txtCantidad_389ME.Text, out double cantidad_389ME))
                MostrarCalculado_389ME(txtPrecioTotal_389ME, precio_389ME * cantidad_389ME);
        }

        private void txtPrecioTotal_TextChanged_389ME(object sender_389ME, EventArgs e_389ME)
        {
            if (calculando_389ME) return;
            if (Validacion.LeerDecimal_389ME(txtPrecioTotal_389ME.Text, out double total_389ME) &&
                Validacion.LeerDecimal_389ME(txtCantidad_389ME.Text, out double cantidad_389ME) && cantidad_389ME > 0)
                MostrarCalculado_389ME(txtPrecio_389ME, total_389ME / cantidad_389ME);
        }

        private void MostrarCalculado_389ME(TextBox caja_389ME, double valor_389ME)
        {
            calculando_389ME = true;
            caja_389ME.Text = Math.Round(valor_389ME, 2).ToString("0.##");
            calculando_389ME = false;
        }

        private void CargarSolicitudes_389ME()
        {
            cmbSolicitud_389ME.DataSource = null;
            cmbSolicitud_389ME.DataSource = bllSolicitud_389ME.ListarPendientes_389ME();
            if (cmbSolicitud_389ME.Items.Count == 0)
                LimpiarSolicitud_389ME();
            // Una cotizacion no puede vencer antes de hoy
            dtpVencimiento_389ME.MinDate = DateTime.Today;
            dtpVencimiento_389ME.Value = DateTime.Today.AddDays(7);
        }

        private void LimpiarSolicitud_389ME()
        {
            dgvInsumos_389ME.DataSource = null;
            cmbProveedor_389ME.DataSource = null;
            dgvCotizaciones_389ME.DataSource = null;
        }

        private void cmbSolicitud_SelectedIndexChanged_389ME(object sender_389ME, EventArgs e_389ME)
        {
            if (!(cmbSolicitud_389ME.SelectedItem is SolicitudCotizacion solicitud_389ME))
            {
                LimpiarSolicitud_389ME();
                return;
            }

            dgvInsumos_389ME.DataSource = bllSolicitud_389ME.ObtenerInsumos_389ME(solicitud_389ME.CodSolicitud_389ME);
            if (dgvInsumos_389ME.Columns.Contains("CodSolicitud_389ME"))
                dgvInsumos_389ME.Columns["CodSolicitud_389ME"].Visible = false;
            // Referencias para comparar lo que cotiza el proveedor: lo que conviene pedir y el ultimo precio pagado
            if (dgvInsumos_389ME.Columns.Contains("Cantidad_389ME"))
                dgvInsumos_389ME.Columns["Cantidad_389ME"].HeaderText = "Cantidad sugerida";
            if (dgvInsumos_389ME.Columns.Contains("PrecioActual_389ME"))
                dgvInsumos_389ME.Columns["PrecioActual_389ME"].HeaderText = "Precio actual";

            cmbProveedor_389ME.DataSource = bllSolicitud_389ME.ObtenerProveedores_389ME(solicitud_389ME.CodSolicitud_389ME);
            ActualizarCotizaciones_389ME(solicitud_389ME.CodSolicitud_389ME);
        }

        private void ActualizarCotizaciones_389ME(int codSolicitud_389ME)
        {
            dgvCotizaciones_389ME.DataSource = null;
            dgvCotizaciones_389ME.DataSource = bllCotizacion_389ME.ListarPorSolicitud_389ME(codSolicitud_389ME);
            foreach (string columna_389ME in new[] { "CodCotizacion_389ME", "CodSolicitud_389ME", "CodInsumo_389ME" })
                if (dgvCotizaciones_389ME.Columns.Contains(columna_389ME))
                    dgvCotizaciones_389ME.Columns[columna_389ME].Visible = false;
        }

        private void btnGuardarCotizacion_Click_389ME(object sender_389ME, EventArgs e_389ME)
        {
            if (!(cmbSolicitud_389ME.SelectedItem is SolicitudCotizacion solicitud_389ME) ||
                !(cmbProveedor_389ME.SelectedItem is Proveedor proveedor_389ME) ||
                !(AyudaGrilla.Seleccionado_389ME(dgvInsumos_389ME) is SolicitudInsumo item_389ME))
            {
                MessageBox.Show("Seleccione la solicitud, el proveedor y el insumo cotizado.");
                return;
            }

            if (!Validacion.LeerDecimal_389ME(txtPrecio_389ME.Text, out double precio_389ME) ||
                !Validacion.LeerDecimal_389ME(txtCantidad_389ME.Text, out double cantidad_389ME))
            {
                MessageBox.Show("El precio y la cantidad deben ser números. Use coma o punto para los decimales.");
                return;
            }

            try
            {
                bllCotizacion_389ME.Registrar_389ME(new Cotizacion
                {
                    CodSolicitud_389ME = solicitud_389ME.CodSolicitud_389ME,
                    CUIT_389ME = proveedor_389ME.CUIT_389ME,
                    CodInsumo_389ME = item_389ME.CodInsumo_389ME,
                    NombreInsumo_389ME = item_389ME.NombreInsumo_389ME,
                    Precio_389ME = precio_389ME,
                    Cantidad_389ME = cantidad_389ME,
                    FechaVencimiento_389ME = dtpVencimiento_389ME.Value.Date
                });

                txtPrecio_389ME.Clear();
                txtCantidad_389ME.Clear();
                txtPrecioTotal_389ME.Clear();
                ActualizarCotizaciones_389ME(solicitud_389ME.CodSolicitud_389ME);
            }
            catch (Exception ex_389ME)
            {
                MessageBox.Show("Error al guardar la cotizacion: " + ex_389ME.Message);
            }
        }

        public void ActualizarIdioma(string idioma_389ME)
        {
            traductor_389ME.TraducirFormulario(this, int.Parse(idioma_389ME));
        }

        private void frmCotizaciones_FormClosing_389ME(object sender_389ME, FormClosingEventArgs e_389ME)
        {
            if (e_389ME.CloseReason == CloseReason.UserClosing)
            {
                e_389ME.Cancel = true;
                Hide();
            }
        }

        private void frmCotizaciones_VisibleChanged_389ME(object sender_389ME, EventArgs e_389ME)
        {
            if (this.Visible)
            {
                txtPrecio_389ME.Clear();
                txtCantidad_389ME.Clear();
                txtPrecioTotal_389ME.Clear();
                CargarSolicitudes_389ME();
                traductor_389ME.TraducirFormulario(this, GestorIdiomas.Instancia.IdiomaActual);
            }
        }
    }
}
