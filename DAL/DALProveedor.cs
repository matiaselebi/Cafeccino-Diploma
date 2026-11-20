using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using BE;

namespace DAL
{
    public class DALProveedor
    {
        private string cadenaConexion_389ME = Conexion.cadena;

        private const string Columnas_389ME = "CUIT_389ME, Nombre_389ME, RazonSocial_389ME, Telefono_389ME, Email_389ME, Direccion_389ME, CuentaBancaria_389ME, Registrado_389ME";

        public List<Proveedor> Listar_389ME()
        {
            List<Proveedor> lista_389ME = new List<Proveedor>();
            string query_389ME = "SELECT " + Columnas_389ME + " FROM Proveedor ORDER BY Nombre_389ME";

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

        public Proveedor ObtenerPorCUIT_389ME(string cuit_389ME)
        {
            string query_389ME = "SELECT " + Columnas_389ME + " FROM Proveedor WHERE CUIT_389ME = @cuit";

            using (SqlConnection connection_389ME = new SqlConnection(cadenaConexion_389ME))
            using (SqlCommand command_389ME = new SqlCommand(query_389ME, connection_389ME))
            {
                command_389ME.Parameters.AddWithValue("@cuit", cuit_389ME);
                connection_389ME.Open();
                using (SqlDataReader datareader_389ME = command_389ME.ExecuteReader())
                {
                    if (!datareader_389ME.Read()) return null;
                    return Mapear_389ME(datareader_389ME);
                }
            }
        }

        // Pre-registro: solo los datos de contacto. Registrado_389ME queda en 0 por el DEFAULT.
        public void Insertar_389ME(Proveedor proveedor_389ME)
        {
            string query_389ME = @"INSERT INTO Proveedor (CUIT_389ME, Nombre_389ME, RazonSocial_389ME, Telefono_389ME, Email_389ME,
                                   Direccion_389ME, CuentaBancaria_389ME, Registrado_389ME)
                                   VALUES (@cuit, @nombre, @razonSocial, @telefono, @email, @direccion, @cuenta, @registrado)";
            EjecutarContacto_389ME(query_389ME, proveedor_389ME);
        }

        public void Modificar_389ME(Proveedor proveedor_389ME)
        {
            string query_389ME = @"UPDATE Proveedor SET Nombre_389ME = @nombre, RazonSocial_389ME = @razonSocial,
                                   Telefono_389ME = @telefono, Email_389ME = @email, Direccion_389ME = @direccion,
                                   CuentaBancaria_389ME = @cuenta, Registrado_389ME = @registrado WHERE CUIT_389ME = @cuit";
            EjecutarContacto_389ME(query_389ME, proveedor_389ME);
        }

        private void EjecutarContacto_389ME(string query_389ME, Proveedor proveedor_389ME)
        {
            using (SqlConnection connection_389ME = new SqlConnection(cadenaConexion_389ME))
            using (SqlCommand command_389ME = new SqlCommand(query_389ME, connection_389ME))
            {
                command_389ME.Parameters.AddWithValue("@cuit", proveedor_389ME.CUIT_389ME);
                command_389ME.Parameters.AddWithValue("@nombre", proveedor_389ME.Nombre_389ME);
                command_389ME.Parameters.AddWithValue("@razonSocial", proveedor_389ME.RazonSocial_389ME);
                command_389ME.Parameters.AddWithValue("@telefono", proveedor_389ME.Telefono_389ME);
                command_389ME.Parameters.AddWithValue("@email", proveedor_389ME.Email_389ME);
                command_389ME.Parameters.AddWithValue("@direccion", (object)proveedor_389ME.Direccion_389ME ?? DBNull.Value);
                command_389ME.Parameters.AddWithValue("@cuenta", (object)proveedor_389ME.CuentaBancaria_389ME ?? DBNull.Value);
                command_389ME.Parameters.AddWithValue("@registrado", proveedor_389ME.Registrado_389ME);
                connection_389ME.Open();
                command_389ME.ExecuteNonQuery();
            }
        }

        private Proveedor Mapear_389ME(SqlDataReader datareader_389ME)
        {
            return new Proveedor
            {
                CUIT_389ME = datareader_389ME["CUIT_389ME"].ToString(),
                Nombre_389ME = datareader_389ME["Nombre_389ME"].ToString(),
                RazonSocial_389ME = datareader_389ME["RazonSocial_389ME"].ToString(),
                Telefono_389ME = datareader_389ME["Telefono_389ME"].ToString(),
                Email_389ME = datareader_389ME["Email_389ME"].ToString(),
                Direccion_389ME = datareader_389ME["Direccion_389ME"] == DBNull.Value ? "" : datareader_389ME["Direccion_389ME"].ToString(),
                CuentaBancaria_389ME = datareader_389ME["CuentaBancaria_389ME"] == DBNull.Value ? "" : datareader_389ME["CuentaBancaria_389ME"].ToString(),
                Registrado_389ME = Convert.ToBoolean(datareader_389ME["Registrado_389ME"])
            };
        }
    }
}
