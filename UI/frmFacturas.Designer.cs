namespace CAFECCINO_INGENIERIA_DE_SOFTWARE
{
    partial class frmFacturas
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmFacturas));
            this.lblFacturas_389ME = new System.Windows.Forms.Label();
            this.dgvFacturas_389ME = new System.Windows.Forms.DataGridView();
            this.lblDetalle_389ME = new System.Windows.Forms.Label();
            this.dgvDetalle_389ME = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFacturas_389ME)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalle_389ME)).BeginInit();
            this.SuspendLayout();
            // 
            // lblFacturas_389ME
            // 
            this.lblFacturas_389ME.AutoSize = true;
            this.lblFacturas_389ME.Location = new System.Drawing.Point(10, 10);
            this.lblFacturas_389ME.Name = "lblFacturas_389ME";
            this.lblFacturas_389ME.Size = new System.Drawing.Size(48, 13);
            this.lblFacturas_389ME.TabIndex = 0;
            this.lblFacturas_389ME.Text = "Facturas";
            // 
            // dgvFacturas_389ME
            // 
            this.dgvFacturas_389ME.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvFacturas_389ME.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFacturas_389ME.Location = new System.Drawing.Point(10, 30);
            this.dgvFacturas_389ME.Name = "dgvFacturas_389ME";
            this.dgvFacturas_389ME.ReadOnly = true;
            this.dgvFacturas_389ME.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvFacturas_389ME.Size = new System.Drawing.Size(760, 220);
            this.dgvFacturas_389ME.TabIndex = 1;
            this.dgvFacturas_389ME.SelectionChanged += new System.EventHandler(this.dgvFacturas_SelectionChanged_389ME);
            // 
            // lblDetalle_389ME
            // 
            this.lblDetalle_389ME.AutoSize = true;
            this.lblDetalle_389ME.Location = new System.Drawing.Point(10, 260);
            this.lblDetalle_389ME.Name = "lblDetalle_389ME";
            this.lblDetalle_389ME.Size = new System.Drawing.Size(183, 13);
            this.lblDetalle_389ME.TabIndex = 2;
            this.lblDetalle_389ME.Text = "Productos de la factura seleccionada";
            // 
            // dgvDetalle_389ME
            // 
            this.dgvDetalle_389ME.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDetalle_389ME.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDetalle_389ME.Location = new System.Drawing.Point(10, 280);
            this.dgvDetalle_389ME.Name = "dgvDetalle_389ME";
            this.dgvDetalle_389ME.ReadOnly = true;
            this.dgvDetalle_389ME.Size = new System.Drawing.Size(760, 180);
            this.dgvDetalle_389ME.TabIndex = 3;
            // 
            // frmFacturas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Chocolate;
            this.ClientSize = new System.Drawing.Size(782, 475);
            this.Controls.Add(this.lblFacturas_389ME);
            this.Controls.Add(this.dgvFacturas_389ME);
            this.Controls.Add(this.lblDetalle_389ME);
            this.Controls.Add(this.dgvDetalle_389ME);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmFacturas";
            this.Text = "Facturas";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmFacturas_FormClosing_389ME);
            this.Load += new System.EventHandler(this.frmFacturas_Load_389ME);
            this.VisibleChanged += new System.EventHandler(this.frmFacturas_VisibleChanged_389ME);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFacturas_389ME)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalle_389ME)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Label lblFacturas_389ME;
        private System.Windows.Forms.DataGridView dgvFacturas_389ME;
        private System.Windows.Forms.Label lblDetalle_389ME;
        private System.Windows.Forms.DataGridView dgvDetalle_389ME;
    }
}
