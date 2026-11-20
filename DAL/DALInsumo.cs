using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using BE;

namespace DAL
{
    public class DALInsumo
    {
        private string cadenaConexion_389ME = Conexion.cadena;

        private const string Columnas_389ME = "CodInsumo_389ME, Nombre_389ME, Tipo_389ME, UnidadMedida_389ME, Precio_389ME, Stock_389ME, StockMinimo_389ME, StockMaximo_389ME";

        public List<Insumo> Listar_389ME()
        {
            List<Insumo> lista_389ME = new List<Insumo>();
            string query_389ME = "SELECT " + Columnas_389ME + " FROM Insumo ORDER BY Nombre_389ME";

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

        public Insumo ObtenerPorCodigo_389ME(int codInsumo_389ME)
        {
            string query_389ME = "SELECT " + Columnas_389ME + " FROM Insumo WHERE CodInsumo_389ME = @cod";

            using (SqlConnection connection_389ME = new SqlConnection(cadenaConexion_389ME))
            using (SqlCommand command_389ME = new SqlCommand(query_389ME, connection_389ME))
            {
                command_389ME.Parameters.AddWithValue("@cod", codInsumo_389ME);
                connection_389ME.Open();
                using (SqlDataReader datareader_389ME = command_389ME.ExecuteReader())
                {
                    if (!datareader_389ME.Read()) return null;
                    return Mapear_389ME(datareader_389ME);
                }
            }
        }

        public void Insertar_389ME(Insumo insumo_389ME)
        {
            string query_389ME = @"INSERT INTO Insumo (Nombre_389ME, Tipo_389ME, UnidadMedida_389ME, Precio_389ME, Stock_389ME, StockMinimo_389ME, StockMaximo_389ME)
                                   VALUES (@nombre, @tipo, @unidad, @precio, @stock, @minimo, @maximo)";
            Ejecutar_389ME(query_389ME, insumo_389ME);
        }

        public void Modificar_389ME(Insumo insumo_389ME)
        {
            string query_389ME = @"UPDATE Insumo SET Nombre_389ME = @nombre, Tipo_389ME = @tipo, UnidadMedida_389ME = @unidad, Precio_389ME = @precio,
                                   Stock_389ME = @stock, StockMinimo_389ME = @minimo, StockMaximo_389ME = @maximo
                                   WHERE CodInsumo_389ME = @cod";
            Ejecutar_389ME(query_389ME, insumo_389ME);
        }

        public void Eliminar_389ME(int codInsumo_389ME)
        {
            using (SqlConnection connection_389ME = new SqlConnection(cadenaConexion_389ME))
            using (SqlCommand command_389ME = new SqlCommand("DELETE FROM Insumo WHERE CodInsumo_389ME = @cod", connection_389ME))
            {
                command_389ME.Parameters.AddWithValue("@cod", codInsumo_389ME);
                connection_389ME.Open();
                command_389ME.ExecuteNonQuery();
            }
        }

        // Cuanto se consumio de cada insumo en las ventas hechas desde la fecha indicada
        public Dictionary<int, double> ObtenerConsumoDesde_389ME(DateTime desde_389ME)
        {
            Dictionary<int, double> consumo_389ME = new Dictionary<int, double>();
            string query_389ME = @"SELECT ci.CodInsumo_389ME, SUM(ci.CantidadConsumida_389ME) AS Total
                                   FROM ConsumoInsumo ci
                                   INNER JOIN Factura f ON f.CodFactura_389ME = ci.CodFactura_389ME
                                   WHERE f.Fecha_389ME >= @desde
                                   GROUP BY ci.CodInsumo_389ME";

            using (SqlConnection connection_389ME = new SqlConnection(cadenaConexion_389ME))
            using (SqlCommand command_389ME = new SqlCommand(query_389ME, connection_389ME))
            {
                command_389ME.Parameters.AddWithValue("@desde", desde_389ME);
                connection_389ME.Open();
                using (SqlDataReader datareader_389ME = command_389ME.ExecuteReader())
                {
                    while (datareader_389ME.Read())
                        consumo_389ME[Convert.ToInt32(datareader_389ME["CodInsumo_389ME"])] = Convert.ToDouble(datareader_389ME["Total"]);
                }
            }
            return consumo_389ME;
        }

        private void Ejecutar_389ME(string query_389ME, Insumo insumo_389ME)
        {
            using (SqlConnection connection_389ME = new SqlConnection(cadenaConexion_389ME))
            using (SqlCommand command_389ME = new SqlCommand(query_389ME, connection_389ME))
            {
                command_389ME.Parameters.AddWithValue("@cod", insumo_389ME.CodInsumo_389ME);
                command_389ME.Parameters.AddWithValue("@nombre", insumo_389ME.Nombre_389ME);
                command_389ME.Parameters.AddWithValue("@tipo", insumo_389ME.Tipo_389ME);
                command_389ME.Parameters.AddWithValue("@unidad", insumo_389ME.UnidadMedida_389ME);
                command_389ME.Parameters.AddWithValue("@precio", insumo_389ME.Precio_389ME);
                command_389ME.Parameters.AddWithValue("@stock", insumo_389ME.Stock_389ME);
                command_389ME.Parameters.AddWithValue("@minimo", insumo_389ME.StockMinimo_389ME);
                command_389ME.Parameters.AddWithValue("@maximo", insumo_389ME.StockMaximo_389ME);
                connection_389ME.Open();
                command_389ME.ExecuteNonQuery();
            }
        }

        private Insumo Mapear_389ME(SqlDataReader datareader_389ME)
        {
            return new Insumo
            {
                CodInsumo_389ME = Convert.ToInt32(datareader_389ME["CodInsumo_389ME"]),
                Nombre_389ME = datareader_389ME["Nombre_389ME"].ToString(),
                Tipo_389ME = datareader_389ME["Tipo_389ME"].ToString(),
                UnidadMedida_389ME = datareader_389ME["UnidadMedida_389ME"].ToString(),
                Precio_389ME = Convert.ToDouble(datareader_389ME["Precio_389ME"]),
                Stock_389ME = Convert.ToDouble(datareader_389ME["Stock_389ME"]),
                StockMinimo_389ME = Convert.ToDouble(datareader_389ME["StockMinimo_389ME"]),
                StockMaximo_389ME = Convert.ToDouble(datareader_389ME["StockMaximo_389ME"])
            };
        }
    }
}
