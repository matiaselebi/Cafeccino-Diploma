using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using BE;

namespace DAL
{
    public class DALCliente
    {
        private string cadenaConexion_389ME = Conexion.cadena;

        public List<Cliente> Listar_389ME()
        {
            List<Cliente> lista_389ME = new List<Cliente>();
            string query_389ME = @"SELECT DNI_389ME, Nombre_389ME, Apellido_389ME, Direccion_389ME, Mail_389ME, NumTelefono_389ME, Bloqueado_389ME, DVH_389ME
                              FROM Cliente ORDER BY Apellido_389ME, Nombre_389ME";

            using (SqlConnection connection_389ME = new SqlConnection(cadenaConexion_389ME))
            using (SqlCommand command_389ME = new SqlCommand(query_389ME, connection_389ME))
            {
                connection_389ME.Open();
                using (SqlDataReader datareader_389ME = command_389ME.ExecuteReader())
                {
                    while (datareader_389ME.Read())
                        lista_389ME.Add(Mapear_389ME(datareader_389ME));
                }
            }
            return lista_389ME;
        }

        public Cliente ObtenerPorDNI_389ME(int dni_389ME)
        {
            string query_389ME = @"SELECT DNI_389ME, Nombre_389ME, Apellido_389ME, Direccion_389ME, Mail_389ME, NumTelefono_389ME, Bloqueado_389ME, DVH_389ME
                              FROM Cliente WHERE DNI_389ME = @dni";

            using (SqlConnection connection_389ME = new SqlConnection(cadenaConexion_389ME))
            using (SqlCommand command_389ME = new SqlCommand(query_389ME, connection_389ME))
            {
                command_389ME.Parameters.AddWithValue("@dni", dni_389ME);
                connection_389ME.Open();
                using (SqlDataReader datareader_389ME = command_389ME.ExecuteReader())
                {
                    if (!datareader_389ME.Read()) return null;
                    return Mapear_389ME(datareader_389ME);
                }
            }
        }

        public void Insertar_389ME(Cliente cliente_389ME)
        {
            string query_389ME = @"INSERT INTO Cliente (DNI_389ME, Nombre_389ME, Apellido_389ME, Direccion_389ME, Mail_389ME, NumTelefono_389ME)
                              VALUES (@dni, @nombre, @apellido, @direccion, @mail, @tel)";

            using (SqlConnection connection_389ME = new SqlConnection(cadenaConexion_389ME))
            using (SqlCommand command_389ME = new SqlCommand(query_389ME, connection_389ME))
            {
                command_389ME.Parameters.AddWithValue("@dni", cliente_389ME.DNI_389ME);
                command_389ME.Parameters.AddWithValue("@nombre", cliente_389ME.Nombre_389ME);
                command_389ME.Parameters.AddWithValue("@apellido", cliente_389ME.Apellido_389ME);
                command_389ME.Parameters.AddWithValue("@direccion", (object)cliente_389ME.Direccion_389ME ?? DBNull.Value);
                command_389ME.Parameters.AddWithValue("@mail", (object)cliente_389ME.Mail_389ME ?? DBNull.Value);
                command_389ME.Parameters.AddWithValue("@tel", cliente_389ME.NumTelefono_389ME);
                connection_389ME.Open();
                command_389ME.ExecuteNonQuery();
            }
        }

        public void Modificar_389ME(Cliente cliente_389ME)
        {
            string query_389ME = @"UPDATE Cliente SET Nombre_389ME = @nombre, Apellido_389ME = @apellido, Direccion_389ME = @direccion,
                              Mail_389ME = @mail, NumTelefono_389ME = @tel WHERE DNI_389ME = @dni";

            using (SqlConnection connection_389ME = new SqlConnection(cadenaConexion_389ME))
            using (SqlCommand command_389ME = new SqlCommand(query_389ME, connection_389ME))
            {
                command_389ME.Parameters.AddWithValue("@dni", cliente_389ME.DNI_389ME);
                command_389ME.Parameters.AddWithValue("@nombre", cliente_389ME.Nombre_389ME);
                command_389ME.Parameters.AddWithValue("@apellido", cliente_389ME.Apellido_389ME);
                command_389ME.Parameters.AddWithValue("@direccion", (object)cliente_389ME.Direccion_389ME ?? DBNull.Value);
                command_389ME.Parameters.AddWithValue("@mail", (object)cliente_389ME.Mail_389ME ?? DBNull.Value);
                command_389ME.Parameters.AddWithValue("@tel", cliente_389ME.NumTelefono_389ME);
                connection_389ME.Open();
                command_389ME.ExecuteNonQuery();
            }
        }

        public void CambiarBloqueo_389ME(int dni_389ME, bool bloqueado_389ME)
        {
            string query_389ME = "UPDATE Cliente SET Bloqueado_389ME = @bloqueado WHERE DNI_389ME = @dni";

            using (SqlConnection connection_389ME = new SqlConnection(cadenaConexion_389ME))
            using (SqlCommand command_389ME = new SqlCommand(query_389ME, connection_389ME))
            {
                command_389ME.Parameters.AddWithValue("@dni", dni_389ME);
                command_389ME.Parameters.AddWithValue("@bloqueado", bloqueado_389ME);
                connection_389ME.Open();
                command_389ME.ExecuteNonQuery();
            }
        }

        private Cliente Mapear_389ME(SqlDataReader datareader_389ME)
        {
            return new Cliente
            {
                DNI_389ME = Convert.ToInt32(datareader_389ME["DNI_389ME"]),
                Nombre_389ME = datareader_389ME["Nombre_389ME"].ToString(),
                Apellido_389ME = datareader_389ME["Apellido_389ME"].ToString(),
                Direccion_389ME = datareader_389ME["Direccion_389ME"] == DBNull.Value ? "" : datareader_389ME["Direccion_389ME"].ToString(),
                Mail_389ME = datareader_389ME["Mail_389ME"] == DBNull.Value ? "" : datareader_389ME["Mail_389ME"].ToString(),
                NumTelefono_389ME = datareader_389ME["NumTelefono_389ME"] == DBNull.Value ? 0 : Convert.ToInt32(datareader_389ME["NumTelefono_389ME"]),
                Bloqueado_389ME = datareader_389ME["Bloqueado_389ME"] != DBNull.Value && Convert.ToBoolean(datareader_389ME["Bloqueado_389ME"]),
                DVH_389ME = datareader_389ME["DVH_389ME"] == DBNull.Value ? 0 : Convert.ToInt32(datareader_389ME["DVH_389ME"])
            };
        }
    }
}
