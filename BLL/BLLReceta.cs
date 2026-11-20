using System;
using System.Collections.Generic;
using System.Linq;
using BE;
using DAL;
using SERVICIOS;

namespace BLL
{
    public class BLLReceta
    {
        private readonly DALReceta dalReceta_389ME = new DALReceta();
        private readonly BLLBitacoraEventos bllBitacora_389ME = new BLLBitacoraEventos();
        private readonly BLLDigitoVerificador bllDigito_389ME = new BLLDigitoVerificador();

        public List<Receta> ListarPorProducto_389ME(int codProducto_389ME)
        {
            return dalReceta_389ME.ListarPorProducto_389ME(codProducto_389ME);
        }

        public void Guardar_389ME(Producto producto_389ME, List<Receta> receta_389ME)
        {
            if (receta_389ME.Any(linea_389ME => linea_389ME.Cantidad_389ME <= 0))
                throw new Exception("Las cantidades de la receta deben ser mayores a cero.");
            if (receta_389ME.Any(linea_389ME => linea_389ME.Cantidad_389ME >= 10000000))
                throw new Exception("Las cantidades de la receta no pueden superar 9.999.999.");
            if (receta_389ME.Select(linea_389ME => linea_389ME.CodInsumo_389ME).Distinct().Count() != receta_389ME.Count)
                throw new Exception("Un insumo no puede estar dos veces en la misma receta.");

            dalReceta_389ME.Guardar_389ME(producto_389ME.CodProducto_389ME, receta_389ME);
            bllDigito_389ME.Recalcular_389ME("Receta");

            bllBitacora_389ME.RegistrarEvento_389ME("Modificación de receta",
                $"{producto_389ME.Nombre_389ME}: " + string.Join("; ", receta_389ME.Select(linea_389ME =>
                    $"{linea_389ME.NombreInsumo_389ME} {linea_389ME.Cantidad_389ME} {linea_389ME.UnidadMedida_389ME}")),
                "Compras", "Media");
        }
    }
}
