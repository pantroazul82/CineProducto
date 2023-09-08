<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmRecordarPass.aspx.cs" Inherits="CineProducto.frmRecordarPass" %>
<asp:Content ID="TitleContent" runat="server" ContentPlaceHolderID="TitleContent">Trámite Reconocimiento Como Obra Nacional - Cambio de contraseña</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div id="cine">
    <form name="nuevo_proyecto" method="post" action="Nuevo.aspx">
    <div id='Nuevo'>
    <fieldset>
        <legend>Recordar contraseña</legend>
        <ul>
            
            <li>
                <div class="field_label">Ingrese su nombre de usuario
                    <span class="required_field_text">*</span></div>
                <div class="field_input">
<asp:TextBox runat="server" ID="txtUsuario" ></asp:TextBox>
                </div>
            </li>

<li><asp:Label ID="lblError" ForeColor="Red" Font-Bold="true" runat="server"></asp:Label></li>
            <li>
                <div class="field_input">
                    <asp:Button runat="server" ID="btnRecordar" Text="Recordar contraseña"  OnClick="btnRecordar_Click" />
                    <asp:HyperLink ID="linkInicio"  runat="server" NavigateUrl="~/Default.aspx" Text="Volver al Inicio"></asp:HyperLink>
                </div>
            </li>
        </ul>
    </fieldset>
    </div>
    </form>
</div>
</asp:Content>
