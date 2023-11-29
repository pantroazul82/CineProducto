<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmReporteMaestroDoc.aspx.cs" Inherits="CineProducto.frmReporteMaestroDoc" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>


<%@ Register assembly="DevExpress.Web.v17.2, Version=17.2.13.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>


<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .auto-style1 {
            width: 165px;
        }
        .auto-style2 {
            width: 26px;
        }

        .panelBusqueda {

        }
    </style>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
       <asp:ScriptManager runat="server" ID="scManager"
           EnableScriptGlobalization="true"
EnableScriptLocalization="true" ></asp:ScriptManager>
 
    <script language="javascript" type="text/javascript">
<!-- 
    var prm = Sys.WebForms.PageRequestManager.getInstance();

    // -->
    </script>

    <asp:Label runat="server" ID="lblSort" Visible="false" Text="project_id"></asp:Label>
    <asp:Label runat="server" ID="lblSortDirection" Visible="false" Text="desc"></asp:Label>
    <p>

     <%--   <a href="~/Default.aspx" runat="server" type="text/asp">Volver a la página principal del trámite</a>
    

        <a style="margin-left:20px;" href="OpcionesAdministracion.aspx">Opciones de administración del sistema</a>--%>



        </p>
    
   
    
    <div class="row" style="width:98%">
        <div class="col-12">

    <asp:Label runat="server" ID="lbltitulo" Font-Size="15px" Font-Bold="true" Text="Reporte Maestro de Documento de Solicitudes"></asp:Label>
      
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

        <dx:ASPxGridView  ID="grdDatosdev"  AutoGenerateColumns="False" runat="server" Theme="DevEx">
                  <SettingsPager PageSize="50">
                  </SettingsPager>
            
            <GroupSummary></GroupSummary>
                  <Settings ShowFilterRow="True" ShowGroupPanel="True"  />
         <SettingsDataSecurity AllowDelete="False" AllowEdit="False" AllowInsert="False" />
                        <SettingsText GroupPanel="Agrege una columna en este lugar para agrupar valores"  EmptyDataRow="No hay registros que cumplan con las condiciones de la búsqueda o que requieran su atención." />
        <Columns>
                <dx:GridViewDataTextColumn Settings-AutoFilterCondition="Contains" FieldName="project_id" Caption="Id"  />
                
                
                <dx:GridViewDataTextColumn Settings-AutoFilterCondition="Contains" FieldName="Productor" Caption="Productor"  />
                
                <dx:GridViewDataColumn Settings-AutoFilterCondition="Contains" FieldName="Titulo"  Caption="Título obra"  >
                    <DataItemTemplate>
                        <asp:HyperLink runat="server" ID="lnkProyecto" Text='<%# Eval("Titulo") %>' 
                            NavigateUrl='<%# "DatosProyecto.aspx?project_id="+Eval("project_id") %>'></asp:HyperLink>
                    </DataItemTemplate>    
                </dx:GridViewDataColumn>
                <dx:GridViewDataTextColumn Settings-AutoFilterCondition="Contains" Settings-AllowGroup="True" FieldName="Tipo_Produccion" Caption="Tipo de Producción"  />
                <dx:GridViewDataTextColumn Settings-AutoFilterCondition="Contains" Settings-AllowGroup="True" FieldName="Tipo_Obra" Caption="Tipo de obra"  />
                <dx:GridViewDataTextColumn Settings-AutoFilterCondition="Contains" Settings-AllowGroup="True" FieldName="Clasificacion_Duracion" Caption="Clasificación Duracion"  />
                <dx:GridViewDataTextColumn Settings-AutoFilterCondition="Contains" FieldName="attachment_description" Caption="Descripcion de anexo"  />
            
             <dx:GridViewDataColumn Settings-AutoFilterCondition="Contains" FieldName="nombre_original"  Caption="Adjuntos"  >
                    <DataItemTemplate>
                        <asp:HyperLink runat="server" Target="_blank" ID="lnkProyecto" Text='<%# Eval("nombre_original") %>' 
                            NavigateUrl='<%# Eval("project_attachment_path") %>'></asp:HyperLink>
                    </DataItemTemplate>    
                </dx:GridViewDataColumn>



                <dx:GridViewDataDateColumn Settings-AutoFilterCondition="Contains" FieldName="Fecha_Solicitud" Caption="Fecha Notificacion" >
                    <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy" Height="30" />
                </dx:GridViewDataDateColumn>
                <dx:GridViewDataDateColumn Settings-AutoFilterCondition="Contains" FieldName="fecha_tramite_fin" Caption="Fecha Final" >
                    <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy" Height="30" />
                </dx:GridViewDataDateColumn>
                <dx:GridViewDataDateColumn Settings-AutoFilterCondition="Contains" FieldName="Fecha_Resolucion" Caption="Fecha Resolución" >              
                    <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy" Height="30" />
                </dx:GridViewDataDateColumn>
            </Columns>
        </dx:ASPxGridView>
     
                        
                        
                
        </asp:Panel>
                 </div>
              </div>

     


</asp:Content>