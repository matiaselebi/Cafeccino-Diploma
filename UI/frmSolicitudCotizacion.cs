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
    public partial class frmSolicitudCotizacion : Form, IObserver
    {
        private readonly BLLInsumo bllInsumo_389ME = new BLLInsumo();
        private readonly BLLProveedor bllProveedor_389ME = new BLLProveedor();
        private readonly BLLSolicitudCotizacion bllSolicitud_389ME = new BLLSolicitudCotizacion();
        private readonly List<SolicitudInsumo> solicitud_389ME = new List<SolicitudInsumo>();
        private readonly Traductor traductor_389ME = new Traductor();

        public frmSolicitudCotizacion()
        {
            InitializeComponent();
            AyudaGrilla.Preparar_389ME(dgvSugerencias_389ME, dgvSolicitud_389ME);
        }

        private void ActualizarSugerencias_389ME()
        {
            dgvSugerencias_389ME.DataSource = null;
            dgvSugerencias_389ME.DataSource = bllInsumo_389ME.ObtenerSugerencias_389ME();

            // Los insumos que hay que reponer se resaltan
            foreach (DataGridViewRow fila_389ME in dgvSugerencias_389ME.Rows)
                if (fila_389ME.DataBoundItem is SugerenciaCompra sugerencia_389ME && sugerencia_389ME.Comprar_389ME)
                    fila_389ME.DefaultCellStyle.BackColor = Color.LightSalmon;
        }

        private void CargarProveedores_389ME()
        {
            clbProveedores_389ME.Items.Clear();
            foreach (Proveedor proveedor_389ME in bllProveedor_389ME.Listar_389ME())
                clbProveedores_389ME.Items.Add(proveedor_389ME);
        }

        private void AgregarInsumo_389ME(SugerenciaCompra sugerencia_389ME, double cantidad_389ME)
        {
            SolicitudInsumo item_389ME = solicitud_389ME.FirstOrDefault(linea_389ME => linea_389ME.CodInsumo_389ME == sugerencia_389ME.CodInsumo_389ME);
            if (item_389ME == null)
            {
                item_389ME = new SolicitudInsumo
                {
                    CodInsumo_389ME = sugerencia_389ME.CodInsumo_389ME,
                    NombreInsumo_389ME = sugerencia_389ME.Nombre_389ME,
                    UnidadMedida_389ME = sugerencia_389ME.UnidadMedida_389ME
                };
                solicitud_389ME.Add(item_389ME);
            }
            item_389ME.Cantidad_389ME = cantidad_389ME;
        }

        private void MostrarSolicitud_389ME()
        {
            dgvSolicitud_389ME.DataSource = null;
            dgvSolicitud_389ME.DataSource = new List<SolicitudInsumo>(solicitud_389ME);
            if (dgvSolicitud_389ME.Columns.Contains("CodSolicitud_389ME"))
                dgvSolicitud_389ME.Columns["CodSolicitud_389ME"].Visible = false;
            if (dgvSolicitud_389ME.Columns.Contains("PrecioActual_389ME"))
                dgvSolicitud_389ME.Columns["PrecioActual_389ME"].Visible = false;
            // La cantidad que se compra se carga en cada cotizacion; aca queda la sugerida como referencia
            if (dgvSolicitud_389ME.Columns.Contains("Cantidad_389ME"))
                dgvSolicitud_389ME.Columns["Cantidad_389ME"].HeaderText = "Cantidad sugerida";
        }

        private void btnAgregarSugeridos_Click_389ME(object sender_389ME, EventArgs e_389ME)
        {
            List<SugerenciaCompra> sugeridos_389ME = ((List<SugerenciaCompra>)dgvSugerencias_389ME.DataSource)
                .Where(sugerencia_389ME => sugerencia_389ME.Comprar_389ME && sugerencia_389ME.CantidadSugerida_389ME > 0)
                .ToList();

            if (sugeridos_389ME.Count == 0)
            {
                MessageBox.Show("No hay insumos que reponer.");
                return;
            }

            foreach (SugerenciaCompra sugerencia_389ME in sugeridos_389ME)
                AgregarInsumo_389ME(sugerencia_389ME, sugerencia_389ME.CantidadSugerida_389ME);
            MostrarSolicitud_389ME();
        }

        private void btnAgregarInsumo_Click_389ME(object sender_389ME, EventArgs e_389ME)
        {
            if (!(AyudaGrilla.Seleccionado_389ME(dgvSugerencias_389ME) is SugerenciaCompra sugerencia_389ME))
            {
                MessageBox.Show("Seleccione un insumo.");
                return;
            }

            // Se pide como referencia lo que falta para llegar al stock maximo
            double cantidad_389ME = sugerencia_389ME.StockMaximo_389ME - sugerencia_389ME.Stock_389ME;
            if (cantidad_389ME <= 0)
            {
                MessageBox.Show("El insumo ya está en su stock máximo.");
                return;
            }

            AgregarInsumo_389ME(sugerencia_389ME, Math.Round(cantidad_389ME, 3));
            MostrarSolicitud_389ME();
        }

        private void btnQuitarInsumo_Click_389ME(object sender_389ME, EventArgs e_389ME)
        {
            if (!(AyudaGrilla.Seleccionado_389ME(dgvSolicitud_389ME) is SolicitudInsumo item_389ME)) return;

            solicitud_389ME.RemoveAll(linea_389ME => linea_389ME.CodInsumo_389ME == item_389ME.CodInsumo_389ME);
            MostrarSolicitud_389ME();
        }

        private void btnConfirmarSolicitud_Click_389ME(object sender_389ME, EventArgs e_389ME)
        {
            try
            {
                List<Proveedor> proveedores_389ME = clbProveedores_389ME.CheckedItems.Cast<Proveedor>().ToList();
                int codSolicitud_389ME = bllSolicitud_389ME.Registrar_389ME(new List<SolicitudInsumo>(solicitud_389ME), proveedores_389ME);

                MessageBox.Show("Solicitud N° " + codSolicitud_389ME + " registrada. Envíela a los proveedores seleccionados.");
                LimpiarSolicitud_389ME();
                ActualizarSugerencias_389ME();
            }
            catch (Exception ex_389ME)
            {
                MessageBox.Show("Error al registrar la solicitud: " + ex_389ME.Message);
            }
        }

        private void LimpiarSolicitud_389ME()
        {
            solicitud_389ME.Clear();
            MostrarSolicitud_389ME();
            for (int posicion_389ME = 0; posicion_389ME < clbProveedores_389ME.Items.Count; posicion_389ME++)
                clbProveedores_389ME.SetItemChecked(posicion_389ME, false);
        }

        public void ActualizarIdioma(string idioma_389ME)
        {
            traductor_389ME.TraducirFormulario(this, int.Parse(idioma_389ME));
        }

        private void frmSolicitudCotizacion_FormClosing_389ME(object sender_389ME, FormClosingEventArgs e_389ME)
        {
            if (e_389ME.CloseReason == CloseReason.UserClosing)
            {
                e_389ME.Cancel = true;
                Hide();
            }
        }

        private void frmSolicitudCotizacion_VisibleChanged_389ME(object sender_389ME, EventArgs e_389ME)
        {
            if (this.Visible)
            {
                ActualizarSugerencias_389ME();
                CargarProveedores_389ME();
                traductor_389ME.TraducirFormulario(this, GestorIdiomas.Instancia.IdiomaActual);
            }
            else
            {
                LimpiarSolicitud_389ME();
            }
        }
    }
}
