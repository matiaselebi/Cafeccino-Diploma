namespace CAFECCINO_INGENIERIA_DE_SOFTWARE
{
    partial class frmRecepcionInsumos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRecepcionInsumos));
            this.lblOrden_389ME = new System.Windows.Forms.Label();
            this.cmbOrden_389ME = new System.Windows.Forms.ComboBox();
            this.lblDetalle_389ME = new System.Windows.Forms.Label();
            this.dgvDetalle_389ME = new System.Windows.Forms.DataGridView();
            this.lblCantidadRecibida_389ME = new System.Windows.Forms.Label();
            this.txtCantidadRecibida_389ME = new System.Windows.Forms.TextBox();
            this.btnActualizarCantidad_389ME = new System.Windows.Forms.Button();
            this.btnConfirmarRecepcion_389ME = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalle_389ME)).BeginInit();
            this.SuspendLayout();
            // 
            // lblOrden_389ME
            // 
            this.lblOrden_389ME.AutoSize = true;
            this.lblOrden_389ME.Location = new System.Drawing.Point(10, 15);
            this.lblOrden_389ME.Name = "lblOrden_389ME";
            this.lblOrden_389ME.Size = new System.Drawing.Size(89, 13);
            this.lblOrden_389ME.TabIndex = 0;
            this.lblOrden_389ME.Text = "Orden de compra";
            // 
            // cmbOrden_389ME
            // 
            this.cmbOrden_389ME.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOrden_389ME.FormattingEnabled = true;
            this.cmbOrden_389ME.Location = new System.Drawing.Point(120, 12);
            this.cmbOrden_389ME.Name = "cmbOrden_389ME";
            this.cmbOrden_389ME.Size = new System.Drawing.Size(350, 21);
            this.cmbOrden_389ME.TabIndex = 1;
            this.cmbOrden_389ME.SelectedIndexChanged += new System.EventHandler(this.cmbOrden_SelectedIndexChanged_389ME);
            // 
            // lblDetalle_389ME
            // 
            this.lblDetalle_389ME.AutoSize = true;
            this.lblDetalle_389ME.Location = new System.Drawing.Point(10, 45);
            this.lblDetalle_389ME.Name = "lblDetalle_389ME";
            this.lblDetalle_389ME.Size = new System.Drawing.Size(102, 13);
            this.lblDetalle_389ME.TabIndex = 2;
            this.lblDetalle_389ME.Text = "Insumos de la orden";
            // 
            // dgvDetalle_389ME
            // 
            this.dgvDetalle_389ME.AllowUserToAddRows = false;
            this.dgvDetalle_389ME.AllowUserToDeleteRows = false;
            this.dgvDetalle_389ME.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDetalle_389ME.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDetalle_389ME.Location = new System.Drawing.Point(10, 65);
            this.dgvDetalle_389ME.MultiSelect = false;
            this.dgvDetalle_389ME.Name = "dgvDetalle_389ME";
            this.dgvDetalle_389ME.ReadOnly = true;
            this.dgvDetalle_389ME.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDetalle_389ME.Size = new System.Drawing.Size(670, 220);
            this.dgvDetalle_389ME.TabIndex = 3;
            this.dgvDetalle_389ME.SelectionChanged += new System.EventHandler(this.dgvDetalle_SelectionChanged_389ME);
            // 
            // lblCantidadRecibida_389ME
            // 
            this.lblCantidadRecibida_389ME.AutoSize = true;
            this.lblCantidadRecibida_389ME.Location = new System.Drawing.Point(10, 305);
            this.lblCantidadRecibida_389ME.Name = "lblCantidadRecibida_389ME";
            this.lblCantidadRecibida_389ME.Size = new System.Drawing.Size(89, 13);
            this.lblCantidadRecibida_389ME.TabIndex = 4;
            this.lblCantidadRecibida_389ME.Text = "Cantidad recibida";
            // 
            // txtCantidadRecibida_389ME
            // 
            this.txtCantidadRecibida_389ME.Location = new System.Drawing.Point(130, 302);
            this.txtCantidadRecibida_389ME.MaxLength = 10;
            this.txtCantidadRecibida_389ME.Name = "txtCantidadRecibida_389ME";
            this.txtCantidadRecibida_389ME.Size = new System.Drawing.Size(100, 20);
            this.txtCantidadRecibida_389ME.TabIndex = 5;
            // 
            // btnActualizarCantidad_389ME
            // 
            this.btnActualizarCantidad_389ME.Location = new System.Drawing.Point(245, 298);
            this.btnActualizarCantidad_389ME.Name = "btnActualizarCantidad_389ME";
            this.btnActualizarCantidad_389ME.Size = new System.Drawing.Size(150, 30);
            this.btnActualizarCantidad_389ME.TabIndex = 6;
            this.btnActualizarCantidad_389ME.Text = "Actualizar cantidad";
            this.btnActualizarCantidad_389ME.UseVisualStyleBackColor = true;
            this.btnActualizarCantidad_389ME.Click += new System.EventHandler(this.btnActualizarCantidad_Click_389ME);
            // 
            // btnConfirmarRecepcion_389ME
            // 
            this.btnConfirmarRecepcion_389ME.Location = new System.Drawing.Point(430, 298);
            this.btnConfirmarRecepcion_389ME.Name = "btnConfirmarRecepcion_389ME";
            this.btnConfirmarRecepcion_389ME.Size = new System.Drawing.Size(250, 30);
            this.btnConfirmarRecepcion_389ME.TabIndex = 7;
            this.btnConfirmarRecepcion_389ME.Text = "Confirmar recepcion";
            this.btnConfirmarRecepcion_389ME.UseVisualStyleBackColor = true;
            this.btnConfirmarRecepcion_389ME.Click += new System.EventHandler(this.btnConfirmarRecepcion_Click_389ME);
            // 
            // frmRecepcionInsumos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Chocolate;
            this.ClientSize = new System.Drawing.Size(692, 345);
            this.Controls.Add(this.lblOrden_389ME);
            this.Controls.Add(this.cmbOrden_389ME);
            this.Controls.Add(this.lblDetalle_389ME);
            this.Controls.Add(this.dgvDetalle_389ME);
            this.Controls.Add(this.lblCantidadRecibida_389ME);
            this.Controls.Add(this.txtCantidadRecibida_389ME);
            this.Controls.Add(this.btnActualizarCantidad_389ME);
            this.Controls.Add(this.btnConfirmarRecepcion_389ME);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmRecepcionInsumos";
            this.Text = "Recepcion de insumos";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmRecepcionInsumos_FormClosing_389ME);
            this.VisibleChanged += new System.EventHandler(this.frmRecepcionInsumos_VisibleChanged_389ME);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalle_389ME)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Label lblOrden_389ME;
        private System.Windows.Forms.ComboBox cmbOrden_389ME;
        private System.Windows.Forms.Label lblDetalle_389ME;
        private System.Windows.Forms.DataGridView dgvDetalle_389ME;
        private System.Windows.Forms.Label lblCantidadRecibida_389ME;
        private System.Windows.Forms.TextBox txtCantidadRecibida_389ME;
        private System.Windows.Forms.Button btnActualizarCantidad_389ME;
        private System.Windows.Forms.Button btnConfirmarRecepcion_389ME;
    }
}
