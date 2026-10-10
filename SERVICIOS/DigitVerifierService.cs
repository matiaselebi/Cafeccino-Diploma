using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace SERVICIOS
{
    public static class DigitVerifierService
    {
        private static int ToNumeric(object value)
        {
            if (value == null) return 0;
            if (value is int entero) return entero;
            if (value is bool booleano) return booleano ? 1 : 0;
            if (value is string texto) return texto.Sum(caracter => (int)caracter);
            return Convert.ToInt32(value);
        }

        public static int CalcularDVH(Usuario usuario)
        {
            var valores = new object[]
            {
                usuario.DNI,
                usuario.NombreUsuario,
                usuario.Contraseña,
                usuario.Nombre,
                usuario.Apellido,
                usuario.Mail,
                usuario.NumTelefono,
                usuario.Rol
            };

            int suma = 0;
            for (int posicion = 0; posicion < valores.Length; posicion++)
                suma += ToNumeric(valores[posicion]) * (posicion + 1);

            return suma % 1000;
        }

        public static int CalcularDVV(List<int> listaDVH)
        {
            int suma = 0;
            for (int posicion = 0; posicion < listaDVH.Count; posicion++)
                suma += listaDVH[posicion] * (posicion + 1);

            return suma % 1000;
        }

        public static DigitoVerificador Recalcular(List<Usuario> usuarios)
        {
            List<int> listaDVH = new List<int>();

            foreach (Usuario usuario in usuarios)
            {
                int dvh = CalcularDVH(usuario);

                
                usuario.DVH = dvh;

                listaDVH.Add(dvh);
            }

            int dvv = CalcularDVV(listaDVH);

            return new DigitoVerificador
            {
                Tabla = "Usuario",
                DVV = dvv.ToString()
            };
        }

        public static bool Validar(List<Usuario> usuarios, DigitoVerificador dvGuardado)
        {
            if (dvGuardado == null)
                return false;

            return ObtenerFilasAlteradas(usuarios, dvGuardado).Count == 0;
        }
        public static List<int> ObtenerFilasAlteradas(List<Usuario> usuarios, DigitoVerificador dvGuardado)
        {
            List<int> filasAlteradas = new List<int>();

            if (dvGuardado == null)
                return filasAlteradas;


            List<int> dvhActual = new List<int>();

            foreach (Usuario usuario in usuarios)
            {
                int dvhCalculado = CalcularDVH(usuario);

                dvhActual.Add(dvhCalculado);

                
                if (dvhCalculado != usuario.DVH)
                {
                    filasAlteradas.Add(usuario.DNI);
                }
            }

            int dvvActual = CalcularDVV(dvhActual);

            int dvvGuardado = dvGuardado.ObtenerDVV();

            if (dvvActual != dvvGuardado)
            {
                filasAlteradas.Add(-2);
            }

            return filasAlteradas;
        }
    }
}