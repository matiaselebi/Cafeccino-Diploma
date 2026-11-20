using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BE;
using BLL;
using SERVICIOS;

namespace CAFECCINO_INGENIERIA_DE_SOFTWARE
{
    public partial class frmInicio : Form, IObserver
    {
        public frmInicio()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // El fondo grande de la ventana MDI se redibuja sobre un control interno (MdiClient)
            // que no respeta el DoubleBuffered del Form, y eso es lo que se ve lento/tironeado al
            // arrastrar la ventana rapido. Se activa el double buffer en ese control puntual.
            foreach (Control control in this.Controls)
            {
                if (control is MdiClient mdiClient)
                {
                    typeof(Control).InvokeMember(
                        "DoubleBuffered",
                        System.Reflection.BindingFlags.SetProperty | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic,
                        null, mdiClient, new object[] { true });
                    break;
                }
            }
        }
        public Login login = new Login();
        public CerrarSesion cerrarSesion = new CerrarSesion();
        public frmBitacoraEventos eventos = new frmBitacoraEventos();
        private GestionUsuarios gestionUsuarios;
        public AdministradorIdiomas administradorIdiomas = new AdministradorIdiomas();
        public GestionDePerfiles gestionDePerfiles = new GestionDePerfiles();
        public GestionDeFamilias gestionDeFamilias = new GestionDeFamilias();
        public frmAuditoriaCambios bitacoraCambios = new frmAuditoriaCambios();
        public frmCambiarContraseña cambiarContraseña = new frmCambiarContraseña();
        public frmVerificacionIntegridad verificacionIntegridad = new frmVerificacionIntegridad();
        public GestionProductos gestionProductos = new GestionProductos();
        public frmVenta frmVenta = new frmVenta();
        public frmFacturas frmFacturas = new frmFacturas();
        public frmGestionClientes gestionClientes = new frmGestionClientes();
        public frmCambiarIdioma cambiarIdioma = new frmCambiarIdioma();
        public frmInsumos insumos = new frmInsumos();
        public frmRecetas recetas = new frmRecetas();
        public frmProveedores proveedores = new frmProveedores();
        public frmSolicitudCotizacion solicitudesCotizacion = new frmSolicitudCotizacion();
        public frmCotizaciones cotizaciones = new frmCotizaciones();
        public frmOrdenesCompra ordenesCompra = new frmOrdenesCompra();
        public frmRecepcionInsumos recepcionInsumos = new frmRecepcionInsumos();
        private frmInicio menuPrincipal;
        Traductor traductor = new Traductor();

        public void ActualizarIdioma(string idioma)
        {
            traductor.TraducirFormulario(this, int.Parse(idioma));
        }
        public frmInicio MenuPrincipal
        {
            get
            {
                if (menuPrincipal == null || menuPrincipal.IsDisposed)
                    menuPrincipal = this;
                return menuPrincipal;
            }
        }

        private void frmInicio_Load(object sender, EventArgs e)
        {
            cerrarSesionToolStripMenuItem.Visible = false;
            administradorToolStripMenuItem.Visible = false;
            cambiarContraseñaToolStripMenuItem.Visible = false;


            gestionUsuarios = new GestionUsuarios();
            gestionUsuarios.MdiParent = this;
            GestorIdiomas.Instancia.Suscribir(gestionUsuarios);


            eventos.MdiParent = this;
            GestorIdiomas.Instancia.Suscribir(eventos);


            administradorIdiomas.MdiParent = this;
            GestorIdiomas.Instancia.Suscribir(administradorIdiomas);


            gestionDePerfiles.MdiParent = this;
            GestorIdiomas.Instancia.Suscribir(gestionDePerfiles);


            gestionDeFamilias.MdiParent = this;
            GestorIdiomas.Instancia.Suscribir(gestionDeFamilias);

            GestorIdiomas.Instancia.Suscribir(bitacoraCambios);
            GestorIdiomas.Instancia.Suscribir(cambiarContraseña);
            GestorIdiomas.Instancia.Suscribir(verificacionIntegridad);
            GestorIdiomas.Instancia.Suscribir(cerrarSesion);
            GestorIdiomas.Instancia.Suscribir(gestionProductos);
            GestorIdiomas.Instancia.Suscribir(frmVenta);
            GestorIdiomas.Instancia.Suscribir(frmFacturas);
            GestorIdiomas.Instancia.Suscribir(gestionClientes);
            GestorIdiomas.Instancia.Suscribir(cambiarIdioma);
            GestorIdiomas.Instancia.Suscribir(insumos);
            GestorIdiomas.Instancia.Suscribir(recetas);
            GestorIdiomas.Instancia.Suscribir(proveedores);
            GestorIdiomas.Instancia.Suscribir(solicitudesCotizacion);
            GestorIdiomas.Instancia.Suscribir(cotizaciones);
            GestorIdiomas.Instancia.Suscribir(ordenesCompra);
            GestorIdiomas.Instancia.Suscribir(recepcionInsumos);
            ventasToolStripMenuItem.Visible = false;
            comprasToolStripMenuItem.Visible = false;

            //Este metodo es para el frmInicio

            GestorIdiomas.Instancia.Suscribir(this);

            ValidarIntegridadInicial();
        }
        private void ValidarIntegridadInicial()
        {

            {
                new BLLBitacoraEventos().RegistrarDesbloqueos_389ME();
                BLLUsuario bllUsuario = new BLLUsuario();
                bool integro = bllUsuario.ValidarIntegridad();

                // Las demas tablas tienen su propio control de digitos verificadores
                List<string> alteradas_389ME = new BLLDigitoVerificador().Verificar_389ME();
                if (!integro)
                    alteradas_389ME.Insert(0, "Usuario: se modificaron, agregaron o borraron filas por fuera del sistema.");

                if (alteradas_389ME.Count > 0)
                {
                    MessageBox.Show(
                        "Se detectaron tablas alteradas fuera del sistema:\n\n" + string.Join("\n", alteradas_389ME) + "\n\n" +
                        "Inicie sesión como administrador y utilice 'Administrador > Verificación de Integridad' " +
                        "para restaurar el último backup o recalcular los dígitos verificadores.",
                        "Alerta de integridad",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
            }
        }
        private void iniciarSesionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            login.MdiParent = this;
            login.Show();
        }

        private void cerrarSesionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cerrarSesion.MdiParent = this;
            cerrarSesion.Show();

        }


