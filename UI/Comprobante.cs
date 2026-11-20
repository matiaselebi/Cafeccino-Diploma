using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using BE;

namespace CAFECCINO_INGENIERIA_DE_SOFTWARE
{
    // Arma el texto de las facturas de venta y de las ordenes de compra y lo guarda en PDF.
    // Para el PDF se usa la impresora "Microsoft Print to PDF" que trae Windows, asi no hace falta instalar nada.
    public static class Comprobante
    {
        // En la PC de desarrollo el programa corre desde UI\bin\Debug y los PDF van a la carpeta de la solucion (tres mas arriba).
        // Instalado van a Documentos\Cafeccino, porque en Archivos de programa no se puede escribir.
        private static readonly string carpetaSolucion_389ME = Path.GetFullPath(Path.Combine(Application.StartupPath, @"..\..\.."));
        private static readonly string carpetaBase_389ME = File.Exists(Path.Combine(carpetaSolucion_389ME, "CAFECCINO.sln"))
            ? carpetaSolucion_389ME
            : Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Cafeccino");
        public static readonly string CarpetaVentas_389ME = Path.Combine(carpetaBase_389ME, "Facturas Venta");
        public static readonly string CarpetaCompras_389ME = Path.Combine(carpetaBase_389ME, "Facturas Compra");

        public static string TextoFactura_389ME(Factura factura_389ME)
        {
            StringBuilder texto_389ME = new StringBuilder();
            texto_389ME.AppendLine("CAFECCINO");
            texto_389ME.AppendLine("Factura N° " + factura_389ME.CodFactura_389ME);
            texto_389ME.AppendLine(new string('-', 60));
            texto_389ME.AppendLine("Fecha: " + factura_389ME.Fecha_389ME.ToString("dd/MM/yyyy HH:mm"));
            texto_389ME.AppendLine("DNI cliente: " + factura_389ME.DNICliente_389ME);
            texto_389ME.AppendLine("Metodo de pago: " + factura_389ME.MetodoPago_389ME);
            if (factura_389ME.MetodoPago_389ME == "Tarjeta")
            {
                texto_389ME.AppendLine("Banco: " + factura_389ME.Banco_389ME);
                texto_389ME.AppendLine("Tipo de tarjeta: " + factura_389ME.TipoTarjeta_389ME);
                texto_389ME.AppendLine("Marca de tarjeta: " + factura_389ME.MarcaTarjeta_389ME);
                texto_389ME.AppendLine("Recargo por pago con tarjeta: " + (BLL.BLLFactura.RecargoTarjeta_389ME * 100).ToString("N0") + "%");
            }
            texto_389ME.AppendLine();
            texto_389ME.AppendLine(string.Format("{0,-24}{1,8}{2,14}{3,14}", "Producto", "Cant.", "Precio", "Subtotal"));
            foreach (DetalleFactura detalle_389ME in factura_389ME.Detalles_389ME)
                texto_389ME.AppendLine(string.Format("{0,-24}{1,8}{2,14:N2}{3,14:N2}", detalle_389ME.NombreProducto_389ME,
                    detalle_389ME.Cantidad_389ME, detalle_389ME.PrecioUnitario_389ME, detalle_389ME.Subtotal_389ME));
            texto_389ME.AppendLine(new string('-', 60));
            texto_389ME.AppendLine("Monto total: $" + factura_389ME.MontoTotal_389ME.ToString("N2"));
            return texto_389ME.ToString();
        }

