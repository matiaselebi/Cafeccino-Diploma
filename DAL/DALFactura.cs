using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using BE;

namespace DAL
{
    public class DALFactura
    {
        private string cadenaConexion_389ME = Conexion.cadena;

        public int RegistrarVenta_389ME(Factura factura_389ME)
        {
            using (SqlConnection connection_389ME = new SqlConnection(cadenaConexion_389ME))
            {
                connection_389ME.Open();
                SqlTransaction transaccion_389ME = connection_389ME.BeginTransaction();

                try
                {
                    int codFactura_389ME;

                    using (SqlCommand command_389ME = new SqlCommand(
                        @"INSERT INTO Factura (DNICliente_389ME, Fecha_389ME, MetodoPago_389ME, Banco_389ME, TipoTarjeta_389ME, MarcaTarjeta_389ME, MontoTotal_389ME)
                          VALUES (@dni, @fecha, @metodoPago, @banco, @tipoTarjeta, @marcaTarjeta, @monto)
                          SELECT SCOPE_IDENTITY()", connection_389ME, transaccion_389ME))
                    {
                        command_389ME.Parameters.AddWithValue("@dni", factura_389ME.DNICliente_389ME);
                        command_389ME.Parameters.AddWithValue("@fecha", factura_389ME.Fecha_389ME);
                        command_389ME.Parameters.AddWithValue("@metodoPago", factura_389ME.MetodoPago_389ME);
                        command_389ME.Parameters.AddWithValue("@banco", (object)factura_389ME.Banco_389ME ?? DBNull.Value);
                        command_389ME.Parameters.AddWithValue("@tipoTarjeta", (object)factura_389ME.TipoTarjeta_389ME ?? DBNull.Value);
                        command_389ME.Parameters.AddWithValue("@marcaTarjeta", (object)factura_389ME.MarcaTarjeta_389ME ?? DBNull.Value);
                        command_389ME.Parameters.AddWithValue("@monto", factura_389ME.MontoTotal_389ME);
                        codFactura_389ME = Convert.ToInt32(command_389ME.ExecuteScalar());
                    }

                    foreach (DetalleFactura detalle_389ME in factura_389ME.Detalles_389ME)
                    {
                        using (SqlCommand command_389ME = new SqlCommand(
                            @"INSERT INTO FacturaDetalle (CodFactura_389ME, CodProducto_389ME, Cantidad_389ME, PrecioUnitario_389ME)
                              VALUES (@codFactura, @codProducto, @cantidad, @precio)", connection_389ME, transaccion_389ME))
                        {
                            command_389ME.Parameters.AddWithValue("@codFactura", codFactura_389ME);
                            command_389ME.Parameters.AddWithValue("@codProducto", detalle_389ME.CodProducto_389ME);
                            command_389ME.Parameters.AddWithValue("@cantidad", detalle_389ME.Cantidad_389ME);
                            command_389ME.Parameters.AddWithValue("@precio", detalle_389ME.PrecioUnitario_389ME);
                            command_389ME.ExecuteNonQuery();
                        }

                        using (SqlCommand command_389ME = new SqlCommand(
                            "UPDATE Producto SET Stock_389ME = Stock_389ME - @cantidad WHERE CodProducto_389ME = @codProducto", connection_389ME, transaccion_389ME))
                        {
                            command_389ME.Parameters.AddWithValue("@cantidad", detalle_389ME.Cantidad_389ME);
                            command_389ME.Parameters.AddWithValue("@codProducto", detalle_389ME.CodProducto_389ME);
                            command_389ME.ExecuteNonQuery();
                        }
                    }

                    transaccion_389ME.Commit();
                    return codFactura_389ME;
                }
                catch
                {
                    transaccion_389ME.Rollback();
                    throw;
                }
            }
        }

        public List<Factura> Listar_389ME()
        {
            List<Factura> lista_389ME = new List<Factura>();
            string query_389ME = @"SELECT CodFactura_389ME, DNICliente_389ME, Fecha_389ME, MetodoPago_389ME, Banco_389ME, TipoTarjeta_389ME, MarcaTarjeta_389ME, MontoTotal_389ME, DVH_389ME
                             FROM Factura ORDER BY Fecha_389ME DESC";

            using (SqlConnection connection_389ME = new SqlConnection(cadenaConexion_389ME))
            using (SqlCommand command_389ME = new SqlCommand(query_389ME, connection_389ME))
            {
                connection_389ME.Open();
                using (SqlDataReader datareader_389ME = command_389ME.ExecuteReader())
                {
                    while (datareader_389ME.Read())
                    {
                        lista_389ME.Add(new Factura
                        {
                            CodFactura_389ME = Convert.ToInt32(datareader_389ME["CodFactura_389ME"]),
                            DNICliente_389ME = Convert.ToInt32(datareader_389ME["DNICliente_389ME"]),
                            Fecha_389ME = Convert.ToDateTime(datareader_389ME["Fecha_389ME"]),
                            MetodoPago_389ME = datareader_389ME["MetodoPago_389ME"].ToString(),
                            Banco_389ME = datareader_389ME["Banco_389ME"] == DBNull.Value ? "" : datareader_389ME["Banco_389ME"].ToString(),
                            TipoTarjeta_389ME = datareader_389ME["TipoTarjeta_389ME"] == DBNull.Value ? "" : datareader_389ME["TipoTarjeta_389ME"].ToString(),
                            MarcaTarjeta_389ME = datareader_389ME["MarcaTarjeta_389ME"] == DBNull.Value ? "" : datareader_389ME["MarcaTarjeta_389ME"].ToString(),
                            MontoTotal_389ME = Convert.ToDouble(datareader_389ME["MontoTotal_389ME"]),
                            DVH_389ME = datareader_389ME["DVH_389ME"] == DBNull.Value ? 0 : Convert.ToInt32(datareader_389ME["DVH_389ME"])
                        });
                    }
                }
            }
            return lista_389ME;
        }

        public List<DetalleFactura> ObtenerDetalle_389ME(int codFactura_389ME)
        {
            List<DetalleFactura> lista_389ME = new List<DetalleFactura>();
            string query_389ME = @"SELECT fd.CodFactura_389ME, fd.CodProducto_389ME, p.Nombre_389ME AS NombreProducto_389ME, fd.Cantidad_389ME, fd.PrecioUnitario_389ME
                             FROM FacturaDetalle fd
                             INNER JOIN Producto p ON p.CodProducto_389ME = fd.CodProducto_389ME
                             WHERE fd.CodFactura_389ME = @cod";

            using (SqlConnection connection_389ME = new SqlConnection(cadenaConexion_389ME))
            using (SqlCommand command_389ME = new SqlCommand(query_389ME, connection_389ME))
            {
                command_389ME.Parameters.AddWithValue("@cod", codFactura_389ME);
                connection_389ME.Open();
                using (SqlDataReader datareader_389ME = command_389ME.ExecuteReader())
                {
                    while (datareader_389ME.Read())
                    {
                        lista_389ME.Add(new DetalleFactura
                        {
                            CodFactura_389ME = Convert.ToInt32(datareader_389ME["CodFactura_389ME"]),
                            CodProducto_389ME = Convert.ToInt32(datareader_389ME["CodProducto_389ME"]),
                            NombreProducto_389ME = datareader_389ME["NombreProducto_389ME"].ToString(),
                            Cantidad_389ME = Convert.ToInt32(datareader_389ME["Cantidad_389ME"]),
                            PrecioUnitario_389ME = Convert.ToDouble(datareader_389ME["PrecioUnitario_389ME"])
                        });
                    }
                }
            }
            return lista_389ME;
        }
    }
}
