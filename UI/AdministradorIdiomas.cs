using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL;
using SERVICIOS;
using BE;

namespace CAFECCINO_INGENIERIA_DE_SOFTWARE
{
    public partial class AdministradorIdiomas : Form, IObserver
    {
        public BLLIdioma bllIdioma = new BLLIdioma();
        Traductor traductor = new Traductor();
        public AdministradorIdiomas()
        {
            InitializeComponent();
            txtNuevoIdioma.KeyPress += Validacion.SoloLetras_389ME;
          
        }
        public void ActualizarIdioma(string idioma)
        {
            traductor.TraducirFormulario(this, int.Parse(idioma));
        }
        private void ConfigurarGrilla()
        {
            dgvIdiomas.AutoGenerateColumns = false;
            dgvIdiomas.Columns.Clear();

           
            DataGridViewTextBoxColumn colId = new DataGridViewTextBoxColumn();
            colId.Name = "IdControl";
            colId.HeaderText = "Id";
            colId.DataPropertyName = "IdControl";
            colId.Visible = false;
            dgvIdiomas.Columns.Add(colId);

            
            DataGridViewTextBoxColumn colControl = new DataGridViewTextBoxColumn();
            colControl.Name = "Control";
            colControl.HeaderText = "Control";
            colControl.DataPropertyName = "Control";
            colControl.ReadOnly = true;
            dgvIdiomas.Columns.Add(colControl);

            
            DataGridViewTextBoxColumn colTextoOriginal = new DataGridViewTextBoxColumn();
            colTextoOriginal.Name = "TextoOriginal";
            colTextoOriginal.HeaderText = "Texto Original";
            colTextoOriginal.DataPropertyName = "TextoOriginal";
            colTextoOriginal.ReadOnly = true;
            dgvIdiomas.Columns.Add(colTextoOriginal);

            
            DataGridViewTextBoxColumn colTraduccion = new DataGridViewTextBoxColumn();
            colTraduccion.Name = "Traduccion";
            colTraduccion.HeaderText = "Traducción";
            colTraduccion.DataPropertyName = "Traduccion";
            colTraduccion.MaxInputLength = 300;   // largo de la columna Texto en la base
            dgvIdiomas.Columns.Add(colTraduccion);
        }
        private void AdministradorIdiomas_Load(object sender, EventArgs e)
        {
            ConfigurarGrilla();

            cmbIdioma.DataSource = bllIdioma.ListarIdiomas();
            cmbIdioma.DisplayMember = "Nombre";
            cmbIdioma.ValueMember = "IdIdioma";

            
            cmbFormulario.Items.Clear();
            cmbFormulario.Items.Add("Login");
            cmbFormulario.Items.Add("GestionUsuarios");
            cmbFormulario.Items.Add("frmBitacoraEventos");
            cmbFormulario.Items.Add("frmInicio");
            cmbFormulario.Items.Add("AdministradorIdiomas");
            cmbFormulario.Items.Add("GestionDePerfiles");
            cmbFormulario.Items.Add("GestionDeFamilias");
            cmbFormulario.Items.Add("frmAuditoriaCambios");
            cmbFormulario.Items.Add("CerrarSesion");
            cmbFormulario.Items.Add("frmCambiarContraseña");
            cmbFormulario.Items.Add("frmVerificacionIntegridad");
            cmbFormulario.Items.Add("GestionProductos");
            cmbFormulario.Items.Add("frmVenta");
            cmbFormulario.Items.Add("frmFacturas");
            cmbFormulario.Items.Add("frmGestionClientes");
            cmbFormulario.Items.Add("frmCambiarIdioma");
            cmbFormulario.Items.Add("frmInsumos");
            cmbFormulario.Items.Add("frmRecetas");
            cmbFormulario.Items.Add("frmProveedores");
            cmbFormulario.Items.Add("frmSolicitudCotizacion");
            cmbFormulario.Items.Add("frmCotizaciones");
            cmbFormulario.Items.Add("frmOrdenesCompra");
            cmbFormulario.Items.Add("frmRecepcionInsumos");
            cmbFormulario.SelectedIndex = -1;
        }

