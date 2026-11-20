namespace CAFECCINO_INGENIERIA_DE_SOFTWARE
{
    partial class frmRecetas
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRecetas));
            this.lblProducto_389ME = new System.Windows.Forms.Label();
            this.cmbProducto_389ME = new System.Windows.Forms.ComboBox();
            this.lblReceta_389ME = new System.Windows.Forms.Label();
            this.dgvReceta_389ME = new System.Windows.Forms.DataGridView();
            this.lblInsumo_389ME = new System.Windows.Forms.Label();
            this.cmbInsumo_389ME = new System.Windows.Forms.ComboBox();
            this.lblCantidad_389ME = new System.Windows.Forms.Label();
            this.txtCantidad_389ME = new System.Windows.Forms.TextBox();
            this.btnAgregarInsumo_389ME = new System.Windows.Forms.Button();
            this.btnQuitarInsumo_389ME = new System.Windows.Forms.Button();
            this.btnGuardarReceta_389ME = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReceta_389ME)).BeginInit();
            this.SuspendLayout();
            // 
            // lblProducto_389ME
            // 
            this.lblProducto_389ME.AutoSize = true;
            this.lblProducto_389ME.Location = new System.Drawing.Point(10, 15);
            this.lblProducto_389ME.Name = "lblProducto_389ME";
            this.lblProducto_389ME.Size = new System.Drawing.Size(50, 13);
            this.lblProducto_389ME.TabIndex = 0;
            this.lblProducto_389ME.Text = "Producto";
            // 
            // cmbProducto_389ME
            // 
            this.cmbProducto_389ME.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProducto_389ME.FormattingEnabled = true;
            this.cmbProducto_389ME.Location = new System.Drawing.Point(120, 12);
            this.cmbProducto_389ME.Name = "cmbProducto_389ME";
            this.cmbProducto_389ME.Size = new System.Drawing.Size(250, 21);
            this.cmbProducto_389ME.TabIndex = 1;
            this.cmbProducto_389ME.SelectedIndexChanged += new System.EventHandler(this.cmbProducto_SelectedIndexChanged_389ME);
            // 
            // lblReceta_389ME
            // 
            this.lblReceta_389ME.AutoSize = true;
            this.lblReceta_389ME.Location = new System.Drawing.Point(10, 45);
            this.lblReceta_389ME.Name = "lblReceta_389ME";
            this.lblReceta_389ME.Size = new System.Drawing.Size(159, 13);
            this.lblReceta_389ME.TabIndex = 2;
            this.lblReceta_389ME.Text = "Insumos por unidad de producto";
            // 
            // dgvReceta_389ME
            // 
            this.dgvReceta_389ME.AllowUserToAddRows = false;
            this.dgvReceta_389ME.AllowUserToDeleteRows = false;
            this.dgvReceta_389ME.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvReceta_389ME.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvReceta_389ME.Location = new System.Drawing.Point(10, 65);
            this.dgvReceta_389ME.MultiSelect = false;
            this.dgvReceta_389ME.Name = "dgvReceta_389ME";
            this.dgvReceta_389ME.ReadOnly = true;
            this.dgvReceta_389ME.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvReceta_389ME.Size = new System.Drawing.Size(600, 200);
            this.dgvReceta_389ME.TabIndex = 3;
            // 
            // lblInsumo_389ME
            // 
            this.lblInsumo_389ME.AutoSize = true;
            this.lblInsumo_389ME.Location = new System.Drawing.Point(10, 285);
            this.lblInsumo_389ME.Name = "lblInsumo_389ME";
            this.lblInsumo_389ME.Size = new System.Drawing.Size(41, 13);
            this.lblInsumo_389ME.TabIndex = 4;
            this.lblInsumo_389ME.Text = "Insumo";
            // 
            // cmbInsumo_389ME
            // 
            this.cmbInsumo_389ME.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbInsumo_389ME.FormattingEnabled = true;
            this.cmbInsumo_389ME.Location = new System.Drawing.Point(120, 282);
            this.cmbInsumo_389ME.Name = "cmbInsumo_389ME";
            this.cmbInsumo_389ME.Size = new System.Drawing.Size(250, 21);
            this.cmbInsumo_389ME.TabIndex = 5;
            // 
            // lblCantidad_389ME
            // 
            this.lblCantidad_389ME.AutoSize = true;
            this.lblCantidad_389ME.Location = new System.Drawing.Point(390, 285);
            this.lblCantidad_389ME.Name = "lblCantidad_389ME";
            this.lblCantidad_389ME.Size = new System.Drawing.Size(49, 13);
            this.lblCantidad_389ME.TabIndex = 6;
            this.lblCantidad_389ME.Text = "Cantidad";
            // 
            // txtCantidad_389ME
            // 
            this.txtCantidad_389ME.Location = new System.Drawing.Point(460, 282);
            this.txtCantidad_389ME.MaxLength = 10;
            this.txtCantidad_389ME.Name = "txtCantidad_389ME";
            this.txtCantidad_389ME.Size = new System.Drawing.Size(80, 20);
            this.txtCantidad_389ME.TabIndex = 7;
            // 
            // btnAgregarInsumo_389ME
            // 
            this.btnAgregarInsumo_389ME.Location = new System.Drawing.Point(120, 320);
            this.btnAgregarInsumo_389ME.Name = "btnAgregarInsumo_389ME";
            this.btnAgregarInsumo_389ME.Size = new System.Drawing.Size(130, 30);
            this.btnAgregarInsumo_389ME.TabIndex = 8;
            this.btnAgregarInsumo_389ME.Text = "Agregar insumo";
            this.btnAgregarInsumo_389ME.UseVisualStyleBackColor = true;
            this.btnAgregarInsumo_389ME.Click += new System.EventHandler(this.btnAgregarInsumo_Click_389ME);
            // 
            // btnQuitarInsumo_389ME
            // 
            this.btnQuitarInsumo_389ME.Location = new System.Drawing.Point(260, 320);
            this.btnQuitarInsumo_389ME.Name = "btnQuitarInsumo_389ME";
            this.btnQuitarInsumo_389ME.Size = new System.Drawing.Size(130, 30);
            this.btnQuitarInsumo_389ME.TabIndex = 9;
            this.btnQuitarInsumo_389ME.Text = "Quitar insumo";
            this.btnQuitarInsumo_389ME.UseVisualStyleBackColor = true;
            this.btnQuitarInsumo_389ME.Click += new System.EventHandler(this.btnQuitarInsumo_Click_389ME);
            // 
            // btnGuardarReceta_389ME
            // 
            this.btnGuardarReceta_389ME.Location = new System.Drawing.Point(460, 320);
            this.btnGuardarReceta_389ME.Name = "btnGuardarReceta_389ME";
            this.btnGuardarReceta_389ME.Size = new System.Drawing.Size(150, 30);
            this.btnGuardarReceta_389ME.TabIndex = 10;
            this.btnGuardarReceta_389ME.Text = "Guardar receta";
            this.btnGuardarReceta_389ME.UseVisualStyleBackColor = true;
            this.btnGuardarReceta_389ME.Click += new System.EventHandler(this.btnGuardarReceta_Click_389ME);
            // 
            // frmRecetas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Chocolate;
            this.ClientSize = new System.Drawing.Size(622, 365);
            this.Controls.Add(this.lblProducto_389ME);
            this.Controls.Add(this.cmbProducto_389ME);
            this.Controls.Add(this.lblReceta_389ME);
            this.Controls.Add(this.dgvReceta_389ME);
            this.Controls.Add(this.lblInsumo_389ME);
            this.Controls.Add(this.cmbInsumo_389ME);
            this.Controls.Add(this.lblCantidad_389ME);
            this.Controls.Add(this.txtCantidad_389ME);
            this.Controls.Add(this.btnAgregarInsumo_389ME);
            this.Controls.Add(this.btnQuitarInsumo_389ME);
            this.Controls.Add(this.btnGuardarReceta_389ME);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmRecetas";
            this.Text = "Recetas";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmRecetas_FormClosing_389ME);
            this.VisibleChanged += new System.EventHandler(this.frmRecetas_VisibleChanged_389ME);
            ((System.ComponentModel.ISupportInitialize)(this.dgvReceta_389ME)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Label lblProducto_389ME;
        private System.Windows.Forms.ComboBox cmbProducto_389ME;
        private System.Windows.Forms.Label lblReceta_389ME;
        private System.Windows.Forms.DataGridView dgvReceta_389ME;
        private System.Windows.Forms.Label lblInsumo_389ME;
        private System.Windows.Forms.ComboBox cmbInsumo_389ME;
        private System.Windows.Forms.Label lblCantidad_389ME;
        private System.Windows.Forms.TextBox txtCantidad_389ME;
        private System.Windows.Forms.Button btnAgregarInsumo_389ME;
        private System.Windows.Forms.Button btnQuitarInsumo_389ME;
        private System.Windows.Forms.Button btnGuardarReceta_389ME;
    }
}
