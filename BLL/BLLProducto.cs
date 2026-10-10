using System;
using System.Collections.Generic;
using BE;
using DAL;

namespace BLL
{
    public class BLLProducto
    {
        private readonly DALProducto dalProducto_389ME = new DALProducto();

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
        }

        public void Modificar_389ME(Producto producto_389ME)
        {
            Validar_389ME(producto_389ME);
            dalProducto_389ME.Modificar_389ME(producto_389ME);
        }

        public void Eliminar_389ME(int codProducto_389ME)
        {
            dalProducto_389ME.Eliminar_389ME(codProducto_389ME);
        }

        private void Validar_389ME(Producto producto_389ME)
        {
            if (string.IsNullOrWhiteSpace(producto_389ME.Nombre_389ME))
                throw new Exception("El nombre del producto es obligatorio.");
            if (producto_389ME.Precio_389ME <= 0)
                throw new Exception("El precio debe ser mayor a cero.");
            if (producto_389ME.Stock_389ME < 0)
                throw new Exception("El stock no puede ser negativo.");
        }
    }
}
