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
    public partial class frmOrdenesCompra : Form, IObserver
    {
        private readonly BLLSolicitudCotizacion bllSolicitud_389ME = new BLLSolicitudCotizacion();
        private readonly BLLCotizacion bllCotizacion_389ME = new BLLCotizacion();
        private readonly BLLOrdenCompra bllOrden_389ME = new BLLOrdenCompra();
        private readonly Traductor traductor_389ME = new Traductor();

        public frmOrdenesCompra()
        {
            InitializeComponent();
            AyudaGrilla.Preparar_389ME(dgvComparacion_389ME, dgvCotizacionesProveedor_389ME, dgvOrdenes_389ME, dgvDetalleOrden_389ME);
        }

        private void CargarSolicitudes_389ME()
        {
            cmbSolicitud_389ME.DataSource = null;
            cmbSolicitud_389ME.DataSource = bllSolicitud_389ME.ListarPendientes_389ME();
            if (cmbSolicitud_389ME.Items.Count == 0)
            {
                dgvComparacion_389ME.DataSource = null;
                dgvCotizacionesProveedor_389ME.DataSource = null;
            }
        }

        private void ActualizarOrdenes_389ME()
        {
            dgvDetalleOrden_389ME.DataSource = null;
            dgvOrdenes_389ME.DataSource = null;
            dgvOrdenes_389ME.DataSource = bllOrden_389ME.Listar_389ME();
        }

        private void cmbSolicitud_SelectedIndexChanged_389ME(object sender_389ME, EventArgs e_389ME)
        {
            dgvComparacion_389ME.DataSource = cmbSolicitud_389ME.SelectedItem is SolicitudCotizacion solicitud_389ME
                ? bllCotizacion_389ME.CompararProveedores_389ME(solicitud_389ME.CodSolicitud_389ME)
                : null;
        }

        // Al elegir un proveedor se muestran sus cotizaciones
        private void dgvComparacion_SelectionChanged_389ME(object sender_389ME, EventArgs e_389ME)
        {
            if (!(AyudaGrilla.Seleccionado_389ME(dgvComparacion_389ME) is ResumenCotizacion resumen_389ME) ||
                !(cmbSolicitud_389ME.SelectedItem is SolicitudCotizacion solicitud_389ME))
            {
                dgvCotizacionesProveedor_389ME.DataSource = null;
                return;
            }

            dgvCotizacionesProveedor_389ME.DataSource = bllCotizacion_389ME.ListarPorSolicitud_389ME(solicitud_389ME.CodSolicitud_389ME)
                .Where(cotizacion_389ME => cotizacion_389ME.CUIT_389ME == resumen_389ME.CUIT_389ME)
                .ToList();
            foreach (string columna_389ME in new[] { "CodCotizacion_389ME", "CodSolicitud_389ME", "CUIT_389ME", "NombreProveedor_389ME", "CodInsumo_389ME" })
                if (dgvCotizacionesProveedor_389ME.Columns.Contains(columna_389ME))
                    dgvCotizacionesProveedor_389ME.Columns[columna_389ME].Visible = false;

        }

        private void btnGenerarOrden_Click_389ME(object sender_389ME, EventArgs e_389ME)
        {
            if (!(AyudaGrilla.Seleccionado_389ME(dgvComparacion_389ME) is ResumenCotizacion resumen_389ME))
            {
                MessageBox.Show("Seleccione el proveedor elegido.");
                return;
            }

            List<Cotizacion> seleccionadas_389ME = dgvCotizacionesProveedor_389ME.SelectedRows.Cast<DataGridViewRow>()
                .Select(fila_389ME => (Cotizacion)fila_389ME.DataBoundItem)
                .ToList();
            if (seleccionadas_389ME.Count == 0)
            {
                MessageBox.Show("Seleccione los insumos que va a comprar.");
                return;
            }

            try
            {

                OrdenCompra orden_389ME = bllOrden_389ME.Generar_389ME(seleccionadas_389ME);
                string mensaje_389ME = "Orden de compra N° " + orden_389ME.CodOrden_389ME + " generada para " + orden_389ME.NombreProveedor_389ME +
                                       " por $" + orden_389ME.PrecioTotal_389ME.ToString("N2") + ".";

                // La orden ya quedo registrada: si el PDF no se puede generar solo se avisa
                try
                {
                    mensaje_389ME += "\nLa orden se guardó en: " + GuardarPdf_389ME(orden_389ME);
                }
                catch (Exception exPdf_389ME)
                {
                    mensaje_389ME += "\nNo se pudo generar el PDF: " + exPdf_389ME.Message;
                }
                MessageBox.Show(mensaje_389ME);

                CargarSolicitudes_389ME();
                ActualizarOrdenes_389ME();
            }
            catch (Exception ex_389ME)
            {
                MessageBox.Show("Error al generar la orden: " + ex_389ME.Message);
            }
        }

        private void dgvOrdenes_SelectionChanged_389ME(object sender_389ME, EventArgs e_389ME)
        {
            if (!(AyudaGrilla.Seleccionado_389ME(dgvOrdenes_389ME) is OrdenCompra orden_389ME))
            {
                dgvDetalleOrden_389ME.DataSource = null;
                return;
            }

            dgvDetalleOrden_389ME.DataSource = bllOrden_389ME.ObtenerDetalle_389ME(orden_389ME.CodOrden_389ME);
            foreach (string columna_389ME in new[] { "CodOrden_389ME", "CodInsumo_389ME" })
                if (dgvDetalleOrden_389ME.Columns.Contains(columna_389ME))
                    dgvDetalleOrden_389ME.Columns[columna_389ME].Visible = false;
        }

        // Genera el PDF de la orden seleccionada en la carpeta Facturas Compra y lo abre para imprimirlo
        private void btnImprimirOrden_Click_389ME(object sender_389ME, EventArgs e_389ME)
        {
            if (!(AyudaGrilla.Seleccionado_389ME(dgvOrdenes_389ME) is OrdenCompra orden_389ME))
            {
                MessageBox.Show("Seleccione una orden de compra.");
                return;
            }

            try
            {
                System.Diagnostics.Process.Start(GuardarPdf_389ME(orden_389ME));
            }
            catch (Exception ex_389ME)
            {
                MessageBox.Show("No se pudo generar el PDF: " + ex_389ME.Message);
            }
        }

        private string GuardarPdf_389ME(OrdenCompra orden_389ME)
        {
            List<OrdenCompraDetalle> detalles_389ME = bllOrden_389ME.ObtenerDetalle_389ME(orden_389ME.CodOrden_389ME);
            return Comprobante.GuardarPdf_389ME(Comprobante.CarpetaCompras_389ME,
                "Orden de compra " + orden_389ME.CodOrden_389ME + " - " + orden_389ME.FechaCreacion_389ME.ToString("yyyy-MM-dd"),
                Comprobante.TextoOrdenCompra_389ME(orden_389ME, detalles_389ME));
        }

        public void ActualizarIdioma(string idioma_389ME)
        {
            traductor_389ME.TraducirFormulario(this, int.Parse(idioma_389ME));
        }

        private void frmOrdenesCompra_FormClosing_389ME(object sender_389ME, FormClosingEventArgs e_389ME)
        {
            if (e_389ME.CloseReason == CloseReason.UserClosing)
            {
                e_389ME.Cancel = true;
                Hide();
            }
        }

        private void frmOrdenesCompra_VisibleChanged_389ME(object sender_389ME, EventArgs e_389ME)
        {
            if (this.Visible)
            {
                CargarSolicitudes_389ME();
                ActualizarOrdenes_389ME();
                traductor_389ME.TraducirFormulario(this, GestorIdiomas.Instancia.IdiomaActual);
            }
        }
    }
}
