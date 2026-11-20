using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace DAL
{
    public class DALAuditoriaCambios
    {
        private string cadenaConexion = Conexion.cadena;

        public DataTable ObtenerCambios()
        {
            using (SqlConnection connection = new SqlConnection(cadenaConexion))
            using (SqlCommand command = new SqlCommand(
                "SELECT * FROM AuditoriaCambios ORDER BY FechaHora DESC", connection))
            {
                SqlDataAdapter da = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public AuditoriaCambios ObtenerUltimoPorDni(int dni)
        {
            using (SqlConnection connection = new SqlConnection(cadenaConexion))
            using (SqlCommand command = new SqlCommand(
                "SELECT TOP 1 * FROM AuditoriaCambios WHERE DNIAfectado = @dni ORDER BY FechaHora DESC, IdBitacora DESC", connection))
            {
                command.Parameters.AddWithValue("@dni", dni);
                connection.Open();
                using (SqlDataReader datareader = command.ExecuteReader())
                {
                    if (!datareader.Read()) return null;
                    return new AuditoriaCambios
                    {
                        IdBitacora = datareader.GetInt32(0),
                        FechaHora = datareader.GetDateTime(1),
                        UsuarioEditor = datareader.GetString(2),
                        Operacion = datareader.GetString(3),
                        DNIAfectado = datareader.GetInt32(4),
                        DatosAnteriores = datareader["DatosAnteriores"] == DBNull.Value ? null : datareader.GetString(5),
                        DatosNuevos = datareader["DatosNuevos"] == DBNull.Value ? null : datareader.GetString(6)
                    };
                }
            }
        }

        public AuditoriaCambios ObtenerPorId(int idBitacora)
        {
            using (SqlConnection connection = new SqlConnection(cadenaConexion))
            using (SqlCommand command = new SqlCommand(
                "SELECT * FROM AuditoriaCambios WHERE IdBitacora = @id", connection))
            {
                command.Parameters.AddWithValue("@id", idBitacora);
                connection.Open();
                using (SqlDataReader datareader = command.ExecuteReader())
                {
                    if (!datareader.Read()) return null;
                    return new AuditoriaCambios
                    {
                        IdBitacora = datareader.GetInt32(0),
                        FechaHora = datareader.GetDateTime(1),
                        UsuarioEditor = datareader.GetString(2),
                        Operacion = datareader.GetString(3),
                        DNIAfectado = datareader.GetInt32(4),
                        DatosAnteriores = datareader["DatosAnteriores"] == DBNull.Value ? null : datareader.GetString(5),
                        DatosNuevos = datareader["DatosNuevos"] == DBNull.Value ? null : datareader.GetString(6)
                    };
                }
            }
        }
    }
}
