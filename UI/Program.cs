using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CAFECCINO_INGENIERIA_DE_SOFTWARE
{
    internal static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Si algun error no se controlo en la pantalla, se informa y el programa sigue abierto
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.ThreadException += (sender_389ME, e_389ME) =>
                MessageBox.Show("Ocurrió un error inesperado: " + e_389ME.Exception.Message, "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);

            Application.Run(new frmInicio());
        }
    }
}
