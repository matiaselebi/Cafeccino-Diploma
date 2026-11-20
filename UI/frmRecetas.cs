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
    public partial class frmRecetas : Form, IObserver
    {
        private readonly BLLProducto bllProducto_389ME = new BLLProducto();
        private readonly BLLInsumo bllInsumo_389ME = new BLLInsumo();
        private readonly BLLReceta bllReceta_389ME = new BLLReceta();
        // La receta se arma en memoria y se guarda completa con el boton Guardar receta
        private List<Receta> receta_389ME = new List<Receta>();
        private readonly Traductor traductor_389ME = new Traductor();

        public frmRecetas()
        {
            InitializeComponent();
            AyudaGrilla.Preparar_389ME(dgvReceta_389ME);
            txtCantidad_389ME.KeyPress += Validacion.SoloDecimales_389ME;
        }

        private void CargarCombos_389ME()
        {
            cmbInsumo_389ME.DataSource = bllInsumo_389ME.Listar_389ME();
            cmbProducto_389ME.DataSource = bllProducto_389ME.Listar_389ME();
        }

        private void cmbProducto_SelectedIndexChanged_389ME(object sender_389ME, EventArgs e_389ME)
        {
            receta_389ME = cmbProducto_389ME.SelectedItem is Producto producto_389ME
                ? bllReceta_389ME.ListarPorProducto_389ME(producto_389ME.CodProducto_389ME)
                : new List<Receta>();
            MostrarReceta_389ME();
        }

        private void MostrarReceta_389ME()
        {
            dgvReceta_389ME.DataSource = null;
            dgvReceta_389ME.DataSource = new List<Receta>(receta_389ME);
            if (dgvReceta_389ME.Columns.Contains("CodProducto_389ME"))
                dgvReceta_389ME.Columns["CodProducto_389ME"].Visible = false;
        }

        private void btnAgregarInsumo_Click_389ME(object sender_389ME, EventArgs e_389ME)
        {
            if (!(cmbInsumo_389ME.SelectedItem is Insumo insumo_389ME))
            {
                MessageBox.Show("Seleccione un insumo.");
                return;
            }

            if (!Validacion.LeerDecimal_389ME(txtCantidad_389ME.Text, out double cantidad_389ME) || cantidad_389ME <= 0)
            {
                MessageBox.Show("Ingrese una cantidad mayor a cero.");
                return;
            }

            // Si el insumo ya estaba en la receta, se reemplaza su cantidad
            Receta linea_389ME = receta_389ME.FirstOrDefault(item_389ME => item_389ME.CodInsumo_389ME == insumo_389ME.CodInsumo_389ME);
            if (linea_389ME == null)
            {
                linea_389ME = new Receta
                {
                    CodInsumo_389ME = insumo_389ME.CodInsumo_389ME,
                    NombreInsumo_389ME = insumo_389ME.Nombre_389ME,
                    UnidadMedida_389ME = insumo_389ME.UnidadMedida_389ME
                };
                receta_389ME.Add(linea_389ME);
            }
            linea_389ME.Cantidad_389ME = cantidad_389ME;

            txtCantidad_389ME.Clear();
            MostrarReceta_389ME();
        }

        private void btnQuitarInsumo_Click_389ME(object sender_389ME, EventArgs e_389ME)
        {
            if (!(AyudaGrilla.Seleccionado_389ME(dgvReceta_389ME) is Receta linea_389ME)) return;

            receta_389ME.RemoveAll(item_389ME => item_389ME.CodInsumo_389ME == linea_389ME.CodInsumo_389ME);
            MostrarReceta_389ME();
        }

        private void btnGuardarReceta_Click_389ME(object sender_389ME, EventArgs e_389ME)
        {
            if (!(cmbProducto_389ME.SelectedItem is Producto producto_389ME))
            {
                MessageBox.Show("Seleccione un producto.");
                return;
            }

            try
            {
                bllReceta_389ME.Guardar_389ME(producto_389ME, receta_389ME);
                MessageBox.Show("Receta guardada correctamente.");
            }
            catch (Exception ex_389ME)
            {
                MessageBox.Show("Error al guardar la receta: " + ex_389ME.Message);
            }
        }

        public void ActualizarIdioma(string idioma_389ME)
        {
            traductor_389ME.TraducirFormulario(this, int.Parse(idioma_389ME));
        }

        private void frmRecetas_FormClosing_389ME(object sender_389ME, FormClosingEventArgs e_389ME)
        {
            if (e_389ME.CloseReason == CloseReason.UserClosing)
            {
                e_389ME.Cancel = true;
                Hide();
            }
        }

        private void frmRecetas_VisibleChanged_389ME(object sender_389ME, EventArgs e_389ME)
        {
            if (this.Visible)
            {
                CargarCombos_389ME();
                traductor_389ME.TraducirFormulario(this, GestorIdiomas.Instancia.IdiomaActual);
            }
        }
    }
}
