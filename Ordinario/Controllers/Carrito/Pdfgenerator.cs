using System;
using System.Collections.Generic;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using System.IO;


namespace Ordinario.Controllers.Carrito
{
    public class Pdfgenerator
    {
        public static byte[] GenerarFactura(List<Carrito> carrito)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                // Crear un documento PDF
                PdfWriter writer = new PdfWriter(ms);
                PdfDocument pdf = new PdfDocument(writer);
                Document document = new Document(pdf);

                // Encabezado
                document.Add(new Paragraph("Factura de Compra")
                    .SetTextAlignment(TextAlignment.CENTER)
                    .SetFontSize(20));

                document.Add(new Paragraph($"Fecha: {DateTime.Now}")
                    .SetTextAlignment(TextAlignment.LEFT)
                    .SetFontSize(12));
                document.Add(new Paragraph("\n"));

                // Crear una tabla
                Table table = new Table(3); // Número de columnas
                table.AddCell("Producto");
                table.AddCell("Cantidad");
                table.AddCell("Total");

                // Agregar datos del carrito
                foreach (var item in carrito)
                {
                    table.AddCell(item.Nombre);
                    table.AddCell(item.Cantidad.ToString());
                    table.AddCell(item.Total.ToString());
                }

                document.Add(table);

                // Cerrar el documento
                document.Close();
                return ms.ToArray(); // Devuelve el PDF como un arreglo de bytes
            }
        }
    }
}