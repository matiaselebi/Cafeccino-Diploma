using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class GenerarHash
    {
        public void ActualizarPasswordHash(string usuario, string hash)
        {
            SqlConnection cn = new SqlConnection(@"Data Source=localhost;Initial Catalog=Cafeccino;Integrated Security=True");

            string query = @"UPDATE Usuario
                     SET Contraseña = @Contraseña
                     WHERE Username = @Nombre";

            SqlCommand cmd = new SqlCommand(query, cn);

            cmd.Parameters.AddWithValue("@Nombre", usuario);
            cmd.Parameters.AddWithValue("@Contraseña", hash);

            cn.Open();
            cmd.ExecuteNonQuery();
            cn.Close();
        }
    }
}
