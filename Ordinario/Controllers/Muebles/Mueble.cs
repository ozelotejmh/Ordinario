using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ordinario.Controllers
{
    public class Mueble
    {
        public Mueble(int iD_Mueble, string nombre, string material, string dimensiones, decimal precio, decimal peso, string color, int cantidad_Disponible)
        {
            ID_Mueble = iD_Mueble;
            Nombre = nombre;
            Material = material;
            Dimensiones = dimensiones;
            Precio = precio;
            Peso = peso;
            Color = color;
            Cantidad_Disponible = cantidad_Disponible;
        }

        public Mueble() { }

        public int ID_Mueble { get; set; }
        public string Nombre { get; set; }
        public string Material { get; set; }
        public string Dimensiones { get; set; }
        public decimal Precio { get; set; }
        public decimal Peso { get; set; }
        public string Color { get; set; }
        public int Cantidad_Disponible { get; set; }
    }
}