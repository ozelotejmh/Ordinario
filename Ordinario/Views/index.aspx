<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="Ordinario.Views.index" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Catálogo de Muebles</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet" />
    <style>
        body {
            margin: 0;
            padding: 0;
            font-family: 'Arial', sans-serif;
            background: url('https://media.admagazine.com/photos/648cd919d6ffbb9c781e28c0/16:9/w_2560%2Cc_limit/renovar-los-muebles-sala.jpg') no-repeat center center fixed;
            background-size: cover;
        }

        .menu-placeholder {
            background-color: rgba(62, 39, 35, 0.8);
            color: white;
            display: flex;
            justify-content: space-between;
            align-items: center;
            padding: 10px 20px;
            border-radius: 15px;
            margin-bottom: 20px;
        }

            .menu-placeholder a {
                color: white;
                text-decoration: none;
                font-size: 1.1em;
                margin: 0 10px;
                transition: color 0.3s ease;
            }

                .menu-placeholder a:hover {
                    color: #e07a5f;
                }

        .overlay {
            background-color: rgba(255, 255, 255, 0.8);
            padding: 20px;
            border-radius: 15px;
            margin: 20px;
        }

        .card {
            background: white;
            border-radius: 15px;
            box-shadow: 0 4px 10px rgba(0, 0, 0, 0.2);
            padding: 20px;
            margin: 10px;
            text-align: center;
            transition: transform 0.3s ease;
        }

            .card:hover {
                transform: scale(1.03);
            }

        .card-container {
            display: grid;
            grid-template-columns: repeat(auto-fill, minmax(300px, 1fr));
            gap: 20px;
        }

        <style >
        #carritoModal .modal-dialog {
            max-width: 900px;
        }

        #carritoModal .modal-body {
            background-color: #f9f9f9;
            border-radius: 10px;
            padding: 20px;
        }

        #carritoModal .row {
            align-items: center;
        }

            #carritoModal .row p {
                margin: 0;
            }

        #carritoModal .btn-danger {
            background-color: #e07a5f;
            border: none;
        }

            #carritoModal .btn-danger:hover {
                background-color: #b63d2b;
            }
    </style>

    </style>
