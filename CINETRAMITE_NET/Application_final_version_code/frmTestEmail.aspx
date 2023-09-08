<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmTestEmail.aspx.cs" Inherits="CineProducto.frmTestEmail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            PRUEBA DE ENVIO DE EMAIL<br />
            <br />
            Para:<br />
            <asp:TextBox ID="txtpara" runat="server" Width="483px" Text="luisgabrielquiceno@gmail.com"></asp:TextBox>
            <br />
            <br />
            Asunto<br />
            <asp:TextBox ID="txtasunto" runat="server" Height="25px" Width="457px" Text="Prueba de envio de correos"></asp:TextBox>
            <br />
            <br />
            <br />
            Msg<br />
            <asp:TextBox ID="txtMsg" runat="server" Height="94px" Width="510px" Text="Este es una prueba de envio de correos de cineproducto. Este es una prueba de envio de correos de cineproducto. Este es una prueba de envio de correos de cineproducto. "></asp:TextBox>
            <br />
            <br />
            <br />
            No Correos Evniados:<br />
            <asp:TextBox ID="txtNumeroEnviados" runat="server" Width="483px" Text="0"></asp:TextBox>
            <br /><br />
            Clave:<br />
            <asp:TextBox ID="txtClave" runat="server" Width="483px" Text="0"></asp:TextBox>
            <br /><br />
&nbsp;<asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Enviar" />
        </div>
    </form>
</body>
</html>
