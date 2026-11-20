using System.Globalization;
using System.Windows.Forms;

namespace CAFECCINO_INGENIERIA_DE_SOFTWARE
{
    // Ayudas para controlar lo que se escribe en los TextBox
    public static class Validacion
    {
        // Campos numericos (DNI, telefono, CUIT, tarjeta, codigo de seguridad, cantidades enteras)
        public static void SoloNumeros_389ME(object sender_389ME, KeyPressEventArgs e_389ME)
        {
            e_389ME.Handled = !char.IsDigit(e_389ME.KeyChar) && !char.IsControl(e_389ME.KeyChar);
        }

        // Nombres y apellidos: letras y espacios
        public static void SoloLetras_389ME(object sender_389ME, KeyPressEventArgs e_389ME)
        {
            e_389ME.Handled = !char.IsLetter(e_389ME.KeyChar) && e_389ME.KeyChar != ' ' && !char.IsControl(e_389ME.KeyChar);
        }

        // Precios y cantidades con decimales: numeros y una sola coma o punto
        public static void SoloDecimales_389ME(object sender_389ME, KeyPressEventArgs e_389ME)
        {
            string texto_389ME = ((TextBox)sender_389ME).Text;
            bool separador_389ME = (e_389ME.KeyChar == ',' || e_389ME.KeyChar == '.') && texto_389ME.IndexOfAny(new[] { ',', '.' }) < 0;
            e_389ME.Handled = !char.IsDigit(e_389ME.KeyChar) && !separador_389ME && !char.IsControl(e_389ME.KeyChar);
        }

        // Acepta coma o punto como separador decimal, sin depender de la configuracion regional de Windows
        // (en una PC argentina double.TryParse("1.5") devuelve 15). No acepta negativos ni letras.
        public static bool LeerDecimal_389ME(string texto_389ME, out double numero_389ME)
        {
            return double.TryParse(texto_389ME.Trim().Replace(',', '.'), NumberStyles.AllowDecimalPoint,
                                   CultureInfo.InvariantCulture, out numero_389ME);
        }
    }
}
