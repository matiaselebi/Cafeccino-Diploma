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
    public partial class frmInsumos : Form, IObserver
    {
        private readonly BLLInsumo bllInsumo_389ME = new BLLInsumo();
        private readonly Traductor traductor_389ME = new Traductor();

        public frmInsumos()
        {
            InitializeComponent();
            AyudaGrilla.Preparar_389ME(dgvInsumos_389ME);
            cmbUnidad_389ME.Items.AddRange(new object[] { "kg", "g", "l", "ml", "unidades" });

            txtTipo_389ME.KeyPress += Validacion.SoloLetras_389ME;
            txtPrecio_389ME.KeyPress += Validacion.SoloDecimales_389ME;
            txtStock_389ME.KeyPress += Validacion.SoloDecimales_389ME;
            txtStockMinimo_389ME.KeyPress += Validacion.SoloDecimales_389ME;
            txtStockMaximo_389ME.KeyPress += Validacion.SoloDecimales_389ME;
        }

        private void ActualizarGrilla_389ME()
        {
            dgvInsumos_389ME.DataSource = null;
            dgvInsumos_389ME.DataSource = bllInsumo_389ME.Listar_389ME();
        }

        private void dgvInsumos_SelectionChanged_389ME(object sender_389ME, EventArgs e_389ME)
        {
            if (!(AyudaGrilla.Seleccionado_389ME(dgvInsumos_389ME) is Insumo insumo_389ME)) return;

            txtNombre_389ME.Text = insumo_389ME.Nombre_389ME;
            txtTipo_389ME.Text = insumo_389ME.Tipo_389ME;
            cmbUnidad_389ME.SelectedItem = insumo_389ME.UnidadMedida_389ME;
            txtPrecio_389ME.Text = insumo_389ME.Precio_389ME.ToString();
            txtStock_389ME.Text = insumo_389ME.Stock_389ME.ToString();
            txtStockMinimo_389ME.Text = insumo_389ME.StockMinimo_389ME.ToString();
            txtStockMaximo_389ME.Text = insumo_389ME.StockMaximo_389ME.ToString();
        }

        // Devuelve null si algun numero no es valido
        private Insumo ArmarInsumo_389ME()
        {
            if (!Validacion.LeerDecimal_389ME(txtPrecio_389ME.Text, out double precio_389ME) ||
                !Validacion.LeerDecimal_389ME(txtStock_389ME.Text, out double stock_389ME) ||
                !Validacion.LeerDecimal_389ME(txtStockMinimo_389ME.Text, out double minimo_389ME) ||
                !Validacion.LeerDecimal_389ME(txtStockMaximo_389ME.Text, out double maximo_389ME))
            {
                MessageBox.Show("El precio y los stocks deben ser números. Use coma o punto para los decimales.");
                return null;
            }

            return new Insumo
            {
                Nombre_389ME = txtNombre_389ME.Text.Trim(),
                Tipo_389ME = txtTipo_389ME.Text.Trim(),
                UnidadMedida_389ME = cmbUnidad_389ME.Text,
                Precio_389ME = precio_389ME,
                Stock_389ME = stock_389ME,
                StockMinimo_389ME = minimo_389ME,
                StockMaximo_389ME = maximo_389ME
            };
        }

        private void btnAgregar_Click_389ME(object sender_389ME, EventArgs e_389ME)
        {
            Insumo insumo_389ME = ArmarInsumo_389ME();
            if (insumo_389ME == null) return;

            try
            {
                bllInsumo_389ME.Registrar_389ME(insumo_389ME);
                MessageBox.Show("Insumo agregado correctamente.");
                LimpiarCampos_389ME();
                ActualizarGrilla_389ME();
            }
            catch (Exception ex_389ME)
            {
                MessageBox.Show("Error al agregar el insumo: " + ex_389ME.Message);
            }
        }

        private void btnModificar_Click_389ME(object sender_389ME, EventArgs e_389ME)
        {
            if (!(AyudaGrilla.Seleccionado_389ME(dgvInsumos_389ME) is Insumo seleccionado_389ME))
            {
                MessageBox.Show("Seleccione un insumo de la grilla.");
                return;
            }

            Insumo insumo_389ME = ArmarInsumo_389ME();
            if (insumo_389ME == null) return;
            insumo_389ME.CodInsumo_389ME = seleccionado_389ME.CodInsumo_389ME;

            try
            {
                bllInsumo_389ME.Modificar_389ME(insumo_389ME);
                MessageBox.Show("Insumo modificado correctamente.");
                LimpiarCampos_389ME();
                ActualizarGrilla_389ME();
            }
            catch (Exception ex_389ME)
            {
                MessageBox.Show("Error al modificar el insumo: " + ex_389ME.Message);
            }
        }

        private void btnEliminar_Click_389ME(object sender_389ME, EventArgs e_389ME)
        {
            if (!(AyudaGrilla.Seleccionado_389ME(dgvInsumos_389ME) is Insumo insumo_389ME))
            {
                MessageBox.Show("Seleccione un insumo de la grilla.");
                return;
            }

            if (MessageBox.Show("¿Desea eliminar el insumo " + insumo_389ME.Nombre_389ME + "?", "Confirmar",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                bllInsumo_389ME.Eliminar_389ME(insumo_389ME.CodInsumo_389ME);
                MessageBox.Show("Insumo eliminado correctamente.");
                LimpiarCampos_389ME();
                ActualizarGrilla_389ME();
            }
            catch (Exception ex_389ME)
            {
                MessageBox.Show(ex_389ME.Message);
            }
        }

        private void LimpiarCampos_389ME()
        {
            txtNombre_389ME.Clear();
            txtTipo_389ME.Clear();
            cmbUnidad_389ME.SelectedIndex = -1;
            txtPrecio_389ME.Clear();
            txtStock_389ME.Clear();
            txtStockMinimo_389ME.Clear();
            txtStockMaximo_389ME.Clear();
        }

        public void ActualizarIdioma(string idioma_389ME)
        {
            traductor_389ME.TraducirFormulario(this, int.Parse(idioma_389ME));
        }

        private void frmInsumos_FormClosing_389ME(object sender_389ME, FormClosingEventArgs e_389ME)
        {
            if (e_389ME.CloseReason == CloseReason.UserClosing)
            {
                e_389ME.Cancel = true;
                Hide();
            }
        }

        private void frmInsumos_VisibleChanged_389ME(object sender_389ME, EventArgs e_389ME)
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
