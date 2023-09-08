<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmReportePivote.aspx.cs" Inherits="CineProducto.frmReportePivote" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">

       
        <!-- PivotTable.js libs from ../dist -->
        <link rel="stylesheet" type="text/css" href="/scripts/dist/pivot.css">
        <script type="text/javascript" src="/scripts/dist/pivot.js"></script>
        <script type="text/javascript" src="/scripts/dist/d3_renderers.js"></script>
        <script type="text/javascript" src="/scripts/dist/c3_renderers.js"></script>
        <script type="text/javascript" src="/scripts/dist/export_renderers.js"></script>

        <style>
            body {font-family: Verdana;}
            .node {
              border: solid 1px white;
              font: 10px sans-serif;
              line-height: 12px;
              overflow: hidden;
              position: absolute;
              text-indent: 2px;
            }
            .c3-line, .c3-focused {stroke-width: 3px !important;}
            .c3-bar {stroke: white !important; stroke-width: 1;}
            .c3 text { font-size: 12px; color: grey;}
            .tick line {stroke: white;}
            .c3-axis path {stroke: grey;}
            .c3-circle { opacity: 1 !important; }
            .c3-xgrid-focus {visibility: hidden !important;}
        </style>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
            
          <script type="text/javascript">

              var tpl = $.pivotUtilities.aggregatorTemplates;
              $(function () {
                  $("#output").pivotUI(
                      [
                         <%= datos %>
                     
                      ],
                      
                      {
                          rows: [ <%= filas %>],
                          cols: [ <%= columnas %>],
                          aggregators: {
                              "Cantidad sin repetición":
                                  function () { return tpl.sum()(["cnt_sin_repeticion"]) }
                              ,
                              "Cantidad":
                                  function () { return tpl.count()(["project_id"]) }
                              ,
                              
                              "Costo Preproducción":
                                   function () { return tpl.sum()(["Costo_Total_Preproduccion"]) }
                                      ,
                              "Costo Producción":
                                   function () { return tpl.sum()(["Costo_Produccion"]) }
                              ,
                              "Costo Postproducción":
                                   function () { return tpl.sum()(["Costo_Posproduccion"]) }
                              ,
                              "Costo Total":
                                   function () { return tpl.sum()(["Costo_total"]) }
                             
                          },
                          hiddenAttributes: ["project_id","cnt_sin_repeticion"]
                         
                      }
                  );
              });
        </script>    

       <asp:ScriptManager runat="server" ID="scManager"
           EnableScriptGlobalization="true"
EnableScriptLocalization="true" ></asp:ScriptManager>
 

        <div class="row" style="width:98%">
        <div class="col-12">
    <asp:Label runat="server" ID="lbltitulo" Font-Size="15px" Font-Bold="true" Text="Reporte Maestro de Solicitudes"></asp:Label>
       </div>
    </div>
      

    <div class="row">
        <div class="col-12">
