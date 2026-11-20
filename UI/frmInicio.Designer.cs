namespace CAFECCINO_INGENIERIA_DE_SOFTWARE
{
    partial class frmInicio
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmInicio));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.usuarioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.iniciarSesionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cerrarSesionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cambiarIdiomaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cambiarContraseñaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.administradorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gestionDeUsuariosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gestionDeClientesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gestionDePerfilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gestionDeIdiomasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bitacoraDeEventosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bitacoraDeCambiosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.verificacionIntegridadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ventasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gestionDeProductosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nuevaVentaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.verFacturasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.comprasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.insumosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.recetasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.proveedoresToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.solicitudesCotizacionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cotizacionesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ordenesCompraToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.recepcionInsumosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.usuarioToolStripMenuItem,
            this.administradorToolStripMenuItem,
            this.ventasToolStripMenuItem,
            this.comprasToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
            // 
            // usuarioToolStripMenuItem
            // 
            this.usuarioToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.iniciarSesionToolStripMenuItem,
            this.cerrarSesionToolStripMenuItem,
            this.cambiarIdiomaToolStripMenuItem,
            this.cambiarContraseñaToolStripMenuItem});
            this.usuarioToolStripMenuItem.Name = "usuarioToolStripMenuItem";
            this.usuarioToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.usuarioToolStripMenuItem.Text = "Usuario";
            // 
            // iniciarSesionToolStripMenuItem
            // 
            this.iniciarSesionToolStripMenuItem.Name = "iniciarSesionToolStripMenuItem";
            this.iniciarSesionToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.iniciarSesionToolStripMenuItem.Text = "Iniciar Sesion";
            this.iniciarSesionToolStripMenuItem.Click += new System.EventHandler(this.iniciarSesionToolStripMenuItem_Click);
            // 
            // cerrarSesionToolStripMenuItem
            // 
            this.cerrarSesionToolStripMenuItem.Name = "cerrarSesionToolStripMenuItem";
            this.cerrarSesionToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.cerrarSesionToolStripMenuItem.Text = "Cerrar Sesion";
            this.cerrarSesionToolStripMenuItem.Click += new System.EventHandler(this.cerrarSesionToolStripMenuItem_Click);
            // 
            // cambiarIdiomaToolStripMenuItem
            // 
            this.cambiarIdiomaToolStripMenuItem.Name = "cambiarIdiomaToolStripMenuItem";
            this.cambiarIdiomaToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.cambiarIdiomaToolStripMenuItem.Text = "Cambiar Idioma";
            this.cambiarIdiomaToolStripMenuItem.Click += new System.EventHandler(this.cambiarIdiomaToolStripMenuItem_Click);
            // 
            // cambiarContraseñaToolStripMenuItem
            // 
            this.cambiarContraseñaToolStripMenuItem.Name = "cambiarContraseñaToolStripMenuItem";
            this.cambiarContraseñaToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.cambiarContraseñaToolStripMenuItem.Text = "Cambiar Contraseña";
            this.cambiarContraseñaToolStripMenuItem.Click += new System.EventHandler(this.cambiarContraseñaToolStripMenuItem_Click_1);
            // 
            // administradorToolStripMenuItem
            // 
            this.administradorToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.gestionDeUsuariosToolStripMenuItem,
            this.gestionDeClientesToolStripMenuItem,
            this.gestionDePerfilesToolStripMenuItem,
            this.gestionDeIdiomasToolStripMenuItem,
            this.bitacoraDeEventosToolStripMenuItem,
            this.bitacoraDeCambiosToolStripMenuItem,
            this.verificacionIntegridadToolStripMenuItem});
            this.administradorToolStripMenuItem.Name = "administradorToolStripMenuItem";
            this.administradorToolStripMenuItem.Size = new System.Drawing.Size(95, 20);
            this.administradorToolStripMenuItem.Text = "Administrador";
            // 
            // gestionDeUsuariosToolStripMenuItem
            // 
            this.gestionDeUsuariosToolStripMenuItem.Name = "gestionDeUsuariosToolStripMenuItem";
            this.gestionDeUsuariosToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.gestionDeUsuariosToolStripMenuItem.Text = "Gestion de usuarios";
            this.gestionDeUsuariosToolStripMenuItem.Click += new System.EventHandler(this.gestionDeUsuariosToolStripMenuItem_Click);
            // 
            // gestionDeClientesToolStripMenuItem
            // 
            this.gestionDeClientesToolStripMenuItem.Name = "gestionDeClientesToolStripMenuItem";
            this.gestionDeClientesToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.gestionDeClientesToolStripMenuItem.Text = "Gestion de clientes";
            this.gestionDeClientesToolStripMenuItem.Click += new System.EventHandler(this.gestionDeClientesToolStripMenuItem_Click);
            // 
            // gestionDePerfilesToolStripMenuItem
            // 
            this.gestionDePerfilesToolStripMenuItem.Name = "gestionDePerfilesToolStripMenuItem";
            this.gestionDePerfilesToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.gestionDePerfilesToolStripMenuItem.Text = "Gestion de perfiles";
            this.gestionDePerfilesToolStripMenuItem.Click += new System.EventHandler(this.gestionDePerfilesToolStripMenuItem_Click);
            // 
            // gestionDeIdiomasToolStripMenuItem
            // 
            this.gestionDeIdiomasToolStripMenuItem.Name = "gestionDeIdiomasToolStripMenuItem";
            this.gestionDeIdiomasToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.gestionDeIdiomasToolStripMenuItem.Text = "Gestion de idiomas";
            this.gestionDeIdiomasToolStripMenuItem.Click += new System.EventHandler(this.gestionDeIdiomasToolStripMenuItem_Click);
            // 
            // bitacoraDeEventosToolStripMenuItem
            // 
            this.bitacoraDeEventosToolStripMenuItem.Name = "bitacoraDeEventosToolStripMenuItem";
            this.bitacoraDeEventosToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.bitacoraDeEventosToolStripMenuItem.Text = "Bitacora de eventos";
            this.bitacoraDeEventosToolStripMenuItem.Click += new System.EventHandler(this.bitacoraDeEventosToolStripMenuItem_Click);
            // 
            // bitacoraDeCambiosToolStripMenuItem
            // 
            this.bitacoraDeCambiosToolStripMenuItem.Name = "bitacoraDeCambiosToolStripMenuItem";
            this.bitacoraDeCambiosToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.bitacoraDeCambiosToolStripMenuItem.Text = "Bitacora de cambios";
            this.bitacoraDeCambiosToolStripMenuItem.Click += new System.EventHandler(this.bitacoraDeCambiosToolStripMenuItem_Click);
            // 
            // verificacionIntegridadToolStripMenuItem
            // 
            this.verificacionIntegridadToolStripMenuItem.Name = "verificacionIntegridadToolStripMenuItem";
            this.verificacionIntegridadToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.verificacionIntegridadToolStripMenuItem.Text = "Verificación de Integridad";
            this.verificacionIntegridadToolStripMenuItem.Click += new System.EventHandler(this.verificaciónDeIntegridadToolStripMenuItem_Click);
            // 
            // ventasToolStripMenuItem
            // 
            this.ventasToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.gestionDeProductosToolStripMenuItem,
            this.nuevaVentaToolStripMenuItem,
            this.verFacturasToolStripMenuItem});
            this.ventasToolStripMenuItem.Name = "ventasToolStripMenuItem";
            this.ventasToolStripMenuItem.Size = new System.Drawing.Size(53, 20);
            this.ventasToolStripMenuItem.Text = "Ventas";
            // 
            // gestionDeProductosToolStripMenuItem
            // 
            this.gestionDeProductosToolStripMenuItem.Name = "gestionDeProductosToolStripMenuItem";
            this.gestionDeProductosToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.gestionDeProductosToolStripMenuItem.Text = "Gestion de productos";
            this.gestionDeProductosToolStripMenuItem.Click += new System.EventHandler(this.gestionDeProductosToolStripMenuItem_Click);
            // 
            // nuevaVentaToolStripMenuItem
            // 
            this.nuevaVentaToolStripMenuItem.Name = "nuevaVentaToolStripMenuItem";
            this.nuevaVentaToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.nuevaVentaToolStripMenuItem.Text = "Nueva venta";
            this.nuevaVentaToolStripMenuItem.Click += new System.EventHandler(this.nuevaVentaToolStripMenuItem_Click);
            // 
            // verFacturasToolStripMenuItem
            // 
            this.verFacturasToolStripMenuItem.Name = "verFacturasToolStripMenuItem";
            this.verFacturasToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.verFacturasToolStripMenuItem.Text = "Ver facturas";
            this.verFacturasToolStripMenuItem.Click += new System.EventHandler(this.verFacturasToolStripMenuItem_Click);
            // 
            // comprasToolStripMenuItem
            // 
            this.comprasToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.insumosToolStripMenuItem,
            this.recetasToolStripMenuItem,
            this.proveedoresToolStripMenuItem,
            this.solicitudesCotizacionToolStripMenuItem,
            this.cotizacionesToolStripMenuItem,
            this.ordenesCompraToolStripMenuItem,
            this.recepcionInsumosToolStripMenuItem});
            this.comprasToolStripMenuItem.Name = "comprasToolStripMenuItem";
            this.comprasToolStripMenuItem.Size = new System.Drawing.Size(67, 20);
            this.comprasToolStripMenuItem.Text = "Compras";
            // 
            // insumosToolStripMenuItem
            // 
            this.insumosToolStripMenuItem.Name = "insumosToolStripMenuItem";
            this.insumosToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.insumosToolStripMenuItem.Text = "Insumos";
            this.insumosToolStripMenuItem.Click += new System.EventHandler(this.insumosToolStripMenuItem_Click);
            // 
            // recetasToolStripMenuItem
            // 
            this.recetasToolStripMenuItem.Name = "recetasToolStripMenuItem";
            this.recetasToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.recetasToolStripMenuItem.Text = "Recetas";
            this.recetasToolStripMenuItem.Click += new System.EventHandler(this.recetasToolStripMenuItem_Click);
            // 
            // proveedoresToolStripMenuItem
            // 
            this.proveedoresToolStripMenuItem.Name = "proveedoresToolStripMenuItem";
            this.proveedoresToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.proveedoresToolStripMenuItem.Text = "Gestion de proveedores";
            this.proveedoresToolStripMenuItem.Click += new System.EventHandler(this.proveedoresToolStripMenuItem_Click);
            // 
            // solicitudesCotizacionToolStripMenuItem
            // 
            this.solicitudesCotizacionToolStripMenuItem.Name = "solicitudesCotizacionToolStripMenuItem";
            this.solicitudesCotizacionToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.solicitudesCotizacionToolStripMenuItem.Text = "Solicitudes de cotizacion";
            this.solicitudesCotizacionToolStripMenuItem.Click += new System.EventHandler(this.solicitudesCotizacionToolStripMenuItem_Click);
            // 
            // cotizacionesToolStripMenuItem
            // 
            this.cotizacionesToolStripMenuItem.Name = "cotizacionesToolStripMenuItem";
            this.cotizacionesToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.cotizacionesToolStripMenuItem.Text = "Cotizaciones";
            this.cotizacionesToolStripMenuItem.Click += new System.EventHandler(this.cotizacionesToolStripMenuItem_Click);
            // 
            // ordenesCompraToolStripMenuItem
            // 
            this.ordenesCompraToolStripMenuItem.Name = "ordenesCompraToolStripMenuItem";
            this.ordenesCompraToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.ordenesCompraToolStripMenuItem.Text = "Ordenes de compra";
            this.ordenesCompraToolStripMenuItem.Click += new System.EventHandler(this.ordenesCompraToolStripMenuItem_Click);
            // 
            // recepcionInsumosToolStripMenuItem
            // 
            this.recepcionInsumosToolStripMenuItem.Name = "recepcionInsumosToolStripMenuItem";
            this.recepcionInsumosToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.recepcionInsumosToolStripMenuItem.Text = "Recepcion de insumos";
            this.recepcionInsumosToolStripMenuItem.Click += new System.EventHandler(this.recepcionInsumosToolStripMenuItem_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // frmInicio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::UI.Properties.Resources.Fondo;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmInicio";
            this.Text = "Inicio";
            this.Load += new System.EventHandler(this.frmInicio_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem usuarioToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem iniciarSesionToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        public System.Windows.Forms.ToolStripMenuItem cerrarSesionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bitacoraDeEventosToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem administradorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gestionDeUsuariosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gestionDeClientesToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem cambiarIdiomaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gestionDeIdiomasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gestionDePerfilesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bitacoraDeCambiosToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem cambiarContraseñaToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem verificacionIntegridadToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem ventasToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem comprasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem insumosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem recetasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem proveedoresToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem solicitudesCotizacionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cotizacionesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ordenesCompraToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem recepcionInsumosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gestionDeProductosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nuevaVentaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem verFacturasToolStripMenuItem;
    }
}

