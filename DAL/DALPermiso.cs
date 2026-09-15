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

            using (SqlConnection con = new SqlConnection(cadenaConexion))
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        bool esFamilia = Convert.ToBoolean(reader["EsFamilia"]);
                        Perfil p = esFamilia ? (Perfil)new Familia() : (Perfil)new Permiso();
                        p.Id = Convert.ToInt32(reader["CodPermiso"]);
                        p.Nombre = reader["Nombre"].ToString();
                        lista.Add(p);
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

            using (SqlConnection con = new SqlConnection(cadenaConexion))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@IdPadre", idPadre);
                    con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Perfil hijo;
                            bool esFamilia = Convert.ToBoolean(reader["EsFamilia"]);

                            if (esFamilia)
                            {
                                hijo = new Familia();
                            }
                            else
                            {
                                hijo = new Permiso();
                                if (Enum.TryParse(reader["Nombre"].ToString(), out TipoPermiso tipo))
                                {
                                    hijo.Permiso = tipo;
                                }
                            }

                            hijo.Id = Convert.ToInt32(reader["CodPermiso"]);
                            hijo.Nombre = reader["Nombre"].ToString();

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
            using (SqlConnection con = new SqlConnection(cadenaConexion))
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@nombre", nombre);
                con.Open();
                return (int)cmd.ExecuteScalar() > 0;
            }
        }
        public bool TieneUsuariosAsignados(int idPermiso)
        {
            string query = "SELECT COUNT(*) FROM Usuario_Permiso WHERE CodPermiso = @id";
            using (SqlConnection con = new SqlConnection(cadenaConexion))
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@id", idPermiso);
                con.Open();
                return (int)cmd.ExecuteScalar() > 0;
            }
        }
        public List<Perfil> ObtenerTodosLosComponentes(bool soloFamilias, bool soloPerfiles)
        {
            List<Perfil> lista = new List<Perfil>();


            string query = @"SELECT CodPermiso, Nombre, EsFamilia 
                     FROM Permiso 
                     WHERE EsFamilia = @EsFamilia AND EsPerfil = @EsPerfil";

            using (SqlConnection con = new SqlConnection(cadenaConexion))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {

                    cmd.Parameters.AddWithValue("@EsFamilia", soloFamilias ? 1 : 0);
                    cmd.Parameters.AddWithValue("@EsPerfil", soloPerfiles ? 1 : 0);

                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            Perfil p = soloFamilias ? (Perfil)new Familia() : (Perfil)new Permiso();
                            p.Id = Convert.ToInt32(reader["CodPermiso"]);
                            p.Nombre = reader["Nombre"].ToString();
                            lista.Add(p);
                        }
                    }
                }
            }
            return lista;
        }
        public void EliminarRelacionesComponente(int idComponente)
        {
            string query = "DELETE FROM Permiso_Permiso WHERE CodPadre = @Id OR CodHijo = @Id";
            using (SqlConnection con = new SqlConnection(cadenaConexion))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Id", idComponente);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }


        public void EliminarAsignacionesUsuario(int idComponente)
        {
            string query = "DELETE FROM Usuario_Permiso WHERE CodPermiso = @Id";
            using (SqlConnection con = new SqlConnection(cadenaConexion))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Id", idComponente);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }


        public void EliminarComponenteBase(int idComponente)
        {
            string query = "DELETE FROM Permiso WHERE CodPermiso = @Id";
            using (SqlConnection con = new SqlConnection(cadenaConexion))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Id", idComponente);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public int InsertarComponente(string nombre, bool esFamilia, bool esPerfil)
        {

            string query = @"INSERT INTO Permiso (Nombre, EsFamilia, EsPerfil) 
                     OUTPUT INSERTED.CodPermiso 
                     VALUES (@Nombre, @EsFamilia, @EsPerfil)";

            using (SqlConnection con = new SqlConnection(cadenaConexion))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Nombre", nombre);
                    cmd.Parameters.AddWithValue("@EsFamilia", esFamilia ? 1 : 0);
                    cmd.Parameters.AddWithValue("@EsPerfil", esPerfil ? 1 : 0);

                    con.Open();
                    return (int)cmd.ExecuteScalar();
                }
            }
        }


        public void EliminarRelacionesPadre(int idPadre)
        {

            string query = "DELETE FROM Permiso_Permiso WHERE CodPadre = @idPadre";

            using (SqlConnection con = new SqlConnection(cadenaConexion))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@idPadre", idPadre);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }


        public void InsertarRelacion(int idPadre, int idHijo)
        {
            string query = "INSERT INTO Permiso_Permiso (CodPadre, CodHijo) VALUES (@CodPadre, @IdHijo)";
            using (SqlConnection con = new SqlConnection(cadenaConexion))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@CodPadre", idPadre);
                    cmd.Parameters.AddWithValue("@IdHijo", idHijo);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
