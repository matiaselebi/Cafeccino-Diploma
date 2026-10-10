using System;
using System.Windows.Forms;
using BE;
using BLL;
using SERVICIOS;

namespace CAFECCINO_INGENIERIA_DE_SOFTWARE
{
    public partial class GestionProductos : Form, IObserver
    {
        private readonly BLLProducto bllProducto_389ME = new BLLProducto();
        Traductor traductor = new Traductor();

        public GestionProductos()
        {
            InitializeComponent();
        }

        public void ActualizarIdioma(string idioma)
        {
            traductor.TraducirFormulario(this, int.Parse(idioma));
        }

        private void GestionProductos_Load_389ME(object sender_389ME, EventArgs e_389ME)
        {
            ActualizarGrilla_389ME();
        }

        private void ActualizarGrilla_389ME()
        {
            dataGridView1_389ME.DataSource = bllProducto_389ME.Listar_389ME();
            if (dataGridView1_389ME.Columns.Contains("DVH_389ME"))
                dataGridView1_389ME.Columns["DVH_389ME"].Visible = false;
        }

        private void btnAgregar_Click_389ME(object sender_389ME, EventArgs e_389ME)
        {
            if (!ValidarCampos_389ME(out double precio_389ME, out int stock_389ME))
                return;

            try
            {
                bllProducto_389ME.Registrar_389ME(new Producto
                {
                    Nombre_389ME = txtNombre_389ME.Text.Trim(),
                    Titulo_389ME = txtTitulo_389ME.Text.Trim(),
                    Precio_389ME = precio_389ME,
                    Stock_389ME = stock_389ME
                });

                MessageBox.Show("Producto agregado correctamente.");
                LimpiarCampos_389ME();
                ActualizarGrilla_389ME();
            }
            catch (Exception ex_389ME)
            {
                MessageBox.Show("Error al agregar el producto: " + ex_389ME.Message);
            }
        }

        private void btnModificar_Click_389ME(object sender_389ME, EventArgs e_389ME)
        {
            if (dataGridView1_389ME.CurrentRow == null)
            {
                MessageBox.Show("Seleccione un producto de la grilla.");
                return;
            }

            if (!ValidarCampos_389ME(out double precio_389ME, out int stock_389ME))
                return;

            try
            {
                int codProducto_389ME = Convert.ToInt32(dataGridView1_389ME.CurrentRow.Cells["CodProducto_389ME"].Value);

                bllProducto_389ME.Modificar_389ME(new Producto
                {
                    CodProducto_389ME = codProducto_389ME,
                    Nombre_389ME = txtNombre_389ME.Text.Trim(),
                    Titulo_389ME = txtTitulo_389ME.Text.Trim(),
                    Precio_389ME = precio_389ME,
                    Stock_389ME = stock_389ME
                });

                MessageBox.Show("Producto modificado correctamente.");
                LimpiarCampos_389ME();
                ActualizarGrilla_389ME();
            }
            catch (Exception ex_389ME)
            {
                MessageBox.Show("Error al modificar el producto: " + ex_389ME.Message);
            }
        }

        private void btnEliminar_Click_389ME(object sender_389ME, EventArgs e_389ME)
        {
            if (dataGridView1_389ME.CurrentRow == null)
            {
                MessageBox.Show("Seleccione un producto de la grilla.");
                return;
            }

            if (MessageBox.Show("¿Desea eliminar el producto?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                int codProducto_389ME = Convert.ToInt32(dataGridView1_389ME.CurrentRow.Cells["CodProducto_389ME"].Value);
                bllProducto_389ME.Eliminar_389ME(codProducto_389ME);

                MessageBox.Show("Producto eliminado correctamente.");
                LimpiarCampos_389ME();
                ActualizarGrilla_389ME();
            }
            catch (Exception ex_389ME)
            {
                MessageBox.Show("Error al eliminar el producto: " + ex_389ME.Message);
            }
        }

        private void dataGridView1_CellClick_389ME(object sender_389ME, DataGridViewCellEventArgs e_389ME)
        {
            if (dataGridView1_389ME.CurrentRow == null) return;

            txtNombre_389ME.Text = dataGridView1_389ME.CurrentRow.Cells["Nombre_389ME"].Value.ToString();
            txtTitulo_389ME.Text = dataGridView1_389ME.CurrentRow.Cells["Titulo_389ME"].Value.ToString();
            txtPrecio_389ME.Text = dataGridView1_389ME.CurrentRow.Cells["Precio_389ME"].Value.ToString();
            txtStock_389ME.Text = dataGridView1_389ME.CurrentRow.Cells["Stock_389ME"].Value.ToString();
        }

        private bool ValidarCampos_389ME(out double precio_389ME, out int stock_389ME)
        {
            precio_389ME = 0;
            stock_389ME = 0;

            if (string.IsNullOrWhiteSpace(txtNombre_389ME.Text) || string.IsNullOrWhiteSpace(txtTitulo_389ME.Text))
            {
                MessageBox.Show("Complete nombre y titulo.");
                return false;
            }

            if (!double.TryParse(txtPrecio_389ME.Text, out precio_389ME))
            {
                MessageBox.Show("El precio debe ser un numero valido.");
                return false;
            }

            if (!int.TryParse(txtStock_389ME.Text, out stock_389ME))
            {
                MessageBox.Show("El stock debe ser un numero entero valido.");
                return false;
            }

            return true;
        }

        private void LimpiarCampos_389ME()
        {
            txtNombre_389ME.Clear();
            txtTitulo_389ME.Clear();
            txtPrecio_389ME.Clear();
            txtStock_389ME.Clear();
        }

        private void GestionProductos_FormClosing_389ME(object sender_389ME, FormClosingEventArgs e_389ME)
        {
            if (e_389ME.CloseReason == CloseReason.UserClosing)
            {
                e_389ME.Cancel = true;
                Hide();
            }
        }

        private void GestionProductos_VisibleChanged_389ME(object sender_389ME, EventArgs e_389ME)
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
