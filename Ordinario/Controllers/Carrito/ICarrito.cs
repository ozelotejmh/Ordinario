using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordinario.Controllers.Carrito
{
    interface ICarrito
    {
        List<Carrito> mostrarcarrito();
        (bool, int) agregarcompra(int id_mueble, string nombre, decimal precio);
        bool quitarcompra(int id_mueble);
        void realizarcompra(string direccion, List<Carrito> carritos);
    }
}
