<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" ValidateRequest="false"
    CodeBehind="Nuevo.aspx.cs" Inherits="CineProducto.Nuevo" %>
<asp:Content ID="TitleContent" runat="server" ContentPlaceHolderID="TitleContent">Trámite Reconocimiento Como Obra Nacional - Nueva Solicitud - Mincultura</asp:Content>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">

     <script src="<%= Page.ResolveClientUrl("~/Scripts/cine.js")%>" type="text/javascript"></script>
  
     <style>
        .progressbar
        {
            background-color: black;
            background-repeat: repeat-x;
            border-radius: 13px; /* (height of inner div) / 2 + padding */
            padding: 3px;
        }
        
        .progressbar > div
        {
            background-color: orange;
            width: 0%; /* Adjust with JavaScript */
            height: 20px;
            border-radius: 10px;
        }
    </style>
    <%--<script src="blueimp/jquery-1.11.1.js" type="text/javascript"></script>--%>
    <script src="blueimp/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="blueimp/jquery.iframe-transport.js" type="text/javascript"></script>
    <script src="blueimp/jquery.fileupload.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
        <script type="text/javaScript">
        

            $(document).ready(function () {
                $('#project_name').tooltip({ bodyHandler: function () { return '<asp:Literal id="tooltip_project_name" runat="server"></asp:Literal>'; }, showURL: false });
                $('#project_name').addClass("user-input");

            });
            </script>


                <div id="cine">
                    <form name="nuevo_proyecto" method="post" action="Nuevo.aspx">
                    <div id='Nuevo'>
                    <fieldset>
                        <legend>Creación de una nueva solicitud</legend>
                        <ul>
                            <li><asp:Label ID="message" runat="server" Text=""></asp:Label></li>
                            <li>
                                <div class="field_label">T&iacute;tulo de la obra:<span class="required_field_text">*</span></div>
                                <div class="field_input">
                                    <input type="text" id="project_name" class="inputLargo" name="project_name" />
                                </div>
                            </li>

                            <li><asp:Label ID="Label1" ForeColor="Red"  runat="server" Text="Este campo se podrá modificar durante el proceso de diligenciamiento"></asp:Label></li>
                            <li><br /></li>
                            <li>
                                <div class="field_input"><input  type="submit" id="submit" name="submit_new_project" value="Crear" /></div>
                            </li>
                        </ul>
                    </fieldset>
                    </div>
                    </form>
                </div>
                </asp:Content>