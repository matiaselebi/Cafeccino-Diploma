using System;
using System.Data.SqlClient;
using System.IO;

namespace DAL
{
    public class DALBackup
    {
        private const string NombreBaseDatos = "Cafeccino";
        private static readonly string CarpetaSolucion = ObtenerCarpetaSolucion();
        private string rutaBackup_389ME;

        private static string ObtenerCarpetaSolucion()
        {
            // Sube desde la carpeta de ejecucion (bin\Debug\...) hasta encontrar CAFECCINO.sln.
            // Mas robusto que contar niveles fijos: no depende de Debug/Release/x64/etc.
            DirectoryInfo dir = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
            while (dir != null && !File.Exists(Path.Combine(dir.FullName, "CAFECCINO.sln")))
                dir = dir.Parent;

            return dir?.FullName;
        }

        private readonly string conexionMaster =
            new SqlConnectionStringBuilder(Conexion.cadena) { InitialCatalog = "master" }.ConnectionString;

        // Con la solucion (PC de desarrollo) el backup va a su carpeta Backups.
        // Instalado no hay solucion: se usa la carpeta de backups de SQL Server, porque quien escribe el archivo es
        // el servicio de SQL Server y en Archivos de programa no tiene permiso.
        public string ObtenerRutaBackup()
        {
            if (rutaBackup_389ME == null)
                rutaBackup_389ME = CarpetaSolucion != null
                    ? Path.Combine(CarpetaSolucion, "Backups", "Cafeccino.bak")
                    : Path.Combine(ObtenerCarpetaBackupsSql_389ME(), "Cafeccino.bak");
            return rutaBackup_389ME;
        }

        private string ObtenerCarpetaBackupsSql_389ME()
        {
            using (SqlConnection connection_389ME = new SqlConnection(conexionMaster))
            using (SqlCommand command_389ME = new SqlCommand(
                @"DECLARE @carpeta nvarchar(4000) = CAST(SERVERPROPERTY('InstanceDefaultBackupPath') AS nvarchar(4000));
                  IF @carpeta IS NULL
                      EXEC master.dbo.xp_instance_regread N'HKEY_LOCAL_MACHINE', N'Software\Microsoft\MSSQLServer\MSSQLServer', N'BackupDirectory', @carpeta OUTPUT;
                  SELECT @carpeta;", connection_389ME))
            {
                connection_389ME.Open();
                return Convert.ToString(command_389ME.ExecuteScalar());
            }
        }

        // Se le pregunta a SQL Server, que es quien lee el archivo: el programa puede no tener permiso sobre esa carpeta
        public bool ExisteBackup()
        {
            using (SqlConnection connection_389ME = new SqlConnection(conexionMaster))
            using (SqlCommand command_389ME = new SqlCommand("EXEC master.dbo.xp_fileexist @ruta", connection_389ME))
            {
                command_389ME.Parameters.AddWithValue("@ruta", ObtenerRutaBackup());
                connection_389ME.Open();
                using (SqlDataReader reader_389ME = command_389ME.ExecuteReader())
                    return reader_389ME.Read() && Convert.ToInt32(reader_389ME[0]) == 1;
            }
        }

        public void GenerarBackup()
        {
            // La carpeta Backups de la solucion puede no existir todavia; la de SQL Server ya existe
            if (CarpetaSolucion != null)
                Directory.CreateDirectory(Path.GetDirectoryName(ObtenerRutaBackup()));

            using (SqlConnection connection = new SqlConnection(conexionMaster))
            using (SqlCommand command = new SqlCommand(
                $"BACKUP DATABASE [{NombreBaseDatos}] TO DISK = @ruta WITH INIT, STATS = 10", connection))
            {
                command.Parameters.AddWithValue("@ruta", ObtenerRutaBackup());
                command.CommandTimeout = 120;
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void RestaurarBackup()
        {
            if (!ExisteBackup())
                throw new Exception("No existe un backup generado todavía. Use 'Recalcular' al menos una vez para generar uno.");

            using (SqlConnection connection = new SqlConnection(conexionMaster))
            {
                connection.Open();

                EjecutarNonQuery(connection, $"ALTER DATABASE [{NombreBaseDatos}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE");

                try
                {
                    using (SqlCommand command = new SqlCommand(
                        $"RESTORE DATABASE [{NombreBaseDatos}] FROM DISK = @ruta WITH REPLACE, STATS = 10", connection))
                    {
                        command.Parameters.AddWithValue("@ruta", ObtenerRutaBackup());
                        command.CommandTimeout = 180;
                        command.ExecuteNonQuery();
                    }
                }
                finally
                {
                    EjecutarNonQuery(connection, $"ALTER DATABASE [{NombreBaseDatos}] SET MULTI_USER");
                }
            }

            SqlConnection.ClearAllPools();
        }

        private void EjecutarNonQuery(SqlConnection connection, string sql)
        {
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                command.ExecuteNonQuery();
            }
        }
    }
}
