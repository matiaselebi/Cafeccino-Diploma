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
        SqlConnection connection = new SqlConnection(@"Data Source=localhost;Initial Catalog=Cafeccino;Integrated Security=True");
        
        public DataTable ListarIdiomas()
        {
            connection.Open();

            SqlDataAdapter da = new SqlDataAdapter(
                "SELECT IdIdioma, Nombre FROM Idioma",
                connection);

            DataTable dt = new DataTable();

            da.Fill(dt);

            connection.Close();

            return dt;
        }
        public void AgregarIdioma(Idioma idioma)
        {
            connection.Open();

            string query =  @" IF NOT EXISTS( SELECT * FROM Idioma WHERE Nombre=@nombre)
                            BEGIN INSERT INTO Idioma(Nombre) VALUES(@nombre) END"; ;

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@nombre", idioma.Nombre);

            command.ExecuteNonQuery();

            connection.Close();
        }

        public void AgregarControl(ControlIdioma control)
        {
            connection.Open();

            SqlCommand command =
                new SqlCommand(
                @"INSERT INTO ControlIdioma
        (Formulario,NombreControl)
        VALUES(@form,@control)", connection);

            command.Parameters.AddWithValue("@form", control.Formulario);

            command.Parameters.AddWithValue("@control", control.NombreControl);

            command.ExecuteNonQuery();

            connection.Close();
        }
        public DataTable ObtenerTraducciones(string formulario, int idIdioma)
        {
            connection.Open();

            string query = @"SELECT c.IdControl,c.NombreControl AS Control,c.TextoOriginal,ISNULL(t.Texto,'') AS Traduccion
                            FROM ControlIdioma c LEFT JOIN Traduccion t ON c.IdControl = t.IdControl
                            AND t.IdIdioma = @idIdioma WHERE c.Formulario = @formulario";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@formulario", formulario);
            command.Parameters.AddWithValue("@idIdioma", idIdioma);

            SqlDataAdapter da = new SqlDataAdapter(command);

            DataTable dt = new DataTable();

            da.Fill(dt);

            connection.Close();

            return dt;
        }
        public string ObtenerTexto(int idIdioma,
                           string formulario,
                           string control)
        {
            connection.Open();

            string query = @"SELECT Texto FROM Traduccion t INNER JOIN ControlIdioma c ON c.IdControl=t.IdControl
                            WHERE t.IdIdioma=@idioma AND c.Formulario=@form AND c.NombreControl=@control";

            SqlCommand command =
                new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@idioma", idIdioma);

            command.Parameters.AddWithValue("@form", formulario);

            command.Parameters.AddWithValue("@control", control);

            object resultado =
                command.ExecuteScalar();

            connection.Close();

            if (resultado == null)
                return "";

            return resultado.ToString();
        }
        public void GuardarTraduccion(Traduccion traduccion)
        {
            connection.Open();

            string query = @" IF EXISTS ( SELECT *FROM Traduccion WHERE IdIdioma=@idioma AND IdControl=@control)
                            BEGIN UPDATE Traduccion SET Texto=@texto WHERE IdIdioma=@idioma AND IdControl=@control
                            END
                            ELSE
                            BEGIN
                            INSERT INTO Traduccion (IdIdioma, IdControl, Texto)
                            VALUES (@idioma, @control, @texto)
                            END";

            SqlCommand command =
                new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@idioma",
                traduccion.Idioma);

            command.Parameters.AddWithValue("@control",
                traduccion.Control);

            command.Parameters.AddWithValue("@texto",
                traduccion.Texto);

            command.ExecuteNonQuery();

            connection.Close();
        }
        public void RegistrarControl(string formulario, string nombreControl, string textoOriginal)
        {
            connection.Open();

            string query = @" IF NOT EXISTS( SELECT * FROM ControlIdioma WHERE Formulario=@formulario
                            AND NombreControl=@nombreControl)
                            BEGIN INSERT INTO ControlIdioma(Formulario,NombreControl,TextoOriginal)
                            VALUES(@formulario,@nombreControl,@textoOriginal)
                            END";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@formulario", formulario);
            command.Parameters.AddWithValue("@nombreControl", nombreControl);
            command.Parameters.AddWithValue("@textoOriginal", textoOriginal);

            command.ExecuteNonQuery();

            connection.Close();
        }
        public DataTable ObtenerControles(string formulario)
        {
            connection.Open();

            string query = @"SELECT IdControl,
                            NombreControl AS Control,
                            TextoOriginal
                     FROM ControlIdioma
                     WHERE Formulario = @formulario";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@formulario", formulario);

            SqlDataAdapter da = new SqlDataAdapter(command);

            DataTable dt = new DataTable();
            da.Fill(dt);

            connection.Close();

            return dt;
        }
    }
}
