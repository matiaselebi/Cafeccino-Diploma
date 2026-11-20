using System;
using System.Collections.Generic;
using System.Linq;
using BE;
using DAL;
using SERVICIOS;

namespace BLL
{
    public class BLLSolicitudCotizacion
    {
        public const string EstadoPendiente_389ME = "Pendiente";
        public const string EstadoCerrada_389ME = "Cerrada";

        private readonly DALSolicitudCotizacion dalSolicitud_389ME = new DALSolicitudCotizacion();
        private readonly DALInsumo dalInsumo_389ME = new DALInsumo();
        private readonly BLLProveedor bllProveedor_389ME = new BLLProveedor();
        private readonly BLLBitacoraEventos bllBitacora_389ME = new BLLBitacoraEventos();
        private readonly BLLDigitoVerificador bllDigito_389ME = new BLLDigitoVerificador();

        public int Registrar_389ME(List<SolicitudInsumo> insumos_389ME, List<Proveedor> proveedores_389ME)
        {
            if (insumos_389ME.Count == 0)
                throw new Exception("Agregue al menos un insumo a la solicitud.");
            if (proveedores_389ME.Count == 0)
                throw new Exception("Seleccione al menos un proveedor.");

            foreach (SolicitudInsumo item_389ME in insumos_389ME)
            {
                if (item_389ME.Cantidad_389ME <= 0)
                    throw new Exception($"La cantidad de {item_389ME.NombreInsumo_389ME} debe ser mayor a cero.");

                // La compra no puede dejar el stock por encima del maximo permitido
                Insumo insumo_389ME = dalInsumo_389ME.ObtenerPorCodigo_389ME(item_389ME.CodInsumo_389ME);
                if (insumo_389ME.Stock_389ME + item_389ME.Cantidad_389ME > insumo_389ME.StockMaximo_389ME)
                    throw new Exception($"Con {item_389ME.Cantidad_389ME} {insumo_389ME.UnidadMedida_389ME} de {insumo_389ME.Nombre_389ME} se supera el stock maximo " +
                                        $"({insumo_389ME.StockMaximo_389ME}). Como maximo se pueden pedir {insumo_389ME.StockMaximo_389ME - insumo_389ME.Stock_389ME}.");
            }

            SolicitudCotizacion solicitud_389ME = new SolicitudCotizacion
            {
                FechaEmision_389ME = DateTime.Now,
                Estado_389ME = EstadoPendiente_389ME,
                Insumos_389ME = insumos_389ME,
                Proveedores_389ME = proveedores_389ME
            };
            solicitud_389ME.CodSolicitud_389ME = dalSolicitud_389ME.Registrar_389ME(solicitud_389ME);
            bllDigito_389ME.Recalcular_389ME("SolicitudCotizacion", "SolicitudInsumo", "SolicitudProveedor");

            bllBitacora_389ME.RegistrarEvento_389ME("Solicitud de cotización",
                $"Solicitud N° {solicitud_389ME.CodSolicitud_389ME} - {insumos_389ME.Count} insumo(s) - Proveedores: " +
                string.Join(", ", proveedores_389ME.Select(proveedor_389ME => proveedor_389ME.Nombre_389ME)),
                "Compras", "Baja");
            return solicitud_389ME.CodSolicitud_389ME;
        }

        public List<SolicitudCotizacion> ListarPendientes_389ME()
        {
            return dalSolicitud_389ME.ListarPorEstado_389ME(EstadoPendiente_389ME);
        }

        public List<SolicitudInsumo> ObtenerInsumos_389ME(int codSolicitud_389ME)
        {
            return dalSolicitud_389ME.ObtenerInsumos_389ME(codSolicitud_389ME);
        }

        public List<Proveedor> ObtenerProveedores_389ME(int codSolicitud_389ME)
        {
            return dalSolicitud_389ME.ObtenerCUITProveedores_389ME(codSolicitud_389ME)
                .Select(cuit_389ME => bllProveedor_389ME.ObtenerPorCUIT_389ME(cuit_389ME))
                .ToList();
        }
    }
}
