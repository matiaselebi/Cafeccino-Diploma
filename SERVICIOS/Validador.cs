using System.Linq;
using System.Text.RegularExpressions;

namespace SERVICIOS
{
    // Reglas de formato que usan las clases de negocio antes de guardar los datos
    public static class Validador
    {
        // Formato nombre@dominio.com, sin espacios
        public static bool EsMail_389ME(string mail_389ME)
        {
            return mail_389ME != null && Regex.IsMatch(mail_389ME, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }

        // Solo letras (con tildes y ñ) y espacios, como un nombre o un apellido
        public static bool EsTexto_389ME(string texto_389ME)
        {
            return !string.IsNullOrWhiteSpace(texto_389ME) &&
                   texto_389ME.All(caracter_389ME => char.IsLetter(caracter_389ME) || caracter_389ME == ' ');
        }

        // Solo numeros, con una cantidad de digitos entre minimo y maximo (CUIT, CBU, telefono)
        public static bool EsNumero_389ME(string texto_389ME, int minimo_389ME, int maximo_389ME)
        {
            return texto_389ME != null && texto_389ME.Length >= minimo_389ME && texto_389ME.Length <= maximo_389ME &&
                   texto_389ME.All(caracter_389ME => caracter_389ME >= '0' && caracter_389ME <= '9');
        }
    }
}