        private void bitacoraDeEventosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            eventos.MdiParent = this;
            eventos.Show();
        }

        private void gestionDeUsuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (gestionUsuarios == null || gestionUsuarios.IsDisposed)
            {
                gestionUsuarios = new GestionUsuarios();
                gestionUsuarios.MdiParent = this;
            }
            gestionUsuarios.Show();
        }



        private void cambiarIdiomaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cambiarIdioma.MdiParent = this;
            cambiarIdioma.Show();
        }

        private void insumosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            insumos.MdiParent = this;
            insumos.Show();
        }

        private void recetasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            recetas.MdiParent = this;
            recetas.Show();
        }

        private void proveedoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            proveedores.MdiParent = this;
            proveedores.Show();
        }

        private void solicitudesCotizacionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            solicitudesCotizacion.MdiParent = this;
            solicitudesCotizacion.Show();
        }

        private void cotizacionesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cotizaciones.MdiParent = this;
            cotizaciones.Show();
        }

        private void ordenesCompraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ordenesCompra.MdiParent = this;
            ordenesCompra.Show();
        }

        private void recepcionInsumosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            recepcionInsumos.MdiParent = this;
            recepcionInsumos.Show();
        }

        private void gestionDeClientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gestionClientes.MdiParent = this;
            gestionClientes.Show();
        }

        private void gestionDeIdiomasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            administradorIdiomas.MdiParent = this;
            administradorIdiomas.Show();
        }

        private void gestionDePerfilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gestionDePerfiles.MdiParent = this;
            gestionDeFamilias.MdiParent = this;
            gestionDePerfiles.Show();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
        public void AplicarPermisosUsuario(List<Perfil> componentesUsuario)
        {

            ApagarMenuCompleto(menuStrip1.Items);


            HabilitarControlesRecursivo(componentesUsuario);
        }


        private void ApagarMenuCompleto(ToolStripItemCollection items)
        {
            foreach (ToolStripItem item in items)
            {


                if (item.Name == "usuarioToolStripMenuItem")
                {
                    item.Visible = true;


                    if (item is ToolStripMenuItem menuDesplegableUsuario)
                    {

                        foreach (ToolStripItem subItem in menuDesplegableUsuario.DropDownItems)
                        {
                            subItem.Visible = true;
                        }
                    }

                    continue;
                }


                item.Visible = false;

                if (item is ToolStripMenuItem menuDesplegable)
                {
                    ApagarMenuCompleto(menuDesplegable.DropDownItems);
                }
            }
        }

        private void HabilitarControlesRecursivo(List<Perfil> componentes)
        {
            foreach (var comp in componentes)
            {
                if (comp is Permiso)
                {

                    ToolStripItem controlEncontrado = BuscarControlMenu(menuStrip1.Items, comp.Nombre);

                    if (controlEncontrado != null)
                    {
                        controlEncontrado.Visible = true;


                        PrenderPadresOcultos(controlEncontrado);
                    }
                }
                else if (comp is Familia)
                {
                    HabilitarControlesRecursivo(((Familia)comp).Hijos.ToList());
                }
            }
        }


        private ToolStripItem BuscarControlMenu(ToolStripItemCollection items, string nombreBuscar)
        {
            foreach (ToolStripItem item in items)
            {
                if (item.Name == nombreBuscar) return item;

                if (item is ToolStripMenuItem menuDesplegable)
                {
                    ToolStripItem encontrado = BuscarControlMenu(menuDesplegable.DropDownItems, nombreBuscar);
                    if (encontrado != null) return encontrado;
                }
            }
            return null;
        }


        private void PrenderPadresOcultos(ToolStripItem item)
        {
            if (item.OwnerItem != null)
            {
                item.OwnerItem.Visible = true;
                PrenderPadresOcultos(item.OwnerItem);
            }
        }

        private void bitacoraDeCambiosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bitacoraCambios.MdiParent = this;
            bitacoraCambios.Show();
        }



        private void verificaciónDeIntegridadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            verificacionIntegridad.MdiParent = this;
            verificacionIntegridad.Show();
        }

        private void cambiarContraseñaToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            cambiarContraseña.MdiParent = this;
            cambiarContraseña.Show();
        }

        private void gestionDeProductosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gestionProductos.MdiParent = this;
            gestionProductos.Show();
        }

        private void nuevaVentaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmVenta.MdiParent = this;
            frmVenta.Show();
        }

        private void verFacturasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmFacturas.MdiParent = this;
            frmFacturas.Show();
        }
    }
}
