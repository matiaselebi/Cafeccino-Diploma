using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BE;
using BLL;
using SERVICIOS;

namespace CAFECCINO_INGENIERIA_DE_SOFTWARE
{
    public partial class frmVenta : Form, IObserver
    {
        private readonly BLLProducto bllProducto_389ME = new BLLProducto();
        private readonly BLLCliente bllCliente_389ME = new BLLCliente();
        private readonly BLLFactura bllFactura_389ME = new BLLFactura();
        private readonly List<DetalleFactura> carrito_389ME = new List<DetalleFactura>();
        Traductor traductor = new Traductor();

        public frmVenta()
        {
            InitializeComponent();
        }

        public void ActualizarIdioma(string idioma)
        {
            traductor.TraducirFormulario(this, int.Parse(idioma));
        }

        private void frmVenta_Load_389ME(object sender_389ME, EventArgs e_389ME)
        {
            cmbMetodoPago_389ME.Items.Clear();
            cmbMetodoPago_389ME.Items.Add("Efectivo");
            cmbMetodoPago_389ME.Items.Add("Tarjeta");
            cmbMetodoPago_389ME.SelectedIndex = 0;

            cmbTipoTarjeta_389ME.Items.Clear();
            cmbTipoTarjeta_389ME.Items.Add("Debito");
            cmbTipoTarjeta_389ME.Items.Add("Credito");

            // No se puede elegir un mes anterior al actual, asi que una tarjeta vencida no se puede cargar.
            dtpFechaExpiracion_389ME.MinDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

            ActualizarProductos_389ME();
        }

        private void ActualizarProductos_389ME()
        {
            dgvProductos_389ME.DataSource = bllProducto_389ME.Listar_389ME();
            if (dgvProductos_389ME.Columns.Contains("DVH_389ME"))
                dgvProductos_389ME.Columns["DVH_389ME"].Visible = false;
        }

        private void btnAgregarCarrito_Click_389ME(object sender_389ME, EventArgs e_389ME)
        {
            if (dgvProductos_389ME.CurrentRow == null)
            {
                MessageBox.Show("Seleccione un producto.");
                return;
            }

            if (!int.TryParse(txtCantidad_389ME.Text, out int cantidad_389ME) || cantidad_389ME <= 0)
            {
                MessageBox.Show("Ingrese una cantidad valida.");
                return;
            }

            int codProducto_389ME = Convert.ToInt32(dgvProductos_389ME.CurrentRow.Cells["CodProducto_389ME"].Value);
            Producto producto_389ME = bllProducto_389ME.ObtenerPorCodigo_389ME(codProducto_389ME);

            DetalleFactura item_389ME = carrito_389ME.FirstOrDefault(detalle_389ME => detalle_389ME.CodProducto_389ME == codProducto_389ME);
            int cantidadEnCarrito_389ME = (item_389ME?.Cantidad_389ME ?? 0) + cantidad_389ME;

            if (producto_389ME.Stock_389ME < cantidadEnCarrito_389ME)
            {
                MessageBox.Show($"No hay stock suficiente de '{producto_389ME.Nombre_389ME}' (disponible: {producto_389ME.Stock_389ME}).");
                return;
            }

            if (item_389ME != null)
            {
                item_389ME.Cantidad_389ME += cantidad_389ME;
            }
            else
            {
                carrito_389ME.Add(new DetalleFactura
                {
                    CodProducto_389ME = producto_389ME.CodProducto_389ME,
                    NombreProducto_389ME = producto_389ME.Nombre_389ME,
                    Cantidad_389ME = cantidad_389ME,
                    PrecioUnitario_389ME = producto_389ME.Precio_389ME
                });
            }

            ActualizarCarrito_389ME();
        }

        private void btnQuitarItem_Click_389ME(object sender_389ME, EventArgs e_389ME)
        {
            if (dgvCarrito_389ME.CurrentRow == null) return;

            int codProducto_389ME = Convert.ToInt32(dgvCarrito_389ME.CurrentRow.Cells["CodProducto_389ME"].Value);
            DetalleFactura item_389ME = carrito_389ME.FirstOrDefault(detalle_389ME => detalle_389ME.CodProducto_389ME == codProducto_389ME);
            if (item_389ME != null)
                carrito_389ME.Remove(item_389ME);

            ActualizarCarrito_389ME();
        }

        private void ActualizarCarrito_389ME()
        {
            dgvCarrito_389ME.DataSource = null;
            dgvCarrito_389ME.DataSource = new List<DetalleFactura>(carrito_389ME);

            if (dgvCarrito_389ME.Columns.Contains("CodProducto_389ME"))
                dgvCarrito_389ME.Columns["CodProducto_389ME"].Visible = false;

            double subtotal_389ME = carrito_389ME.Sum(detalle_389ME => detalle_389ME.Subtotal_389ME);
            double total_389ME = bllFactura_389ME.CalcularTotal_389ME(carrito_389ME, cmbMetodoPago_389ME.Text);

            lblRecargo_389ME.Visible = cmbMetodoPago_389ME.Text == "Tarjeta";
            lblRecargo_389ME.Text = "Recargo " + (BLLFactura.RecargoTarjeta_389ME * 100).ToString("N0") +
                                    "%: $" + (total_389ME - subtotal_389ME).ToString("N2");

            lblTotal_389ME.Text = "Total: $" + total_389ME.ToString("N2");
        }

