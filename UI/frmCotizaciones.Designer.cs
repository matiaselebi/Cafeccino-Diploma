namespace CAFECCINO_INGENIERIA_DE_SOFTWARE
{
    partial class frmCotizaciones
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCotizaciones));
            this.lblSolicitud_389ME = new System.Windows.Forms.Label();
            this.cmbSolicitud_389ME = new System.Windows.Forms.ComboBox();
            this.lblProveedor_389ME = new System.Windows.Forms.Label();
            this.cmbProveedor_389ME = new System.Windows.Forms.ComboBox();
            this.lblInsumosSolicitados_389ME = new System.Windows.Forms.Label();
            this.dgvInsumos_389ME = new System.Windows.Forms.DataGridView();
            this.lblPrecio_389ME = new System.Windows.Forms.Label();
            this.txtPrecio_389ME = new System.Windows.Forms.TextBox();
            this.lblCantidad_389ME = new System.Windows.Forms.Label();
            this.txtCantidad_389ME = new System.Windows.Forms.TextBox();
            this.lblPrecioTotal_389ME = new System.Windows.Forms.Label();
            this.txtPrecioTotal_389ME = new System.Windows.Forms.TextBox();
            this.lblVencimiento_389ME = new System.Windows.Forms.Label();
            this.dtpVencimiento_389ME = new System.Windows.Forms.DateTimePicker();
            this.btnGuardarCotizacion_389ME = new System.Windows.Forms.Button();
            this.lblCotizaciones_389ME = new System.Windows.Forms.Label();
            this.dgvCotizaciones_389ME = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInsumos_389ME)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCotizaciones_389ME)).BeginInit();
            this.SuspendLayout();
            // 
            // lblSolicitud_389ME
            // 
            this.lblSolicitud_389ME.AutoSize = true;
            this.lblSolicitud_389ME.Location = new System.Drawing.Point(10, 15);
            this.lblSolicitud_389ME.Name = "lblSolicitud_389ME";
            this.lblSolicitud_389ME.Size = new System.Drawing.Size(47, 13);
            this.lblSolicitud_389ME.TabIndex = 0;
            this.lblSolicitud_389ME.Text = "Solicitud";
            // 
            // cmbSolicitud_389ME
            // 
            this.cmbSolicitud_389ME.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSolicitud_389ME.FormattingEnabled = true;
            this.cmbSolicitud_389ME.Location = new System.Drawing.Point(110, 12);
            this.cmbSolicitud_389ME.Name = "cmbSolicitud_389ME";
            this.cmbSolicitud_389ME.Size = new System.Drawing.Size(300, 21);
            this.cmbSolicitud_389ME.TabIndex = 1;
            this.cmbSolicitud_389ME.SelectedIndexChanged += new System.EventHandler(this.cmbSolicitud_SelectedIndexChanged_389ME);
            // 
            // lblProveedor_389ME
            // 
            this.lblProveedor_389ME.AutoSize = true;
            this.lblProveedor_389ME.Location = new System.Drawing.Point(430, 15);
            this.lblProveedor_389ME.Name = "lblProveedor_389ME";
            this.lblProveedor_389ME.Size = new System.Drawing.Size(56, 13);
            this.lblProveedor_389ME.TabIndex = 2;
            this.lblProveedor_389ME.Text = "Proveedor";
            // 
            // cmbProveedor_389ME
            // 
            this.cmbProveedor_389ME.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProveedor_389ME.FormattingEnabled = true;
            this.cmbProveedor_389ME.Location = new System.Drawing.Point(520, 12);
            this.cmbProveedor_389ME.Name = "cmbProveedor_389ME";
            this.cmbProveedor_389ME.Size = new System.Drawing.Size(280, 21);
            this.cmbProveedor_389ME.TabIndex = 3;
            // 
            // lblInsumosSolicitados_389ME
            // 
            this.lblInsumosSolicitados_389ME.AutoSize = true;
            this.lblInsumosSolicitados_389ME.Location = new System.Drawing.Point(10, 45);
            this.lblInsumosSolicitados_389ME.Name = "lblInsumosSolicitados_389ME";
            this.lblInsumosSolicitados_389ME.Size = new System.Drawing.Size(98, 13);
            this.lblInsumosSolicitados_389ME.TabIndex = 4;
            this.lblInsumosSolicitados_389ME.Text = "Insumos solicitados";
            // 
            // dgvInsumos_389ME
            // 
            this.dgvInsumos_389ME.AllowUserToAddRows = false;
            this.dgvInsumos_389ME.AllowUserToDeleteRows = false;
            this.dgvInsumos_389ME.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvInsumos_389ME.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvInsumos_389ME.Location = new System.Drawing.Point(10, 65);
            this.dgvInsumos_389ME.MultiSelect = false;
            this.dgvInsumos_389ME.Name = "dgvInsumos_389ME";
            this.dgvInsumos_389ME.ReadOnly = true;
            this.dgvInsumos_389ME.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvInsumos_389ME.Size = new System.Drawing.Size(380, 170);
            this.dgvInsumos_389ME.TabIndex = 5;
            // 
            // lblPrecio_389ME
            // 
            this.lblPrecio_389ME.AutoSize = true;
            this.lblPrecio_389ME.Location = new System.Drawing.Point(410, 70);
            this.lblPrecio_389ME.Name = "lblPrecio_389ME";
            this.lblPrecio_389ME.Size = new System.Drawing.Size(74, 13);
            this.lblPrecio_389ME.TabIndex = 6;
            this.lblPrecio_389ME.Text = "Precio unitario";
            // 
            // txtPrecio_389ME
            // 
            this.txtPrecio_389ME.Location = new System.Drawing.Point(530, 67);
            this.txtPrecio_389ME.MaxLength = 10;
            this.txtPrecio_389ME.Name = "txtPrecio_389ME";
            this.txtPrecio_389ME.Size = new System.Drawing.Size(120, 20);
            this.txtPrecio_389ME.TabIndex = 7;
            // 
            // lblCantidad_389ME
            // 
            this.lblCantidad_389ME.AutoSize = true;
            this.lblCantidad_389ME.Location = new System.Drawing.Point(410, 100);
            this.lblCantidad_389ME.Name = "lblCantidad_389ME";
            this.lblCantidad_389ME.Size = new System.Drawing.Size(49, 13);
            this.lblCantidad_389ME.TabIndex = 8;
            this.lblCantidad_389ME.Text = "Cantidad";
            // 
            // txtCantidad_389ME
            // 
            this.txtCantidad_389ME.Location = new System.Drawing.Point(530, 97);
            this.txtCantidad_389ME.MaxLength = 10;
            this.txtCantidad_389ME.Name = "txtCantidad_389ME";
            this.lblPrecioTotal_389ME.Location = new System.Drawing.Point(410, 130);
            this.lblPrecioTotal_389ME.Name = "lblPrecioTotal_389ME";
            this.lblPrecioTotal_389ME.AutoSize = true;
            this.lblPrecioTotal_389ME.Text = "Precio total";
            this.txtPrecioTotal_389ME.Location = new System.Drawing.Point(530, 127);
            this.txtPrecioTotal_389ME.Name = "txtPrecioTotal_389ME";
            this.txtPrecioTotal_389ME.Size = new System.Drawing.Size(120, 20);
            this.txtPrecioTotal_389ME.MaxLength = 12;
            this.txtCantidad_389ME.Size = new System.Drawing.Size(120, 20);
            this.txtCantidad_389ME.TabIndex = 9;
            // 
            // lblVencimiento_389ME
            // 
            this.lblVencimiento_389ME.AutoSize = true;
            this.lblVencimiento_389ME.Location = new System.Drawing.Point(410, 160);
            this.lblVencimiento_389ME.Name = "lblVencimiento_389ME";
            this.lblVencimiento_389ME.Size = new System.Drawing.Size(65, 13);
            this.lblVencimiento_389ME.TabIndex = 10;
            this.lblVencimiento_389ME.Text = "Vencimiento";
            // 
            // dtpVencimiento_389ME
            // 
            this.dtpVencimiento_389ME.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpVencimiento_389ME.Location = new System.Drawing.Point(530, 157);
            this.dtpVencimiento_389ME.Name = "dtpVencimiento_389ME";
            this.dtpVencimiento_389ME.Size = new System.Drawing.Size(120, 20);
            this.dtpVencimiento_389ME.TabIndex = 11;
            // 
            // btnGuardarCotizacion_389ME
            // 
            this.btnGuardarCotizacion_389ME.Location = new System.Drawing.Point(530, 195);
            this.btnGuardarCotizacion_389ME.Name = "btnGuardarCotizacion_389ME";
            this.btnGuardarCotizacion_389ME.Size = new System.Drawing.Size(160, 30);
            this.btnGuardarCotizacion_389ME.TabIndex = 12;
            this.btnGuardarCotizacion_389ME.Text = "Guardar cotizacion";
            this.btnGuardarCotizacion_389ME.UseVisualStyleBackColor = true;
            this.btnGuardarCotizacion_389ME.Click += new System.EventHandler(this.btnGuardarCotizacion_Click_389ME);
            // 
            // lblCotizaciones_389ME
            // 
            this.lblCotizaciones_389ME.AutoSize = true;
            this.lblCotizaciones_389ME.Location = new System.Drawing.Point(10, 250);
            this.lblCotizaciones_389ME.Name = "lblCotizaciones_389ME";
            this.lblCotizaciones_389ME.Size = new System.Drawing.Size(114, 13);
            this.lblCotizaciones_389ME.TabIndex = 13;
            this.lblCotizaciones_389ME.Text = "Cotizaciones cargadas";
            // 
            // dgvCotizaciones_389ME
            // 
            this.dgvCotizaciones_389ME.AllowUserToAddRows = false;
            this.dgvCotizaciones_389ME.AllowUserToDeleteRows = false;
            this.dgvCotizaciones_389ME.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvCotizaciones_389ME.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCotizaciones_389ME.Location = new System.Drawing.Point(10, 270);
            this.dgvCotizaciones_389ME.MultiSelect = false;
            this.dgvCotizaciones_389ME.Name = "dgvCotizaciones_389ME";
            this.dgvCotizaciones_389ME.ReadOnly = true;
            this.dgvCotizaciones_389ME.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCotizaciones_389ME.Size = new System.Drawing.Size(790, 200);
            this.dgvCotizaciones_389ME.TabIndex = 14;
            // 
            // frmCotizaciones
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Chocolate;
            this.ClientSize = new System.Drawing.Size(812, 485);
            this.Controls.Add(this.lblSolicitud_389ME);
            this.Controls.Add(this.cmbSolicitud_389ME);
            this.Controls.Add(this.lblProveedor_389ME);
            this.Controls.Add(this.cmbProveedor_389ME);
            this.Controls.Add(this.lblInsumosSolicitados_389ME);
            this.Controls.Add(this.dgvInsumos_389ME);
            this.Controls.Add(this.lblPrecio_389ME);
            this.Controls.Add(this.txtPrecio_389ME);
            this.Controls.Add(this.lblCantidad_389ME);
            this.Controls.Add(this.txtCantidad_389ME);
            this.Controls.Add(this.lblPrecioTotal_389ME);
            this.Controls.Add(this.txtPrecioTotal_389ME);
            this.Controls.Add(this.lblVencimiento_389ME);
            this.Controls.Add(this.dtpVencimiento_389ME);
            this.Controls.Add(this.btnGuardarCotizacion_389ME);
            this.Controls.Add(this.lblCotizaciones_389ME);
            this.Controls.Add(this.dgvCotizaciones_389ME);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmCotizaciones";
            this.Text = "Cotizaciones";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmCotizaciones_FormClosing_389ME);
            this.VisibleChanged += new System.EventHandler(this.frmCotizaciones_VisibleChanged_389ME);
            ((System.ComponentModel.ISupportInitialize)(this.dgvInsumos_389ME)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCotizaciones_389ME)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Label lblSolicitud_389ME;
        private System.Windows.Forms.ComboBox cmbSolicitud_389ME;
        private System.Windows.Forms.Label lblProveedor_389ME;
        private System.Windows.Forms.ComboBox cmbProveedor_389ME;
        private System.Windows.Forms.Label lblInsumosSolicitados_389ME;
        private System.Windows.Forms.DataGridView dgvInsumos_389ME;
        private System.Windows.Forms.Label lblPrecio_389ME;
        private System.Windows.Forms.TextBox txtPrecio_389ME;
        private System.Windows.Forms.Label lblCantidad_389ME;
        private System.Windows.Forms.TextBox txtCantidad_389ME;
        private System.Windows.Forms.Label lblPrecioTotal_389ME;
        private System.Windows.Forms.TextBox txtPrecioTotal_389ME;
        private System.Windows.Forms.Label lblVencimiento_389ME;
        private System.Windows.Forms.DateTimePicker dtpVencimiento_389ME;
        private System.Windows.Forms.Button btnGuardarCotizacion_389ME;
        private System.Windows.Forms.Label lblCotizaciones_389ME;
        private System.Windows.Forms.DataGridView dgvCotizaciones_389ME;
    }
}
