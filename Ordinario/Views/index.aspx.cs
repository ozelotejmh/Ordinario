using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ordinario.Controllers;
using System.Web.UI.HtmlControls;
using Ordinario.Controllers.Usuario;
using System.Collections.Generic;
using Ordinario.Controllers.Carrito;


namespace Ordinario.Views
{
    public partial class index : System.Web.UI.Page
    {
        ControladorMueble muebles = new ControladorMueble();
        ControladorUsuario usuarios = new ControladorUsuario();
        public ControladorCarrito carrito
        {
            get
            {
                if (Session["Carrito"] == null)
                {
                    Session["Carrito"] = new ControladorCarrito();
                }
                return (ControladorCarrito)Session["Carrito"];
            }
            set
            {
                Session["Carrito"] = value;
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bool isLoggedIn = Session["username"] != null;

                btnLogin.Visible = !isLoggedIn;
                btnCarrito.Visible = !isLoggedIn;
                btnLogout.Visible = isLoggedIn;
                pnlEditor.Visible = isLoggedIn;
            }
            RecargarMuebles();
            RecargarCarrito();
        }

        // LOGEO

        protected void btnIniciarSesion_Click(object sender, EventArgs e)
        {
            string username = txtUser.Text;
            string password = txtPassword.Text;
            if (usuarios.logeo(username, password))
            {
                Session["username"] = username;
                btnLogin.Visible = false;
                btnCarrito.Visible = false;
                btnLogout.Visible = true;
                pnlEditor.Visible = true;

                rptMuebles.DataBind();
            }
            else
            {
                Response.Write("<script>alert('Usuario o contraseña incorrectos');</script>");
            }
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Remove("username");
            Response.Redirect(Request.RawUrl);
        }

        // CAMBIOS DE SESION

        protected void rptMuebles_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                bool isLoggedIn = Session["username"] != null;

                var idContainer = (HtmlGenericControl)e.Item.FindControl("idMuebleContainer");
                var btnComprar = (LinkButton)e.Item.FindControl("btnComprar");
                if (idContainer != null)
                {
                    idContainer.Style["display"] = isLoggedIn ? "block" : "none";
                }
                if (btnComprar != null)
                {
                    btnComprar.Visible = !isLoggedIn; // Solo visible si no hay sesión
                }
            }
        }

        // ÇOMPRAS

        protected void RecargarCarrito()
        {
            List<Carrito> compra = carrito.mostrarcarrito();
            foreach (var c in compra)
            {
                c.Total = c.Cantidad * c.Precio;
            }
            rptCarrito.DataSource = compra;
            compra = new List<Carrito>();
            rptCarrito.DataBind();
            
        }

        protected void rptMuebles_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Comprar")
            {
                string[] args = e.CommandArgument.ToString().Split(',');

                int idMueble = Convert.ToInt32(args[0]);
                string nombreMueble = args[1];
                decimal precioMueble = Convert.ToDecimal(args[2]);

                (bool, int) regreso = carrito.agregarcompra(idMueble, nombreMueble, precioMueble);
                Session["cart"] = carrito.mostrarcarrito();
                RecargarCarrito();
                if (regreso.Item1)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", $"alert('Mueble: {nombreMueble} añadido al carrito. Restantes: {regreso.Item2 - 1}');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", $"alert('No se ha añadido: {nombreMueble} al carrito.');", true);
                }
            }
        }

        protected void btnEnviarCorreo_Click(object sender, EventArgs e)
        {
            carrito.realizarcompra(txtCorreo.Text, carrito.mostrarcarrito());
        }

        // MUEBLES CRUD

        protected void RecargarMuebles()
        {
            rptMuebles.DataSource = muebles.mostrarMuebles();
            rptMuebles.DataBind();
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            if (muebles.agregarMueble(txtNombre.Text, txtMaterial.Text, txtDimensiones.Text, Convert.ToDecimal(txtPrecio.Text), Convert.ToDecimal(txtPeso.Text), txtColor.Text, Convert.ToInt32(txtCantidad.Text)))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertSuccess", "alert('Mueble agregado correctamente.');", true);
                RecargarMuebles();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertError", "alert('Hubo un error al agregar el mueble.');", true);
            }
        }

        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            if (muebles.actualizarMueble(txtNombre.Text, txtMaterial.Text, txtDimensiones.Text, Convert.ToDecimal(txtPrecio.Text), Convert.ToDecimal(txtPeso.Text), txtColor.Text, Convert.ToInt32(txtCantidad.Text), Convert.ToInt32(txtID.Text)))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertSuccess", "alert('Mueble actualizado correctamente.');", true);
                RecargarMuebles();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertError", "alert('Hubo un error al actualizar el mueble.');", true);
            }
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            if (muebles.borrarMueble(Convert.ToInt32(txtID.Text)))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertSuccess", "alert('Mueble borrado correctamente.');", true);
                RecargarMuebles();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertError", "alert('Hubo un error al borrar el mueble.');", true);
            }
        }
    }
}
