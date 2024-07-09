<%@ Page Title="Listado de proyectos" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="Lista.aspx.cs" Inherits="CineProducto.Lista" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<%@ Register src="usercontrols/cargando.ascx" tagname="cargando" tagprefix="uc1" %>

<%@ Register assembly="DevExpress.Web.v17.2, Version=17.2.13.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>
<asp:Content ID="TitleContent" runat="server" ContentPlaceHolderID="TitleContent">Trámite Reconocimiento Como Obra Nacional - Listado de Solicitudes - Mincultura</asp:Content>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <script src="blueimp/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="blueimp/jquery.iframe-transport.js" type="text/javascript"></script>
    <script src="blueimp/jquery.fileupload.js" type="text/javascript"></script>
    <script src="Scripts/js/footable.min.js"></script>
    <link href="Styles/css/footable.standalone.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    
     <script type = "text/javaScript">
         function cargarArchivo(id_projecto)
         {
             $('#hdFrmCodigo').val(id_projecto);
            // alert($('#<%=hdFrmCodigo.ClientID %>').val());
             FileUpload_formulario_solicitud.click();
         }

         $(document).ready(function () {
             $('#loading').hide();
             //---
             

             $(function () {

                 $('#FileUpload_formulario_solicitud').fileupload({
                     url: 'FileUploadHandler.ashx?upload=start&folder=<%= Page.ResolveClientUrl("~/uploads")%>' + '/'+$('#<%=hdFrmCodigo.ClientID %>').val() +'/' + '&attachment_id=0',
                     add: function (e, data) {
                         var cnt = 0;
                         var ext = '';
                         $.each(data.files, function (index, file) {
                             cnt = cnt + 1;
                             ext = file.name.substring(file.name.lastIndexOf('.'));
                             if (file.size > 5242880) {
                                 alert('El archivo ' + file.name + '  supera el tamaño máximo de 5 Megas.');
                                 return;
                             }
                         });

                         if (cnt > 1) {
                             alert('Solo debe seleccionar un archivo');
                             return;
                         }

                         if (ext.toUpperCase() != '.PDF') {
                             alert('solo es valido subir archivos en formato pdf.');
                             return;
                         }
                         //   console.log('add', data);
                         $('#progressbar' + $('#<%=hdFrmCodigo.ClientID %>').val()).show();
                         //    $('#progressbar1').show();
                         data.submit();
                     },
                     progress: function (e, data) {
                         var progress = parseInt(data.loaded / data.total * 100, 10);
                         $('#progressbar' + $('#<%=hdFrmCodigo.ClientID %>').val()).css('width', progress + '%');
                     },
                     success: function (response, status) {
                         $('#progressbar' + $('#<%=hdFrmCodigo.ClientID %>').val()).hide();
                         $('#progressbar'+$('#<%=hdFrmCodigo.ClientID %>').val()+' div').css('width', '0%');

                         if (response.indexOf("Error") >= 0) {
                             alert(response);
                         } else {

                             $.ajax({
                                 type: 'POST',
                                 url: '<%=Page.ResolveClientUrl("~/Default.aspx/proccessRequestForm") %>',
                                 data: '{project_id:'+$('#<%=hdFrmCodigo.ClientID %>').val()+',filename:"' + response + '"}',
                                 contentType: 'application/json; charset=utf-8',
                                 dataType: 'json',
                                 success: function (msg) {
                                     alert('Formulario cargado en la obra con cod:' + $('#<%=hdFrmCodigo.ClientID %>').val());
                                    // alert('vamos bien 2'+msg.d);                                     
                                 }
                             });
                             // $('#aspnetForm').submit();
                         }


                     },

                     error: function (error) {
                         alert('error');
                         $('#progressbar' + $('#<%=hdFrmCodigo.ClientID %>').val()).hide();
                         $('#progressbar' + $('#<%=hdFrmCodigo.ClientID %>').val() + ' div').css('width', '0%');
                         console.log('error', error);
                     }
                 }).bind('fileuploadadd', function (e, data) {
                     data.url = 'FileUploadHandler.ashx?upload=start&folder=<%= Page.ResolveClientUrl("~/uploads")%>' + '/' + $('#<%=hdFrmCodigo.ClientID %>').val() + '/' + '&attachment_id=0';
                 });
             });
         
         });
         </script>

       <asp:ScriptManager runat="server" ID="scManager" EnableScriptGlobalization="true" EnableScriptLocalization="true" ></asp:ScriptManager>
 

    <asp:Label runat="server" ID="lblSort" Visible="false" Text="project_request_date"></asp:Label>
    <asp:Label runat="server" ID="lblSortDirection" Visible="false" Text="desc"></asp:Label>
    

    <div class="row">
        
    <div class="col-12">
    <asp:Label runat="server" ID="lbltitulo" Font-Size="15px" Font-Bold="true" Text="Listado de Solicitudes"></asp:Label>
    </div>
          <div class="col-12">
    <asp:Panel runat="server" DefaultButton="btnFiltrar">
        <table>
           <tr>
           <td>Título Obra<asp:CheckBox runat="server" ID="chkOprimioBoton" Checked="false" Visible="false" /></td>
           <td><asp:TextBox ID="txtTitulo" placeholder="Ingrese el título de la obra" runat="server" Width="250px"></asp:TextBox></td>
           <td>&nbsp;</td>
           <td>
               <asp:CheckBox ID="chkCreadasNoEnviadas" runat="server" Checked="true" Text="Incluir Creadas no enviadas" />
           </td>
       </tr>
           <tr>
           <td>Estado actual</td>
           <td><asp:DropDownList ID="cmbEstado" runat="server" Width="250px">
               <asp:ListItem Text="Todos" Value="-1"></asp:ListItem>
        </asp:DropDownList>
               <asp:Label runat="server" ID="lblEstados" Visible="false"></asp:Label>
           </td>
           <td>Productor</td>
           <td>
               <asp:TextBox ID="txtProductor" runat="server" placeholder="Ingrese el nombre del productor" Width="250px"></asp:TextBox>
           </td>
       </tr>
           <tr>
           <td>Solicitudes Enviadas Desde</td>
           <td><asp:TextBox ID="txtInicio" runat="server" Width="220px" OnTextChanged="txtInicio_TextChanged"></asp:TextBox>
         <cc1:calendarextender  ID="txtInicio_CalendarExtender" runat="server" BehaviorID="txtInicio_CalendarExtender" TargetControlID="txtInicio"
             >
             
 </cc1:calendarextender>
               <asp:Button ID="btnLimpiarDesde" runat="server" OnClick="btnLimpiarDesde_Click" Text="&lt;" />
           </td>
           <td>Solicitudes Enviadas Hasta</td>
           <td>
               <asp:TextBox ID="txtFin" runat="server" OnTextChanged="txtFin_TextChanged" Width="220px"></asp:TextBox>
               <cc1:CalendarExtender ID="txtFin_CalendarExtender" runat="server" BehaviorID="txtFin_CalendarExtender" TargetControlID="txtFin" />
               <asp:Button ID="btnLimpiarHasta" runat="server" OnClick="btnLimpiarHasta_Click" Text="&lt;" />
           </td>
       </tr>


       <tr>
           <td colspan="4">
               
               <asp:Label ForeColor="Blue" runat="server" ID="Label1" ></asp:Label>
               <cc1:CollapsiblePanelExtender ID="cpe" runat="Server"
    TargetControlID="estadosAdicional" CollapsedSize="0" ExpandedSize="250"  
         Collapsed="True"
    ExpandControlID="Label1" CollapseControlID="Label1" AutoCollapse="False"
    AutoExpand="False"    ScrollContents="True"    TextLabelID="Label1"    CollapsedText="Mas filtros."
    ExpandedText="Menos Filtros"     ExpandDirection="Vertical" />
               <asp:Panel runat="server" ID="estadosAdicional" Style="background-color:whitesmoke;">
                   <table>
           <tr>
           <td>Título anterior</td>
           <td><asp:TextBox ID="txtTituloAnterior" placeholder="Ingrese el título de la obra" runat="server" Width="250px"></asp:TextBox></td>
           <td>&nbsp;</td>
           <td>
            
           </td>
       </tr>
       <tr>
           <td>Tipo de Producción&nbsp;</td>
           <td>
               <asp:DropDownList ID="cmbTipoProduccion" runat="server" AppendDataBoundItems="True" DataSourceID="SqlDataSourceTipoProduccion" DataTextField="production_type_name" DataValueField="production_type_id" Width="250px">
                   <asp:ListItem Text="Todos" Value="-1"></asp:ListItem>
               </asp:DropDownList>
               <asp:SqlDataSource ID="SqlDataSourceTipoProduccion" runat="server" ConnectionString="<%$ ConnectionStrings:cineConnectionString %>" SelectCommand="SELECT [production_type_id], [production_type_name] FROM dboPrd.[production_type] ORDER BY [production_type_name]"></asp:SqlDataSource>
           </td>
           <td>Tipo de obra&nbsp;</td>
           <td>
               <asp:DropDownList ID="cmbTipoObra" runat="server" AppendDataBoundItems="True" DataSourceID="SqlDataSourceTipoObra" DataTextField="project_genre_name" DataValueField="project_genre_id" Width="250px">
                   <asp:ListItem Text="Todos" Value="-1"></asp:ListItem>
               </asp:DropDownList>
               <asp:SqlDataSource ID="SqlDataSourceTipoObra" runat="server" ConnectionString="<%$ ConnectionStrings:cineConnectionString %>" SelectCommand="SELECT [project_genre_id], [project_genre_name] FROM dboPrd.[project_genre] ORDER BY [project_genre_name]"></asp:SqlDataSource>
           </td>
       </tr>
       <tr>
           <td>Duración de obra</td>
           <td>
               <asp:DropDownList ID="cmbDuracionObra" runat="server" AppendDataBoundItems="True" DataSourceID="SqlDataSourceDuracionObra" DataTextField="project_type_name" DataValueField="project_type_id" Width="250px">
                   <asp:ListItem Text="Todos" Value="-1"></asp:ListItem>
               </asp:DropDownList>
               <asp:SqlDataSource ID="SqlDataSourceDuracionObra" runat="server" ConnectionString="<%$ ConnectionStrings:cineConnectionString %>" SelectCommand="SELECT [project_type_id], [project_type_name] FROM dboPrd.[project_type] ORDER BY [project_type_name]"></asp:SqlDataSource>
           </td>
           <td>Productor Principal</td>
           <td>
               <asp:DropDownList ID="cmbProdcutorPrincipal" runat="server" AppendDataBoundItems="True" DataSourceID="SqlDataSourceProductorPrincipal" DataTextField="person_type_name" DataValueField="person_type_id" Width="250px">
                   <asp:ListItem Text="Todos" Value="-1"></asp:ListItem>
               </asp:DropDownList>
               <asp:SqlDataSource ID="SqlDataSourceProductorPrincipal" runat="server" ConnectionString="<%$ ConnectionStrings:cineConnectionString %>" SelectCommand="SELECT [person_type_id], [person_type_name] FROM dboPrd.[person_type] ORDER BY [person_type_name]"></asp:SqlDataSource>
           </td>
       </tr>
