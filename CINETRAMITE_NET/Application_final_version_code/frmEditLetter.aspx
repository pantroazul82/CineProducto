<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmEditLetter.aspx.cs" Inherits="CineProducto.frmEditLetter" %>
<asp:Content ID="TitleContent" runat="server" ContentPlaceHolderID="TitleContent">Trámite Reconocimiento Como Obra Nacional - Opciones de Administración - Mincultura</asp:Content>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
  
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        Opciones de administración
    </h2>
    <p>
        <a href="~/Lista.aspx" runat="server" type="text/asp"><< Volver al listado de solicitudes</a>
    </p>
    
 
        <div>Edición de la carta de aclaraciones<br />
   
        <fieldset>
        <legend>Información básica de la carta</legend>
        <span>Saludo : </span><br /><asp:TextBox   runat="server" TextMode="SingleLine" id="letter_greeting"  /><br />
        <span>Cuerpo de la carta (Maximo 1500 Caracteres) : </span><br /><asp:TextBox Height="150"   runat="server" TextMode="MultiLine" name="body" id="letter_body" class="letter_body" Rows="10" Columns="125" maxlength="1500" ></asp:TextBox><br />
        <span>Parrafo final (Maximo 1500 Caracteres) : </span><br /><asp:TextBox Height="150"  runat="server" TextMode="MultiLine" name="body" id="letter_prefirma" class="letter_prefirma" Rows="10" Columns="125" maxlength="1500" ></asp:TextBox><br />
        <span>Firma : </span><br /><asp:TextBox   runat="server" TextMode="SingleLine" type="text" name="letter_footer_message" id="letter_footer_message" class="letter_footer_message"  /><br />
        <span>Nota : </span><br /><asp:TextBox   runat="server" TextMode="SingleLine" type="text" name="letter_note" id="letter_note" class="letter_note"/><br /><br />
        <asp:Label runat="server" ID="lblError" ForeColor="Red"></asp:Label>
            <asp:Button runat="server" Text="Guardar" ID="btnguardar" OnClick="btnguardar_Click" />
            
        </fieldset>
   
        </div>
 
</asp:Content>
