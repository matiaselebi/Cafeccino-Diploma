using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SERVICIOS
{
    public class Encriptacion
    {
        // AES-256 necesita una clave de exactamente 32 bytes.
        private static readonly byte[] claveAES_389ME = Encoding.UTF8.GetBytes("Cafeccino389ME-Clave-AES-256-32b");

        public string Encriptar_389ME(string texto_389ME)
        {
            if (string.IsNullOrEmpty(texto_389ME)) return texto_389ME;

            using (Aes aes_389ME = Aes.Create())
            {
                aes_389ME.Key = claveAES_389ME;
                aes_389ME.GenerateIV();

                byte[] datos_389ME = Encoding.UTF8.GetBytes(texto_389ME);
                byte[] cifrado_389ME = aes_389ME.CreateEncryptor().TransformFinalBlock(datos_389ME, 0, datos_389ME.Length);

                // Se guarda el IV junto al dato cifrado para poder descifrarlo despues.
                byte[] resultado_389ME = new byte[aes_389ME.IV.Length + cifrado_389ME.Length];
                Buffer.BlockCopy(aes_389ME.IV, 0, resultado_389ME, 0, aes_389ME.IV.Length);
                Buffer.BlockCopy(cifrado_389ME, 0, resultado_389ME, aes_389ME.IV.Length, cifrado_389ME.Length);

                return Convert.ToBase64String(resultado_389ME);
            }
        }

        public string Desencriptar_389ME(string textoCifrado_389ME)
        {
            if (string.IsNullOrEmpty(textoCifrado_389ME)) return textoCifrado_389ME;

            byte[] datos_389ME = Convert.FromBase64String(textoCifrado_389ME);

            using (Aes aes_389ME = Aes.Create())
            {
                aes_389ME.Key = claveAES_389ME;

                byte[] iv_389ME = new byte[16];
                Buffer.BlockCopy(datos_389ME, 0, iv_389ME, 0, iv_389ME.Length);
                aes_389ME.IV = iv_389ME;

                byte[] plano_389ME = aes_389ME.CreateDecryptor()
                    .TransformFinalBlock(datos_389ME, iv_389ME.Length, datos_389ME.Length - iv_389ME.Length);

                return Encoding.UTF8.GetString(plano_389ME);
            }
        }

        public string GenerarHash(string texto)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(texto);
                byte[] hash = sha256.ComputeHash(bytes);

                StringBuilder sb = new StringBuilder();

                foreach (byte valor in hash)
                {
                    sb.Append(valor.ToString("x2"));
                }

                return sb.ToString();
            }
        }
    }
}
