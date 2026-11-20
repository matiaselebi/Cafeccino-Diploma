using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using BE;

namespace DAL
{
    public class DALReceta
    {
        private string cadenaConexion_389ME = Conexion.cadena;

        public List<Receta> ListarPorProducto_389ME(int codProducto_389ME)
        {
            List<Receta> lista_389ME = new List<Receta>();
            string query_389ME = @"SELECT r.CodProducto_389ME, r.CodInsumo_389ME, i.Nombre_389ME, i.UnidadMedida_389ME, r.Cantidad_389ME
                                   FROM Receta r
                                   INNER JOIN Insumo i ON i.CodInsumo_389ME = r.CodInsumo_389ME
                                   WHERE r.CodProducto_389ME = @codProducto
                                   ORDER BY i.Nombre_389ME";

            using (SqlConnection connection_389ME = new SqlConnection(cadenaConexion_389ME))
            using (SqlCommand command_389ME = new SqlCommand(query_389ME, connection_389ME))
            {
                command_389ME.Parameters.AddWithValue("@codProducto", codProducto_389ME);
                connection_389ME.Open();
                using (SqlDataReader datareader_389ME = command_389ME.ExecuteReader())
                {
                    while (datareader_389ME.Read())
                    {
                        lista_389ME.Add(new Receta
                        {
                            CodProducto_389ME = Convert.ToInt32(datareader_389ME["CodProducto_389ME"]),
                            CodInsumo_389ME = Convert.ToInt32(datareader_389ME["CodInsumo_389ME"]),
                            NombreInsumo_389ME = datareader_389ME["Nombre_389ME"].ToString(),
                            UnidadMedida_389ME = datareader_389ME["UnidadMedida_389ME"].ToString(),
                            Cantidad_389ME = Convert.ToDouble(datareader_389ME["Cantidad_389ME"])
                        });
                    }
                }
            }
            return lista_389ME;
        }

        // Reemplaza la receta completa del producto: borra la anterior y guarda la nueva en una transaccion
        public void Guardar_389ME(int codProducto_389ME, List<Receta> receta_389ME)
        {
            using (SqlConnection connection_389ME = new SqlConnection(cadenaConexion_389ME))
            {
                connection_389ME.Open();
                SqlTransaction transaccion_389ME = connection_389ME.BeginTransaction();

                try
                {
                    using (SqlCommand command_389ME = new SqlCommand("DELETE FROM Receta WHERE CodProducto_389ME = @codProducto", connection_389ME, transaccion_389ME))
                    {
                        command_389ME.Parameters.AddWithValue("@codProducto", codProducto_389ME);
                        command_389ME.ExecuteNonQuery();
                    }

                    foreach (Receta linea_389ME in receta_389ME)
                    {
                        using (SqlCommand command_389ME = new SqlCommand(
                            "INSERT INTO Receta (CodProducto_389ME, CodInsumo_389ME, Cantidad_389ME) VALUES (@codProducto, @codInsumo, @cantidad)",
                            connection_389ME, transaccion_389ME))
                        {
                            command_389ME.Parameters.AddWithValue("@codProducto", codProducto_389ME);
                            command_389ME.Parameters.AddWithValue("@codInsumo", linea_389ME.CodInsumo_389ME);
                            command_389ME.Parameters.AddWithValue("@cantidad", linea_389ME.Cantidad_389ME);
                            command_389ME.ExecuteNonQuery();
                        }
                    }

                    transaccion_389ME.Commit();
                }
                catch
                {
                    transaccion_389ME.Rollback();
                    throw;
                }
            }
        }
    }
}
