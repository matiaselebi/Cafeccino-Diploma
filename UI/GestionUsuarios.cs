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
    public partial class GestionUsuarios : Form, IObserver
    {
        private BLLBitacoraUsuario _bllBitacora = new BLLBitacoraUsuario();
        BLLUsuario bllUsuario = new BLLUsuario();
        BLLPermiso _permisoBLL = new BLLPermiso();
        Traductor traductor = new Traductor();
       
        public GestionUsuarios()
        {
            InitializeComponent();
        }
        public void ActualizarIdioma(string idioma)
        {
            

            traductor.TraducirFormulario(this, int.Parse(idioma));
        }
        private void Agregar_Click(object sender, EventArgs e)
        {
            
            if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox2.Text) ||
                string.IsNullOrWhiteSpace(textBox3.Text) || CBRoles.SelectedItem == null)
            {
                MessageBox.Show("Por favor, complete los campos obligatorios (DNI, Usuario, Contraseña y Rol).", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

           
            if (!int.TryParse(textBox1.Text.Trim(), out int dni))
            {
                MessageBox.Show("El DNI debe ser un número válido.", "Error de formato", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int.TryParse(textBox7.Text.Trim(), out int telefono); 

            try
            {
                
                Usuario usuario = new Usuario();
                usuario.DNI = dni;
                usuario.NombreUsuario = textBox2.Text.Trim();
                usuario.Contraseña = textBox3.Text.Trim(); 
                usuario.Nombre = textBox5.Text.Trim();
                usuario.Apellido = textBox6.Text.Trim();
                usuario.Mail = textBox4.Text.Trim();
                usuario.NumTelefono = telefono;
                usuario.Rol = CBRoles.Text;


                Perfil rolSeleccionado = (Perfil)CBRoles.SelectedItem;

                
                bllUsuario.RegistrarUsuario(usuario, rolSeleccionado.Id);
                bllUsuario.SincronizarDigitos();
               
                MessageBox.Show("Usuario agregado y rol asignado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                
                LimpiarCampos();
                actualizarGrilla();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al registrar el usuario: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LimpiarCampos()
            {
            textBox1.Clear(); textBox2.Clear(); textBox3.Clear();
            textBox4.Clear(); textBox5.Clear(); textBox6.Clear(); textBox7.Clear();
            CBRoles.SelectedIndex = -1;
            }
         


        private void Modificar_Click(object sender, EventArgs e)
        {
            Usuario usuario = new Usuario();
            Usuario anterior = bllUsuario.ObtenerPorDNI(Convert.ToInt32(textBox1.Text));

            usuario.DNI = Convert.ToInt32(textBox1.Text);
            usuario.NombreUsuario = textBox2.Text;
            usuario.Contraseña = textBox3.Text;
            usuario.Nombre = textBox5.Text;
            usuario.Apellido = textBox6.Text;
            usuario.Mail = textBox4.Text;
            usuario.NumTelefono = Convert.ToInt32(textBox7.Text);
            usuario.Rol = CBRoles.Text;

            bllUsuario.ModificarUsuario(usuario);
            bllUsuario.SincronizarDigitos();
            _bllBitacora.RegistrarModificacion(anterior, usuario, Singleton.Instancia.Usuario.NombreUsuario);

            MessageBox.Show("Usuario modificado correctamente.");

            actualizarGrilla();
        }

        private void Eliminar_Click(object sender, EventArgs e)
        {
            Usuario eliminado = bllUsuario.ObtenerPorDNI(Convert.ToInt32(textBox1.Text));

            if (MessageBox.Show("¿Desea eliminar el usuario?",
                "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                bllUsuario.EliminarUsuario(eliminado.DNI);
                bllUsuario.SincronizarDigitos();

                
               

                MessageBox.Show("Usuario eliminado correctamente.");
                actualizarGrilla();
            }
        }
        private void actualizarGrilla()
        {
            dataGridView1.DataSource = bllUsuario.ListarUsuarios();

        }

        private void GestionUsuarios_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
          

        }

        private void GestionUsuarios_Load(object sender, EventArgs e)
        {
            try
            {
                LlenarComboRoles();
                actualizarGrilla();
                
            }
            catch (Exception)
            {
                MessageBox.Show("Error al inicializar la pantalla");
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells["DNI"].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells["Username"].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells["Contraseña"].Value.ToString();
            textBox4.Text = dataGridView1.CurrentRow.Cells["EMail"].Value.ToString();
            textBox5.Text = dataGridView1.CurrentRow.Cells["Nombre"].Value.ToString();
            textBox6.Text = dataGridView1.CurrentRow.Cells["Apellido"].Value.ToString();
            textBox7.Text = dataGridView1.CurrentRow.Cells["NumTelefono"].Value.ToString();
            CBRoles.Text = dataGridView1.CurrentRow.Cells["Rol"].Value.ToString();
        }
        private void LlenarComboRoles()
        {
            
            CBRoles.DataSource = _permisoBLL.ObtenerPerfiles();
            CBRoles.DisplayMember = "Nombre";
            CBRoles.ValueMember = "Id";
            CBRoles.SelectedIndex = -1;
        }

      

        private void v(object sender, EventArgs e)
        {

        }

        private void GestionUsuarios_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                
                traductor.TraducirFormulario(this, Singleton.Instancia.IdiomaActual);
            }
        }
    }
}