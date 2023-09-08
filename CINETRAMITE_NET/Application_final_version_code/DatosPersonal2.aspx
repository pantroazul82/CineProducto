<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DatosPersonal2.aspx.cs" Inherits="CineProducto.DatosPersonal2" %>
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
        
        function validaNumericos(event) {
            if (event.charCode >= 48 && event.charCode <= 57) {
                return true;
            }
            return false;
        }

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
                             var stopIteration = false;
                             $.each(data.files, function (index, file) {
                                 cnt = cnt + 1;
                                 ext = file.name.substring(file.name.lastIndexOf('.'));
                                 if (file.size > 5242880) {
                                     alert('El archivo ' + file.name + '  supera el tamaño máximo de 5 Megas.');
                                     return;
                                 }

                                 var CaracterEsp = /^[a-zA-Z0-9\s-._]+$/;
                                 if (!CaracterEsp.test(file.name)) {
                                     alert('El nombre del archivo no debe contener caracteres especiales: #@+(){}°~“´´%&');
                                     stopIteration = true;
                                     return false;
                                 }
                             });

                             if (stopIteration) {
                                 return;
                             }

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
                             alert('El Archivo se cargo correctamente.');
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
            
            $('#fecha_nacimiento_1').datepicker({ dateFormat: "yy-mm-dd", changeMonth: true, changeYear: true, });
            $('#fecha_nacimiento_2').datepicker({ dateFormat: "yy-mm-dd", changeMonth: true, changeYear: true, });
            $('#fecha_nacimiento_3').datepicker({ dateFormat: "yy-mm-dd", changeMonth: true, changeYear: true, });
            $('#fecha_nacimiento_4').datepicker({ dateFormat: "yy-mm-dd", changeMonth: true, changeYear: true, });
            $('#fecha_nacimiento_5').datepicker({ dateFormat: "yy-mm-dd", changeMonth: true, changeYear: true, });
            $('#fecha_nacimiento_6').datepicker({ dateFormat: "yy-mm-dd", changeMonth: true, changeYear: true, });
            $('#fecha_nacimiento_7').datepicker({ dateFormat: "yy-mm-dd", changeMonth: true, changeYear: true, });
            $('#fecha_nacimiento_8').datepicker({ dateFormat: "yy-mm-dd", changeMonth: true, changeYear: true, });
            $('#fecha_nacimiento_9').datepicker({ dateFormat: "yy-mm-dd", changeMonth: true, changeYear: true, });
            $('#fecha_nacimiento_10').datepicker({ dateFormat: "yy-mm-dd", changeMonth: true, changeYear: true, });
            $('#fecha_nacimiento_11').datepicker({ dateFormat: "yy-mm-dd", changeMonth: true, changeYear: true, });
            $('#fecha_nacimiento_12').datepicker({ dateFormat: "yy-mm-dd", changeMonth: true, changeYear: true, });
            $('#fecha_nacimiento_13').datepicker({ dateFormat: "yy-mm-dd", changeMonth: true, changeYear: true, });
            $('#fecha_nacimiento_14').datepicker({ dateFormat: "yy-mm-dd", changeMonth: true, changeYear: true, });
            $('#fecha_nacimiento_15').datepicker({ dateFormat: "yy-mm-dd", changeMonth: true, changeYear: true, });
            $('#fecha_nacimiento_16').datepicker({ dateFormat: "yy-mm-dd", changeMonth: true, changeYear: true, });
            $('#fecha_nacimiento_17').datepicker({ dateFormat: "yy-mm-dd", changeMonth: true, changeYear: true, });
            $('#fecha_nacimiento_18').datepicker({ dateFormat: "yy-mm-dd", changeMonth: true, changeYear: true, });
            $('#fecha_nacimiento_19').datepicker({ dateFormat: "yy-mm-dd", changeMonth: true, changeYear: true, });
            $('#fecha_nacimiento_20').datepicker({ dateFormat: "yy-mm-dd", changeMonth: true, changeYear: true, });
            

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
             <%  if (user_role == 6)  { %>
            DisableEnableForm(true, 'desactivar');
            <%} %>  

            if ($("#hdHabilitarForm").val() == "Activo") {
                DisableEnableForm(false, 'activar');
                $("#hdHabilitarForm").val("");
            }

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


        function PopulateControl(list, control) {
            if (list.length > 0) {
                control.removeAttr("disabled");
                control.empty();
                control.append($("<option></option>").val("0").html("Seleccione"));
                $.each(list, function () {
                    control.append($("<option></option>").val(this['Value']).html(this['Text']));
                });
            } else {
                control.empty();
                control.append($("<option></option>").val("0").html("Seleccione"));
            }
        }
    </script>

