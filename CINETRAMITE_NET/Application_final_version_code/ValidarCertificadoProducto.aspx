<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ValidarCertificadoProducto.aspx.cs" Inherits="CineProducto.ValidarCertificadoProducto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container">
  <div class="row">       

    <div style="text-align:center">
        <h3>Validación de Certificados de Productos Cinematograficos </h3>
    </div>    
    <blockquote class="jus">
    <p class="justify-content-lg-start">
         A continuación puede realizar el proceso de validación de certificados cionematograficos, por favor ingrese la informacion solicitada.
         <br /> 
         Señor ciudadano, recuerde que esta opción NO es para la generación de certificados de cinematrograficos. 
         En esta sección puede unicamente validar los certificados ya generados por la plataforma web, con el PIN que aparece en la parte inferior del mismo 
    </p>
 </blockquote>
    <asp:Panel runat="server" ID="panelConsultar">
        
    <div style="text-align:center">

        <table style="margin: 0 auto;text-align: left; width:35%" border="0" >
            <tr>
                <th style="width:220px"><asp:Label ID="Label1" runat="server" Text="Código de Validación:"></asp:Label> </th>
                <td><asp:TextBox ID="txtCodigoValidacion" runat="server" Width="300"></asp:TextBox> </td>
            </tr>
            <tr>
                <th></th>
                <td> 
                    <asp:Image ID="Image2" runat="server" Height="55px" ImageUrl="~/Captcha.aspx" Width="186px" />                     
                </td>
            </tr>
            <tr>
                <th>Escriba el texto del recuadro:</th>
                <td><asp:TextBox runat="server" ID="txtVerificationCode"></asp:TextBox>   
                     <br />  
                    <asp:Label runat="server" ID="lblCaptchaMessage"></asp:Label>                    
                </td>
            </tr>
        </table> 
                
        <br /> 
        <br /> 
        <asp:Button ID="btnValidar" runat="server" Text="Validar" OnClick="btnValidar_Click" />
      </div>
      </asp:Panel>

        <br />
        <br />
        <asp:Panel runat="server" ID="panelResultado" Visible="false">
          <div style="text-align:center">
              <h4><asp:Label ID="lblResultado" runat="server" Text=""></asp:Label></h4>
              <table style="margin: 0 auto;text-align: left; width:50%" border="1" >
                  <tr>
                      <th style="width:150px">Titulo:</th>
                      <td><asp:Label runat="server" ID="lblTitulo" Text=""></asp:Label> </td>
                  </tr>
                  <tr>
                     <th>Tipo de Producción:</th>
                      <td><asp:Label runat="server" ID="lblTipoProduccion" Text=""></asp:Label> </td>
                  </tr>
                  <tr>
                     <th>Productor(es):</th>
                      <td><asp:Label runat="server" ID="lblProductor" Text=""></asp:Label> </td>
                  </tr>
                   <tr>
                     <th>Coproductor(es):</th>
                      <td><asp:Label runat="server" ID="lblCoproductor" Text=""></asp:Label> </td>
                  </tr>
                  <tr>
                     <th>Costo:</th>
                      <td><asp:Label runat="server" ID="lblCosto" Text=""></asp:Label> </td>
                  </tr>
                  <tr>
                     <th>Tipo de obra:</th>
                      <td><asp:Label runat="server" ID="lblTipoObra" Text=""></asp:Label> </td>
                  </tr>
                  <tr>
                     <th>Duracion:</th>
                      <td><asp:Label runat="server" ID="lblDuracion" Text=""></asp:Label> </td>
                  </tr>
                  <tr>
                     <th>Fecha Inicio Rodaje:</th>
                      <td><asp:Label runat="server" ID="lblFechaInicioRodaje" Text=""></asp:Label> </td>
                  </tr>
                  <tr>
                     <th>Fecha Fin Rodaje:</th>
                      <td><asp:Label runat="server" ID="lblFechaFinRodaje" Text=""></asp:Label> </td>
                  </tr>
                  <tr>
                     <th>Sinopsis:</th>
                      <td><asp:Label runat="server" ID="lblSinopsis" Text=""></asp:Label> </td>
                  </tr>
                  
                  <tr>
                     <th>Genero:</th>
                      <td><asp:Label runat="server" ID="lblGenero" Text=""></asp:Label> </td>
                  </tr>
              </table>
              <br />
              <a href="ValidarCertificadoProducto.aspx">Realizar otra validación</a>
             
          </div>
        </asp:Panel>
       
    
</div></div>

</asp:Content>
