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

namespace CAFECCINO_INGENIERIA_DE_SOFTWARE
{
    public partial class frmBitacoraEventos : Form, IObserver
    {
        BLLBitacoraEventos bllevento = new BLLBitacoraEventos();
        Traductor traductor = new Traductor();
        public frmBitacoraEventos()
        {
            InitializeComponent();
            AyudaGrilla.OcultarDVH_389ME(dataGridView1);
        }

        public void ActualizarIdioma(string idioma)
        {
            traductor.TraducirFormulario(this, int.Parse(idioma));
        }

        private void frmBitacoraEventos_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = bllevento.ObtenerEventos();
            
            cmbModulo.Items.AddRange(new object[] { "Todos", "Login", "Ventas", "Clientes", "Productos", "Compras",
                                                    "Usuarios", "Permisos", "Idiomas", "Integridad" });
            cmbEvento.Items.AddRange(new object[] { "Todos",
                "Inicio de sesión exitoso", "Intento de inicio de sesión fallido", "Intento de inicio de sesión con usuario bloqueado", "Bloqueo de usuario", "Desbloqueo de usuario", "Cierre de sesión",
                "Venta registrada", "Venta rechazada", "Error al registrar venta",
                "Alta de cliente", "Modificación de cliente", "Bloqueo de cliente", "Desbloqueo de cliente", "Serialización de clientes", "Deserialización de clientes",
                "Alta de producto", "Modificación de producto", "Baja de producto",
                "Alta de usuario", "Modificación de usuario", "Baja de usuario", "Cambio de contraseña", "Restauración de usuario",
                "Alta de perfil", "Alta de familia", "Alta de permiso", "Modificación de permisos", "Baja de perfil o familia",
                "Alta de idioma", "Traducciones guardadas", "Cambio de idioma",
                "Recálculo de dígitos verificadores", "Restauración de backup",
                "Alta de insumo", "Modificación de insumo", "Baja de insumo", "Modificación de receta",
                "Pre-registro de proveedor", "Modificación de proveedor", "Registro de proveedor",
                "Solicitud de cotización", "Carga de cotización", "Generación de orden de compra", "Recepción de insumos" });

            cmbEvento.SelectedIndex = 0;
            cmbModulo.SelectedIndex = 0;

            cmbCriticidad.Items.Add("Todas");
            cmbCriticidad.Items.Add("Alta");
            cmbCriticidad.Items.Add("Media");
            cmbCriticidad.Items.Add("Baja");

            cmbCriticidad.SelectedIndex = 0;

            // No hay eventos en el futuro
            dateTimePicker1.MaxDate = DateTime.Today.AddDays(1).AddSeconds(-1);
        }

        private void Buscar_Click(object sender, EventArgs e)
        {
            string modulo = cmbModulo.Text;
            string criticidad = cmbCriticidad.Text;
            string fecha = dateTimePicker1.Value.ToString("yyyy-MM-dd");
          string evento = cmbEvento.Text;

            if (modulo == "Todos")
                modulo = "";

            if (criticidad == "Todas")
                criticidad = "";

            if (evento == "Todos")
                evento = "";

            BLLBitacoraEventos bll = new BLLBitacoraEventos();

            dataGridView1.DataSource = bll.FiltrarEventos(modulo, criticidad,  evento, fecha);
        }

        private void frmBitacoraEventos_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();

            }
        }

        private void frmBitacoraEventos_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
            
                traductor.TraducirFormulario(this, GestorIdiomas.Instancia.IdiomaActual);
            }
        }
    }
}
