using System;
using System.Collections.Generic;
using System.Linq;
using BE;
using DAL;
using SERVICIOS;

namespace BLL
{
    public class BLLProveedor
    {
        private readonly DALProveedor dalProveedor_389ME = new DALProveedor();
        private readonly Encriptacion encriptacion_389ME = new Encriptacion();
        private readonly BLLBitacoraEventos bllBitacora_389ME = new BLLBitacoraEventos();
        private readonly BLLDigitoVerificador bllDigito_389ME = new BLLDigitoVerificador();

        public List<Proveedor> Listar_389ME()
        {
            List<Proveedor> lista_389ME = dalProveedor_389ME.Listar_389ME();

            foreach (Proveedor proveedor_389ME in lista_389ME)
                proveedor_389ME.CuentaBancaria_389ME = encriptacion_389ME.Desencriptar_389ME(proveedor_389ME.CuentaBancaria_389ME);

            return lista_389ME;
        }

        public Proveedor ObtenerPorCUIT_389ME(string cuit_389ME)
        {
            Proveedor proveedor_389ME = dalProveedor_389ME.ObtenerPorCUIT_389ME(cuit_389ME);

            if (proveedor_389ME != null)
                proveedor_389ME.CuentaBancaria_389ME = encriptacion_389ME.Desencriptar_389ME(proveedor_389ME.CuentaBancaria_389ME);

            return proveedor_389ME;
        }

        // Proveedor nuevo: si se cargan la direccion y la cuenta bancaria queda registrado;
        // si no, queda pre-registrado hasta que se completen
        public void PreRegistrar_389ME(Proveedor proveedor_389ME)
        {
            Validar_389ME(proveedor_389ME);

            if (dalProveedor_389ME.ObtenerPorCUIT_389ME(proveedor_389ME.CUIT_389ME) != null)
                throw new Exception("Ya existe un proveedor con ese CUIT.");

            dalProveedor_389ME.Insertar_389ME(PrepararDatosDePago_389ME(proveedor_389ME));
            bllDigito_389ME.Recalcular_389ME("Proveedor");

            bllBitacora_389ME.RegistrarEvento_389ME("Pre-registro de proveedor",
                $"CUIT {proveedor_389ME.CUIT_389ME} - {proveedor_389ME.Nombre_389ME}", "Compras", "Baja");
        }

        public void Modificar_389ME(Proveedor proveedor_389ME)
        {
            Validar_389ME(proveedor_389ME);

            // Se lee con la cuenta bancaria descifrada para poder compararla
            Proveedor anterior_389ME = ObtenerPorCUIT_389ME(proveedor_389ME.CUIT_389ME);
            if (anterior_389ME == null)
                throw new Exception("El proveedor no existe.");

            List<string> cambios_389ME = new List<string>();
            BLLBitacoraEventos.AgregarCambio_389ME(cambios_389ME, "Direccion", anterior_389ME.Direccion_389ME ?? "", proveedor_389ME.Direccion_389ME);
            // La cuenta bancaria es un dato sensible: se registra que cambio, pero no su valor
            if ((anterior_389ME.CuentaBancaria_389ME ?? "") != proveedor_389ME.CuentaBancaria_389ME)
                cambios_389ME.Add("Cuenta bancaria modificada");

            dalProveedor_389ME.Modificar_389ME(PrepararDatosDePago_389ME(proveedor_389ME));
            bllDigito_389ME.Recalcular_389ME("Proveedor");

            BLLBitacoraEventos.AgregarCambio_389ME(cambios_389ME, "Nombre", anterior_389ME.Nombre_389ME, proveedor_389ME.Nombre_389ME);
            BLLBitacoraEventos.AgregarCambio_389ME(cambios_389ME, "Razon social", anterior_389ME.RazonSocial_389ME, proveedor_389ME.RazonSocial_389ME);
            BLLBitacoraEventos.AgregarCambio_389ME(cambios_389ME, "Telefono", anterior_389ME.Telefono_389ME, proveedor_389ME.Telefono_389ME);
            BLLBitacoraEventos.AgregarCambio_389ME(cambios_389ME, "Email", anterior_389ME.Email_389ME, proveedor_389ME.Email_389ME);

            bllBitacora_389ME.RegistrarEvento_389ME("Modificación de proveedor",
                $"CUIT {proveedor_389ME.CUIT_389ME}: " + (cambios_389ME.Count == 0 ? "sin cambios" : string.Join("; ", cambios_389ME)),
                "Compras", "Media");
        }

        // Queda registrado solo si tiene direccion y cuenta bancaria; la cuenta se guarda cifrada,
        // igual que la direccion de los clientes
        private Proveedor PrepararDatosDePago_389ME(Proveedor proveedor_389ME)
        {
            proveedor_389ME.Registrado_389ME = proveedor_389ME.Direccion_389ME != "" && proveedor_389ME.CuentaBancaria_389ME != "";
            proveedor_389ME.CuentaBancaria_389ME = proveedor_389ME.CuentaBancaria_389ME == "" ? null
                : encriptacion_389ME.Encriptar_389ME(proveedor_389ME.CuentaBancaria_389ME);
            if (proveedor_389ME.Direccion_389ME == "")
                proveedor_389ME.Direccion_389ME = null;
            return proveedor_389ME;
        }

        private void Validar_389ME(Proveedor proveedor_389ME)
        {
            if (!Validador.EsNumero_389ME(proveedor_389ME.CUIT_389ME, 11, 11))
                throw new Exception("El CUIT debe tener 11 números.");
            if (string.IsNullOrWhiteSpace(proveedor_389ME.Nombre_389ME) || string.IsNullOrWhiteSpace(proveedor_389ME.RazonSocial_389ME))
                throw new Exception("El nombre y la razon social son obligatorios.");
            if (!Validador.EsNumero_389ME(proveedor_389ME.Telefono_389ME, 8, 15))
                throw new Exception("El teléfono debe tener entre 8 y 15 números.");
            if (!Validador.EsMail_389ME(proveedor_389ME.Email_389ME))
                throw new Exception("El email no es válido. Ejemplo: ventas@proveedor.com");

            // Los datos de pago son opcionales, pero si se carga uno hay que cargar los dos
            proveedor_389ME.Direccion_389ME = proveedor_389ME.Direccion_389ME ?? "";
            proveedor_389ME.CuentaBancaria_389ME = proveedor_389ME.CuentaBancaria_389ME ?? "";
            if ((proveedor_389ME.Direccion_389ME == "") != (proveedor_389ME.CuentaBancaria_389ME == ""))
                throw new Exception("Complete la dirección y la cuenta bancaria, o deje las dos vacías para pre-registrarlo.");
            if (proveedor_389ME.CuentaBancaria_389ME != "" && !Validador.EsNumero_389ME(proveedor_389ME.CuentaBancaria_389ME, 22, 22))
                throw new Exception("La cuenta bancaria debe ser un CBU de 22 números.");
        }
    }
}
