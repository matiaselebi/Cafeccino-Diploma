namespace CAFECCINO_INGENIERIA_DE_SOFTWARE
{
    partial class frmCambiarIdioma
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCambiarIdioma));
            this.lblIdioma_389ME = new System.Windows.Forms.Label();
            this.cmbIdiomas_389ME = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // lblIdioma_389ME
            // 
            this.lblIdioma_389ME.AutoSize = true;
            this.lblIdioma_389ME.Location = new System.Drawing.Point(20, 30);
            this.lblIdioma_389ME.Name = "lblIdioma_389ME";
            this.lblIdioma_389ME.Size = new System.Drawing.Size(93, 13);
            this.lblIdioma_389ME.TabIndex = 0;
            this.lblIdioma_389ME.Text = "Idioma del sistema";
            // 
            // cmbIdiomas_389ME
            // 
            this.cmbIdiomas_389ME.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbIdiomas_389ME.FormattingEnabled = true;
            this.cmbIdiomas_389ME.Location = new System.Drawing.Point(130, 27);
            this.cmbIdiomas_389ME.Name = "cmbIdiomas_389ME";
            this.cmbIdiomas_389ME.Size = new System.Drawing.Size(160, 21);
            this.cmbIdiomas_389ME.TabIndex = 1;
            this.cmbIdiomas_389ME.SelectedIndexChanged += new System.EventHandler(this.cmbIdiomas_SelectedIndexChanged_389ME);
            // 
            // frmCambiarIdioma
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Chocolate;
            this.ClientSize = new System.Drawing.Size(324, 82);
            this.Controls.Add(this.lblIdioma_389ME);
            this.Controls.Add(this.cmbIdiomas_389ME);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmCambiarIdioma";
            this.Text = "Cambiar idioma";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmCambiarIdioma_FormClosing_389ME);
            this.Load += new System.EventHandler(this.frmCambiarIdioma_Load_389ME);
            this.VisibleChanged += new System.EventHandler(this.frmCambiarIdioma_VisibleChanged_389ME);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Label lblIdioma_389ME;
        private System.Windows.Forms.ComboBox cmbIdiomas_389ME;
    }
}
