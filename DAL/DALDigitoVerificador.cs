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
        private string _conn = @"Data Source=localhost;Initial Catalog=Cafeccino;Integrated Security=True";

        public DigitoVerificador ObtenerPorTabla(string tabla)
        {
            using (SqlConnection cn = new SqlConnection(_conn))
            using (SqlCommand cmd = new SqlCommand(
                "SELECT Tabla, DVV FROM DigitoVerificador WHERE Tabla=@t", cn))
            {
                cmd.Parameters.AddWithValue("@t", tabla);
                cn.Open();
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    if (!rd.Read()) return null;
                    return new DigitoVerificador
                    {
                        Tabla = rd.GetString(0),
                      
                        DVV = rd.GetString(1)
                    };
                }
            }
        }

        public void Guardar(DigitoVerificador dv)
        {
            using (SqlConnection cn = new SqlConnection(_conn))
            using (SqlCommand cmd = new SqlCommand(@"
        MERGE DigitoVerificador AS target
        USING (SELECT @tabla AS Tabla) AS source 
        ON target.Tabla = source.Tabla

        WHEN MATCHED THEN 
            UPDATE SET DVV = @dvv

        WHEN NOT MATCHED THEN 
            INSERT (Tabla, DVV) 
            VALUES (@tabla, @dvv);",
                cn))
            {
                cmd.Parameters.AddWithValue("@tabla", dv.Tabla);
                cmd.Parameters.AddWithValue("@dvv", dv.DVV);

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}