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
            if (value is int i) return i;
            if (value is bool b) return b ? 1 : 0;
            if (value is string s) return s.Sum(c => (int)c);
            return Convert.ToInt32(value);
        }

        public static int CalcularDVH(Usuario u)
        {
            var valores = new object[]
            {
                u.DNI,
                u.NombreUsuario,
                u.Contraseña,
                u.Nombre,
                u.Apellido,
                u.Mail,
                u.NumTelefono,
                u.Rol
            };

            int suma = 0;
            for (int i = 0; i < valores.Length; i++)
                suma += ToNumeric(valores[i]) * (i + 1);

            return suma % 1000;
        }

        public static int CalcularDVV(List<int> listaDVH)
        {
            int suma = 0;
            for (int i = 0; i < listaDVH.Count; i++)
                suma += listaDVH[i] * (i + 1);

            return suma % 1000;
        }

        public static DigitoVerificador Recalcular(List<Usuario> usuarios)
        {
            List<int> listaDVH = new List<int>();

            foreach (Usuario u in usuarios)
            {
                int dvh = CalcularDVH(u);

                
                u.DVH = dvh;

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

            foreach (Usuario u in usuarios)
            {
                int dvhCalculado = CalcularDVH(u);

                dvhActual.Add(dvhCalculado);

                
                if (dvhCalculado != u.DVH)
                {
                    filasAlteradas.Add(u.DNI);
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