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
using BE;
using BLL;
using SERVICIOS;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace CAFECCINO_INGENIERIA_DE_SOFTWARE
{
    public partial class GestionDePerfiles : Form , IObserver
    {

        private BLLPermiso _permisoBLL = new BLLPermiso();
        private Familia _perfilSeleccionado; 
        Traductor traductor = new Traductor();
        frmInicio parent;
        private bool CambioManual = true;
        public GestionDePerfiles()
        {
            InitializeComponent();
        }
        public void ActualizarIdioma(string idioma)
        {
            traductor.TraducirFormulario(this, int.Parse(idioma));
        }
        private void GestionDePerfiles_Load(object sender, EventArgs e)
        {
            parent = this.MdiParent as frmInicio;
            LlenarCombosPerfiles();
        }

        private void GestionDePerfiles_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }

            parent.gestionDeFamilias.Hide();
            treePerfil.Nodes.Clear();
            TXTPerfil.Text = "";
            parent.gestionDeFamilias.TXTFamilia.Text = "";
        }

        private void BTNConfigurarFamilias_Click(object sender, EventArgs e)
        {
            frmInicio parentUI = this.MdiParent as frmInicio;
            if (parentUI != null)
            {
                parentUI.gestionDeFamilias.MdiParent = null; 
                parentUI.gestionDeFamilias.Show();
                LlenarCombosPerfiles();
            }
        }
        public void LlenarCombosPerfiles()
        {
            CambioManual = false;

            
            List<Perfil> perfiles = _permisoBLL.ObtenerPerfiles();
            cmbPerfiles.DataSource = perfiles;
            cmbPerfiles.DisplayMember = "Nombre";
            cmbPerfiles.ValueMember = "Id";
            cmbPerfiles.SelectedIndex = -1;

           
            List<Perfil> familias = _permisoBLL.ObtenerFamilias();
            cmbFamiliasAsociar.DataSource = familias;
            cmbFamiliasAsociar.DisplayMember = "Nombre";
            cmbFamiliasAsociar.ValueMember = "Id";
            cmbFamiliasAsociar.SelectedIndex = -1;


            List<Perfil> permisos = _permisoBLL.ObtenerPermisosYPerfiles();
            cmbPermisos.DataSource = permisos;
            cmbPermisos.DisplayMember = "Nombre";
            cmbPermisos.ValueMember = "Id";
            cmbPermisos.SelectedIndex = -1;

            CambioManual = true;
        }

        private void BTNCrearPerfil_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TXTPerfil.Text)) return;

            try
            {
                string nombrePerfil = TXTPerfil.Text.Trim();

                
                
                int idGenerado = _permisoBLL.InsertarComponente(nombrePerfil, esFamilia: true, esPerfil: true);

                
                frmInicio parentUI = this.MdiParent as frmInicio;
                if (parentUI != null && parentUI.gestionDeFamilias != null)
                {
                    parentUI.gestionDeFamilias.LlenarCombobox();
                }

               
                TXTPerfil.Clear();
                LlenarCombosPerfiles(); 

                MessageBox.Show("Perfil creado con éxito en la base de datos.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al crear el perfil: " + ex.Message);
            }
        }

        private void BTNEliminarPerfil_Click(object sender, EventArgs e)
        {
            
            if (cmbPerfiles.SelectedItem == null)
            {
                MessageBox.Show("Por favor, seleccione de la lista el perfil que desea eliminar.");
                return;
            }

            Familia perfilAEliminar = (Familia)cmbPerfiles.SelectedItem;

            DialogResult resultado = MessageBox.Show($"¿Está seguro de que desea eliminar el perfil '{perfilAEliminar.Nombre}'? Se borrarán todas sus asociaciones.", "Confirmar Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (resultado == DialogResult.Yes)
            {
                try
                {
                   
                    _permisoBLL.EliminarComponente(perfilAEliminar.Id);

                 

                    MessageBox.Show("Perfil eliminado correctamente.");

                 
                    treePerfil.Nodes.Clear();
                    _perfilSeleccionado = null;
                    LlenarCombosPerfiles();

                    frmInicio parentUI = this.MdiParent as frmInicio;
                    if (parentUI != null)
                    {
                        
                        parentUI.gestionDeFamilias.LlenarCombobox();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al eliminar el perfil: " + ex.Message);
                }
            }
        }

        private void treeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {

            TreeViewHitTestInfo hitTest = treePerfil.HitTest(e.Location);

            if (hitTest.Location != TreeViewHitTestLocations.PlusMinus)
            {
                if (e.Node.Parent != null)
                {
                    if (e.Node.Parent != treePerfil.Nodes[0])
                    {
                        
                        Perfil padreCompuesto = (Perfil)e.Node.Parent.Tag;
                        Perfil hijoAEliminar = (Perfil)e.Node.Tag;
                        padreCompuesto.EliminarHijo(hijoAEliminar.Id);

                        
                        if (padreCompuesto is Familia)
                            _permisoBLL.GuardarFamilia((Familia)padreCompuesto, esPerfil: false);
                    }
                    else
                    {
                    
                        Perfil hijoAEliminar = (Perfil)e.Node.Tag;
                        _perfilSeleccionado.EliminarHijo(hijoAEliminar.Id);

                     
                        _permisoBLL.GuardarFamilia(_perfilSeleccionado, esPerfil: true);
                    }

                  
                    frmInicio parentUI = this.MdiParent as frmInicio;
                    if (parentUI != null)
                    {
                        Usuario usuarioActual = Singleton.Instancia.Usuario;
                        if (usuarioActual != null && usuarioActual.Perfil != null
                            && usuarioActual.Perfil.Id == _perfilSeleccionado.Id)
                        {
                            _permisoBLL.ArmarArbolComponente(usuarioActual.Perfil);
                            parentUI.AplicarPermisosUsuario(usuarioActual.Perfil.Hijos.ToList());
                        }
                    }

                    MostrarEnTreeView();
                }
            }
        }

        private void BTNAgregarFamilia_Click(object sender, EventArgs e)
        {
            if (_perfilSeleccionado == null || cmbFamiliasAsociar.SelectedItem == null) return;

            Familia subFamiliaOrigen = (Familia)cmbFamiliasAsociar.SelectedItem;

            if (subFamiliaOrigen.Id == _perfilSeleccionado.Id)
            {
                MessageBox.Show("No se puede agregar un perfil dentro de sí mismo.");
                return;
            }

            if (_perfilSeleccionado.Hijos.Any(h => h.Id == subFamiliaOrigen.Id))
            {
                MessageBox.Show("Esta familia ya se encuentra ingresada.");
                return;
            }

         
            Familia copia = new Familia();
            copia.Id = subFamiliaOrigen.Id;
            copia.Nombre = subFamiliaOrigen.Nombre;

            
            _permisoBLL.ArmarArbolComponente(copia);

            _perfilSeleccionado.AgregarHijo(copia);
            MostrarEnTreeView();
        }

        private void BTNAgregarPermiso_Click(object sender, EventArgs e)
        {
            if (_perfilSeleccionado == null || cmbPermisos.SelectedItem == null) return;

            Perfil permisoSeleccionado = (Perfil)cmbPermisos.SelectedItem;

          
            if (permisoSeleccionado.Id == _perfilSeleccionado.Id)
            {
                MessageBox.Show("No se puede agregar un perfil dentro de sí mismo.");
                return;
            }

            if (_perfilSeleccionado.Hijos.Any(h => h.Id == permisoSeleccionado.Id))
            {
                MessageBox.Show("Este permiso ya se encuentra agregado.");
                return;
            }

            if (permisoSeleccionado is Familia)
            {
                Familia copia = new Familia();
                copia.Id = permisoSeleccionado.Id;
                copia.Nombre = permisoSeleccionado.Nombre;
                _permisoBLL.ArmarArbolComponente(copia);
                _perfilSeleccionado.AgregarHijo(copia);
            }
            else
            {
                _perfilSeleccionado.AgregarHijo(permisoSeleccionado);
            }

            MostrarEnTreeView();
        }

        private void BTNAplicar_Click(object sender, EventArgs e)
        {
            if (_perfilSeleccionado == null) return;

            try
            {
                _permisoBLL.GuardarFamilia(_perfilSeleccionado, esPerfil: true);

                frmInicio parentUI = this.MdiParent as frmInicio;
                if (parentUI != null)
                {
                    
                    Usuario usuarioActual = Singleton.Instancia.Usuario;
                    if (usuarioActual != null && usuarioActual.Perfil != null
                        && usuarioActual.Perfil.Id == _perfilSeleccionado.Id)
                    {
                        
                        _permisoBLL.ArmarArbolComponente(usuarioActual.Perfil);

                        
                        parentUI.AplicarPermisosUsuario(usuarioActual.Perfil.Hijos.ToList());
                    }
                }

                MessageBox.Show("Cambios aplicados con éxito.");
                LlenarCombosPerfiles();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al aplicar los cambios: " + ex.Message);
            }
        }

        private void CBPerfiles_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (!CambioManual) return;
            if (cmbPerfiles.SelectedItem == null) return;

            try
            {
                _perfilSeleccionado = (Familia)cmbPerfiles.SelectedItem;

                _perfilSeleccionado.EliminarHijo(((Perfil)cmbPerfiles.SelectedItem).Id); 
                _permisoBLL.ArmarArbolComponente(_perfilSeleccionado);

                MostrarEnTreeView();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar el árbol del perfil: " + ex.Message);
            }
        }
       
        private void MostrarEnTreeView()
        {
            treePerfil.Nodes.Clear();
            if (_perfilSeleccionado == null) return;

            TreeNode root = new TreeNode(_perfilSeleccionado.Nombre) { Tag = _perfilSeleccionado };
            treePerfil.Nodes.Add(root);

            
            DibujarRamas(root, _perfilSeleccionado.Hijos);
            treePerfil.ExpandAll();
        }
        private void GestionDePerfiles_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
                LlenarCombosPerfiles();
            
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
    }
}
