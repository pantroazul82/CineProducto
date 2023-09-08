<%@ Page Title="Formulario de datos del personal" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" ValidateRequest="false"
    CodeBehind="DatosPersonal.aspx.cs" Inherits="CineProducto.DatosPersonal" EnableEventValidation="false" %>
<%@ Register src="usercontrols/cargando.ascx" tagname="cargando" tagprefix="uc1" %>


<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="TitleContent" runat="server" ContentPlaceHolderID="TitleContent">Trámite Reconocimiento Como Obra Nacional - Datos de Personal - Mincultura</asp:Content>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <script src="<%= Page.ResolveClientUrl("~/Scripts/cine.js")%>" type="text/javascript"></script>
     <style>
        .progressbar
        {
            background-color: black;
            background-repeat: repeat-x;
            border-radius: 13px; /* (height of inner div) / 2 + padding */
            padding: 3px;
        }
        
        .progressbar > div
        {
            background-color: orange;
            width: 0%; /* Adjust with JavaScript */
            height: 20px;
            border-radius: 10px;
        }
    </style>
    <%--<script src="blueimp/jquery-1.11.1.js" type="text/javascript"></script>--%>
    <script src="blueimp/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="blueimp/jquery.iframe-transport.js" type="text/javascript"></script>
    <script src="blueimp/jquery.fileupload.js" type="text/javascript"></script>

</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
       <asp:ScriptManager runat="server" ID="scManager"
           EnableScriptGlobalization="true"
