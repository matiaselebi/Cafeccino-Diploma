using System;
using System.Collections.Generic;
using System.Data;
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
        private string cadenaConexion = Conexion.cadena;

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

        // Las tablas salen de la lista fija de BLLDigitoVerificador, nunca de lo que escribe el usuario
        public List<string> ObtenerClaves_389ME(string tabla_389ME)
        {
            List<string> claves_389ME = new List<string>();
            string query_389ME = @"SELECT k.COLUMN_NAME FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS t
                                   INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE k ON k.CONSTRAINT_NAME = t.CONSTRAINT_NAME
                                   WHERE t.TABLE_NAME = @tabla AND t.CONSTRAINT_TYPE = 'PRIMARY KEY' ORDER BY k.ORDINAL_POSITION";

            using (SqlConnection connection_389ME = new SqlConnection(cadenaConexion))
            using (SqlCommand command_389ME = new SqlCommand(query_389ME, connection_389ME))
            {
                command_389ME.Parameters.AddWithValue("@tabla", tabla_389ME);
                connection_389ME.Open();
                using (SqlDataReader datareader_389ME = command_389ME.ExecuteReader())
                    while (datareader_389ME.Read())
                        claves_389ME.Add(datareader_389ME.GetString(0));
            }
            return claves_389ME;
        }

        // Todas las filas ordenadas por la clave primaria, asi el DVV siempre se calcula en el mismo orden
        public DataTable ObtenerFilas_389ME(string tabla_389ME)
        {
            string orden_389ME = string.Join(", ", ObtenerClaves_389ME(tabla_389ME).Select(clave_389ME => "[" + clave_389ME + "]"));
            DataTable filas_389ME = new DataTable();

            using (SqlConnection connection_389ME = new SqlConnection(cadenaConexion))
            using (SqlDataAdapter adaptador_389ME = new SqlDataAdapter("SELECT * FROM [" + tabla_389ME + "] ORDER BY " + orden_389ME, connection_389ME))
                adaptador_389ME.Fill(filas_389ME);

            return filas_389ME;
        }

        public void ActualizarDVH_389ME(string tabla_389ME, string columnaDVH_389ME, List<string> claves_389ME, DataRow fila_389ME, int dvh_389ME)
        {
            string condicion_389ME = string.Join(" AND ", claves_389ME.Select((clave_389ME, i_389ME) => "[" + clave_389ME + "] = @k" + i_389ME));
            string query_389ME = "UPDATE [" + tabla_389ME + "] SET [" + columnaDVH_389ME + "] = @dvh WHERE " + condicion_389ME;

            using (SqlConnection connection_389ME = new SqlConnection(cadenaConexion))
            using (SqlCommand command_389ME = new SqlCommand(query_389ME, connection_389ME))
            {
                command_389ME.Parameters.AddWithValue("@dvh", dvh_389ME);
                for (int i_389ME = 0; i_389ME < claves_389ME.Count; i_389ME++)
                    command_389ME.Parameters.AddWithValue("@k" + i_389ME, fila_389ME[claves_389ME[i_389ME]]);
                connection_389ME.Open();
                command_389ME.ExecuteNonQuery();
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