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
        BLLUsuario bllUsuario = new BLLUsuario();
        BLLPermiso bllPermiso = new BLLPermiso();
        Traductor traductor = new Traductor();
       
        public GestionUsuarios()
        {
            InitializeComponent();
            AyudaGrilla.OcultarDVH_389ME(dataGridView1);

            textBox1.KeyPress += Validacion.SoloNumeros_389ME;
            textBox7.KeyPress += Validacion.SoloNumeros_389ME;
            textBox5.KeyPress += Validacion.SoloLetras_389ME;
            textBox6.KeyPress += Validacion.SoloLetras_389ME;
        }
        public void ActualizarIdioma(string idioma)
        {
            

            traductor.TraducirFormulario(this, int.Parse(idioma));
        }
        private void Agregar_Click(object sender, EventArgs e)
        {
            Usuario usuario = LeerUsuario_389ME();
            if (usuario == null) return;

            try
            {
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

        // Arma el usuario con los datos de la pantalla. Devuelve null si el DNI o el telefono no son numeros
        // o si no se eligio un rol; el resto de los datos los valida BLLUsuario.
        private Usuario LeerUsuario_389ME()
        {
            if (!int.TryParse(textBox1.Text.Trim(), out int dni_389ME))
            {
                MessageBox.Show("El DNI debe tener 8 números.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }

            long telefono_389ME = 0;
            if (textBox7.Text.Trim().Length > 0 && !long.TryParse(textBox7.Text.Trim(), out telefono_389ME))
            {
                MessageBox.Show("El teléfono debe tener solo números.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }

            if (!(CBRoles.SelectedItem is Perfil))
            {
                MessageBox.Show("Seleccione un rol.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }

            return new Usuario
            {
                DNI = dni_389ME,
                NombreUsuario = textBox2.Text.Trim(),
                Contraseña = textBox3.Text,
                Nombre = textBox5.Text.Trim(),
                Apellido = textBox6.Text.Trim(),
                Mail = textBox4.Text.Trim(),
                NumTelefono = telefono_389ME,
                Rol = CBRoles.Text
            };
        }

        private void LimpiarCampos()
            {
            textBox1.Clear(); textBox2.Clear(); textBox3.Clear();
            textBox4.Clear(); textBox5.Clear(); textBox6.Clear(); textBox7.Clear();
            CBRoles.SelectedIndex = -1;
            }
         


        private void Modificar_Click(object sender, EventArgs e)
        {
            Usuario usuario = LeerUsuario_389ME();
            if (usuario == null) return;

            try
            {
                // Si la contraseña se deja vacia, BLLUsuario mantiene la que ya tenia
                bllUsuario.ModificarUsuario(usuario, ((Perfil)CBRoles.SelectedItem).Id);
                bllUsuario.SincronizarDigitos();

                MessageBox.Show("Usuario modificado correctamente.");

                LimpiarCampos();
                actualizarGrilla();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al modificar el usuario: " + ex.Message);
            }
        }

        private void Eliminar_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(textBox1.Text.Trim(), out int dni_389ME))
            {
                MessageBox.Show("Seleccione un usuario de la grilla.");
                return;
            }

            if (MessageBox.Show("¿Desea eliminar el usuario?",
                "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                bllUsuario.EliminarUsuario(dni_389ME);
                bllUsuario.SincronizarDigitos();

                MessageBox.Show("Usuario eliminado correctamente.");

                LimpiarCampos();
                actualizarGrilla();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar el usuario: " + ex.Message);
            }
        }
        private void btnBloquear_389ME_Click(object sender, EventArgs e)
        {
            CambiarBloqueo_389ME(true);
        }

        private void btnDesbloquear_389ME_Click(object sender, EventArgs e)
        {
            CambiarBloqueo_389ME(false);
        }

        private void CambiarBloqueo_389ME(bool bloquear_389ME)
        {
            if (!int.TryParse(textBox1.Text.Trim(), out int dni_389ME))
            {
                MessageBox.Show("Seleccione un usuario de la grilla.");
                return;
            }

            string accion_389ME = bloquear_389ME ? "bloquear" : "desbloquear";
            if (MessageBox.Show($"¿Desea {accion_389ME} el usuario?",
                "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                if (bloquear_389ME)
                    bllUsuario.Bloquear_389ME(dni_389ME);
                else
                    bllUsuario.Desbloquear_389ME(dni_389ME);

                MessageBox.Show(bloquear_389ME ? "Usuario bloqueado correctamente." : "Usuario desbloqueado correctamente.");

                LimpiarCampos();
                actualizarGrilla();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al {accion_389ME} el usuario: " + ex.Message);
            }
        }

        private void actualizarGrilla()
        {
            dataGridView1.DataSource = bllUsuario.ListarUsuarios();

            // La contraseña se guarda como hash: no tiene sentido mostrarla
            if (dataGridView1.Columns.Contains("Contraseña"))
                dataGridView1.Columns["Contraseña"].Visible = false;
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
            if (dataGridView1.CurrentRow == null) return;

            textBox1.Text = dataGridView1.CurrentRow.Cells["DNI"].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells["Username"].Value.ToString();
            // La contraseña no se carga: si se deja vacia, al modificar se mantiene la actual
            textBox3.Clear();
            textBox4.Text = dataGridView1.CurrentRow.Cells["EMail"].Value.ToString();
            textBox5.Text = dataGridView1.CurrentRow.Cells["Nombre"].Value.ToString();
            textBox6.Text = dataGridView1.CurrentRow.Cells["Apellido"].Value.ToString();
            string telefono_389ME = dataGridView1.CurrentRow.Cells["NumTelefono"].Value.ToString();
            textBox7.Text = telefono_389ME == "0" ? "" : telefono_389ME;
            CBRoles.Text = dataGridView1.CurrentRow.Cells["Rol"].Value.ToString();
        }
        private void LlenarComboRoles()
        {
            
            CBRoles.DataSource = bllPermiso.ObtenerPerfiles();
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
                LimpiarCampos();
                traductor.TraducirFormulario(this, GestorIdiomas.Instancia.IdiomaActual);
            }
        }
    }
}