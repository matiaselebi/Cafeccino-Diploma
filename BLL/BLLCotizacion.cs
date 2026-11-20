using System;
using System.Collections.Generic;
using System.Linq;
using BE;
using DAL;
using SERVICIOS;

namespace BLL
{
    public class BLLCotizacion
    {
        private readonly DALCotizacion dalCotizacion_389ME = new DALCotizacion();
        private readonly DALSolicitudCotizacion dalSolicitud_389ME = new DALSolicitudCotizacion();
        private readonly DALProveedor dalProveedor_389ME = new DALProveedor();
        private readonly BLLBitacoraEventos bllBitacora_389ME = new BLLBitacoraEventos();
        private readonly BLLDigitoVerificador bllDigito_389ME = new BLLDigitoVerificador();

        // El comprador carga lo que le respondio el proveedor (el proveedor no usa el sistema)
        public void Registrar_389ME(Cotizacion cotizacion_389ME)
        {
            if (cotizacion_389ME.Precio_389ME <= 0 || cotizacion_389ME.Cantidad_389ME <= 0)
                throw new Exception("El precio y la cantidad deben ser mayores a cero.");
            if (cotizacion_389ME.Precio_389ME >= 100000000 || cotizacion_389ME.Cantidad_389ME >= 10000000)
                throw new Exception("El precio no puede superar $99.999.999 y la cantidad 9.999.999.");
            // La cantidad se carga a mano en cada cotizacion: no puede dejar el insumo por encima de su stock maximo
            Insumo insumo_389ME = new DAL.DALInsumo().ObtenerPorCodigo_389ME(cotizacion_389ME.CodInsumo_389ME);
            if (insumo_389ME != null && insumo_389ME.Stock_389ME + cotizacion_389ME.Cantidad_389ME > insumo_389ME.StockMaximo_389ME)
                throw new Exception($"Con {cotizacion_389ME.Cantidad_389ME} {insumo_389ME.UnidadMedida_389ME} se supera el stock máximo de " +
                                    $"{insumo_389ME.Nombre_389ME} ({insumo_389ME.StockMaximo_389ME}; stock actual {insumo_389ME.Stock_389ME}).");
            if (cotizacion_389ME.FechaVencimiento_389ME.Date < DateTime.Today)
                throw new Exception("La cotizacion no puede estar vencida.");
            if (!dalSolicitud_389ME.ObtenerCUITProveedores_389ME(cotizacion_389ME.CodSolicitud_389ME).Contains(cotizacion_389ME.CUIT_389ME))
                throw new Exception("El proveedor no fue invitado a cotizar esta solicitud.");
            if (!dalSolicitud_389ME.ObtenerInsumos_389ME(cotizacion_389ME.CodSolicitud_389ME).Any(item_389ME => item_389ME.CodInsumo_389ME == cotizacion_389ME.CodInsumo_389ME))
                throw new Exception("El insumo no forma parte de esta solicitud.");

            dalCotizacion_389ME.Guardar_389ME(cotizacion_389ME);
            bllDigito_389ME.Recalcular_389ME("Cotizacion");

            bllBitacora_389ME.RegistrarEvento_389ME("Carga de cotización",
                $"Solicitud N° {cotizacion_389ME.CodSolicitud_389ME} - CUIT {cotizacion_389ME.CUIT_389ME} - {cotizacion_389ME.NombreInsumo_389ME}: " +
                $"{cotizacion_389ME.Cantidad_389ME} a ${cotizacion_389ME.Precio_389ME:N2}",
                "Compras", "Baja");
        }

        public List<Cotizacion> ListarPorSolicitud_389ME(int codSolicitud_389ME)
        {
            return dalCotizacion_389ME.ListarPorSolicitud_389ME(codSolicitud_389ME);
        }

        // Un renglon por proveedor con el total de lo que cotizo, para elegir el mas conveniente.
        // Solo cuentan las cotizaciones que siguen vigentes.
        public List<ResumenCotizacion> CompararProveedores_389ME(int codSolicitud_389ME)
        {
            return dalCotizacion_389ME.ListarPorSolicitud_389ME(codSolicitud_389ME)
                .Where(cotizacion_389ME => cotizacion_389ME.FechaVencimiento_389ME.Date >= DateTime.Today)
                .GroupBy(cotizacion_389ME => cotizacion_389ME.CUIT_389ME)
                .Select(grupo_389ME => new ResumenCotizacion
                {
                    CUIT_389ME = grupo_389ME.Key,
                    NombreProveedor_389ME = grupo_389ME.First().NombreProveedor_389ME,
                    InsumosCotizados_389ME = grupo_389ME.Count(),
                    Total_389ME = grupo_389ME.Sum(cotizacion_389ME => cotizacion_389ME.Subtotal_389ME),
                    Registrado_389ME = dalProveedor_389ME.ObtenerPorCUIT_389ME(grupo_389ME.Key).Registrado_389ME
                })
                .OrderByDescending(resumen_389ME => resumen_389ME.InsumosCotizados_389ME)
                .ThenBy(resumen_389ME => resumen_389ME.Total_389ME)
                .ToList();
        }
    }
}