EnableScriptLocalization="true" ></asp:ScriptManager>

    <script type = "text/javaScript">
        $(document).ready(function () {
            scroll();
            autosize($('#<%=solicitud_aclaraciones.ClientID %>'));
            autosize($('#<%=comentarios_adicionales.ClientID %>'));
            
            autosize($('#<%=informacion_correcta.ClientID %>'));
            autosize($('#<%=producer_clarifications_field.ClientID %>'));

            habilitarTracker();
            /**
            * Incluimos codigo js para el proceso de adjuntos de la seccion datos del proyecto
            */
            <asp:Repeater id="AttachmentRepeater2" runat="server">
                <ItemTemplate>

                 $(function () {

                     $('#FileUpload<%# Eval("attachment_id")%>').fileupload({
                         url: 'FileUploadHandler.ashx?upload=start&folder=<%= Page.ResolveClientUrl("~/uploads")%>'+'/<%=project_id %>'+'&attachment_id=<%# Eval("attachment_id")%>',
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

                             if (cnt > 1){
                                 alert('Solo debe seleccionar un archivo');
                                 return;
                             }
                             if('<%# Eval("attachment_id")%>' != '50'){
                                 if (ext.toUpperCase() != '.PDF') {
                                     alert('solo es valido subir archivos en formato pdf.');
                                     return;
                                 }
                             }else{
                                 if (ext.toUpperCase() != '.XLS' && ext.toUpperCase() != '.XLSX') {
                                     alert('solo es valido subir archivos en formato xls para el costo discriminado.');
                                     return;
                                 }
                             }
                        
                             //   console.log('add', data);
                             $('#progressbar<%# Eval("attachment_id")%>').show();
                             //    $('#progressbar1').show();
                             data.submit();
                         },
                         progress: function (e, data) {
                             var progress = parseInt(data.loaded / data.total * 100, 10);
                             $('#progressbar<%# Eval("attachment_id")%> div').css('width', progress + '%');
                         },
                         success: function (response, status) {
                             $('#progressbar<%# Eval("attachment_id")%>').hide();
                             $('#progressbar<%# Eval("attachment_id")%> div').css('width', '0%');

                             if (response.indexOf("Error") >= 0) {
                                 alert(response);
                             } else {

                                 $.ajax({
                                     type: 'POST',
                                     url: '<%=Page.ResolveClientUrl("~/Default.aspx/getPersonalAttachmentStatus") %>',
                                     data: '{pAttachment_id:"<%# Eval("attachment_id")%>",idproyecto:<%=project_id%>,uploadedfilename:"' + response + '",_project_staff_id:"<%# Eval("project_staff_id").ToString()%>"}',
                                     
                                     
                                      contentType: 'application/json; charset=utf-8',
                                     dataType: 'json',
                                     success: function (msg) {
                                         // Replace the div's content with the page method's return.
                                         $('#name_<%# Eval("attachment_id")%>').html(msg.d);
                                     }
                                 });
                             }

                      
                         },
                         error: function (error) {
                             alert('error');
                             $('#progressbar<%# Eval("attachment_id")%>').hide();
                             $('#progressbar<%# Eval("attachment_id")%> div').css('width', '0%');
                             console.log('error', error);
                         }
                     });
                 });

                </ItemTemplate>
            </asp:Repeater>
            


            $('#<%=hasDomesticDirectorDDL.ClientID %>').change(function () {
                $('#loading').show();
                $('#aspnetForm').submit();
               // $('#loading').hide();
            });
            $('#<%=staffOptionDDL.ClientID %>').change(function () {
                $('#loading').show();
                $('#change_staff_option').val("1");
                $('#aspnetForm').submit();
                //$('#loading').hide();
            });
            $('#change_has_domestic_director_link').click(function () {
                $('#loading').show();
                $('#change_has_domestic_director').val("1");
                $('#aspnetForm').submit();
               // $('#loading').hide();
            });

            /* Agrega la clase user-input a todos los input que diligencia el usuario con el fin de poderlos desactivar y activar */
            $('#<%=hasDomesticDirectorDDL.ClientID %>').addClass("tooltip-staff-hasdomesticdirector user-input");
            $('#<%= staffOptionDDL.ClientID %>').addClass("tooltip-staff-option user-input");
            $('#<%= comentarios_adicionales.ClientID %>').addClass("user-input");
            /* Crea la función de presentación del tooltip en todos los campos */
            $('.tooltip-staff-hasdomesticdirector').tooltip({ bodyHandler: function () { return '<asp:Literal id="tooltip_staff_hasdomesticdirector" runat="server"></asp:Literal>'; }, showURL: false });
            $('.tooltip-staff-option').tooltip({ bodyHandler: function () { return '<asp:Literal id="tooltip_staff_option" runat="server"></asp:Literal>'; }, showURL: false });
            $('.tooltip-staff-firstname').tooltip({ bodyHandler: function () { return '<asp:Literal id="tooltip_staff_firstname" runat="server"></asp:Literal>'; }, showURL: false });
            $('.tooltip-staff-lastname').tooltip({ bodyHandler: function () { return '<asp:Literal id="tooltip_staff_lastname" runat="server"></asp:Literal>'; }, showURL: false });
            $('.tooltip-staff-identification-type').tooltip({ bodyHandler: function () { return '<asp:Literal id="tooltip_staff_identification_type" runat="server"></asp:Literal>'; }, showURL: false });
            $('.tooltip-staff-identification-number').tooltip({ bodyHandler: function () { return '<asp:Literal id="tooltip_staff_identification_number" runat="server"></asp:Literal>'; }, showURL: false });
            $('.tooltip-staff-city').tooltip({ bodyHandler: function () { return '<asp:Literal id="tooltip_staff_city" runat="server"></asp:Literal>'; }, showURL: false });
            $('.tooltip-staff-state').tooltip({ bodyHandler: function () { return '<asp:Literal id="tooltip_staff_state" runat="server"></asp:Literal>'; }, showURL: false });
            $('.tooltip-staff-address').tooltip({ bodyHandler: function () { return '<asp:Literal id="tooltip_staff_address" runat="server"></asp:Literal>'; }, showURL: false });
            $('.tooltip-staff-phone').tooltip({ bodyHandler: function () { return '<asp:Literal id="tooltip_staff_phone" runat="server"></asp:Literal>'; }, showURL: false });
            $('.tooltip-staff-movil').tooltip({ bodyHandler: function () { return '<asp:Literal id="tooltip_staff_movil" runat="server"></asp:Literal>'; }, showURL: false });
            $('.tooltip-staff-email').tooltip({ bodyHandler: function () { return '<asp:Literal id="tooltip_staff_email" runat="server"></asp:Literal>'; }, showURL: false });
        
            <%
            if (showAdvancedForm || (project_state_id == 5 && section_state_id != 10) 
                || ((project_state_id == 2 || project_state_id == 3 || project_state_id == 4 
                || project_state_id == 6 || project_state_id == 7 || 
                project_state_id >= 8) && user_role <= 1))
            { %>
                DisableEnableForm(true, 'desactivar');
            <%
            }
            %>

            $('#loading').hide();
        });
        /* Función que maneja el comportamiento para agregar cargos */
        function agregarCargo(staff_option_detail_id) {
            $('#add_optional_position').val(staff_option_detail_id);
            $('#aspnetForm').submit();
        }
        /* Función que maneja el comportamiento para agregar cargos */
        function removerCargo(staff_option_detail_id) {
            $('#remove_optional_position').val(staff_option_detail_id);
            $('#aspnetForm').submit();
        }

    </script>