<tr>
    <td colspan="4"><hr style="border-color:black !important;border-width:1px !important;border-style:solid !important;background-color:black !important;" /><br /></td>
</tr>

                       <tr>
                           <td>
               <asp:CheckBox runat="server" ID="chkEstado" Text="Que haya tenido el estado" /></td>
                           <td>
                <asp:DropDownList ID="cmbEstado2" runat="server">
        </asp:DropDownList></td>
                           <td>
           Entre el dia
               <asp:TextBox ID="txtEstadoDesde" runat="server" OnTextChanged="txtEstadoDesde_TextChanged" Width="80px"></asp:TextBox>
               <cc1:CalendarExtender ID="CalendarExtenderDesde" runat="server" BehaviorID="txtEstadoDesde_CalendarExtender" TargetControlID="txtEstadoDesde" /></td>
                           <td>
           y el dia  <asp:TextBox ID="txtEstadoHasta" runat="server" OnTextChanged="txtEstadoHasta_TextChanged" Width="80px"></asp:TextBox>
               <cc1:CalendarExtender ID="CalendarExtenderHasta" runat="server" BehaviorID="txtEstadoHasta_CalendarExtender" TargetControlID="txtEstadoHasta" /></td>
                       </tr>
                       <tr>
                           <td>
               <asp:CheckBox runat="server" ID="chkResolucion" Text="Que haya tenido resolución entre el dia" /></td>
                           <td>
               <asp:TextBox ID="txtResolucionDesde" runat="server" OnTextChanged="txtResolucionDesde_TextChanged" Width="80px"></asp:TextBox>
               <cc1:CalendarExtender ID="txtResolucionDesdeCalendarExtender1" runat="server" BehaviorID="txtResolucionDesdeCalendarExtender" TargetControlID="txtResolucionDesde" /></td>
                           <td>
           y el dia  <asp:TextBox ID="txtResolucionHasta" runat="server" OnTextChanged="txtResolucionHasta_TextChanged" Width="80px"></asp:TextBox>
               <cc1:CalendarExtender ID="txtResolucionHastaCalendarExtender2" runat="server" BehaviorID="txtResolucionHasta_CalendarExtender" TargetControlID="txtResolucionHasta" /></td>
                           <td></td>
                       </tr>
                   </table>
                       
