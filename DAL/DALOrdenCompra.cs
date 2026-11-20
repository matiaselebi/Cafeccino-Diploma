using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using BE;

namespace DAL
{
    public class DALOrdenCompra
    {
        private string cadenaConexion_389ME = Conexion.cadena;

        // Guarda la orden con sus lineas y cierra la solicitud de cotizacion, en una transaccion
        public int Registrar_389ME(OrdenCompra orden_389ME)
        {
            using (SqlConnection connection_389ME = new SqlConnection(cadenaConexion_389ME))
            {
                connection_389ME.Open();
                SqlTransaction transaccion_389ME = connection_389ME.BeginTransaction();

                try
                {
                    int codOrden_389ME;

                    using (SqlCommand command_389ME = new SqlCommand(
                        @"INSERT INTO OrdenCompra (CUIT_389ME, CodSolicitud_389ME, FechaCreacion_389ME, PrecioTotal_389ME, Estado_389ME)
                          VALUES (@cuit, @codSolicitud, @fecha, @total, @estado)
                          SELECT SCOPE_IDENTITY()", connection_389ME, transaccion_389ME))
                    {
                        command_389ME.Parameters.AddWithValue("@cuit", orden_389ME.CUIT_389ME);
                        command_389ME.Parameters.AddWithValue("@codSolicitud", orden_389ME.CodSolicitud_389ME);
                        command_389ME.Parameters.AddWithValue("@fecha", orden_389ME.FechaCreacion_389ME);
                        command_389ME.Parameters.AddWithValue("@total", orden_389ME.PrecioTotal_389ME);
                        command_389ME.Parameters.AddWithValue("@estado", orden_389ME.Estado_389ME);
                        codOrden_389ME = Convert.ToInt32(command_389ME.ExecuteScalar());
                    }

                    foreach (OrdenCompraDetalle detalle_389ME in orden_389ME.Detalles_389ME)
                    {
                        using (SqlCommand command_389ME = new SqlCommand(
                            @"INSERT INTO OrdenCompraDetalle (CodOrden_389ME, CodInsumo_389ME, Cantidad_389ME, PrecioUnitario_389ME)
                              VALUES (@codOrden, @codInsumo, @cantidad, @precio)", connection_389ME, transaccion_389ME))
                        {
                            command_389ME.Parameters.AddWithValue("@codOrden", codOrden_389ME);
                            command_389ME.Parameters.AddWithValue("@codInsumo", detalle_389ME.CodInsumo_389ME);
                            command_389ME.Parameters.AddWithValue("@cantidad", detalle_389ME.Cantidad_389ME);
                            command_389ME.Parameters.AddWithValue("@precio", detalle_389ME.PrecioUnitario_389ME);
                            command_389ME.ExecuteNonQuery();
                        }
                    }

                    using (SqlCommand command_389ME = new SqlCommand(
                        "UPDATE SolicitudCotizacion SET Estado_389ME = 'Cerrada' WHERE CodSolicitud_389ME = @codSolicitud",
                        connection_389ME, transaccion_389ME))
                    {
                        command_389ME.Parameters.AddWithValue("@codSolicitud", orden_389ME.CodSolicitud_389ME);
                        command_389ME.ExecuteNonQuery();
                    }

                    transaccion_389ME.Commit();
                    return codOrden_389ME;
                }
                catch
                {
                    transaccion_389ME.Rollback();
                    throw;
                }
            }
        }

        // estado vacio = todas las ordenes
        public List<OrdenCompra> Listar_389ME(string estado_389ME)
        {
            List<OrdenCompra> lista_389ME = new List<OrdenCompra>();
            string query_389ME = @"SELECT o.CodOrden_389ME, o.CUIT_389ME, p.Nombre_389ME, o.CodSolicitud_389ME, o.FechaCreacion_389ME, o.PrecioTotal_389ME, o.Estado_389ME
                                   FROM OrdenCompra o
                                   INNER JOIN Proveedor p ON p.CUIT_389ME = o.CUIT_389ME
                                   WHERE (@estado = '' OR o.Estado_389ME = @estado)
                                   ORDER BY o.FechaCreacion_389ME DESC";

            using (SqlConnection connection_389ME = new SqlConnection(cadenaConexion_389ME))
            using (SqlCommand command_389ME = new SqlCommand(query_389ME, connection_389ME))
            {
                command_389ME.Parameters.AddWithValue("@estado", estado_389ME);
                connection_389ME.Open();
                using (SqlDataReader datareader_389ME = command_389ME.ExecuteReader())
                {
                    while (datareader_389ME.Read())
                    {
                        lista_389ME.Add(new OrdenCompra
                        {
                            CodOrden_389ME = Convert.ToInt32(datareader_389ME["CodOrden_389ME"]),
                            CUIT_389ME = datareader_389ME["CUIT_389ME"].ToString(),
                            NombreProveedor_389ME = datareader_389ME["Nombre_389ME"].ToString(),
                            CodSolicitud_389ME = Convert.ToInt32(datareader_389ME["CodSolicitud_389ME"]),
                            FechaCreacion_389ME = Convert.ToDateTime(datareader_389ME["FechaCreacion_389ME"]),
                            PrecioTotal_389ME = Convert.ToDouble(datareader_389ME["PrecioTotal_389ME"]),
                            Estado_389ME = datareader_389ME["Estado_389ME"].ToString()
                        });
                    }
                }
            }
            return lista_389ME;
        }

        public List<OrdenCompraDetalle> ObtenerDetalle_389ME(int codOrden_389ME)
        {
            List<OrdenCompraDetalle> lista_389ME = new List<OrdenCompraDetalle>();
            string query_389ME = @"SELECT d.CodOrden_389ME, d.CodInsumo_389ME, i.Nombre_389ME, d.Cantidad_389ME, d.PrecioUnitario_389ME, d.CantidadRecibida_389ME
                                   FROM OrdenCompraDetalle d
                                   INNER JOIN Insumo i ON i.CodInsumo_389ME = d.CodInsumo_389ME
                                   WHERE d.CodOrden_389ME = @codOrden";

            using (SqlConnection connection_389ME = new SqlConnection(cadenaConexion_389ME))
            using (SqlCommand command_389ME = new SqlCommand(query_389ME, connection_389ME))
            {
                command_389ME.Parameters.AddWithValue("@codOrden", codOrden_389ME);
                connection_389ME.Open();
                using (SqlDataReader datareader_389ME = command_389ME.ExecuteReader())
                {
                    while (datareader_389ME.Read())
                    {
                        lista_389ME.Add(new OrdenCompraDetalle
                        {
                            CodOrden_389ME = Convert.ToInt32(datareader_389ME["CodOrden_389ME"]),
                            CodInsumo_389ME = Convert.ToInt32(datareader_389ME["CodInsumo_389ME"]),
                            NombreInsumo_389ME = datareader_389ME["Nombre_389ME"].ToString(),
                            Cantidad_389ME = Convert.ToDouble(datareader_389ME["Cantidad_389ME"]),
                            PrecioUnitario_389ME = Convert.ToDouble(datareader_389ME["PrecioUnitario_389ME"]),
                            CantidadRecibida_389ME = Convert.ToDouble(datareader_389ME["CantidadRecibida_389ME"])
                        });
                    }
                }
            }
            return lista_389ME;
        }

        // Anota lo recibido de cada linea, suma ese stock a los insumos y actualiza el estado de la orden
        public void RegistrarRecepcion_389ME(int codOrden_389ME, List<OrdenCompraDetalle> detalles_389ME, string estado_389ME)
        {
            using (SqlConnection connection_389ME = new SqlConnection(cadenaConexion_389ME))
            {
                connection_389ME.Open();
                SqlTransaction transaccion_389ME = connection_389ME.BeginTransaction();

                try
                {
                    foreach (OrdenCompraDetalle detalle_389ME in detalles_389ME)
                    {
                        using (SqlCommand command_389ME = new SqlCommand(
                            @"UPDATE OrdenCompraDetalle SET CantidadRecibida_389ME = @recibida
                              WHERE CodOrden_389ME = @codOrden AND CodInsumo_389ME = @codInsumo;
                              UPDATE Insumo SET Stock_389ME = Stock_389ME + @recibida, Precio_389ME = @precio
                              WHERE CodInsumo_389ME = @codInsumo AND @recibida > 0", connection_389ME, transaccion_389ME))
                        {
                            command_389ME.Parameters.AddWithValue("@codOrden", codOrden_389ME);
                            command_389ME.Parameters.AddWithValue("@codInsumo", detalle_389ME.CodInsumo_389ME);
                            command_389ME.Parameters.AddWithValue("@recibida", detalle_389ME.CantidadRecibida_389ME);
                            command_389ME.Parameters.AddWithValue("@precio", detalle_389ME.PrecioUnitario_389ME);
                            command_389ME.ExecuteNonQuery();
                        }
                    }

                    using (SqlCommand command_389ME = new SqlCommand(
                        "UPDATE OrdenCompra SET Estado_389ME = @estado WHERE CodOrden_389ME = @codOrden", connection_389ME, transaccion_389ME))
                    {
                        command_389ME.Parameters.AddWithValue("@estado", estado_389ME);
                        command_389ME.Parameters.AddWithValue("@codOrden", codOrden_389ME);
                        command_389ME.ExecuteNonQuery();
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
