using System;
using System.Collections.Generic;
using System.Linq;
using BE;
using DAL;

namespace BLL
{
    public class BLLFactura
    {
        public const double RecargoTarjeta_389ME = 0.10;

        private readonly DALFactura dalFactura_389ME = new DALFactura();
        private readonly DALProducto dalProducto_389ME = new DALProducto();
        private readonly BLLCliente bllCliente_389ME = new BLLCliente();
        private readonly BLLBitacoraEventos bllBitacora_389ME = new BLLBitacoraEventos();
        private readonly BLLDigitoVerificador bllDigito_389ME = new BLLDigitoVerificador();

        public Factura RealizarVenta_389ME(Cliente cliente_389ME, List<DetalleFactura> carrito_389ME, string metodoPago_389ME, string banco_389ME, string tipoTarjeta_389ME, string marcaTarjeta_389ME)
        {
            if (carrito_389ME == null || carrito_389ME.Count == 0)
                throw new Exception("El carrito no puede estar vacío.");

            foreach (DetalleFactura item_389ME in carrito_389ME)
            {
                Producto producto_389ME = dalProducto_389ME.ObtenerPorCodigo_389ME(item_389ME.CodProducto_389ME);
                if (producto_389ME == null)
                {
                    bllBitacora_389ME.RegistrarEvento_389ME("Venta rechazada", $"El producto {item_389ME.NombreProducto_389ME} ya no existe", "Ventas", "Media");
                    throw new Exception($"El producto '{item_389ME.NombreProducto_389ME}' ya no existe.");
                }
                if (producto_389ME.Stock_389ME < item_389ME.Cantidad_389ME)
                {
                    bllBitacora_389ME.RegistrarEvento_389ME("Venta rechazada", $"Sin stock de {producto_389ME.Nombre_389ME} (disponible {producto_389ME.Stock_389ME}, solicitado {item_389ME.Cantidad_389ME})", "Ventas", "Media");
                    throw new Exception($"No hay stock suficiente de '{producto_389ME.Nombre_389ME}' (disponible: {producto_389ME.Stock_389ME}, solicitado: {item_389ME.Cantidad_389ME}).");
                }
            }

            Cliente registrado_389ME = bllCliente_389ME.ObtenerPorDNI_389ME(cliente_389ME.DNI_389ME);

            if (registrado_389ME == null)
                bllCliente_389ME.Registrar_389ME(cliente_389ME);
            else if (registrado_389ME.Bloqueado_389ME)
            {
                bllBitacora_389ME.RegistrarEvento_389ME("Venta rechazada", $"Cliente DNI {cliente_389ME.DNI_389ME} bloqueado", "Ventas", "Media");
                throw new Exception("El cliente esta bloqueado y no puede comprar.");
            }

            Factura factura_389ME = new Factura
            {
                DNICliente_389ME = cliente_389ME.DNI_389ME,
                Fecha_389ME = DateTime.Now,
                MetodoPago_389ME = metodoPago_389ME,
                Banco_389ME = banco_389ME,
                TipoTarjeta_389ME = tipoTarjeta_389ME,
                MarcaTarjeta_389ME = marcaTarjeta_389ME,
                MontoTotal_389ME = CalcularTotal_389ME(carrito_389ME, metodoPago_389ME),
                Detalles_389ME = carrito_389ME
            };

            try
            {
                factura_389ME.CodFactura_389ME = dalFactura_389ME.RegistrarVenta_389ME(factura_389ME);
                bllDigito_389ME.Recalcular_389ME("Factura", "FacturaDetalle", "Producto", "ConsumoInsumo", "Insumo");
            }
            catch (Exception ex_389ME)
            {
                bllBitacora_389ME.RegistrarEvento_389ME("Error al registrar venta", $"Cliente DNI {cliente_389ME.DNI_389ME}: {ex_389ME.Message}", "Ventas", "Alta");
                throw;
            }

            bllBitacora_389ME.RegistrarEvento_389ME("Venta registrada",
                $"Factura N° {factura_389ME.CodFactura_389ME} - Cliente DNI {cliente_389ME.DNI_389ME} - {carrito_389ME.Count} producto(s) - {metodoPago_389ME} - Total ${factura_389ME.MontoTotal_389ME:N2}",
                "Ventas", "Baja");
            return factura_389ME;
        }

        public double CalcularTotal_389ME(List<DetalleFactura> carrito_389ME, string metodoPago_389ME)
        {
            double total_389ME = carrito_389ME.Sum(detalle_389ME => detalle_389ME.Subtotal_389ME);

            if (metodoPago_389ME == "Tarjeta")
                total_389ME += total_389ME * RecargoTarjeta_389ME;

            return total_389ME;
        }

        public List<Factura> Listar_389ME()
        {
            return dalFactura_389ME.Listar_389ME();
        }

        public List<DetalleFactura> ObtenerDetalle_389ME(int codFactura_389ME)
        {
            return dalFactura_389ME.ObtenerDetalle_389ME(codFactura_389ME);
        }
    }
}
