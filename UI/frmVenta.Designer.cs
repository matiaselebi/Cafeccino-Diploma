namespace CAFECCINO_INGENIERIA_DE_SOFTWARE
{
    partial class frmVenta
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmVenta));
            this.lblProductos_389ME = new System.Windows.Forms.Label();
            this.dgvProductos_389ME = new System.Windows.Forms.DataGridView();
            this.lblCantidad_389ME = new System.Windows.Forms.Label();
            this.txtCantidad_389ME = new System.Windows.Forms.TextBox();
            this.btnAgregarCarrito_389ME = new System.Windows.Forms.Button();
            this.lblCarrito_389ME = new System.Windows.Forms.Label();
            this.dgvCarrito_389ME = new System.Windows.Forms.DataGridView();
            this.lblTotal_389ME = new System.Windows.Forms.Label();
            this.lblRecargo_389ME = new System.Windows.Forms.Label();
            this.btnQuitarItem_389ME = new System.Windows.Forms.Button();
            this.lblDNI_389ME = new System.Windows.Forms.Label();
            this.txtDNI_389ME = new System.Windows.Forms.TextBox();
            this.btnBuscarCliente_389ME = new System.Windows.Forms.Button();
            this.lblNombreCliente_389ME = new System.Windows.Forms.Label();
            this.txtNombreCliente_389ME = new System.Windows.Forms.TextBox();
            this.lblApellidoCliente_389ME = new System.Windows.Forms.Label();
            this.txtApellidoCliente_389ME = new System.Windows.Forms.TextBox();
            this.lblDireccionCliente_389ME = new System.Windows.Forms.Label();
            this.txtDireccionCliente_389ME = new System.Windows.Forms.TextBox();
            this.lblMailCliente_389ME = new System.Windows.Forms.Label();
            this.txtMailCliente_389ME = new System.Windows.Forms.TextBox();
            this.lblTelCliente_389ME = new System.Windows.Forms.Label();
            this.txtTelCliente_389ME = new System.Windows.Forms.TextBox();
            this.lblMetodoPago_389ME = new System.Windows.Forms.Label();
            this.cmbMetodoPago_389ME = new System.Windows.Forms.ComboBox();
            this.lblBanco_389ME = new System.Windows.Forms.Label();
            this.txtBanco_389ME = new System.Windows.Forms.TextBox();
            this.lblTipoTarjeta_389ME = new System.Windows.Forms.Label();
            this.cmbTipoTarjeta_389ME = new System.Windows.Forms.ComboBox();
            this.lblMarcaTarjeta_389ME = new System.Windows.Forms.Label();
            this.txtMarcaTarjeta_389ME = new System.Windows.Forms.TextBox();
            this.lblFechaExpiracion_389ME = new System.Windows.Forms.Label();
            this.dtpFechaExpiracion_389ME = new System.Windows.Forms.DateTimePicker();
            this.lblCVV_389ME = new System.Windows.Forms.Label();
            this.txtCVV_389ME = new System.Windows.Forms.TextBox();
            this.btnConfirmarVenta_389ME = new System.Windows.Forms.Button();
            this.txtResultado_389ME = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductos_389ME)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCarrito_389ME)).BeginInit();
            this.SuspendLayout();
            this.txtDNI_389ME.MaxLength = 8;
            this.txtNombreCliente_389ME.MaxLength = 50;
            this.txtApellidoCliente_389ME.MaxLength = 50;
            this.txtDireccionCliente_389ME.MaxLength = 100;
            this.txtMailCliente_389ME.MaxLength = 100;
            this.txtTelCliente_389ME.MaxLength = 10;
            this.txtBanco_389ME.MaxLength = 50;
            this.txtMarcaTarjeta_389ME.MaxLength = 30;
            this.txtCantidad_389ME.MaxLength = 4;
            this.txtCVV_389ME.MaxLength = 3;
            // 
            // lblProductos_389ME
            // 
            this.lblProductos_389ME.AutoSize = true;
            this.lblProductos_389ME.Location = new System.Drawing.Point(10, 10);
            this.lblProductos_389ME.Name = "lblProductos_389ME";
            this.lblProductos_389ME.Size = new System.Drawing.Size(110, 13);
            this.lblProductos_389ME.TabIndex = 0;
            this.lblProductos_389ME.Text = "Productos disponibles";
            // 
            // dgvProductos_389ME
            // 
            this.dgvProductos_389ME.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvProductos_389ME.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProductos_389ME.Location = new System.Drawing.Point(10, 30);
            this.dgvProductos_389ME.Name = "dgvProductos_389ME";
            this.dgvProductos_389ME.ReadOnly = true;
            this.dgvProductos_389ME.Size = new System.Drawing.Size(300, 200);
            this.dgvProductos_389ME.TabIndex = 1;
            // 
            // lblCantidad_389ME
            // 
            this.lblCantidad_389ME.AutoSize = true;
            this.lblCantidad_389ME.Location = new System.Drawing.Point(10, 240);
            this.lblCantidad_389ME.Name = "lblCantidad_389ME";
            this.lblCantidad_389ME.Size = new System.Drawing.Size(52, 13);
            this.lblCantidad_389ME.TabIndex = 2;
            this.lblCantidad_389ME.Text = "Cantidad:";
            // 
            // txtCantidad_389ME
            // 
            this.txtCantidad_389ME.Location = new System.Drawing.Point(75, 237);
            this.txtCantidad_389ME.Name = "txtCantidad_389ME";
            this.txtCantidad_389ME.Size = new System.Drawing.Size(60, 20);
            this.txtCantidad_389ME.TabIndex = 3;
            this.txtCantidad_389ME.Text = "1";
            // 
            // btnAgregarCarrito_389ME
            // 
            this.btnAgregarCarrito_389ME.Location = new System.Drawing.Point(150, 235);
            this.btnAgregarCarrito_389ME.Name = "btnAgregarCarrito_389ME";
            this.btnAgregarCarrito_389ME.Size = new System.Drawing.Size(160, 23);
            this.btnAgregarCarrito_389ME.TabIndex = 4;
            this.btnAgregarCarrito_389ME.Text = "Agregar al carrito";
            this.btnAgregarCarrito_389ME.UseVisualStyleBackColor = true;
            this.btnAgregarCarrito_389ME.Click += new System.EventHandler(this.btnAgregarCarrito_Click_389ME);
            // 
            // lblCarrito_389ME
            // 
            this.lblCarrito_389ME.AutoSize = true;
            this.lblCarrito_389ME.Location = new System.Drawing.Point(330, 10);
            this.lblCarrito_389ME.Name = "lblCarrito_389ME";
            this.lblCarrito_389ME.Size = new System.Drawing.Size(37, 13);
            this.lblCarrito_389ME.TabIndex = 5;
            this.lblCarrito_389ME.Text = "Carrito";
            // 
            // dgvCarrito_389ME
            // 
            this.dgvCarrito_389ME.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvCarrito_389ME.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCarrito_389ME.Location = new System.Drawing.Point(330, 30);
            this.dgvCarrito_389ME.Name = "dgvCarrito_389ME";
            this.dgvCarrito_389ME.ReadOnly = true;
            this.dgvCarrito_389ME.Size = new System.Drawing.Size(320, 200);
            this.dgvCarrito_389ME.TabIndex = 6;
            // 
            // lblTotal_389ME
            // 
            this.lblTotal_389ME.AutoSize = true;
            this.lblTotal_389ME.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblTotal_389ME.Location = new System.Drawing.Point(330, 237);
            this.lblTotal_389ME.Name = "lblTotal_389ME";
            this.lblTotal_389ME.Size = new System.Drawing.Size(67, 16);
            this.lblTotal_389ME.TabIndex = 7;
            this.lblTotal_389ME.Text = "Total: $0";
            // 
            // lblRecargo_389ME
            // 
            this.lblRecargo_389ME.AutoSize = true;
            this.lblRecargo_389ME.Location = new System.Drawing.Point(330, 265);
            this.lblRecargo_389ME.Name = "lblRecargo_389ME";
            this.lblRecargo_389ME.Size = new System.Drawing.Size(0, 13);
            this.lblRecargo_389ME.TabIndex = 8;
            this.lblRecargo_389ME.Visible = false;
            // 
            // btnQuitarItem_389ME
            // 
            this.btnQuitarItem_389ME.Location = new System.Drawing.Point(500, 235);
            this.btnQuitarItem_389ME.Name = "btnQuitarItem_389ME";
            this.btnQuitarItem_389ME.Size = new System.Drawing.Size(150, 23);
            this.btnQuitarItem_389ME.TabIndex = 9;
            this.btnQuitarItem_389ME.Text = "Quitar del carrito";
            this.btnQuitarItem_389ME.UseVisualStyleBackColor = true;
            this.btnQuitarItem_389ME.Click += new System.EventHandler(this.btnQuitarItem_Click_389ME);
            // 
            // lblDNI_389ME
            // 
            this.lblDNI_389ME.AutoSize = true;
            this.lblDNI_389ME.Location = new System.Drawing.Point(670, 10);
            this.lblDNI_389ME.Name = "lblDNI_389ME";
            this.lblDNI_389ME.Size = new System.Drawing.Size(26, 13);
            this.lblDNI_389ME.TabIndex = 10;
            this.lblDNI_389ME.Text = "DNI";
            // 
            // txtDNI_389ME
            // 
            this.txtDNI_389ME.Location = new System.Drawing.Point(730, 7);
            this.txtDNI_389ME.Name = "txtDNI_389ME";
            this.txtDNI_389ME.Size = new System.Drawing.Size(100, 20);
            this.txtDNI_389ME.TabIndex = 11;
            // 
            // btnBuscarCliente_389ME
            // 
            this.btnBuscarCliente_389ME.Location = new System.Drawing.Point(836, 5);
            this.btnBuscarCliente_389ME.Name = "btnBuscarCliente_389ME";
            this.btnBuscarCliente_389ME.Size = new System.Drawing.Size(90, 23);
            this.btnBuscarCliente_389ME.TabIndex = 12;
            this.btnBuscarCliente_389ME.Text = "Buscar";
            this.btnBuscarCliente_389ME.UseVisualStyleBackColor = true;
            this.btnBuscarCliente_389ME.Click += new System.EventHandler(this.btnBuscarCliente_Click_389ME);
            // 
            // lblNombreCliente_389ME
            // 
            this.lblNombreCliente_389ME.AutoSize = true;
            this.lblNombreCliente_389ME.Location = new System.Drawing.Point(670, 40);
            this.lblNombreCliente_389ME.Name = "lblNombreCliente_389ME";
            this.lblNombreCliente_389ME.Size = new System.Drawing.Size(44, 13);
            this.lblNombreCliente_389ME.TabIndex = 13;
            this.lblNombreCliente_389ME.Text = "Nombre";
            // 
            // txtNombreCliente_389ME
            // 
            this.txtNombreCliente_389ME.Location = new System.Drawing.Point(730, 37);
            this.txtNombreCliente_389ME.Name = "txtNombreCliente_389ME";
            this.txtNombreCliente_389ME.Size = new System.Drawing.Size(196, 20);
            this.txtNombreCliente_389ME.TabIndex = 14;
            // 
            // lblApellidoCliente_389ME
            // 
            this.lblApellidoCliente_389ME.AutoSize = true;
            this.lblApellidoCliente_389ME.Location = new System.Drawing.Point(670, 70);
            this.lblApellidoCliente_389ME.Name = "lblApellidoCliente_389ME";
            this.lblApellidoCliente_389ME.Size = new System.Drawing.Size(44, 13);
            this.lblApellidoCliente_389ME.TabIndex = 15;
            this.lblApellidoCliente_389ME.Text = "Apellido";
            // 
            // txtApellidoCliente_389ME
            // 
            this.txtApellidoCliente_389ME.Location = new System.Drawing.Point(730, 67);
            this.txtApellidoCliente_389ME.Name = "txtApellidoCliente_389ME";
            this.txtApellidoCliente_389ME.Size = new System.Drawing.Size(196, 20);
            this.txtApellidoCliente_389ME.TabIndex = 16;
            // 
            // lblDireccionCliente_389ME
            // 
            this.lblDireccionCliente_389ME.AutoSize = true;
            this.lblDireccionCliente_389ME.Location = new System.Drawing.Point(670, 100);
            this.lblDireccionCliente_389ME.Name = "lblDireccionCliente_389ME";
            this.lblDireccionCliente_389ME.Size = new System.Drawing.Size(52, 13);
            this.lblDireccionCliente_389ME.TabIndex = 17;
            this.lblDireccionCliente_389ME.Text = "Direccion";
            // 
            // txtDireccionCliente_389ME
            // 
            this.txtDireccionCliente_389ME.Location = new System.Drawing.Point(730, 97);
            this.txtDireccionCliente_389ME.Name = "txtDireccionCliente_389ME";
            this.txtDireccionCliente_389ME.Size = new System.Drawing.Size(196, 20);
            this.txtDireccionCliente_389ME.TabIndex = 18;
            // 
            // lblMailCliente_389ME
            // 
            this.lblMailCliente_389ME.AutoSize = true;
            this.lblMailCliente_389ME.Location = new System.Drawing.Point(670, 130);
            this.lblMailCliente_389ME.Name = "lblMailCliente_389ME";
            this.lblMailCliente_389ME.Size = new System.Drawing.Size(26, 13);
            this.lblMailCliente_389ME.TabIndex = 19;
            this.lblMailCliente_389ME.Text = "Mail";
            // 
            // txtMailCliente_389ME
            // 
            this.txtMailCliente_389ME.Location = new System.Drawing.Point(730, 127);
            this.txtMailCliente_389ME.Name = "txtMailCliente_389ME";
            this.txtMailCliente_389ME.Size = new System.Drawing.Size(196, 20);
            this.txtMailCliente_389ME.TabIndex = 20;
            // 
            // lblTelCliente_389ME
            // 
            this.lblTelCliente_389ME.AutoSize = true;
            this.lblTelCliente_389ME.Location = new System.Drawing.Point(670, 160);
            this.lblTelCliente_389ME.Name = "lblTelCliente_389ME";
            this.lblTelCliente_389ME.Size = new System.Drawing.Size(49, 13);
            this.lblTelCliente_389ME.TabIndex = 21;
            this.lblTelCliente_389ME.Text = "Telefono";
            // 
            // txtTelCliente_389ME
            // 
            this.txtTelCliente_389ME.Location = new System.Drawing.Point(730, 157);
            this.txtTelCliente_389ME.Name = "txtTelCliente_389ME";
            this.txtTelCliente_389ME.Size = new System.Drawing.Size(196, 20);
            this.txtTelCliente_389ME.TabIndex = 22;
            // 
            // lblMetodoPago_389ME
            // 
            this.lblMetodoPago_389ME.AutoSize = true;
            this.lblMetodoPago_389ME.Location = new System.Drawing.Point(670, 200);
            this.lblMetodoPago_389ME.Name = "lblMetodoPago_389ME";
            this.lblMetodoPago_389ME.Size = new System.Drawing.Size(85, 13);
            this.lblMetodoPago_389ME.TabIndex = 23;
            this.lblMetodoPago_389ME.Text = "Metodo de pago";
            // 
            // cmbMetodoPago_389ME
            // 
            this.cmbMetodoPago_389ME.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMetodoPago_389ME.FormattingEnabled = true;
            this.cmbMetodoPago_389ME.Location = new System.Drawing.Point(790, 197);
            this.cmbMetodoPago_389ME.Name = "cmbMetodoPago_389ME";
            this.cmbMetodoPago_389ME.Size = new System.Drawing.Size(136, 21);
            this.cmbMetodoPago_389ME.TabIndex = 24;
            this.cmbMetodoPago_389ME.SelectedIndexChanged += new System.EventHandler(this.cmbMetodoPago_SelectedIndexChanged_389ME);
            // 
            // lblBanco_389ME
            // 
            this.lblBanco_389ME.AutoSize = true;
            this.lblBanco_389ME.Location = new System.Drawing.Point(670, 230);
            this.lblBanco_389ME.Name = "lblBanco_389ME";
            this.lblBanco_389ME.Size = new System.Drawing.Size(38, 13);
            this.lblBanco_389ME.TabIndex = 25;
            this.lblBanco_389ME.Text = "Banco";
            // 
            // txtBanco_389ME
            // 
            this.txtBanco_389ME.Location = new System.Drawing.Point(790, 227);
            this.txtBanco_389ME.Name = "txtBanco_389ME";
            this.txtBanco_389ME.Size = new System.Drawing.Size(136, 20);
            this.txtBanco_389ME.TabIndex = 26;
            // 
            // lblTipoTarjeta_389ME
            // 
            this.lblTipoTarjeta_389ME.AutoSize = true;
            this.lblTipoTarjeta_389ME.Location = new System.Drawing.Point(670, 260);
            this.lblTipoTarjeta_389ME.Name = "lblTipoTarjeta_389ME";
            this.lblTipoTarjeta_389ME.Size = new System.Drawing.Size(75, 13);
            this.lblTipoTarjeta_389ME.TabIndex = 27;
            this.lblTipoTarjeta_389ME.Text = "Tipo de tarjeta";
            // 
            // cmbTipoTarjeta_389ME
            // 
            this.cmbTipoTarjeta_389ME.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTipoTarjeta_389ME.FormattingEnabled = true;
            this.cmbTipoTarjeta_389ME.Location = new System.Drawing.Point(790, 257);
            this.cmbTipoTarjeta_389ME.Name = "cmbTipoTarjeta_389ME";
            this.cmbTipoTarjeta_389ME.Size = new System.Drawing.Size(136, 21);
            this.cmbTipoTarjeta_389ME.TabIndex = 28;
            // 
            // lblMarcaTarjeta_389ME
            // 
            this.lblMarcaTarjeta_389ME.AutoSize = true;
            this.lblMarcaTarjeta_389ME.Location = new System.Drawing.Point(670, 290);
            this.lblMarcaTarjeta_389ME.Name = "lblMarcaTarjeta_389ME";
            this.lblMarcaTarjeta_389ME.Size = new System.Drawing.Size(84, 13);
            this.lblMarcaTarjeta_389ME.TabIndex = 29;
            this.lblMarcaTarjeta_389ME.Text = "Marca de tarjeta";
            // 
            // txtMarcaTarjeta_389ME
            // 
            this.txtMarcaTarjeta_389ME.Location = new System.Drawing.Point(790, 287);
            this.txtMarcaTarjeta_389ME.Name = "txtMarcaTarjeta_389ME";
            this.txtMarcaTarjeta_389ME.Size = new System.Drawing.Size(136, 20);
            this.txtMarcaTarjeta_389ME.TabIndex = 30;
            // 
            // lblFechaExpiracion_389ME
            // 
            this.lblFechaExpiracion_389ME.AutoSize = true;
            this.lblFechaExpiracion_389ME.Location = new System.Drawing.Point(670, 320);
            this.lblFechaExpiracion_389ME.Name = "lblFechaExpiracion_389ME";
            this.lblFechaExpiracion_389ME.Size = new System.Drawing.Size(101, 13);
            this.lblFechaExpiracion_389ME.TabIndex = 31;
            this.lblFechaExpiracion_389ME.Text = "Vencimiento (MM/AA)";
            // 
            // dtpFechaExpiracion_389ME
            // 
            this.dtpFechaExpiracion_389ME.CustomFormat = "MM/yy";
            this.dtpFechaExpiracion_389ME.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaExpiracion_389ME.Location = new System.Drawing.Point(790, 317);
            this.dtpFechaExpiracion_389ME.Name = "dtpFechaExpiracion_389ME";
            this.dtpFechaExpiracion_389ME.ShowUpDown = true;
            this.dtpFechaExpiracion_389ME.Size = new System.Drawing.Size(136, 20);
            this.dtpFechaExpiracion_389ME.TabIndex = 32;
            // 
            // lblCVV_389ME
            // 
            this.lblCVV_389ME.AutoSize = true;
            this.lblCVV_389ME.Location = new System.Drawing.Point(670, 350);
            this.lblCVV_389ME.Name = "lblCVV_389ME";
            this.lblCVV_389ME.Size = new System.Drawing.Size(101, 13);
            this.lblCVV_389ME.TabIndex = 33;
            this.lblCVV_389ME.Text = "Codigo de seguridad";
            // 
            // txtCVV_389ME
            // 
            this.txtCVV_389ME.Location = new System.Drawing.Point(790, 347);
            this.txtCVV_389ME.Name = "txtCVV_389ME";
            this.txtCVV_389ME.Size = new System.Drawing.Size(136, 20);
            this.txtCVV_389ME.TabIndex = 34;
            this.txtCVV_389ME.UseSystemPasswordChar = true;
            // 
            // btnConfirmarVenta_389ME
            // 
            this.btnConfirmarVenta_389ME.Location = new System.Drawing.Point(670, 385);
            this.btnConfirmarVenta_389ME.Name = "btnConfirmarVenta_389ME";
            this.btnConfirmarVenta_389ME.Size = new System.Drawing.Size(256, 32);
            this.btnConfirmarVenta_389ME.TabIndex = 35;
            this.btnConfirmarVenta_389ME.Text = "Confirmar venta";
            this.btnConfirmarVenta_389ME.UseVisualStyleBackColor = true;
            this.btnConfirmarVenta_389ME.Click += new System.EventHandler(this.btnConfirmarVenta_Click_389ME);
            // 
            // txtResultado_389ME
            // 
            this.txtResultado_389ME.BackColor = System.Drawing.SystemColors.Window;
            this.txtResultado_389ME.Location = new System.Drawing.Point(10, 430);
            this.txtResultado_389ME.Multiline = true;
            this.txtResultado_389ME.Name = "txtResultado_389ME";
            this.txtResultado_389ME.ReadOnly = true;
            this.txtResultado_389ME.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtResultado_389ME.Size = new System.Drawing.Size(916, 140);
            this.txtResultado_389ME.TabIndex = 36;
            // 
            // frmVenta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Chocolate;
            this.ClientSize = new System.Drawing.Size(940, 585);
            this.Controls.Add(this.lblProductos_389ME);
            this.Controls.Add(this.dgvProductos_389ME);
            this.Controls.Add(this.lblCantidad_389ME);
            this.Controls.Add(this.txtCantidad_389ME);
            this.Controls.Add(this.btnAgregarCarrito_389ME);
            this.Controls.Add(this.lblCarrito_389ME);
            this.Controls.Add(this.dgvCarrito_389ME);
            this.Controls.Add(this.lblTotal_389ME);
            this.Controls.Add(this.lblRecargo_389ME);
            this.Controls.Add(this.btnQuitarItem_389ME);
            this.Controls.Add(this.lblDNI_389ME);
            this.Controls.Add(this.txtDNI_389ME);
            this.Controls.Add(this.btnBuscarCliente_389ME);
            this.Controls.Add(this.lblNombreCliente_389ME);
            this.Controls.Add(this.txtNombreCliente_389ME);
            this.Controls.Add(this.lblApellidoCliente_389ME);
            this.Controls.Add(this.txtApellidoCliente_389ME);
            this.Controls.Add(this.lblDireccionCliente_389ME);
            this.Controls.Add(this.txtDireccionCliente_389ME);
            this.Controls.Add(this.lblMailCliente_389ME);
            this.Controls.Add(this.txtMailCliente_389ME);
            this.Controls.Add(this.lblTelCliente_389ME);
            this.Controls.Add(this.txtTelCliente_389ME);
            this.Controls.Add(this.lblMetodoPago_389ME);
            this.Controls.Add(this.cmbMetodoPago_389ME);
            this.Controls.Add(this.lblBanco_389ME);
            this.Controls.Add(this.txtBanco_389ME);
            this.Controls.Add(this.lblTipoTarjeta_389ME);
            this.Controls.Add(this.cmbTipoTarjeta_389ME);
            this.Controls.Add(this.lblMarcaTarjeta_389ME);
            this.Controls.Add(this.txtMarcaTarjeta_389ME);
            this.Controls.Add(this.lblFechaExpiracion_389ME);
            this.Controls.Add(this.dtpFechaExpiracion_389ME);
            this.Controls.Add(this.lblCVV_389ME);
            this.Controls.Add(this.txtCVV_389ME);
            this.Controls.Add(this.btnConfirmarVenta_389ME);
            this.Controls.Add(this.txtResultado_389ME);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmVenta";
            this.Text = "Venta en tienda";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmVenta_FormClosing_389ME);
            this.Load += new System.EventHandler(this.frmVenta_Load_389ME);
            this.VisibleChanged += new System.EventHandler(this.frmVenta_VisibleChanged_389ME);
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductos_389ME)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCarrito_389ME)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Label lblProductos_389ME;
        private System.Windows.Forms.DataGridView dgvProductos_389ME;
        private System.Windows.Forms.Label lblCantidad_389ME;
        private System.Windows.Forms.TextBox txtCantidad_389ME;
        private System.Windows.Forms.Button btnAgregarCarrito_389ME;
        private System.Windows.Forms.Label lblCarrito_389ME;
        private System.Windows.Forms.DataGridView dgvCarrito_389ME;
        private System.Windows.Forms.Label lblTotal_389ME;
        private System.Windows.Forms.Label lblRecargo_389ME;
        private System.Windows.Forms.Button btnQuitarItem_389ME;
        private System.Windows.Forms.Label lblDNI_389ME;
        private System.Windows.Forms.TextBox txtDNI_389ME;
        private System.Windows.Forms.Button btnBuscarCliente_389ME;
        private System.Windows.Forms.Label lblNombreCliente_389ME;
        private System.Windows.Forms.TextBox txtNombreCliente_389ME;
        private System.Windows.Forms.Label lblApellidoCliente_389ME;
        private System.Windows.Forms.TextBox txtApellidoCliente_389ME;
        private System.Windows.Forms.Label lblDireccionCliente_389ME;
        private System.Windows.Forms.TextBox txtDireccionCliente_389ME;
        private System.Windows.Forms.Label lblMailCliente_389ME;
        private System.Windows.Forms.TextBox txtMailCliente_389ME;
        private System.Windows.Forms.Label lblTelCliente_389ME;
        private System.Windows.Forms.TextBox txtTelCliente_389ME;
        private System.Windows.Forms.Label lblMetodoPago_389ME;
        private System.Windows.Forms.ComboBox cmbMetodoPago_389ME;
        private System.Windows.Forms.Label lblBanco_389ME;
        private System.Windows.Forms.TextBox txtBanco_389ME;
        private System.Windows.Forms.Label lblTipoTarjeta_389ME;
        private System.Windows.Forms.ComboBox cmbTipoTarjeta_389ME;
        private System.Windows.Forms.Label lblMarcaTarjeta_389ME;
        private System.Windows.Forms.TextBox txtMarcaTarjeta_389ME;
        private System.Windows.Forms.Label lblFechaExpiracion_389ME;
        private System.Windows.Forms.DateTimePicker dtpFechaExpiracion_389ME;
        private System.Windows.Forms.Label lblCVV_389ME;
        private System.Windows.Forms.TextBox txtCVV_389ME;
        private System.Windows.Forms.Button btnConfirmarVenta_389ME;
        private System.Windows.Forms.TextBox txtResultado_389ME;
    }
}