        public static string TextoOrdenCompra_389ME(OrdenCompra orden_389ME, List<OrdenCompraDetalle> detalles_389ME)
        {
            StringBuilder texto_389ME = new StringBuilder();
            texto_389ME.AppendLine("CAFECCINO");
            texto_389ME.AppendLine("Orden de compra N° " + orden_389ME.CodOrden_389ME);
            texto_389ME.AppendLine(new string('-', 60));
            texto_389ME.AppendLine("Fecha: " + orden_389ME.FechaCreacion_389ME.ToString("dd/MM/yyyy"));
            texto_389ME.AppendLine("Proveedor: " + orden_389ME.NombreProveedor_389ME + " - CUIT " + orden_389ME.CUIT_389ME);
            texto_389ME.AppendLine("Solicitud de cotizacion N° " + orden_389ME.CodSolicitud_389ME);
            texto_389ME.AppendLine("Estado: " + orden_389ME.Estado_389ME);
            texto_389ME.AppendLine();
            texto_389ME.AppendLine(string.Format("{0,-24}{1,10}{2,13}{3,13}", "Insumo", "Cantidad", "Precio", "Subtotal"));
            foreach (OrdenCompraDetalle detalle_389ME in detalles_389ME)
                texto_389ME.AppendLine(string.Format("{0,-24}{1,10:0.###}{2,13:N2}{3,13:N2}", detalle_389ME.NombreInsumo_389ME,
                    detalle_389ME.Cantidad_389ME, detalle_389ME.PrecioUnitario_389ME, detalle_389ME.Subtotal_389ME));
            texto_389ME.AppendLine(new string('-', 60));
            texto_389ME.AppendLine("Precio total: $" + orden_389ME.PrecioTotal_389ME.ToString("N2"));
            return texto_389ME.ToString();
        }

        // Imprime el texto en un PDF dentro de la carpeta y devuelve la ruta del archivo
        public static string GuardarPdf_389ME(string carpeta_389ME, string nombre_389ME, string texto_389ME)
        {
            // Si la carpeta no existe se crea; si ya existe no se toca lo que tiene adentro
            Directory.CreateDirectory(carpeta_389ME);

            // Nunca se pisa un PDF anterior: si el nombre ya existe se agrega (2), (3), ...
            string ruta_389ME = Path.Combine(carpeta_389ME, nombre_389ME + ".pdf");
            for (int copia_389ME = 2; File.Exists(ruta_389ME); copia_389ME++)
                ruta_389ME = Path.Combine(carpeta_389ME, nombre_389ME + " (" + copia_389ME + ").pdf");

            string[] lineas_389ME = texto_389ME.Replace("\r", "").Split('\n');
            int siguienteLinea_389ME = 0;

            using (PrintDocument documento_389ME = new PrintDocument())
            using (Font fuente_389ME = new Font("Courier New", 10))
            {
                documento_389ME.PrinterSettings.PrinterName = "Microsoft Print to PDF";
                if (!documento_389ME.PrinterSettings.IsValid)
                    throw new Exception("No está instalada la impresora \"Microsoft Print to PDF\" de Windows.");

                documento_389ME.PrinterSettings.PrintToFile = true;
                documento_389ME.PrinterSettings.PrintFileName = ruta_389ME;
                documento_389ME.PrintController = new StandardPrintController();   // sin la ventana "Imprimiendo..."

                // Se escribe una linea abajo de la otra; si no entran en la hoja se sigue en otra
                documento_389ME.PrintPage += (sender_389ME, e_389ME) =>
                {
                    float y_389ME = e_389ME.MarginBounds.Top;
                    float alto_389ME = fuente_389ME.GetHeight(e_389ME.Graphics);
                    while (siguienteLinea_389ME < lineas_389ME.Length && y_389ME + alto_389ME < e_389ME.MarginBounds.Bottom)
                    {
                        e_389ME.Graphics.DrawString(lineas_389ME[siguienteLinea_389ME], fuente_389ME, Brushes.Black, e_389ME.MarginBounds.Left, y_389ME);
                        y_389ME += alto_389ME;
                        siguienteLinea_389ME++;
                    }
                    e_389ME.HasMorePages = siguienteLinea_389ME < lineas_389ME.Length;
                };

                documento_389ME.Print();
            }

            // La impresora de PDF escribe el archivo un instante despues de Print(): se espera hasta 10 segundos
            for (int espera_389ME = 0; espera_389ME < 50 && !File.Exists(ruta_389ME); espera_389ME++)
                System.Threading.Thread.Sleep(200);

            return ruta_389ME;
        }
    }
}
