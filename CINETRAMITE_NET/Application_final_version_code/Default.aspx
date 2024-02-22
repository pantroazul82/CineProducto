<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="CineProducto._Default" %>

<asp:Content ID="TitleContent" runat="server" ContentPlaceHolderID="TitleContent">Trámite Reconocimiento Como Obra Nacional - Página Principal - Mincultura</asp:Content>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent"></asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div style="margin: 0px auto; width: 1200px">


        <div class="span-15">
            <div class="span-15 last">
                <%if (!loggeduser)
                    { %>
                <table class="instrucciones">
                    <tbody>
                        <tr>
                            <td colspan="2">
                                <p class="titulo">Bienvenido</p>


                                <!-- <p class="resumen">A continuación encontrará las instrucciones para utilizar el aplicativo  de trámite en línea para Reconocimiento como Proyecto Nacional.</p> -->
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div class="instrucciones_imagen">

                                    <img alt="Boton de acceso al primer paso de las instrucciones" src="/images/boton_paso_1.png" />

                                </div>
                                <div class="instrucciones">
                                    <p>Diligencie el <b>Registro de usuarios, </b>abajo a su derecha, y en su correo electrónico recibirá los datos de la cuenta para poder ingresar al aplicativo.</p>
                                </div>
                            </td>
                            <td>
                                <div class="instrucciones_imagen">

                                    <img alt="Boton de acceso al segundo paso de las instrucciones" src="/images/boton_paso_2.png" />

                                </div>
                                <div class="instrucciones">
                                    <p>
                                        Diligencie los datos de su cuenta en <b>Ingreso para usuarios registrados</b> y pique sobre el botón acceder. 
Al ingresar puede <b>Crear</b> una nueva solicitud o <b>Consultar</b> las ya existentes.
                                    </p>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div>
                                    <img alt="Boton de acceso al tercer paso de las instrucciones" src="/images/boton_paso_3.png" />
                                </div>
                                <div>
                                    <p>Cargue la información y los documentos requeridos, siguiendo el orden indicado por el aplicativo. Los campos obligatorios están marcados con un asterisco de color rojo. Para crear una solicitud es indispensable conocer las normas aplicables (Decreto 1080 de 2015 modificado por el Decreto 525 de 2021, y la Resolución 1021 de 2016).</p>
                                </div>
                            </td>
                            <td>
                                <div>
                                    <img alt="Boton de acceso al cuarto paso de las instrucciones" src="/images/boton_paso_4.png" />
                                </div>
                                <div>
                                    <p>Una vez cargados todos los datos y documentos requeridos, desde el formulario <b>Finalización</b> envíe la solicitud a la Dirección de Audiovisuales, Cine y Medios Interactivos.</p>
                                </div>
                            </td>
                        </tr>
                        <!--tr>
                    <td colspan="2">
                        <button type="button" class="btn btn-primary"><a target="_blank" href="https://cineproducto.mincultura.gov.co/uploads/instructivo.pdf" style="color:white">Consultar Manual</a></button>
                            <button type="button" class="btn btn-primary"><a target="_blank" href="https://cineproducto.mincultura.gov.co/uploads/resolucion.pdf" style="color:white">Consultar Resolución</a></button>
                        </td>
                </tr-->

                        <tr>
                            <td colspan="2">
                                <div class="alert alert-warning">
                                    Si su solicitud fue radicada despues del 19 de agosto de 2021 puede consultar sus certificados de Producto Nacional Expedidos
                            <br />
                                    <br />
                                    <button type="button" class="btn btn-primary"><a href="ValidarCertificadoProducto.aspx" style="color: white">Consultar Certificado</a></button>
                                </div>

                            </td>
                        </tr>
                    </tbody>
                </table>
                <% 
                    }
                    else
                    { %>

                <div class="row">
                    <div class="col-12">
                        <p class="titulo">Bienvenido</p>
                        <p class="resumen">
                            Las normas que regulan este trámite son el Decreto 1080 de 2015 modificado por el Decreto 525 de 2021, y la Resolución 1021 de 2016. 
                
                        </p>

                        <br />
                        <br />

                        <%if (Session["user_role_id"] != null && (Session["user_role_id"].ToString() == "0" || Session["user_role_id"].ToString() == "1"))
                            { %>
                        <a href="Nuevo.aspx">
                            <img alt="Crear" src="images/btn_crear_nueva_solicitud.png" /></a>
                        <%} %>


                        <%if (Session["user_role_id"] != null && Session["user_role_id"].ToString() == "7")
                            { %>
                        <a href="ListProjectos.aspx">
                            <img alt="Consultar Proyectos" src="images/btn_consultar_mis_solicitudes.png" /></a>

                        <%}
                            else
                            { %>
                        <a href="Lista.aspx">
                            <img alt="Consultar" src="images/btn_consultar_mis_solicitudes.png" /></a>

                        <%} %>


                        <%if (Session["user_role_id"] != null && (Session["user_role_id"].ToString() == "2" || Session["user_role_id"].ToString() == "3" || Session["user_role_id"].ToString() == "4" || Session["user_role_id"].ToString() == "5"))
                            { %>
                        <a href="ListaHojaTransferencia.aspx">
                            <img alt="Consultar Proyectos" src="images/btn_GestionDocumental.png" /></a>
                        <%} %>
                    </div>
                    <br />
                    <table>
                        <tr>
                            <!--td colspan="2">
            <br />
            <br />
            <br />
                        <button type="button" class="btn btn-primary"><a target="_blank" href="https://cineproducto.mincultura.gov.co/uploads/instructivo.pdf" style="color:white">Consultar Manual</a></!--button>
                            <button type="button" class="btn btn-primary"><a target="_blank" href="https://cineproducto.mincultura.gov.co/uploads/resolucion.pdf" style="color:white">Consultar Resolución</a></button>
                        </td-->
                        </tr>
                    </table>
                </div>


                <%} %>
            </div>
        </div>
        <div class="span-9 last">
            <%if (!loggeduser)
                { %>
            <div class="span-9 last">
                <form method="post" action="" id="autenticacion_rapida">
                    <div class="fondo_ingreso">Ingreso para usuarios registrados</div>
                    <div class="autenticacion_rapida">
                        <span style="color: red">
                            <asp:Label ID="auth_error_message" runat="server"></asp:Label></span>
                        <fieldset>
                            <label for="usuario">Correo electrónico</label>
                            <input type="text" name="user" value="" />
                            <label for="password">Contraseña</label>
                            <input type="password" name="pass" value="" />
                            <button type="submit">Acceder</button>
                        </fieldset>
                        <div id="link-forgot-password">
                            <a href="frmRecordarPass.aspx" style="text-align: right; width: 100%; font-size: 0.8em; color: #005F8C;">¿Olvidó contraseña?</a>
                        </div>
                    </div>
                </form>
            </div>
            <div class="clear span-9 last">
                <span style="color: green">
                    <asp:Label ID="confirmacion_creacion_cuenta" runat="server"></asp:Label></span>
                <span style="color: red">
                    <asp:Label ID="error_creacion_cuenta" runat="server"></asp:Label></span>
                <form method="post" name="form" action="">
                    <table cellspacing="0" class="register-form">
                        <tbody>
                            <tr>
                                <td colspan="2">
                                    <img alt="Titulo del formulario de creacion de nuevo usuario" src="/images/titulo_login.gif" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <p id="instruccion-formulario-registro">Si usted es un usuario nuevo y aún no tiene cuenta de acceso, por favor ingrese sus datos para obtener una cuenta.</p>
                                </td>
                            </tr>
                            <tr>
                                <td class="label">Nombres:</td>
                                <td>
                                    <input maxlength="200" type="text" name="nombre_form" /></td>
                            </tr>
                            <tr>
                                <td class="label">Apellidos:</td>
                                <td>
                                    <input maxlength="200" type="text" name="apellido_form" /></td>
                            </tr>
                            <tr>
                                <td class="label">Correo:</td>
                                <td>
                                    <input maxlength="200" type="text" name="email_form" /></td>
                            </tr>
                            <tr>

                                <td colspan="2">
                                    <asp:Image ID="imgCaptcha" runat="server" ImageUrl="~/CImage.aspx" />
                                </td>
                            </tr>
                            <tr>
                                <td class="label">Texto de la imagen:</td>
                                <td>
                                    <input maxlength="200" type="text" name="txtcaptcha" /></td>
                            </tr>
                            <tr>
                                <td class="register-button" colspan="2">
                                    <input id="registrar" type="image" alt="Boton de envio del formulario de creacion de nuevo usuario" src="/images/btn_registro.gif" name="enviar" />
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </form>
                <script>

                    //testButton.addEventListener("click", deshabilitarregistro);

                    /*function deshabilitarregistro()
                    {
                        
                        //const deshabilitarRegistro = () => {
                        $("registrar").hide();
                        
                        //};
                    }*/
                    $(document).ready(function () {
                        //alert("Mensaje123");
                        $("#registrar").click(function () {
                            //alert("Mensaje");
                            $("#registrar").hide();
                        });
                    });
                    
                </script>
            </div>
            <%}%>
        </div>


    </div>
</asp:Content>
