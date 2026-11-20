using System;
using System.Collections.Generic;
using System.Linq;
using BE;
using DAL;
using SERVICIOS;

namespace BLL
{
    public class BLLInsumo
    {
        // El consumo de los ultimos 30 dias se usa para estimar el de los proximos 7
        public const int DiasHistorial_389ME = 30;
        public const int DiasProyeccion_389ME = 7;

        private readonly DALInsumo dalInsumo_389ME = new DALInsumo();
        private readonly BLLBitacoraEventos bllBitacora_389ME = new BLLBitacoraEventos();
        private readonly BLLDigitoVerificador bllDigito_389ME = new BLLDigitoVerificador();

        public List<Insumo> Listar_389ME()
        {
            return dalInsumo_389ME.Listar_389ME();
        }

        public Insumo ObtenerPorCodigo_389ME(int codInsumo_389ME)
        {
            return dalInsumo_389ME.ObtenerPorCodigo_389ME(codInsumo_389ME);
        }

        public void Registrar_389ME(Insumo insumo_389ME)
        {
            Validar_389ME(insumo_389ME);
            dalInsumo_389ME.Insertar_389ME(insumo_389ME);
            bllDigito_389ME.Recalcular_389ME("Insumo");

            bllBitacora_389ME.RegistrarEvento_389ME("Alta de insumo",
                $"{insumo_389ME.Nombre_389ME} - Stock {insumo_389ME.Stock_389ME} {insumo_389ME.UnidadMedida_389ME}", "Compras", "Baja");
        }

        public void Modificar_389ME(Insumo insumo_389ME)
        {
            Validar_389ME(insumo_389ME);

            Insumo anterior_389ME = dalInsumo_389ME.ObtenerPorCodigo_389ME(insumo_389ME.CodInsumo_389ME);
            if (anterior_389ME == null)
                throw new Exception("El insumo no existe.");

            dalInsumo_389ME.Modificar_389ME(insumo_389ME);
            bllDigito_389ME.Recalcular_389ME("Insumo");

            List<string> cambios_389ME = new List<string>();
            BLLBitacoraEventos.AgregarCambio_389ME(cambios_389ME, "Nombre", anterior_389ME.Nombre_389ME, insumo_389ME.Nombre_389ME);
            BLLBitacoraEventos.AgregarCambio_389ME(cambios_389ME, "Tipo", anterior_389ME.Tipo_389ME, insumo_389ME.Tipo_389ME);
            BLLBitacoraEventos.AgregarCambio_389ME(cambios_389ME, "Unidad", anterior_389ME.UnidadMedida_389ME, insumo_389ME.UnidadMedida_389ME);
            BLLBitacoraEventos.AgregarCambio_389ME(cambios_389ME, "Precio", anterior_389ME.Precio_389ME, insumo_389ME.Precio_389ME);
            BLLBitacoraEventos.AgregarCambio_389ME(cambios_389ME, "Stock", anterior_389ME.Stock_389ME, insumo_389ME.Stock_389ME);
            BLLBitacoraEventos.AgregarCambio_389ME(cambios_389ME, "Stock minimo", anterior_389ME.StockMinimo_389ME, insumo_389ME.StockMinimo_389ME);
            BLLBitacoraEventos.AgregarCambio_389ME(cambios_389ME, "Stock maximo", anterior_389ME.StockMaximo_389ME, insumo_389ME.StockMaximo_389ME);

            bllBitacora_389ME.RegistrarEvento_389ME("Modificación de insumo",
                $"Código {insumo_389ME.CodInsumo_389ME} ({insumo_389ME.Nombre_389ME}): " + (cambios_389ME.Count == 0 ? "sin cambios" : string.Join("; ", cambios_389ME)),
                "Compras", "Media");
        }

        public void Eliminar_389ME(int codInsumo_389ME)
        {
            Insumo insumo_389ME = dalInsumo_389ME.ObtenerPorCodigo_389ME(codInsumo_389ME);

            try
            {
                dalInsumo_389ME.Eliminar_389ME(codInsumo_389ME);
                bllDigito_389ME.Recalcular_389ME("Insumo");
            }
            catch (Exception)
            {
                // La base no deja borrarlo si esta en una receta, una solicitud o una orden de compra
                throw new Exception("No se puede eliminar el insumo porque se usa en una receta o en una compra.");
            }

            bllBitacora_389ME.RegistrarEvento_389ME("Baja de insumo", $"Código {codInsumo_389ME} - {insumo_389ME?.Nombre_389ME}", "Compras", "Alta");
        }

