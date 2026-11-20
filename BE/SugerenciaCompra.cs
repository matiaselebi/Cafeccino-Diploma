using System;
using System.Collections.Generic;
using System.Linq;

namespace BE
{
    // Resultado del calculo de reposicion de un insumo. No se guarda: se calcula cada vez.
    public class SugerenciaCompra
    {
        public int CodInsumo_389ME { get; set; }
        public string Nombre_389ME { get; set; }
        public string UnidadMedida_389ME { get; set; }
        public double Stock_389ME { get; set; }
        public double StockMinimo_389ME { get; set; }
        public double StockMaximo_389ME { get; set; }
        public double ConsumoEstimado_389ME { get; set; }
        public double StockProyectado_389ME { get; set; }
        public double CantidadSugerida_389ME { get; set; }
        public bool Comprar_389ME { get; set; }
    }
}
