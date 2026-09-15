using System;
using System.Data.SqlClient;
using System.IO;

namespace DAL
{
    public class DALBackup
    {
        private const string NombreBaseDatos = "Cafeccino";
        private static readonly string RutaBackup = Path.Combine(ObtenerCarpetaSolucion(), "Backups", "Cafeccino.bak");

        private static string ObtenerCarpetaSolucion()
        {
            // Sube desde la carpeta de ejecucion (bin\Debug\...) hasta encontrar CAFECCINO.sln.
            // Mas robusto que contar niveles fijos: no depende de Debug/Release/x64/etc.
            DirectoryInfo dir = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
            while (dir != null && !File.Exists(Path.Combine(dir.FullName, "CAFECCINO.sln")))
                dir = dir.Parent;

            return dir != null ? dir.FullName : AppDomain.CurrentDomain.BaseDirectory;
        }

        private readonly string _conexionMaster =
            @"Data Source=localhost;Initial Catalog=master;Integrated Security=True";

        public string ObtenerRutaBackup() => RutaBackup;

        public bool ExisteBackup() => File.Exists(RutaBackup);

        public void GenerarBackup()
        {
            Directory.CreateDirectory(Path.GetDirectoryName(RutaBackup));

            using (SqlConnection cn = new SqlConnection(_conexionMaster))
            using (SqlCommand cmd = new SqlCommand(
                $"BACKUP DATABASE [{NombreBaseDatos}] TO DISK = @ruta WITH INIT, STATS = 10", cn))
            {
                cmd.Parameters.AddWithValue("@ruta", RutaBackup);
                cmd.CommandTimeout = 120;
                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void RestaurarBackup()
        {
            if (!File.Exists(RutaBackup))
                throw new Exception("No existe un backup generado todavía. Use 'Recalcular' al menos una vez para generar uno.");

            using (SqlConnection cn = new SqlConnection(_conexionMaster))
            {
                cn.Open();

                EjecutarNonQuery(cn, $"ALTER DATABASE [{NombreBaseDatos}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE");

                try
                {
                    using (SqlCommand cmd = new SqlCommand(
                        $"RESTORE DATABASE [{NombreBaseDatos}] FROM DISK = @ruta WITH REPLACE, STATS = 10", cn))
                    {
                        cmd.Parameters.AddWithValue("@ruta", RutaBackup);
                        cmd.CommandTimeout = 180;
                        cmd.ExecuteNonQuery();
                    }
                }
                finally
                {
                    EjecutarNonQuery(cn, $"ALTER DATABASE [{NombreBaseDatos}] SET MULTI_USER");
                }
            }

            SqlConnection.ClearAllPools();
        }

        private void EjecutarNonQuery(SqlConnection cn, string sql)
        {
            using (SqlCommand cmd = new SqlCommand(sql, cn))
            {
                cmd.ExecuteNonQuery();
            }
        }
    }
}
