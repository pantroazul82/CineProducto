<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DatosPersonal3.aspx.cs" Inherits="CineProducto.DatosPersonal3" %>

<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.13.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register src="usercontrols/cargando.ascx" tagname="cargando" tagprefix="uc1" %>


<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="TitleContent" runat="server" ContentPlaceHolderID="TitleContent">Trámite Reconocimiento Como Obra Nacional - Datos de Personal - Mincultura</asp:Content>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <script>
        function OnCloseUp(s, e) {
            __doPostBack('', "RefreshPage");
        }

    </script>
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
            
            
            autosize($('#<%=informacion_correcta.ClientID %>'));
            autosize($('#<%=producer_clarifications_field.ClientID %>'));

            habilitarTracker();              

           
           
            $('#change_has_domestic_director_link').click(function () {
                $('#loading').show();
                $('#change_has_domestic_director').val("1");
                $('#aspnetForm').submit();
               // $('#loading').hide();
            });

            /* Agrega la clase user-input a todos los input que diligencia el usuario con el fin de poderlos desactivar y activar */
            
            
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
               <div class="pull-right" ><asp:Label ID="opciones_adicionales" runat="server"></asp:Label>
                   <asp:Label runat="server" ID="lblCodProyecto" Visible="false"></asp:Label></div>
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

  <asp:HiddenField runat="server" ClientIDMode="Static" ID="HiddenField1" Value=""></asp:HiddenField>
    <div id='Personal'>
        <asp:HiddenField runat="server" ClientIDMode="Static" ID="hdHabilitarForm" Value=""></asp:HiddenField>
        <p>Cuando varias personas se hayan desempeñado en un mismo cargo, solo incluya la información y los documentos de quien figura en primer lugar en el respectivo crédito. Cada cargo es válido solo una vez.
          </p>
        <input style="min-width:300px !important" type="hidden" name="change_has_domestic_director" id="change_has_domestic_director" value="0" />
        <input style="min-width:300px !important" type="hidden" name="change_staff_option" id="change_staff_option" value="0" />
        <input style="min-width:300px !important" type="hidden" name="add_optional_position" id="add_optional_position" value="0" />
        <input style="min-width:300px !important" type="hidden" name="remove_optional_position" id="remove_optional_position" value="0" />
          <ul>

                   
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
                                                    <%if (project_state_id != 9 && project_state_id != 10)        { %>
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








    <br /><br /><br /><br />





      <table class="table table-striped table-hover dataTable align-content-center" style="width:96%">                                              
                
    </table>
<div class="table table-striped table-hover dataTable align-content-center" style="width:96%">                                              
  <br />
                            <div class="form-horizontal center" style="width:100%;padding:5px">   

                                 <div class ="row">
                                    <div class="form-group col-sm-12">
                                        <label for="cmbProductorNacional" class="control-label"><h3>¿Tiene Director Colombiano?</h3></label>
                                                                               
                                        
                                    </div>                               
                                    
                                </div>
                                <div class ="row">
                                    <div class="form-group col-sm-2">
                                            <asp:DropDownList runat="server" AutoPostBack="true" AppendDataBoundItems="true" CssClass="form-control" ID="cmbProductorNacional" OnSelectedIndexChanged="cmbProductorNacional_SelectedIndexChanged" 
                                             onfocus="this.setAttribute('PrvSelectedValue',this.value);"  onchange="if(confirm('Se eliminaran los datos almacenados del personal. Por favor confirme?')==false){ this.value=this.getAttribute('PrvSelectedValue');return false; }" >
                                             <asp:ListItem Text="Seleccione..." Value=""></asp:ListItem>
                                             <asp:ListItem Text="Si" Value="1"></asp:ListItem>
                                             <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                         </asp:DropDownList>                              
                                        
                                    </div>                                
                                    
                                </div>
                                </div>
    </div>

    
     <div class="additional_producers_title"><h3>Personal de la obra</h3></div>

        <dx:ASPxGridView ID="grdPersonal" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDSExt">
            <SettingsPager PageSize="50"></SettingsPager>  
            <Toolbars>
                <dx:GridViewToolbar Position="Bottom" EnableAdaptivity="true" ItemAlign="Left" >
                    <Items>
                        <dx:GridViewToolbarItem Command="ExportToXlsx" Text="Exportar a Excel" Enabled="true" ItemStyle-HorizontalAlign="Left" />                        
                    </Items>
                </dx:GridViewToolbar>
            </Toolbars>
            <SettingsExport EnableClientSideExportAPI="true" ExcelExportMode="WYSIWYG" />
            <Columns>
                <dx:GridViewDataColumn Caption="#" Width="60px" VisibleIndex="0">
                    <DataItemTemplate>       
                        <%if ((project_state_id == 1 || (project_state_id == 5  && ProductorPuedeEditar == true ) ) || (project_state_id !=9 && project_state_id != 10 && (user_role == 4 || user_role == 2  || user_role == 5 )) || ( fecha_subsanacion != string.Empty && subsanado == true && ProductorPuedeEditar == true)){ %>
                        <!--Inicio popup-->
                        <asp:LinkButton ID="btnShowModal1" OnClick="btShowModal_Click"  CommandArgument='<%# Eval("project_staff_id") %>'  runat="server" Text="Editar" AutoPostBack="true" UseSubmitBehavior="false" Width="20%">
                        </asp:LinkButton>
                        <!--fin pupup--> 
                        <br />
                        <% } %>
                         <!--Inicio popup-->
                        <asp:LinkButton ID="btnShowModalVer1" OnClick="btShowModalVer_Click"  CommandArgument='<%# Eval("project_staff_id") %>'  runat="server" Text="Ver" AutoPostBack="true" UseSubmitBehavior="false" Width="20%">
                        </asp:LinkButton>
                        <!--fin pupup--> 
                    </DataItemTemplate>
                </dx:GridViewDataColumn>                
                <dx:GridViewDataTextColumn FieldName="TipoCargo" Caption="Tipo de Cargo" Width="100px" ReadOnly="True" VisibleIndex="0">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="Position" Caption="Cargo" Width="100px" VisibleIndex="1">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="Nombre" Width="150px" ReadOnly="True" VisibleIndex="2">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="tipo_identificacion" Width="100px" Caption="Tipo Identificación"  ReadOnly="True" VisibleIndex="3">
                </dx:GridViewDataTextColumn>                
                <dx:GridViewDataTextColumn FieldName="Identificacion" Width="100px" VisibleIndex="4">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="fecha_nacimiento" Caption="Fecha Nacimiento" Width="150px" VisibleIndex="4">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="Telefono" Caption="Telefono" Width="100px" VisibleIndex="5">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="Email" Caption="Email" Width="100px" VisibleIndex="6">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="genero" Width="100px" Caption="Genero" VisibleIndex="7">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataDateColumn FieldName="etnia" Width="100px" Caption="Etnia"  VisibleIndex="8">
                </dx:GridViewDataDateColumn>
                <dx:GridViewDataTextColumn FieldName="grupo_poblacional" Caption="Grupo Poblacional" Width="100px" VisibleIndex="9">
                </dx:GridViewDataTextColumn> 
                <dx:GridViewDataColumn Caption="#" Width="60px" VisibleIndex="10">
                    <DataItemTemplate>       
                       <asp:Image src="images/aprobado.png" runat="server" Width="17px" ID="imgAdjuntosAprobados" Visible= '<%# EstanAprobadosAdjuntos(Eval("project_staff_id"))  %>' />
                        <asp:Image src="images/error.png" runat="server" Width="17px" ID="imgAdjuntosRechazados"  Visible= '<%# EstanRechazadosAdjuntos(Eval("project_staff_id"))  %>'/>
                    </DataItemTemplate>
                </dx:GridViewDataColumn> 
            </Columns>
    </dx:ASPxGridView>        
        <asp:SqlDataSource ID="SqlDSExt" runat="server" ConnectionString="<%$ ConnectionStrings:cineConnectionString %>" SelectCommand="
set dateformat dmy
select * from (

select
project_staff_id,
case when position.position_father_id = 0 then position.position_name else p2.position_name end as TipoCargo,
case when position.position_father_id = 0 then '' else position.position_name end as Position,
case when position.position_father_id = 0 then position.position_id else p2.position_id end pid,
ROW_NUMBER() OVER (PARTITION BY case when position.position_father_id = 0 then position.position_name else p2.position_name end order by project_staff_id) AS cnt 
,st.staff_option_detail_quantity,
project_staff_firstname + ' ' + isnull(project_staff_firstname2,'')+' '+project_staff_lastname+' '+project_staff_lastname2  as 'Nombre',
identification_type.identification_type_name as tipo_identificacion,
[project_staff_identification_number] 'Identificacion',
[project_staff_city] 'Ciudad' ,
fecha_nacimiento,
project_staff_address 'Direccion', 
project_staff_phone 'Telefono', 
project_staff_movil 'Celular', 
project_staff_email 'Email', 
project_staff.project_staff_project_id,
genero.nombre as genero,
etnia.nombre as etnia,
grupo_poblacional.nombre as grupo_poblacional
from dboPrd.project_staff  
left join dboPrd.position on position.position_id =  project_staff.project_staff_position_id
left join dboPrd.position p2 on position.position_father_id =  p2.position_id
left join dboPrd.genero on genero.id_genero = project_staff.id_genero
left join dboPrd.etnia on etnia.id_etnia = project_staff.id_etnia
left join dboPrd.grupo_poblacional on grupo_poblacional.id_grupo_poblacional = project_staff.id_grupo_poblacional
left join dboPrd.identification_type on identification_type.identification_type_id = project_staff.identification_type_id
join(
select p.project_id,position.position_id,
position.position_name,staff_option_detail.[staff_option_detail_quantity]
 from dboPrd.project p 
join dboPrd.staff_option on staff_option.project_type_id = p.project_type_id and
staff_option.project_type_id = p.project_type_id and staff_option.project_genre_id = p.project_genre_id and 
staff_option.staff_option_has_domestic_director = p.project_has_domestic_director and 
p.project_percentage between staff_option.staff_option_percentage_init and  staff_option.staff_option_percentage_end
and staff_option.staff_option_deleted=0
join dboPrd.staff_option_detail on staff_option_detail.staff_option_id= staff_option.staff_option_id and staff_option_detail.version = p.version and staff_option_detail.staff_option_detail_deleted=0
join dboPrd.position on position.position_id= staff_option_detail.position_id

) st on st.project_id = project_staff.project_staff_project_id and st.position_id = (case when position.position_father_id = 0 then position.position_id else p2.position_id end)

WHERE project_staff.project_staff_project_id= @pIdProjectExtran 

)ss where ss.cnt<= staff_option_detail_quantity">
            <SelectParameters>
                <asp:ControlParameter ControlID="lblCodProyecto" DefaultValue="0" Name="pIdProjectExtran" PropertyName="Text" />
            </SelectParameters>
    </asp:SqlDataSource>





</div>




    <%if (project_state_id != 1 && project_state_id != 9 && project_state_id != 10 && (user_role == 4 || user_role == 3  || user_role == 2 || user_role == 5)){ %>

<asp:Panel runat="server" ID="pnAclaraciones">

    <h3><b>Formulario de gestión de la solicitud</b></h3>
    
     <div class="form-horizontal center" style="width:100%;padding:5px">  
          <div class ="row">
              <div class="form-group col-sm-2">
                  

                  

                 <% if (project_state_id == 6 || project_state_id == 7 || project_state_id == 8)
                     {%>
                   <dx:ASPxRadioButtonList ID="radiobRevisarInfoMark" AutoPostBack="true" runat="server"  TextWrap="false" TextAlign="Right" RepeatDirection="Vertical" OnSelectedIndexChanged="radiobRevisarInfoMark_SelectedIndexChanged">                      
                      <items>
                          <dx:ListEditItem Value="1" Text="Sin Revisar" />
                          <dx:ListEditItem Value="2" Text="No Cumple" />
                          <dx:ListEditItem Value="3" Text="Cumple" />
                      </items>
                  </dx:ASPxRadioButtonList>
                  <%} else {%>
                    <dx:ASPxRadioButtonList ID="radiobRevisarInfo" AutoPostBack="true" runat="server" TextWrap="false" TextAlign="Right" RepeatDirection="Vertical" OnSelectedIndexChanged="radiobRevisarInfo_SelectedIndexChanged">                      
                      <items>
                          <dx:ListEditItem Value="1" Text="Sin Revisar" />
                          <dx:ListEditItem Value="2" Text="Solicitar aclaraciones" />
                          <dx:ListEditItem Value="3" Text="Información correcta" />
                      </items>
                  </dx:ASPxRadioButtonList>
                  <% }%>

                </div> 
              <div class="form-group col-sm-8">
                                <div class ="row">
                                    <div class="form-group col-sm-12">
                                        <label for="txtAclaraciones" class="control-label">Solicitud de Aclaraciones:</label>
                                         <asp:TextBox CssClass="form-control" TextMode="MultiLine" Rows="5" runat="server" ID="txtAclaraciones"></asp:TextBox>
                                    </div> 
                                </div> 
                                <div class ="row">
                                    <div class="form-group col-sm-12">                                        
                                         <label for="txtObservaciones" class="control-label">Observaciones:</label>
                                         <asp:TextBox CssClass="form-control" TextMode="MultiLine" Rows="5"  runat="server" ID="txtObservaciones"></asp:TextBox>
                                        </div>
                                </div>
                                <div class="row">
                                     <div class="form-group col-sm-3"></div>
                                    <div class="form-group col-sm-4">
                                        <asp:Button runat="server" Text="Guardar" CssClass="form-control alert-primary" ID="btnGuardar" OnClick="btnGuardar_Click" />
                                    </div>
                                </div>
                  </div>

                <div class="form-group col-sm-2">
                    <br />
                    <a href="/SolicitudAclaraciones.aspx" target="_blank">Previsualizaci&oacute;n de la solicitud de aclaraciones</a>
                </div>
              </div>
         </div>

</asp:Panel>


         <% } %>






        <%
        if (false)
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


         
    <asp:Panel runat="server" ID="PanelAclaracionesProductor">

    <h3><b>Solicitud de aclaraciones de la solicitud</b></h3>
    
     <div class="form-horizontal center" style="width:100%;padding:5px">  
          <div class ="row">
              <div class="form-group col-sm-2">                  
                  
                </div> 
              <div class="form-group col-sm-8">
                                <div class ="row">
                                    <div class="form-group col-sm-12">
                                        <label for="txtAclaraciones" class="control-label">Solicitud de Aclaraciones:</label>
                                        <br />
                                         <asp:label runat="server" ID="lblAclaracionesSolicitadas"></asp:label>
                                    </div> 
                                </div> 
                                <div class ="row">
                                    <div class="form-group col-sm-12">                                        
                                         <label for="txtAclaracionesProductor" class="control-label">Escriba sus aclaraciones a continuación:</label>
                                         <asp:TextBox CssClass="form-control" TextMode="MultiLine" Rows="5"  runat="server" ID="txtAclaracionesProductor"></asp:TextBox>
                                        </div>
                                </div>
                                <div class="row">
                                     <div class="form-group col-sm-3"></div>
                                    <div class="form-group col-sm-4">
                                        <asp:Button runat="server" Text="Guardar" CssClass="form-control alert-primary" ID="btnGuardarAclaracionesProductor" OnClick="btnGuardarAclaracionesProductor_Click" />
                                    </div>
                                </div>
                  </div>

                <div class="form-group col-sm-2">
                    <br />
                    <a href="/SolicitudAclaraciones.aspx" target="_blank">Previsualizaci&oacute;n de la solicitud de aclaraciones</a>
                </div>
              </div>
         </div>

</asp:Panel>

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

            



    <!--inicio popup-->

     <dx:ASPxPopupControl ID="pcEditFormProducer" runat="server" Width="900px" Height="700" CloseAction="CloseButton" CloseOnEscape="true" Modal="True"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="pcEditFormProducer"
        HeaderText="Actualizar Personal" ScrollBars="Both" ShowPageScrollbarWhenModal="true" AllowDragging="True" PopupAnimationType="Slide" EnableViewState="False" AutoUpdatePosition="true">
         <ClientSideEvents CloseUp="OnCloseUp" />
        <ContentCollection>

            <dx:PopupControlContentControl runat="server">
                <dx:ASPxPanel ID="ASPxPanel1" runat="server" DefaultButton="btOK">
                    <PanelCollection>
                        <dx:PanelContent runat="server">

                            
                        </dx:PanelContent>



                    </PanelCollection>
                </dx:ASPxPanel>

            </dx:PopupControlContentControl>
        </ContentCollection>
        <ContentStyle>
            <Paddings PaddingBottom="5px" />
        </ContentStyle>
    </dx:ASPxPopupControl>





    <!--inicio popup-->     
     <dx:ASPxPopupControl ID="pcVerCoProductor" runat="server" Width="800px" Height="600px" CloseAction="CloseButton" CloseOnEscape="true" Modal="True"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="pcVerProductor"
        HeaderText="Informacion del Personal" ScrollBars="Vertical" AllowDragging="True" PopupAnimationType="Slide" EnableViewState="False" AutoUpdatePosition="true">         
        <ContentCollection>
            <dx:PopupControlContentControl runat="server">
                <dx:ASPxPanel ID="ASPxPanel2" runat="server" DefaultButton="btOK">
                    <PanelCollection>
                        <dx:PanelContent runat="server">
                        </dx:PanelContent>         
                    </PanelCollection>
                </dx:ASPxPanel>
            </dx:PopupControlContentControl>
        </ContentCollection>
        <ContentStyle>
            <Paddings PaddingBottom="5px" />
        </ContentStyle>
    </dx:ASPxPopupControl>

</asp:Content>

