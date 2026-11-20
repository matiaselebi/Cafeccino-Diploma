using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using BE;

namespace DAL
{
    public class DALSolicitudCotizacion
    {
        private string cadenaConexion_389ME = Conexion.cadena;

        // Guarda la solicitud con sus insumos y sus proveedores en una transaccion. Devuelve el codigo nuevo.
        public int Registrar_389ME(SolicitudCotizacion solicitud_389ME)
        {
            using (SqlConnection connection_389ME = new SqlConnection(cadenaConexion_389ME))
            {
                connection_389ME.Open();
                SqlTransaction transaccion_389ME = connection_389ME.BeginTransaction();

                try
                {
                    int codSolicitud_389ME;

                    using (SqlCommand command_389ME = new SqlCommand(
                        @"INSERT INTO SolicitudCotizacion (FechaEmision_389ME, Estado_389ME) VALUES (@fecha, @estado)
                          SELECT SCOPE_IDENTITY()", connection_389ME, transaccion_389ME))
                    {
                        command_389ME.Parameters.AddWithValue("@fecha", solicitud_389ME.FechaEmision_389ME);
                        command_389ME.Parameters.AddWithValue("@estado", solicitud_389ME.Estado_389ME);
                        codSolicitud_389ME = Convert.ToInt32(command_389ME.ExecuteScalar());
                    }

                    foreach (SolicitudInsumo item_389ME in solicitud_389ME.Insumos_389ME)
                    {
                        using (SqlCommand command_389ME = new SqlCommand(
                            "INSERT INTO SolicitudInsumo (CodSolicitud_389ME, CodInsumo_389ME, Cantidad_389ME) VALUES (@codSolicitud, @codInsumo, @cantidad)",
                            connection_389ME, transaccion_389ME))
                        {
                            command_389ME.Parameters.AddWithValue("@codSolicitud", codSolicitud_389ME);
                            command_389ME.Parameters.AddWithValue("@codInsumo", item_389ME.CodInsumo_389ME);
                            command_389ME.Parameters.AddWithValue("@cantidad", item_389ME.Cantidad_389ME);
                            command_389ME.ExecuteNonQuery();
                        }
                    }

                    foreach (Proveedor proveedor_389ME in solicitud_389ME.Proveedores_389ME)
                    {
                        using (SqlCommand command_389ME = new SqlCommand(
                            "INSERT INTO SolicitudProveedor (CodSolicitud_389ME, CUIT_389ME) VALUES (@codSolicitud, @cuit)",
                            connection_389ME, transaccion_389ME))
                        {
                            command_389ME.Parameters.AddWithValue("@codSolicitud", codSolicitud_389ME);
                            command_389ME.Parameters.AddWithValue("@cuit", proveedor_389ME.CUIT_389ME);
                            command_389ME.ExecuteNonQuery();
                        }
                    }

                    transaccion_389ME.Commit();
                    return codSolicitud_389ME;
                }
                catch
                {
                    transaccion_389ME.Rollback();
                    throw;
                }
            }
        }

        public List<SolicitudCotizacion> ListarPorEstado_389ME(string estado_389ME)
        {
            List<SolicitudCotizacion> lista_389ME = new List<SolicitudCotizacion>();
            string query_389ME = @"SELECT CodSolicitud_389ME, FechaEmision_389ME, Estado_389ME FROM SolicitudCotizacion
                                   WHERE Estado_389ME = @estado ORDER BY FechaEmision_389ME DESC";

            using (SqlConnection connection_389ME = new SqlConnection(cadenaConexion_389ME))
            using (SqlCommand command_389ME = new SqlCommand(query_389ME, connection_389ME))
            {
                command_389ME.Parameters.AddWithValue("@estado", estado_389ME);
                connection_389ME.Open();
                using (SqlDataReader datareader_389ME = command_389ME.ExecuteReader())
                {
                    while (datareader_389ME.Read())
                    {
                        lista_389ME.Add(new SolicitudCotizacion
                        {
                            CodSolicitud_389ME = Convert.ToInt32(datareader_389ME["CodSolicitud_389ME"]),
                            FechaEmision_389ME = Convert.ToDateTime(datareader_389ME["FechaEmision_389ME"]),
                            Estado_389ME = datareader_389ME["Estado_389ME"].ToString()
                        });
                    }
                }
            }
            return lista_389ME;
        }

        public List<SolicitudInsumo> ObtenerInsumos_389ME(int codSolicitud_389ME)
        {
            List<SolicitudInsumo> lista_389ME = new List<SolicitudInsumo>();
            string query_389ME = @"SELECT si.CodSolicitud_389ME, si.CodInsumo_389ME, i.Nombre_389ME, i.UnidadMedida_389ME, si.Cantidad_389ME,
                                   i.Precio_389ME
                                   FROM SolicitudInsumo si
                                   INNER JOIN Insumo i ON i.CodInsumo_389ME = si.CodInsumo_389ME
                                   WHERE si.CodSolicitud_389ME = @codSolicitud";

            using (SqlConnection connection_389ME = new SqlConnection(cadenaConexion_389ME))
            using (SqlCommand command_389ME = new SqlCommand(query_389ME, connection_389ME))
            {
                command_389ME.Parameters.AddWithValue("@codSolicitud", codSolicitud_389ME);
                connection_389ME.Open();
                using (SqlDataReader datareader_389ME = command_389ME.ExecuteReader())
                {
                    while (datareader_389ME.Read())
                    {
                        lista_389ME.Add(new SolicitudInsumo
                        {
                            CodSolicitud_389ME = Convert.ToInt32(datareader_389ME["CodSolicitud_389ME"]),
                            CodInsumo_389ME = Convert.ToInt32(datareader_389ME["CodInsumo_389ME"]),
                            NombreInsumo_389ME = datareader_389ME["Nombre_389ME"].ToString(),
                            UnidadMedida_389ME = datareader_389ME["UnidadMedida_389ME"].ToString(),
                            Cantidad_389ME = Convert.ToDouble(datareader_389ME["Cantidad_389ME"]),
                            PrecioActual_389ME = Convert.ToDouble(datareader_389ME["Precio_389ME"])
                        });
                    }
                }
            }
            return lista_389ME;
        }

        public List<string> ObtenerCUITProveedores_389ME(int codSolicitud_389ME)
        {
            List<string> lista_389ME = new List<string>();

            using (SqlConnection connection_389ME = new SqlConnection(cadenaConexion_389ME))
            using (SqlCommand command_389ME = new SqlCommand(
                "SELECT CUIT_389ME FROM SolicitudProveedor WHERE CodSolicitud_389ME = @codSolicitud", connection_389ME))
            {
                command_389ME.Parameters.AddWithValue("@codSolicitud", codSolicitud_389ME);
                connection_389ME.Open();
                using (SqlDataReader datareader_389ME = command_389ME.ExecuteReader())
                {
                    while (datareader_389ME.Read())
                        lista_389ME.Add(datareader_389ME["CUIT_389ME"].ToString());
                }
            }
            return lista_389ME;
        }
    }
}
