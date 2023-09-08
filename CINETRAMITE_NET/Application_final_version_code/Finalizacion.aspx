<%@ Page Title="Formulario de finalizaci&oacute;n" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" ValidateRequest="false"
    CodeBehind="Finalizacion.aspx.cs" Inherits="CineProducto.Finalizacion" EnableEventValidation="false" %>
<%@ Register src="usercontrols/cargando.ascx" tagname="cargando" tagprefix="uc1" %>


<asp:Content ID="TitleContent" runat="server" ContentPlaceHolderID="TitleContent">Trámite Reconocimiento Como Obra Nacional - Finalización - Mincultura</asp:Content>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
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

    <script type = "text/javaScript">
        $(document).ready(function () {
            scroll();
            
            autosize($('#<%=comentarios_adicionales.ClientID %>'));
            autosize($('#<%=schedulefilmview_result.ClientID %>'));
            autosize($('#<%=txtComplementoCartaAclaraciones.ClientID %>'));
            autosize($('#<%=txtRespuestaVisualizacion.ClientID %>'));
            autosize($('#<%=txtRespuestaVisualizacion2.ClientID %>'));
            $('#loading').hide();

            $(function () {

                $('#FileUpload_formulario_solicitud').fileupload({
                    url: 'FileUploadHandler.ashx?upload=start&folder=<%= Page.ResolveClientUrl("~/uploads")%>' + '/<%=project_id %>/'+ '&attachment_id=0',
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

                        if (cnt > 1) {
                            alert('Solo debe seleccionar un archivo');
                            return;
                        }
                        
                            if (ext.toUpperCase() != '.PDF') {
                                alert('solo es valido subir archivos en formato pdf.');
                                return;
                            }
                        //   console.log('add', data);
                        $('#progressbar').show();
                        //    $('#progressbar1').show();
                        data.submit();
                    },
                    progress: function (e, data) {
                        var progress = parseInt(data.loaded / data.total * 100, 10);
                        $('#progressbar').css('width', progress + '%');
                    },
                    success: function (response, status) {
                        $('#progressbar').hide();
                        $('#progressbar div').css('width', '0%');

                        if (response.indexOf("Error") >= 0) {
                            alert(response);
                        } else {

                            $.ajax({
                                type: 'POST',
                                url: '<%=Page.ResolveClientUrl("~/Default.aspx/proccessRequestForm") %>',
                                data: '{project_id:<%=project_id%>,filename:"' + response + '"}',
                                contentType: 'application/json; charset=utf-8',
                                dataType: 'json',
                                success: function (msg) {
                                    // Replace the div's content with the page method's return.
                                    $('#name_formulario_solicitud').html(msg.d);
                                }
                            });
                           // $('#aspnetForm').submit();
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

            $('#deshacerenvio').click(function () {
                if (confirm('PRECAUCION: ESTA ACCIÓN DEVUELVE LA SOLICITUD AL PRODUCTOR. ¿DESEA CONTINUAR?')) {
                    $('#specialaction').val("undorequest");
                    $('#aspnetForm').submit();
                }
            });
           
            $('#pasaraeditor_div').click(function () {
                if (confirm('PASARÁ LA SOLICITUD PARA REVISIÓN DEL EDITOR. ¿DESEA CONTINUAR?')) {
                    $('#pasaraeditor_field').val("1");
                    $('#loading').show();
                    $('#aspnetForm').submit();
                }
            });
            $('#back_to_revisor_div').click(function () {
                if (confirm('VA A REGRESAR LA SOLICITUD AL REVISOR. ¿DESEA CONTINUAR?')) {
                    $('#back_to_revisor').val("1");
                    $('#loading').show();
                    $('#aspnetForm').submit();
                }
            });
            $('#back_to_revisor2_div').click(function () {
                if (confirm('VA A REGRESAR LA SOLICITUD AL REVISOR. ¿DESEA CONTINUAR?')) {
                    $('#back_to_revisor2').val("1");
                    $('#loading').show();
                    $('#aspnetForm').submit();
                }
            });
            $('#back_to_editor_div').click(function () {
                if (confirm('VA A REGRESAR LA SOLICITUD AL EDITOR. ¿DESEA CONTINUAR?')) {
                    $('#back_to_editor').val("1");
                    $('#loading').show();
                    $('#aspnetForm').submit();
                }
            });
            $('#back_to_editor2_div').click(function () {
                if (confirm('VA A REGRESAR LA SOLICITUD AL EDITOR. ¿DESEA CONTINUAR?')) {
                    $('#back_to_editor2').val("1");
                    $('#loading').show();
                    $('#aspnetForm').submit();
                }
            });
            
            $('.pasaraeditor2_div ').click(function () {
                if (confirm('SE ENVIARAN LAS ACLARACIONES AL EDITOR. ¿DESEA CONTINUAR?')) {
                    $('#pasaraeditor2_field').val("1");
                    $('#loading').show();
                    $('#aspnetForm').submit();
                }
            });
            $('#pasaradirector2_div').click(function () {
                if (confirm('SE ENVIARAN LAS ACLARACIONES AL DIRECTOR. ¿DESEA CONTINUAR?')) {
                    $('#pasaradirector2_field').val("1");
                    $('#loading').show();
                    $('#aspnetForm').submit();
                }
            });
            $('#pasaradirector_div').click(function () {
                if (confirm('PASARÁ LA SOLICITUD PARA REVISIÓN DEL DIRECTOR. ¿DESEA CONTINUAR?')) {
                    $('#pasaradirector_field').val("1");
                    $('#loading').show();
                    $('#aspnetForm').submit();
                }
            });
            $('#enviarsolicituddeaclaraciones_div').click(function () {
                if (confirm('ENVIARÁ LA SOLICITUD DE ACLARACIONES AL PRODUCTOR. ¿DESEA CONTINUAR?')) {
                    $('#enviarsolicituddeaclaraciones_field').val("1");
                    $('#loading').show();
                    $('#aspnetForm').submit();
                }
            });

           

            

            $('#enviaraclaraciones_div').click(function () {
                if (confirm('ESTA ENVIANDO SUS ACLARACIONES, LUEGO DE HACERLO NO PODRÁ MODIFICARLAS. ¿DESEA CONTINUAR?')) {
                    $('#enviaraclaraciones_field').val("1");
                    $('#loading').show();
                    $('#aspnetForm').submit();
                }
            });
            $('#aprobarsolicitud1_div').click(function () {
                if (confirm('SE APROBARÁ LA SOLICITUD ¿DESEA CONTINUAR?')) {
                    $('#aprobarsolicitud1_field').val("1");
                    $('#loading').show();
                    $('#aspnetForm').submit();
                }
            });
            $('#rechazarsolicitud1_div').click(function () {
                if (confirm('SE RECHAZARÁ LA SOLICITUD ¿DESEA CONTINUAR?')) {
                    $('#rechazarsolicitud1_field').val("1");
                    $('#loading').show();
                    $('#aspnetForm').submit();
                }
            });
            $('#cancelarsolicitud1_div').click(function () {
                if (confirm('SE CANCELARÁ LA SOLICITUD ¿DESEA CONTINUAR?')) {
                    $('#cancelarsolicitud1_field').val("1");
                    $('#loading').show();
                    $('#aspnetForm').submit();
                }
            });
            $('#schedulefilmview_submit').click(function () {
                if (confirm('SE GUARDARÁ LA INFORMACIÓN REGISTRADA DE LA VISUALIZACIÓN DE LA OBRA ¿DESEA CONTINUAR?')) {
                    $('#schedulefilmview_field').val("1");
                    $('#loading').show();
                    $('#aspnetForm').submit();
                }
            });
            $('#resolution_date_submit').click(function () {
                if (confirm('SE GUARDARÁ LA FECHA DE RESOLUCIÓN INDICADA EN EL FORMULARIO ¿DESEA CONTINUAR?')) {
                    $('#resolution_date_field').val("1");
                    $('#loading').show();
                    $('#aspnetForm').submit();
                }
            });
            $('#resolution_date2_submit').click(function () {
                if (confirm('SE GUARDARÁ LA FECHA DE RESOLUCIÓN ACLARATORIA INDICADA EN EL FORMULARIO ¿DESEA CONTINUAR?')) {
                    $('#resolution_date2_field').val("1");
                    $('#loading').show();
                    $('#aspnetForm').submit();
                }
            });

            $('#notification_date_submit').click(function () {
                if (confirm('SE GUARDARÁ LA FECHA DE NOTIFICACIÓN INDICADA EN EL FORMULARIO ¿DESEA CONTINUAR?')) {
                    $('#notification_date_field').val("1");
                    $('#loading').show();
                    $('#aspnetForm').submit();
                }
            });
            $('#notification_date2_submit').click(function () {
                if (confirm('SE GUARDARÁ LA FECHA DE NOTIFICACIÓN DE RESOLUCIÓN ACLARATORIA INDICADA EN EL FORMULARIO ¿DESEA CONTINUAR?')) {
                    $('#notification_date2_field').val("1");
                    $('#loading').show();
                    $('#aspnetForm').submit();
                }
            });

            $('#submit_project').click(function () {
                $('#submit_field').val(null);
                if (confirm('ENVIARÁ EL PROYECTO A LA Dirección de Audiovisuales, Cine y Medios Interactivos, LUEGO DE HACERLO NO PODRÁ MODIFICAR EL PROYECTO ¿DESEA CONTINUAR?')) {
                    $('#submit_field').val("1");
                    $('#loading').show();
                    $('#aspnetForm').submit();
                }
            });

            $('#<%=schedulefilmview_date.ClientID %>').datepicker({dateFormat: "yy-mm-dd",changeMonth: true,changeYear: true,});
            $('#<%=resolution_date.ClientID %>').datepicker({dateFormat: "yy-mm-dd",changeMonth: true,changeYear: true,});
            $('#<%=notification_date.ClientID %>').datepicker({ dateFormat: "yy-mm-dd", changeMonth: true, changeYear: true, });
            $('#<%=resolution_date2.ClientID %>').datepicker({ dateFormat: "yy-mm-dd", changeMonth: true, changeYear: true, });
            $('#<%=notification_date2.ClientID %>').datepicker({ dateFormat: "yy-mm-dd", changeMonth: true, changeYear: true, });

        });
    </script>
<script type="text/javascript" src="Scripts/ajaxfileupload.js"></script>
<script type="text/javascript" src="Scripts/jquery.ajaxDownload.js"></script>

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
        <div class="bloque">
                <div class="pull-right" >
            <asp:Label ID="opciones_adicionales" runat="server"></asp:Label>
            <asp:Label runat="server" ID="lblCodProyecto" Visible="false"></asp:Label>
            
        </div>
        </div>
        </div>
    </div>

    <!-- Menu-->
    <div class="tabs">
            <ul id='menu'>
                <li class="<%=tab_datos_proyecto_css_class %>"><a href="DatosProyecto.aspx">Datos de<br />
                    la Obra<%=tab_datos_proyecto_revision_mark_image %></a></li>
                <li class="<%=tab_datos_productor_css_class %>"><a href="DatosProductor.aspx">Datos
                    del<br />
                    Productor<%=tab_datos_productor_revision_mark_image %></a></li>
                <li class="<%=tab_productores_adicionales_css_class %>"><a href="ProductoresAdicionales.aspx">
                    Coproductores<%=tab_datos_productores_adicionales_revision_mark_image %></a></li>
                
                <li class="<%=tab_datos_personal_css_class %>"><a href="DatosPersonal.aspx">Personal<%=tab_datos_personal_revision_mark_image %></a></li>
                <!-- <li class="<%--=tab_datos_adjuntos_css_class --%>"><a href="DatosAdjuntos.aspx">Adjuntos<%--=tab_datos_adjuntos_revision_mark_image --%></a></li> -->
<li class="<%=tab_datos_formato_personal_css_class %>"><a href="DatosFormatoPersonal.aspx">Registro de personal <br />artístico y técnico   <%=tab_datos_formato_personal_revision_mark_image %></a></li>
                
                <li class="<%=tab_datos_finalizacion_css_class %>"><a href="Finalizacion.aspx">Finalizaci&oacute;n</a></li>
            </ul>
        </div>
	<!-- End of Nav Div -->	
    <script type = "text/javaScript">
        $(document).ready(function () {
            $('#loading').hide();
        });
    </script>
        <asp:Panel runat="server" ID="pnlMensajeVisible" Visible="false" >
      
      <div style="clear:left;"></div>
     <div class="warning">
            <p>
                   <asp:Label runat="server"  ID="lblCortoViejo" Visible="false" Text="Una vez enviada la solicitud debe anexar el link protegido de la obra en la pestaña de finalización o en comentarios adicionales" ></asp:Label>
            
                <asp:Label runat="server"  ID="lblCorto" Text="Una vez revisada la solicitud, la Dirección de Audiovisuales, Cine y Medios Interactivos se comunicará para programa la visualización de la obra. " ></asp:Label>
                <asp:Label runat="server" ID="lblLargo" Text="Por favor, en la casilla Comentarios adicionales, copie un vínculo seguro, en el cual la Dirección de Audiovisuales, Cine y Medios Interactivos pueda visualizar la obra. "></asp:Label>
             
        </div>
      <div style="clear:left;"></div>
        </asp:Panel>

    <div id='Finalizacion'>
      
        <div>
        <span style="font-weight:bold;"><asp:Label ID="labelestadoproyecto" runat="server"></asp:Label></span>
            </div>

        <div style="background-color:white;float:right;width:400px;">
            <fieldset class="fieldset-final">
    <legend>Información Adicional</legend>
                <ul>
                <li>
                         <div class="field_label"><strong>Comentarios adicionales:</strong><span class="required_field_text"></span>
                            
                        </div>
                        <div class="field_input">
                          <asp:TextBox name="comentarios_adicionales" 
                              TextMode="MultiLine" Width="300px" Height="50px"
                              id="comentarios_adicionales"  class="user-input"
                                 runat="server"></asp:TextBox>
                        </div>
                    </li>
                    <li>
                        <asp:Button runat="server" ID="btnGuardarInfoAdicional" Text="Guardar comentarios adicionales"  OnClientClick="$('#loading').show();" OnClick="btnGuardarInfoAdicional_Click" />
                    </li>
                    <li><hr /></li>
                    <li>
                          <div class="field_label"><strong> Adjuntos Adicionales:</strong><span class="required_field_text"></span>
                        </div>
                    </li>
                    <li>
                <asp:Panel runat="server" ID="pnlAdicionarOtrosAdjuntos" 
                      >
                <table>
                   
                    <tr>
                        <td>Archivo</td>
                        <td colspan="2"><asp:FileUpload runat="server" ID="fileOtrosAdjuntos" /></td>
                    </tr>
                    <tr>
                        <td>Descripción</td>
                        <td><asp:TextBox runat="server" ID="txtOtrosAdjuntos"> </asp:TextBox></td>
                        <td><asp:Button runat="server" ID="btnCargarArchivo" Text="Cargar Archivo" OnClick="btnCargarARchivo_Click" OnClientClick="$('#loading').show();"/></td>
                    </tr>
                    <tr>
                        <td colspan="3"><asp:label runat="server" ForeColor="Red" id="lblErrorAdjuntos" ></asp:label></td>
                        
                    </tr>
                </table>
                    </asp:Panel>
                <asp:GridView runat="server"  ID="grdOtrosAdjuntos" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="cod_adjunto_projecto" DataSourceID="SqlDataSourceOtrosAdjuntos" ForeColor="#333333" GridLines="None">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                   
                    <asp:TemplateField ItemStyle-Width="350px" HeaderText="Otros Adjuntos">
                        <ItemTemplate>
                            <asp:HyperLink runat="server" ID="lnkOtrosAdjuntos" Target="_blank" ToolTip='<%# Eval("nombre_original") %>'
                                 Text='<%# "Adjunto - "+Eval("descripcion") %>' NavigateUrl='<%# Eval("url_adjunto") %>'></asp:HyperLink>

                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button runat="server" Text="Eliminar" OnClientClick="return confirm('Esta seguro de eliminar este adjunto?');"
                                 ID="btnQuitarADjuntoADicional" OnClick="btnQuitarADjuntoADicional_Click"
                                visible='<%# pnlAdicionarOtrosAdjuntos.Visible %>' 
                                 CommandArgument='<%# Eval("cod_adjunto_projecto") %>'  />
                        </ItemTemplate>
                    </asp:TemplateField>

                </Columns>
                <EditRowStyle BackColor="#999999" />
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
            </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSourceOtrosAdjuntos" runat="server" ConnectionString="<%$ ConnectionStrings:cineConnectionString %>" SelectCommand="SELECT [cod_adjunto_projecto], [url_adjunto], [nombre_original], [descripcion], [eliminado], [project_id] FROM [adjunto_projecto] WHERE (([project_id] = @project_id) AND ([eliminado] = @eliminado))">
                <SelectParameters>
                    <asp:ControlParameter ControlID="lblCodProyecto" Name="project_id" PropertyName="Text" Type="Int32" />
                    <asp:Parameter DefaultValue="false" Name="eliminado" Type="Boolean" />
                </SelectParameters>
            </asp:SqlDataSource>
                    </li>
                </ul>
                </fieldset>

        </div>

         <%if (project_state == 1 || project_state == 5) /* No ha sido enviado por el productor, por lo cual se presenta el resumen de estado de la solicitud */
        { %>
        <ul>
            <%if ((project_state == 1 || project_state == 5) && !showAtachhForm)
              { %>
                <li class="validation_messages_item">Datos de la Obra: <asp:Label ID="mensaje_validacion_datos_proyecto" runat="server"></asp:Label></li>
                <li class="validation_messages_item">Datos del Productor: <asp:Label ID="mensaje_validacion_datos_productor" runat="server"></asp:Label></li>
                <li class="validation_messages_item">Datos de los coproductores: <asp:Label ID="mensaje_validacion_productores_adicionales" runat="server"></asp:Label></li>
                <li class="validation_messages_item">Datos del Formato de Personal: <asp:Label ID="mensaje_validacion_datos_fomato_personal" runat="server"></asp:Label></li>
                <li class="validation_messages_item">Datos de Personal: <asp:Label ID="mensaje_validacion_datos_personales" runat="server"></asp:Label></li>
            <%} %>
                <li class="validation_messages_item">Datos de Finalización: <asp:Label ID="mensaje_validacion_datos_finalizacion" runat="server"></asp:Label></li>
            <!--<li class="validation_messages_item">Documentos Adjuntos: <asp:Label ID="mensaje_validacion_datos_adjuntos" runat="server"></asp:Label></li>-->
        </ul>
        <%} %>


        <div style="display:block;border-color:lightgray;border-style:solid;border-width:1px;height:auto;width:auto;padding:1%;max-width:320px;">
             
        <% if (showDescargarButton){ %>
            <div>
        <asp:LinkButton ID="LinkButton1" runat="server" Text="Descargar Fomulario de solicitud para firma " OnClick="generatePDFForm" ></asp:LinkButton>
                </div>
        <% } %>
      
        <% if ( path_request_form != null && path_request_form.Trim() != ""){ %>
        <div>
         <a target="_blank" href="<%=path_request_form%>">Formulario de solicitud firmado</a>
         </div>
         <%} %>

         <% if ( user_role>1 &&( project_state == 2 || project_state == 3 || project_state == 4 ||
               project_state == 6 || project_state == 7 || project_state == 8 )){ %>
            <div>
        <asp:RadioButton runat="server" GroupName="frm" Text="formulario de solicitud sin revisar" ID="rdFormulariosinRevizar" Enabled="false" Checked="true" />
                <br />
        <asp:RadioButton runat="server" GroupName="frm" Text="formulario de solicitud aprobado" ID="rdFormularioAprobado" Enabled="false" />
                <br />
        <asp:RadioButton runat="server" GroupName="frm" Text="formulario de solicitud no aprobado" ID="rdFormularioNoAprobado" Enabled="false" /><br />
                </div>
            <div>
        <asp:Button runat="server" ID="btnAprobarFormulario" Text="Aprobar Formulario" CssClass="boton" OnClick="btnAprobarFormulario_Click" OnClientClick="$('#loading').show();" />
        <asp:Button runat="server" ID="btnRechazarFormulario" Text="Solicitar Cambiar Formulario" CssClass="boton" OnClick="btnRechazarFormulario_Click" OnClientClick="$('#loading').show();" />
                </div>
        <% } %>

           <% if ((project_state == 1 || project_state == 5) && (user_role <= 1 && showAtachhForm))
              { %>
          <br />
            <div style="border-style:solid;border-color:lightblue;border-width:1px;">
                <div>
                <b>Cargar formulario de solicitud firmado</b><br />
                    <span id="name_formulario_solicitud"><% if (path_request_form != "" && path_request_form != null)
                           { %>
                          <a target="_blank" href="<%=path_request_form%>">Formulario de solicitud firmado</a>
                          <%}else{ %>Adjunte el Formulario de solicitud firmado<%} %></span>*
                   </div>
                    <div>
                            <div id='divFileUpload_formulario_solicitud'  >
                                <input type='file' name='file' id='FileUpload_formulario_solicitud' style='display:none' />  
                                <input type='button' style='width:110px;height:30px;background-color:darkblue;color:white;' id='btnFileUploadText' 
                                    value='Seleccionar'  onclick='FileUpload_formulario_solicitud.click();' />
                            </div>
                         
                           <div class='progressbar' id='progressbar' style='width:100px;display:none;'>
                               <div></div>
                            </div>
                      </div>
             
          </div>
        <% } %>
        </div>

        <%-- se crea este div vacio para ajustar cosas de diseño no borrar --%>
        <div></div>

        <%if (project_state == 1 || project_state == 5) /* No ha sido enviado por el productor, por lo cual se presenta el resumen de estado de la solicitud */
        { %>
          <% if (project_state == 1 && user_role <=1 && showSendButton){ %>
        <fieldset>                <legend></legend><div >
        <asp:Label runat="server" ID="lblErrorEnviar" forecolor="Red"></asp:Label>
        <input class="boton" type="submit" id="submit_project" name="submit_project"   value="Enviar a Dirección de Audiovisuales, Cine y Medios Interactivos " />
       <input type="hidden" id="submit_field" name="submit_field" value="" />
            </div>
            </fieldset>
         <% }else if (project_state == 5 && user_role <=1 ){ %>
         <div><a href="/SolicitudAclaraciones.aspx" target="_blank">Previsualizaci&oacute;n de la solicitud de aclaraciones</a></div>
            <div > <asp:Label runat="server" ID="lblErrorEnvirAclaraciones"  forecolor="Red"></asp:Label>
           <div id="enviaraclaraciones_div" class="final_link">
                <span>Enviar aclaraciones</span>
                <input type="hidden" id="enviaraclaraciones_field" name="enviaraclaraciones_field" value="" />
            </div>
             </div>
        <% }else if (project_state == 5 && user_role >1){ %>
         <%if (previsualizar_solicitud_aclaraciones_permission){ %>
                <div><a href="/SolicitudAclaraciones.aspx" target="_blank">Previsualizaci&oacute;n de la solicitud de aclaraciones</a></div>
            <%} %>
            <% if (ver_hoja_de_control_permission){ %>
                <div><a href="/HojaControl.aspx" target="_blank">Hoja de Control</a></div>
            <%} %>
        <%} %>

    <%}else if (project_state == 2) /* La solicitud ya ha sido enviada */
        { %>
            <%
              if (previsualizar_solicitud_aclaraciones_permission){ %>
                <div><a href="/SolicitudAclaraciones.aspx" target="_blank">Previsualizaci&oacute;n de la solicitud de aclaraciones</a></div>
            <%} %>
            <% if (ver_hoja_de_control_permission){ %>
                <div><a href="/HojaControl.aspx" target="_blank">Hoja de control</a></div>
            <%} %>

            <%if (pasar_solicitud_a_editor_permission)
            { %>
                <fieldset class="fieldset-final">
                <legend>Acciones de Flujo</legend>
                <div id="pasaraeditor_div" class="final_link"><span>Pasar a editor</span><input type="hidden" id="pasaraeditor_field" name="pasaraeditor_field" value="" /></div>
                </fieldset>
            <%} %>
       
       
        <% }
        else if (project_state == 3) /* La solicitud ha sido pasada al editor en su primera iteración de revisión */
        { %>
            <%if (previsualizar_solicitud_aclaraciones_permission)
            { %>
                <div><a href="/SolicitudAclaraciones.aspx" target="_blank">Previsualizaci&oacute;n de la solicitud de aclaraciones</a></div>
            <%} %>
            <% if (ver_hoja_de_control_permission)
            { %>
                <div><a href="/HojaControl.aspx" target="_blank">Hoja de Control</a></div>
            <%} %>
            <%if(user_role == 3){ %>
            <fieldset class="fieldset-final">
            <legend>Acciones de Flujo</legend>
                <div id="pasaradirector_div" class="final_link"><span>Pasar a director</span><input type="hidden" id="pasaradirector_field" name="pasaradirector_field" value="" /></div>
                <div id="back_to_revisor_div" class="final_link"><span>Devolver a revisor</span><input type="hidden" id="back_to_revisor" name="back_to_revisor" value="" /></div>
            </fieldset>
            <%} %>
        <% }
        else if (project_state == 4)
        { %>
            <%if (previsualizar_solicitud_aclaraciones_permission)
            { %>
                <div><a href="/SolicitudAclaraciones.aspx" target="_blank">Previsualizaci&oacute;n de la solicitud de aclaraciones</a></div>
            <%} %>
            <% if (ver_hoja_de_control_permission)
            { %>
                <div><a href="/HojaControl.aspx" target="_blank">Hoja de Control</a></div>
            <%} %>
            <%if(user_role == 4){ %>
            <fieldset class="fieldset-final">
            <legend>Acciones de Flujo</legend>
                <div id="enviarsolicituddeaclaraciones_div" class="final_link"><span>Enviar solicitud de aclaraciones</span><input type="hidden" id="enviarsolicituddeaclaraciones_field" name="enviarsolicituddeaclaraciones_field" value="" /></div>
                <div id="back_to_editor_div" class="final_link"><span>Devolver a Editor</span><input type="hidden" id="back_to_editor" name="back_to_editor" value="" /></div>
            </fieldset>
            <%} %>

        <% } else if (project_state == 6)
        { %>
            <%if (true || previsualizar_solicitud_aclaraciones_permission)
            { %>
                <div><a href="/SolicitudAclaraciones.aspx" target="_blank">Previsualizaci&oacute;n de la solicitud de aclaraciones</a></div>
            <%} %>
            <% if (ver_hoja_de_control_permission)
            { %>
                <div><a href="/HojaControl.aspx" target="_blank">Hoja de Control</a></div>
            <%} %>

            <%if(user_role == 2){ %>
            <fieldset class="fieldset-final">
                <legend>Acciones de Flujo</legend>
                <div id="pasaraeditor2_div " class="final_link pasaraeditor2_div"><span>Pasar a editor</span><input type="hidden" id="pasaraeditor2_field" name="pasaraeditor2_field" value="" /></div>
            </fieldset>
            <%} %>
        <% }
        else if (project_state == 7)
        { %>
            <%if (true || previsualizar_solicitud_aclaraciones_permission)
            { %>
                <div><a href="/SolicitudAclaraciones.aspx" target="_blank">Previsualizaci&oacute;n de la solicitud de aclaraciones</a></div>
            <%} %>
            <% if (ver_hoja_de_control_permission)
            { %>
                <div><a href="/HojaControl.aspx" target="_blank">Hoja de Control</a></div>
            <%} %>
        
        <%  if (user_role == 3)
            { %>
            <fieldset class="fieldset-final">
                <legend>Acciones de Flujo</legend>
                <div id="pasaradirector2_div" class="final_link"><span>Pasar a director</span><input type="hidden" id="pasaradirector2_field" name="pasaradirector2_field" value="" /></div>
                <div id="back_to_revisor2_div" class="final_link"><span>Devolver a revisor</span><input type="hidden" id="back_to_revisor2" name="back_to_revisor2" value="" /></div>
            </fieldset>
        <%}%>

        <% }
        else if (project_state == 8)
        { %>
            <%if (true || previsualizar_solicitud_aclaraciones_permission)
            { %>
                <div><a href="/SolicitudAclaraciones.aspx" target="_blank">Previsualizaci&oacute;n de la solicitud de aclaraciones</a></div>
            <%} %>
            <% if (ver_hoja_de_control_permission)
            { %>
                <div><a href="/HojaControl.aspx" target="_blank">Hoja de Control</a></div>
                <fieldset class="fieldset-final">
                <legend>Acciones de Flujo</legend>
                     <% if (user_role == 4)
            { %>
                <div id="back_to_editor2_div" class="final_link"><span>Devolver a Editor</span><input type="hidden" id="back_to_editor2" name="back_to_editor2" value="" /></div>
                    <%} %>
            </fieldset>
            <%} %>
        <% }
        else if (project_state == 9 || project_state == 10)
        { %>
            <%if (true || previsualizar_solicitud_aclaraciones_permission)
            { %>
                <div><a href="/SolicitudAclaraciones.aspx" target="_blank">Previsualizaci&oacute;n de la solicitud de aclaraciones</a></div>
            <%} %>
            <% if (ver_hoja_de_control_permission)
            { %>
                <div><a href="/HojaControl.aspx" target="_blank">Hoja de Control</a></div>
            <%} %>
       
<asp:Panel runat="server" ID="pnlResolucion">
        <div>
            <fieldset>
                <legend>Resolución</legend>
            <table>
                <tr>
                    <td>
                        <asp:LinkButton  runat="server" ID="lnkResolucion" Text="Descarga del archivo de la resolución" OnClick="lnkResolucion_Click"></asp:LinkButton>
                    </td>
                    <td>   <asp:Button runat="server" class="boton" ID="btnEliminarArchivoResolucion" Text="Eliminar Resolución" 
                            OnClick="btnEliminarArchivoResolucion_Click" 
                            OnClientClick="return confirm('Esta seguro de eliminar la resolución?');" /></td>
                </tr>
                <tr>
                    <td colspan="2">
                     <hr />&nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                         <asp:FileUpload runat="server" ID="fileResolucion"   />
                    </td>
                    <td>
                        <asp:Button runat="server" class="boton" Text="Cargar Resolucion" ID="btnCargarResolution" OnClick="btnCargarResolution_Click"  />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Label runat="server" ID="lblErrorReolucion" ForeColor="Red" ></asp:Label>
                    </td>
                </tr>
            </table>
                </fieldset>
</div>
            </asp:Panel >
            <asp:Panel runat="server" ID="pnlResolucion2">
                <div>
    <fieldset>
                <legend>Resolución Aclaratoria</legend>
            <table>
                <tr>
                    <td>
                        <asp:LinkButton  runat="server" ID="lnkResolucion2" Text="Descarga del archivo de la resolución aclaratoria" OnClick="lnkResolucion2_Click"></asp:LinkButton>
                    </td>
                    <td>   <asp:Button runat="server" class="boton" ID="btnEliminarArchivoResolucion2" Text="Eliminar Resolución aclaratoria" 
                            OnClick="btnEliminarArchivoResolucion2_Click" 
                            OnClientClick="return confirm('Esta seguro de eliminar la resolución aclaratoria?');" /></td>
                </tr>
                <tr>
                    <td colspan="2">
                     <hr />&nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                         <asp:FileUpload runat="server" ID="fileResolucion2"   />
                    </td>
                    <td>
                        <asp:Button runat="server" class="boton" Text="Cargar Resolucion aclaratoria" ID="btnCargarResolution2" OnClick="btnCargarResolution2_Click"  />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Label runat="server" ID="lblErrorReolucion2" ForeColor="Red" ></asp:Label>
                    </td>
                </tr>
            </table>
                </fieldset>
            </div>
    </asp:Panel>

      

 <%if (registrar_fecha_de_resolucion_permission)
            { %>
                <div id="resolution_date_register">
                    <fieldset>
                        <legend>Registro de la fecha de la resoluci&oacute;n</legend>
                        <div class="field_label"><label for="resolution_date">Fecha de la resoluci&oacute;n de la solicitud</label></div>
                        <div class="field_input"><input type="text" name="resolution_date" id="resolution_date" class="user-input" runat="server"/></div>
                        <input type="hidden" id="resolution_date_field" name="resolution_date_field" value="" />
                        <button class="boton"  id="resolution_date_submit">Guardar la fecha de la resoluci&oacute;n</button>
                    </fieldset>
                </div>     
            <%} %>      
            <%if (registrar_fecha_de_notificacion_permission)
            { %>
                <div id="notification_date_register">
                    <fieldset>
                        <legend>Registro de la fecha de la notificaci&oacute;n</legend>
                        <div class="field_label"><label for="notification_date">Fecha de la notificaci&oacute;n de la solicitud</label></div>
                        <div class="field_input"><input type="text" name="notification_date" id="notification_date" class="user-input" runat="server"/></div>
                        <input type="hidden" id="notification_date_field" name="notification_date_field" value="" />
                        <button class="boton" id="notification_date_submit">Guardar la fecha de la notificaci&oacute;n</button>
                    </fieldset>
                </div>
      <%} %> 
         <%if (registrar_fecha_de_resolucion_permission && project_state ==9)
            { %>
           <div id="resolution_date2_register">
                    <fieldset>
                        <legend>Registro de la fecha de la resoluci&oacute;n aclaratoria</legend>
                        <div class="field_label"><label for="resolution_date2">Fecha de la resoluci&oacute;n de la solicitud</label></div>
                        <div class="field_input"><input type="text" name="resolution_date2" id="resolution_date2" class="user-input" runat="server"/></div>
                        <input type="hidden" id="resolution_date2_field" name="resolution_date2_field" value="" />
                        <button class="boton"  id="resolution_date2_submit">Guardar la fecha de la resoluci&oacute;n aclaratoria</button>
                    </fieldset>
                </div>
         <%} %>
        
  
          <%if (registrar_fecha_de_notificacion_permission && project_state == 9)
            { %>
                <div id="notification_date2_register">
                    <fieldset>
                        <legend>Registro de la fecha de la notificaci&oacute;n aclaratoria</legend>
                        <div class="field_label"><label for="notification_date2">Fecha de la notificaci&oacute;n de la resoluci&oacute;n aclaratoria</label></div>
                        <div class="field_input"><input type="text" name="notification_date2" id="notification_date2" class="user-input" runat="server"/></div>
                        <input type="hidden" id="notification_date2_field" name="notification_date2_field" value="" />
                        <button class="boton" id="notification_date2_submit">Guardar la fecha de la notificaci&oacute;n de la resoluci&oacute;n aclaratoria</button>
                    </fieldset>
                </div>
       <%} %> 

        <% } %>
      

                
            <div style="background-color:white;">
                  <%if (acciones_finales){ %>
        <asp:Label runat ="server" ID="lblErrorAprobacion" Text="" ForeColor="Red"></asp:Label>
              <fieldset class="fieldset-final">
              <legend>Acciones Finales</legend>
                  <%--boton de aprobar solicitud--%>
                  <%if (project_state !=9 ){ %>
                <div id="aprobarsolicitud1_div" class="accion_finalizar final_link"><span>Aprobar solicitud</span><input type="hidden" id="aprobarsolicitud1_field" name="aprobarsolicitud1_field" value="" /></div>
                  <%}%>
                <%if (project_state >= 4 && project_state <=9 ){ %>
                <div id="rechazarsolicitud1_div" class="accion_finalizar final_link"><span>Rechazar solicitud</span><input type="hidden" id="rechazarsolicitud1_field" name="rechazarsolicitud1_field" value="" /></div>
                <%}%>
                <div id="cancelarsolicitud1_div" class="accion_finalizar final_link"><span>Cancelar solicitud</span><input type="hidden" id="cancelarsolicitud1_field" name="cancelarsolicitud1_field" value="" /></div>
              </fieldset>
            <%}%>
            </div>

        
      
          <asp:Panel runat="server" ID="pnlAclaraciones" Visible="false">
              <fieldset>
                  <legend></legend>

            <div class="field_label"><b>Texto adicional carta de aclaraciones</b></div>
            <asp:TextBox runat="server" Width="300px" Height="50px" ID="txtComplementoCartaAclaraciones" TextMode="MultiLine"
                ></asp:TextBox>
             <br />

        

                   <div class="field_label"><b>Texto sustituto carta de aclaraciones</b></div>
            <asp:TextBox runat="server" Width="300px" Height="50px" ID="txtSustitutoCartaAclaraciones" TextMode="MultiLine"
                ></asp:TextBox>
             <br />


            <asp:Button runat="server" OnClientClick="$('#loading').show();" class="boton" Text="Guardar texto adicional" id="btnGuardarTExtoAdicional" OnClick="btnGuardarTExtoAdicional_Click" />
                  </fieldset>
        </asp:Panel>
          <%if ( project_state >= 5 && user_role <=1 ) 
            { %>
        <br />
        <asp:Panel runat="server" ID="pnlObservacionesAclaracion" Visible="false">
            <div style="border-color:lightblue;border-style:solid;border-width:1px;padding:1%;">
                <div>
             <div class="field_label"><label for="schedulefilmview_result">Resultado de la visualización</label></div>
                     <asp:TextBox Width="350px" Height="45px" runat="server" ReadOnly="true" TextMode="MultiLine" ID="schedulefilmview_result2"></asp:TextBox>
             
                   </div>
                <br />
                <asp:Panel runat="server" ID="pnlLargos">
                <div>
            <div class="field_label"><label for="schedulefilmview_result2">Datos persona de contacto para visualización de la obra</label></div>
              <asp:TextBox Width="350px" Height="45px" runat="server" TextMode="MultiLine" ID="txtRespuestaVisualizacion"></asp:TextBox>
                    </div>
                    </asp:Panel>
            <asp:Label runat="server" ID="lblErrorRespuestaVisualizacion" ForeColor="Red"></asp:Label>
            <asp:Button CssClass="boton" runat="server" ID="btnGuardarRespuestaVisualizacion" 
                Text="Guardar Respuesta visualización" OnClick="btnGuardarRespuestaVisualizacion_Click" />
                </div>
        </asp:Panel>
        <%} %>

          <%if ( (project_state > 1 && user_role >1) )
            { %>
                <div id="schedulefilmview">
                    <fieldset>
                        <legend>Programación y resultado de la visualización  de la obra</legend> 
                        <div>
                         <asp:RadioButton runat="server" ID="rdSinRevizarVisualizacion" Checked="true" Text="visualización sin revisar" GroupName="vis" />
                        <asp:RadioButton runat="server" ID="rdSolicitarAclaracionesVisualizacion" Text="Solicitar aclaraciones de la visualización"  GroupName="vis" />
                        <asp:RadioButton runat="server" ID="rdInformacionCorrectaVisualizacion" Text="Información correcta de la visualización"  GroupName="vis" />
                           </div>
                        <div class="field_label"><label for="schedulefilmview_date">Fecha de cita de visualización de la obra yyyy-MM-dd hh:mm</label></div>
                        <div class="field_input"><input type="text" name="schedulefilmview_date" id="schedulefilmview_date" class="user-input" runat="server"/></div>
                        <div class="field_label"><label for="schedulefilmview_result">Resultado de la visualización</label></div>
                        <div class="field_input"><textarea  style="width:620px;min-height:100px;"  rows="5" cols="60" name="schedulefilmview_result" id="schedulefilmview_result" class="user-input" runat="server"/></div>

                        <asp:Panel runat="server" ID="pnlLargos2">
                        <asp:Panel runat="server" ID="pnlRespuestaVisualizacionProductor" Visible="false">
                         <div class="field_label"><label for="schedulefilmview_result2">Datos persona de contacto para visualización de la obra</label></div>
              <asp:TextBox Width="350px" Height="45px" runat="server" TextMode="MultiLine" ID="txtRespuestaVisualizacion2" ReadOnly="true"></asp:TextBox>
                    </asp:Panel>
                            </asp:Panel>


                        <input type="hidden" id="schedulefilmview_field" name="schedulefilmview_field" value="" />
                      
                        <button id="schedulefilmview_submit" onclick="$('#loading').show();" class="boton">Guardar informaci&oacute;n de la visualizaci&oacute;n  de la obra</button>
                    </fieldset>
                </div>
            <%} %>

        <%if (project_state==2 && deshacer_envio_solicitud_permission)
            { %>
        <div id="deshacerenvio"><span>Deshacer el env&iacute;o de la solicitud</span><input id="specialaction" name="specialaction" type="hidden" value="" /></div>
        <%} %>
         
    </div>
</div>
    
     <uc1:cargando ID="cargando1" runat="server" />

</div>

</asp:Content>
