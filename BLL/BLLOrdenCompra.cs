using System;
using System.Collections.Generic;
using System.Linq;
using BE;
using DAL;
using SERVICIOS;

namespace BLL
{
    public class BLLOrdenCompra
    {
        public const string EstadoPendiente_389ME = "Pendiente";
        public const string EstadoRecibida_389ME = "Recibida";
        public const string EstadoRecibidaParcial_389ME = "Recibida parcial";

        private readonly DALOrdenCompra dalOrden_389ME = new DALOrdenCompra();
        private readonly DALProveedor dalProveedor_389ME = new DALProveedor();
        private readonly BLLBitacoraEventos bllBitacora_389ME = new BLLBitacoraEventos();
        private readonly BLLDigitoVerificador bllDigito_389ME = new BLLDigitoVerificador();

        // Arma la orden con las cotizaciones elegidas de un mismo proveedor
        public OrdenCompra Generar_389ME(List<Cotizacion> cotizaciones_389ME)
        {
            if (cotizaciones_389ME.Count == 0)
                throw new Exception("Seleccione los insumos que va a comprar.");
            if (cotizaciones_389ME.Select(cotizacion_389ME => cotizacion_389ME.CUIT_389ME).Distinct().Count() > 1)
                throw new Exception("Una orden de compra es para un solo proveedor.");
            if (cotizaciones_389ME.Any(cotizacion_389ME => cotizacion_389ME.FechaVencimiento_389ME.Date < DateTime.Today))
                throw new Exception("Hay cotizaciones vencidas: pida al proveedor que las actualice.");

            Proveedor proveedor_389ME = dalProveedor_389ME.ObtenerPorCUIT_389ME(cotizaciones_389ME[0].CUIT_389ME);
            if (!proveedor_389ME.Registrado_389ME)
                throw new Exception("El proveedor no tiene dirección ni cuenta bancaria. Complételas en Gestión de proveedores.");

            OrdenCompra orden_389ME = new OrdenCompra
            {
                CUIT_389ME = proveedor_389ME.CUIT_389ME,
                NombreProveedor_389ME = proveedor_389ME.Nombre_389ME,
                CodSolicitud_389ME = cotizaciones_389ME[0].CodSolicitud_389ME,
                FechaCreacion_389ME = DateTime.Now,
                Estado_389ME = EstadoPendiente_389ME,
                Detalles_389ME = cotizaciones_389ME.Select(cotizacion_389ME => new OrdenCompraDetalle
                {
                    CodInsumo_389ME = cotizacion_389ME.CodInsumo_389ME,
                    NombreInsumo_389ME = cotizacion_389ME.NombreInsumo_389ME,
                    Cantidad_389ME = cotizacion_389ME.Cantidad_389ME,
                    PrecioUnitario_389ME = cotizacion_389ME.Precio_389ME
                }).ToList()
            };
            orden_389ME.PrecioTotal_389ME = orden_389ME.Detalles_389ME.Sum(detalle_389ME => detalle_389ME.Subtotal_389ME);
            orden_389ME.CodOrden_389ME = dalOrden_389ME.Registrar_389ME(orden_389ME);
            bllDigito_389ME.Recalcular_389ME("OrdenCompra", "OrdenCompraDetalle", "SolicitudCotizacion");

            bllBitacora_389ME.RegistrarEvento_389ME("Generación de orden de compra",
                $"Orden N° {orden_389ME.CodOrden_389ME} - {proveedor_389ME.Nombre_389ME} - {orden_389ME.Detalles_389ME.Count} insumo(s) - Total ${orden_389ME.PrecioTotal_389ME:N2}",
                "Compras", "Media");
            return orden_389ME;
        }

        public List<OrdenCompra> Listar_389ME()
        {
            return dalOrden_389ME.Listar_389ME("");
        }

        public List<OrdenCompra> ListarPendientes_389ME()
        {
            return dalOrden_389ME.Listar_389ME(EstadoPendiente_389ME);
        }

        public List<OrdenCompraDetalle> ObtenerDetalle_389ME(int codOrden_389ME)
        {
            return dalOrden_389ME.ObtenerDetalle_389ME(codOrden_389ME);
        }

        // El almacenista indica cuanto llego de cada insumo; se suma al stock y la orden queda recibida
        public void RegistrarRecepcion_389ME(OrdenCompra orden_389ME, List<OrdenCompraDetalle> detalles_389ME)
        {
            if (orden_389ME.Estado_389ME != EstadoPendiente_389ME)
                throw new Exception("La orden ya fue recibida.");
            if (detalles_389ME.Any(detalle_389ME => detalle_389ME.CantidadRecibida_389ME < 0 || detalle_389ME.CantidadRecibida_389ME > detalle_389ME.Cantidad_389ME))
                throw new Exception("La cantidad recibida debe estar entre cero y la cantidad pedida.");

            bool completa_389ME = detalles_389ME.All(detalle_389ME => detalle_389ME.CantidadRecibida_389ME == detalle_389ME.Cantidad_389ME);
            string estado_389ME = completa_389ME ? EstadoRecibida_389ME : EstadoRecibidaParcial_389ME;

            dalOrden_389ME.RegistrarRecepcion_389ME(orden_389ME.CodOrden_389ME, detalles_389ME, estado_389ME);
            bllDigito_389ME.Recalcular_389ME("OrdenCompra", "OrdenCompraDetalle", "Insumo");

            bllBitacora_389ME.RegistrarEvento_389ME("Recepción de insumos",
                $"Orden N° {orden_389ME.CodOrden_389ME} - {estado_389ME}: " +
                string.Join("; ", detalles_389ME.Select(detalle_389ME => $"{detalle_389ME.NombreInsumo_389ME} {detalle_389ME.CantidadRecibida_389ME} de {detalle_389ME.Cantidad_389ME}")),
                "Compras", completa_389ME ? "Baja" : "Media");
        }
    }
}