        // Calcula para cada insumo cuanto se va a consumir en los proximos dias y si hay que reponerlo.
        //   consumo estimado  = consumo de los ultimos 30 dias / 30 * 7
        //   stock proyectado  = stock actual - consumo estimado
        //   se sugiere comprar si el stock proyectado queda por debajo del stock minimo
        //   cantidad sugerida = lo que falta para llegar al stock maximo, asi cubre el consumo sin superarlo
        public List<SugerenciaCompra> ObtenerSugerencias_389ME()
        {
            Dictionary<int, double> consumo_389ME = dalInsumo_389ME.ObtenerConsumoDesde_389ME(DateTime.Now.AddDays(-DiasHistorial_389ME));
            List<SugerenciaCompra> sugerencias_389ME = new List<SugerenciaCompra>();

            foreach (Insumo insumo_389ME in dalInsumo_389ME.Listar_389ME())
            {
                consumo_389ME.TryGetValue(insumo_389ME.CodInsumo_389ME, out double consumido_389ME);

                double consumoEstimado_389ME = consumido_389ME / DiasHistorial_389ME * DiasProyeccion_389ME;
                double stockProyectado_389ME = insumo_389ME.Stock_389ME - consumoEstimado_389ME;
                bool comprar_389ME = stockProyectado_389ME < insumo_389ME.StockMinimo_389ME;

                sugerencias_389ME.Add(new SugerenciaCompra
                {
                    CodInsumo_389ME = insumo_389ME.CodInsumo_389ME,
                    Nombre_389ME = insumo_389ME.Nombre_389ME,
                    UnidadMedida_389ME = insumo_389ME.UnidadMedida_389ME,
                    Stock_389ME = insumo_389ME.Stock_389ME,
                    StockMinimo_389ME = insumo_389ME.StockMinimo_389ME,
                    StockMaximo_389ME = insumo_389ME.StockMaximo_389ME,
                    ConsumoEstimado_389ME = Math.Round(consumoEstimado_389ME, 3),
                    StockProyectado_389ME = Math.Round(stockProyectado_389ME, 3),
                    CantidadSugerida_389ME = comprar_389ME ? Math.Round(insumo_389ME.StockMaximo_389ME - insumo_389ME.Stock_389ME, 3) : 0,
                    Comprar_389ME = comprar_389ME
                });
            }

            // Primero los que hay que reponer
            return sugerencias_389ME.OrderByDescending(sugerencia_389ME => sugerencia_389ME.Comprar_389ME).ToList();
        }

        private void Validar_389ME(Insumo insumo_389ME)
        {
            if (string.IsNullOrWhiteSpace(insumo_389ME.Nombre_389ME) || string.IsNullOrWhiteSpace(insumo_389ME.Tipo_389ME) ||
                string.IsNullOrWhiteSpace(insumo_389ME.UnidadMedida_389ME))
                throw new Exception("El nombre, el tipo y la unidad de medida son obligatorios.");
            if (insumo_389ME.Precio_389ME <= 0)
                throw new Exception("El precio debe ser mayor a cero.");
            if (insumo_389ME.Stock_389ME < 0 || insumo_389ME.StockMinimo_389ME < 0)
                throw new Exception("El stock y el stock minimo no pueden ser negativos.");
            if (insumo_389ME.StockMaximo_389ME <= insumo_389ME.StockMinimo_389ME)
                throw new Exception("El stock maximo debe ser mayor al stock minimo.");
            // Limites de las columnas de la base: decimal(10,2) para el precio y decimal(10,3) para los stocks
            if (insumo_389ME.Precio_389ME >= 100000000)
                throw new Exception("El precio no puede superar $99.999.999.");
            if (insumo_389ME.Stock_389ME >= 10000000 || insumo_389ME.StockMaximo_389ME >= 10000000)
                throw new Exception("Los stocks no pueden superar 9.999.999.");

            bool repetido_389ME = dalInsumo_389ME.Listar_389ME().Exists(otro_389ME =>
                otro_389ME.CodInsumo_389ME != insumo_389ME.CodInsumo_389ME &&
                string.Equals(otro_389ME.Nombre_389ME.Trim(), insumo_389ME.Nombre_389ME.Trim(), StringComparison.OrdinalIgnoreCase));
            if (repetido_389ME)
                throw new Exception("Ya existe un insumo con ese nombre.");
        }
    }
}
