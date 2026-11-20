using System;
using System.Collections.Generic;
using BE;
using DAL;
using SERVICIOS;

namespace BLL
{
    public class BLLCliente
    {
        private readonly DALCliente dalCliente_389ME = new DALCliente();
        private readonly Encriptacion encriptacion_389ME = new Encriptacion();
        private readonly BLLBitacoraEventos bllBitacora_389ME = new BLLBitacoraEventos();
        private readonly BLLDigitoVerificador bllDigito_389ME = new BLLDigitoVerificador();

        public List<Cliente> Listar_389ME()
        {
            List<Cliente> lista_389ME = dalCliente_389ME.Listar_389ME();

            foreach (Cliente cliente_389ME in lista_389ME)
                cliente_389ME.Direccion_389ME = encriptacion_389ME.Desencriptar_389ME(cliente_389ME.Direccion_389ME);

            return lista_389ME;
        }

        public Cliente ObtenerPorDNI_389ME(int dni_389ME)
        {
            Cliente cliente_389ME = dalCliente_389ME.ObtenerPorDNI_389ME(dni_389ME);

            if (cliente_389ME != null)
                cliente_389ME.Direccion_389ME = encriptacion_389ME.Desencriptar_389ME(cliente_389ME.Direccion_389ME);

            return cliente_389ME;
        }

        public void Registrar_389ME(Cliente cliente_389ME)
        {
            Validar_389ME(cliente_389ME);

            if (dalCliente_389ME.ObtenerPorDNI_389ME(cliente_389ME.DNI_389ME) != null)
                throw new Exception("El DNI ya está en uso por otro cliente. Si sus datos cambiaron, selecciónelo en la grilla y use Modificar.");

            cliente_389ME.Direccion_389ME = encriptacion_389ME.Encriptar_389ME(cliente_389ME.Direccion_389ME);

            dalCliente_389ME.Insertar_389ME(cliente_389ME);
            bllDigito_389ME.Recalcular_389ME("Cliente");

            bllBitacora_389ME.RegistrarEvento_389ME("Alta de cliente",
                $"DNI {cliente_389ME.DNI_389ME} - {cliente_389ME.Nombre_389ME} {cliente_389ME.Apellido_389ME}", "Clientes", "Baja");
        }

        public void Modificar_389ME(Cliente cliente_389ME)
        {
            Validar_389ME(cliente_389ME);

            Cliente anterior_389ME = ObtenerPorDNI_389ME(cliente_389ME.DNI_389ME);
            if (anterior_389ME == null)
                throw new Exception("El cliente no esta registrado.");

            // La direccion es un dato personal: se registra que cambio, pero no su valor.
            List<string> cambios_389ME = new List<string>();
            BLLBitacoraEventos.AgregarCambio_389ME(cambios_389ME, "Nombre", anterior_389ME.Nombre_389ME, cliente_389ME.Nombre_389ME);
            BLLBitacoraEventos.AgregarCambio_389ME(cambios_389ME, "Apellido", anterior_389ME.Apellido_389ME, cliente_389ME.Apellido_389ME);
            if (anterior_389ME.Direccion_389ME != cliente_389ME.Direccion_389ME)
                cambios_389ME.Add("Direccion modificada");
            BLLBitacoraEventos.AgregarCambio_389ME(cambios_389ME, "Mail", anterior_389ME.Mail_389ME, cliente_389ME.Mail_389ME);
            BLLBitacoraEventos.AgregarCambio_389ME(cambios_389ME, "Telefono", anterior_389ME.NumTelefono_389ME, cliente_389ME.NumTelefono_389ME);

            cliente_389ME.Direccion_389ME = encriptacion_389ME.Encriptar_389ME(cliente_389ME.Direccion_389ME);

            dalCliente_389ME.Modificar_389ME(cliente_389ME);
            bllDigito_389ME.Recalcular_389ME("Cliente");

            bllBitacora_389ME.RegistrarEvento_389ME("Modificación de cliente",
                $"DNI {cliente_389ME.DNI_389ME}: " + (cambios_389ME.Count == 0 ? "sin cambios" : string.Join("; ", cambios_389ME)),
                "Clientes", "Media");
        }

        public void CambiarBloqueo_389ME(int dni_389ME, bool bloqueado_389ME)
        {
            Cliente cliente_389ME = dalCliente_389ME.ObtenerPorDNI_389ME(dni_389ME);

            if (cliente_389ME == null)
                throw new Exception("El cliente no esta registrado.");

            if (cliente_389ME.Bloqueado_389ME == bloqueado_389ME)
                throw new Exception(bloqueado_389ME ? "El cliente ya esta bloqueado." : "El cliente no esta bloqueado.");

            dalCliente_389ME.CambiarBloqueo_389ME(dni_389ME, bloqueado_389ME);
            bllDigito_389ME.Recalcular_389ME("Cliente");

            bllBitacora_389ME.RegistrarEvento_389ME(bloqueado_389ME ? "Bloqueo de cliente" : "Desbloqueo de cliente",
                $"DNI {dni_389ME} - {cliente_389ME.Nombre_389ME} {cliente_389ME.Apellido_389ME}", "Clientes", bloqueado_389ME ? "Alta" : "Media");
        }

        public void Serializar_389ME(List<Cliente> clientes_389ME, string ruta_389ME)
        {
            Serializador.Serializar_389ME(clientes_389ME, ruta_389ME);
            bllBitacora_389ME.RegistrarEvento_389ME("Serialización de clientes", $"{clientes_389ME.Count} clientes en {ruta_389ME}", "Clientes", "Baja");
        }

        public List<Cliente> Deserializar_389ME(string ruta_389ME)
        {
            List<Cliente> clientes_389ME;
            try
            {
                clientes_389ME = Serializador.Deserializar_389ME<List<Cliente>>(ruta_389ME);
            }
            catch (InvalidOperationException)
            {
                throw new Exception("El archivo no es un XML de clientes válido.");
            }

            bllBitacora_389ME.RegistrarEvento_389ME("Deserialización de clientes", $"{clientes_389ME.Count} clientes desde {ruta_389ME}", "Clientes", "Baja");
            return clientes_389ME;
        }

        private void Validar_389ME(Cliente cliente_389ME)
        {
            if (cliente_389ME.DNI_389ME < 10000000 || cliente_389ME.DNI_389ME > 99999999)
                throw new Exception("El DNI debe tener 8 números.");
            if (string.IsNullOrWhiteSpace(cliente_389ME.Nombre_389ME) || string.IsNullOrWhiteSpace(cliente_389ME.Apellido_389ME))
                throw new Exception("Nombre y apellido del cliente son obligatorios.");
            if (!Validador.EsTexto_389ME(cliente_389ME.Nombre_389ME) || !Validador.EsTexto_389ME(cliente_389ME.Apellido_389ME))
                throw new Exception("El nombre y el apellido solo pueden tener letras.");
            if (!Validador.EsMail_389ME(cliente_389ME.Mail_389ME))
                throw new Exception("El mail no es válido. Ejemplo: nombre@correo.com");

            // El telefono es opcional: 0 significa que no se cargo
            if (cliente_389ME.NumTelefono_389ME != 0 && (cliente_389ME.NumTelefono_389ME < 10000000 || cliente_389ME.NumTelefono_389ME > 9999999999))
                throw new Exception("El teléfono debe tener entre 8 y 10 números.");
        }
    }
}
