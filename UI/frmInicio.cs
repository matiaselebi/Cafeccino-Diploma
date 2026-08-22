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
        }
        public Login login = new Login();
        public CerrarSesion cerrarSesion = new CerrarSesion();
        public frmEventos eventos = new frmEventos();
        private GestionUsuarios gestionUsuarios;
        public AdministradorIdiomas administradorIdiomas = new AdministradorIdiomas();
        public GestionDePerfiles gestionDePerfiles = new GestionDePerfiles();
        public GestionDeFamilias gestionDeFamilias = new GestionDeFamilias();
        public frmBitacoraCambios bitacoraCambios = new frmBitacoraCambios();
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
                    DialogResult resultado = MessageBox.Show(
                        "Se detectó que la tabla Usuario fue alterada fuera del sistema.\n\n" +
                        "¿Desea ingresar al sistema de todas formas?",
                        "Alerta de integridad",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning);

                    if (resultado == DialogResult.No)
                    {
                        Application.Exit();
                    }
                    else
                    {
                       
                        bllUsuario.SincronizarDigitos();
                    }
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
    }
}