</asp:Panel>
           </td>
       </tr>

       <tr>
           <td colspan="2">
               <asp:Label ID="lblError" runat="server" Text="" ForeColor="Red"></asp:Label>
           </td>
           <td>&nbsp;</td>
           <td>&nbsp;
               <asp:Button ID="btnFiltrar" runat="server" CssClass="boton" Font-Size="12pt" OnClick="btnFiltrar_Click" OnClientClick="$('#loading').show();" Text="Filtrar" /></td>
       </tr>
   </table>
    </asp:Panel> 
    <div>
            <div id='divFileUpload_formulario_solicitud'  >
              <input type='file' name='file' id='FileUpload_formulario_solicitud' style='display:none' />  

             </div>
                         
             <asp:HiddenField runat="server" Value="777" ClientIDMode="Static" ID="hdFrmCodigo" />                  
     </div>



          </div>
        
         <div class="col-12">
     <asp:Button ID="btnExportarExcel" runat="server" CssClass="boton" Font-Size="12pt" OnClick="btnExportarExcel_Click"  Text="Exportar Excel" />
 <% if (user_role < 6) { %>
     <asp:Button ID="btnDesistidos" runat="server" CssClass="boton" Font-Size="12pt" Text="Marcar desistidos" OnClick="btnDesistidos_Click" />
             <!--
     <asp:Button ID="btnDesistidosAviso" runat="server" CssClass="boton" Font-Size="12pt" Text="Aviso desistidos" OnClick="btnDesistidosAviso_Click"/>
             -->
<% } %>
     <asp:Label ID="lblDesistidos" runat="server" Text=""></asp:Label>
     <dx:ASPxGridView ID="grdDevDatos" runat="server" AutoGenerateColumns="False" EnableTheming="True" KeyFieldName="project_id" Theme="DevEx" OnRowCommand="grdDevDatos_RowCommand"
     
         >
         
