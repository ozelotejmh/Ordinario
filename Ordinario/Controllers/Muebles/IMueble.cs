using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordinario.Controllers
{
    interface IMueble
    {
        // C
        bool agregarMueble(string nombre, string material, string dimensiones, decimal precio, decimal peso, string color, int cantidad_Disponible);
        // R
        List<Mueble> mostrarMuebles();
        // U
        bool actualizarMueble(string nombre, string material, string dimensiones, decimal precio, decimal peso, string color, int cantidad_Disponible, int iD_Mueble);
        // D
        bool borrarMueble(int iD_Mueble);

    }
}