<asp:Panel ID="pnlFiltros" style="border-color:gray; border-radius: 25px;border-style:solid;padding:15px 15px 15px 15px;" runat="server" CssClass="ui-tabs-panel">

   <table>
       <tr>
           <td>Estado</td>
           <td class="auto-style1"><asp:DropDownList ID="cmbEstado" runat="server">
               <asp:ListItem Text="Todos" Value="-1"></asp:ListItem>
        </asp:DropDownList>
               <asp:Label runat="server" ID="lblEstados" Visible="false"></asp:Label>
           </td>
           <td class="auto-style2">&nbsp;</td>
           <td>Tipo de Producción</td>
           <td>
               <asp:DropDownList ID="cmbTipoProduccion" runat="server" AppendDataBoundItems="True" DataSourceID="SqlDataSourceTipoProduccion" DataTextField="production_type_name" DataValueField="production_type_id">
                   <asp:ListItem Value="-1">todos</asp:ListItem>
               </asp:DropDownList>
               <asp:SqlDataSource ID="SqlDataSourceTipoProduccion" runat="server" ConnectionString="<%$ ConnectionStrings:cineConnectionString %>" SelectCommand="SELECT [production_type_id], [production_type_name] FROM [production_type] ORDER BY [production_type_name]"></asp:SqlDataSource>
           </td>
       </tr>
       <tr>
           <td style="width:160px;">Título Obra</td>
           <td class="auto-style1"><asp:TextBox runat="server" ID="txtTitulo" Width="300px"></asp:TextBox></td>
           <td class="auto-style2">&nbsp;</td>
           <td>Tipo de obra</td>
           <td>
               <asp:DropDownList ID="cmbTipoObra" runat="server" AppendDataBoundItems="True" DataSourceID="SqlDataSourceGenero" DataTextField="project_genre_name" DataValueField="project_genre_id">
                   <asp:ListItem Value="-1">todos</asp:ListItem>
               </asp:DropDownList>
               <asp:SqlDataSource ID="SqlDataSourceGenero" runat="server" ConnectionString="<%$ ConnectionStrings:cineConnectionString %>" SelectCommand="SELECT [project_genre_id], [project_genre_name] FROM [project_genre] ORDER BY [project_genre_name]"></asp:SqlDataSource>
           </td>
       </tr>
       <tr>
           <td style="width:160px;">Valor Total entre</td>
           <td class="auto-style1">&nbsp;<asp:TextBox ID="txtDesde" runat="server" Width="130px"></asp:TextBox>
               &nbsp;y
               <asp:TextBox ID="txtHasta" runat="server" Width="130px"></asp:TextBox>
           </td>
           <td class="auto-style2">&nbsp;</td>
           <td>Duración</td>
           <td>
               <asp:DropDownList ID="cmbDuracion" runat="server" AppendDataBoundItems="True" DataSourceID="SqlDataSourceDuracion" DataTextField="project_type_name" DataValueField="project_type_id">
                   <asp:ListItem Value="-1">todos</asp:ListItem>
               </asp:DropDownList>
               <asp:SqlDataSource ID="SqlDataSourceDuracion" runat="server" ConnectionString="<%$ ConnectionStrings:cineConnectionString %>" SelectCommand="SELECT [project_type_id], [project_type_name] FROM [project_type] ORDER BY [project_type_name]"></asp:SqlDataSource>
           </td>
       </tr>
       <tr>
           <td style="width:160px;">Productor Nombre</td>
           <td class="auto-style1">
               <asp:TextBox ID="txtProductor" runat="server"></asp:TextBox>
           </td>
           <td class="auto-style2">&nbsp;</td>
           <td>Tipo Productor</td>
           <td>
               <asp:DropDownList ID="cmbTipoProductor" runat="server" AppendDataBoundItems="True" DataSourceID="SqlDataSourceTipoProdcutr" DataTextField="person_type_name" DataValueField="person_type_id">
                   <asp:ListItem Value="-1">todos</asp:ListItem>
               </asp:DropDownList>
               <asp:SqlDataSource ID="SqlDataSourceTipoProdcutr" runat="server" ConnectionString="<%$ ConnectionStrings:cineConnectionString %>" SelectCommand="SELECT [person_type_id], [person_type_name] FROM [person_type]"></asp:SqlDataSource>
           </td>
       </tr>
       <tr>
       <td>Solicitudes Desde</td>
           <td class="auto-style1"><asp:TextBox ID="txtInicio" runat="server" Width="80px" OnTextChanged="txtInicio_TextChanged"></asp:TextBox>
         <cc1:calendarextender  ID="txtInicio_CalendarExtender" runat="server" BehaviorID="txtInicio_CalendarExtender" TargetControlID="txtInicio"></cc1:calendarextender>
         <asp:Button ID="btnLimpiarDesde" runat="server" OnClick="btnLimpiarDesde_Click" Text="&lt;" />
         
         </td>
           <td class="auto-style2">&nbsp;</td>
           <td>Solicitudes Hasta</td>
           <td>
               <asp:TextBox ID="txtFin" runat="server" OnTextChanged="txtFin_TextChanged" Width="80px"></asp:TextBox>
               <cc1:CalendarExtender ID="txtFin_CalendarExtender" runat="server" BehaviorID="txtFin_CalendarExtender" TargetControlID="txtFin" />
               <asp:Button ID="btnLimpiarHasta" runat="server" OnClick="btnLimpiarHasta_Click" Text="&lt;" />
           </td>
       </tr>
       <tr>
           <td>&nbsp;</td>
           <td class="auto-style1">      &nbsp;</td>
           <td class="auto-style2">&nbsp;</td>
           <td>&nbsp;</td>
           <td>
               <asp:Button ID="btnFiltrar" runat="server" CssClass="boton" Font-Size="12pt" OnClick="btnFiltrar_Click" Text="Filtrar" />
              
           </td>
       </tr>
       <tr>
           <td colspan="2">
               <asp:Label ID="lblError" runat="server" Text="" ForeColor="Red"></asp:Label>
           </td>
           <td class="auto-style2">&nbsp;</td>
           <td>&nbsp;</td>
           <td>&nbsp;</td>
       </tr>
   </table>
 
</asp:Panel>
   </div>
              </div>
    
          <div class="row">
        <div class="col-12">

                    <asp:Panel runat="server" ID="pnlGrilla" Visible="false">
           <asp:Button ID="btnCAmbiarFiltros" runat="server" CssClass="boton" Font-Size="12pt" OnClick="btnCAmbiarFiltros_Click" Text="Cambiar Filtros" />
 &nbsp;
<asp:Button ID="btnExportarExcel" runat="server" CssClass="boton" Font-Size="12pt" OnClick="btnExportarExcel_Click" Text="Exportar Excel" />
<asp:Label runat="server" Text="" ID="lblregistros"></asp:Label>

<div id="output" style="margin: 10px;"></div>

</asp:Panel>
      

               </div>
              </div>

  
    


</asp:Content>