<div id="cine">
    <!-- Bloque de información contextual -->
    <div id="informacion-contextual">
        <div class="bloque"><strong>Nombre:</strong><br /><strong><asp:Label ID="nombre_proyecto" runat="server"></asp:Label></strong></div>
        <div class="bloque">
            <strong><asp:Label ID="tipo_produccion" runat="server"></asp:Label></strong><br />
            <asp:Label ID="tipo_proyecto" runat="server"></asp:Label>
        </div>
        <div class="bloque"><strong>Productor:</strong><br /><asp:Label ID="nombre_productor" runat="server"></asp:Label></div>
        <div class="bloque">
               <div class="pull-right" ><asp:Label ID="opciones_adicionales" runat="server"></asp:Label><asp:Label runat="server" ID="lblCodProyecto" Visible="false"></asp:Label></div>
    </div></div>

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

  <asp:HiddenField runat="server" ClientIDMode="Static" ID="hdHabilitarForm" Value=""></asp:HiddenField>
    <div id='Personal'>
        <p>Cuando varias personas se hayan desempeñado en un mismo cargo, solo incluya la información y los documentos de quien figura en primer lugar en el respectivo crédito. Cada cargo es válido solo una vez.
          </p>
        <input style="min-width:300px !important" type="hidden" name="change_has_domestic_director" id="change_has_domestic_director" value="0" />
        <input style="min-width:300px !important" type="hidden" name="change_staff_option" id="change_staff_option" value="0" />
        <input style="min-width:300px !important" type="hidden" name="add_optional_position" id="add_optional_position" value="0" />
        <input style="min-width:300px !important" type="hidden" name="remove_optional_position" id="remove_optional_position" value="0" />
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
                    <div class="">¿Tiene Director Colombiano?</div>
                    <div class="field_input">
                        <% if (showDirectorSelect)
                           { %>
                           <asp:DropDownList ID="hasDomesticDirectorDDL" runat="server"></asp:DropDownList>
                        <% } %>
                        <% else { %>
                            
                            <asp:Label ID="has_domestic_director_label" runat="server"></asp:Label>
                              <% if (user_role != 6){ %>
                                <a href="#" class="small-link" id="change_has_domestic_director_link">[cambiar opción]</a>
                             <% } %>
                        <% } %>
                    </div>
                </li>
                <% if (showStaffOptions)
                   { %>
                        <li>
                            <div class="">Opci&oacute;n de personal<span class="required_field_text">*</span>:</div>
                            <div class="field_input">
                                <asp:Label ID="lblOpcionPersonalSeleccionado" runat="server"></asp:Label>
                                <asp:DropDownList ID="staffOptionDDL" runat="server" Visible="false"></asp:DropDownList>                                
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
                         <div class="">Comentarios adicionales:<span class="required_field_text"></span>
                            
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


        </div></div>
        <% if (showPersonalForms)
           { %>
        <asp:Panel runat="server" ID="pnlDatosPErsonal">
               <fieldset>
                    <asp:Repeater id="StaffRepeater" runat="server">
                        <HeaderTemplate><legend>Detalle del personal</legend></HeaderTemplate>
                        <ItemTemplate>
                 
                            <div style="width:100%;min-width:900px;">
                                
                             <asp:Image ID="ImgExpand" Width="25px" EnableTheming="false" AlternateText="Mas Información" runat="server"  />
                                <b> <%# Eval("position_name").ToString()+":" %></b><%# ((Eval("textoCargo").ToString().Trim() == string.Empty)?"":" ("+Eval("textoCargo").ToString().Trim()+") ") %> <%# Eval("project_staff_firstname").ToString()+" "+Eval("project_staff_firstname2").ToString()+" "+Eval("project_staff_lastname").ToString() +" "+Eval("project_staff_lastname2").ToString() %>

                                <asp:Image ID="Image1" Width="15px" EnableTheming="false" AlternateText="" runat="server" Visible='<%# (int)Session["user_role_id"] >=1 %>'
                                    ImageUrl='<%# (Eval("attachment_status") != null && bool.Parse(Eval("attachment_status").ToString()) )?"~/images/success.png":"~/images/error.png"  %>'  
                                      />
                      
                             </div>
                            
                            <input style="min-width:300px !important" type="hidden" name="project_staff_id_<%# Eval("repeater_index")%>" id="project_staff_id_<%# Eval("repeater_index")%>" value="<%# Eval("project_staff_id") %>" />
 
                                   <cc1:CollapsiblePanelExtender ID="cpe" runat="Server"
    TargetControlID='pnlPersonal'    CollapsedSize="0"    ExpandedSize="700"    Collapsed="True"    ExpandControlID='ImgExpand'
    CollapseControlID='ImgExpand'    AutoCollapse="False"    AutoExpand="False" ImageControlID="ImgExpand"
                                       ExpandedImage="~/Images/collapse.png"  CollapsedImage="~/Images/expand.png" ExpandDirection="Vertical" />


                             <asp:Panel runat="server" ID='pnlPersonal'>
                                 <br />
                            <fieldset class="<%# Eval("fielset_css_class")%>" style="line-height:0px;">
                                <legend><%# Eval("position_name")%></legend>
                                <table width="90%">
                                    <tr>
                                        <td colspan="4">
                                            <%#  Eval("project_staff_position")%>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td>Tipo de Documento de identidad:<span class="required_field_text">*</span></td>
                                        <td> <%# Eval("identification_type_id")%>   </td>                                        
                                        <td>N&uacute;mero de documento de identidad:<span class="required_field_text">*</span></td>
                                        <td><input style="min-width:300px !important" type="text" onpaste="return false;" onkeypress='return validaNumericos(event)' maxlength="50"  name="identification_number_<%# Eval("repeater_index")%>"  <%# !bool.Parse(Eval("adjuntosPendientes").ToString())?"readonly":""%> id="identification_number" class="<%# Eval("project_staff_identification_number_css_class")%>" value="<%# Eval("project_staff_identification_number")%>"/></td>
                                        
                                    </tr>
                                   <tr><td>
                                           Primer Nombre:<span class="required_field_text">*</span>
                                            </td>
                                       <td>
                                           <input style="min-width:300px !important" type="text" name="firstname_<%# Eval("repeater_index")%>"  <%# !bool.Parse(Eval("adjuntosPendientes").ToString())?"readonly":""%> id="firstname_<%# Eval("repeater_index")%>" class="<%# Eval("project_staff_firstname_css_class")%>" value="<%# Eval("project_staff_firstname")%>" />
                                       </td>
                                    <td>
                                          Segundo Nombre:<span class=""></span>
                                            </td>
                                       <td>
                                           <input style="min-width:300px !important" type="text" name="firstname2_<%# Eval("repeater_index")%>"  <%# !bool.Parse(Eval("adjuntosPendientes").ToString())?"readonly":""%> id="firstname2_<%# Eval("repeater_index")%>" class="<%# Eval("project_staff_firstname_css_class")%>" value="<%# Eval("project_staff_firstname2")%>" />
                                    </td>
                                    </tr>
                                    <tr><td>
                                        <div class="">Primer Apellido:<span class="required_field_text">*</span>
                                            </td><td><input style="min-width:300px !important" type="text"  name="lastname_<%# Eval("repeater_index")%>"  <%# !bool.Parse(Eval("adjuntosPendientes").ToString())?"readonly":""%> id="lastname_<%# Eval("repeater_index")%>" class="<%# Eval("project_staff_lastname_css_class")%>" value="<%# Eval("project_staff_lastname")%>"/>
                                    </td>
                                    <td>
                                        <div class="">Segundo Apellido:<span class=""></span>
                                            </td>
                                        <td>
                                            <input style="min-width:300px !important" type="text" name="lastname2_<%# Eval("repeater_index")%>"  <%# !bool.Parse(Eval("adjuntosPendientes").ToString())?"readonly":""%> id="lastname2_<%# Eval("repeater_index")%>" class="<%# Eval("project_staff_lastname_css_class")%>" value="<%# Eval("project_staff_lastname2")%>"/>
                                        </td>
                                    <%# Eval("project_staff_identification_type")%>
                                    </tr>

                                    <tr>
                                        <td>Fecha de nacimiento:</td>
                                        <td>
                                            <input style="min-width:300px !important" type="text" readonly="readonly"  name="fecha_nacimiento_<%# Eval("repeater_index")%>"  <%# !bool.Parse(Eval("adjuntosPendientes").ToString())?"readonly":""%> id="fecha_nacimiento_<%# Eval("repeater_index")%>" class="<%# Eval("project_staff_lastname_css_class")%>" value="<%# Eval("fecha_nacimiento")%>"/>                                            
                                        </td>


                                        <td>G&eacute;nero:<span class="required_field_text">*</span></td>
                                        <td>
                                             <%# Eval("id_genero")%>                                                                                   
                                        </td>
                                    </tr>
                                    
                                    <tr>
                                        <td>
                                            Grupo étnico:<span class="required_field_text">*</span>
                                        </td>
                                        <td>
                                             <%# Eval("id_etnia")%>     
                                        </td>
                                        <td>Grupo poblacional:</td>
                                        <td>  
                                             <%# Eval("id_grupo_poblacional")%>      
                                        </td>
                                    </tr>
                                    <tr>
                                            <td>
                                               Departamento de origen:<span class="required_field_text">*</span>
                                            </td>
                                             <td>      
                                                  <%# Eval("project_staff_state_id")%>                                                                                                

                                                 <!--input type="text" name="state_<%# Eval("repeater_index")%>" <%# !bool.Parse(Eval("adjuntosPendientes").ToString())?"readonly":""%> id="state" class="<%# Eval("project_staff_state_css_class")%>" value="<%# Eval("project_staff_state")%>"/-->                 
                                                
                                                                                                  

                                             </td>
                                        <td>
                                        Municipio de origen:<span class="required_field_text">*</span>
                                            </td>
                                        <td>                                           
                                            
                                            
                                            <%# Eval("project_staff_localization_id")%>        
                                            

                                            <!--input type="text" name="city_<%# Eval("repeater_index")%>"  <%# !bool.Parse(Eval("adjuntosPendientes").ToString())?"readonly":""%> id="city" class="<%# Eval("project_staff_city_css_class")%>" value="<%# Eval("project_staff_city")%>"/-->

                                             <script type="text/javaScript">
                                                 $('#cmbDepto_<%# Eval("repeater_index")%>').change(function () {
                                                     $('#localizacion_id_<%# Eval("repeater_index")%>').empty().append('<option selected="selected" value="0">Cargando...</option>');
                                                     $.ajax({
                                                         type: "POST",
                                                         url: "Default.aspx/obtenerMunicipios",
                                                         data: '{departamento: "' + $('#cmbDepto_<%# Eval("repeater_index")%>').val() + '"}',
                                                         contentType: "application/json; charset=utf-8",
                                                         dataType: "json",
                                                         success: OnMunicipiosPopulated<%# Eval("repeater_index")%>,
                                                         failure: function (response) {
                                                             alert(response.d);
                                                         }
                                                     });
                                                 });

                                                 function OnMunicipiosPopulated<%# Eval("repeater_index")%>(response) {
                                                      PopulateControl(response.d, $('#localizacion_id_<%# Eval("repeater_index")%>'));
                                                 }
                                             </script>
                                                                                  </td>
                                    </tr>

                                    
                                    <!--tr>
                                    <td>
                                        <div class="">Direcci&oacute;n:<span class="required_field_text">*</span>
                                            </td>
                                        <td><input style="min-width:300px !important" type="text" name="address_<%# Eval("repeater_index")%>" <%# !bool.Parse(Eval("adjuntosPendientes").ToString())?"readonly":""%> id="address_<%# Eval("repeater_index")%>" class="<%# Eval("project_staff_address_css_class")%>" value="<%# Eval("project_staff_address")%>" />
                                    </td>
                                   </tr-->
                                    
                                    <tr> 
                                       <td>
                                        Tel&eacute;fono:<span class="required_field_text">*</span>
                                            </td>
                                        <td>
                                                <input style="min-width:300px !important" type="text" onpaste="return false;" onkeypress='return validaNumericos(event)' maxlength="50" name="phone_<%# Eval("repeater_index")%>" <%# !bool.Parse(Eval("adjuntosPendientes").ToString())?"readonly":""%> id="phone_<%# Eval("repeater_index")%>" class="<%# Eval("project_staff_phone_css_class")%>" value="<%# Eval("project_staff_phone")%>"/>
                                    </td>
                                    <td>
                                        Tel&eacute;fono alternativo:
                                        </td>
                                        <td>
                                            <input style="min-width:300px !important" type="text" onpaste="return false;" onkeypress='return validaNumericos(event)' maxlength="50" name="movil_<%# Eval("repeater_index")%>" <%# !bool.Parse(Eval("adjuntosPendientes").ToString())?"readonly":""%> id="movil" class="<%# Eval("project_staff_movil_css_class")%>" value="<%# Eval("project_staff_movil")%>"/>
                                    </td>
                                    </tr>
                                    <tr><td>
                                        Correo electr&oacute;nico:<span class="required_field_text">*</span>

                                        </td>
                                        <td>
                                            <input style="min-width:300px !important" type="text" name="email_<%# Eval("repeater_index")%>" <%# !bool.Parse(Eval("adjuntosPendientes").ToString())?"readonly":""%>  id="email_<%# Eval("repeater_index")%>" class="<%# Eval("project_staff_email_css_class")%>" value="<%# Eval("project_staff_email")%>"/>
                                    </td>
                                    </tr>
                                    <tr>
                                    <td colspan="4">
                                        <%# Eval("additional_option_block")%>
                                        <fieldset style="width:95%;">
                                            <legend>Adjuntos</legend>
                                            <div id="attachment-box">
                                                <%# Eval("Attachment_code")%>
                                            </div>
                                        </fieldset>
                                    </td>
                                        </tr>                                    
                                    
                                </table>
                                <div class="col-1">
                                 <% if (showStaffOptions)
                                               { %>
                                                    <%if (project_state_id != 9 && project_state_id != 10  && user_role != 6)        { %>
                                                        <div class="field_input" style="width:809px;">
                                                            <input style="min-width:150px !important;height:30px" type="submit" id="submit_personal_data" class="boton"  name="submit_personal_data" value="Guardar" onclick=' $("#loading").show();DisableEnableForm(false,"activar");'/></div>
                                                    <%} %>
                                    <% 
                                    } %>
                                </div>
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

                    <%if(project_state_id == 9 ) {%>
                                <ul>                                                                        
                                    <li>
                                        <label>Información corecta!</label></li>                                  

                                </ul>
                                <%}%>

                 <%if (project_state_id != 6 && project_state_id != 7 && project_state_id != 8 && project_state_id != 9)
                   {%>
                    <ul>
                        <li><input style="min-width:300px !important" type="radio" name="gestion_realizada" id="gestion_realizada_sin_revisar" value="none" runat="server" /><label for="gestion-realizada-sin-revisar">Sin revisar</label></li>
                        <li><input style="min-width:300px !important" type="radio" name="gestion_realizada" id="gestion_realizada_solicitar_aclaraciones" value="solicitar-aclaraciones" runat="server" /><label for="gestion-realizada-solicitar-aclaraciones">Solicitar aclaraciones</label></li>
                        <li><input style="min-width:300px !important" type="radio" name="gestion_realizada" id="gestion_realizada_informacion_correcta" value="informacion-correcta" runat="server" class="depending-box" /><label for="gestion-realizada-informacion-correcta">Informaci&oacute;n correcta</label></li>
                    </ul>
                    <%}
                    if (project_state_id == 6 || project_state_id == 7 || project_state_id == 8)
                   {%>
                    <fieldset>
                        <ul>
                            <li><input style="min-width:300px !important" type="radio" name="estado_revision" id="estado_revision_sin_revisar" value="none" runat="server" /><label for="estado_revision_sin_revisar">Sin revisar</label></li>
                            <li><input style="min-width:300px !important" type="radio" name="estado_revision" id="estado_revision_revisado" value="revisado" runat="server" /><label for="estado_revision_revisado">No cumple</label></li>
                            <li><input style="min-width:300px !important" type="radio" name="estado_revision" id="estado_revision_aprobado" value="aprobado"  runat="server" class="depending-box" /><label for="estado_revision_aprobado">Cumple</label></li>
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
                         <li><h3>Observaciones</h3>                             
                             <textarea  style="width:620px;min-height:200px;"  name="informacion_correcta" id="informacion_correcta"  rows="5" cols="40" runat="server"></textarea></li>
                    </ul>
                </div>
                <div id="admin-form-right">
                    <a href="/SolicitudAclaraciones.aspx" target="_blank">Previsualizaci&oacute;n de la solicitud de aclaraciones</a>
                </div>
                <%if (project_state_id != 9 && project_state_id != 10) { %>
                <div id="link">
                    <div style='margin:0;text-align:left;text-decoration:underline;cursor:pointer;padding:0 0 20px 0;' onclick='DisableEnableForm(true,"desactivar")'>Desactivar el formulario</div>
                </div>
                <%}%>
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
                        <%if (project_state_id != 9 && project_state_id != 10 && user_role != 6)        { %>

                            <div class="alert alert-warning" style="position: fixed; right: 50px;z-index:9999; margin-top: 200px;  min-height: 60px;    width: 250px;    text-align: center;    word-wrap: break-word;   >
                       <div class="alert alert-warning">
                        Guardar tu información!
                        
                            <input style="min-width:150px !important; height:30px" type="submit" id="submit_personal_data" class="boton"  name="submit_personal_data" value="Guardar" onclick=' $("#loading").show();DisableEnableForm(false,"activar");'/>

                           </div>
                    </div>
                   
                            
                        <%} %>
        <% 
        } %>
    </div>
    
</div>
</asp:Content>