<%@ Page Title="Formulario de datos del productor" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" ValidateRequest="false"
    CodeBehind="ProductoresAdicionales.aspx.cs" Inherits="CineProducto.ProductoresAdicionales" EnableEventValidation="false" %>
<%@ Register src="usercontrols/cargando.ascx" tagname="cargando" tagprefix="uc1" %>


<asp:Content ID="TitleContent" runat="server" ContentPlaceHolderID="TitleContent">Trámite Reconocimiento Como Obra Nacional - Coproductores - Mincultura</asp:Content>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <script src="/Scripts/cine.js" type="text/javascript"></script>
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
<div id="cine">
    <!-- Bloque de información contextual -->
    <div id="informacion-contextual">
        <div class="bloque"><strong><asp:Label ID="nombre_proyecto" runat="server"></asp:Label></strong></div>
        <div class="bloque">
            <strong><asp:Label ID="tipo_produccion" runat="server"></asp:Label></strong><br />
            <asp:Label ID="tipo_proyecto" runat="server"></asp:Label>
        </div>
        <div class="bloque"><strong>Productor:</strong><br /><asp:Label ID="nombre_productor" runat="server"></asp:Label></div>
        <div class="bloque"><asp:Label ID="opciones_adicionales" runat="server"></asp:Label><asp:Label runat="server" ID="lblCodProyecto" Visible="false"></asp:Label></div>
    </div>

    <!-- Menu-->
    <div class="tabs">
        <ul id='menu'>
            <li class="<%=tab_datos_proyecto_css_class %>"><a href="DatosProyecto.aspx">Datos de<br />la Obra<%=tab_datos_proyecto_revision_mark_image %></a></li>
            <li class="<%=tab_datos_productor_css_class %>"><a href="DatosProductor.aspx">Datos del<br /> Productor<%=tab_datos_productor_revision_mark_image %></a></li>
            <li class="<%=tab_productores_adicionales_css_class %>"><a href="ProductoresAdicionales.aspx">Coproductores<%=tab_datos_productores_adicionales_revision_mark_image %></a></li>
            
            <li class="<%=tab_datos_personal_css_class %>"><a href="DatosPersonal.aspx">Personal<%=tab_datos_personal_revision_mark_image %></a></li>
            
            <!--<li class="<%--=tab_datos_adjuntos_css_class --%>"><a href="DatosAdjuntos.aspx">Adjuntos<%--=tab_datos_adjuntos_revision_mark_image --%></a></li>-->
            <li class="<%=tab_datos_finalizacion_css_class %>"><a href="Finalizacion.aspx">Finalizaci&oacute;n</a></li>
        </ul>
    </div>
	<!-- End of Nav Div -->
     <script type = "text/javaScript">
         function mostrar_persona_natural() {
             $("#persona_juridica_field_group").hide("slow");
             $("#persona_natural_field_group").show("slow");
         }
         function mostrar_persona_juridica() {
             $("#persona_natural_field_group").hide("slow");
             $("#persona_juridica_field_group").show("slow");
         }
         function ocultar_campos_tipo_persona() {
             $("#persona_natural_field_group").hide("slow");
             $("#persona_juridica_field_group").hide("slow");
         }
         function showPersonType() {
             var person_type = $('#<%=personTypeDDL.ClientID %>').val();
             if (person_type == '1') {
                 mostrar_persona_natural();
             }
             if (person_type == '2') {
                 mostrar_persona_juridica();
             }
             if (person_type == '0') {
                 ocultar_campos_tipo_persona();
             }
             checkLocalizationFields();
         }
         $(document).ready(function () {
             scroll();
             autosize($('#<%=solicitud_aclaraciones.ClientID %>'));
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
                            url: 'FileUploadHandler.ashx?upload=start&folder=<%= Page.ResolveClientUrl("~/uploads")%>' +'/<%=project_id %>' +'/<%=producer_id_additional %>' +'&attachment_id=<%# Eval("attachment_id")%>',
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
                             if ('<%# Eval("attachment_id")%>' != '50') {
                                 if (ext.toUpperCase() != '.PDF') {
                                     alert('solo es valido subir archivos en formato pdf.');
                                     return;
                                 }
                             } else {
                                 if (ext.toUpperCase() != '.XLS' && ext.toUpperCase() != '.XLSX') {
                                     alert('solo es valido subir archivos en formato xls para el costo discriminado.');
                                     return;
                                 }
                             }
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
                                     url: '<%=Page.ResolveClientUrl("~/Default.aspx/getAttachmentStatus") %>',
                                     data: '{pAttachment_id:<%# Eval("attachment_id")%>,idproyecto:<%=project_id%>,uploadedfilename:"' + response + '",producer_id:<%=producer_id_additional %>}',
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
             showPersonType();

             $('#<%=personTypeDDL.ClientID %>').change(function () {
                 showPersonType();
             });

             // Solo se ejecuta cuando seleccionamos alguna opcion.
             $('#<%=departamentoDDL.ClientID %>').change(function () {
                 $('#<%=municipioDDL.ClientID %>').empty().append('<option selected="selected" value="0">Cargando...</option>');
                 $.ajax({
                     type: "POST",
                     url: "Default.aspx/obtenerMunicipios",
                     data: '{departamento: "' + $('#<%=departamentoDDL.ClientID%>').val() + '"}',
                     contentType: "application/json; charset=utf-8",
                     dataType: "json",
                     success: OnMunicipiosPopulated,
                     failure: function (response) {
                         alert(response.d);
                     }
                 });
             });

             $(function () {
                 $("#additionalDomesticProducerAccordion").accordion({ header: "h3", collapsible: true });
                 $("#additionalForeignProducerAccordion").accordion({ header: "h3", collapsible: true });
                 $("#additionalDomesticProducerAccordion").accordion("activate", 0);
                 $("#additionalForeignProducerAccordion").accordion("activate", 0);
             });

             /* Cada vez que cambia el valor del select de municipios se almacena el
             valor en una variable oculta para poder recuperar el valor en el momento
             del procesamiento del formulario */
             $('#selectedMunicipio').val($('#<%=municipioDDL.ClientID %>').val());
             $('#<%=municipioDDL.ClientID %>').change(function () {
                 $('#selectedMunicipio').val($('#<%=municipioDDL.ClientID %>').val());
             });

             /* Oculta o muestra los campos de ubicación segun la seleccion del checkbox que
             indica si el productor esta en colombia o fuera de colombia */
             checkLocalizationFields();
             $('#<%=localization_out_of_colombia.ClientID %>').change(function () {
                 checkLocalizationFields();
             });
             <%if (ShowEditForm){ %>
             /* Crea la función de presentación del tooltip en todos los campos */
             $('#<%=personTypeDDL.ClientID %>').tooltip({ bodyHandler: function () { return '<asp:Literal id="tooltip_personTypeDDL" runat="server"></asp:Literal>'; }, showURL: false });
             $('#<%=producer_firstname.ClientID %>').tooltip({ bodyHandler: function () { return '<asp:Literal id="tooltip_producer_firstname" runat="server"></asp:Literal>'; }, showURL: false });
             $('#<%=producer_lastname.ClientID %>').tooltip({ bodyHandler: function () { return '<asp:Literal id="tooltip_producer_lastname" runat="server"></asp:Literal>'; }, showURL: false });
             $('#<%=producer_identification_number.ClientID %>').tooltip({ bodyHandler: function () { return '<asp:Literal id="tooltip_producer_identification_number" runat="server"></asp:Literal>'; }, showURL: false });
             $('#<%=producer_name.ClientID %>').tooltip({ bodyHandler: function () { return '<asp:Literal id="tooltip_producer_name" runat="server"></asp:Literal>'; }, showURL: false });

             $('#<%=producer_firstname_juridica.ClientID %>').tooltip({ bodyHandler: function () { return '<asp:Literal id="tooltip_producer_firstname_juridica" runat="server"></asp:Literal>'; }, showURL: false });
             $('#<%=producer_lastname_juridica.ClientID %>').tooltip({ bodyHandler: function () { return '<asp:Literal id="tooltip_producer_lastname_juridica" runat="server"></asp:Literal>'; }, showURL: false });
             
            
             $('#<%=localization_out_of_colombia.ClientID %>').tooltip({ bodyHandler: function () { return '<asp:Literal id="tooltip_localization_out_of_colombia" runat="server"></asp:Literal>'; }, showURL: false });
             $('#<%=departamentoDDL.ClientID %>').tooltip({ bodyHandler: function () { return '<asp:Literal id="tooltip_departamentoDDL" runat="server"></asp:Literal>'; }, showURL: false });
             $('#<%=municipioDDL.ClientID %>').tooltip({ bodyHandler: function () { return '<asp:Literal id="tooltip_municipioDDL" runat="server"></asp:Literal>'; }, showURL: false });
             $('#<%=producer_country.ClientID %>').tooltip({ bodyHandler: function () { return '<asp:Literal id="tooltip_producer_country" runat="server"></asp:Literal>'; }, showURL: false });
             $('#<%=producer_city.ClientID %>').tooltip({ bodyHandler: function () { return '<asp:Literal id="tooltip_producer_city" runat="server"></asp:Literal>'; }, showURL: false });
             $('#<%=producer_address.ClientID %>').tooltip({ bodyHandler: function () { return '<asp:Literal id="tooltip_producer_address" runat="server"></asp:Literal>'; }, showURL: false });
             $('#<%=producer_phone.ClientID %>').tooltip({ bodyHandler: function () { return '<asp:Literal id="tooltip_producer_phone" runat="server"></asp:Literal>'; }, showURL: false });
             $('#<%=producer_movil.ClientID %>').tooltip({ bodyHandler: function () { return '<asp:Literal id="tooltip_producer_movil" runat="server"></asp:Literal>'; }, showURL: false });
             $('#<%=producer_email.ClientID %>').tooltip({ bodyHandler: function () { return '<asp:Literal id="tooltip_producer_email" runat="server"></asp:Literal>'; }, showURL: false });
             $('#<%=producer_website.ClientID %>').tooltip({ bodyHandler: function () { return '<asp:Literal id="tooltip_producer_website" runat="server"></asp:Literal>'; }, showURL: false });

             /* Agrega la clase user-input a todos los input que diligencia el usuario con el fin de poderlos desactivar y activar */
             <% } %> 

              <%if (ShowEditForm )//solo en el primer estado y en aclaraciones solicitadas permite modificar, de lo contrario
                    //los deja deshabilitados.
                { %>

             $('#<%=personTypeDDL.ClientID %>').addClass("user-input");
             $('#<%=producer_firstname.ClientID %>').addClass("user-input");
             $('#<%=producer_lastname.ClientID %>').addClass("user-input");
             $('#<%=producer_identification_number.ClientID %>').addClass("user-input");
             $('#<%=producer_name.ClientID %>').addClass("user-input");

             $('#<%=producer_nit.ClientID %>').addClass("user-input");
             $('#<%=companyTypeDDL.ClientID %>').addClass("user-input");
             $('#<%=producer_identification_number_juridica.ClientID %>').addClass("user-input");


             
             $('#<%=producer_firstname_juridica.ClientID %>').addClass("user-input");
             $('#<%=producer_lastname_juridica.ClientID %>').addClass("user-input");
            
            
             $('#<%=localization_out_of_colombia.ClientID %>').addClass("user-input");
             $('#<%=departamentoDDL.ClientID %>').addClass("user-input");
             $('#<%=municipioDDL.ClientID %>').addClass("user-input");
             $('#<%=producer_country.ClientID %>').addClass("user-input");
             $('#<%=producer_city.ClientID %>').addClass("user-input");
             $('#<%=producer_address.ClientID %>').addClass("user-input");
             $('#<%=producer_phone.ClientID %>').addClass("user-input");
             $('#<%=producer_movil.ClientID %>').addClass("user-input");
             $('#<%=producer_email.ClientID %>').addClass("user-input");
             $('#<%=producer_website.ClientID %>').addClass("user-input");
             $('#<%=producer_nacionalidad.ClientID %>').addClass("user-input");
             <% } %>

          <%
            if (ShowEditForm && showAdvancedForm)
            { %>
                DisableEnableForm(true, 'desactivar');
            <%
            }
            else if (showAdvancedForm || (project_state_id == 5 && section_state_id != 10) || 
            ((project_state_id == 2 || project_state_id == 3 || project_state_id == 4 
            || project_state_id == 6 || project_state_id == 7 || project_state_id >= 8 )
            && user_role <= 1))
        { %>
                DisableEnableForm(true, 'desactivar');
             <%
            }%>

             $('#loading').hide();
         });
         function OnMunicipiosPopulated(response) {
             PopulateControl(response.d, $("#<%=municipioDDL.ClientID %>"));
         }

         function PopulateControl(list, control) {
             if (list.length > 0) {
                 control.removeAttr("disabled");
                 control.empty();
                 control.append($("<option></option>").val("0").html("Seleccione"));
                 $.each(list, function () {
                     control.append($("<option></option>").val(this['Value']).html(this['Text']));
                 });
             }
             else {
                 control.empty();
                 control.append($("<option></option>").val("0").html("Seleccione"));
             }
         }

         function checkLocalizationFields() {
             if ($('#<%=producer_type.ClientID %>').val() == 1) {
                 var person_type = $('#<%=personTypeDDL.ClientID %>').val();
                 if (person_type == '2') {
                     if ($('#<%=localization_out_of_colombia.ClientID %>').is(':checked')) {
                         $('#<%=localization_out_of_colombia.ClientID %>').attr('checked', false);
                     }
                     $('#localization_out_of_colombia_block').hide("slow");
                 }
                 else {
                     $('#localization_out_of_colombia_block').show("slow");
                 }
             }
             else if ($('#<%=producer_type.ClientID %>').val() == 2) {
                 var person_type = $('#<%=personTypeDDL.ClientID %>').val();
                 if (person_type == '2') {
                     if ($('#<%=localization_out_of_colombia.ClientID %>').is(':checked') == false) {
                         $('#<%=localization_out_of_colombia.ClientID %>').attr('checked', true);
                     }
                     $('#localization_out_of_colombia_block').hide("slow");
                 }
                 else {
                     $('#localization_out_of_colombia_block').show("slow");
                 }
             }
             
             if ($('#<%=localization_out_of_colombia.ClientID %>').is(':checked')) {
                 $('#departamento_field').hide();
                 $('#municipio_field').hide();
                 $('#pais_field').show();
                 $('#ciudad_field').show();
             }
             else {
                 $('#departamento_field').show();
                 $('#municipio_field').show();
                 $('#pais_field').hide();
                 $('#ciudad_field').hide();
             }
         }
     </script>
    <div id='Productor'>
    <%if (ShowEditForm){ %>
     
        <form method="post" action="" name="datos_productor" id="additional_producer">
            <fieldset>
                <legend>Datos del Productor<input type="hidden" id="producer_id" name="producer_id" runat="server"/><input type="hidden" id="producer_type" name="producer_type" runat="server"/></legend>
                <ul>
                    <li>
                        <div class="field_label">Tipo de productor:<span class="required_field_text">*</span></div>
                        <div class="field_input"><asp:DropDownList id="personTypeDDL" runat="server" name="person_type"></asp:DropDownList></div>
                    </li>
                    <li id="persona_natural_field_group">
                        <div class="field_group" >Datos Persona Natural</div>
                        <ul>
                            <li>
                                <div class="field_label">Nombres:<span class="required_field_text">*</span></div>
                                <div class="field_input"><input type="text" name="producer_firstname" id="producer_firstname" runat="server"/></div>
                            </li>
                            <li>
                                <div class="field_label">Apellidos:<span class="required_field_text">*</span></div>
                                <div class="field_input"><input type="text" name="producer_lastname" id="producer_lastname" runat="server"/></div>
                            </li>
                            <% if (typeProducervalue)
                               { %>
                            <li>
                                <div class="field_label">Número de c&eacute;dula de ciudadan&iacute;a:<span class="required_field_text">*</span></div>
                                <div class="field_input"><input type="text" name="producer_identification_number" id="producer_identification_number" runat="server"/></div>
                            </li>
                            <% } %>
                        </ul>
                    </li>
                    <li id="persona_juridica_field_group">
                        <div class="field_group">Datos Persona Jurídica</div>
                        <ul>
                            <li>
                                <div class="field_label">Nombre o Raz&oacute;n Social:<span class="required_field_text">*</span></div>
                                <div class="field_input"><input type="text" name="producer_name" id="producer_name" runat="server"/></div>
                            </li>
                            <% if (typeProducervalue)
                               { %>
                            <li>
                                <div class="field_label">NIT:<span class="required_field_text">*</span></div>
                                <div class="field_input"><input type="text" name="producer_nit" id="producer_nit" runat="server" /></div>
                            </li>
                            <li>
                                <div class="field_label">Tipo de empresa:<span class="required_field_text">*</span></div>
                                <div class="field_input"><asp:DropDownList id="companyTypeDDL" runat="server" name="company_type"></asp:DropDownList></div>
                            </li>
                            <% } %>
                            
                            <li>
                                <div class="field_label">Nombres del representante legal:<span class="required_field_text">*</span></div>
                                <div class="field_input"><input type="text" name="producer_firstname_juridica" id="producer_firstname_juridica" runat="server"/></div>
                            </li>
                            <li>
                                <div class="field_label">Apellidos del representante legal:<span class="required_field_text">*</span></div>
                                <div class="field_input"><input type="text" name="producer_lastname_juridica" id="producer_lastname_juridica" runat="server"/></div>
                            </li>

                             <li>
                                 <asp:Panel runat="server" ID="pnlJuridicoNacional">
                                <div class="field_label">Número de c&eacute;dula de ciudadan&iacute;a del representante legal:<span  class="required_field_text">*</span></div>
                                     </asp:Panel>

                                 
                                 <asp:Panel runat="server" ID="pnlJuridicoExtranjero">
                                <div class="field_label">Número de identificación:<span runat="server" id="divCedulaRep" class="required_field_text">*</span></div>
                                     </asp:Panel>

                                <div class="field_input"><input type="text" name="producer_identification_number_juridica" id="producer_identification_number_juridica" runat="server"/></div>
                            </li>
                           
                        </ul>
                    </li>
                    <li>
                        <div class="field_group">Datos de ubicación</div>
                    </li>
                    <li  id="localization_out_of_colombia_block">
                        <div class="field_label">Marque esta casilla si es fuera de Colombia:</div>
                        <div class="field_input"><input type="checkbox" name="localization_out_of_colombia" id="localization_out_of_colombia" runat="server"/></div>
                    </li>
                    <li id="departamento_field">
                        <div class="field_label">Departamento:<span class="required_field_text">*</span></div>
                        <div class="field_input">
                            <asp:DropDownList ID="departamentoDDL" runat="server" name="departamentoDDL"></asp:DropDownList>
                        </div>
                    </li>
                    <li id="municipio_field">
                        <div class="field_label">Municipio:<span class="required_field_text">*</span><input type="hidden" name="selectedMunicipio" id="selectedMunicipio" value="0"/></div>
                        <div class="field_input">
                            <asp:DropDownList ID="municipioDDL" runat="server" name="municipioDDL"></asp:DropDownList>
                        </div>
                    </li>
                    <li id="pais_field">
                        <div class="field_label">País:<span class="required_field_text">*</span></div>
                        <div class="field_input"><input type="text" name="producer_country" id="producer_country" runat="server"/></div>
                    </li>
                    <li id="ciudad_field">
                        <div class="field_label">Ciudad:<span class="required_field_text">*</span></div>
                        <div class="field_input"><input type="text" name="producer_city" id="producer_city" runat="server"/></div>
                    </li>
                    <li>
                        <div class="field_group">Datos de contacto</div>
                    </li>
                    <li>
                        <div class="field_label">Direcci&oacute;n:<span class="required_field_text">*</span></div>
                        <div class="field_input"><input type="text" name="producer_address" id="producer_address" runat="server"/></div>
                    </li>
                    <li>
                        <div class="field_label">Teléfonos:<span class="required_field_text">*</span></div>
                        <div class="field_input"><input type="text" name="producer_phone" id="producer_phone" runat="server"/></div>
                    </li>
                    <li>
                        <div class="field_label">Celular:<span class="required_field_text">*</span></div>
                        <div class="field_input"><input type="text" name="producer_movil" id="producer_movil" runat="server"/></div>
                    </li>
                    <li>
                        <div class="field_label">Correo electrónico:<span class="required_field_text">*</span></div>
                        <div class="field_input"><input type="text" name="producer_email" id="producer_email" runat="server"/></div>
                    </li>
                    <li>
                        <div class="field_label">Sitio web:</div>
                        <div class="field_input"><input type="text" name="producer_website" id="producer_website" runat="server"/></div>
                    </li>

                    <asp:Panel runat="server" ID="pnlNacionalidad">
                    <li>
                        <div class="field_label">Nacionalidad:</div>
                        <div class="field_input"><input type="text" name="producer_nacionalidad" id="producer_nacionalidad" runat="server"/></div>
                    </li>
                        </asp:Panel>
                    <li>
                        <div id="attachment-box">
                            <asp:Repeater ID="AttachmentRepeater" runat="server">
                                <HeaderTemplate>
                                    <fieldset>
                                        <legend>Adjuntos</legend>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <ul>
                                        <li id="">
                                            <%# Eval("attachment_render") %>
                                        </li>
                                    </ul>
                                </ItemTemplate>
                                <FooterTemplate>
                                    </fieldset></FooterTemplate>
                            </asp:Repeater>
                        </div>
                    </li>
                    <%
          if (!showAdvancedForm )
                      { %>
                                <li>
                                    <asp:Label ID="lblMensajeGuardarProductor" ForeColor="Red" runat="server"></asp:Label>
                                    <div class="field_label"><input type="submit" class="boton"  name="save_producer_info" value="Guardar"   onclick='$("#loading").show();DisableEnableForm(false,"activar")'  /></div>
                                </li>
                    <%
                      } %>
                </ul>
            </fieldset>
        </form>
    <%} %>
    <%else { %>
        <div class="additional_producers_title">Productores Nacionales</div>
      <%if (showDomesticProducers)
        { %>
        
        <asp:Repeater id="AdditionalDomesticProducer" runat="server">
            <HeaderTemplate><div id="additionalDomesticProducerAccordion"></HeaderTemplate>
            <ItemTemplate>
                <h3><a href="#" class="<%# Eval("producer_state_css_class") %>"><%# Eval("producer_title") %></a></h3>
                <div>
                    <ul>
                        <%# Eval("producer_person_type")%>
                        <%# Eval("producer_name")%>
                        <%# Eval("producer_nit")%>
                        <%# Eval("producer_firstname")%>
                        <%# Eval("producer_lastname")%>
                        <%# Eval("producer_identification_number")%>
                        <%# Eval("producer_location")%>
                        <%# Eval("producer_address")%>
                        <%# Eval("producer_phone")%>
                        <%# Eval("producer_movil")%>
                        <%# Eval("producer_email")%>
                        <%# Eval("producer_website")%>
                        <%# Eval("action_form")%>
                    </ul>
                </div>
            </ItemTemplate>
            <FooterTemplate>
                </div>
            </FooterTemplate>
        </asp:Repeater>
        <%} %>
        <%if (showForeignProducers)
        { %>
        <div class="additional_producers_title">Productores Extranjeros</div>
        <asp:Repeater id="AdditionalForeignProducer" runat="server">
            <HeaderTemplate><div id="additionalForeignProducerAccordion"></HeaderTemplate>
            <ItemTemplate>
                <h3><a href="#" class="<%# Eval("producer_state_css_class") %>"><%# Eval("producer_title") %></a></h3>
                <div>
                    <ul>
                        <%# Eval("producer_person_type")%>
                        <%# Eval("producer_name")%>
                        <%# Eval("producer_nit")%>
                        <%# Eval("producer_firstname")%>
                        <%# Eval("producer_lastname")%>
                        <%# Eval("producer_identification_number")%>
                        <%# Eval("producer_location")%>
                        <%# Eval("producer_address")%>
                        <%# Eval("producer_phone")%>
                        <%# Eval("producer_movil")%>
                        <%# Eval("producer_email")%>
                        <%# Eval("producer_website")%>                        
                        <%# Eval("producer_nacionalidad")%>
                        <%# Eval("action_form")%>
                    </ul>
                </div>
            </ItemTemplate>
            <FooterTemplate>
                </div>
            </FooterTemplate>
        </asp:Repeater>
        </div>
        <%} %>
    <%} %>
    <%
        if (showAdvancedForm )
    { %>
        <div id="admin-form">
            <h3><b>Formulario de gestión de la solicitud</b></h3>
            <div id="admin-form-left">
            <%if( !ShowEditForm &&( project_state_id != 6 && project_state_id != 7 && project_state_id != 8)) {%>
                <ul>
                    <li><input type="radio" name="gestion_realizada" id="gestion_realizada_sin_revisar" value="none" runat="server" /><label for="gestion-realizada-sin-revisar">Sin revisar</label></li>
                    <li><input type="radio" name="gestion_realizada" id="gestion_realizada_solicitar_aclaraciones" value="solicitar-aclaraciones" runat="server" /><label for="gestion-realizada-solicitar-aclaraciones">Solicitar aclaraciones</label></li>
                    <li><input type="radio" name="gestion_realizada" id="gestion_realizada_informacion_correcta" value="informacion-correcta"  runat="server" /><label for="gestion-realizada-informacion-correcta">Informaci&oacute;n correcta</label></li>
                </ul>
                <%}
                 if (!ShowEditForm &&(project_state_id == 6 || project_state_id == 7 || project_state_id == 8))
                  { %>
                <fieldset>
                    <ul>
                        <li><input type="radio" name="estado_revision" id="estado_revision_sin_revisar" value="none" runat="server" class="notRevision"/><label for="estado_revision_sin_revisar">Sin revisar</label></li>
                        <li><input type="radio" name="estado_revision" id="estado_revision_revisado" value="revisado" runat="server" /><label for="estado_revision_revisado">No Cumple</label></li>
                        <li><input type="radio" name="estado_revision" id="estado_revision_aprobado" value="aprobado" runat="server" /><label for="estado_revision_aprobado">Cumple</label></li>
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
                                           <b> Solicitud de aclaraciones</b></h3><textarea style="width:620px;min-height:200px;"  name="solicitud_aclaraciones" id="solicitud_aclaraciones" rows="5" cols="40" runat="server"></textarea></li>
                    
                     <%} %>
                     <li><h3>Observaciones</h3><textarea style="width:620px;min-height:200px;"  name="informacion_correcta" id="informacion_correcta" rows="5" cols="40" runat="server"></textarea></li>
                    <li>
                    <%if (ShowEditForm)
                      { %>
                        <div class="field_label"><input type="submit" name="save_producer_info" value="Guardar" onclick='$("#loading").show();DisableEnableForm(false,"activar")' /></div>
                    <%}
                      else
                      { %>
                        <div class="field_label"><input type="submit" name="save_producer_info_read_only" value="Guardar"  onclick='$("#loading").show();DisableEnableForm(false,"activar")' /></div>
                    <% } %>
                    </li>
                </ul>
            </div>
            <div id="admin-form-right">
                <a href="/SolicitudAclaraciones.aspx" target="_blank">Previsualizaci&oacute;n de la solicitud de aclaraciones</a>
            </div>
            <%if (ShowEditForm)
              { %>
            <div id="link">
                <div style='margin:0;text-align:left;text-decoration:underline;cursor:pointer;padding:0 0 20px 0;' onclick='DisableEnableForm(true,"desactivar")'>Desactivar el formulario</div>
            </div>
            <% } %>
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
            <%--        <h3>Formulario de registro de aclaraciones</h3>--%>
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
                        <textarea class="user-input"  maxlength="4000"  style="width:620px;min-height:200px;"  name="producer_clarifications_field" id="producer_clarifications_field" rows="5" cols="80" runat="server"></textarea>
                    </li>

                </ul>
            </div>
              <% } %>    <%if (!ShowEditForm && user_role <=1)
                      { %>
                        <div class="field_label"><input type="submit" name="save_producer_infoc" value="Guardar aclaraciones" onclick='$("#loading").show();DisableEnableForm(false,"activar")' /></div>
                    <%}%>
    
    <% 
    } %>
        </div>
                  
</div>

    
       <uc1:cargando ID="cargando1" runat="server" />

</asp:Content>