using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using BE;

namespace DAL
{
    public class DALCotizacion
    {
        private string cadenaConexion_389ME = Conexion.cadena;

        // Si el proveedor ya habia cotizado ese insumo para esa solicitud, se reemplaza su cotizacion
        public void Guardar_389ME(Cotizacion cotizacion_389ME)
        {
            string query_389ME = @"IF EXISTS (SELECT 1 FROM Cotizacion WHERE CodSolicitud_389ME = @codSolicitud AND CUIT_389ME = @cuit AND CodInsumo_389ME = @codInsumo)
                                       UPDATE Cotizacion SET Precio_389ME = @precio, Cantidad_389ME = @cantidad, FechaVencimiento_389ME = @vencimiento
                                       WHERE CodSolicitud_389ME = @codSolicitud AND CUIT_389ME = @cuit AND CodInsumo_389ME = @codInsumo
                                   ELSE
                                       INSERT INTO Cotizacion (CodSolicitud_389ME, CUIT_389ME, CodInsumo_389ME, Precio_389ME, Cantidad_389ME, FechaVencimiento_389ME)
                                       VALUES (@codSolicitud, @cuit, @codInsumo, @precio, @cantidad, @vencimiento)";

            using (SqlConnection connection_389ME = new SqlConnection(cadenaConexion_389ME))
            using (SqlCommand command_389ME = new SqlCommand(query_389ME, connection_389ME))
            {
                command_389ME.Parameters.AddWithValue("@codSolicitud", cotizacion_389ME.CodSolicitud_389ME);
                command_389ME.Parameters.AddWithValue("@cuit", cotizacion_389ME.CUIT_389ME);
                command_389ME.Parameters.AddWithValue("@codInsumo", cotizacion_389ME.CodInsumo_389ME);
                command_389ME.Parameters.AddWithValue("@precio", cotizacion_389ME.Precio_389ME);
                command_389ME.Parameters.AddWithValue("@cantidad", cotizacion_389ME.Cantidad_389ME);
                command_389ME.Parameters.AddWithValue("@vencimiento", cotizacion_389ME.FechaVencimiento_389ME);
                connection_389ME.Open();
                command_389ME.ExecuteNonQuery();
            }
        }

        public List<Cotizacion> ListarPorSolicitud_389ME(int codSolicitud_389ME)
        {
            List<Cotizacion> lista_389ME = new List<Cotizacion>();
            string query_389ME = @"SELECT c.CodCotizacion_389ME, c.CodSolicitud_389ME, c.CUIT_389ME, p.Nombre_389ME AS NombreProveedor,
                                          c.CodInsumo_389ME, i.Nombre_389ME AS NombreInsumo, c.Precio_389ME, c.Cantidad_389ME, c.FechaVencimiento_389ME
                                   FROM Cotizacion c
                                   INNER JOIN Proveedor p ON p.CUIT_389ME = c.CUIT_389ME
                                   INNER JOIN Insumo i ON i.CodInsumo_389ME = c.CodInsumo_389ME
                                   WHERE c.CodSolicitud_389ME = @codSolicitud
                                   ORDER BY p.Nombre_389ME, i.Nombre_389ME";

            using (SqlConnection connection_389ME = new SqlConnection(cadenaConexion_389ME))
            using (SqlCommand command_389ME = new SqlCommand(query_389ME, connection_389ME))
            {
                command_389ME.Parameters.AddWithValue("@codSolicitud", codSolicitud_389ME);
                connection_389ME.Open();
                using (SqlDataReader datareader_389ME = command_389ME.ExecuteReader())
                {
                    while (datareader_389ME.Read())
                    {
                        lista_389ME.Add(new Cotizacion
                        {
                            CodCotizacion_389ME = Convert.ToInt32(datareader_389ME["CodCotizacion_389ME"]),
                            CodSolicitud_389ME = Convert.ToInt32(datareader_389ME["CodSolicitud_389ME"]),
                            CUIT_389ME = datareader_389ME["CUIT_389ME"].ToString(),
                            NombreProveedor_389ME = datareader_389ME["NombreProveedor"].ToString(),
                            CodInsumo_389ME = Convert.ToInt32(datareader_389ME["CodInsumo_389ME"]),
                            NombreInsumo_389ME = datareader_389ME["NombreInsumo"].ToString(),
                            Precio_389ME = Convert.ToDouble(datareader_389ME["Precio_389ME"]),
                            Cantidad_389ME = Convert.ToDouble(datareader_389ME["Cantidad_389ME"]),
                            FechaVencimiento_389ME = Convert.ToDateTime(datareader_389ME["FechaVencimiento_389ME"])
                        });
                    }
                }
            }
            return lista_389ME;
        }
    }
}
