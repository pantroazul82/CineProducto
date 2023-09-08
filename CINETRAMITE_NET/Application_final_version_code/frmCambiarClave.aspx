<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmCambiarClave.aspx.cs" Inherits="CineProducto.frmCambiarClave" %>
<asp:Content ID="TitleContent" runat="server" ContentPlaceHolderID="TitleContent">Trámite Reconocimiento Como Obra Nacional - Cambio de contraseña</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div id="cine">
    <form name="nuevo_proyecto" method="post" action="Nuevo.aspx">
    <div id='Nuevo'>
    <fieldset>
        <legend>Cambio de contraseña</legend>
        <ul>
            
            <li>
                <div class="field_label">Contraseña actual
                    <span class="required_field_text">*</span></div>
                <div class="field_input">
<asp:TextBox runat="server" ID="txtPasswordActual" TextMode="Password"></asp:TextBox>
                </div>
            </li>

            <li>
                <div class="field_label">Contraseña nueva
                    <span class="required_field_text">*</span></div>
                <div class="field_input">
<asp:TextBox runat="server" ID="txtPasswordNueva" TextMode="Password"></asp:TextBox>
                </div>
            </li>

            <li>
                <div class="field_label">Confirmacion contraseña nueva
                    <span class="required_field_text">*</span></div>
                <div class="field_input">
<asp:TextBox runat="server" ID="txtPasswordConfirmacion" TextMode="Password"></asp:TextBox>
                </div>
            </li>
<li><asp:Label ID="lblError" ForeColor="Red" Font-Bold="true" runat="server"></asp:Label></li>
            <li>
                <div class="field_input">
                    <asp:Button runat="server" OnClick="btnCambiarPassword_Click" ID="btnCambiarPassword" Text="Guardar" />
                    <asp:HyperLink ID="linkInicio" Visible="false" runat="server" NavigateUrl="~/Default.aspx" Text="Volver al Inicio"></asp:HyperLink>
                </div>
            </li>
        </ul>
    </fieldset>
    </div>
    </form>
</div>
</asp:Content>
