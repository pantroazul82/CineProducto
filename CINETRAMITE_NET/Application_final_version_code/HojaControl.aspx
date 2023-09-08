<%@ Page Title="Previsualización de la Solicitud de Aclaraciones" Language="C#" AutoEventWireup="True"
    CodeBehind="HojaControl.aspx.cs" Inherits="CineProducto.HojaControl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="en">
<head id="Head1" runat="server">

<!-- Título personalizado para cada página -->
<title>Trámite Reconocimiento Como Obra Nacional - Página Principal - Mincultura</title>

<!-- Framework CSS -->
<link rel="stylesheet" href="~/Styles/screen.css" type="text/css" media="screen, projection"/>
<link rel="stylesheet" href="~/Styles/print.css" type="text/css" media="print"/>
<link rel="stylesheet" href="~/Styles/controlsheet.css" type="text/css"/>

<!-- Otros encabezados -->
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
    <style>
        .tableCG {
font-size:1.1em;padding:20px 0 20px 0; border: 1px solid gray;

        }

        .tableCG td {
            border: 1px solid gray;

        }

        .tableCG th {
             border: 1px solid gray;

        }

    </style>
</head>
<body>
    <div id="hoja_control">
        <div style="text-align:center;width:100%;"><a href="javascript:window.print()">Imprimir</a></div>
        <div class="titulo">Hoja de Control</div>
        <table class="tableCG">
            <tr>
                <td style="width:160px;font-weight:bold;">T&iacute;tulo del proyecto:</td>
                <td colspan="4"><asp:Literal ID="project_name" runat="server"></asp:Literal></td>                          
            </tr>

             <tr>
                <td style="width:160px;font-weight:bold;">T&iacute;tulo anterior:</td>
                <td colspan="4"><asp:Literal ID="titulo_anterior" runat="server"></asp:Literal></td>                          
            </tr>
            <tr>
                <td style="font-weight:bold;">Nombre del solicitante:</td>
                <td colspan="3"><asp:Literal ID="producer_name" runat="server"></asp:Literal></td>
            </tr>
            <tr><td colspan="4"> <hr /></td></tr>
            <tr>
                <td style="font-weight:bold;">Tipo de proyecto:</td>
                <td style="width:160px;"><asp:Literal ID="project_type" runat="server"></asp:Literal></td>
                <td style="width:200px;font-weight:bold;">Fecha Solicitud</td>
                <td style="width:140px;"><asp:Literal ID="request_date" runat="server"></asp:Literal></td>
            </tr>
            <tr>
                <td style="font-weight:bold;"> </td>
                <td><asp:Literal ID="project_genre" runat="server"></asp:Literal></td>
                
                <td style="font-weight:bold;">Fecha Solicitud Aclaraciones</td>
                <td><asp:Literal ID="clarification_request_date" runat="server"></asp:Literal></td>
            </tr>
            <tr>
                <td style="font-weight:bold;"><strong>Tipo de producci&oacute;n:</strong></td>
                <td><asp:Literal ID="production_type" runat="server"></asp:Literal></td>
                
                <td style="font-weight:bold;">Fecha Recibo Aclaraciones</td>
                <td><asp:Literal ID="clarification_date" runat="server"></asp:Literal></td>
            </tr>
            <tr>
                <td style="font-weight:bold;">Duraci&oacute;n:</td>
                <td><asp:Literal ID="project_duration" runat="server"></asp:Literal></td>
                
                <td style="font-weight:bold;">Fecha Resoluci&oacute;n</td>
                <td><asp:Literal ID="project_resolution_date" runat="server"></asp:Literal></td>
            </tr>
            <tr>
                <td style="font-weight:bold;">Costo del proyecto sin promoción:</td>
                <td><asp:Literal ID="project_total_cost" runat="server"></asp:Literal></td>
                
                <td style="font-weight:bold;"></td>
                <td></td>
            </tr>

              <tr>
                <td style="font-weight:bold;">Costo del proyecto con promoción:</td>
                <td><asp:Literal ID="project_total_cost_total" runat="server"></asp:Literal></td>
                
                <td style="font-weight:bold;"></td>
                <td></td>
            </tr>

            <tr>
                <td style="font-weight:bold;">Formato de rodaje:</td>
                <td><asp:Literal ID="project_shooting_formats" runat="server"></asp:Literal></td>
                
                <td style="font-weight:bold;"></td>
                <td></td>
            </tr>

             <tr>
                <td style="font-weight:bold;">Texto adicional carta de aclaraciones:</td>
                <td colspan="3"><asp:Literal ID="Texto_adicional_carta_de_aclaraciones" runat="server"></asp:Literal></td>
                
            </tr>

             <tr>
                <td style="font-weight:bold;">Texto adicional carta de negación:</td>
                <td colspan="3"><asp:Literal ID="Texto_adicional_carta_de_negacion" runat="server"></asp:Literal></td>
            </tr>

             <tr>
                <td style="font-weight:bold;">Texto sustituto carta de aclaraciones:</td>
                <td colspan="3"><asp:Literal ID="Texto_sustituto_carta_de_aclaraciones" runat="server"></asp:Literal></td>
                
            </tr>
        </table>
        <div style="page-break-before:always;"></div>
        <asp:Repeater id="SectionRepeater" runat="server">
            <HeaderTemplate><ul></HeaderTemplate>
            <ItemTemplate>
                <li>
                    
        <div style="page-break-inside:avoid;">
                    <fieldset>
                    <legend><%# Eval("section_name") %></legend>
                    <div>
                        <h4>Solicitud de Aclaraciones</h4>
                        <p><%# Eval("clarification_request")%></p>
                    </div>
                     <div>
                        <h4>Respuesta a las aclaraciones</h4>
                        <p><%# Eval("aclaraciones_productor")%></p>
                    </div>
                    <div>
                        <h4><%# ((Eval("initial_observation") != null && Eval("initial_observation") != System.DBNull.Value && Eval("initial_observation").ToString().Trim() != string.Empty)?"Observaciones":"") %></h4>
                        <p><%# Eval("initial_observation")%></p>
                    </div>
                   
                    </fieldset>
        </div>
                </li>
            </ItemTemplate>
            <FooterTemplate></ul></FooterTemplate>
        </asp:Repeater>
        <div style="text-align:center;width:100%;"><a href="javascript:window.print()">Imprimir</a></div>
    </div>
</body>
</html>