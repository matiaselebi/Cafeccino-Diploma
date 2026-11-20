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
    public partial class frmRecepcionInsumos : Form, IObserver
    {
        private readonly BLLOrdenCompra bllOrden_389ME = new BLLOrdenCompra();
        private List<OrdenCompraDetalle> detalle_389ME = new List<OrdenCompraDetalle>();
        private readonly Traductor traductor_389ME = new Traductor();

        public frmRecepcionInsumos()
        {
            InitializeComponent();
            AyudaGrilla.Preparar_389ME(dgvDetalle_389ME);
            txtCantidadRecibida_389ME.KeyPress += Validacion.SoloDecimales_389ME;
        }

        private void CargarOrdenes_389ME()
        {
            cmbOrden_389ME.DataSource = null;
            cmbOrden_389ME.DataSource = bllOrden_389ME.ListarPendientes_389ME();
            if (cmbOrden_389ME.Items.Count == 0)
            {
                detalle_389ME = new List<OrdenCompraDetalle>();
                MostrarDetalle_389ME();
            }
        }

        // Por defecto se asume que llego todo lo pedido; el almacenista corrige las lineas que no
        private void cmbOrden_SelectedIndexChanged_389ME(object sender_389ME, EventArgs e_389ME)
        {
            detalle_389ME = cmbOrden_389ME.SelectedItem is OrdenCompra orden_389ME
                ? bllOrden_389ME.ObtenerDetalle_389ME(orden_389ME.CodOrden_389ME)
                : new List<OrdenCompraDetalle>();

            foreach (OrdenCompraDetalle linea_389ME in detalle_389ME)
                linea_389ME.CantidadRecibida_389ME = linea_389ME.Cantidad_389ME;
            MostrarDetalle_389ME();
        }

        private void MostrarDetalle_389ME()
        {
            dgvDetalle_389ME.DataSource = null;
            dgvDetalle_389ME.DataSource = new List<OrdenCompraDetalle>(detalle_389ME);
            foreach (string columna_389ME in new[] { "CodOrden_389ME", "CodInsumo_389ME" })
                if (dgvDetalle_389ME.Columns.Contains(columna_389ME))
                    dgvDetalle_389ME.Columns[columna_389ME].Visible = false;
        }

        private void dgvDetalle_SelectionChanged_389ME(object sender_389ME, EventArgs e_389ME)
        {
            if (AyudaGrilla.Seleccionado_389ME(dgvDetalle_389ME) is OrdenCompraDetalle linea_389ME)
                txtCantidadRecibida_389ME.Text = linea_389ME.CantidadRecibida_389ME.ToString();
        }

        private void btnActualizarCantidad_Click_389ME(object sender_389ME, EventArgs e_389ME)
        {
            if (!(AyudaGrilla.Seleccionado_389ME(dgvDetalle_389ME) is OrdenCompraDetalle linea_389ME))
            {
                MessageBox.Show("Seleccione un insumo de la orden.");
                return;
            }

            if (!Validacion.LeerDecimal_389ME(txtCantidadRecibida_389ME.Text, out double recibida_389ME) || recibida_389ME < 0 || recibida_389ME > linea_389ME.Cantidad_389ME)
            {
                MessageBox.Show("La cantidad recibida debe estar entre 0 y " + linea_389ME.Cantidad_389ME + ".");
                return;
            }

            detalle_389ME.First(item_389ME => item_389ME.CodInsumo_389ME == linea_389ME.CodInsumo_389ME).CantidadRecibida_389ME = recibida_389ME;
            MostrarDetalle_389ME();
        }

        private void btnConfirmarRecepcion_Click_389ME(object sender_389ME, EventArgs e_389ME)
        {
            if (!(cmbOrden_389ME.SelectedItem is OrdenCompra orden_389ME))
            {
                MessageBox.Show("Seleccione una orden de compra.");
                return;
            }

            try
            {
                bllOrden_389ME.RegistrarRecepcion_389ME(orden_389ME, detalle_389ME);
                MessageBox.Show("Recepcion registrada. El stock de los insumos se actualizo.");
                txtCantidadRecibida_389ME.Clear();
                CargarOrdenes_389ME();
            }
            catch (Exception ex_389ME)
            {
                MessageBox.Show("Error al registrar la recepcion: " + ex_389ME.Message);
            }
        }

        public void ActualizarIdioma(string idioma_389ME)
        {
            traductor_389ME.TraducirFormulario(this, int.Parse(idioma_389ME));
        }

        private void frmRecepcionInsumos_FormClosing_389ME(object sender_389ME, FormClosingEventArgs e_389ME)
        {
            if (e_389ME.CloseReason == CloseReason.UserClosing)
            {
                e_389ME.Cancel = true;
                Hide();
            }
        }

        private void frmRecepcionInsumos_VisibleChanged_389ME(object sender_389ME, EventArgs e_389ME)
        {
            if (this.Visible)
            {
                txtCantidadRecibida_389ME.Clear();
                CargarOrdenes_389ME();
                traductor_389ME.TraducirFormulario(this, GestorIdiomas.Instancia.IdiomaActual);
            }
        }
    }
}
