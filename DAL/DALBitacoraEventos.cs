using BE;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DAL
{
    public class DALBitacoraEventos
    {
        private string conexion = @"Data Source=localhost;Initial Catalog=Cafeccino;Integrated Security=True";
        public void RegistrarEvento(BitacoraEventos evento)
        {
            SqlConnection cn = new SqlConnection(conexion);

            string query = @"INSERT INTO BitacoraEventos
                        (NombreUsuario, Fecha, Evento, Modulo, Criticidad)
                        VALUES
                        (@NombreUsuario, @Fecha, @Evento, @Modulo, @Criticidad)";

            SqlCommand cmd = new SqlCommand(query, cn);

            cmd.Parameters.AddWithValue("@NombreUsuario", evento.NombreUsuario);
            cmd.Parameters.AddWithValue("@Fecha", evento.Fecha);
            cmd.Parameters.AddWithValue("@Evento", evento.Evento);
            cmd.Parameters.AddWithValue("@Modulo", evento.Modulo);
            cmd.Parameters.AddWithValue("@Criticidad", evento.Criticidad);

            cn.Open();
            cmd.ExecuteNonQuery();
            cn.Close();
        }
        public DataTable ListarEventos()
        {
            SqlConnection cn = new SqlConnection(conexion);

            string query = "SELECT * FROM BitacoraEventos";

            SqlDataAdapter da = new SqlDataAdapter(query, cn);

            DataTable dt = new DataTable();

            da.Fill(dt);

            return dt;
        }
        public DataTable FiltrarEventos(string modulo, string criticidad,string evento, string fecha)
        {
            SqlConnection cn = new SqlConnection(conexion);

            string query = @"SELECT *
                     FROM BitacoraEventos
                     WHERE (@Modulo = '' OR Modulo = @Modulo)
                     AND (@Criticidad = '' OR Criticidad = @Criticidad)  AND (@Fecha = '' OR CONVERT(date, Fecha) = @Fecha)     
                      AND (@Evento = '' OR Evento = @Evento)    
                     ORDER BY Fecha DESC";

            SqlCommand cmd = new SqlCommand(query, cn);

            cmd.Parameters.AddWithValue("@Evento", evento);
            cmd.Parameters.AddWithValue("@Fecha", fecha);
            cmd.Parameters.AddWithValue("@Modulo", modulo);
            cmd.Parameters.AddWithValue("@Criticidad", criticidad);
       

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();

            da.Fill(dt);

            return dt;
        }
    }
}
