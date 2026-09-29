using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using BE;

namespace DAL
{
    public class DALProducto
    {
        private string cadenaConexion_389ME = Conexion.cadena;

        public List<Producto> Listar_389ME()
        {
            List<Producto> lista_389ME = new List<Producto>();
            string query_389ME = "SELECT CodProducto_389ME, Nombre_389ME, Titulo_389ME, Precio_389ME, Stock_389ME, DVH_389ME FROM Producto ORDER BY Nombre_389ME";

            using (SqlConnection connection_389ME = new SqlConnection(cadenaConexion_389ME))
            using (SqlCommand command_389ME = new SqlCommand(query_389ME, connection_389ME))
            {
                connection_389ME.Open();
                using (SqlDataReader datareader_389ME = command_389ME.ExecuteReader())
                {
                    while (datareader_389ME.Read())
                    {
                        lista_389ME.Add(new Producto
                        {
                            CodProducto_389ME = Convert.ToInt32(datareader_389ME["CodProducto_389ME"]),
                            Nombre_389ME = datareader_389ME["Nombre_389ME"].ToString(),
                            Titulo_389ME = datareader_389ME["Titulo_389ME"].ToString(),
                            Precio_389ME = Convert.ToDouble(datareader_389ME["Precio_389ME"]),
                            Stock_389ME = Convert.ToInt32(datareader_389ME["Stock_389ME"]),
                            DVH_389ME = datareader_389ME["DVH_389ME"] == DBNull.Value ? 0 : Convert.ToInt32(datareader_389ME["DVH_389ME"])
                        });
                    }
                }
            }
            return lista_389ME;
        }

        public Producto ObtenerPorCodigo_389ME(int codProducto_389ME)
        {
            string query_389ME = "SELECT CodProducto_389ME, Nombre_389ME, Titulo_389ME, Precio_389ME, Stock_389ME, DVH_389ME FROM Producto WHERE CodProducto_389ME = @cod";

            using (SqlConnection connection_389ME = new SqlConnection(cadenaConexion_389ME))
            using (SqlCommand command_389ME = new SqlCommand(query_389ME, connection_389ME))
            {
                command_389ME.Parameters.AddWithValue("@cod", codProducto_389ME);
                connection_389ME.Open();
                using (SqlDataReader datareader_389ME = command_389ME.ExecuteReader())
                {
                    if (!datareader_389ME.Read()) return null;
                    return new Producto
                    {
                        CodProducto_389ME = Convert.ToInt32(datareader_389ME["CodProducto_389ME"]),
                        Nombre_389ME = datareader_389ME["Nombre_389ME"].ToString(),
                        Titulo_389ME = datareader_389ME["Titulo_389ME"].ToString(),
                        Precio_389ME = Convert.ToDouble(datareader_389ME["Precio_389ME"]),
                        Stock_389ME = Convert.ToInt32(datareader_389ME["Stock_389ME"]),
                        DVH_389ME = datareader_389ME["DVH_389ME"] == DBNull.Value ? 0 : Convert.ToInt32(datareader_389ME["DVH_389ME"])
                    };
                }
            }
        }

        public void Insertar_389ME(Producto producto_389ME)
        {
            string query_389ME = @"INSERT INTO Producto (Nombre_389ME, Titulo_389ME, Precio_389ME, Stock_389ME) VALUES (@nombre, @titulo, @precio, @stock)";

            using (SqlConnection connection_389ME = new SqlConnection(cadenaConexion_389ME))
            using (SqlCommand command_389ME = new SqlCommand(query_389ME, connection_389ME))
            {
                command_389ME.Parameters.AddWithValue("@nombre", producto_389ME.Nombre_389ME);
                command_389ME.Parameters.AddWithValue("@titulo", producto_389ME.Titulo_389ME);
                command_389ME.Parameters.AddWithValue("@precio", producto_389ME.Precio_389ME);
                command_389ME.Parameters.AddWithValue("@stock", producto_389ME.Stock_389ME);
                connection_389ME.Open();
                command_389ME.ExecuteNonQuery();
            }
        }

        public void Modificar_389ME(Producto producto_389ME)
        {
            string query_389ME = @"UPDATE Producto SET Nombre_389ME = @nombre, Titulo_389ME = @titulo, Precio_389ME = @precio, Stock_389ME = @stock WHERE CodProducto_389ME = @cod";

            using (SqlConnection connection_389ME = new SqlConnection(cadenaConexion_389ME))
            using (SqlCommand command_389ME = new SqlCommand(query_389ME, connection_389ME))
            {
                command_389ME.Parameters.AddWithValue("@nombre", producto_389ME.Nombre_389ME);
                command_389ME.Parameters.AddWithValue("@titulo", producto_389ME.Titulo_389ME);
                command_389ME.Parameters.AddWithValue("@precio", producto_389ME.Precio_389ME);
                command_389ME.Parameters.AddWithValue("@stock", producto_389ME.Stock_389ME);
                command_389ME.Parameters.AddWithValue("@cod", producto_389ME.CodProducto_389ME);
                connection_389ME.Open();
                command_389ME.ExecuteNonQuery();
            }
        }

        public void Eliminar_389ME(int codProducto_389ME)
        {
            string query_389ME = "DELETE FROM Producto WHERE CodProducto_389ME = @cod";

            using (SqlConnection connection_389ME = new SqlConnection(cadenaConexion_389ME))
            using (SqlCommand command_389ME = new SqlCommand(query_389ME, connection_389ME))
            {
                command_389ME.Parameters.AddWithValue("@cod", codProducto_389ME);
                connection_389ME.Open();
                command_389ME.ExecuteNonQuery();
            }
        }
    }
}
