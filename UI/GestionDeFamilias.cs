using BE;
using BLL;
using SERVICIOS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CAFECCINO_INGENIERIA_DE_SOFTWARE
{
    public partial class GestionDeFamilias : Form , IObserver
    {
        private BLLPermiso NegociosPermiso = new BLLPermiso();
        

        private Familia _familiaSeleccionada;
        private bool CambioManual = true;
        Traductor traductor = new Traductor();
        private frmInicio parent;

        public GestionDeFamilias()
        {
            InitializeComponent();
        }

        public void ActualizarIdioma(string idioma)
        {
          
            traductor.TraducirFormulario(this, int.Parse(idioma));
        }

        private void GestionDeFamilias_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }

            treeView1.Nodes.Clear();
            TXTFamilia.Text = "";
            _familiaSeleccionada = null;
        }
        public void LlenarCombobox()
        {
            CambioManual = false;

            
            List<Perfil> permisos = NegociosPermiso.ObtenerPermisosSimples();
            CBPermisos.DataSource = permisos;
            CBPermisos.DisplayMember = "Nombre";
            CBPermisos.ValueMember = "Id";
            CBPermisos.SelectedIndex = -1;

            
            List<Perfil> familias = NegociosPermiso.ObtenerFamilias();

            CBFamilias.DataSource = new List<Perfil>(familias);
            CBFamilias.DisplayMember = "Nombre";
            CBFamilias.ValueMember = "Id";
            CBFamilias.SelectedIndex = -1;

            CBFamilias2.DataSource = new List<Perfil>(familias);
            CBFamilias2.DisplayMember = "Nombre";
            CBFamilias2.ValueMember = "Id";
            CBFamilias2.SelectedIndex = -1;

            CambioManual = true;
        }

        private void GestionDeFamilias_Load(object sender, EventArgs e)
        {
            LlenarCombobox();
            parent = this.MdiParent as frmInicio;
        }

        private void BTNCrearFamilia_Click(object sender, EventArgs e)
        {
         
            if (string.IsNullOrWhiteSpace(TXTFamilia.Text)) return;

            try
            {
                string nombreFamilia = TXTFamilia.Text.Trim();

          
                int idGenerado = NegociosPermiso.InsertarComponente(nombreFamilia, esFamilia: true, esPerfil: false);

              
                frmInicio parentUI = this.MdiParent as frmInicio;
                if (parentUI != null && parentUI.gestionDePerfiles != null)
                {
                    parentUI.gestionDePerfiles.LlenarCombosPerfiles();
                }

                
                TXTFamilia.Clear();
                LlenarCombobox(); 

                MessageBox.Show("Familia creada con éxito en la base de datos.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al crear la familia: " + ex.Message);
            }
        }

        private void BTNAgregarPermiso_Click(object sender, EventArgs e)
        {
            if (_familiaSeleccionada == null || CBPermisos.SelectedItem == null) return;

            Permiso permisoSeleccionado = (Permiso)CBPermisos.SelectedItem;

            if (_familiaSeleccionada.Hijos.Any(h => h.Id == permisoSeleccionado.Id))
            {
                MessageBox.Show("Este permiso ya se encuentra seleccionado en la familia actual.");
                return;
            }

            _familiaSeleccionada.AgregarHijo(permisoSeleccionado);
            MostrarEnTreeView();
        }

        private void CBFamilias_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!CambioManual || CBFamilias.SelectedItem == null) return;

            _familiaSeleccionada = (Familia)CBFamilias.SelectedItem;

            _familiaSeleccionada.VaciarHijos(); 
            NegociosPermiso.ArmarArbolComponente(_familiaSeleccionada);

            MostrarEnTreeView();
        }


        private void treeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {

            TreeViewHitTestInfo hitTest = treeView1.HitTest(e.Location);
            if (hitTest.Location != TreeViewHitTestLocations.PlusMinus)
            {
                if (e.Node.Parent != null)
                {
                    if (e.Node.Parent != treeView1.Nodes[0])
                    {
                        Perfil padreCompuesto = (Perfil)e.Node.Parent.Tag;
                        Perfil hijoAEliminar = (Perfil)e.Node.Tag;
                        padreCompuesto.EliminarHijo(hijoAEliminar.Id);
                    }
                    else
                    {
                        Perfil hijoAEliminar = (Perfil)e.Node.Tag;
                        _familiaSeleccionada.EliminarHijo(hijoAEliminar.Id);
                    }


                    NegociosPermiso.GuardarFamilia(_familiaSeleccionada, esPerfil: false);

                    MostrarEnTreeView();
                }
            } 





        }

        private void BTNAplicar_Click(object sender, EventArgs e)
        {
            if (_familiaSeleccionada == null) return;

            try
            {
                NegociosPermiso.GuardarFamilia(_familiaSeleccionada, esPerfil: false);

                frmInicio parentUI = this.MdiParent as frmInicio;
                if (parentUI != null)
                {
                    Usuario usuarioActual = Singleton.Instancia.Usuario;
                    if (usuarioActual != null && usuarioActual.Perfil != null)
                    {
                        
                        
                        NegociosPermiso.ArmarArbolComponente(usuarioActual.Perfil);
                        parentUI.AplicarPermisosUsuario(usuarioActual.Perfil.Hijos.ToList());
                    }
                }

                MessageBox.Show("Cambios guardados con éxito.");
                LlenarCombobox();
            }
            catch (Exception)
            {
                MessageBox.Show("Ocurrió un error al intentar guardar los cambios.");
            }
        }

        private void BTNEliminarFamilia_Click(object sender, EventArgs e)
        {
            
            if (CBFamilias.SelectedItem == null)
            {
                MessageBox.Show("Por favor, seleccione de la lista la familia que desea eliminar.");
                return;
            }

            
            Familia familiaAEliminar = (Familia)CBFamilias.SelectedItem;

            DialogResult resultado = MessageBox.Show($"¿Está seguro de que desea eliminar la familia '{familiaAEliminar.Nombre}'? Se perderán todas sus uniones jerárquicas.", "Confirmar Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (resultado == DialogResult.Yes)
            {
                try
                {
                      
                    NegociosPermiso.EliminarFamilia(familiaAEliminar.Id);

                    
                   

                    MessageBox.Show("Familia eliminada con éxito del sistema.");

                   
                    treeView1.Nodes.Clear();
                    _familiaSeleccionada = null;
                    LlenarCombobox();

                    
                    frmInicio parentUI = this.MdiParent as frmInicio;
                    if (parentUI != null)
                    {
                       
                        if (parentUI.gestionDePerfiles != null) parentUI.gestionDePerfiles.LlenarCombosPerfiles();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al eliminar la familia: " + ex.Message);
                }
            }
        }
        private void GestionDeFamilias_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
                LlenarCombobox();
         
        }

        private void BTNAgregarFamilia_Click(object sender, EventArgs e)
        {
            if (_familiaSeleccionada == null || CBFamilias2.SelectedItem == null) return;

            Familia subFamiliaOrigen = (Familia)CBFamilias2.SelectedItem;

            if (_familiaSeleccionada.Id == subFamiliaOrigen.Id)
            {
                MessageBox.Show("No se puede agregar una familia dentro de sí misma.");
                return;
            }

            if (_familiaSeleccionada.Hijos.Any(h => h.Id == subFamiliaOrigen.Id))
            {
                MessageBox.Show("Esta familia ya se encuentra ingresada.");
                return;
            }

            
            Familia copia = new Familia();
            copia.Id = subFamiliaOrigen.Id;
            copia.Nombre = subFamiliaOrigen.Nombre;

            
            NegociosPermiso.ArmarArbolComponente(copia);

            _familiaSeleccionada.AgregarHijo(copia);
            MostrarEnTreeView();
        }
        private void MostrarEnTreeView()
        {
            treeView1.Nodes.Clear();
            if (_familiaSeleccionada == null) return;

            TreeNode root = new TreeNode(_familiaSeleccionada.Nombre) { Tag = _familiaSeleccionada };
            treeView1.Nodes.Add(root);

            
            DibujarRamas(root, _familiaSeleccionada.Hijos);
            treeView1.ExpandAll();
        }
        private void DibujarRamas(TreeNode nodoVisual, IList<Perfil> hijos)
        {
            foreach (var hijo in hijos)
            {
                TreeNode nuevoNodo = new TreeNode(hijo.Nombre) { Tag = hijo };
                nodoVisual.Nodes.Add(nuevoNodo);

             
                if (hijo.Hijos != null && hijo.Hijos.Count > 0)
                    DibujarRamas(nuevoNodo, hijo.Hijos);
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeViewHitTestInfo hitTest = treeView1.HitTest(e.Location);
            if (hitTest.Location != TreeViewHitTestLocations.PlusMinus)
            {
                if (e.Node.Parent != null)
                {
                    if (e.Node.Parent != treeView1.Nodes[0])
                    {
                        Perfil padreCompuesto = (Perfil)e.Node.Parent.Tag;
                        Perfil hijoAEliminar = (Perfil)e.Node.Tag;
                        padreCompuesto.EliminarHijo(hijoAEliminar.Id);
                    }
                    else
                    {
                        Perfil hijoAEliminar = (Perfil)e.Node.Tag;
                        _familiaSeleccionada.EliminarHijo(hijoAEliminar.Id);
                    }

                    NegociosPermiso.GuardarFamilia(_familiaSeleccionada, esPerfil: false);

                    
                    frmInicio parentUI = this.MdiParent as frmInicio;
                    if (parentUI != null)
                    {
                        Usuario usuarioActual = Singleton.Instancia.Usuario;
                        if (usuarioActual != null && usuarioActual.Perfil != null)
                        {
                            NegociosPermiso.ArmarArbolComponente(usuarioActual.Perfil);
                            parentUI.AplicarPermisosUsuario(usuarioActual.Perfil.Hijos.ToList());
                        }
                    }

                    MostrarEnTreeView();
                }
            }
        }

        private void CBFamilias2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
