namespace CAFECCINO_INGENIERIA_DE_SOFTWARE
{
    partial class frmProveedores
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmProveedores));
            this.lblProveedores_389ME = new System.Windows.Forms.Label();
            this.dgvProveedores_389ME = new System.Windows.Forms.DataGridView();
            this.lblCUIT_389ME = new System.Windows.Forms.Label();
            this.txtCUIT_389ME = new System.Windows.Forms.TextBox();
            this.lblNombre_389ME = new System.Windows.Forms.Label();
            this.txtNombre_389ME = new System.Windows.Forms.TextBox();
            this.lblRazonSocial_389ME = new System.Windows.Forms.Label();
            this.txtRazonSocial_389ME = new System.Windows.Forms.TextBox();
            this.lblTelefono_389ME = new System.Windows.Forms.Label();
            this.txtTelefono_389ME = new System.Windows.Forms.TextBox();
            this.lblEmail_389ME = new System.Windows.Forms.Label();
            this.txtEmail_389ME = new System.Windows.Forms.TextBox();
            this.lblDireccion_389ME = new System.Windows.Forms.Label();
            this.txtDireccion_389ME = new System.Windows.Forms.TextBox();
            this.lblCuentaBancaria_389ME = new System.Windows.Forms.Label();
            this.txtCuentaBancaria_389ME = new System.Windows.Forms.TextBox();
            this.btnPreRegistrar_389ME = new System.Windows.Forms.Button();
            this.btnModificar_389ME = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProveedores_389ME)).BeginInit();
            this.SuspendLayout();
            // 
            // lblProveedores_389ME
            // 
            this.lblProveedores_389ME.AutoSize = true;
            this.lblProveedores_389ME.Location = new System.Drawing.Point(10, 10);
            this.lblProveedores_389ME.Name = "lblProveedores_389ME";
            this.lblProveedores_389ME.Size = new System.Drawing.Size(67, 13);
            this.lblProveedores_389ME.TabIndex = 0;
            this.lblProveedores_389ME.Text = "Gestion de proveedores";
            // 
            // dgvProveedores_389ME
            // 
            this.dgvProveedores_389ME.AllowUserToAddRows = false;
            this.dgvProveedores_389ME.AllowUserToDeleteRows = false;
            this.dgvProveedores_389ME.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvProveedores_389ME.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProveedores_389ME.Location = new System.Drawing.Point(10, 30);
            this.dgvProveedores_389ME.MultiSelect = false;
            this.dgvProveedores_389ME.Name = "dgvProveedores_389ME";
            this.dgvProveedores_389ME.ReadOnly = true;
            this.dgvProveedores_389ME.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvProveedores_389ME.Size = new System.Drawing.Size(760, 200);
            this.dgvProveedores_389ME.TabIndex = 1;
            this.dgvProveedores_389ME.SelectionChanged += new System.EventHandler(this.dgvProveedores_SelectionChanged_389ME);
            // 
            // lblCUIT_389ME
            // 
            this.lblCUIT_389ME.AutoSize = true;
            this.lblCUIT_389ME.Location = new System.Drawing.Point(10, 250);
            this.lblCUIT_389ME.Name = "lblCUIT_389ME";
            this.lblCUIT_389ME.Size = new System.Drawing.Size(32, 13);
            this.lblCUIT_389ME.TabIndex = 2;
            this.lblCUIT_389ME.Text = "CUIT";
            // 
            // txtCUIT_389ME
            // 
            this.txtCUIT_389ME.Location = new System.Drawing.Point(120, 247);
            this.txtCUIT_389ME.MaxLength = 11;
            this.txtCUIT_389ME.Name = "txtCUIT_389ME";
            this.txtCUIT_389ME.Size = new System.Drawing.Size(200, 20);
            this.txtCUIT_389ME.TabIndex = 3;
            // 
            // lblNombre_389ME
            // 
            this.lblNombre_389ME.AutoSize = true;
            this.lblNombre_389ME.Location = new System.Drawing.Point(10, 280);
            this.lblNombre_389ME.Name = "lblNombre_389ME";
            this.lblNombre_389ME.Size = new System.Drawing.Size(44, 13);
            this.lblNombre_389ME.TabIndex = 4;
            this.lblNombre_389ME.Text = "Nombre";
            // 
            // txtNombre_389ME
            // 
            this.txtNombre_389ME.Location = new System.Drawing.Point(120, 277);
            this.txtNombre_389ME.MaxLength = 50;
            this.txtNombre_389ME.Name = "txtNombre_389ME";
            this.txtNombre_389ME.Size = new System.Drawing.Size(200, 20);
            this.txtNombre_389ME.TabIndex = 5;
            // 
            // lblRazonSocial_389ME
            // 
            this.lblRazonSocial_389ME.AutoSize = true;
            this.lblRazonSocial_389ME.Location = new System.Drawing.Point(10, 310);
            this.lblRazonSocial_389ME.Name = "lblRazonSocial_389ME";
            this.lblRazonSocial_389ME.Size = new System.Drawing.Size(68, 13);
            this.lblRazonSocial_389ME.TabIndex = 6;
            this.lblRazonSocial_389ME.Text = "Razon social";
            // 
            // txtRazonSocial_389ME
            // 
            this.txtRazonSocial_389ME.Location = new System.Drawing.Point(120, 307);
            this.txtRazonSocial_389ME.MaxLength = 100;
            this.txtRazonSocial_389ME.Name = "txtRazonSocial_389ME";
            this.txtRazonSocial_389ME.Size = new System.Drawing.Size(200, 20);
            this.txtRazonSocial_389ME.TabIndex = 7;
            // 
            // lblTelefono_389ME
            // 
            this.lblTelefono_389ME.AutoSize = true;
            this.lblTelefono_389ME.Location = new System.Drawing.Point(400, 250);
            this.lblTelefono_389ME.Name = "lblTelefono_389ME";
            this.lblTelefono_389ME.Size = new System.Drawing.Size(49, 13);
            this.lblTelefono_389ME.TabIndex = 8;
            this.lblTelefono_389ME.Text = "Telefono";
            // 
            // txtTelefono_389ME
            // 
            this.txtTelefono_389ME.Location = new System.Drawing.Point(520, 247);
            this.txtTelefono_389ME.MaxLength = 15;
            this.txtTelefono_389ME.Name = "txtTelefono_389ME";
            this.txtTelefono_389ME.Size = new System.Drawing.Size(200, 20);
            this.txtTelefono_389ME.TabIndex = 9;
            // 
            // lblEmail_389ME
            // 
            this.lblEmail_389ME.AutoSize = true;
            this.lblEmail_389ME.Location = new System.Drawing.Point(400, 280);
            this.lblEmail_389ME.Name = "lblEmail_389ME";
            this.lblEmail_389ME.Size = new System.Drawing.Size(32, 13);
            this.lblEmail_389ME.TabIndex = 10;
            this.lblEmail_389ME.Text = "Email";
            // 
            // txtEmail_389ME
            // 
            this.txtEmail_389ME.Location = new System.Drawing.Point(520, 277);
            this.txtEmail_389ME.MaxLength = 100;
            this.txtEmail_389ME.Name = "txtEmail_389ME";
            this.lblDireccion_389ME.Location = new System.Drawing.Point(400, 310);
            this.lblDireccion_389ME.Name = "lblDireccion_389ME";
            this.lblDireccion_389ME.AutoSize = true;
            this.lblDireccion_389ME.Text = "Direccion";
            this.txtDireccion_389ME.Location = new System.Drawing.Point(520, 307);
            this.txtDireccion_389ME.Name = "txtDireccion_389ME";
            this.txtDireccion_389ME.Size = new System.Drawing.Size(200, 20);
            this.txtDireccion_389ME.MaxLength = 100;
            this.lblCuentaBancaria_389ME.Location = new System.Drawing.Point(400, 340);
            this.lblCuentaBancaria_389ME.Name = "lblCuentaBancaria_389ME";
            this.lblCuentaBancaria_389ME.AutoSize = true;
            this.lblCuentaBancaria_389ME.Text = "Cuenta bancaria (CBU)";
            this.txtCuentaBancaria_389ME.Location = new System.Drawing.Point(520, 337);
            this.txtCuentaBancaria_389ME.Name = "txtCuentaBancaria_389ME";
            this.txtCuentaBancaria_389ME.Size = new System.Drawing.Size(200, 20);
            this.txtCuentaBancaria_389ME.MaxLength = 22;
            this.txtEmail_389ME.Size = new System.Drawing.Size(200, 20);
            this.txtEmail_389ME.TabIndex = 11;
            // 
            // btnPreRegistrar_389ME
            // 
            this.btnPreRegistrar_389ME.Location = new System.Drawing.Point(120, 380);
            this.btnPreRegistrar_389ME.Name = "btnPreRegistrar_389ME";
            this.btnPreRegistrar_389ME.Size = new System.Drawing.Size(130, 30);
            this.btnPreRegistrar_389ME.TabIndex = 12;
            this.btnPreRegistrar_389ME.Text = "Pre-registrar";
            this.btnPreRegistrar_389ME.UseVisualStyleBackColor = true;
            this.btnPreRegistrar_389ME.Click += new System.EventHandler(this.btnPreRegistrar_Click_389ME);
            // 
            // btnModificar_389ME
            // 
            this.btnModificar_389ME.Location = new System.Drawing.Point(270, 380);
            this.btnModificar_389ME.Name = "btnModificar_389ME";
            this.btnModificar_389ME.Size = new System.Drawing.Size(130, 30);
            this.btnModificar_389ME.TabIndex = 13;
            this.btnModificar_389ME.Text = "Modificar";
            this.btnModificar_389ME.UseVisualStyleBackColor = true;
            this.btnModificar_389ME.Click += new System.EventHandler(this.btnModificar_Click_389ME);
            // 
            // frmProveedores
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Chocolate;
            this.ClientSize = new System.Drawing.Size(782, 430);
            this.Controls.Add(this.lblProveedores_389ME);
            this.Controls.Add(this.dgvProveedores_389ME);
            this.Controls.Add(this.lblCUIT_389ME);
            this.Controls.Add(this.txtCUIT_389ME);
            this.Controls.Add(this.lblNombre_389ME);
            this.Controls.Add(this.txtNombre_389ME);
            this.Controls.Add(this.lblRazonSocial_389ME);
            this.Controls.Add(this.txtRazonSocial_389ME);
            this.Controls.Add(this.lblTelefono_389ME);
            this.Controls.Add(this.txtTelefono_389ME);
            this.Controls.Add(this.lblEmail_389ME);
            this.Controls.Add(this.txtEmail_389ME);
            this.Controls.Add(this.lblDireccion_389ME);
            this.Controls.Add(this.txtDireccion_389ME);
            this.Controls.Add(this.lblCuentaBancaria_389ME);
            this.Controls.Add(this.txtCuentaBancaria_389ME);
            this.Controls.Add(this.btnPreRegistrar_389ME);
            this.Controls.Add(this.btnModificar_389ME);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmProveedores";
            this.Text = "Gestion de proveedores";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmProveedores_FormClosing_389ME);
            this.VisibleChanged += new System.EventHandler(this.frmProveedores_VisibleChanged_389ME);
            ((System.ComponentModel.ISupportInitialize)(this.dgvProveedores_389ME)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Label lblProveedores_389ME;
        private System.Windows.Forms.DataGridView dgvProveedores_389ME;
        private System.Windows.Forms.Label lblCUIT_389ME;
        private System.Windows.Forms.TextBox txtCUIT_389ME;
        private System.Windows.Forms.Label lblNombre_389ME;
        private System.Windows.Forms.TextBox txtNombre_389ME;
        private System.Windows.Forms.Label lblRazonSocial_389ME;
        private System.Windows.Forms.TextBox txtRazonSocial_389ME;
        private System.Windows.Forms.Label lblTelefono_389ME;
        private System.Windows.Forms.TextBox txtTelefono_389ME;
        private System.Windows.Forms.Label lblEmail_389ME;
        private System.Windows.Forms.TextBox txtEmail_389ME;
        private System.Windows.Forms.Label lblDireccion_389ME;
        private System.Windows.Forms.TextBox txtDireccion_389ME;
        private System.Windows.Forms.Label lblCuentaBancaria_389ME;
        private System.Windows.Forms.TextBox txtCuentaBancaria_389ME;
        private System.Windows.Forms.Button btnPreRegistrar_389ME;
        private System.Windows.Forms.Button btnModificar_389ME;
    }
}
