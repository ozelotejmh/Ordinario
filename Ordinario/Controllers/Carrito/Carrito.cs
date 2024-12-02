using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ordinario.Controllers.Carrito
{
    public class Carrito
    {
        public Carrito(int id_mueble, string nombre, decimal precio)
        {
            Id_mueble = id_mueble;
            Nombre = nombre;
            Precio = precio;
            Cantidad = 1;
            Total = 0;
        }

        public int Id_mueble { get; set; }
        public decimal Precio { get; set; }
        public string Nombre { get; set; }
        public int Cantidad { get; set; }
        public decimal Total { get; set; }
    }
}