        private bool DniValido_389ME(out int dni_389ME)
        {
            string texto_389ME = txtDNI_389ME.Text.Trim();
            dni_389ME = 0;
            return texto_389ME.Length == 8 && texto_389ME.All(char.IsDigit) && int.TryParse(texto_389ME, out dni_389ME);
        }

        private void btnBuscarCliente_Click_389ME(object sender_389ME, EventArgs e_389ME)
        {
            if (!DniValido_389ME(out int dni_389ME))
            {
                MessageBox.Show("El DNI debe tener 8 numeros.");
                return;
            }

            Cliente cliente_389ME = bllCliente_389ME.ObtenerPorDNI_389ME(dni_389ME);

            if (cliente_389ME != null)
            {
                txtNombreCliente_389ME.Text = cliente_389ME.Nombre_389ME;
                txtApellidoCliente_389ME.Text = cliente_389ME.Apellido_389ME;
                txtDireccionCliente_389ME.Text = cliente_389ME.Direccion_389ME;
                txtMailCliente_389ME.Text = cliente_389ME.Mail_389ME;
                txtTelCliente_389ME.Text = cliente_389ME.NumTelefono_389ME.ToString();
                SetCamposClienteHabilitados_389ME(false);
                MessageBox.Show("Cliente registrado encontrado.");
            }
            else
            {
                txtNombreCliente_389ME.Clear();
                txtApellidoCliente_389ME.Clear();
                txtDireccionCliente_389ME.Clear();
                txtMailCliente_389ME.Clear();
                txtTelCliente_389ME.Clear();
                SetCamposClienteHabilitados_389ME(true);
                MessageBox.Show("Cliente no registrado. Complete sus datos para registrarlo.");
            }
        }

        private void LimpiarDatosTarjeta_389ME()
        {
            txtBanco_389ME.Clear();
            cmbTipoTarjeta_389ME.SelectedIndex = -1;
            cmbTipoTarjeta_389ME.Text = "";
            txtMarcaTarjeta_389ME.Clear();
            dtpFechaExpiracion_389ME.Value = DateTime.Now;
            txtCVV_389ME.Clear();
        }

        private void LimpiarVenta_389ME()
        {
            carrito_389ME.Clear();

            txtCantidad_389ME.Clear();
            txtDNI_389ME.Clear();
            txtNombreCliente_389ME.Clear();
            txtApellidoCliente_389ME.Clear();
            txtDireccionCliente_389ME.Clear();
            txtMailCliente_389ME.Clear();
            txtTelCliente_389ME.Clear();
            SetCamposClienteHabilitados_389ME(true);

            cmbMetodoPago_389ME.SelectedIndex = 0;
            LimpiarDatosTarjeta_389ME();

            txtResultado_389ME.Clear();
            ActualizarCarrito_389ME();
        }

        private void SetCamposClienteHabilitados_389ME(bool habilitado_389ME)
        {
            txtNombreCliente_389ME.Enabled = habilitado_389ME;
            txtApellidoCliente_389ME.Enabled = habilitado_389ME;
            txtDireccionCliente_389ME.Enabled = habilitado_389ME;
            txtMailCliente_389ME.Enabled = habilitado_389ME;
            txtTelCliente_389ME.Enabled = habilitado_389ME;
        }

        private void cmbMetodoPago_SelectedIndexChanged_389ME(object sender_389ME, EventArgs e_389ME)
        {
            bool esTarjeta_389ME = cmbMetodoPago_389ME.Text == "Tarjeta";
            txtBanco_389ME.Enabled = esTarjeta_389ME;
            cmbTipoTarjeta_389ME.Enabled = esTarjeta_389ME;
            txtMarcaTarjeta_389ME.Enabled = esTarjeta_389ME;
            dtpFechaExpiracion_389ME.Enabled = esTarjeta_389ME;
            txtCVV_389ME.Enabled = esTarjeta_389ME;

            if (!esTarjeta_389ME)
                LimpiarDatosTarjeta_389ME();

            ActualizarCarrito_389ME();
        }

