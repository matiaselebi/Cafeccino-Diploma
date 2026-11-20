using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace CAFECCINO_INGENIERIA_DE_SOFTWARE
{
    // Ayudas comunes para las grillas de las pantallas
    public static class AyudaGrilla
    {
        // Cada vez que la grilla se carga, sus encabezados se muestran sin el sufijo y con espacios:
        // "StockMinimo_389ME" se ve como "Stock Minimo". Las fechas se muestran sin la hora.
        public static void Preparar_389ME(params DataGridView[] grillas_389ME)
        {
            foreach (DataGridView grilla_389ME in grillas_389ME)
                grilla_389ME.DataBindingComplete += (sender_389ME, e_389ME) => FormatearEncabezados_389ME(grilla_389ME);
        }

        public static void FormatearEncabezados_389ME(DataGridView grilla_389ME)
        {
            foreach (DataGridViewColumn columna_389ME in grilla_389ME.Columns)
            {
                columna_389ME.HeaderText = Regex.Replace(columna_389ME.Name.Replace("_389ME", ""), "(?<=[a-z])(?=[A-Z])", " ");
                if (columna_389ME.ValueType == typeof(DateTime))
                    columna_389ME.DefaultCellStyle.Format = "dd/MM/yyyy";
            }
        }

        // El digito verificador es un control interno: no se muestra en las grillas
        public static void OcultarDVH_389ME(params DataGridView[] grillas_389ME)
        {
            foreach (DataGridView grilla_389ME in grillas_389ME)
                grilla_389ME.DataBindingComplete += (sender_389ME, e_389ME) =>
                {
                    foreach (string nombre_389ME in new[] { "DVH", "DVH_389ME" })
                        if (grilla_389ME.Columns.Contains(nombre_389ME))
                            grilla_389ME.Columns[nombre_389ME].Visible = false;
                };
        }

        // Objeto de la fila seleccionada, o null si no hay ninguna
        public static object Seleccionado_389ME(DataGridView grilla_389ME)
        {
            return grilla_389ME.SelectedRows.Count > 0 ? grilla_389ME.SelectedRows[0].DataBoundItem : null;
        }
    }
}
