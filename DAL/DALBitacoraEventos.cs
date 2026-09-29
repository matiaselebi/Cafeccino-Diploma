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
            SqlConnection connection = new SqlConnection(conexion);

            string query = @"INSERT INTO BitacoraEventos
                        (NombreUsuario, Fecha, Evento, Modulo, Criticidad)
                        VALUES
                        (@NombreUsuario, @Fecha, @Evento, @Modulo, @Criticidad)";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@NombreUsuario", evento.NombreUsuario);
            command.Parameters.AddWithValue("@Fecha", evento.Fecha);
            command.Parameters.AddWithValue("@Evento", evento.Evento);
            command.Parameters.AddWithValue("@Modulo", evento.Modulo);
            command.Parameters.AddWithValue("@Criticidad", evento.Criticidad);

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }
        public DataTable ListarEventos()
        {
            SqlConnection connection = new SqlConnection(conexion);

            string query = "SELECT * FROM BitacoraEventos";

            SqlDataAdapter da = new SqlDataAdapter(query, connection);

            DataTable dt = new DataTable();

            da.Fill(dt);

            return dt;
        }
        public DataTable FiltrarEventos(string modulo, string criticidad,string evento, string fecha)
        {
            SqlConnection connection = new SqlConnection(conexion);

            string query = @"SELECT *
                     FROM BitacoraEventos
                     WHERE (@Modulo = '' OR Modulo = @Modulo)
                     AND (@Criticidad = '' OR Criticidad = @Criticidad)  AND (@Fecha = '' OR CONVERT(date, Fecha) = @Fecha)     
                      AND (@Evento = '' OR Evento = @Evento)    
                     ORDER BY Fecha DESC";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@Evento", evento);
            command.Parameters.AddWithValue("@Fecha", fecha);
            command.Parameters.AddWithValue("@Modulo", modulo);
            command.Parameters.AddWithValue("@Criticidad", criticidad);
       

            SqlDataAdapter da = new SqlDataAdapter(command);

            DataTable dt = new DataTable();

            da.Fill(dt);

            return dt;
        }
    }
}
