using System;
using System.Collections.Generic;
using BE;
using DAL;

namespace BLL
{
    public class BLLProducto
    {
        private readonly DALProducto dalProducto_389ME = new DALProducto();
        private readonly BLLBitacoraEventos bllBitacora_389ME = new BLLBitacoraEventos();
        private readonly BLLDigitoVerificador bllDigito_389ME = new BLLDigitoVerificador();

        public List<Producto> Listar_389ME()
        {
            return dalProducto_389ME.Listar_389ME();
        }

        public Producto ObtenerPorCodigo_389ME(int codProducto_389ME)
        {
            return dalProducto_389ME.ObtenerPorCodigo_389ME(codProducto_389ME);
        }

        public void Registrar_389ME(Producto producto_389ME)
        {
            Validar_389ME(producto_389ME);
            dalProducto_389ME.Insertar_389ME(producto_389ME);
            bllDigito_389ME.Recalcular_389ME("Producto");

            bllBitacora_389ME.RegistrarEvento_389ME("Alta de producto",
                $"{producto_389ME.Nombre_389ME} - Precio ${producto_389ME.Precio_389ME:N2} - Stock {producto_389ME.Stock_389ME}", "Productos", "Baja");
        }

        public void Modificar_389ME(Producto producto_389ME)
        {
            Validar_389ME(producto_389ME);
            Producto anterior_389ME = dalProducto_389ME.ObtenerPorCodigo_389ME(producto_389ME.CodProducto_389ME);

            dalProducto_389ME.Modificar_389ME(producto_389ME);
            bllDigito_389ME.Recalcular_389ME("Producto");

            List<string> cambios_389ME = new List<string>();
            if (anterior_389ME != null)
            {
                BLLBitacoraEventos.AgregarCambio_389ME(cambios_389ME, "Nombre", anterior_389ME.Nombre_389ME, producto_389ME.Nombre_389ME);
                BLLBitacoraEventos.AgregarCambio_389ME(cambios_389ME, "Titulo", anterior_389ME.Titulo_389ME, producto_389ME.Titulo_389ME);
                BLLBitacoraEventos.AgregarCambio_389ME(cambios_389ME, "Precio", anterior_389ME.Precio_389ME, producto_389ME.Precio_389ME);
                BLLBitacoraEventos.AgregarCambio_389ME(cambios_389ME, "Stock", anterior_389ME.Stock_389ME, producto_389ME.Stock_389ME);
            }

            bllBitacora_389ME.RegistrarEvento_389ME("Modificación de producto",
                $"Código {producto_389ME.CodProducto_389ME} ({producto_389ME.Nombre_389ME}): " + (cambios_389ME.Count == 0 ? "sin cambios" : string.Join("; ", cambios_389ME)),
                "Productos", "Media");
        }

        public void Eliminar_389ME(int codProducto_389ME)
        {
            Producto producto_389ME = dalProducto_389ME.ObtenerPorCodigo_389ME(codProducto_389ME);

            try
            {
                dalProducto_389ME.Eliminar_389ME(codProducto_389ME);
                bllDigito_389ME.Recalcular_389ME("Producto");
            }
            catch (Exception)
            {
                // La base no deja borrarlo si ya se vendio o si tiene una receta cargada
                throw new Exception("No se puede eliminar el producto porque tiene ventas o una receta cargada.");
            }

            bllBitacora_389ME.RegistrarEvento_389ME("Baja de producto",
                $"Código {codProducto_389ME} - {producto_389ME?.Nombre_389ME}", "Productos", "Alta");
        }

        private void Validar_389ME(Producto producto_389ME)
        {
            if (string.IsNullOrWhiteSpace(producto_389ME.Nombre_389ME) || string.IsNullOrWhiteSpace(producto_389ME.Titulo_389ME))
                throw new Exception("El nombre y el título del producto son obligatorios.");
            if (producto_389ME.Precio_389ME <= 0)
                throw new Exception("El precio debe ser mayor a cero.");
            // La columna es decimal(10,2): no admite importes de 100 millones o mas
            if (producto_389ME.Precio_389ME >= 100000000)
                throw new Exception("El precio no puede superar $99.999.999.");
            if (producto_389ME.Stock_389ME < 0)
                throw new Exception("El stock no puede ser negativo.");

            bool repetido_389ME = dalProducto_389ME.Listar_389ME().Exists(otro_389ME =>
                otro_389ME.CodProducto_389ME != producto_389ME.CodProducto_389ME &&
                string.Equals(otro_389ME.Nombre_389ME.Trim(), producto_389ME.Nombre_389ME.Trim(), StringComparison.OrdinalIgnoreCase));
            if (repetido_389ME)
                throw new Exception("Ya existe un producto con ese nombre.");
        }
    }
}
