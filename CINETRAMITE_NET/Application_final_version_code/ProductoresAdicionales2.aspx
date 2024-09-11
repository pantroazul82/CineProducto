<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProductoresAdicionales2.aspx.cs" 
    Inherits="CineProducto.ProductoresAdicionales2" EnableEventValidation="false" %>
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
<div id="cine" style="width:100% !important;">

    <div id="informacion-contextual">
        <div class="bloque"><strong><asp:Label ID="nombre_proyecto" runat="server"></asp:Label></strong></div>
        <div class="bloque">
            <strong><asp:Label ID="tipo_produccion" runat="server"></asp:Label></strong><br />
            <asp:Label ID="tipo_proyecto" runat="server"></asp:Label>
        </div>
        <div class="bloque"><strong>Productor:</strong><br /><asp:Label ID="nombre_productor" runat="server"></asp:Label></div>
        <div class="bloque">
            <div class="pull-right">
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
            
            <!--<li class="<%--=tab_datos_adjuntos_css_class --%>"><a href="DatosAdjuntos.aspx">Adjuntos<%--=tab_datos_adjuntos_revision_mark_image --%></a></li>-->
            <li class="<%=tab_datos_finalizacion_css_class %>"><a href="Finalizacion.aspx">Finalizaci&oacute;n</a></li>
        </ul>
    </div>
	<!-- End of Nav Div -->
     <script type = "text/javaScript">
                 
        
         function validaNumericos(event) {
             if (event.charCode >= 48 && event.charCode <= 57 ) {
                 return true;
             }
             return false;
         }

         function validaNumericosId(event) {
              <% if (!typeProducervalue)
                 { %>
                   return true;
             <% } %>

             if (event.charCode >= 48 && event.charCode <= 57) {
                 return true;
             }
             return false;
         }

        


         function validaNumericosFloat(event) {
             if (event.charCode >= 48 && event.charCode <= 57 || vent.charCode == 46) {
                 return true;
             }
             return false;
         }

         function verificaPaste(n) {
             var nombreControl = "";
             if (n == "producer_movil") {
                 n = <%=producer_movil.ClientID %>;
             }
             if (n == "producer_phone") {
                 n = <%=producer_phone.ClientID %>;
             }
             if (n == "producer_identification_number") {
                 n = <%=producer_identification_number.ClientID %>;
                  <% if (!typeProducervalue)
                 { %>
                   return true;
                 <% } %>

                }
            if (n == "producer_nit") {
                n = <%=producer_nit.ClientID %>;
                 <% if (!typeProducervalue)
                 { %>
                   return true;
                 <% } %>

            }
             if (n == "producer_identification_number_juridica") {
                 n = <%=producer_identification_number_juridica.ClientID %>;
                  <% if (!typeProducervalue)
                 { %>
                   return true;
                 <% } %>

             }

             permitidos = /[^0-9]/;
             if (permitidos.test(n.value)) {
                 alert("Solo se puedeingresar numeros. Corregir: " + n.value);
                 n.value = "";
                 n.focus();
                 return false;
             }
             return true;

         }
         

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
                         url: 'FileUploadHandler.ashx?upload=start&folder=<%= Page.ResolveClientUrl("~/uploads")%>'+'/<%=project_id %>'+'/<%=producer_id_additional %>'+'&attachment_id=<%# Eval("attachment_id")%>',
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

             // Solo se ejecuta cuando seleccionamos alguna opcion.
             $('#<%=companyTypeDDL.ClientID %>').change(function () {                 
                 $('#save_producer_info').click();

                 ///document.getElementById("save_producer_info").click()
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

             $('#<%=fecha_nacimiento.ClientID %>').datepicker({ dateFormat: "yy-mm-dd", changeMonth: true, changeYear: true, });

             $('#<%=abreviatura.ClientID %>').tooltip({ bodyHandler: function () { return ''; }, showURL: false });
              <% if (!typeProducervalue){ %>
                          
             $('#<%=fecha_nacimiento.ClientID %>').tooltip({ bodyHandler: function () { return ''; }, showURL: false });


               $('#<%=producer_identification_number.ClientID %>').tooltip({ bodyHandler: function () { return 'Este número corresponde a la identificación de persona natural extranjera, verifique que este coincida con el relacionado en el contrato de cooproducción'; }, showURL: false });
             <% } %>   

             $('#<%=producer_name.ClientID %>').tooltip({ bodyHandler: function () { return '<asp:Literal id="tooltip_producer_name" runat="server"></asp:Literal>'; }, showURL: false });
           
             $('#<%=producer_firstname_juridica.ClientID %>').tooltip({ bodyHandler: function () { return '<asp:Literal id="tooltip_producer_firstname_juridica" runat="server"></asp:Literal>'; }, showURL: false });
             $('#<%=producer_lastname_juridica.ClientID %>').tooltip({ bodyHandler: function () { return '<asp:Literal id="tooltip_producer_lastname_juridica" runat="server"></asp:Literal>'; }, showURL: false });


             <% if (!typeProducervalue){ %>
             $('#<%=producer_nit.ClientID %>').tooltip({ bodyHandler: function () { return 'Este número corresponde a la identificación de persona jurídica extranjera, verifique que este coincida con el relacionado en el contrato de coproducción"'; }, showURL: false });
             <% } %>            
             
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
             $('#<%=producer_nit_dig_verif.ClientID %>').addClass("user-input");

             $('#<%=informacion_correcta.ClientID %>').addClass("user-input");
             
             
             $('#<%=companyTypeDDL.ClientID %>').addClass("user-input");
             $('#<%=producer_identification_number_juridica.ClientID %>').addClass("user-input");

             $('#<%=participation_percentage.ClientID %>').addClass("user-input");
             $('#<%=producer_firstname2.ClientID %>').addClass("user-input");
             $('#<%=producer_lastname2.ClientID %>').addClass("user-input");
             $('#<%=cmbEtnia.ClientID %>').addClass("user-input");
             $('#<%=cmbGenero.ClientID %>').addClass("user-input");
             $('#<%=cmbGrupoPoblacional.ClientID %>').addClass("user-input");
             
             
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

             if ($("#hdHabilitarForm").val() == "Activo") {
                 DisableEnableForm(false, 'activar');
                 $("#hdHabilitarForm").val("");
             }

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
                <legend>Datos del Productor  <% if (typeProducervalue)
                               { %>
                                   Nacional
                             <% }else{ %>
                    Extranjero
                    <% } %>
                    <input type="hidden" id="producer_id" name="producer_id" runat="server"/>
                    <input type="hidden" id="producer_type" name="producer_type" runat="server"/>
                </legend>
                <ul>
                    <li>
                        <div class="field_label">Tipo de productor:<span class="required_field_text">*</span></div>
                        <div class="field_input"><asp:DropDownList id="personTypeDDL" runat="server" name="person_type"></asp:DropDownList></div>
                    </li>
                    <li>
                        <div class="field_label">
                            Porcentaje de participación:<span class="required_field_text">*</span></div>
                        <div class="field_input">
                             <input type="text" name="participation_percentage" id="participation_percentage"
                                        runat="server" onkeypress='return validaNumericosFloat(event)' maxlength="5" size="3"/>
                        </div>                       
                    </li>
                    <li id="persona_natural_field_group">
                        <div class="field_group" >Datos Persona Natural</div>
                         <ul>
                            <li>
                                <div class="field_label">
                                    Primer Nombre:<span class="required_field_text">*</span></div>
                                <div class="field_input">
                                    <input type="text" name="producer_firstname" id="producer_firstname" runat="server" /></div>
                            </li>
                            <li>
                                <div class="field_label">
                                    Segundo Nombre:<span class=""></span></div>
                                <div class="field_input">
                                    <input type="text" name="producer_firstname2" id="producer_firstname2" runat="server" /></div>
                            </li>
                            <li>
                                <div class="field_label">
                                    Primer Apellido:<span class="required_field_text">*</span></div>
                                <div class="field_input">
                                    <input type="text" name="producer_lastname" id="producer_lastname" runat="server" /></div>
                            </li>
                             <li>
                                <div class="field_label">
                                   Segundo Apellido:<span class=""></span></div>
                                <div class="field_input">
                                    <input type="text" name="producer_lastname2" id="producer_lastname2" runat="server" /></div>
                            </li>
                            <li>
                                <div class="field_label">
                                    <asp:Label ID="lblCedula" runat="server" Text="" Visible="false"></asp:Label>

                                     <% if (typeProducervalue)
                                       { %>Número de c&eacute;dula de ciudadan&iacute;a:
                                     <% }else{ %>
                                      ID:
                                     <% } %>
                                    
                                    <span class="required_field_text"><asp:Label ID="lblCedulaObligatoria" runat="server" Text="*"></asp:Label></span></div>
                                <div class="field_input">
                                    <input type="text" name="producer_identification_number" id="producer_identification_number"
                                        runat="server" onchange="return verificaPaste('producer_identification_number')" onkeypress='return validaNumericosID(event)' maxlength="40"/></div>
                            </li>

                             <li>
                                <div class="field_label">
                                    Género:<span class="required_field_text">*</span></div>
                                <div class="field_input">
                                    <asp:DropDownList ID="cmbGenero" runat="server" name="cmbGenero" AppendDataBoundItems="True" DataSourceID="SqlDataSourceGenero" DataTextField="nombre" DataValueField="id_genero">
                                            <asp:ListItem Text="Seleccione" Value="0"></asp:ListItem>
                                        </asp:DropDownList>    
                                        <asp:SqlDataSource ID="SqlDataSourceGenero" runat="server" ConnectionString="<%$ ConnectionStrings:cineConnectionString %>" SelectCommand="SELECT [id_genero], [nombre] FROM dboPrd.[genero]"></asp:SqlDataSource>
                            
                                </div>
                            </li>

                              <% if (typeProducervalue)
                                  { %>
                             <li>
                                <div class="field_label">
                                    Fecha de nacimiento:<span class="required_field_text"></span></div>
                                <div class="field_input">                                    
                                    <input type="text" autocomplete="off"  name="fecha_nacimiento" id="fecha_nacimiento" class="user-input" runat="server" />
                                </div>
                            </li>


                              <li>
                                <div class="field_label">
                                    Grupo Poblacional:<span class="required_field_text"></span></div>
                                <div class="field_input">
                                    <asp:DropDownList ID="cmbGrupoPoblacional" runat="server" name="cmbGrupoPoblacional" AppendDataBoundItems="True" DataSourceID="SqlDataSourceGrupoPoblacional" DataTextField="nombre" DataValueField="id_grupo_poblacional">
                                            <asp:ListItem Text="Seleccione" Value="0"></asp:ListItem>
                                        </asp:DropDownList>    
                                        <asp:SqlDataSource ID="SqlDataSourceGrupoPoblacional" runat="server" ConnectionString="<%$ ConnectionStrings:cineConnectionString %>" SelectCommand="SELECT [id_grupo_poblacional], [nombre] FROM dboPrd.[grupo_poblacional]"></asp:SqlDataSource>
                            
                                </div>
                            </li>
                              <% } %>

                             <% if (typeProducervalue)
                                 { %>
                             <li>
                                <div class="field_label">
                                    Grupo étnico:<span class="required_field_text">*</span></div>
                                <div class="field_input">
                                     <asp:DropDownList ID="cmbEtnia" runat="server" name="cmbEtnia" AppendDataBoundItems="True" DataSourceID="SqlDataSourceEtnia" DataTextField="nombre" DataValueField="id_etnia">
                                            <asp:ListItem Text="Seleccione" Value="0"></asp:ListItem>
                                        </asp:DropDownList>    
                                        <asp:SqlDataSource ID="SqlDataSourceEtnia" runat="server" ConnectionString="<%$ ConnectionStrings:cineConnectionString %>" SelectCommand="SELECT [id_etnia], [nombre] FROM dboPrd.[etnia]"></asp:SqlDataSource>
                              
                                    </div>
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
                                <div class="field_label">
                                    Abreviatura:<span class="required_field_text"></span></div>
                                <div class="field_input">
                                    <input type="text" name="abreviatura" id="abreviatura" runat="server" class="user-input"/></div>
                            </li>

                             <% } %>
                            <li>
                                <div class="field_label">
                                    <asp:Label ID="lblNit" runat="server" Text="NIT:" visible="false"></asp:Label>
                                    <% if (typeProducervalue)
                                       { %>Nit:
                                     <% }else{ %>
                                      ID:
                                     <% } %>
                                    <span class="required_field_text"><asp:Label ID="lblNitObligatorio" runat="server" Text="*"></asp:Label></span></div>
                                <div class="field_input"><input type="text" name="producer_nit"  id="producer_nit"  runat="server" onchange="return verificaPaste('producer_nit')" onkeypress='return validaNumericosId(event)' maxlength="50"/>
                                     <% if (typeProducervalue)
                                    { %>
                                    -<input type="text" name="producer_nit_dig_verif" id="producer_nit_dig_verif" runat="server" onkeypress='return validaNumericos(event)' maxlength="1" style="width : 25px;"/>
                                    <% } %>
                                    
                                </div>
                            </li>
                            <li>
                                <div class="field_label">Tipo de empresa:<span class="required_field_text">*</span></div>
                                <div class="field_input">
                                    <asp:DropDownList id="companyTypeDDL" runat="server" name="companyTypeDDL" ></asp:DropDownList>                                    
                                </div>
                            </li>
                           
                            
                            <li>
                                <div class="field_label">Primer Nombre del Rep. Legal:<span class="required_field_text">*</span></div>
                                <div class="field_input"><input type="text" name="producer_firstname_juridica" id="producer_firstname_juridica" runat="server"/></div>
                            </li>
                             <li>
                                <div class="field_label">Segundo Nombre del Rep Legal:<span class="required_field_text"></span></div>
                                <div class="field_input"><input type="text" name="producer_firstname_juridica2" id="producer_firstname_juridica2" runat="server"/></div>
                            </li>
                            <li>
                                <div class="field_label">Primer Apellido del Rep. Legal:<span class="required_field_text">*</span></div>
                                <div class="field_input"><input type="text" name="producer_lastname_juridica" id="producer_lastname_juridica" runat="server"/></div>
                            </li>
                            <li>
                                <div class="field_label">Segundo Apellido del Rep. Legal:<span class="required_field_text"></span></div>
                                <div class="field_input"><input type="text" name="producer_lastname_juridica2" id="producer_lastname_juridica2" runat="server"/></div>
                            </li>

                             <li>
                                 <asp:Panel runat="server" ID="pnlJuridicoNacional">
                                <div class="field_label">Número de c&eacute;dula de ciudadan&iacute;a del representante legal:<span  class="required_field_text">*</span></div>
                                     </asp:Panel>

                                 
                                 <asp:Panel runat="server" ID="pnlJuridicoExtranjero">
                                <div class="field_label">ID Representante Legal:<span runat="server" id="divCedulaRep" class="required_field_text">*</span></div>
                                     </asp:Panel>

                                <div class="field_input"><input type="text" name="producer_identification_number_juridica" id="producer_identification_number_juridica" runat="server" onchange="return verificaPaste('producer_identification_number_juridica')"  onkeypress='return validaNumericosID(event)' maxlength="50"/></div>
                            </li>

                             <% if (typeProducervalue)
                                 { %>
                             
                            <li>
                                <div class="field_label">Número de cedula de ciudadania del Rep. Legal Suplente:<span class="required_field_text"></span></div>
                                <div class="field_input"><input type="text" name="num_id_sup" id="num_id_sup" runat="server"/></div>
                            </li>
                            <li>
                                <div class="field_label">Primer Nombre del Rep. Legal Suplente:<span class="required_field_text"></span></div>
                                <div class="field_input"><input type="text" name="primer_nombre_sup" id="primer_nombre_sup" runat="server"/></div>
                            </li>
                             <li>
                                <div class="field_label">Segundo Nombre del Rep Legal Suplente:<span class="required_field_text"></span></div>
                                <div class="field_input"><input type="text" name="segundo_nombre_sup" id="segundo_nombre_sup" runat="server"/></div>
                            </li>
                            <li>
                                <div class="field_label">Primer Apellido del Rep. Legal Suplente:<span class="required_field_text"></span></div>
                                <div class="field_input"><input type="text" name="primer_apellido_sup" id="primer_apellido_sup" runat="server"/></div>
                            </li>
                            <li>
                                <div class="field_label">Segundo Apellido del Rep. Legal Suplente:<span class="required_field_text"></span></div>
                                <div class="field_input"><input type="text" name="segundo_apellido_sup" id="segundo_apellido_sup" runat="server"/></div>
                            </li>
                             <% } %>
                           
                        </ul>
                    </li>
                    <li>
                        <div class="field_group">Datos de origen/Nacimiento</div>
                    </li>

                    <!--li  id="localization_out_of_colombia_block">
                        <div class="field_label">Marque esta casilla si es fuera de Colombia:</div>
                        <div class="field_input"><input type="checkbox" name="localization_out_of_colombia" id="localization_out_of_colombia" runat="server"/></div>
                    </li-->

                     <% if (typeProducervalue)
                         { %>
                    <li id="departamento_field2">
                        <div class="field_label">Departamento:<span class="required_field_text">*</span></div>
                        <div class="field_input">
                            <asp:DropDownList ID="departamentoDDL" runat="server" name="departamentoDDL"></asp:DropDownList>
                        </div>
                    </li>
                    <li id="municipio_field2">
                        <div class="field_label">Municipio:<span class="required_field_text">*</span><input type="hidden" name="selectedMunicipio" id="selectedMunicipio" value="0"/></div>
                        <div class="field_input">
                            <asp:DropDownList ID="municipioDDL" runat="server" name="municipioDDL"></asp:DropDownList>
                        </div>
                    </li>
                     <% }else { %>
                    <li id="pais_field2">
                        <div class="field_label">País:<span class="required_field_text">*</span></div>
                        <div class="field_input"><input type="text" name="producer_country" id="producer_country" runat="server"/></div>
                    </li>
                    <li id="ciudad_field2">
                        <div class="field_label">Ciudad:<span class="required_field_text">*</span></div>
                        <div class="field_input"><input type="text" name="producer_city" id="producer_city" runat="server"/></div>
                    </li>
                    <% } %>
                    <li>
                        <div class="field_group">Datos de contacto</div>
                    </li>

                    <% if (typeProducervalue)
                        { %>
                    <!--li>
                        <div class="field_label">Direcci&oacute;n:<span class="required_field_text">*</span></div>
                        <div class="field_input"><input type="text" name="producer_address" id="producer_address" runat="server"/></div>
                    </li-->
                    <% } %>
                    <li>
                        <div class="field_label">Teléfono:<span class="required_field_text">*</span></div>
                        <div class="field_input"><input type="text" name="producer_phone" id="producer_phone" runat="server" onchange="return verificaPaste('producer_phone')" onkeypress='return validaNumericos(event)' maxlength="50"/></div>
                    </li>
                    <li>
                        <div class="field_label">Teléfono alternativo:<span class="required_field_text">*</span></div>
                        <div class="field_input"><input type="text" name="producer_movil" id="producer_movil" runat="server" onchange="return verificaPaste('producer_movil')" onkeypress='return validaNumericos(event)' maxlength="50"/></div>
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

                    <div class="alert alert-warning" style="position: fixed; right: 50px;z-index:9999; margin-top: 30px;  min-height: 60px;    width: 400px;    text-align: center;    word-wrap: break-word;   >
                       <div class="alert alert-warning">
                        <div class="alert alert-warning" role="alert">
                                        <p style="font-size:11px">Recuerde! una vez guardado el Productor o al modificar el tipo de empresa, deberá modificar este registro para adjuntar los archivos.
                                            <div class="field_label"><input type="submit" class="boton" ID="save_producer_info"  name="save_producer_info" value="Guardar"   onclick='$("#loading").show();DisableEnableForm(false,"activar")'  /></div>
                                       <br /><br />
                                        </p>
                                    </div>
                           <!--
                                    <asp:Label ID="lblMensajeGuardarProductor" ForeColor="Red" runat="server"></asp:Label>
                           -->
                                    
                            
                           </div>
                    </div>
                                
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
        
        <asp:Repeater id="AdditionalDomesticProducer" runat="server"  >
            <HeaderTemplate><div id="additionalDomesticProducerAccordion"></HeaderTemplate>
            <ItemTemplate>
                <h3><a href="#" class="<%# Eval("producer_state_css_class") %>"><%# Eval("producer_title") %></a></h3>
                <div>
                    <ul>
                        <%# Eval("producer_person_type")%>
                        <%# Eval("participation_percentage")%>
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
                        <li><br /></li>
                        <li><br /></li>
                        <li><br /></li>
                        <li><br /></li>
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
                        <%# Eval("participation_percentage")%>
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
                        <li><br /></li>
                        <li><br /></li>
                        <li><br /></li>
                        <li><br /></li>
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
                 <%if(project_state_id == 9 ) {%>
                                <ul>                                                                        
                                    <li>
                                        <label>Información corecta!</label></li>                                  

                                </ul>
                                <%}%>

            <%if( !ShowEditForm &&( project_state_id != 6 && project_state_id != 7 && project_state_id != 8 && project_state_id != 9)) {%>
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
                     <li><h3>Observaciones</h3>
                         
                         <textarea style="width:620px;min-height:200px;" class=""  name="informacion_correcta" id="informacion_correcta" rows="5" cols="40" runat="server"></textarea></li>
                    
                    <li>
                        <%if (project_state_id != 9 && project_state_id != 10) { %>

                            <%if (ShowEditForm)
                                { %>                         
                                    <div class="field_label"><input type="submit" name="save_producer_info" value="Guardar" onclick='$("#loading").show();DisableEnableForm(false,"activar")' /></div>
                        
                            <%}
                              else
                              { %>
                                <div class="field_label"><input type="submit" name="save_producer_info_read_only" value="Guardar"  onclick='$("#loading").show();DisableEnableForm(false,"activar")' /></div>
                            <% } %>

                         <%}%>
                    </li>
                </ul>
            </div>
            <div id="admin-form-right">
                <a href="/SolicitudAclaraciones.aspx" target="_blank">Previsualizaci&oacute;n de la solicitud de aclaraciones</a>
            </div>
            <asp:HiddenField runat="server" ClientIDMode="Static" ID="hdHabilitarForm" Value=""></asp:HiddenField>
            <%if (ShowEditForm)
              { %>
                    <%if (project_state_id != 9 && project_state_id != 10){ %>
                    <div id="link">
                        <div style='margin:0;text-align:left;text-decoration:underline;cursor:pointer;padding:0 0 20px 0;' onclick='DisableEnableForm(true,"desactivar")'>Desactivar el formulario</div>
                    </div>
                    <%} %>
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
              <% } %>    <%if (!ShowEditForm && user_role <=1)  { %>

                                    <%if (project_state_id != 9 && project_state_id != 10)                { %>
                                                <div class="field_label"><input type="submit" name="save_producer_infoc" value="Guardar aclaraciones" onclick='$("#loading").show();DisableEnableForm(false,"activar")' /></div>
                                    <%} %>
             <%}%>
    
    <% 
    } %>
        </div>
                  
</div>

    
       <uc1:cargando ID="cargando1" runat="server" />

</asp:Content>