using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using BE;

namespace DAL
{
    public class DALDigitoVerificador
    {
        private string cadenaConexion = @"Data Source=localhost;Initial Catalog=Cafeccino;Integrated Security=True";

        public DigitoVerificador ObtenerPorTabla(string tabla)
        {
            using (SqlConnection connection = new SqlConnection(cadenaConexion))
            using (SqlCommand command = new SqlCommand(
                "SELECT Tabla, DVV FROM DigitoVerificador WHERE Tabla=@t", connection))
            {
                command.Parameters.AddWithValue("@t", tabla);
                connection.Open();
                using (SqlDataReader datareader = command.ExecuteReader())
                {
                    if (!datareader.Read()) return null;
                    return new DigitoVerificador
                    {
                        Tabla = datareader.GetString(0),
                      
                        DVV = datareader.GetString(1)
                    };
                }
            }
        }

        public void Guardar(DigitoVerificador dv)
        {
            using (SqlConnection connection = new SqlConnection(cadenaConexion))
            using (SqlCommand command = new SqlCommand(@"
        MERGE DigitoVerificador AS target
        USING (SELECT @tabla AS Tabla) AS source 
        ON target.Tabla = source.Tabla

        WHEN MATCHED THEN 
            UPDATE SET DVV = @dvv

        WHEN NOT MATCHED THEN 
            INSERT (Tabla, DVV) 
            VALUES (@tabla, @dvv);",
                connection))
            {
                command.Parameters.AddWithValue("@tabla", dv.Tabla);
                command.Parameters.AddWithValue("@dvv", dv.DVV);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}