</head>
<body>
    <form id="form1" runat="server">
        <!-- Menú superior -->
        <div class="menu-placeholder">
            <div>Catálogo</div>
            <div>
                <asp:LinkButton ID="btnLogin" runat="server" CssClass="btn btn-link text-white" OnClientClick="return false;" data-bs-toggle="modal" data-bs-target="#loginModal">Login</asp:LinkButton>
                <asp:LinkButton ID="btnLogout" runat="server" CssClass="btn btn-link text-white" OnClick="btnLogout_Click" Visible="false">Cerrar Sesión</asp:LinkButton>
                <asp:LinkButton ID="btnCarrito" runat="server" CssClass="btn btn-link text-white" OnClientClick="return false;" data-bs-toggle="modal" data-bs-target="#carritoModal">Carrito</asp:LinkButton>

            </div>
        </div>

        <!-- Contenedor Compras -->
        <div class="modal fade" id="carritoModal" tabindex="-1" aria-labelledby="carritoModalLabel" aria-hidden="true">
            <div class="modal-dialog modal-dialog-centered modal-lg">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="carritoModalLabel">Carrito de Compras</h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body">
                        <div class="container">
                            <div class="row">
                                <div class="col-12">
                                    <asp:Repeater ID="rptCarrito" runat="server">
                                        <ItemTemplate>
                                            <div class="row border-bottom py-2">
                                                <div class="col-md-4">
                                                    <p><strong>Producto:</strong> <%# Eval("Nombre") %></p>
                                                </div>
                                                <div class="col-md-3">
                                                    <p><strong>Cantidad:</strong> <%# Eval("Cantidad") %></p>
                                                </div>
                                                <div class="col-md-3">
                                                    <p><strong>Total:</strong> $<%# Eval("Total") %></p>
                                                </div>
                                                <div class="col-md-2 text-end">
                                                    <asp:LinkButton
                                                        ID="btnEliminar"
                                                        runat="server"
                                                        CssClass="btn btn-danger btn-sm"
                                                        CommandName="Eliminar"
                                                        CommandArgument='<%# Eval("Id_mueble") %>'>
                                                Eliminar
                                                    </asp:LinkButton>
                                                </div>
                                            </div>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <asp:Button
                            ID="btnFinalizarCompra"
                            runat="server"
                            Text="Finalizar Compra"
                            CssClass="btn btn-primary"
                            OnClientClick="return false;"
                            data-bs-toggle="modal"
                            data-bs-target="#correoModal" />
                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cerrar</button>
                    </div>
                </div>
            </div>
        </div>

        <!-- Solicitar Correo -->
        <div class="modal fade" id="correoModal" tabindex="-1" aria-labelledby="correoModalLabel" aria-hidden="true">
            <div class="modal-dialog modal-dialog-centered">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="correoModalLabel">¿A qué correo enviar la compra?</h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body">
                        <asp:TextBox
                            ID="txtCorreo"
                            runat="server"
                            CssClass="form-control"
                            Placeholder="Ingrese su correo electrónico">
                        </asp:TextBox>
                    </div>
                    <div class="modal-footer">
                        <asp:Button
                            ID="btnEnviarCorreo"
                            runat="server"
                            CssClass="btn btn-primary"
                            Text="Enviar"
                            OnClick="btnEnviarCorreo_Click" />
                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
                    </div>
                </div>
            </div>
        </div>




        <!-- Contenedor Inicio Sesión -->
        <div class="modal fade" id="loginModal" tabindex="-1" aria-labelledby="loginModalLabel" aria-hidden="true">
            <div class="modal-dialog modal-dialog-centered">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="loginModalLabel">Iniciar Sesión</h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body">
                        <asp:TextBox ID="txtUser" runat="server" CssClass="form-control mb-3" Placeholder="Usuario"></asp:TextBox>
                        <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control mb-3" TextMode="Password" Placeholder="Contraseña"></asp:TextBox>
                    </div>
                    <div class="modal-footer">
                        <asp:Button ID="btnIniciarSesion" runat="server" CssClass="btn btn-primary" Text="Iniciar Sesión" OnClick="btnIniciarSesion_Click" />
                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cerrar</button>
                    </div>
                </div>
            </div>
        </div>
        <!-- Contenedor del editor -->
        <asp:Panel ID="pnlEditor" runat="server" CssClass="overlay" Visible="false">
            <h2>Editor de Muebles</h2>
            <div class="row">
                <div class="col-md-6">
                    <div class="mb-3">
                        <label for="txtID" class="form-label"><strong>ID:</strong></label>
                        <asp:TextBox ID="txtID" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="mb-3">
                        <label for="txtNombre" class="form-label"><strong>Nombre:</strong></label>
                        <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="mb-3">
                        <label for="txtMaterial" class="form-label"><strong>Material:</strong></label>
                        <asp:TextBox ID="txtMaterial" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="mb-3">
                        <label for="txtDimensiones" class="form-label"><strong>Dimensiones:</strong></label>
                        <asp:TextBox ID="txtDimensiones" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="mb-3">
                        <label for="txtPrecio" class="form-label"><strong>Precio:</strong></label>
                        <asp:TextBox ID="txtPrecio" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="mb-3">
                        <label for="txtPeso" class="form-label"><strong>Peso:</strong></label>
                        <asp:TextBox ID="txtPeso" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="mb-3">
                        <label for="txtColor" class="form-label"><strong>Color:</strong></label>
                        <asp:TextBox ID="txtColor" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="mb-3">
                        <label for="txtCantidad" class="form-label"><strong>Cantidad:</strong></label>
                        <asp:TextBox ID="txtCantidad" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
            </div>

            <!-- Botones de acción -->
            <div class="text-center mt-4">
                <asp:Button ID="btnAgregar" runat="server" Text="Agregar" CssClass="btn btn-success me-2" OnClick="btnAgregar_Click" />
                <asp:Button ID="btnActualizar" runat="server" Text="Actualizar" CssClass="btn btn-warning me-2" OnClick="btnActualizar_Click" />
                <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CssClass="btn btn-danger" OnClick="btnEliminar_Click" />
            </div>
        </asp:Panel>
        <!-- Contenedor de los muebles -->
        <div class="overlay">
            <div class="card-container">
                <asp:Repeater ID="rptMuebles" runat="server" OnItemDataBound="rptMuebles_ItemDataBound" OnItemCommand="rptMuebles_ItemCommand">
                    <ItemTemplate>
                        <div class="card">
                            <h3><%# Eval("Nombre") %></h3>
                            <p><strong>Material:</strong> <%# Eval("Material") %></p>
                            <p><strong>Dimensiones:</strong> <%# Eval("Dimensiones") %></p>
                            <p><strong>Precio:</strong> $<%# Eval("Precio") %></p>
                            <p><strong>Peso:</strong> <%# Eval("Peso") %> kg</p>
                            <p><strong>Color:</strong> <%# Eval("Color") %></p>
                            <p><strong>Cantidad disponible:</strong> <%# Eval("Cantidad_Disponible") %></p>
                            <p id="idMuebleContainer" runat="server" style="display: none;">
                                <strong>ID:</strong> <%# Eval("ID_Mueble") %>
                            </p>
                            <asp:LinkButton
                                ID="btnComprar"
                                CssClass="btn btn-comprar"
                                Text="Comprar"
                                runat="server"
                                CommandName="Comprar"
                                CommandArgument='<%# Eval("ID_Mueble") + "," + Eval("Nombre") + "," + Eval("Precio") %>'>
                            </asp:LinkButton>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>

            </div>
        </div>
    </form>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js"></script>
</body>
</html>
