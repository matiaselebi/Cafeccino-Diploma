namespace CAFECCINO_INGENIERIA_DE_SOFTWARE
{
    partial class frmSolicitudCotizacion
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSolicitudCotizacion));
            this.lblSugerencias_389ME = new System.Windows.Forms.Label();
            this.dgvSugerencias_389ME = new System.Windows.Forms.DataGridView();
            this.btnAgregarSugeridos_389ME = new System.Windows.Forms.Button();
            this.btnAgregarInsumo_389ME = new System.Windows.Forms.Button();
            this.lblInsumosSolicitud_389ME = new System.Windows.Forms.Label();
            this.dgvSolicitud_389ME = new System.Windows.Forms.DataGridView();
            this.btnQuitarInsumo_389ME = new System.Windows.Forms.Button();
            this.lblProveedores_389ME = new System.Windows.Forms.Label();
            this.clbProveedores_389ME = new System.Windows.Forms.CheckedListBox();
            this.btnConfirmarSolicitud_389ME = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSugerencias_389ME)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSolicitud_389ME)).BeginInit();
            this.SuspendLayout();
            // 
            // lblSugerencias_389ME
            // 
            this.lblSugerencias_389ME.AutoSize = true;
            this.lblSugerencias_389ME.Location = new System.Drawing.Point(10, 10);
            this.lblSugerencias_389ME.Name = "lblSugerencias_389ME";
            this.lblSugerencias_389ME.Size = new System.Drawing.Size(162, 13);
            this.lblSugerencias_389ME.TabIndex = 0;
            this.lblSugerencias_389ME.Text = "Insumos y sugerencia de compra";
            // 
            // dgvSugerencias_389ME
            // 
            this.dgvSugerencias_389ME.AllowUserToAddRows = false;
            this.dgvSugerencias_389ME.AllowUserToDeleteRows = false;
            this.dgvSugerencias_389ME.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSugerencias_389ME.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSugerencias_389ME.Location = new System.Drawing.Point(10, 30);
            this.dgvSugerencias_389ME.MultiSelect = false;
            this.dgvSugerencias_389ME.Name = "dgvSugerencias_389ME";
            this.dgvSugerencias_389ME.ReadOnly = true;
            this.dgvSugerencias_389ME.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSugerencias_389ME.Size = new System.Drawing.Size(870, 180);
            this.dgvSugerencias_389ME.TabIndex = 1;
            // 
            // btnAgregarSugeridos_389ME
            // 
            this.btnAgregarSugeridos_389ME.Location = new System.Drawing.Point(10, 220);
            this.btnAgregarSugeridos_389ME.Name = "btnAgregarSugeridos_389ME";
            this.btnAgregarSugeridos_389ME.Size = new System.Drawing.Size(160, 30);
            this.btnAgregarSugeridos_389ME.TabIndex = 2;
            this.btnAgregarSugeridos_389ME.Text = "Agregar los sugeridos";
            this.btnAgregarSugeridos_389ME.UseVisualStyleBackColor = true;
            this.btnAgregarSugeridos_389ME.Click += new System.EventHandler(this.btnAgregarSugeridos_Click_389ME);
            // 
            // 
            // 
            // 
            // 
            // btnAgregarInsumo_389ME
            // 
            this.btnAgregarInsumo_389ME.Location = new System.Drawing.Point(345, 220);
            this.btnAgregarInsumo_389ME.Name = "btnAgregarInsumo_389ME";
            this.btnAgregarInsumo_389ME.Size = new System.Drawing.Size(130, 30);
            this.btnAgregarInsumo_389ME.TabIndex = 5;
            this.btnAgregarInsumo_389ME.Text = "Agregar insumo";
            this.btnAgregarInsumo_389ME.UseVisualStyleBackColor = true;
            this.btnAgregarInsumo_389ME.Click += new System.EventHandler(this.btnAgregarInsumo_Click_389ME);
            // 
            // lblInsumosSolicitud_389ME
            // 
            this.lblInsumosSolicitud_389ME.AutoSize = true;
            this.lblInsumosSolicitud_389ME.Location = new System.Drawing.Point(10, 262);
            this.lblInsumosSolicitud_389ME.Name = "lblInsumosSolicitud_389ME";
            this.lblInsumosSolicitud_389ME.Size = new System.Drawing.Size(113, 13);
            this.lblInsumosSolicitud_389ME.TabIndex = 6;
            this.lblInsumosSolicitud_389ME.Text = "Insumos de la solicitud";
            // 
            // dgvSolicitud_389ME
            // 
            this.dgvSolicitud_389ME.AllowUserToAddRows = false;
            this.dgvSolicitud_389ME.AllowUserToDeleteRows = false;
            this.dgvSolicitud_389ME.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSolicitud_389ME.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSolicitud_389ME.Location = new System.Drawing.Point(10, 280);
            this.dgvSolicitud_389ME.MultiSelect = false;
            this.dgvSolicitud_389ME.Name = "dgvSolicitud_389ME";
            this.dgvSolicitud_389ME.ReadOnly = true;
            this.dgvSolicitud_389ME.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSolicitud_389ME.Size = new System.Drawing.Size(540, 180);
            this.dgvSolicitud_389ME.TabIndex = 7;
            // 
            // btnQuitarInsumo_389ME
            // 
            this.btnQuitarInsumo_389ME.Location = new System.Drawing.Point(10, 470);
            this.btnQuitarInsumo_389ME.Name = "btnQuitarInsumo_389ME";
            this.btnQuitarInsumo_389ME.Size = new System.Drawing.Size(130, 30);
            this.btnQuitarInsumo_389ME.TabIndex = 8;
            this.btnQuitarInsumo_389ME.Text = "Quitar insumo";
            this.btnQuitarInsumo_389ME.UseVisualStyleBackColor = true;
            this.btnQuitarInsumo_389ME.Click += new System.EventHandler(this.btnQuitarInsumo_Click_389ME);
            // 
            // lblProveedores_389ME
            // 
            this.lblProveedores_389ME.AutoSize = true;
            this.lblProveedores_389ME.Location = new System.Drawing.Point(570, 262);
            this.lblProveedores_389ME.Name = "lblProveedores_389ME";
            this.lblProveedores_389ME.Size = new System.Drawing.Size(110, 13);
            this.lblProveedores_389ME.TabIndex = 9;
            this.lblProveedores_389ME.Text = "Proveedores a cotizar";
            // 
            // clbProveedores_389ME
            // 
            this.clbProveedores_389ME.CheckOnClick = true;
            this.clbProveedores_389ME.FormattingEnabled = true;
            this.clbProveedores_389ME.Location = new System.Drawing.Point(570, 280);
            this.clbProveedores_389ME.Name = "clbProveedores_389ME";
            this.clbProveedores_389ME.Size = new System.Drawing.Size(310, 184);
            this.clbProveedores_389ME.TabIndex = 10;
            // 
            // btnConfirmarSolicitud_389ME
            // 
            this.btnConfirmarSolicitud_389ME.Location = new System.Drawing.Point(570, 470);
            this.btnConfirmarSolicitud_389ME.Name = "btnConfirmarSolicitud_389ME";
            this.btnConfirmarSolicitud_389ME.Size = new System.Drawing.Size(310, 30);
            this.btnConfirmarSolicitud_389ME.TabIndex = 11;
            this.btnConfirmarSolicitud_389ME.Text = "Confirmar solicitud";
            this.btnConfirmarSolicitud_389ME.UseVisualStyleBackColor = true;
            this.btnConfirmarSolicitud_389ME.Click += new System.EventHandler(this.btnConfirmarSolicitud_Click_389ME);
            // 
            // frmSolicitudCotizacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Chocolate;
            this.ClientSize = new System.Drawing.Size(892, 515);
            this.Controls.Add(this.lblSugerencias_389ME);
            this.Controls.Add(this.dgvSugerencias_389ME);
            this.Controls.Add(this.btnAgregarSugeridos_389ME);
            this.Controls.Add(this.btnAgregarInsumo_389ME);
            this.Controls.Add(this.lblInsumosSolicitud_389ME);
            this.Controls.Add(this.dgvSolicitud_389ME);
            this.Controls.Add(this.btnQuitarInsumo_389ME);
            this.Controls.Add(this.lblProveedores_389ME);
            this.Controls.Add(this.clbProveedores_389ME);
            this.Controls.Add(this.btnConfirmarSolicitud_389ME);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmSolicitudCotizacion";
            this.Text = "Solicitud de cotizacion";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmSolicitudCotizacion_FormClosing_389ME);
            this.VisibleChanged += new System.EventHandler(this.frmSolicitudCotizacion_VisibleChanged_389ME);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSugerencias_389ME)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSolicitud_389ME)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Label lblSugerencias_389ME;
        private System.Windows.Forms.DataGridView dgvSugerencias_389ME;
        private System.Windows.Forms.Button btnAgregarSugeridos_389ME;
        private System.Windows.Forms.Button btnAgregarInsumo_389ME;
        private System.Windows.Forms.Label lblInsumosSolicitud_389ME;
        private System.Windows.Forms.DataGridView dgvSolicitud_389ME;
        private System.Windows.Forms.Button btnQuitarInsumo_389ME;
        private System.Windows.Forms.Label lblProveedores_389ME;
        private System.Windows.Forms.CheckedListBox clbProveedores_389ME;
        private System.Windows.Forms.Button btnConfirmarSolicitud_389ME;
    }
}
