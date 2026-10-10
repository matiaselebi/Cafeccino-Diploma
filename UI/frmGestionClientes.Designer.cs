namespace CAFECCINO_INGENIERIA_DE_SOFTWARE
{
    partial class frmGestionClientes
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmGestionClientes));
            this.lblClientes_389ME = new System.Windows.Forms.Label();
            this.dgvClientes_389ME = new System.Windows.Forms.DataGridView();
            this.lblDNI_389ME = new System.Windows.Forms.Label();
            this.txtDNI_389ME = new System.Windows.Forms.TextBox();
            this.lblNombre_389ME = new System.Windows.Forms.Label();
            this.txtNombre_389ME = new System.Windows.Forms.TextBox();
            this.lblApellido_389ME = new System.Windows.Forms.Label();
            this.txtApellido_389ME = new System.Windows.Forms.TextBox();
            this.lblDireccion_389ME = new System.Windows.Forms.Label();
            this.txtDireccion_389ME = new System.Windows.Forms.TextBox();
            this.lblMail_389ME = new System.Windows.Forms.Label();
            this.txtMail_389ME = new System.Windows.Forms.TextBox();
            this.lblTelefono_389ME = new System.Windows.Forms.Label();
            this.txtTelefono_389ME = new System.Windows.Forms.TextBox();
            this.btnAgregar_389ME = new System.Windows.Forms.Button();
            this.btnModificar_389ME = new System.Windows.Forms.Button();
            this.btnBloquear_389ME = new System.Windows.Forms.Button();
            this.btnDesbloquear_389ME = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvClientes_389ME)).BeginInit();
            this.SuspendLayout();
            // 
            // lblClientes_389ME
            // 
            this.lblClientes_389ME.AutoSize = true;
            this.lblClientes_389ME.Location = new System.Drawing.Point(10, 10);
            this.lblClientes_389ME.Name = "lblClientes_389ME";
            this.lblClientes_389ME.Size = new System.Drawing.Size(98, 13);
            this.lblClientes_389ME.TabIndex = 0;
            this.lblClientes_389ME.Text = "Clientes registrados";
            // 
            // dgvClientes_389ME
            // 
            this.dgvClientes_389ME.AllowUserToAddRows = false;
            this.dgvClientes_389ME.AllowUserToDeleteRows = false;
            this.dgvClientes_389ME.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvClientes_389ME.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvClientes_389ME.Location = new System.Drawing.Point(10, 30);
            this.dgvClientes_389ME.Name = "dgvClientes_389ME";
            this.dgvClientes_389ME.ReadOnly = true;
            this.dgvClientes_389ME.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvClientes_389ME.Size = new System.Drawing.Size(760, 230);
            this.dgvClientes_389ME.TabIndex = 1;
            this.dgvClientes_389ME.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvClientes_CellClick_389ME);
            // 
            // lblDNI_389ME
            // 
            this.lblDNI_389ME.AutoSize = true;
            this.lblDNI_389ME.Location = new System.Drawing.Point(10, 280);
            this.lblDNI_389ME.Name = "lblDNI_389ME";
            this.lblDNI_389ME.Size = new System.Drawing.Size(26, 13);
            this.lblDNI_389ME.TabIndex = 2;
            this.lblDNI_389ME.Text = "DNI";
            // 
            // txtDNI_389ME
            // 
            this.txtDNI_389ME.Location = new System.Drawing.Point(90, 277);
            this.txtDNI_389ME.MaxLength = 8;
            this.txtDNI_389ME.Name = "txtDNI_389ME";
            this.txtDNI_389ME.Size = new System.Drawing.Size(200, 20);
            this.txtDNI_389ME.TabIndex = 3;
            // 
            // lblNombre_389ME
            // 
            this.lblNombre_389ME.AutoSize = true;
            this.lblNombre_389ME.Location = new System.Drawing.Point(10, 310);
            this.lblNombre_389ME.Name = "lblNombre_389ME";
            this.lblNombre_389ME.Size = new System.Drawing.Size(44, 13);
            this.lblNombre_389ME.TabIndex = 4;
            this.lblNombre_389ME.Text = "Nombre";
            // 
            // txtNombre_389ME
            // 
            this.txtNombre_389ME.Location = new System.Drawing.Point(90, 307);
            this.txtNombre_389ME.MaxLength = 50;
            this.txtNombre_389ME.Name = "txtNombre_389ME";
            this.txtNombre_389ME.Size = new System.Drawing.Size(200, 20);
            this.txtNombre_389ME.TabIndex = 5;
            // 
            // lblApellido_389ME
            // 
            this.lblApellido_389ME.AutoSize = true;
            this.lblApellido_389ME.Location = new System.Drawing.Point(10, 340);
            this.lblApellido_389ME.Name = "lblApellido_389ME";
            this.lblApellido_389ME.Size = new System.Drawing.Size(44, 13);
            this.lblApellido_389ME.TabIndex = 6;
            this.lblApellido_389ME.Text = "Apellido";
            // 
            // txtApellido_389ME
            // 
            this.txtApellido_389ME.Location = new System.Drawing.Point(90, 337);
            this.txtApellido_389ME.MaxLength = 50;
            this.txtApellido_389ME.Name = "txtApellido_389ME";
            this.txtApellido_389ME.Size = new System.Drawing.Size(200, 20);
            this.txtApellido_389ME.TabIndex = 7;
            // 
            // lblDireccion_389ME
            // 
            this.lblDireccion_389ME.AutoSize = true;
            this.lblDireccion_389ME.Location = new System.Drawing.Point(400, 280);
            this.lblDireccion_389ME.Name = "lblDireccion_389ME";
            this.lblDireccion_389ME.Size = new System.Drawing.Size(52, 13);
            this.lblDireccion_389ME.TabIndex = 8;
            this.lblDireccion_389ME.Text = "Direccion";
            // 
            // txtDireccion_389ME
            // 
            this.txtDireccion_389ME.Location = new System.Drawing.Point(480, 277);
            this.txtDireccion_389ME.MaxLength = 100;
            this.txtDireccion_389ME.Name = "txtDireccion_389ME";
            this.txtDireccion_389ME.Size = new System.Drawing.Size(200, 20);
            this.txtDireccion_389ME.TabIndex = 9;
            // 
            // lblMail_389ME
            // 
            this.lblMail_389ME.AutoSize = true;
            this.lblMail_389ME.Location = new System.Drawing.Point(400, 310);
            this.lblMail_389ME.Name = "lblMail_389ME";
            this.lblMail_389ME.Size = new System.Drawing.Size(26, 13);
            this.lblMail_389ME.TabIndex = 10;
            this.lblMail_389ME.Text = "Mail";
            // 
            // txtMail_389ME
            // 
            this.txtMail_389ME.Location = new System.Drawing.Point(480, 307);
            this.txtMail_389ME.MaxLength = 100;
            this.txtMail_389ME.Name = "txtMail_389ME";
            this.txtMail_389ME.Size = new System.Drawing.Size(200, 20);
            this.txtMail_389ME.TabIndex = 11;
            // 
            // lblTelefono_389ME
            // 
            this.lblTelefono_389ME.AutoSize = true;
            this.lblTelefono_389ME.Location = new System.Drawing.Point(400, 340);
            this.lblTelefono_389ME.Name = "lblTelefono_389ME";
            this.lblTelefono_389ME.Size = new System.Drawing.Size(49, 13);
            this.lblTelefono_389ME.TabIndex = 12;
            this.lblTelefono_389ME.Text = "Telefono";
            // 
            // txtTelefono_389ME
            // 
            this.txtTelefono_389ME.Location = new System.Drawing.Point(480, 337);
            this.txtTelefono_389ME.MaxLength = 10;
            this.txtTelefono_389ME.Name = "txtTelefono_389ME";
            this.txtTelefono_389ME.Size = new System.Drawing.Size(200, 20);
            this.txtTelefono_389ME.TabIndex = 13;
            // 
            // btnAgregar_389ME
            // 
            this.btnAgregar_389ME.Location = new System.Drawing.Point(90, 380);
            this.btnAgregar_389ME.Name = "btnAgregar_389ME";
            this.btnAgregar_389ME.Size = new System.Drawing.Size(120, 30);
            this.btnAgregar_389ME.TabIndex = 14;
            this.btnAgregar_389ME.Text = "Agregar";
            this.btnAgregar_389ME.UseVisualStyleBackColor = true;
            this.btnAgregar_389ME.Click += new System.EventHandler(this.btnAgregar_Click_389ME);
            // 
            // btnModificar_389ME
            // 
            this.btnModificar_389ME.Location = new System.Drawing.Point(230, 380);
            this.btnModificar_389ME.Name = "btnModificar_389ME";
            this.btnModificar_389ME.Size = new System.Drawing.Size(120, 30);
            this.btnModificar_389ME.TabIndex = 15;
            this.btnModificar_389ME.Text = "Modificar";
            this.btnModificar_389ME.UseVisualStyleBackColor = true;
            this.btnModificar_389ME.Click += new System.EventHandler(this.btnModificar_Click_389ME);
            // 
            // btnBloquear_389ME
            // 
            this.btnBloquear_389ME.Location = new System.Drawing.Point(400, 380);
            this.btnBloquear_389ME.Name = "btnBloquear_389ME";
            this.btnBloquear_389ME.Size = new System.Drawing.Size(120, 30);
            this.btnBloquear_389ME.TabIndex = 16;
            this.btnBloquear_389ME.Text = "Bloquear";
            this.btnBloquear_389ME.UseVisualStyleBackColor = true;
            this.btnBloquear_389ME.Click += new System.EventHandler(this.btnBloquear_Click_389ME);
            // 
            // btnDesbloquear_389ME
            // 
            this.btnDesbloquear_389ME.Location = new System.Drawing.Point(540, 380);
            this.btnDesbloquear_389ME.Name = "btnDesbloquear_389ME";
            this.btnDesbloquear_389ME.Size = new System.Drawing.Size(120, 30);
            this.btnDesbloquear_389ME.TabIndex = 17;
            this.btnDesbloquear_389ME.Text = "Desbloquear";
            this.btnDesbloquear_389ME.UseVisualStyleBackColor = true;
            this.btnDesbloquear_389ME.Click += new System.EventHandler(this.btnDesbloquear_Click_389ME);
            // 
            // frmGestionClientes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Chocolate;
            this.ClientSize = new System.Drawing.Size(782, 430);
            this.Controls.Add(this.lblClientes_389ME);
            this.Controls.Add(this.dgvClientes_389ME);
            this.Controls.Add(this.lblDNI_389ME);
            this.Controls.Add(this.txtDNI_389ME);
            this.Controls.Add(this.lblNombre_389ME);
            this.Controls.Add(this.txtNombre_389ME);
            this.Controls.Add(this.lblApellido_389ME);
            this.Controls.Add(this.txtApellido_389ME);
            this.Controls.Add(this.lblDireccion_389ME);
            this.Controls.Add(this.txtDireccion_389ME);
            this.Controls.Add(this.lblMail_389ME);
            this.Controls.Add(this.txtMail_389ME);
            this.Controls.Add(this.lblTelefono_389ME);
            this.Controls.Add(this.txtTelefono_389ME);
            this.Controls.Add(this.btnAgregar_389ME);
            this.Controls.Add(this.btnModificar_389ME);
            this.Controls.Add(this.btnBloquear_389ME);
            this.Controls.Add(this.btnDesbloquear_389ME);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmGestionClientes";
            this.Text = "Gestion de clientes";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmGestionClientes_FormClosing_389ME);
            this.Load += new System.EventHandler(this.frmGestionClientes_Load_389ME);
            this.VisibleChanged += new System.EventHandler(this.frmGestionClientes_VisibleChanged_389ME);
            ((System.ComponentModel.ISupportInitialize)(this.dgvClientes_389ME)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Label lblClientes_389ME;
        private System.Windows.Forms.DataGridView dgvClientes_389ME;
        private System.Windows.Forms.Label lblDNI_389ME;
        private System.Windows.Forms.TextBox txtDNI_389ME;
        private System.Windows.Forms.Label lblNombre_389ME;
        private System.Windows.Forms.TextBox txtNombre_389ME;
        private System.Windows.Forms.Label lblApellido_389ME;
        private System.Windows.Forms.TextBox txtApellido_389ME;
        private System.Windows.Forms.Label lblDireccion_389ME;
        private System.Windows.Forms.TextBox txtDireccion_389ME;
        private System.Windows.Forms.Label lblMail_389ME;
        private System.Windows.Forms.TextBox txtMail_389ME;
        private System.Windows.Forms.Label lblTelefono_389ME;
        private System.Windows.Forms.TextBox txtTelefono_389ME;
        private System.Windows.Forms.Button btnAgregar_389ME;
        private System.Windows.Forms.Button btnModificar_389ME;
        private System.Windows.Forms.Button btnBloquear_389ME;
        private System.Windows.Forms.Button btnDesbloquear_389ME;
    }
}
