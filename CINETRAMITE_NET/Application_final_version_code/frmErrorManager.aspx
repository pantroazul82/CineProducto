<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmErrorManager.aspx.cs" Inherits="CineProducto.frmErrorManager" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="lblTitulo" runat="server" Font-Size="24px" Font-Bold="true" Text="Problema encontrado en la aplicacion."></asp:Label>
    <br />
    <asp:Label ID="lblError" runat="server" Text="" Font-Size="12px"></asp:Label>
</asp:Content>
