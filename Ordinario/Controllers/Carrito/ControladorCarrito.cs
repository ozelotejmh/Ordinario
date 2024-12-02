using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Mail;
using System.IO;


namespace Ordinario.Controllers.Carrito
{
    public class ControladorCarrito : ICarrito
    {
        List<Carrito> compra = new List<Carrito>();
        ControladorMueble mueble = new ControladorMueble();
        public (bool, int) agregarcompra(int id_mueble, string nombre, decimal precio)
        {
            try
            {
                int total;
                int diferencial;
                foreach (var mueble in mueble.mostrarMuebles())
                {
                    if (mueble.ID_Mueble == id_mueble)
                    {
                        total = mueble.Cantidad_Disponible;
                        foreach (var carrito in compra)
                        {
                            if (carrito.Id_mueble == id_mueble)
                            {
                                diferencial = total - carrito.Cantidad;
                                if (diferencial <= 0)
                                {
                                    return (false, 0);
                                }
                                else
                                {
                                    carrito.Cantidad++;
                                    return (true, diferencial);
                                }
                            }
                        }
                        compra.Add(new Carrito(id_mueble, nombre, precio));
                        return (true, total);
                    }
                    else
                    {
                        continue;
                    }
                }
                return (false, 0);
            }
            catch (Exception)
            {
                return (false, 0);
            }
        }

        public List<Carrito> mostrarcarrito()
        {
            compra = compra.Where(carrito => carrito.Cantidad > 0).ToList();
            foreach (var carrito in compra)
            {
                carrito.Total = carrito.Cantidad * carrito.Precio;
            }

            return compra;
        }

        public bool quitarcompra(int id_mueble)
        {
            try
            {
                foreach (var carrito in compra)
                {
                    if (carrito.Id_mueble == id_mueble)
                    {
                        carrito.Cantidad--;
                        return true;
                    }
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void realizarcompra(string direccion, List<Carrito> carritos)
        {
            MailMessage correo = new MailMessage();
            correo.From = new MailAddress("112374@alumnouninter.mx");
            // Destinatario(s)
            correo.To.Add(direccion);
            //correo.To.Add("ecorrales@uninter.edu.mx");

            correo.Subject = "Gracias por su compra";
            // Cuerpo
            correo.Body = "Su pedido";

            byte[] pdfBytes = Pdfgenerator.GenerarFactura(carritos);

            // Crear el archivo adjunto
            Attachment pdfAttachment = new Attachment(new MemoryStream(pdfBytes), "Factura.pdf", "application/pdf");
            correo.Attachments.Add(pdfAttachment);

            SmtpClient clienteSMTP = new SmtpClient("smtp.office365.com", 587);
            clienteSMTP.Credentials = new NetworkCredential("112374@alumnouninter.mx", "Jaker12.");
            clienteSMTP.EnableSsl = true;

            clienteSMTP.Send(correo);
        }
    }
}