<SettingsPager ShowEmptyDataRows="True" PageSize="50"></SettingsPager>

         <Settings ShowFilterRow="True" />
         <SettingsDataSecurity AllowDelete="False" AllowEdit="False" AllowInsert="False" />
         <SettingsText  EmptyDataRow="No hay registros que cumplan con las condiciones de la búsqueda o que requieran su atención." />
         
         <Columns>
              <dx:GridViewDataColumn Settings-AutoFilterCondition="Contains" Caption="Cod" FieldName="project_id" VisibleIndex="1">
                 <DataItemTemplate>
                    <%# Eval("project_id") %>
                     <asp:PlaceHolder runat="server"  visible='<%# ((int)Session["user_role_id"] >=1) %>'>
                    <input type='button' style='width:110px;height:20px;background-color:darkblue;color:white;' id='btnFileUploadText' 
                        value='cargar formulario' onclick='<%# "cargarArchivo("+Eval("project_id")+")" %>' );' />

                    <div class='progressbar' id='<%# "progressbar"+Eval("project_id") %>'  style='width:100px;display:none;'>
                        <div></div>
                    </div>                        
                    </asp:PlaceHolder>
                 </DataItemTemplate>
                  </dx:GridViewDataColumn>

             
             <dx:GridViewDataTextColumn Settings-AutoFilterCondition="Contains" FieldName="login" VisibleIndex="2" Caption="Login">
             </dx:GridViewDataTextColumn>
             <dx:GridViewDataTextColumn Settings-AutoFilterCondition="Contains" FieldName="Productor" ReadOnly="True" VisibleIndex="3" Caption="Productor">
             </dx:GridViewDataTextColumn>
             <dx:GridViewDataColumn Settings-AutoFilterCondition="Contains" Caption="Título obra" FieldName="Obra" VisibleIndex="4">
                 <DataItemTemplate>
                      <asp:HyperLink runat="server" ID="lnkProyecto" Text='<%# Eval("Obra") %>' 
                        NavigateUrl='<%# "DatosProyecto.aspx?project_id="+Eval("project_id") %>'></asp:HyperLink>
                 </DataItemTemplate>
             </dx:GridViewDataColumn>
              <dx:GridViewDataColumn Settings-AutoFilterCondition="Contains" Caption="Estado" FieldName="Estado" VisibleIndex="5">
                 <DataItemTemplate>
                              <asp:label runat="server" ID="lblEstado" Text='<%# verEstado(Eval("state_id"),Eval("Estado"),Eval("fecha_notificacion")) %>'></asp:label> 
                 </DataItemTemplate>
             </dx:GridViewDataColumn>
             <dx:GridViewDataDateColumn  Settings-AutoFilterCondition="Contains"   PropertiesDateEdit-DisplayFormatString="dd/MM/yyyy hh:mm:ss"   FieldName="Fecha_y_Hora_de_Solicitud" caption="Fecha solicitud" ReadOnly="True" VisibleIndex="6">
             </dx:GridViewDataDateColumn>
             <dx:GridViewDataTextColumn Settings-AutoFilterCondition="Contains" FieldName="hora_envio" Visible="false" caption="Hora solicitud" VisibleIndex="7">
             </dx:GridViewDataTextColumn>
             <dx:GridViewDataDateColumn  Settings-AutoFilterCondition="Contains"   PropertiesDateEdit-DisplayFormatString="dd/MM/yyyy hh:mm:ss"  FieldName="fecha_solicitud_aclaraciones" caption="Fecha Solicitud aclaraciones" ReadOnly="True" VisibleIndex="8">
             </dx:GridViewDataDateColumn>
             <dx:GridViewDataTextColumn Settings-AutoFilterCondition="Contains" Visible="false" FieldName="hora_solicitud_aclaraciones" caption="Hora Solicitud aclaraciones" VisibleIndex="9">
             </dx:GridViewDataTextColumn>
             <dx:GridViewDataDateColumn  Settings-AutoFilterCondition="Contains"  FieldName="fecha_envio_aclaraciones"  PropertiesDateEdit-DisplayFormatString="dd/MM/yyyy hh:mm:ss"   caption="Fecha Recibo Aclaraciones" ReadOnly="True" VisibleIndex="10">
             </dx:GridViewDataDateColumn>
             <dx:GridViewDataTextColumn Settings-AutoFilterCondition="Contains" Visible="false" FieldName="hora_envio_aclaraciones" caption="Hora Envio Aclaraciones" VisibleIndex="11">
             </dx:GridViewDataTextColumn>

             <dx:GridViewDataTextColumn Settings-AutoFilterCondition="Contains" FieldName="responsable"  caption="Responsable"  ReadOnly="True" VisibleIndex="13"></dx:GridViewDataTextColumn>
             <dx:GridViewDataDateColumn Settings-AutoFilterCondition="Contains" FieldName="Fecha_Limite" caption="Fecha Limite" ReadOnly="True" VisibleIndex="13"></dx:GridViewDataDateColumn>
             <dx:GridViewDataDateColumn Settings-AutoFilterCondition="Contains" PropertiesDateEdit-DisplayFormatString="dd/MM/yyyy" FieldName="fecha_tramite_fin" caption="Fecha tramite fin" ReadOnly="True" VisibleIndex="13"></dx:GridViewDataDateColumn>

             <dx:GridViewDataColumn Settings-AutoFilterCondition="Contains" FieldName="fecha_resolucion" caption="Resolución/Certificado" VisibleIndex="14">
                 <DataItemTemplate>
                                         

                     <asp:LinkButton ID="LbCertificado" runat="server" 
                         visible='<%# verVisibleCertificado( Eval("fecha_notificacion"),Eval("state_id"),Eval("version") ) %>'
                    Text='Certificado'
                    CommandName="project_id" 
                    CommandArgument='<%#Bind("project_id") %>'>
                </asp:LinkButton>

                      <asp:HyperLink runat="server" ID="lnkResolucion" Text="Resolución" Target="_blank"
                           visible='<%# verVisibleResolucion( Eval("fecha_notificacion"),Eval("pdf_resolucion"),Eval("version")) %>'
                              NavigateUrl='<%# "uploads/resolutions/"+Eval("project_id")+"/"+Eval("pdf_resolucion") %>' ></asp:HyperLink>

                             <asp:HyperLink runat="server" ID="lnkResolucionAclaratoria" Text="<br><br>Resolución Aclaratoria" Target="_blank"
                           visible='<%# verVisibleResolucion( Eval("fecha_notificacion") ,Eval("pdf_resolucion_aclaratoria"),Eval("version")) %>'
                              NavigateUrl='<%# "uploads/resolutions/"+Eval("project_id")+"/"+Eval("pdf_resolucion_aclaratoria") %>' ></asp:HyperLink>


                     
                    </ItemTemplate>
                 </DataItemTemplate>
             </dx:GridViewDataColumn>
             
             <dx:GridViewDataColumn Settings-AutoFilterCondition="Contains" Caption="Opciones" FieldName="project_id" VisibleIndex="15">
             <DataItemTemplate>   
                         <asp:HyperLink runat="server" ID="lnkProyecto" Text='Ver_Proyecto' 
                        NavigateUrl='<%# "DatosProyecto.aspx?project_id="+Eval("project_id") %>'></asp:HyperLink>
                 <!--
                  <asp:HyperLink runat="server" ID="lnkCargarFormulario" Text='Cargar_Formulario' 
                       visible='<%# ((int)Session["user_role_id"] >=1) %>'
                      onclick='<%# "cargarArchivo("+Eval("project_id")+")" %>'
                        NavigateUrl="#"></asp:HyperLink>
                     -->
                         <asp:HyperLink runat="server" ID="lnkResponsable" Text='Responsable' 
                              visible='<%# ((int)Session["user_role_id"] >=1) %>'
                        NavigateUrl='<%# "frmResponsable.aspx?project_id="+Eval("project_id") %>'></asp:HyperLink>
                 </DataItemTemplate>
             </dx:GridViewDataColumn>

         </Columns>
       
     </dx:ASPxGridView>



          </div>

    </div>

   <br />
                      

        <br />

      <uc1:cargando ID="cargando1" runat="server" />
</asp:Content>