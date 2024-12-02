using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ordinario.Models.DBMueblesTableAdapters;

namespace Ordinario.Controllers
{
    public class ControladorMueble : IMueble
    {
        List<Mueble> muebles = new List<Mueble>();
        mueblesTableAdapter a = new mueblesTableAdapter();
        public bool actualizarMueble(string nombre, string material, string dimensiones, decimal precio, decimal peso, string color, int cantidad_Disponible, int iD_Mueble)
        {
            try
            {
                a.ActualizarMueble(nombre, material, dimensiones, precio, peso, color, cantidad_Disponible, iD_Mueble);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool agregarMueble(string nombre, string material, string dimensiones, decimal precio, decimal peso, string color, int cantidad_Disponible)
        {
            try
            {
                using (a)
                {
                    a.AgregarMueble(nombre, material, dimensiones, precio, peso, color, cantidad_Disponible);
                }
                    return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool borrarMueble(int iD_Mueble)
        {
            try
            {
                using (a)
                {
                    a.BorrarMueble(iD_Mueble);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<Mueble> mostrarMuebles()
        {
            using (a)
            {
                var b = a.GetMuebles();
                foreach (var c in b)
                {
                    Mueble mueble = new Mueble();
                    mueble.ID_Mueble = Convert.ToInt32(c["ID_Mueble"]);
                    mueble.Nombre = c["Nombre"].ToString();
                    mueble.Material = c["Material"].ToString();
                    mueble.Dimensiones = c["Dimensiones"].ToString();
                    mueble.Precio = Convert.ToDecimal(c["Precio"]);
                    mueble.Peso = Convert.ToDecimal(c["Peso"]);
                    mueble.Color = c["Color"].ToString();
                    mueble.Cantidad_Disponible = Convert.ToInt32(c["Cantidad_Disponible"]);
                    muebles.Add(mueble);
                }
            }
            return muebles;
        }
    }
}