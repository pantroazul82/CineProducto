<%@ Page Title="Previsualización de la Solicitud de Aclaraciones" Language="C#" AutoEventWireup="True"
    CodeBehind="SolicitudAclaraciones.aspx.cs" Inherits="CineProducto.SolicitudAclaraciones" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="en">
<head id="Head1" runat="server">
<!-- Título personalizado para cada página -->
<title>Trámite Reconocimiento Como Obra Nacional - Página Principal - Mincultura</title>
<!-- Framework CSS -->
<link rel="stylesheet" href="~/Styles/screen.css" type="text/css" media="screen, projection"/>
<link rel="stylesheet" href="~/Styles/print.css" type="text/css" media="print"/>
<!-- Otros encabezados -->
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
</head>
<body>
    <div style="width:100%;">
    <div style="width:800px;background-color:whitesmoke;margin:auto;min-height:600px;padding:20px 20px 20px 20px;text-align: justify">
    <asp:Panel runat="server" ID="pnlHistorico">
         <%= cartaGeneradaHistorico %>
    </asp:Panel>
        </div>
        </div>
</body>
</html>