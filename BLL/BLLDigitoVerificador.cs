using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using BE;
using DAL;
using SERVICIOS;

namespace BLL
{
    // Digitos verificadores de todas las tablas, salvo Usuario, que tiene su propio calculo
    // (BLLUsuario.SincronizarDigitos) porque lo usa la auditoria de cambios.
    //   DVH: se calcula con los valores de cada fila.
    //   DVV: se calcula con los DVH de todas las filas de la tabla y se guarda en DigitoVerificador.
    public class BLLDigitoVerificador
    {
        public static readonly string[] Tablas_389ME =
        {
            "Cliente", "Producto", "Factura", "FacturaDetalle",
            "Insumo", "Receta", "ConsumoInsumo", "Proveedor", "SolicitudCotizacion", "SolicitudInsumo",
            "SolicitudProveedor", "Cotizacion", "OrdenCompra", "OrdenCompraDetalle",
            "Permiso", "Permiso_Permiso", "Usuario_Permiso", "Idioma", "ControlIdioma", "Traduccion",
            "BitacoraEventos", "AuditoriaCambios"
        };

        private readonly DALDigitoVerificador dalDigitoVerificador_389ME = new DALDigitoVerificador();

        // Se llama despues de cada alta, baja o modificacion con las tablas que se tocaron
        public void Recalcular_389ME(params string[] tablas_389ME)
        {
            foreach (string tabla_389ME in tablas_389ME)
                RecalcularTabla_389ME(tabla_389ME);
        }

        public void RecalcularTodo_389ME()
        {
            Recalcular_389ME(Tablas_389ME);
        }

        // Devuelve un mensaje por cada tabla alterada; si la lista esta vacia, todo esta bien
        public List<string> Verificar_389ME()
        {
            List<string> alteradas_389ME = new List<string>();

            foreach (string tabla_389ME in Tablas_389ME)
            {
                DigitoVerificador guardado_389ME = dalDigitoVerificador_389ME.ObtenerPorTabla(tabla_389ME);

                // Si la tabla todavia no tiene digitos (por ejemplo, recien creada con el script), se calculan por primera vez
                if (guardado_389ME == null)
                {
                    RecalcularTabla_389ME(tabla_389ME);
                    continue;
                }

                DataTable filas_389ME = dalDigitoVerificador_389ME.ObtenerFilas_389ME(tabla_389ME);
                string columnaDVH_389ME = ColumnaDVH_389ME(filas_389ME);
                List<int> dvhCalculados_389ME = new List<int>();
                int filasAlteradas_389ME = 0;

                foreach (DataRow fila_389ME in filas_389ME.Rows)
                {
                    int dvh_389ME = CalcularDVH_389ME(fila_389ME, columnaDVH_389ME);
                    dvhCalculados_389ME.Add(dvh_389ME);
                    if (fila_389ME[columnaDVH_389ME] == DBNull.Value || Convert.ToInt32(fila_389ME[columnaDVH_389ME]) != dvh_389ME)
                        filasAlteradas_389ME++;
                }

                if (filasAlteradas_389ME > 0)
                    alteradas_389ME.Add($"{tabla_389ME}: {filasAlteradas_389ME} fila(s) modificada(s) por fuera del sistema.");
                else if (DigitVerifierService.CalcularDVV(dvhCalculados_389ME) != guardado_389ME.ObtenerDVV())
                    alteradas_389ME.Add($"{tabla_389ME}: se agregaron o borraron filas por fuera del sistema.");
            }

            return alteradas_389ME;
        }

        private void RecalcularTabla_389ME(string tabla_389ME)
        {
            DataTable filas_389ME = dalDigitoVerificador_389ME.ObtenerFilas_389ME(tabla_389ME);
            string columnaDVH_389ME = ColumnaDVH_389ME(filas_389ME);
            List<string> claves_389ME = dalDigitoVerificador_389ME.ObtenerClaves_389ME(tabla_389ME);
            List<int> dvhCalculados_389ME = new List<int>();

            foreach (DataRow fila_389ME in filas_389ME.Rows)
            {
                int dvh_389ME = CalcularDVH_389ME(fila_389ME, columnaDVH_389ME);
                dvhCalculados_389ME.Add(dvh_389ME);

                // Solo se actualizan las filas cuyo DVH cambio (por ejemplo, la que se acaba de agregar)
                if (fila_389ME[columnaDVH_389ME] == DBNull.Value || Convert.ToInt32(fila_389ME[columnaDVH_389ME]) != dvh_389ME)
                    dalDigitoVerificador_389ME.ActualizarDVH_389ME(tabla_389ME, columnaDVH_389ME, claves_389ME, fila_389ME, dvh_389ME);
            }

            dalDigitoVerificador_389ME.Guardar(new DigitoVerificador
            {
                Tabla = tabla_389ME,
                DVV = DigitVerifierService.CalcularDVV(dvhCalculados_389ME).ToString()
            });
        }

        // El DVH se calcula con todas las columnas de la fila menos la del propio DVH
        private int CalcularDVH_389ME(DataRow fila_389ME, string columnaDVH_389ME)
        {
            List<object> valores_389ME = new List<object>();
            foreach (DataColumn columna_389ME in fila_389ME.Table.Columns)
                if (columna_389ME.ColumnName != columnaDVH_389ME)
                    valores_389ME.Add(fila_389ME[columna_389ME]);

            return DigitVerifierService.CalcularDVH(valores_389ME.ToArray());
        }

        private string ColumnaDVH_389ME(DataTable filas_389ME)
        {
            return filas_389ME.Columns.Contains("DVH_389ME") ? "DVH_389ME" : "DVH";
        }
    }
}