<div id="cine">
    <!-- Bloque de información contextual -->
    <div id="informacion-contextual">
        <div class="bloque"><strong>proyecto:</strong><br /><asp:Label ID="nombre_proyecto" runat="server"></asp:Label></></div>
        <div class="bloque">
            <strong><asp:Label ID="tipo_produccion" runat="server"></asp:Label></strong><br />
            <asp:Label ID="tipo_proyecto" runat="server"></asp:Label>
        </div>
        <div class="bloque"><strong>Productor:</strong><br /><asp:Label ID="nombre_productor" runat="server"></asp:Label></div>
        <div class="bloque">
               <div class="pull-right" >
            <asp:Label ID="opciones_adicionales" runat="server"></asp:Label>
                   <asp:Label runat="server" ID="lblCodProyecto" Visible="false"></asp:Label>

               </div>
            </div>
    </div>

    <!-- Menu-->
    <div class="tabs">
        <ul id='menu'>
            <li class="<%=tab_datos_proyecto_css_class %>"><a href="DatosProyecto.aspx">Datos de<br />la Obra<%=tab_datos_proyecto_revision_mark_image %></a></li>
            <li class="<%=tab_datos_productor_css_class %>"><a href="DatosProductor.aspx">Datos del<br /> Productor<%=tab_datos_productor_revision_mark_image %></a></li>
            <li class="<%=tab_productores_adicionales_css_class %>"><a href="ProductoresAdicionales.aspx">Coproductores<%=tab_datos_productores_adicionales_revision_mark_image %></a></li>
            
            <li class="<%=tab_datos_personal_css_class %>"><a href="DatosPersonal.aspx">Personal<%=tab_datos_personal_revision_mark_image %></a></li>
            <li class="<%=tab_datos_formato_personal_css_class %>"><a href="DatosFormatoPersonal.aspx">Registro de personal <br />artístico y técnico   <%=tab_datos_formato_personal_revision_mark_image %></a></li>
            <!-- <li class="<%--=tab_datos_adjuntos_css_class --%>"><a href="DatosAdjuntos.aspx">Adjuntos<%--=tab_datos_adjuntos_revision_mark_image --%></a></li> -->
            <li class="<%=tab_datos_finalizacion_css_class %>"><a href="Finalizacion.aspx">Finalizaci&oacute;n</a></li>
        </ul>
    </div>
	<!-- End of Nav Div -->	
  <uc1:cargando ID="cargando1" runat="server" />

  
    <div id='Personal'>
        <p>Cuando varias personas se hayan desempeñado en un mismo cargo, solo incluya la información y los documentos de quien figura en primer lugar en el respectivo crédito. Cada cargo es válido solo una vez.
          </p>
        <input type="hidden" name="change_has_domestic_director" id="change_has_domestic_director" value="0" />
        <input type="hidden" name="change_staff_option" id="change_staff_option" value="0" />
        <input type="hidden" name="add_optional_position" id="add_optional_position" value="0" />
        <input type="hidden" name="remove_optional_position" id="remove_optional_position" value="0" />
  <asp:Panel runat="server" ID="pnlFormularioSeleccion">
        <fieldset>
            <legend>Tipo de personal</legend>
            <ul>
                <li>   
                    <asp:PlaceHolder runat="server" ID="pnlNotaTrabajo" Visible="false">
                     <div class="">
            Recuerde que, cuando hay participación actoral de menores de edad, se debe obtener previamente el respectivo permiso del Ministerio de Trabajo.
     </div>
                        </asp:PlaceHolder> 
                    <br />
                    <div class="field_label">¿Tiene Director Colombiano?</div>
                    <div class="field_input">
                        <% if (showDirectorSelect)
                           { %>
                           <asp:DropDownList ID="hasDomesticDirectorDDL" runat="server"></asp:DropDownList>
                        <% } %>
                        <% else { %>
                            <asp:Label ID="has_domestic_director_label" runat="server"></asp:Label>
                            <a href="#" class="small-link" id="change_has_domestic_director_link">[cambiar opción]</a>
                        <% } %>
                    </div>
                </li>
                <% if (showStaffOptions)
                   { %>
                        <li>
                            <div class="field_label">Opci&oacute;n de personal:<span class="required_field_text">*</span></div>
                            <div class="field_input">
                                <asp:DropDownList ID="staffOptionDDL" runat="server"></asp:DropDownList>
                            </div>
                        </li>
                <% } %>
                <% else if(showStaffOptionsMessage)
                   { %>
                        <li>
                            <div class="warning">
                                <asp:Label ID="staff_option_label" runat="server"></asp:Label>
                            </div>
                        </li>
                <% } %>

            </ul>
        </fieldset>
      </asp:Panel> 
        <ul>

                    <li>
                         <div class="field_label">Comentarios adicionales:<span class="required_field_text"></span>
                            
                        </div>
                        <div class="field_input">
                          <asp:TextBox TextMode="MultiLine" name="comentarios_adicionales" id="comentarios_adicionales" Width="400px" rows="3" class="user-input"
                                cols="60" runat="server"></asp:TextBox>
                        </div>
                    </li>
        </ul>
  <asp:Panel runat="server" ID="pnlMensajeVisible" Visible="false">
      
      <div style="clear:left;"></div>
     <div class="warning">
            <p>
            Debe diligenciar en su totalidad los datos de la pestaña DATOS DE LA OBRA (Tipo de Producción, Porcentajes de participación, Tipo de obra, Duración de la obra), de este modo la pestaña de PERSONAL, cargará correctamente.
                </p>
        </div>
      <div style="clear:left;"></div>
        </asp:Panel>
        <% if (showPersonalForms)
           { %>
        <asp:Panel runat="server" ID="pnlDatosPErsonal">
               <fieldset>
                    <asp:Repeater id="StaffRepeater" runat="server">
                        <HeaderTemplate><legend>Detalle del personal</legend></HeaderTemplate>
                        <ItemTemplate>
                 
                            <div style="width:100%;min-width:900px;">
                                
                             <asp:Image ID="ImgExpand" Width="25px" EnableTheming="false" AlternateText="Mas Información" runat="server"  />
                                <b> <%# Eval("position_name").ToString()+":" %></b><%# ((Eval("textoCargo").ToString().Trim() == string.Empty)?"":" ("+Eval("textoCargo").ToString().Trim()+") ") %> <%# Eval("project_staff_firstname").ToString()+" "+Eval("project_staff_lastname").ToString() %>

                                <asp:Image ID="Image1" Width="15px" EnableTheming="false" AlternateText="" runat="server" Visible='<%# (int)Session["user_role_id"] >=1 %>'
                                    ImageUrl='<%# (Eval("attachment_status") != null && bool.Parse(Eval("attachment_status").ToString()) )?"~/images/success.png":"~/images/error.png"  %>'  
                                      />
                      
                             </div>
                            
                            <input type="hidden" name="project_staff_id_<%# Eval("repeater_index")%>" id="project_staff_id_<%# Eval("repeater_index")%>" value="<%# Eval("project_staff_id") %>" />
 
                                   <cc1:CollapsiblePanelExtender ID="cpe" runat="Server"
    TargetControlID='pnlPersonal'    CollapsedSize="0"    ExpandedSize="750"    Collapsed="True"    ExpandControlID='ImgExpand'
    CollapseControlID='ImgExpand'    AutoCollapse="False"    AutoExpand="False" ImageControlID="ImgExpand"
                                       ExpandedImage="~/Images/collapse.png"  CollapsedImage="~/Images/expand.png" ExpandDirection="Vertical" />


                             <asp:Panel runat="server" ID='pnlPersonal'>
                            <fieldset class="<%# Eval("fielset_css_class")%>">
                                <legend><%# Eval("position_name")%></legend>
                                <ul>
                                    <%#  Eval("project_staff_position")%>
                                    <li>
                                        <div class="field_label">Nombres:<span class="required_field_text">*</span></div>
                                        <div class="field_input"><input type="text" name="firstname_<%# Eval("repeater_index")%>"  <%# !bool.Parse(Eval("adjuntosPendientes").ToString())?"readonly":""%> id="firstname_<%# Eval("repeater_index")%>" class="<%# Eval("project_staff_firstname_css_class")%>" value="<%# Eval("project_staff_firstname")%>" /></div>
                                    </li>
                                    <li>
                                        <div class="field_label">Apellidos:<span class="required_field_text">*</span></div>
                                        <div class="field_input"><input type="text" name="lastname_<%# Eval("repeater_index")%>"  <%# !bool.Parse(Eval("adjuntosPendientes").ToString())?"readonly":""%> id="lastname_<%# Eval("repeater_index")%>" class="<%# Eval("project_staff_lastname_css_class")%>" value="<%# Eval("project_staff_lastname")%>"/></div>
                                    </li>
                                    <%# Eval("project_staff_identification_type")%>
                                    <li>
                                        <div class="field_label">N&uacute;mero de cédula:<span class="required_field_text">*</span></div>
                                        <div class="field_input"><input type="text" name="identification_number_<%# Eval("repeater_index")%>"  <%# !bool.Parse(Eval("adjuntosPendientes").ToString())?"readonly":""%> id="identification_number" class="<%# Eval("project_staff_identification_number_css_class")%>" value="<%# Eval("project_staff_identification_number")%>"/></div>
                                    </li>
                                    <li>
                                        <div class="field_label">Ciudad:<span class="required_field_text">*</span></div>
                                        <div class="field_input"><input type="text" name="city_<%# Eval("repeater_index")%>"  <%# !bool.Parse(Eval("adjuntosPendientes").ToString())?"readonly":""%> id="city" class="<%# Eval("project_staff_city_css_class")%>" value="<%# Eval("project_staff_city")%>"/></div>
                                    </li>
                                    <li>
                                        <div class="field_label">Departamento:<span class="required_field_text">*</span></div>
                                        <div class="field_input"><input type="text" name="state_<%# Eval("repeater_index")%>" <%# !bool.Parse(Eval("adjuntosPendientes").ToString())?"readonly":""%> id="state" class="<%# Eval("project_staff_state_css_class")%>" value="<%# Eval("project_staff_state")%>"/></div>
                                    </li>
                                    <li>
                                        <div class="field_label">Direcci&oacute;n:<span class="required_field_text">*</span></div>
                                        <div class="field_input"><input type="text" name="address_<%# Eval("repeater_index")%>" <%# !bool.Parse(Eval("adjuntosPendientes").ToString())?"readonly":""%> id="address_<%# Eval("repeater_index")%>" class="<%# Eval("project_staff_address_css_class")%>" value="<%# Eval("project_staff_address")%>" /></div>
                                    </li>
                                    <li>
                                        <div class="field_label">Tel&eacute;fono:<span class="required_field_text">*</span></div>
                                        <div class="field_input"><input type="text" name="phone_<%# Eval("repeater_index")%>" <%# !bool.Parse(Eval("adjuntosPendientes").ToString())?"readonly":""%> id="phone_<%# Eval("repeater_index")%>" class="<%# Eval("project_staff_phone_css_class")%>" value="<%# Eval("project_staff_phone")%>"/></div>
                                    </li>
                                    <li>
                                        <div class="field_label">Celular:<span class="required_field_text">*</span></div>
                                        <div class="field_input"><input type="text" name="movil_<%# Eval("repeater_index")%>" <%# !bool.Parse(Eval("adjuntosPendientes").ToString())?"readonly":""%> id="movil" class="<%# Eval("project_staff_movil_css_class")%>" value="<%# Eval("project_staff_movil")%>"/></div>
                                    </li>
                                    <li>
                                        <div class="field_label">Correo electr&oacute;nico:<span class="required_field_text">*</span></div>
                                        <div class="field_input"><input type="text" name="email_<%# Eval("repeater_index")%>" <%# !bool.Parse(Eval("adjuntosPendientes").ToString())?"readonly":""%>  id="email_<%# Eval("repeater_index")%>" class="<%# Eval("project_staff_email_css_class")%>" value="<%# Eval("project_staff_email")%>"/></div>
                                    </li>
                                    
                                    <li>
                                        <%# Eval("additional_option_block")%>
                                        <fieldset style="width:375px;">
                                            <legend>Adjuntos</legend>
                                            <div id="attachment-box">
                                                <%# Eval("Attachment_code")%>
                                            </div>
                                        </fieldset>
                                    </li>
                                </ul>
                            </fieldset>
                                </asp:Panel>
                        </ItemTemplate>
                        <FooterTemplate></FooterTemplate>
                    </asp:Repeater>
                </fieldset> 
            </asp:Panel>
        <% } %>
        <%
        if (showAdvancedForm)
        { %>
            <div id="admin-form">
                <h3>Formulario de gestión de la solicitud</h3>
                <div id="admin-form-left">
                 <%if (project_state_id != 6 && project_state_id != 7 && project_state_id != 8)
                   {%>
                    <ul>
                        <li><input type="radio" name="gestion_realizada" id="gestion_realizada_sin_revisar" value="none" runat="server" /><label for="gestion-realizada-sin-revisar">Sin revisar</label></li>
                        <li><input type="radio" name="gestion_realizada" id="gestion_realizada_solicitar_aclaraciones" value="solicitar-aclaraciones" runat="server" /><label for="gestion-realizada-solicitar-aclaraciones">Solicitar aclaraciones</label></li>
                        <li><input type="radio" name="gestion_realizada" id="gestion_realizada_informacion_correcta" value="informacion-correcta" runat="server" class="depending-box" /><label for="gestion-realizada-informacion-correcta">Informaci&oacute;n correcta</label></li>
                    </ul>
                    <%}
                    if (project_state_id == 6 || project_state_id == 7 || project_state_id == 8)
                   {%>
                    <fieldset>
                        <ul>
                            <li><input type="radio" name="estado_revision" id="estado_revision_sin_revisar" value="none" runat="server" /><label for="estado_revision_sin_revisar">Sin revisar</label></li>
                            <li><input type="radio" name="estado_revision" id="estado_revision_revisado" value="revisado" runat="server" /><label for="estado_revision_revisado">No cumple</label></li>
                            <li><input type="radio" name="estado_revision" id="estado_revision_aprobado" value="aprobado"  runat="server" class="depending-box" /><label for="estado_revision_aprobado">Cumple</label></li>
                        </ul>
                    </fieldset>
                    <%} %>
                </div>
                <div id="admin-form-center">
                    <ul>
                        <%if (project_state_id >= 6 )
                         { %>
                         <li><h3>Aclaraciones solicitadas</h3><asp:Literal ID="clarification_request_summary" runat="server">No se han solicitado aclaraciones sobre esta pestaña</asp:Literal></li>
                         <li><h3>Respuesta a las aclaraciones</h3><asp:Literal ID="producer_clarification_summary" runat="server">No se han solicitado aclaraciones sobre esta pestaña</asp:Literal></li>
                         <%}
                         else
                         { %>
                         <li><h3>
                                           <b> Solicitud de aclaraciones</b></h3><textarea  style="width:620px;min-height:200px;"  name="solicitud_aclaraciones" id="solicitud_aclaraciones" rows="5" cols="40" runat="server"></textarea></li>
                         
                         <%} %>
                         <li><h3>Observaciones</h3><textarea  style="width:620px;min-height:200px;"  name="informacion_correcta" id="informacion_correcta" rows="5" cols="40" runat="server"></textarea></li>
                    </ul>
                </div>
                <div id="admin-form-right">
                    <a href="/SolicitudAclaraciones.aspx" target="_blank">Previsualizaci&oacute;n de la solicitud de aclaraciones</a>
                </div>
                <div id="link">
                    <div style='margin:0;text-align:left;text-decoration:underline;cursor:pointer;padding:0 0 20px 0;' onclick='DisableEnableForm(true,"desactivar")'>Desactivar el formulario</div>
                </div>
            </div>
        <% 
        } %>
        <%
        /* Si el estado del proyecto es "Aclaraciones solicitadas" y el estado de la sección es "rechazado" se presenta el formulario de registro de aclaraciones para el productor */
        if (project_state_id >= 5 && section_state_id == 10)
        { %>
            <div id="registro_aclaraciones_form">
                   <%  if (user_role <=1)  { %>
                <ul>
                    <li>
                    <%--    <h3>Formulario de registro de aclaraciones</h3>--%>
                            <div id="static_info">
                           <h3>
                                           <b> Solicitud de aclaraciones</b></h3>
                                <div><asp:Literal ID="clarification_request" runat="server">No se han solicitado aclaraciones sobre esta pestaña</asp:Literal></div>
                            </div>
                    </li>
                </ul>
                <div id="input_info">
                    <ul>
                        <li>
                            <h4>Escriba sus aclaraciones a continuación</h4>
                            <textarea class="user-input"  style="width:620px;min-height:200px;"  maxlength="4000"   name="producer_clarifications_field" id="producer_clarifications_field" rows="5" cols="80" runat="server"></textarea>
                        </li>
                    </ul>
                </div>
                   <% } %>
            </div>
        <% 
        } %>

         <% if (showStaffOptions)
                   { %>
        <div class="field_input"><input type="submit" id="submit_personal_data" class="boton"  name="submit_personal_data" value="Guardar" onclick=' $("#loading").show();DisableEnableForm(false,"activar");'/></div>
        <% 
        } %>
    </div>
    
</div>
</asp:Content>