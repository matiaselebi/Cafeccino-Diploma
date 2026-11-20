namespace CAFECCINO_INGENIERIA_DE_SOFTWARE
{
    partial class frmOrdenesCompra
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmOrdenesCompra));
            this.lblSolicitud_389ME = new System.Windows.Forms.Label();
            this.cmbSolicitud_389ME = new System.Windows.Forms.ComboBox();
            this.lblComparacion_389ME = new System.Windows.Forms.Label();
            this.dgvComparacion_389ME = new System.Windows.Forms.DataGridView();
            this.lblCotizacionesProveedor_389ME = new System.Windows.Forms.Label();
            this.dgvCotizacionesProveedor_389ME = new System.Windows.Forms.DataGridView();
            this.btnGenerarOrden_389ME = new System.Windows.Forms.Button();
            this.lblOrdenes_389ME = new System.Windows.Forms.Label();
            this.dgvOrdenes_389ME = new System.Windows.Forms.DataGridView();
            this.lblDetalleOrden_389ME = new System.Windows.Forms.Label();
            this.dgvDetalleOrden_389ME = new System.Windows.Forms.DataGridView();
            this.btnImprimirOrden_389ME = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvComparacion_389ME)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCotizacionesProveedor_389ME)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrdenes_389ME)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalleOrden_389ME)).BeginInit();
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
            // lblComparacion_389ME
            // 
            this.lblComparacion_389ME.AutoSize = true;
            this.lblComparacion_389ME.Location = new System.Drawing.Point(10, 45);
            this.lblComparacion_389ME.Name = "lblComparacion_389ME";
            this.lblComparacion_389ME.Size = new System.Drawing.Size(146, 13);
            this.lblComparacion_389ME.TabIndex = 2;
            this.lblComparacion_389ME.Text = "Comparacion de proveedores";
            // 
            // dgvComparacion_389ME
            // 
            this.dgvComparacion_389ME.AllowUserToAddRows = false;
            this.dgvComparacion_389ME.AllowUserToDeleteRows = false;
            this.dgvComparacion_389ME.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvComparacion_389ME.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvComparacion_389ME.Location = new System.Drawing.Point(10, 65);
            this.dgvComparacion_389ME.MultiSelect = false;
            this.dgvComparacion_389ME.Name = "dgvComparacion_389ME";
            this.dgvComparacion_389ME.ReadOnly = true;
            this.dgvComparacion_389ME.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvComparacion_389ME.Size = new System.Drawing.Size(430, 160);
            this.dgvComparacion_389ME.TabIndex = 3;
            this.dgvComparacion_389ME.SelectionChanged += new System.EventHandler(this.dgvComparacion_SelectionChanged_389ME);
            // 
            // lblCotizacionesProveedor_389ME
            // 
            this.lblCotizacionesProveedor_389ME.AutoSize = true;
            this.lblCotizacionesProveedor_389ME.Location = new System.Drawing.Point(460, 45);
            this.lblCotizacionesProveedor_389ME.Name = "lblCotizacionesProveedor_389ME";
            this.lblCotizacionesProveedor_389ME.Size = new System.Drawing.Size(256, 13);
            this.lblCotizacionesProveedor_389ME.TabIndex = 4;
            this.lblCotizacionesProveedor_389ME.Text = "Insumos cotizados (seleccione los que va a comprar)";
            // 
            // dgvCotizacionesProveedor_389ME
            // 
            this.dgvCotizacionesProveedor_389ME.AllowUserToAddRows = false;
            this.dgvCotizacionesProveedor_389ME.AllowUserToDeleteRows = false;
            this.dgvCotizacionesProveedor_389ME.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvCotizacionesProveedor_389ME.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCotizacionesProveedor_389ME.Location = new System.Drawing.Point(460, 65);
            this.dgvCotizacionesProveedor_389ME.Name = "dgvCotizacionesProveedor_389ME";
            this.dgvCotizacionesProveedor_389ME.ReadOnly = true;
            this.dgvCotizacionesProveedor_389ME.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCotizacionesProveedor_389ME.Size = new System.Drawing.Size(420, 160);
            this.dgvCotizacionesProveedor_389ME.TabIndex = 5;
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // btnGenerarOrden_389ME
            // 
            this.btnGenerarOrden_389ME.Location = new System.Drawing.Point(700, 233);
            this.btnGenerarOrden_389ME.Name = "btnGenerarOrden_389ME";
            this.btnGenerarOrden_389ME.Size = new System.Drawing.Size(180, 30);
            this.btnGenerarOrden_389ME.TabIndex = 10;
            this.btnGenerarOrden_389ME.Text = "Generar orden de compra";
            this.btnGenerarOrden_389ME.UseVisualStyleBackColor = true;
            this.btnGenerarOrden_389ME.Click += new System.EventHandler(this.btnGenerarOrden_Click_389ME);
            // 
            // lblOrdenes_389ME
            // 
            this.lblOrdenes_389ME.AutoSize = true;
            this.lblOrdenes_389ME.Location = new System.Drawing.Point(10, 280);
            this.lblOrdenes_389ME.Name = "lblOrdenes_389ME";
            this.lblOrdenes_389ME.Size = new System.Drawing.Size(100, 13);
            this.lblOrdenes_389ME.TabIndex = 11;
            this.lblOrdenes_389ME.Text = "Ordenes de compra";
            // 
            // dgvOrdenes_389ME
            // 
            this.dgvOrdenes_389ME.AllowUserToAddRows = false;
            this.dgvOrdenes_389ME.AllowUserToDeleteRows = false;
            this.dgvOrdenes_389ME.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvOrdenes_389ME.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOrdenes_389ME.Location = new System.Drawing.Point(10, 300);
            this.dgvOrdenes_389ME.MultiSelect = false;
            this.dgvOrdenes_389ME.Name = "dgvOrdenes_389ME";
            this.dgvOrdenes_389ME.ReadOnly = true;
            this.dgvOrdenes_389ME.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvOrdenes_389ME.Size = new System.Drawing.Size(870, 150);
            this.dgvOrdenes_389ME.TabIndex = 12;
            this.dgvOrdenes_389ME.SelectionChanged += new System.EventHandler(this.dgvOrdenes_SelectionChanged_389ME);
            // 
            // lblDetalleOrden_389ME
            // 
            this.btnImprimirOrden_389ME.Location = new System.Drawing.Point(760, 273);
            this.btnImprimirOrden_389ME.Name = "btnImprimirOrden_389ME";
            this.btnImprimirOrden_389ME.Size = new System.Drawing.Size(120, 24);
            this.btnImprimirOrden_389ME.TabIndex = 15;
            this.btnImprimirOrden_389ME.Text = "Imprimir orden";
            this.btnImprimirOrden_389ME.UseVisualStyleBackColor = true;
            this.btnImprimirOrden_389ME.Click += new System.EventHandler(this.btnImprimirOrden_Click_389ME);
            this.lblDetalleOrden_389ME.AutoSize = true;
            this.lblDetalleOrden_389ME.Location = new System.Drawing.Point(10, 460);
            this.lblDetalleOrden_389ME.Name = "lblDetalleOrden_389ME";
            this.lblDetalleOrden_389ME.Size = new System.Drawing.Size(96, 13);
            this.lblDetalleOrden_389ME.TabIndex = 13;
            this.lblDetalleOrden_389ME.Text = "Detalle de la orden";
            // 
            // dgvDetalleOrden_389ME
            // 
            this.dgvDetalleOrden_389ME.AllowUserToAddRows = false;
            this.dgvDetalleOrden_389ME.AllowUserToDeleteRows = false;
            this.dgvDetalleOrden_389ME.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDetalleOrden_389ME.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDetalleOrden_389ME.Location = new System.Drawing.Point(10, 480);
            this.dgvDetalleOrden_389ME.MultiSelect = false;
            this.dgvDetalleOrden_389ME.Name = "dgvDetalleOrden_389ME";
            this.dgvDetalleOrden_389ME.ReadOnly = true;
            this.dgvDetalleOrden_389ME.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDetalleOrden_389ME.Size = new System.Drawing.Size(870, 100);
            this.dgvDetalleOrden_389ME.TabIndex = 14;
            // 
            // frmOrdenesCompra
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Chocolate;
            this.ClientSize = new System.Drawing.Size(892, 595);
            this.Controls.Add(this.lblSolicitud_389ME);
            this.Controls.Add(this.cmbSolicitud_389ME);
            this.Controls.Add(this.lblComparacion_389ME);
            this.Controls.Add(this.dgvComparacion_389ME);
            this.Controls.Add(this.lblCotizacionesProveedor_389ME);
            this.Controls.Add(this.dgvCotizacionesProveedor_389ME);
            this.Controls.Add(this.btnGenerarOrden_389ME);
            this.Controls.Add(this.lblOrdenes_389ME);
            this.Controls.Add(this.dgvOrdenes_389ME);
            this.Controls.Add(this.lblDetalleOrden_389ME);
            this.Controls.Add(this.dgvDetalleOrden_389ME);
            this.Controls.Add(this.btnImprimirOrden_389ME);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmOrdenesCompra";
            this.Text = "Ordenes de compra";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmOrdenesCompra_FormClosing_389ME);
            this.VisibleChanged += new System.EventHandler(this.frmOrdenesCompra_VisibleChanged_389ME);
            ((System.ComponentModel.ISupportInitialize)(this.dgvComparacion_389ME)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCotizacionesProveedor_389ME)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrdenes_389ME)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalleOrden_389ME)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Label lblSolicitud_389ME;
        private System.Windows.Forms.ComboBox cmbSolicitud_389ME;
        private System.Windows.Forms.Label lblComparacion_389ME;
        private System.Windows.Forms.DataGridView dgvComparacion_389ME;
        private System.Windows.Forms.Label lblCotizacionesProveedor_389ME;
        private System.Windows.Forms.DataGridView dgvCotizacionesProveedor_389ME;
        private System.Windows.Forms.Button btnGenerarOrden_389ME;
        private System.Windows.Forms.Label lblOrdenes_389ME;
        private System.Windows.Forms.DataGridView dgvOrdenes_389ME;
        private System.Windows.Forms.Label lblDetalleOrden_389ME;
        private System.Windows.Forms.DataGridView dgvDetalleOrden_389ME;
        private System.Windows.Forms.Button btnImprimirOrden_389ME;
    }
}