        private void btnConfirmarVenta_Click_389ME(object sender_389ME, EventArgs e_389ME)
        {
            if (carrito_389ME.Count == 0)
            {
                MessageBox.Show("El carrito esta vacio.");
                return;
            }

            if (!DniValido_389ME(out int dni_389ME))
            {
                MessageBox.Show("El DNI debe tener 8 numeros.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtNombreCliente_389ME.Text) || string.IsNullOrWhiteSpace(txtApellidoCliente_389ME.Text))
            {
                MessageBox.Show("Faltan datos del cliente.");
                return;
            }

            string mail_389ME = txtMailCliente_389ME.Text.Trim();
            if (!mail_389ME.Contains("@") || !mail_389ME.Contains("."))
            {
                MessageBox.Show("El mail debe tener una arroba y un punto.");
                return;
            }

            if (cmbMetodoPago_389ME.Text == "Tarjeta" &&
                (string.IsNullOrWhiteSpace(txtBanco_389ME.Text) ||
                 string.IsNullOrWhiteSpace(cmbTipoTarjeta_389ME.Text) ||
                 string.IsNullOrWhiteSpace(txtMarcaTarjeta_389ME.Text)))
            {
                MessageBox.Show("Complete el banco, el tipo y la marca de la tarjeta.");
                return;
            }

            // El vencimiento y el codigo de seguridad no se guardan: los procesa el PosNet.
            if (cmbMetodoPago_389ME.Text == "Tarjeta" && (txtCVV_389ME.Text.Length != 3 || !txtCVV_389ME.Text.All(char.IsDigit)))
            {
                MessageBox.Show("El codigo de seguridad debe tener 3 numeros.");
                return;
            }

            int.TryParse(txtTelCliente_389ME.Text, out int telefono_389ME);

            Cliente cliente_389ME = new Cliente
            {
                DNI_389ME = dni_389ME,
                Nombre_389ME = txtNombreCliente_389ME.Text.Trim(),
                Apellido_389ME = txtApellidoCliente_389ME.Text.Trim(),
                Direccion_389ME = txtDireccionCliente_389ME.Text.Trim(),
                Mail_389ME = txtMailCliente_389ME.Text.Trim(),
                NumTelefono_389ME = telefono_389ME
            };

            string metodoPago_389ME = cmbMetodoPago_389ME.Text;
            string banco_389ME = metodoPago_389ME == "Tarjeta" ? txtBanco_389ME.Text.Trim() : null;
            string tipoTarjeta_389ME = metodoPago_389ME == "Tarjeta" ? cmbTipoTarjeta_389ME.Text : null;
            string marcaTarjeta_389ME = metodoPago_389ME == "Tarjeta" ? txtMarcaTarjeta_389ME.Text.Trim() : null;

            try
            {
                Factura factura_389ME = bllFactura_389ME.RealizarVenta_389ME(cliente_389ME, new List<DetalleFactura>(carrito_389ME), metodoPago_389ME, banco_389ME, tipoTarjeta_389ME, marcaTarjeta_389ME);

                LimpiarVenta_389ME();
                ActualizarProductos_389ME();
                MostrarFactura_389ME(factura_389ME);
            }
            catch (Exception ex_389ME)
            {
                MessageBox.Show("Error al procesar la venta: " + ex_389ME.Message);
            }
        }

        private void MostrarFactura_389ME(Factura factura_389ME)
        {
            StringBuilder texto_389ME = new StringBuilder();
            texto_389ME.AppendLine("--- Factura N° " + factura_389ME.CodFactura_389ME + " ---");
            texto_389ME.AppendLine("DNI cliente: " + factura_389ME.DNICliente_389ME);
            texto_389ME.AppendLine("Fecha: " + factura_389ME.Fecha_389ME.ToString("dd/MM/yyyy HH:mm"));
            texto_389ME.AppendLine("Metodo de pago: " + factura_389ME.MetodoPago_389ME);
            if (factura_389ME.MetodoPago_389ME == "Tarjeta")
            {
                texto_389ME.AppendLine("Banco: " + factura_389ME.Banco_389ME);
                texto_389ME.AppendLine("Tipo de tarjeta: " + factura_389ME.TipoTarjeta_389ME);
                texto_389ME.AppendLine("Marca de tarjeta: " + factura_389ME.MarcaTarjeta_389ME);
                texto_389ME.AppendLine("Recargo por pago con tarjeta: " + (BLLFactura.RecargoTarjeta_389ME * 100).ToString("N0") + "%");
            }
            texto_389ME.AppendLine("Productos vendidos:");
            foreach (DetalleFactura detalle_389ME in factura_389ME.Detalles_389ME)
                texto_389ME.AppendLine($"  {detalle_389ME.NombreProducto_389ME} x{detalle_389ME.Cantidad_389ME} = ${detalle_389ME.Subtotal_389ME:N2}");
            texto_389ME.AppendLine("Monto total: $" + factura_389ME.MontoTotal_389ME.ToString("N2"));

            txtResultado_389ME.Text = texto_389ME.ToString();
            MessageBox.Show("Venta registrada correctamente. Factura N° " + factura_389ME.CodFactura_389ME);
        }

        private void frmVenta_FormClosing_389ME(object sender_389ME, FormClosingEventArgs e_389ME)
        {
            if (e_389ME.CloseReason == CloseReason.UserClosing)
            {
                e_389ME.Cancel = true;
                Hide();
            }
        }

        private void frmVenta_VisibleChanged_389ME(object sender_389ME, EventArgs e_389ME)
        {
            if (this.Visible)
            {
                ActualizarProductos_389ME();
                traductor.TraducirFormulario(this, GestorIdiomas.Instancia.IdiomaActual);
            }
            else
            {
                LimpiarVenta_389ME();
            }
        }
    }
}
