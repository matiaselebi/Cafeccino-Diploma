namespace CAFECCINO_INGENIERIA_DE_SOFTWARE
{
    partial class frmInsumos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmInsumos));
            this.lblInsumos_389ME = new System.Windows.Forms.Label();
            this.dgvInsumos_389ME = new System.Windows.Forms.DataGridView();
            this.lblNombre_389ME = new System.Windows.Forms.Label();
            this.txtNombre_389ME = new System.Windows.Forms.TextBox();
            this.lblTipo_389ME = new System.Windows.Forms.Label();
            this.txtTipo_389ME = new System.Windows.Forms.TextBox();
            this.lblUnidad_389ME = new System.Windows.Forms.Label();
            this.cmbUnidad_389ME = new System.Windows.Forms.ComboBox();
            this.lblPrecio_389ME = new System.Windows.Forms.Label();
            this.txtPrecio_389ME = new System.Windows.Forms.TextBox();
            this.lblStock_389ME = new System.Windows.Forms.Label();
            this.txtStock_389ME = new System.Windows.Forms.TextBox();
            this.lblStockMinimo_389ME = new System.Windows.Forms.Label();
            this.txtStockMinimo_389ME = new System.Windows.Forms.TextBox();
            this.lblStockMaximo_389ME = new System.Windows.Forms.Label();
            this.txtStockMaximo_389ME = new System.Windows.Forms.TextBox();
            this.btnAgregar_389ME = new System.Windows.Forms.Button();
            this.btnModificar_389ME = new System.Windows.Forms.Button();
            this.btnEliminar_389ME = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInsumos_389ME)).BeginInit();
            this.SuspendLayout();
            // 
            // lblInsumos_389ME
            // 
            this.lblInsumos_389ME.AutoSize = true;
            this.lblInsumos_389ME.Location = new System.Drawing.Point(10, 10);
            this.lblInsumos_389ME.Name = "lblInsumos_389ME";
            this.lblInsumos_389ME.Size = new System.Drawing.Size(46, 13);
            this.lblInsumos_389ME.TabIndex = 0;
            this.lblInsumos_389ME.Text = "Insumos";
            // 
            // dgvInsumos_389ME
            // 
            this.dgvInsumos_389ME.AllowUserToAddRows = false;
            this.dgvInsumos_389ME.AllowUserToDeleteRows = false;
            this.dgvInsumos_389ME.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvInsumos_389ME.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvInsumos_389ME.Location = new System.Drawing.Point(10, 30);
            this.dgvInsumos_389ME.MultiSelect = false;
            this.dgvInsumos_389ME.Name = "dgvInsumos_389ME";
            this.dgvInsumos_389ME.ReadOnly = true;
            this.dgvInsumos_389ME.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvInsumos_389ME.Size = new System.Drawing.Size(760, 200);
            this.dgvInsumos_389ME.TabIndex = 1;
            this.dgvInsumos_389ME.SelectionChanged += new System.EventHandler(this.dgvInsumos_SelectionChanged_389ME);
            // 
            // lblNombre_389ME
            // 
            this.lblNombre_389ME.AutoSize = true;
            this.lblNombre_389ME.Location = new System.Drawing.Point(10, 250);
            this.lblNombre_389ME.Name = "lblNombre_389ME";
            this.lblNombre_389ME.Size = new System.Drawing.Size(44, 13);
            this.lblNombre_389ME.TabIndex = 2;
            this.lblNombre_389ME.Text = "Nombre";
            // 
            // txtNombre_389ME
            // 
            this.txtNombre_389ME.Location = new System.Drawing.Point(120, 247);
            this.txtNombre_389ME.MaxLength = 50;
            this.txtNombre_389ME.Name = "txtNombre_389ME";
            this.txtNombre_389ME.Size = new System.Drawing.Size(200, 20);
            this.txtNombre_389ME.TabIndex = 3;
            // 
            // lblTipo_389ME
            // 
            this.lblTipo_389ME.AutoSize = true;
            this.lblTipo_389ME.Location = new System.Drawing.Point(10, 280);
            this.lblTipo_389ME.Name = "lblTipo_389ME";
            this.lblTipo_389ME.Size = new System.Drawing.Size(28, 13);
            this.lblTipo_389ME.TabIndex = 4;
            this.lblTipo_389ME.Text = "Tipo";
            // 
            // txtTipo_389ME
            // 
            this.txtTipo_389ME.Location = new System.Drawing.Point(120, 277);
            this.txtTipo_389ME.MaxLength = 50;
            this.txtTipo_389ME.Name = "txtTipo_389ME";
            this.txtTipo_389ME.Size = new System.Drawing.Size(200, 20);
            this.txtTipo_389ME.TabIndex = 5;
            // 
            // lblUnidad_389ME
            // 
            this.lblUnidad_389ME.AutoSize = true;
            this.lblUnidad_389ME.Location = new System.Drawing.Point(10, 310);
            this.lblUnidad_389ME.Name = "lblUnidad_389ME";
            this.lblUnidad_389ME.Size = new System.Drawing.Size(93, 13);
            this.lblUnidad_389ME.TabIndex = 6;
            this.lblUnidad_389ME.Text = "Unidad de medida";
            // 
            // cmbUnidad_389ME
            // 
            this.cmbUnidad_389ME.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUnidad_389ME.FormattingEnabled = true;
            this.cmbUnidad_389ME.Location = new System.Drawing.Point(120, 307);
            this.cmbUnidad_389ME.Name = "cmbUnidad_389ME";
            this.cmbUnidad_389ME.Size = new System.Drawing.Size(200, 21);
            this.cmbUnidad_389ME.TabIndex = 7;
            // 
            // lblPrecio_389ME
            // 
            this.lblPrecio_389ME.AutoSize = true;
            this.lblPrecio_389ME.Location = new System.Drawing.Point(10, 340);
            this.lblPrecio_389ME.Name = "lblPrecio_389ME";
            this.lblPrecio_389ME.Size = new System.Drawing.Size(37, 13);
            this.lblPrecio_389ME.TabIndex = 8;
            this.lblPrecio_389ME.Text = "Precio";
            // 
            // txtPrecio_389ME
            // 
            this.txtPrecio_389ME.Location = new System.Drawing.Point(120, 337);
            this.txtPrecio_389ME.MaxLength = 10;
            this.txtPrecio_389ME.Name = "txtPrecio_389ME";
            this.txtPrecio_389ME.Size = new System.Drawing.Size(200, 20);
            this.txtPrecio_389ME.TabIndex = 9;
            // 
            // lblStock_389ME
            // 
            this.lblStock_389ME.AutoSize = true;
            this.lblStock_389ME.Location = new System.Drawing.Point(400, 250);
            this.lblStock_389ME.Name = "lblStock_389ME";
            this.lblStock_389ME.Size = new System.Drawing.Size(35, 13);
            this.lblStock_389ME.TabIndex = 10;
            this.lblStock_389ME.Text = "Stock";
            // 
            // txtStock_389ME
            // 
            this.txtStock_389ME.Location = new System.Drawing.Point(520, 247);
            this.txtStock_389ME.MaxLength = 10;
            this.txtStock_389ME.Name = "txtStock_389ME";
            this.txtStock_389ME.Size = new System.Drawing.Size(200, 20);
            this.txtStock_389ME.TabIndex = 11;
            // 
            // lblStockMinimo_389ME
            // 
            this.lblStockMinimo_389ME.AutoSize = true;
            this.lblStockMinimo_389ME.Location = new System.Drawing.Point(400, 280);
            this.lblStockMinimo_389ME.Name = "lblStockMinimo_389ME";
            this.lblStockMinimo_389ME.Size = new System.Drawing.Size(70, 13);
            this.lblStockMinimo_389ME.TabIndex = 12;
            this.lblStockMinimo_389ME.Text = "Stock minimo";
            // 
            // txtStockMinimo_389ME
            // 
            this.txtStockMinimo_389ME.Location = new System.Drawing.Point(520, 277);
            this.txtStockMinimo_389ME.MaxLength = 10;
            this.txtStockMinimo_389ME.Name = "txtStockMinimo_389ME";
            this.txtStockMinimo_389ME.Size = new System.Drawing.Size(200, 20);
            this.txtStockMinimo_389ME.TabIndex = 13;
            // 
            // lblStockMaximo_389ME
            // 
            this.lblStockMaximo_389ME.AutoSize = true;
            this.lblStockMaximo_389ME.Location = new System.Drawing.Point(400, 310);
            this.lblStockMaximo_389ME.Name = "lblStockMaximo_389ME";
            this.lblStockMaximo_389ME.Size = new System.Drawing.Size(73, 13);
            this.lblStockMaximo_389ME.TabIndex = 14;
            this.lblStockMaximo_389ME.Text = "Stock maximo";
            // 
            // txtStockMaximo_389ME
            // 
            this.txtStockMaximo_389ME.Location = new System.Drawing.Point(520, 307);
            this.txtStockMaximo_389ME.MaxLength = 10;
            this.txtStockMaximo_389ME.Name = "txtStockMaximo_389ME";
            this.txtStockMaximo_389ME.Size = new System.Drawing.Size(200, 20);
            this.txtStockMaximo_389ME.TabIndex = 15;
            // 
            // btnAgregar_389ME
            // 
            this.btnAgregar_389ME.Location = new System.Drawing.Point(120, 380);
            this.btnAgregar_389ME.Name = "btnAgregar_389ME";
            this.btnAgregar_389ME.Size = new System.Drawing.Size(120, 30);
            this.btnAgregar_389ME.TabIndex = 16;
            this.btnAgregar_389ME.Text = "Agregar";
            this.btnAgregar_389ME.UseVisualStyleBackColor = true;
            this.btnAgregar_389ME.Click += new System.EventHandler(this.btnAgregar_Click_389ME);
            // 
            // btnModificar_389ME
            // 
            this.btnModificar_389ME.Location = new System.Drawing.Point(260, 380);
            this.btnModificar_389ME.Name = "btnModificar_389ME";
            this.btnModificar_389ME.Size = new System.Drawing.Size(120, 30);
            this.btnModificar_389ME.TabIndex = 17;
            this.btnModificar_389ME.Text = "Modificar";
            this.btnModificar_389ME.UseVisualStyleBackColor = true;
            this.btnModificar_389ME.Click += new System.EventHandler(this.btnModificar_Click_389ME);
            // 
            // btnEliminar_389ME
            // 
            this.btnEliminar_389ME.Location = new System.Drawing.Point(400, 380);
            this.btnEliminar_389ME.Name = "btnEliminar_389ME";
            this.btnEliminar_389ME.Size = new System.Drawing.Size(120, 30);
            this.btnEliminar_389ME.TabIndex = 18;
            this.btnEliminar_389ME.Text = "Eliminar";
            this.btnEliminar_389ME.UseVisualStyleBackColor = true;
            this.btnEliminar_389ME.Click += new System.EventHandler(this.btnEliminar_Click_389ME);
            // 
            // frmInsumos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Chocolate;
            this.ClientSize = new System.Drawing.Size(782, 430);
            this.Controls.Add(this.lblInsumos_389ME);
            this.Controls.Add(this.dgvInsumos_389ME);
            this.Controls.Add(this.lblNombre_389ME);
            this.Controls.Add(this.txtNombre_389ME);
            this.Controls.Add(this.lblTipo_389ME);
            this.Controls.Add(this.txtTipo_389ME);
            this.Controls.Add(this.lblUnidad_389ME);
            this.Controls.Add(this.cmbUnidad_389ME);
            this.Controls.Add(this.lblPrecio_389ME);
            this.Controls.Add(this.txtPrecio_389ME);
            this.Controls.Add(this.lblStock_389ME);
            this.Controls.Add(this.txtStock_389ME);
            this.Controls.Add(this.lblStockMinimo_389ME);
            this.Controls.Add(this.txtStockMinimo_389ME);
            this.Controls.Add(this.lblStockMaximo_389ME);
            this.Controls.Add(this.txtStockMaximo_389ME);
            this.Controls.Add(this.btnAgregar_389ME);
            this.Controls.Add(this.btnModificar_389ME);
            this.Controls.Add(this.btnEliminar_389ME);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmInsumos";
            this.Text = "Gestion de insumos";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmInsumos_FormClosing_389ME);
            this.VisibleChanged += new System.EventHandler(this.frmInsumos_VisibleChanged_389ME);
            ((System.ComponentModel.ISupportInitialize)(this.dgvInsumos_389ME)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Label lblInsumos_389ME;
        private System.Windows.Forms.DataGridView dgvInsumos_389ME;
        private System.Windows.Forms.Label lblNombre_389ME;
        private System.Windows.Forms.TextBox txtNombre_389ME;
        private System.Windows.Forms.Label lblTipo_389ME;
        private System.Windows.Forms.TextBox txtTipo_389ME;
        private System.Windows.Forms.Label lblUnidad_389ME;
        private System.Windows.Forms.ComboBox cmbUnidad_389ME;
        private System.Windows.Forms.Label lblPrecio_389ME;
        private System.Windows.Forms.TextBox txtPrecio_389ME;
        private System.Windows.Forms.Label lblStock_389ME;
        private System.Windows.Forms.TextBox txtStock_389ME;
        private System.Windows.Forms.Label lblStockMinimo_389ME;
        private System.Windows.Forms.TextBox txtStockMinimo_389ME;
        private System.Windows.Forms.Label lblStockMaximo_389ME;
        private System.Windows.Forms.TextBox txtStockMaximo_389ME;
        private System.Windows.Forms.Button btnAgregar_389ME;
        private System.Windows.Forms.Button btnModificar_389ME;
        private System.Windows.Forms.Button btnEliminar_389ME;
    }
}
