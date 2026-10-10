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

            cliente_389ME.Direccion_389ME = encriptacion_389ME.Encriptar_389ME(cliente_389ME.Direccion_389ME);

            dalCliente_389ME.Insertar_389ME(cliente_389ME);
        }

        public void Modificar_389ME(Cliente cliente_389ME)
        {
            Validar_389ME(cliente_389ME);

            if (dalCliente_389ME.ObtenerPorDNI_389ME(cliente_389ME.DNI_389ME) == null)
                throw new Exception("El cliente no esta registrado.");

            cliente_389ME.Direccion_389ME = encriptacion_389ME.Encriptar_389ME(cliente_389ME.Direccion_389ME);

            dalCliente_389ME.Modificar_389ME(cliente_389ME);
        }

        public void CambiarBloqueo_389ME(int dni_389ME, bool bloqueado_389ME)
        {
            Cliente cliente_389ME = dalCliente_389ME.ObtenerPorDNI_389ME(dni_389ME);

            if (cliente_389ME == null)
                throw new Exception("El cliente no esta registrado.");

            if (cliente_389ME.Bloqueado_389ME == bloqueado_389ME)
                throw new Exception(bloqueado_389ME ? "El cliente ya esta bloqueado." : "El cliente no esta bloqueado.");

            dalCliente_389ME.CambiarBloqueo_389ME(dni_389ME, bloqueado_389ME);
        }

        private void Validar_389ME(Cliente cliente_389ME)
        {
            if (string.IsNullOrWhiteSpace(cliente_389ME.Nombre_389ME) || string.IsNullOrWhiteSpace(cliente_389ME.Apellido_389ME))
                throw new Exception("Nombre y apellido del cliente son obligatorios.");
        }
    }
}
