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
    public class DALIdioma
    {
        SqlConnection cn = new SqlConnection(@"Data Source=localhost;Initial Catalog=Cafeccino;Integrated Security=True");
        
        public DataTable ListarIdiomas()
        {
            cn.Open();

            SqlDataAdapter da = new SqlDataAdapter(
                "SELECT IdIdioma, Nombre FROM Idioma",
                cn);

            DataTable dt = new DataTable();

            da.Fill(dt);

            cn.Close();

            return dt;
        }
        public void AgregarIdioma(Idioma idioma)
        {
            cn.Open();

            string query =  @" IF NOT EXISTS( SELECT * FROM Idioma WHERE Nombre=@nombre)
                            BEGIN INSERT INTO Idioma(Nombre) VALUES(@nombre) END"; ;

            SqlCommand cmd = new SqlCommand(query, cn);

            cmd.Parameters.AddWithValue("@nombre", idioma.Nombre);

            cmd.ExecuteNonQuery();

            cn.Close();
        }

        public void AgregarControl(ControlIdioma control)
        {
            cn.Open();

            SqlCommand cmd =
                new SqlCommand(
                @"INSERT INTO ControlIdioma
        (Formulario,NombreControl)
        VALUES(@form,@control)", cn);

            cmd.Parameters.AddWithValue("@form", control.Formulario);

            cmd.Parameters.AddWithValue("@control", control.NombreControl);

            cmd.ExecuteNonQuery();

            cn.Close();
        }
        public DataTable ObtenerTraducciones(string formulario, int idIdioma)
        {
            cn.Open();

            string query = @"SELECT c.IdControl,c.NombreControl AS Control,c.TextoOriginal,ISNULL(t.Texto,'') AS Traduccion
                            FROM ControlIdioma c LEFT JOIN Traduccion t ON c.IdControl = t.IdControl
                            AND t.IdIdioma = @idIdioma WHERE c.Formulario = @formulario";

            SqlCommand cmd = new SqlCommand(query, cn);

            cmd.Parameters.AddWithValue("@formulario", formulario);
            cmd.Parameters.AddWithValue("@idIdioma", idIdioma);

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();

            da.Fill(dt);

            cn.Close();

            return dt;
        }
        public string ObtenerTexto(int idIdioma,
                           string formulario,
                           string control)
        {
            cn.Open();

            string query = @"SELECT Texto FROM Traduccion t INNER JOIN ControlIdioma c ON c.IdControl=t.IdControl
                            WHERE t.IdIdioma=@idioma AND c.Formulario=@form AND c.NombreControl=@control";

            SqlCommand cmd =
                new SqlCommand(query, cn);

            cmd.Parameters.AddWithValue("@idioma", idIdioma);

            cmd.Parameters.AddWithValue("@form", formulario);

            cmd.Parameters.AddWithValue("@control", control);

            object resultado =
                cmd.ExecuteScalar();

            cn.Close();

            if (resultado == null)
                return "";

            return resultado.ToString();
        }
        public void GuardarTraduccion(Traduccion traduccion)
        {
            cn.Open();

            string query = @" IF EXISTS ( SELECT *FROM Traduccion WHERE IdIdioma=@idioma AND IdControl=@control)
                            BEGIN UPDATE Traduccion SET Texto=@texto WHERE IdIdioma=@idioma AND IdControl=@control
                            END
                            ELSE
                            BEGIN
                            INSERT INTO Traduccion (IdIdioma, IdControl, Texto)
                            VALUES (@idioma, @control, @texto)
                            END";

            SqlCommand cmd =
                new SqlCommand(query, cn);

            cmd.Parameters.AddWithValue("@idioma",
                traduccion.Idioma);

            cmd.Parameters.AddWithValue("@control",
                traduccion.Control);

            cmd.Parameters.AddWithValue("@texto",
                traduccion.Texto);

            cmd.ExecuteNonQuery();

            cn.Close();
        }
        public void RegistrarControl(string formulario, string nombreControl, string textoOriginal)
        {
            cn.Open();

            string query = @" IF NOT EXISTS( SELECT * FROM ControlIdioma WHERE Formulario=@formulario
                            AND NombreControl=@nombreControl)
                            BEGIN INSERT INTO ControlIdioma(Formulario,NombreControl,TextoOriginal)
                            VALUES(@formulario,@nombreControl,@textoOriginal)
                            END";

            SqlCommand cmd = new SqlCommand(query, cn);

            cmd.Parameters.AddWithValue("@formulario", formulario);
            cmd.Parameters.AddWithValue("@nombreControl", nombreControl);
            cmd.Parameters.AddWithValue("@textoOriginal", textoOriginal);

            cmd.ExecuteNonQuery();

            cn.Close();
        }
        public DataTable ObtenerControles(string formulario)
        {
            cn.Open();

            string query = @"SELECT IdControl,
                            NombreControl AS Control,
                            TextoOriginal
                     FROM ControlIdioma
                     WHERE Formulario = @formulario";

            SqlCommand cmd = new SqlCommand(query, cn);
            cmd.Parameters.AddWithValue("@formulario", formulario);

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();
            da.Fill(dt);

            cn.Close();

            return dt;
        }
    }
}
