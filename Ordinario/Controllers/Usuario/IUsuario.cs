using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordinario.Controllers.Usuario
{
    interface IUsuario
    {
        bool logeo(string name, string contra);
    }
}
