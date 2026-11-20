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
        private string conexion = Conexion.cadena;
        public void RegistrarEvento(BitacoraEventos evento)
        {
            SqlConnection connection = new SqlConnection(conexion);

            string query = @"INSERT INTO BitacoraEventos
                        (NombreUsuario, Fecha, Evento, Modulo, Criticidad, Detalle)
                        VALUES
                        (@NombreUsuario, @Fecha, @Evento, @Modulo, @Criticidad, @Detalle)";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@NombreUsuario", evento.NombreUsuario);
            command.Parameters.AddWithValue("@Fecha", evento.Fecha);
            command.Parameters.AddWithValue("@Evento", evento.Evento);
            command.Parameters.AddWithValue("@Modulo", evento.Modulo);
            command.Parameters.AddWithValue("@Criticidad", evento.Criticidad);
            command.Parameters.AddWithValue("@Detalle", (object)evento.Detalle_389ME ?? DBNull.Value);

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }
        // Desbloqueos hechos desde la base que dejo anotados el trigger TR_Usuario_Desbloqueo
        public DataTable ObtenerDesbloqueos_389ME()
        {
            using (SqlConnection connection_389ME = new SqlConnection(conexion))
            using (SqlDataAdapter adaptador_389ME = new SqlDataAdapter(
                "SELECT * FROM DesbloqueoUsuario ORDER BY CodDesbloqueo_389ME", connection_389ME))
            {
                DataTable tabla_389ME = new DataTable();
                adaptador_389ME.Fill(tabla_389ME);
                return tabla_389ME;
            }
        }

        public void BorrarDesbloqueo_389ME(int codDesbloqueo_389ME)
        {
            using (SqlConnection connection_389ME = new SqlConnection(conexion))
            using (SqlCommand command_389ME = new SqlCommand(
                "DELETE FROM DesbloqueoUsuario WHERE CodDesbloqueo_389ME = @cod", connection_389ME))
            {
                command_389ME.Parameters.AddWithValue("@cod", codDesbloqueo_389ME);
                connection_389ME.Open();
                command_389ME.ExecuteNonQuery();
            }
        }

        public DataTable ListarEventos()
        {
            SqlConnection connection = new SqlConnection(conexion);

            string query = "SELECT * FROM BitacoraEventos ORDER BY Fecha DESC";

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
