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
    public class DALUsuario
    {
        private readonly string connectionString =
        Conexion.cadena;

        private readonly SqlConnection connection;

        public DALUsuario()
        {
            connection = new SqlConnection(connectionString);
        }
        public Usuario ObtenerPorDNI(int dni)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(
                "SELECT DNI, Username, Contraseña, Nombre, Apellido, Email, NumTelefono, Rol, DVH, IntentosFallidos, Bloqueado FROM Usuario WHERE DNI = @dni", connection))
            {
                command.Parameters.AddWithValue("@dni", dni);
                connection.Open();
                using (SqlDataReader datareader = command.ExecuteReader())
                {
                    if (!datareader.Read()) return null;
                    Usuario usuario = new Usuario(
                        Convert.ToInt32(datareader["DNI"]),
                        datareader["Nombre"].ToString(),
                        datareader["Apellido"].ToString(),
                        datareader["Email"].ToString(),
                        Convert.ToInt64(datareader["NumTelefono"]),
                        datareader["Username"].ToString(),
                        datareader["Contraseña"].ToString(),
                        datareader["Rol"].ToString(),
                        datareader["DVH"] == DBNull.Value ? 0 : Convert.ToInt32(datareader["DVH"])
                    );
                    usuario.IntentosFallidos = Convert.ToInt32(datareader["IntentosFallidos"]);
                    usuario.Bloqueado = Convert.ToBoolean(datareader["Bloqueado"]);
                    return usuario;
                }
            }
        }
        public Usuario ObtenerPorUsername(string username)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(
                "SELECT DNI, Username, Contraseña, Nombre, Apellido, Email, NumTelefono, Rol, DVH, IntentosFallidos, Bloqueado FROM Usuario WHERE Username = @username", connection))
            {
                command.Parameters.AddWithValue("@username", username);
                connection.Open();
                using (SqlDataReader datareader = command.ExecuteReader())
                {
                    if (!datareader.Read()) return null;
                    Usuario usuario = new Usuario(
                        Convert.ToInt32(datareader["DNI"]),
                        datareader["Nombre"].ToString(),
                        datareader["Apellido"].ToString(),
                        datareader["Email"].ToString(),
                        Convert.ToInt64(datareader["NumTelefono"]),
                        datareader["Username"].ToString(),
                        datareader["Contraseña"].ToString(),
                        datareader["Rol"].ToString(),
                        datareader["DVH"] == DBNull.Value ? 0 : Convert.ToInt32(datareader["DVH"])
                    );
                    usuario.IntentosFallidos = Convert.ToInt32(datareader["IntentosFallidos"]);
                    usuario.Bloqueado = Convert.ToBoolean(datareader["Bloqueado"]);
                    return usuario;
                }
            }
        }

        public void RegistrarIntentoFallido(string username, int intentosParaBloquear)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(
                @"UPDATE Usuario
                  SET IntentosFallidos = IntentosFallidos + 1,
                      Bloqueado = CASE WHEN IntentosFallidos + 1 >= @tope THEN 1 ELSE 0 END
                  WHERE Username = @Username", connection))
            {
                command.Parameters.AddWithValue("@tope", intentosParaBloquear);
                command.Parameters.AddWithValue("@Username", username);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void ReiniciarIntentos(int dni)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(
                "UPDATE Usuario SET IntentosFallidos = 0, Bloqueado = 0 WHERE DNI = @dni", connection))
            {
                command.Parameters.AddWithValue("@dni", dni);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        // Bloquea o desbloquea al usuario; al desbloquear tambien reinicia los intentos fallidos.
        // AvisarCambio_389ME le indica al trigger TR_Usuario_Desbloqueo que el cambio lo hizo el programa.
        public void CambiarBloqueo_389ME(int dni_389ME, bool bloqueado_389ME, string editor_389ME)
        {
            using (SqlConnection connection_389ME = new SqlConnection(connectionString))
            using (SqlCommand command_389ME = new SqlCommand(
                "UPDATE Usuario SET Bloqueado = @bloqueado, IntentosFallidos = CASE WHEN @bloqueado = 1 THEN IntentosFallidos ELSE 0 END WHERE DNI = @dni",
                connection_389ME))
            {
                command_389ME.Parameters.AddWithValue("@bloqueado", bloqueado_389ME);
                command_389ME.Parameters.AddWithValue("@dni", dni_389ME);
                connection_389ME.Open();
                AvisarCambio_389ME(connection_389ME, editor_389ME, bloqueado_389ME ? "Bloqueo" : "Desbloqueo");
                command_389ME.ExecuteNonQuery();
            }
        }

        // El trigger TR_Usuario_Cambios lee estos datos para guardar quien hizo el cambio y de que tipo es.
        // Si el cambio se hace directo en la base, el trigger guarda el usuario de Windows o de SQL.
        private void AvisarCambio_389ME(SqlConnection connection_389ME, string editor_389ME, string operacion_389ME)
        {
            using (SqlCommand command_389ME = new SqlCommand(
                "EXEC sp_set_session_context N'Usuario', @editor; EXEC sp_set_session_context N'Operacion', @operacion;", connection_389ME))
            {
                command_389ME.Parameters.AddWithValue("@editor", editor_389ME);
                command_389ME.Parameters.AddWithValue("@operacion", operacion_389ME);
                command_389ME.ExecuteNonQuery();
            }
        }

        public void ActualizarContraseña(string username, string nuevaContraseñaHash)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(
                "UPDATE Usuario SET Contraseña = @Contraseña WHERE Username = @Username", connection))
            {
                command.Parameters.AddWithValue("@Contraseña", nuevaContraseñaHash);
                command.Parameters.AddWithValue("@Username", username);
                connection.Open();
                AvisarCambio_389ME(connection, username, "Modificacion");
                command.ExecuteNonQuery();
            }
        }

        public Usuario Login(Usuario user)
        {
            string query = @"SELECT u.DNI, u.Username, u.Rol, up.CodPermiso, p.Nombre as NombrePerfil
                     FROM Usuario u
                     INNER JOIN Usuario_Permiso up ON u.DNI = up.CodUsuario
                     INNER JOIN Permiso p ON up.CodPermiso = p.CodPermiso
                     WHERE u.Username = @User AND u.Contraseña = @Pass";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@User", user.NombreUsuario);
                    command.Parameters.AddWithValue("@Pass", user.Contraseña);
                    connection.Open();

                    using (SqlDataReader datareader = command.ExecuteReader())
                    {
                        if (datareader.Read())
                        {
                            Usuario usuarioValido = new Usuario();


                            usuarioValido.DNI = Convert.ToInt32(datareader["DNI"]);
                            usuarioValido.NombreUsuario = datareader["Username"].ToString();
                            usuarioValido.Rol = datareader["Rol"].ToString();


                            usuarioValido.Perfil = new Familia()
                            {
                                Id = Convert.ToInt32(datareader["CodPermiso"]),
                                Nombre = datareader["NombrePerfil"].ToString()
                            };

                            return usuarioValido;
                        }
                    }
                }
            }
            return null;
        }



        public DataTable ListarUsuarios()
        {
            connection.Open();
            string query = @"SELECT DNI,
                        Username,
                        Contraseña,
                        Nombre,
                        Apellido,
                        Email,
                        NumTelefono,
                        Rol,
                        Bloqueado,
                        DVH
                     FROM Usuario";
            SqlDataAdapter da = new SqlDataAdapter(query, connection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            connection.Close();
            return dt;
        }


        public List<Usuario> ObtenerTodos()
        {
            DataTable dt = ListarUsuarios();
            List<Usuario> lista = new List<Usuario>();

            foreach (DataRow row in dt.Rows)
            {
                lista.Add(new Usuario(
                    Convert.ToInt32(row["DNI"]),
                    row["Nombre"].ToString(),
                    row["Apellido"].ToString(),
                    row["Email"].ToString(),
                    Convert.ToInt64(row["NumTelefono"]),
                    row["Username"].ToString(),
                    row["Contraseña"].ToString(),
                    row["Rol"].ToString(),
                   row["DVH"] == DBNull.Value ? 0 : Convert.ToInt32(row["DVH"])
                ));
            }

            return lista;
        }
        public void RegistrarUsuario(Usuario usuario, int idPerfil)
        {
            // Si quedo el perfil de un usuario borrado antes con el mismo DNI, se quita para que no haya dos
            BorrarPerfil_389ME(usuario.DNI);

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand("InsertarUsuario", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@DNI", usuario.DNI);
                command.Parameters.AddWithValue("@Username", usuario.NombreUsuario);
                command.Parameters.AddWithValue("@Contraseña", usuario.Contraseña);
                command.Parameters.AddWithValue("@Email", usuario.Mail);
                command.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                command.Parameters.AddWithValue("@Apellido", usuario.Apellido);
                command.Parameters.AddWithValue("@NumTelefono", usuario.NumTelefono);
                command.Parameters.AddWithValue("@Rol", usuario.Rol);
                command.Parameters.AddWithValue("@IdPerfil", idPerfil);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }


        public void ModificarUsuario(Usuario usuario, string editor_389ME, string operacion_389ME)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand("ModificarUsuario", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@DNI", usuario.DNI);
                command.Parameters.AddWithValue("@UserName", usuario.NombreUsuario);
                command.Parameters.AddWithValue("@Contraseña", usuario.Contraseña);
                command.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                command.Parameters.AddWithValue("@Apellido", usuario.Apellido);
                command.Parameters.AddWithValue("@EMail", usuario.Mail);
                command.Parameters.AddWithValue("@NumTelefono", usuario.NumTelefono);
                command.Parameters.AddWithValue("@Rol", usuario.Rol);
                connection.Open();
                AvisarCambio_389ME(connection, editor_389ME, operacion_389ME);
                command.ExecuteNonQuery();
            }
        }
        public void EliminarUsuario(int dni)
        {
            // Primero se borra su perfil: Usuario_Permiso tiene una FK hacia Usuario
            BorrarPerfil_389ME(dni);

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand("EliminarUsuario", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@DNI", dni);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        private void BorrarPerfil_389ME(int dni_389ME)
        {
            using (SqlConnection connection_389ME = new SqlConnection(connectionString))
            using (SqlCommand command_389ME = new SqlCommand("DELETE FROM Usuario_Permiso WHERE CodUsuario = @dni", connection_389ME))
            {
                command_389ME.Parameters.AddWithValue("@dni", dni_389ME);
                connection_389ME.Open();
                command_389ME.ExecuteNonQuery();
            }
        }

        // Reemplaza el perfil asignado al usuario por el nuevo
        public void CambiarPerfil_389ME(int dni_389ME, int idPerfil_389ME)
        {
            BorrarPerfil_389ME(dni_389ME);

            using (SqlConnection connection_389ME = new SqlConnection(connectionString))
            using (SqlCommand command_389ME = new SqlCommand("INSERT INTO Usuario_Permiso (CodUsuario, CodPermiso) VALUES (@dni, @perfil)", connection_389ME))
            {
                command_389ME.Parameters.AddWithValue("@dni", dni_389ME);
                command_389ME.Parameters.AddWithValue("@perfil", idPerfil_389ME);
                connection_389ME.Open();
                command_389ME.ExecuteNonQuery();
            }
        }
        public void ActualizarDVH(int dni, int dvh)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(
                    @"UPDATE Usuario 
              SET DVH = @DVH
              WHERE DNI = @DNI", connection);

                command.Parameters.AddWithValue("@DVH", dvh);
                command.Parameters.AddWithValue("@DNI", dni);

                command.ExecuteNonQuery();
            }
        }
    }
}
