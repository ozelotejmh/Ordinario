using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ordinario.Controllers.Usuario
{
    public class Usuario
    {
        public Usuario(int id_usuario, string nombre_usuario, string correo_electronico, string contrasena, DateTime fecha_creacion)
        {
            this.id_usuario = id_usuario;
            this.nombre_usuario = nombre_usuario;
            this.correo_electronico = correo_electronico;
            this.contrasena = contrasena;
            this.fecha_creacion = fecha_creacion;
        }

        public Usuario() { }

        public int id_usuario { get; set; }
        public string nombre_usuario { get; set; }
        public string correo_electronico { get; set; }
        public string contrasena { get; set; }
        public DateTime fecha_creacion { get; set; }
    }
}