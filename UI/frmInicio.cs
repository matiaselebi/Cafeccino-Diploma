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
            Singleton.Instancia.Suscribir(gestionUsuarios);


            eventos.MdiParent = this;
            Singleton.Instancia.Suscribir(eventos);


            administradorIdiomas.MdiParent = this;
            Singleton.Instancia.Suscribir(administradorIdiomas);


            gestionDePerfiles.MdiParent = this;
            Singleton.Instancia.Suscribir(gestionDePerfiles);


            gestionDeFamilias.MdiParent = this;
            Singleton.Instancia.Suscribir(gestionDeFamilias);

            Singleton.Instancia.Suscribir(bitacoraCambios);
            Singleton.Instancia.Suscribir(cambiarContraseña);
            Singleton.Instancia.Suscribir(verificacionIntegridad);
            Singleton.Instancia.Suscribir(cerrarSesion);

            //Este metodo es para el frmInicio

            Singleton.Instancia.Suscribir(this);

            ValidarIntegridadInicial();
        }
        private void ValidarIntegridadInicial()
        {

            {
                BLLUsuario bllUsuario = new BLLUsuario();
                bool integro = bllUsuario.ValidarIntegridad();

                if (!integro)
                {
                    MessageBox.Show(
                        "Se detectó que la tabla Usuario fue alterada fuera del sistema.\n\n" +
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
    }
}
