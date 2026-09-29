using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using BE;

namespace DAL
{
    public class DALPermiso
    {

        private string cadenaConexion = Conexion.cadena;

        public List<Perfil> ObtenerPermisosYPerfiles()
        {
            List<Perfil> lista = new List<Perfil>();


            string query = @"SELECT CodPermiso, Nombre, EsFamilia 
                     FROM Permiso 
                     WHERE EsPerfil = 0
                     ORDER BY Nombre";

            using (SqlConnection connection = new SqlConnection(cadenaConexion))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                connection.Open();
                using (SqlDataReader datareader = command.ExecuteReader())
                {
                    while (datareader.Read())
                    {
                        bool esFamilia = Convert.ToBoolean(datareader["EsFamilia"]);
                        Perfil perfil = esFamilia ? (Perfil)new Familia() : (Perfil)new Permiso();
                        perfil.Id = Convert.ToInt32(datareader["CodPermiso"]);
                        perfil.Nombre = datareader["Nombre"].ToString();
                        lista.Add(perfil);
                    }
                }
            }
            return lista;
        }
        public List<Perfil> ObtenerHijos(int idPadre)
        {
            List<Perfil> hijos = new List<Perfil>();

            string query = @"SELECT p.CodPermiso, p.Nombre, p.EsFamilia 
                         FROM Permiso_Permiso pp
                         INNER JOIN Permiso p ON pp.CodHijo = p.CodPermiso
                         WHERE pp.CodPadre = @IdPadre";

            using (SqlConnection connection = new SqlConnection(cadenaConexion))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdPadre", idPadre);
                    connection.Open();

                    using (SqlDataReader datareader = command.ExecuteReader())
                    {
                        while (datareader.Read())
                        {
                            Perfil hijo;
                            bool esFamilia = Convert.ToBoolean(datareader["EsFamilia"]);

                            if (esFamilia)
                            {
                                hijo = new Familia();
                            }
                            else
                            {
                                hijo = new Permiso();
                                if (Enum.TryParse(datareader["Nombre"].ToString(), out TipoPermiso tipo))
                                {
                                    hijo.Permiso = tipo;
                                }
                            }

                            hijo.Id = Convert.ToInt32(datareader["CodPermiso"]);
                            hijo.Nombre = datareader["Nombre"].ToString();

                            hijos.Add(hijo);
                        }
                    }
                }
            }
            return hijos;
        }
        public bool ExisteConNombre(string nombre)
        {
            string query = "SELECT COUNT(*) FROM Permiso WHERE Nombre = @nombre";
            using (SqlConnection connection = new SqlConnection(cadenaConexion))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@nombre", nombre);
                connection.Open();
                return (int)command.ExecuteScalar() > 0;
            }
        }
        public bool TieneUsuariosAsignados(int idPermiso)
        {
            string query = "SELECT COUNT(*) FROM Usuario_Permiso WHERE CodPermiso = @id";
            using (SqlConnection connection = new SqlConnection(cadenaConexion))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@id", idPermiso);
                connection.Open();
                return (int)command.ExecuteScalar() > 0;
            }
        }
        public List<Perfil> ObtenerTodosLosComponentes(bool soloFamilias, bool soloPerfiles)
        {
            List<Perfil> lista = new List<Perfil>();


            string query = @"SELECT CodPermiso, Nombre, EsFamilia 
                     FROM Permiso 
                     WHERE EsFamilia = @EsFamilia AND EsPerfil = @EsPerfil";

            using (SqlConnection connection = new SqlConnection(cadenaConexion))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {

                    command.Parameters.AddWithValue("@EsFamilia", soloFamilias ? 1 : 0);
                    command.Parameters.AddWithValue("@EsPerfil", soloPerfiles ? 1 : 0);

                    connection.Open();
                    using (SqlDataReader datareader = command.ExecuteReader())
                    {
                        while (datareader.Read())
                        {

                            Perfil perfil = soloFamilias ? (Perfil)new Familia() : (Perfil)new Permiso();
                            perfil.Id = Convert.ToInt32(datareader["CodPermiso"]);
                            perfil.Nombre = datareader["Nombre"].ToString();
                            lista.Add(perfil);
                        }
                    }
                }
            }
            return lista;
        }
        public void EliminarRelacionesComponente(int idComponente)
        {
            string query = "DELETE FROM Permiso_Permiso WHERE CodPadre = @Id OR CodHijo = @Id";
            using (SqlConnection connection = new SqlConnection(cadenaConexion))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", idComponente);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }


        public void EliminarAsignacionesUsuario(int idComponente)
        {
            string query = "DELETE FROM Usuario_Permiso WHERE CodPermiso = @Id";
            using (SqlConnection connection = new SqlConnection(cadenaConexion))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", idComponente);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }


        public void EliminarComponenteBase(int idComponente)
        {
            string query = "DELETE FROM Permiso WHERE CodPermiso = @Id";
            using (SqlConnection connection = new SqlConnection(cadenaConexion))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", idComponente);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public int InsertarComponente(string nombre, bool esFamilia, bool esPerfil)
        {

            string query = @"INSERT INTO Permiso (Nombre, EsFamilia, EsPerfil) 
                     OUTPUT INSERTED.CodPermiso 
                     VALUES (@Nombre, @EsFamilia, @EsPerfil)";

            using (SqlConnection connection = new SqlConnection(cadenaConexion))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Nombre", nombre);
                    command.Parameters.AddWithValue("@EsFamilia", esFamilia ? 1 : 0);
                    command.Parameters.AddWithValue("@EsPerfil", esPerfil ? 1 : 0);

                    connection.Open();
                    return (int)command.ExecuteScalar();
                }
            }
        }


        public void EliminarRelacionesPadre(int idPadre)
        {

            string query = "DELETE FROM Permiso_Permiso WHERE CodPadre = @idPadre";

            using (SqlConnection connection = new SqlConnection(cadenaConexion))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@idPadre", idPadre);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }


        public void InsertarRelacion(int idPadre, int idHijo)
        {
            string query = "INSERT INTO Permiso_Permiso (CodPadre, CodHijo) VALUES (@CodPadre, @IdHijo)";
            using (SqlConnection connection = new SqlConnection(cadenaConexion))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CodPadre", idPadre);
                    command.Parameters.AddWithValue("@IdHijo", idHijo);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