        private void CargarGrilla()
        {
            if (cmbFormulario.SelectedIndex == -1 || cmbIdioma.SelectedIndex == -1)
                return;

            dgvIdiomas.DataSource = bllIdioma.ObtenerTraducciones(
                cmbFormulario.Text,
                Convert.ToInt32(cmbIdioma.SelectedValue));
        }
        private void cmbFormulario_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbFormulario.SelectedIndex == -1) return;

            RegistrarControlesFormulario(cmbFormulario.Text);

            
            if (cmbIdioma.SelectedIndex != -1)
                CargarGrilla();
            else
                dgvIdiomas.DataSource = bllIdioma.ObtenerControles(cmbFormulario.Text);
        }
        private void RegistrarControlesFormulario(string formulario)
        {
            switch (formulario)
            {
                case "Login":
                    bllIdioma.RegistrarControl("Login", "lblUsuario", "Usuario");
                    bllIdioma.RegistrarControl("Login", "lblContraseña", "Contraseña");
                    bllIdioma.RegistrarControl("Login", "btnLogin", "Ingresar");
                    bllIdioma.RegistrarControl("Login", "btnCancelar", "Cancelar");
                    break;

                case "GestionUsuarios":
                    bllIdioma.RegistrarControl("GestionUsuarios", "label1", "DNI");
                    bllIdioma.RegistrarControl("GestionUsuarios", "label2", "Usuario");
                    bllIdioma.RegistrarControl("GestionUsuarios", "label3", "Contraseña");
                    bllIdioma.RegistrarControl("GestionUsuarios", "label4", "Email");
                    bllIdioma.RegistrarControl("GestionUsuarios", "label5", "Nombre");
                    bllIdioma.RegistrarControl("GestionUsuarios", "label6", "Apellido");
                    bllIdioma.RegistrarControl("GestionUsuarios", "label7", "NumTelefono");
                    bllIdioma.RegistrarControl("GestionUsuarios", "label8", "Rol");
                    bllIdioma.RegistrarControl("GestionUsuarios", "Agregar", "Agregar");
                    bllIdioma.RegistrarControl("GestionUsuarios", "Modificar", "Modificar");
                    bllIdioma.RegistrarControl("GestionUsuarios", "Eliminar", "Eliminar");
                    break;

                case "frmBitacoraEventos":
                    bllIdioma.RegistrarControl("frmBitacoraEventos", "label1", "Fecha");
                    bllIdioma.RegistrarControl("frmBitacoraEventos", "label2", "Modulo");
                    bllIdioma.RegistrarControl("frmBitacoraEventos", "label3", "Criticidad");
                    bllIdioma.RegistrarControl("frmBitacoraEventos", "label4", "Evento");
                    bllIdioma.RegistrarControl("frmBitacoraEventos", "Buscar", "Buscar");
                    break;

                case "frmInicio":
                    bllIdioma.RegistrarControl("frmInicio", "iniciarSesionToolStripMenuItem", "Iniciar Sesión");
                    bllIdioma.RegistrarControl("frmInicio", "cerrarSesionToolStripMenuItem", "Cerrar Sesión");
                    bllIdioma.RegistrarControl("frmInicio", "cambiarContraseñaToolStripMenuItem", "Cambiar Contraseña");
                    bllIdioma.RegistrarControl("frmInicio", "verificacionIntegridadToolStripMenuItem", "Verificación de Integridad");
                    bllIdioma.RegistrarControl("frmInicio", "administradorToolStripMenuItem", "Administrador");
                    bllIdioma.RegistrarControl("frmInicio", "gestionDeUsuariosToolStripMenuItem", "Gestión de Usuarios");
                    bllIdioma.RegistrarControl("frmInicio", "gestionDePerfilesToolStripMenuItem", "Gestión de Perfiles");
                    bllIdioma.RegistrarControl("frmInicio", "gestionDeIdiomasToolStripMenuItem", "Gestión de Idiomas");
                    bllIdioma.RegistrarControl("frmInicio", "usuarioToolStripMenuItem", "Usuario");
                    bllIdioma.RegistrarControl("frmInicio", "bitacoraDeEventosToolStripMenuItem", "Eventos");
                    bllIdioma.RegistrarControl("frmInicio", "bitacoraDeCambiosToolStripMenuItem", "Cambios");
                    bllIdioma.RegistrarControl("frmInicio", "ventasToolStripMenuItem", "Ventas");
                    bllIdioma.RegistrarControl("frmInicio", "gestionDeProductosToolStripMenuItem", "Gestion de productos");
                    bllIdioma.RegistrarControl("frmInicio", "nuevaVentaToolStripMenuItem", "Nueva venta");
                    bllIdioma.RegistrarControl("frmInicio", "verFacturasToolStripMenuItem", "Ver facturas");
                    bllIdioma.RegistrarControl("frmInicio", "gestionDeClientesToolStripMenuItem", "Gestion de clientes");
                    bllIdioma.RegistrarControl("frmInicio", "cambiarIdiomaToolStripMenuItem", "Cambiar Idioma");
                    bllIdioma.RegistrarControl("frmInicio", "comprasToolStripMenuItem", "Compras");
                    bllIdioma.RegistrarControl("frmInicio", "insumosToolStripMenuItem", "Insumos");
                    bllIdioma.RegistrarControl("frmInicio", "recetasToolStripMenuItem", "Recetas");
                    bllIdioma.RegistrarControl("frmInicio", "proveedoresToolStripMenuItem", "Gestion de proveedores");
                    bllIdioma.RegistrarControl("frmInicio", "solicitudesCotizacionToolStripMenuItem", "Solicitudes de cotizacion");
                    bllIdioma.RegistrarControl("frmInicio", "cotizacionesToolStripMenuItem", "Cotizaciones");
                    bllIdioma.RegistrarControl("frmInicio", "ordenesCompraToolStripMenuItem", "Ordenes de compra");
                    bllIdioma.RegistrarControl("frmInicio", "recepcionInsumosToolStripMenuItem", "Recepcion de insumos");
                    break;

                case "AdministradorIdiomas":
                    bllIdioma.RegistrarControl("AdministradorIdiomas", "label1", "Formulario");
                    bllIdioma.RegistrarControl("AdministradorIdiomas", "label2", "Idioma");
                    bllIdioma.RegistrarControl("AdministradorIdiomas", "btnGuardar", "Guardar");
                    bllIdioma.RegistrarControl("AdministradorIdiomas", "btnNuevoIdioma", "NuevoIdioma");
                    break;
                
                case "GestionDePerfiles":
                    bllIdioma.RegistrarControl("GestionDePerfiles", "label1", "Perfiles");
                    bllIdioma.RegistrarControl("GestionDePerfiles", "label2", "Familias");
                    bllIdioma.RegistrarControl("GestionDePerfiles", "label3", "Permisos");
                    bllIdioma.RegistrarControl("GestionDePerfiles", "label4", "Nombre del perfil"); 
                    bllIdioma.RegistrarControl("GestionDePerfiles", "BTNAgregarFamilia", "Agregar Familia");
                    bllIdioma.RegistrarControl("GestionDePerfiles", "BTNAgregarPermiso", "Agregar Permiso");
                    bllIdioma.RegistrarControl("GestionDePerfiles", "BTNCrearPerfil", "Agregar Perfil");              
                    bllIdioma.RegistrarControl("GestionDePerfiles", "BTNEliminarPerfil", "Eliminar Perfil");
                    bllIdioma.RegistrarControl("GestionDePerfiles", "BTNAplicar", "Aplicar"); 
                    bllIdioma.RegistrarControl("GestionDePerfiles", "BTNConfigurarFamilias", "Configurar familias");
                    break;

                case "GestionDeFamilias":
                    bllIdioma.RegistrarControl("GestionDeFamilias", "label1", "Nombre de la familia");
                    bllIdioma.RegistrarControl("GestionDeFamilias", "label2", "Familias a editar");
                    bllIdioma.RegistrarControl("GestionDeFamilias", "label3", "Permisos");
                    bllIdioma.RegistrarControl("GestionDeFamilias", "label4", "Familias");
                    bllIdioma.RegistrarControl("GestionDeFamilias", "BTNAgregarFamilia", "Agregar Familia");
                    bllIdioma.RegistrarControl("GestionDeFamilias", "BTNAgregarPermiso", "Agregar permiso");              
                    bllIdioma.RegistrarControl("GestionDeFamilias", "BTNCrearFamilia", "Crear Familia");
                    bllIdioma.RegistrarControl("GestionDeFamilias", "BTNEliminarFamilia", "Eliminar Familia"); 
                    bllIdioma.RegistrarControl("GestionDeFamilias", "BTNAplicar", "Aplicar");
                    break;
                case "frmAuditoriaCambios":

                    bllIdioma.RegistrarControl("frmAuditoriaCambios", "button2", "Restaurar");
                    bllIdioma.RegistrarControl("frmAuditoriaCambios", "button1", "Ver Todos");
                    break;

                case "CerrarSesion":
                    bllIdioma.RegistrarControl("CerrarSesion", "label1", "Confirmar cierre de sesion?");
                    bllIdioma.RegistrarControl("CerrarSesion", "button1", "Confirmar");
                    bllIdioma.RegistrarControl("CerrarSesion", "button2", "Cancelar");
                    break;

                case "frmCambiarContraseña":
                    bllIdioma.RegistrarControl("frmCambiarContraseña", "lblUsuario", "Usuario");
                    bllIdioma.RegistrarControl("frmCambiarContraseña", "lblNuevaContraseña", "Nueva contraseña");
                    bllIdioma.RegistrarControl("frmCambiarContraseña", "lblConfirmarContraseña", "Confirmar contraseña");
                    bllIdioma.RegistrarControl("frmCambiarContraseña", "btnCambiar", "Cambiar");
                    break;

                case "frmVerificacionIntegridad":
                    bllIdioma.RegistrarControl("frmVerificacionIntegridad", "lblTitulo", "Verificación de Integridad");
                    bllIdioma.RegistrarControl("frmVerificacionIntegridad", "btnRecalcular", "Recalcular");
                    bllIdioma.RegistrarControl("frmVerificacionIntegridad", "btnRestaurar", "Restaurar");
                    bllIdioma.RegistrarControl("frmVerificacionIntegridad", "btnSalir", "Salir");
                    break;

                case "GestionProductos":
                    bllIdioma.RegistrarControl("GestionProductos", "label1_389ME", "Nombre");
                    bllIdioma.RegistrarControl("GestionProductos", "label2_389ME", "Titulo");
                    bllIdioma.RegistrarControl("GestionProductos", "label3_389ME", "Precio");
                    bllIdioma.RegistrarControl("GestionProductos", "label4_389ME", "Stock");
                    bllIdioma.RegistrarControl("GestionProductos", "btnAgregar_389ME", "Agregar");
                    bllIdioma.RegistrarControl("GestionProductos", "btnModificar_389ME", "Modificar");
                    bllIdioma.RegistrarControl("GestionProductos", "btnEliminar_389ME", "Eliminar");
                    break;

                case "frmVenta":
                    bllIdioma.RegistrarControl("frmVenta", "lblProductos_389ME", "Productos disponibles");
                    bllIdioma.RegistrarControl("frmVenta", "lblCantidad_389ME", "Cantidad:");
                    bllIdioma.RegistrarControl("frmVenta", "btnAgregarCarrito_389ME", "Agregar al carrito");
                    bllIdioma.RegistrarControl("frmVenta", "lblCarrito_389ME", "Carrito");
                    bllIdioma.RegistrarControl("frmVenta", "btnQuitarItem_389ME", "Quitar del carrito");
                    bllIdioma.RegistrarControl("frmVenta", "lblDNI_389ME", "DNI");
                    bllIdioma.RegistrarControl("frmVenta", "btnBuscarCliente_389ME", "Buscar");
                    bllIdioma.RegistrarControl("frmVenta", "lblNombreCliente_389ME", "Nombre");
                    bllIdioma.RegistrarControl("frmVenta", "lblApellidoCliente_389ME", "Apellido");
                    bllIdioma.RegistrarControl("frmVenta", "lblDireccionCliente_389ME", "Direccion");
                    bllIdioma.RegistrarControl("frmVenta", "lblMailCliente_389ME", "Mail");
                    bllIdioma.RegistrarControl("frmVenta", "lblTelCliente_389ME", "Telefono");
                    bllIdioma.RegistrarControl("frmVenta", "lblMetodoPago_389ME", "Metodo de pago");
                    bllIdioma.RegistrarControl("frmVenta", "lblBanco_389ME", "Banco");
                    bllIdioma.RegistrarControl("frmVenta", "lblTipoTarjeta_389ME", "Tipo de tarjeta");
                    bllIdioma.RegistrarControl("frmVenta", "lblMarcaTarjeta_389ME", "Marca de tarjeta");
                    bllIdioma.RegistrarControl("frmVenta", "lblNumeroTarjeta_389ME", "Numero de tarjeta");
                    bllIdioma.RegistrarControl("frmVenta", "lblFechaExpiracion_389ME", "Vencimiento (MM/AA)");
                    bllIdioma.RegistrarControl("frmVenta", "lblCVV_389ME", "Codigo de seguridad");
                    bllIdioma.RegistrarControl("frmVenta", "btnConfirmarVenta_389ME", "Confirmar venta");
                    break;
                case "frmFacturas":
                    bllIdioma.RegistrarControl("frmFacturas", "lblFacturas_389ME", "Facturas");
                    bllIdioma.RegistrarControl("frmFacturas", "lblDetalle_389ME", "Productos de la factura seleccionada");
                    bllIdioma.RegistrarControl("frmFacturas", "btnImprimir_389ME", "Imprimir");
                    break;

                case "frmGestionClientes":
                    bllIdioma.RegistrarControl("frmGestionClientes", "lblClientes_389ME", "Clientes registrados");
                    bllIdioma.RegistrarControl("frmGestionClientes", "lblDNI_389ME", "DNI");
                    bllIdioma.RegistrarControl("frmGestionClientes", "lblNombre_389ME", "Nombre");
                    bllIdioma.RegistrarControl("frmGestionClientes", "lblApellido_389ME", "Apellido");
                    bllIdioma.RegistrarControl("frmGestionClientes", "lblDireccion_389ME", "Direccion");
                    bllIdioma.RegistrarControl("frmGestionClientes", "lblMail_389ME", "Mail");
                    bllIdioma.RegistrarControl("frmGestionClientes", "lblTelefono_389ME", "Telefono");
                    bllIdioma.RegistrarControl("frmGestionClientes", "btnAgregar_389ME", "Agregar");
                    bllIdioma.RegistrarControl("frmGestionClientes", "btnModificar_389ME", "Modificar");
                    bllIdioma.RegistrarControl("frmGestionClientes", "btnBloquear_389ME", "Bloquear");
                    bllIdioma.RegistrarControl("frmGestionClientes", "btnDesbloquear_389ME", "Desbloquear");
                    bllIdioma.RegistrarControl("frmGestionClientes", "btnSerializar_389ME", "Serializar");
                    bllIdioma.RegistrarControl("frmGestionClientes", "btnDeserializar_389ME", "Des-serializar");
                    bllIdioma.RegistrarControl("frmGestionClientes", "btnLimpiar_389ME", "Limpiar");
                    bllIdioma.RegistrarControl("frmGestionClientes", "btnActualizar_389ME", "Actualizar");
                    bllIdioma.RegistrarControl("frmGestionClientes", "lblArchivoXml_389ME", "Archivo XML");
                    break;

                case "frmCambiarIdioma":
                    bllIdioma.RegistrarControl("frmCambiarIdioma", "lblIdioma_389ME", "Idioma del sistema");
                    break;

                case "frmInsumos":
                    bllIdioma.RegistrarControl("frmInsumos", "lblInsumos_389ME", "Insumos");
                    bllIdioma.RegistrarControl("frmInsumos", "lblNombre_389ME", "Nombre");
                    bllIdioma.RegistrarControl("frmInsumos", "lblTipo_389ME", "Tipo");
                    bllIdioma.RegistrarControl("frmInsumos", "lblUnidad_389ME", "Unidad de medida");
                    bllIdioma.RegistrarControl("frmInsumos", "lblPrecio_389ME", "Precio");
                    bllIdioma.RegistrarControl("frmInsumos", "lblStock_389ME", "Stock");
                    bllIdioma.RegistrarControl("frmInsumos", "lblStockMinimo_389ME", "Stock minimo");
                    bllIdioma.RegistrarControl("frmInsumos", "lblStockMaximo_389ME", "Stock maximo");
                    bllIdioma.RegistrarControl("frmInsumos", "btnAgregar_389ME", "Agregar");
                    bllIdioma.RegistrarControl("frmInsumos", "btnModificar_389ME", "Modificar");
                    bllIdioma.RegistrarControl("frmInsumos", "btnEliminar_389ME", "Eliminar");
                    break;

                case "frmRecetas":
                    bllIdioma.RegistrarControl("frmRecetas", "lblProducto_389ME", "Producto");
                    bllIdioma.RegistrarControl("frmRecetas", "lblReceta_389ME", "Insumos por unidad de producto");
                    bllIdioma.RegistrarControl("frmRecetas", "lblInsumo_389ME", "Insumo");
                    bllIdioma.RegistrarControl("frmRecetas", "lblCantidad_389ME", "Cantidad");
                    bllIdioma.RegistrarControl("frmRecetas", "btnAgregarInsumo_389ME", "Agregar insumo");
                    bllIdioma.RegistrarControl("frmRecetas", "btnQuitarInsumo_389ME", "Quitar insumo");
                    bllIdioma.RegistrarControl("frmRecetas", "btnGuardarReceta_389ME", "Guardar receta");
                    break;

                case "frmProveedores":
                    bllIdioma.RegistrarControl("frmProveedores", "lblProveedores_389ME", "Gestion de proveedores");
                    bllIdioma.RegistrarControl("frmProveedores", "lblCUIT_389ME", "CUIT");
                    bllIdioma.RegistrarControl("frmProveedores", "lblNombre_389ME", "Nombre");
                    bllIdioma.RegistrarControl("frmProveedores", "lblRazonSocial_389ME", "Razon social");
                    bllIdioma.RegistrarControl("frmProveedores", "lblTelefono_389ME", "Telefono");
                    bllIdioma.RegistrarControl("frmProveedores", "lblEmail_389ME", "Email");
                    bllIdioma.RegistrarControl("frmProveedores", "lblDireccion_389ME", "Direccion");
                    bllIdioma.RegistrarControl("frmProveedores", "lblCuentaBancaria_389ME", "Cuenta bancaria (CBU)");
                    bllIdioma.RegistrarControl("frmProveedores", "btnPreRegistrar_389ME", "Pre-registrar");
                    bllIdioma.RegistrarControl("frmProveedores", "btnModificar_389ME", "Modificar");
                    break;

                case "frmSolicitudCotizacion":
                    bllIdioma.RegistrarControl("frmSolicitudCotizacion", "lblSugerencias_389ME", "Insumos y sugerencia de compra");
                    bllIdioma.RegistrarControl("frmSolicitudCotizacion", "btnAgregarSugeridos_389ME", "Agregar los sugeridos");
                    bllIdioma.RegistrarControl("frmSolicitudCotizacion", "btnAgregarInsumo_389ME", "Agregar insumo");
                    bllIdioma.RegistrarControl("frmSolicitudCotizacion", "lblInsumosSolicitud_389ME", "Insumos de la solicitud");
                    bllIdioma.RegistrarControl("frmSolicitudCotizacion", "btnQuitarInsumo_389ME", "Quitar insumo");
                    bllIdioma.RegistrarControl("frmSolicitudCotizacion", "lblProveedores_389ME", "Proveedores a cotizar");
                    bllIdioma.RegistrarControl("frmSolicitudCotizacion", "btnConfirmarSolicitud_389ME", "Confirmar solicitud");
                    break;

                case "frmCotizaciones":
                    bllIdioma.RegistrarControl("frmCotizaciones", "lblSolicitud_389ME", "Solicitud");
                    bllIdioma.RegistrarControl("frmCotizaciones", "lblProveedor_389ME", "Proveedor");
                    bllIdioma.RegistrarControl("frmCotizaciones", "lblInsumosSolicitados_389ME", "Insumos solicitados");
                    bllIdioma.RegistrarControl("frmCotizaciones", "lblPrecio_389ME", "Precio unitario");
                    bllIdioma.RegistrarControl("frmCotizaciones", "lblCantidad_389ME", "Cantidad");
                    bllIdioma.RegistrarControl("frmCotizaciones", "lblPrecioTotal_389ME", "Precio total");
                    bllIdioma.RegistrarControl("frmCotizaciones", "lblVencimiento_389ME", "Vencimiento");
                    bllIdioma.RegistrarControl("frmCotizaciones", "btnGuardarCotizacion_389ME", "Guardar cotizacion");
                    bllIdioma.RegistrarControl("frmCotizaciones", "lblCotizaciones_389ME", "Cotizaciones cargadas");
                    break;

                case "frmOrdenesCompra":
                    bllIdioma.RegistrarControl("frmOrdenesCompra", "lblSolicitud_389ME", "Solicitud");
                    bllIdioma.RegistrarControl("frmOrdenesCompra", "lblComparacion_389ME", "Comparacion de proveedores");
                    bllIdioma.RegistrarControl("frmOrdenesCompra", "lblCotizacionesProveedor_389ME", "Insumos cotizados (seleccione los que va a comprar)");
                    bllIdioma.RegistrarControl("frmOrdenesCompra", "btnGenerarOrden_389ME", "Generar orden de compra");
                    bllIdioma.RegistrarControl("frmOrdenesCompra", "btnImprimirOrden_389ME", "Imprimir orden");
                    bllIdioma.RegistrarControl("frmOrdenesCompra", "lblOrdenes_389ME", "Ordenes de compra");
                    bllIdioma.RegistrarControl("frmOrdenesCompra", "lblDetalleOrden_389ME", "Detalle de la orden");
                    break;

                case "frmRecepcionInsumos":
                    bllIdioma.RegistrarControl("frmRecepcionInsumos", "lblOrden_389ME", "Orden de compra");
                    bllIdioma.RegistrarControl("frmRecepcionInsumos", "lblDetalle_389ME", "Insumos de la orden");
                    bllIdioma.RegistrarControl("frmRecepcionInsumos", "lblCantidadRecibida_389ME", "Cantidad recibida");
                    bllIdioma.RegistrarControl("frmRecepcionInsumos", "btnActualizarCantidad_389ME", "Actualizar cantidad");
                    bllIdioma.RegistrarControl("frmRecepcionInsumos", "btnConfirmarRecepcion_389ME", "Confirmar recepcion");
                    break;
            }
           
        }
        private void cmbIdioma_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            CargarGrilla();
        }

        

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (cmbFormulario.SelectedIndex == -1 || cmbIdioma.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione un formulario y un idioma.");
                return;
            }

            foreach (DataGridViewRow fila in dgvIdiomas.Rows)
            {
                if (fila.IsNewRow)
                    continue;

                if (fila.Cells["Traduccion"].Value == null)
                    continue;

                if (string.IsNullOrWhiteSpace(fila.Cells["Traduccion"].Value.ToString()))
                    continue;

                Traduccion traduccion = new Traduccion();

                traduccion.Idioma = Convert.ToInt32(cmbIdioma.SelectedValue);
                traduccion.Control = Convert.ToInt32(fila.Cells["IdControl"].Value);
                traduccion.Texto = fila.Cells["Traduccion"].Value.ToString();

                bllIdioma.GuardarTraduccion(traduccion);
            }

            new BLLBitacoraEventos().RegistrarEvento_389ME("Traducciones guardadas",
                $"Formulario {cmbFormulario.Text} - Idioma {cmbIdioma.Text}", "Idiomas", "Baja");

            MessageBox.Show("Traducciones guardadas correctamente.");
        }
        

        private void dgvIdiomas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnNuevoIdioma_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNuevoIdioma.Text))
            {
                MessageBox.Show("Ingrese un nombre para el idioma.");
                return;
            }

            try
            {
                bllIdioma.AgregarIdioma(txtNuevoIdioma.Text.Trim());
            }
            catch (Exception ex_389ME)
            {
                MessageBox.Show(ex_389ME.Message);
                return;
            }

            cmbIdioma.DataSource = null;
            cmbIdioma.DataSource = bllIdioma.ListarIdiomas();
            cmbIdioma.DisplayMember = "Nombre";
            cmbIdioma.ValueMember = "IdIdioma";

            txtNuevoIdioma.Clear();


            MessageBox.Show("Idioma agregado correctamente.");
        }

        private void AdministradorIdiomas_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();

            }
        }

        private void AdministradorIdiomas_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
                txtNuevoIdioma.Clear();
        }
    }
}
