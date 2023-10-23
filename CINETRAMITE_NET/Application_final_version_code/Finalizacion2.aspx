<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Finalizacion2.aspx.cs" Inherits="CineProducto.Finalizacion2" %>

<%@ Register Src="usercontrols/cargando.ascx" TagName="cargando" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.13.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="TitleContent" runat="server" ContentPlaceHolderID="TitleContent">Trámite Reconocimiento Como Obra Nacional - Finalización - Mincultura</asp:Content>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <style>
        .progressbar {
            background-color: black;
            background-repeat: repeat-x;
            border-radius: 13px; /* (height of inner div) / 2 + padding */
            padding: 3px;
        }

            .progressbar > div {
                background-color: orange;
                width: 0%; /* Adjust with JavaScript */
                height: 20px;
                border-radius: 10px;
            }

        .modalBackground {
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.8;
        }

        .ajax__calendar_container {
            padding: 4px;
            cursor: default;
            width: 180px !important;
            font-size: 11px;
            text-align: center;
            font-family: tahoma,verdana,helvetica;
        }

        .ajax__calendar_dayname {
            height: 17px;
            width: 20px !important;
            text-align: right;
            padding: 0 2px;
        }

        .ajax__calendar_day {
            height: 17px;
            width: 20px !important;
            text-align: right;
            padding: 0 2px;
            cursor: pointer;
        }

        table {
            margin: 0px !important;
            border-collapse: separate !important;
        }


        .dxMonthGridWithWeekNumbers {
            padding: 0.3em 0.5em;
        }

        .dxeCalendar_MetropolisBlue td.dxMonthGridWithWeekNumbers {
            padding: 0px;
        }

        .dxeCalendarHeader_MetropolisBlue {
            font-size: 13px !important;
            padding: 0 !important;
        }

        .dxeDateEditTimeEditCell_MetropolisBlue {
            padding: 13px 1px 5px !important;
        }
    </style>
    <%--<script src="blueimp/jquery-1.11.1.js" type="text/javascript"></script>--%>
    <script src="blueimp/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="blueimp/jquery.iframe-transport.js" type="text/javascript"></script>
    <script src="blueimp/jquery.fileupload.js" type="text/javascript"></script>
    <script type="text/javaScript">
        $(document).ready(function () {
            scroll();

            autosize($('#<%=infVisualizacion.ClientID %>'));
            autosize($('#<%=comentarios_adicionales.ClientID %>'));
            autosize($('#<%=schedulefilmview_result.ClientID %>'));
            autosize($('#<%=txtComplementoCartaAclaraciones.ClientID %>'));
            autosize($('#<%=txtRespuestaVisualizacion.ClientID %>'));
            autosize($('#<%=txtRespuestaVisualizacion2.ClientID %>'));
            $('#loading').hide();

            //funcion para cargar formulario de solicitud firmado
            $(function () {
                $('#FileUpload_formulario_solicitud').fileupload({
                    url: 'FileUploadHandler.ashx?upload=start&folder=<%= Page.ResolveClientUrl("~/uploads")%>' + '/<%=project_id %>/' + '&attachment_id=0',
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
                        alert('El formulario se cargo correctamente.');

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
                            $('#aspnetForm').submit();
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


            //funcion para cargar Hoja de Tranferncia
            $(function () {
  
                $('#FileUpload_Hoja_Transferencia').fileupload({
                    url: 'FileUploadHandler.ashx?upload=start&folder=<%= Page.ResolveClientUrl("~/uploads")%>' + '/<%=project_id %>/' + '&attachment_id=0',
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
                        alert('Hoja de Transferencia se cargo correctamente.');

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
                                url: '<%=Page.ResolveClientUrl("~/Default.aspx/proccessHojaTransferencia") %>',
                                data: '{project_id:<%=project_id%>,filename:"' + response + '"}',
                                contentType: 'application/json; charset=utf-8',
                                dataType: 'json',
                                success: function (msg) {
                                    // Replace the div's content with the page method's return.
                                    $('#name_hoja_tansferencia').html(msg.d);
                                },
                                error: function (request, error) {
                                    console.log(" Can't do because: " + error);
                                },
                            });
                            $('#aspnetForm').submit();
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
                if (confirm('SE APROBARÁ LA SOLICITUD Y SE ENVIARA EL CERTIFICADO AL EMAIL DEL PRODUCTOR ¿DESEA CONTINUAR?')) {
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
                if (confirm('ENVIARÁ EL PROYECTO A LA DIRECCION DE AUDIOVISUALES, CINE Y MEDIOS INTERACTIVOS, LUEGO DE HACERLO NO PODRÁ MODIFICAR EL PROYECTO ¿DESEA CONTINUAR?')) {
                    $('#submit_field').val("1");
                    $('#loading').show();
                    $('#aspnetForm').submit();
                }
            });

            $('#<%=schedulefilmview_date.ClientID %>').datepicker({ dateFormat: "yy-mm-dd", changeMonth: true, changeYear: true, });
            $('#<%=resolution_date.ClientID %>').datepicker({ dateFormat: "yy-mm-dd", changeMonth: true, changeYear: true, });
            $('#<%=notification_date.ClientID %>').datepicker({ dateFormat: "yy-mm-dd", changeMonth: true, changeYear: true, });
            $('#<%=resolution_date2.ClientID %>').datepicker({ dateFormat: "yy-mm-dd", changeMonth: true, changeYear: true, });
            $('#<%=notification_date2.ClientID %>').datepicker({ dateFormat: "yy-mm-dd", changeMonth: true, changeYear: true, });

        });
    </script>
    <script type="text/javascript" src="Scripts/ajaxfileupload.js"></script>
    <script type="text/javascript" src="Scripts/jquery.ajaxDownload.js"></script>

</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:ScriptManager runat="server" ID="scManager" EnableScriptGlobalization="true" EnableScriptLocalization="true"></asp:ScriptManager>


    <div id="cine">
        <!-- Bloque de información contextual -->
        <div id="informacion-contextual">
            <div class="bloque"><strong>Nombre:</strong><br />
                <strong>
                    <asp:Label ID="nombre_proyecto" runat="server"></asp:Label></strong></div>
            <div class="bloque">
                <strong>
                    <asp:Label ID="tipo_produccion" runat="server"></asp:Label></strong><br />
                <asp:Label ID="tipo_proyecto" runat="server"></asp:Label>
            </div>
            <div class="bloque"><strong>Productor:</strong><br />
                <asp:Label ID="nombre_productor" runat="server"></asp:Label></div>
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
                <li class="<%=tab_datos_proyecto_css_class %>"><a href="DatosProyecto.aspx">Datos de<br />
                    la Obra<%=tab_datos_proyecto_revision_mark_image %></a></li>
                <li class="<%=tab_datos_productor_css_class %>"><a href="DatosProductor.aspx">Datos
                    del<br />
                    Productor<%=tab_datos_productor_revision_mark_image %></a></li>
                <li class="<%=tab_productores_adicionales_css_class %>"><a href="ProductoresAdicionales.aspx">Coproductores<%=tab_datos_productores_adicionales_revision_mark_image %></a></li>

                <li class="<%=tab_datos_personal_css_class %>"><a href="DatosPersonal.aspx">Personal<%=tab_datos_personal_revision_mark_image %></a></li>
                <!-- <li class="<%--=tab_datos_adjuntos_css_class --%>"><a href="DatosAdjuntos.aspx">Adjuntos<%--=tab_datos_adjuntos_revision_mark_image --%></a></li> -->
                <li class="<%=tab_datos_formato_personal_css_class %>"><a href="DatosFormatoPersonal.aspx">Registro de personal
                    <br />
                    artístico y técnico   <%=tab_datos_formato_personal_revision_mark_image %></a></li>

                <li class="<%=tab_datos_finalizacion_css_class %>"><a href="Finalizacion2.aspx">Finalizaci&oacute;n</a></li>
            </ul>
        </div>
        <!-- End of Nav Div -->
        <script type="text/javaScript">
            $(document).ready(function () {
                $('#loading').hide();
            });
        </script>
        <asp:Panel runat="server" ID="pnlMensajeVisible" Visible="false">

            <div style="clear: left;"></div>
            <div class="warning">
                <p>
                    <asp:Label runat="server" ID="lblCortoViejo" Visible="false" Text="Una vez enviada la solicitud debe copiar el link protegido de la obra en la pestaña de finalización o en la casilla información para visualización de la obra"></asp:Label>

                    <asp:Label runat="server" ID="lblCorto" Text="Una vez revisada la solicitud, la Dirección de Audiovisuales, Cine y Medios Interactivos visualizará la obra en el link proporcionado en la casilla información para visualización de la obra. "></asp:Label>
                    <asp:Label runat="server" ID="lblLargo" Text="Por favor, en la casilla información para visualización de la obra, copie un vínculo seguro, en el cual la Dirección de Audiovisuales, Cine y Medios Interactivos pueda visualizar la obra. "></asp:Label>
                    <asp:Label runat="server" Visible="false" ID="lblHorarioRadicacion" Text="<br />Recuerde que las solicitudes que se radiquen despues de las 5:00 pm quedaran radicadas con la fecha del siguiente dia. "></asp:Label>
            </div>
            <div style="clear: left;"></div>
        </asp:Panel>

    </div>



    <div class="row " style="width: 100% !important;">
        <div class="col-12">
            <div class="pull-left">
                <span style="font-weight: bold;">
                    <asp:Label ID="labelestadoproyecto" runat="server"></asp:Label></span><br />
                <%if (project_state ==9) /* mostrar solo esto cuando es aprobado */
        { %>
                <span style="font-weight: bold;">
                    <asp:Label ID="lblNumeroCertificado" runat="server"></asp:Label></span><br />
                <span style="font-weight: bold;">
                    <asp:Label ID="lblFechaCertificado" runat="server"></asp:Label></span><br />
                <span style="font-weight: bold;">
                    <asp:Label ID="lblFechaNotificacion" runat="server"></asp:Label></span><br />
                <asp:LinkButton ID="linkVerResolucion" runat="server" Text="Ver Certificado" OnClick="generatePDFResolucion" Visible="false"></asp:LinkButton>
                <%} %>
            </div>
        </div>
        <div id='Finalizacion' class="col-12">
            <%if (project_state == 1 || project_state == 5) /* No ha sido enviado por el productor, por lo cual se presenta el resumen de estado de la solicitud */
        { %>
            <ul>
                <%if ((project_state == 1 || project_state == 5) && !showAtachhForm)
              { %>
                <li class="validation_messages_item">Datos de la Obra:
                    <asp:Label ID="mensaje_validacion_datos_proyecto" runat="server"></asp:Label></li>
                <li class="validation_messages_item">Datos del Productor:
                    <asp:Label ID="mensaje_validacion_datos_productor" runat="server"></asp:Label></li>
                <li class="validation_messages_item">Datos de los coproductores:
                    <asp:Label ID="mensaje_validacion_productores_adicionales" runat="server"></asp:Label></li>
                <li class="validation_messages_item">Datos del Formato de Personal:
                    <asp:Label ID="mensaje_validacion_datos_fomato_personal" runat="server"></asp:Label></li>
                <li class="validation_messages_item">Datos de Personal:
                    <asp:Label ID="mensaje_validacion_datos_personales" runat="server"></asp:Label></li>
                <%} %>
                <li class="validation_messages_item">Datos de Finalización:
                    <asp:Label ID="mensaje_validacion_datos_finalizacion" runat="server"></asp:Label></li>
                <!--<li class="validation_messages_item">Documentos Adjuntos: <asp:Label ID="mensaje_validacion_datos_adjuntos" runat="server"></asp:Label></li>-->
            </ul>
            <%} %>
        </div>


    </div>

    <div style="min-height: 10px !important; background-color: aliceblue; width: 100% !important;"></div>

    <div class="row" style="width: 100% !important;">
        <!-- aca deberian estar todos los botones -->
        <div class="col-6">
            <%if (project_state == 1 || project_state == 5) /* No ha sido enviado por el productor, por lo cual se presenta el resumen de estado de la solicitud */
        { %>
            <% if (project_state == 1 && user_role <=1 && showSendButton){ %>
            <fieldset>
                <legend>Radicar la solicitud</legend>
                <div>
                    <asp:Label runat="server" ID="lblErrorEnviar" ForeColor="Red"></asp:Label>
                    <input class="boton" type="submit" style="font-size: 15px;" id="submit_project" name="submit_project" value="Enviar a Dirección de Audiovisuales, Cine y Medios Interactivos " />
                    <input type="hidden" id="submit_field" name="submit_field" value="" />
                </div>
            </fieldset>
            <% }
                else if ((project_state == 5 && user_role <=1) && (showBtnAclaraciones == true) ){ %>
            <div><a href="/SolicitudAclaraciones.aspx" target="_blank">Previsualizaci&oacute;n de la solicitud de aclaraciones</a></div>
            <div>
                <asp:Label runat="server" ID="lblErrorEnvirAclaraciones" ForeColor="Red"></asp:Label>
                <div id="enviaraclaraciones_div" class="final_link" style="width: 200px">
                    <span>Enviar aclaraciones</span>
                    <input type="hidden" id="enviaraclaraciones_field" name="enviaraclaraciones_field" value="" />
                </div>
            </div>
            <% }
                else if (project_state == 5 && user_role >1){ %>
            <%if (previsualizar_solicitud_aclaraciones_permission){ %>
            <div><a href="/SolicitudAclaraciones.aspx" target="_blank">Previsualizaci&oacute;n de la solicitud de aclaraciones</a></div>
            <%} %>
            <% if (ver_hoja_de_control_permission){ %>
            <div><a href="/HojaControl.aspx" target="_blank">Hoja de Control</a></div>
            <%} %>
            <%} %>

            <%}
                else if (project_state == 2) /* La solicitud ya ha sido enviada */
                { %>
            <%
                if (previsualizar_solicitud_aclaraciones_permission)
                { %>
            <div><a href="/SolicitudAclaraciones.aspx" target="_blank">Previsualizaci&oacute;n de la solicitud de aclaraciones</a></div>
            <%} %>
            <% if (ver_hoja_de_control_permission){ %>
            <div><a href="/HojaControl.aspx" target="_blank">Hoja de control</a></div>
            <%} %>

            <%if (pasar_solicitud_a_editor_permission)
            { %>
            <fieldset class="fieldset-final">
                <asp:Label runat="server" ID="lblValPasarEditor" Text="" ForeColor="Red"></asp:Label>
                <legend>Acciones de Flujo</legend>
                <%if (bloquearSiguientePasoFlujo == false)
                         {%>
                <div id="pasaraeditor_div" style="width: 230px" class="final_link">
                    <span>Pasar a editor</span>
                    <input type="hidden" id="pasaraeditor_field" name="pasaraeditor_field" value="" />
                </div>
                <%} %>
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
            <%if (user_role == 3){ %>
            <fieldset class="fieldset-final">
                <legend>Acciones de Flujo</legend>
                <asp:Label runat="server" ID="lblValPasarEditor2" Text="" ForeColor="Red"></asp:Label>
                <%if (bloquearSiguientePasoFlujo == false){%>
                <div id="pasaradirector_div" style="width: 230px" class="final_link"><span>Pasar a director</span><input type="hidden" id="pasaradirector_field" name="pasaradirector_field" value="" /></div>
                <div id="back_to_revisor_div" style="width: 230px" class="final_link"><span>Devolver a revisor</span><input type="hidden" id="back_to_revisor" name="back_to_revisor" value="" /></div>
                <%}%>
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
            <%if (user_role == 4){ %>
            <fieldset class="fieldset-final">
                <legend>Acciones de Flujo</legend>
                <asp:Label runat="server" ID="lblValPasarEditor3" Text="" ForeColor="Red"></asp:Label>
                <%if (bloquearSiguientePasoFlujo == false){%>
                <div id="enviarsolicituddeaclaraciones_div" style="width: 250px" class="final_link"><span>Enviar solicitud de aclaraciones</span><input type="hidden" id="enviarsolicituddeaclaraciones_field" name="enviarsolicituddeaclaraciones_field" value="" /></div>
                <div id="back_to_editor_div" style="width: 250px" class="final_link"><span>Devolver a Editor</span><input type="hidden" id="back_to_editor" name="back_to_editor" value="" /></div>
                <%} %>
            </fieldset>
            <%} %>

            <% }
                else if (project_state == 6)
                { %>
            <%if (true || previsualizar_solicitud_aclaraciones_permission)
            { %>
            <div><a href="/SolicitudAclaraciones.aspx" target="_blank">Previsualizaci&oacute;n de la solicitud de aclaraciones</a></div>
            <%} %>
            <% if (ver_hoja_de_control_permission)
            { %>
            <div><a href="/HojaControl.aspx" target="_blank">Hoja de Control</a></div>
            <%} %>

            <%if (user_role == 2){ %>
            <fieldset class="fieldset-final">
                <legend>Acciones de Flujo</legend>
                <div id="pasaraeditor2_div " style="width: 230px" class="final_link pasaraeditor2_div"><span>Pasar a editor</span><input type="hidden" id="pasaraeditor2_field" name="pasaraeditor2_field" value="" /></div>
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
                <div id="pasaradirector2_div" style="width: 200px" class="final_link"><span>Pasar a director</span><input type="hidden" id="pasaradirector2_field" name="pasaradirector2_field" value="" /></div>
                <div id="back_to_revisor2_div" style="width: 200px" class="final_link"><span>Devolver a revisor</span><input type="hidden" id="back_to_revisor2" name="back_to_revisor2" value="" /></div>
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
                if (project_state == 4 || project_state == 8)
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
                <!--div>
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
</div-->
            </asp:Panel>
            <asp:Panel runat="server" ID="pnlResolucion2">
                <!--div>
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
            </div-->
            </asp:Panel>

        </div>
    </div>
    <div id="resolution_date_register24">
        <fieldset class="fieldset-final">
            <legend>Registro de la fecha de expedici&oacute;n del Certificado</legend>

            <%if (registrar_fecha_de_resolucion_permission)
            { %>
            <div id="resolution_date_register">
                <!--fieldset>
                        <legend>Registro de la fecha de expedici&oacute;n del Certificado</legend-->
                <div class="field_label">
                    <label for="resolution_date">Fecha de expedici&oacute;n</label></div>
                <div class="field_input">
                    <input type="text" name="resolution_date" id="resolution_date" class="user-input" runat="server" /></div>
                <input type="hidden" id="resolution_date_field" name="resolution_date_field" value="" />
                <button class="boton" id="resolution_date_submit">Guardar</button>
                <asp:Button runat="server" ID="btnPreview" OnClick="generatePDFResolucion" Text="Visualizar Certificado" CssClass="boton" />

                <!--/fieldset-->
            </div>
            <%} %>
            <%if (registrar_fecha_de_notificacion_permission)
            { %>
            <!--div id="notification_date_register">
                    <fieldset>
                        <legend>Registro de la fecha de la notificaci&oacute;n</legend>
                        <div class="field_label"><label for="notification_date">Fecha de la notificaci&oacute;n de la solicitud</label></div>
                        <div class="field_input"><input type="text" name="notification_date" id="notification_date" class="user-input" runat="server"/></div>
                        <input type="hidden" id="notification_date_field" name="notification_date_field" value="" />
                        <button class="boton" id="notification_date_submit">Guardar</button>
                    </fieldset>
                </div-->
            <%} %>
            <%if (registrar_fecha_de_resolucion_permission && project_state ==9)//// todo se debe poder actualizar antes de probar???
            { %>
            <!--div id="resolution_date2_register">
                    <fieldset>
                        <legend>Registro de la fecha de la resoluci&oacute;n aclaratoria</legend>
                        <div class="field_label"><label for="resolution_date2">Fecha de la resoluci&oacute;n de la solicitud</label></div>
                        <div class="field_input"><input type="text" name="resolution_date2" id="resolution_date2" class="user-input" runat="server"/></div>
                        <input type="hidden" id="resolution_date2_field" name="resolution_date2_field" value="" />
                        <button class="boton"  id="resolution_date2_submit">Guardar</button>
                    </fieldset>
                </div-->
            <%} %>


            <%if (registrar_fecha_de_notificacion_permission && project_state == 9)
            { %>
            <!--div id="notification_date2_register">
                    <fieldset>
                        <legend>Registro de la fecha de la notificaci&oacute;n aclaratoria</legend>
                        <div class="field_label"><label for="notification_date2">Fecha de la notificaci&oacute;n de la resoluci&oacute;n aclaratoria</label></div>
                        <div class="field_input"><input type="text" name="notification_date2" id="notification_date2" class="user-input" runat="server"/></div>
                        <input type="hidden" id="notification_date2_field" name="notification_date2_field" value="" />
                        <button class="boton" id="notification_date2_submit">Guardar</button>
                    </fieldset>
                </div-->
            <%} %>

            <% } %>
        </fieldset>
    </div>

    <div class="row" style="width: 100% !important;">
        <%if (project_state >=2 && (acciones_finales || user_role == 3)){ %>
        <div class="col-6">

            <asp:Panel runat="server" ID="panelRazonRechazo" Visible="true">
                <fieldset>
                    <legend>
                        <label for="txtRazonesRechazo">Razones de No aprobación <span class="form-required"></span></label>
                    </legend>
                    <div class="form-item" style="width: 90%px">

                        <asp:TextBox ID="txtRazonesRechazo" runat="server" Enabled="true" Width="98%" Height="200px" MaxLength="2000" TextMode="MultiLine"></asp:TextBox>
                        <asp:Button CssClass="boton" runat="server" Font-Size="16px" ID="btnVerCarta" Text="Ver carta" OnClick="btnVerCarta_Click" />
                        <asp:Button CssClass="boton" runat="server" Font-Size="16px" ID="btnGuardarCarta" Text="Guardar" OnClick="btnGuardarCarta_Click" />
                        <br />
                        <span style="color: red;">
                            <asp:Label ID="lblMsgRazonesRechazo" runat="server"></asp:Label></span>
                        <br />

                    </div>
                </fieldset>
            </asp:Panel>
        </div>
        <%}%>
        <div class="col-6">
            <div style="background-color: white;">
                <%if (acciones_finales){ %>
                <asp:Label runat="server" ID="lblErrorAprobacion" Text="" ForeColor="Red"></asp:Label>
                <fieldset class="fieldset-final">
                    <legend>Acciones Finales</legend>
                    <asp:Label runat="server" ID="lblSubsanado" Text="Proyecto con subsanación habilitada" BackColor="Orange" Font-Bold="true" Visible="false"></asp:Label>
                    <br />
                    <br />
                    <asp:Panel ID="pnlDatosSubsanacion" runat="server">
                        <asp:Label runat="server" ID="lblFechaSubsanacion" Font-Bold="true"></asp:Label>
                        <br />
                        <asp:Label runat="server" ID="lblObservacionesSubsanacion" Font-Bold="true"></asp:Label>
                        <br />
                        <asp:Label runat="server" ID="lblFechaEnvioSubsanacion" Font-Bold="true"></asp:Label>
                    </asp:Panel>
                    <br />
                    <div style="width: 250px;">
                        <div>
                            <asp:Button runat="server" ID="btnHabilitarSubsanacion" Font-Size="16px" Text="Habilitar subsanación" CssClass="boton" OnClick="btnHabilitarSubsanacion_Click" OnClientClick="$('#loading').show();" />


                        </div>
                        <%--boton de aprobar solicitud--%>
                        <%if (project_state !=9 && project_state != 10 && user_role == 4 ){ %>
                        <div id="aprobarsolicitud1_div" style="height: 30px; text-align: center; line-height: 30px" class="accion_finalizar final_link"><span>Aprobar y Enviar Certificado</span><input type="hidden" id="aprobarsolicitud1_field" name="aprobarsolicitud1_field" value="" /></div>
                        <br />
                        <%}%>
                        <%if (project_state >= 4 && project_state !=9 && project_state != 10 ){ %>
                        <div style="height: 30px; text-align: center; line-height: 30px" id="rechazarsolicitud1_div" class="accion_finalizar final_link">
                            <span>Enviar carta de rechazo</span>
                            <input type="hidden" id="rechazarsolicitud1_field" name="rechazarsolicitud1_field" value="" />
                        </div>
                        <br />

                        <%}%>
                    </div>

                </fieldset>

                <%if (project_state !=9 && project_state != 10 && (user_role == 4 || user_role == 2 )){ %>
                <fieldset>
                    <legend>Cancelar Solicitud</legend>
                    <div class="warning">solo se cancela por solicitud del productor</div>
                    <div id="cancelarsolicitud1_div" style="width: 250px; height: 35px; text-align: center; line-height: 35px" class="accion_finalizar final_link">
                        <span>Cancelar solicitud</span>
                        <input type="hidden" id="cancelarsolicitud1_field" name="cancelarsolicitud1_field" value="" />
                    </div>
                </fieldset>
                <%}%>
                <%}%>
            </div>
        </div>

    </div>



    <div style="min-height: 10px !important; background-color: mistyrose; width: 100% !important;"></div>
    <div class="row" style="width: 100% !important;">




        <div class="col-6">
            <div id="resolution_date_register3">

                <% if (project_state == 10)
                  { %>
                <fieldset>
                    <legend>Carta de rechazo</legend>
                    <div>
                        <asp:LinkButton runat="server" Font-Size="16px" ID="Button1" Text="Ver carta de rechazo" OnClick="btnVerCarta_Click" />
                    </div>
                </fieldset>
                <%} %>

                <fieldset class="fieldset-final">
                    <legend>Fomulario de solicitud</legend>

                    <% if (showDescargarButton){ %>
                    <div>
                        <asp:LinkButton ID="LinkButton1" runat="server" Text="Descargar Fomulario de solicitud para firma " OnClick="generatePDFForm"></asp:LinkButton>
                    </div>
                    <% } %>

                    <% if ( path_request_form != null && path_request_form.Trim() != ""){ %>
                    <div>
                        <a target="_blank" href="<%=path_request_form%>">Formulario de solicitud firmado</a>
                    </div>
                    <%} %>

                    <% if ((user_role > 1 && user_role != 6) && (project_state == 2 || project_state == 3 || project_state == 4 ||
                project_state == 6 || project_state == 7 || project_state == 8 )){ %>
                    <div>
                        <asp:RadioButton runat="server" GroupName="frm" Text="formulario de solicitud sin revisar" ID="rdFormulariosinRevizar" Enabled="false" Checked="true" />
                        <br />
                        <asp:RadioButton runat="server" GroupName="frm" Text="formulario de solicitud aprobado" ID="rdFormularioAprobado" Enabled="false" />
                        <br />
                        <asp:RadioButton runat="server" GroupName="frm" Text="formulario de solicitud no aprobado" ID="rdFormularioNoAprobado" Enabled="false" /><br />
                    </div>
                    <div>
                        <asp:Button runat="server" ID="btnAprobarFormulario" Font-Size="16px" Text="Aprobar Formulario" CssClass="boton" OnClick="btnAprobarFormulario_Click" OnClientClick="$('#loading').show();" />
                        <asp:Button runat="server" ID="btnRechazarFormulario" Font-Size="16px" Text="Solicitar Cambiar Formulario" CssClass="boton" OnClick="btnRechazarFormulario_Click" OnClientClick="$('#loading').show();" />


                    </div>
                    <% } %>

                    <% if ((project_state == 1 || project_state == 5) && (user_role <= 1 && showAtachhForm))
              { %>
                    <br />
                    <div style="border-style: solid; border-color: lightblue; border-width: 1px;">
                        <div>
                            <b>Cargar formulario de solicitud firmado</b><br />
                            <span id="name_formulario_solicitud"><% if (path_request_form != "" && path_request_form != null)
                           { %>
                                <a target="_blank" href="<%=path_request_form%>">Formulario de solicitud firmado</a>
                                <%}
                                else
                                { %>Adjunte el Formulario de solicitud firmado<%} %></span>*
                        </div>
                        <div>
                            <div id='divFileUpload_formulario_solicitud'>
                                <input type='file' name='file' id='FileUpload_formulario_solicitud' style='display: none' />
                                <input type='button' style='width: 110px; height: 30px; background-color: darkblue; color: white;' id='btnFileUploadText'
                                    value='Seleccionar' onclick='FileUpload_formulario_solicitud.click();' />
                            </div>
                            <div class='progressbar' id='progressbar' style='width: 100px; display: none;'>
                                <div></div>
                            </div>
                        </div>

                    </div>
                    <% } %>



                    <% if (project_state < 9 && user_role > 1 && user_role != 6)
              { %>
                    <br />
                    <div style="border-style: solid; border-color: lightblue; border-width: 1px;">
                        <div>
                            <b>Cargar nuevamente formulario de solicitud firmado</b><br />
                            <span id="name_formulario_solicitud">Reemplazar el Formulario de solicitud firmado</span>
                        </div>
                        <div>
                            <div id='divFileUpload_formulario_solicitud'>
                                <input type='file' name='file' id='FileUpload_formulario_solicitud' style='display: none' />
                                <input type='button' style='width: 110px; height: 30px; background-color: darkblue; color: white;' id='btnFileUploadText'
                                    value='Seleccionar' onclick='FileUpload_formulario_solicitud.click();' />
                            </div>
                            <div class='progressbar' id='progressbar2' style='width: 100px; display: none;'>
                                <div></div>
                            </div>
                        </div>

                    </div>
                    <% } %>
                </fieldset>

                <%-- cargar hoja de transferencia --%>
                <fieldset class="fieldset-hoja-transferencia" id="FSHojaTransferencia" runat="server" visible=" true">
                    <legend>Notificación de resolución</legend>


                    <% if (path_hojaTransferencia != null && path_hojaTransferencia.Trim() != "")
                        { %>
                    <div>
                        <a target="_blank" href="<%=path_hojaTransferencia%>">Notificación de resolución</a>
                    </div>
                    <%} %>



                    <% if ((user_role > 1 && user_role != 6) && (project_state == 9 || project_state == 10))
                        { %>
                    <br />
                    <div style="border-style: solid; border-color: lightblue; border-width: 1px;">
                        <div>
                            <b>Cargar Notificación de resolución</b><br />
<%--                                                <span id="name_hoja_tansferencia"><% if (path_hojaTransferencia != "" && path_hojaTransferencia != null)
                           { %>
                          <a target="_blank" href="<%=path_hojaTransferencia%>">Hoja de transferencia</a>
                          <%}else{ %>Adjunte el Hoja de Transferencia<%} %></span>*--%>
                        </div>
                        <div>
                            <div id='divFileUpload_hoja_Transferencia'>
                                <input type='file' name='file' id='FileUpload_Hoja_Transferencia' style='display: none' />
                                <input type='button' style='width: 110px; height: 30px; background-color: darkblue; color: white;' id='btnFileUploadText'
                                  value='Seleccionar' onclick="FileUpload_Hoja_Transferencia.click();" />
                            </div>
                            <div class='progressbar' id='progressbar' style='width: 100px; display: none;'>
                                <div></div>
                            </div>
                        </div>

                    </div>
                    <% } %>
                </fieldset>
            </div>

            <asp:Button runat="server" class="boton" Style="float: left; font-size: 17px; margin-left: 10px;" OnClientClick="return confirm('¿Esta seguro de enviar la subsanación? una vez enviada no podra hacer ningun ajuste');"
                ID="btnEnviarSubsanacion" Text="Enviar subsanación" OnClick="btnEnviarSubsanacion_Click" Visible="false" />
        </div>
        <div class="col-6">

            <div id="resolution_date_register2">



                <fieldset class="fieldset-final">
                    <legend>Información Adicional</legend>
                    <ul>

                        <li>
                            <div class="field_label">
                                <strong>Link protegido de la obra:</strong><span class="required_field_text">*</span>
                            </div>
                            <div class="field_input">
                                <asp:TextBox name="infVisualizacion" TextMode="MultiLine" Width="90%" Height="50px"
                                    ID="infVisualizacion" class="user-input" runat="server"></asp:TextBox>
                            </div>
                        </li>

                        <li>
                            <div class="field_label">
                                <strong>Comentarios adicionales:</strong><span class="required_field_text"></span>
                            </div>
                            <div class="field_input">
                                <asp:TextBox name="comentarios_adicionales"
                                    TextMode="MultiLine" Width="90%" Height="50px"
                                    ID="comentarios_adicionales" class="user-input"
                                    runat="server"></asp:TextBox>
                            </div>
                        </li>
                        <li>
                            <% if (user_role != 6)
                            { %>
                            <asp:Button runat="server" CssClass="accion_finalizar final_link" ID="btnGuardarInfoAdicional" Width="400px" Text="Guardar comentarios adicionales y link protegido" OnClientClick="$('#loading').show();" OnClick="btnGuardarInfoAdicional_Click" ForeColor="White" />
                            <% } %>
                        
                        </li>
                        <li></li>

                        <% if (user_role != 6)
                            { %>
                        <li>
                            <asp:Panel runat="server" ID="pnlAdicionarOtrosAdjuntos">
                                <table>
                                    <tr>
                                        <td>
                                            <hr style="border-style: solid !important; display: block; border-color: lightgray !important; border-width: 1px !important; width: 100% !important;" />

                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="field_label"><strong>Adjuntos Adicionales:</strong><span class="required_field_text"></div>
                                            </span></td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <img src="images/pdf.png" width="25px">
                                            Archivo:
                                            <asp:FileUpload runat="server" ID="fileOtrosAdjuntos" /></td>
                                    </tr>
                                    <tr>
                                        <td>Descripción:
                                            <asp:TextBox Style="width: 90%" runat="server" ID="txtOtrosAdjuntos"> </asp:TextBox></td>

                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <asp:Button runat="server" CssClass="accion_finalizar final_link" ID="btnCargarArchivo" ForeColor="White" Width="150px" Text="Cargar Archivo PDF" OnClick="btnCargarARchivo_Click" OnClientClick="$('#loading').show();" />
                                        </td>

                                        <tr>
                                            <td colspan="3">
                                                <asp:Label runat="server" ForeColor="Red" ID="lblErrorAdjuntos"></asp:Label></td>

                                        </tr>
                                </table>
                            </asp:Panel>
                            <asp:GridView runat="server" ID="grdOtrosAdjuntos" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="cod_adjunto_projecto" DataSourceID="SqlDataSourceOtrosAdjuntos" ForeColor="#333333" GridLines="None">
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                <Columns>

                                    <asp:TemplateField ItemStyle-Width="350px" HeaderText="Otros Adjuntos">
                                        <ItemTemplate>
                                            <asp:HyperLink runat="server" ID="HyperLink1" Target="_blank" ToolTip='<%# Eval("nombre_original") %>'
                                                ImageUrl='images/pdf.png' Text='<%# "Adjunto - "+Eval("descripcion") %>' NavigateUrl='<%# Eval("url_adjunto") %>'>
                                
                                            </asp:HyperLink>
                                            <asp:HyperLink runat="server" ID="lnkOtrosAdjuntos" Target="_blank" ToolTip='<%# Eval("nombre_original") %>'
                                                Text='<%# "Adjunto - "+Eval("descripcion") %>' NavigateUrl='<%# Eval("url_adjunto") %>'>                                
                                            </asp:HyperLink>

                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Button runat="server" Text="Eliminar" OnClientClick="return confirm('Esta seguro de eliminar este adjunto?');"
                                                ID="btnQuitarADjuntoADicional" OnClick="btnQuitarADjuntoADicional_Click"
                                                Visible='<%# pnlAdicionarOtrosAdjuntos.Visible %>'
                                                CommandArgument='<%# Eval("cod_adjunto_projecto") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                </Columns>
                                <EditRowStyle BackColor="#999999" />
                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="LightGray" Font-Bold="True" ForeColor="Gray" />
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
                        <% } %>
                    </ul>
                </fieldset>

            </div>
        </div>
    </div>

    <div style="min-height: 10px !important; background-color: lightyellow; width: 100% !important;"></div>

    <div class="row" style="width: 100%;">

        <div class="col-6">
            <% if (user_role != 6)
                { %>
            <asp:Panel runat="server" ID="pnlAclaraciones" Visible="false">
                <fieldset>
                    <legend>Carta de aclaraciones</legend>

                    <div class="field_label"><b>Texto adicional </b></div>
                    <asp:TextBox runat="server" Width="98%" Height="100px" ID="txtComplementoCartaAclaraciones" TextMode="MultiLine"></asp:TextBox>
                    <br />

                    <div class="field_label"><b>Texto sustituto </b></div>
                    <asp:TextBox runat="server" Width="98%" Height="100px" ID="txtSustitutoCartaAclaraciones" TextMode="MultiLine"></asp:TextBox>
                    <br />
                    <% if (user_role != 6)
                        { %>
                    <asp:Button runat="server" Font-Size="16px" OnClientClick="$('#loading').show();" Style="width: 230px" class="boton" Text="Guardar carta de aclaraciones" ID="btnGuardarTExtoAdicional" OnClick="btnGuardarTExtoAdicional_Click" />
                    <% } %>
                </fieldset>
            </asp:Panel>
            <% } %>
        </div>
        <div class="col-6">

            <%if (project_state >= 5 && user_role <= 1)
                { %>
            <br />
            <asp:Panel runat="server" ID="pnlObservacionesAclaracion" Visible="false">
                <div style="border-color: lightblue; border-style: solid; border-width: 1px; padding: 1%;">
                    <div>
                        <div class="field_label">
                            <label for="schedulefilmview_result">Resultado de la visualización</label></div>
                        <asp:TextBox Width="350px" Height="45px" runat="server" ReadOnly="true" TextMode="MultiLine" ID="schedulefilmview_result2"></asp:TextBox>

                    </div>
                    <br />
                    <asp:Panel runat="server" ID="pnlLargos">
                        <div>
                            <div class="field_label">
                                <label for="schedulefilmview_result2">Datos persona de contacto para visualización de la obra</label></div>
                            <asp:TextBox Width="350px" Height="45px" runat="server" TextMode="MultiLine" ID="txtRespuestaVisualizacion"></asp:TextBox>
                        </div>
                    </asp:Panel>
                    <asp:Label runat="server" ID="lblErrorRespuestaVisualizacion" ForeColor="Red"></asp:Label>
                    <asp:Button CssClass="boton" runat="server" ID="btnGuardarRespuestaVisualizacion"
                        Text="Guardar Respuesta visualización" OnClick="btnGuardarRespuestaVisualizacion_Click" />
                </div>
            </asp:Panel>
            <%} %>

            <%if ((project_state > 1 && user_role > 1 && user_role != 6))
                { %>
            <div id="schedulefilmview">
                <fieldset>
                    <legend>Programación y resultado de la visualización  de la obra</legend>
                    <div>


                        <%if ((project_state != 9))
                            { %>
                        <asp:RadioButton runat="server" ID="rdSinRevizarVisualizacion" Checked="true" Text="visualización sin revisar" GroupName="vis" />
                        <br />
                        <asp:RadioButton runat="server" ID="rdSolicitarAclaracionesVisualizacion" Text="Solicitar aclaraciones de la visualización" GroupName="vis" />
                        <br />
                        <asp:RadioButton runat="server" ID="rdInformacionCorrectaVisualizacion" Text="Información correcta de la visualización" GroupName="vis" />
                    </div>
                    <%}%>

                    <%if ((project_state == 9))
                        { %>

                    <label>Información correcta de la visualización!</label>
                    <br />
                    <br />
                    <%}%>



                    <div class="field_label">
                        <label for="schedulefilmview_date">Fecha de cita de visualización de la obra yyyy-MM-dd hh:mm</label></div>
                    <div class="field_input">
                        <input type="text" name="schedulefilmview_date" id="schedulefilmview_date" class="user-input" runat="server" /></div>
                    <div class="field_label">
                        <label for="schedulefilmview_result">Resultado de la visualización</label></div>
                    <div class="field_input">
                        <textarea style="width: 98%; min-height: 100px;" rows="3" cols="70" name="schedulefilmview_result" id="schedulefilmview_result" class="user-input" runat="server"></textarea>
                    </div>

                    <asp:Panel runat="server" ID="pnlLargos2">
                        <asp:Panel runat="server" ID="pnlRespuestaVisualizacionProductor" Visible="false">
                            <div class="field_label">
                                <label for="schedulefilmview_result2">Datos persona de contacto para visualización de la obra</label></div>
                            <asp:TextBox Width="98%" Height="45px" runat="server" TextMode="MultiLine" ID="txtRespuestaVisualizacion2" ReadOnly="true"></asp:TextBox>
                        </asp:Panel>
                    </asp:Panel>


                    <input type="hidden" id="schedulefilmview_field" name="schedulefilmview_field" value="" />

                    <%if ((project_state != 9 && project_state != 10))
                        {%>
                    <button id="schedulefilmview_submit" onclick="$('#loading').show();" class="boton">Guardar informaci&oacute;n de la visualizaci&oacute;n  de la obra</button>
                    <%}%>
                </fieldset>
            </div>
            <%} %>
        </div>

    </div>


    <%if (project_state >= 4 && (user_role == 2 || user_role == 3 || user_role == 4 || user_role == 5))
        {%>
    <div class="row" style="width: 100%;">
        <div class="col-6">

            <asp:Panel runat="server" ID="Panel1" Visible="true">
                <fieldset>
                    <legend>Reenvio de Notificaciones</legend>

                    <asp:Button runat="server" Font-Size="16px" OnClientClick="$('#loading').show();" class="boton" Text="Reenviar carta de aclaraciones" ID="btnReenviarNotiAclaraciones" OnClick="btnReenviarNotiAclaraciones_Click" Width="260px" />
                    <br />
                    <asp:Button runat="server" Font-Size="16px" OnClientClick="$('#loading').show();" class="boton" Text="Reenviar correo de rechazo" ID="btnReenviarRechazo" OnClick="btnReenviarRechazo_Click" Width="260px" />
                    <br />
                    <asp:Button runat="server" Font-Size="16px" OnClientClick="$('#loading').show();" class="boton" Text="Reenviar correo de aprobado" ID="btnReenviarNotiAprobado" OnClick="btnReenviarNotiAprobado_Click" Width="260px" />
                    <asp:Label runat="server" ID="lblMsgReenviarNotificaciones"></asp:Label>
                </fieldset>
            </asp:Panel>
        </div>

    </div>

    <% } %>



    <%if (es_super_admin == 1)
        {%>
    <div class="row" style="width: 100%;">
        <div class="col-6">

            <asp:Panel runat="server" ID="pnSuperAdmin" Visible="true">
                <fieldset>
                    <legend>Opciones de Super Administrador</legend>
                    Estado:
                    <asp:TextBox runat="server" ID="txtEstadoSuperAdmin"></asp:TextBox>
                    <asp:Button runat="server" Font-Size="16px" Style="width: 230px" class="boton" Text="Actualizar Estado" ID="btnActualizarEstado" OnClick="btnActualizarEstado_Click" />

                    <br />
                    Archivo:
                    <asp:FileUpload runat="server" ID="FileUploadCertificado" />
                    <asp:Button runat="server" Font-Size="16px" Style="width: 230px" class="boton" Text="SubirCertificado" ID="SubirCertificado" OnClick="SubirCertificado_Click" />
                    <asp:Label runat="server" ID="lblResultadoSuperAdmin"></asp:Label>


                    <br />

                </fieldset>
            </asp:Panel>
        </div>
    </div>


    <% } %>




    <div class="row" style="width: 100%">
        <div class="col-12">
            <%if (project_state == 2 && deshacer_envio_solicitud_permission)
                { %>
            <div id="deshacerenvio" style="width: 250px !important;"><span>Deshacer el env&iacute;o de la solicitud</span><input id="specialaction" name="specialaction" type="hidden" value="" /></div>
            <%} %>
        </div>
    </div>








    <br />
    <br />
    <br />
    <br />




    <uc1:cargando ID="cargando1" runat="server" />
    </div>
</div>
</div>

    <asp:Label runat="server" ID="lblInvicibleSubsanar"></asp:Label>
    <cc1:ModalPopupExtender ID="popupSubsanar" runat="server"
        BackgroundCssClass="modalBackground"
        TargetControlID="lblInvicibleSubsanar" DropShadow="true"
        PopupControlID="pnlSubsanar"
        CancelControlID="btnCancelarSubsanacion">

        <Animations>
             <OnHiding>
            <ScriptAction Script="validarPostBack();" />
             </OnHiding>
        </Animations>

    </cc1:ModalPopupExtender>
    <asp:Panel runat="server" Style="padding: 10px 10px 10px 10px;" ID="pnlSubsanar" Width="590px" Height="500px" BackColor="White">
        <asp:UpdatePanel ID="upnNuevaSubsanacion" runat="server">
            <ContentTemplate>
                <asp:HiddenField runat="server" ID="hdSubsanacionEnviada" Value="" ClientIDMode="Static" />
                <h1>Habilitar subsanación</h1>
                <div class="card m-1">
                    <div class="card-body">


                        <div>
                            <label class="form-label" style="font-size: 17px;">Fecha y hora limite que tiene el productor para responder</label>
                            <dx:ASPxDateEdit runat="server" EditFormat="DateTime" Theme="MetropolisBlue" EnableTheming="true" Width="230" Height="26" Font-Size="13" ID="calFechaSubsanacion">
                                <TimeSectionProperties Visible="true">
                                </TimeSectionProperties>
                                <ClearButton DisplayMode="Always">
                                </ClearButton>
                            </dx:ASPxDateEdit>
                        </div>
                        <br />
                        <div class="field_input">
                            <label class="form-label" style="font-size: 17px;">Observaciones de la subsanación (Estas observaciones solo los vera el ministerio)</label>
                            <asp:TextBox ID="txtObservacionesSubsanacion" runat="server" TextMode="MultiLine" Style="height: 140px; width: 500px;"></asp:TextBox>
                        </div>
                        <br />
                        <div class="alert alert-warning" role="alert">
                            Tenga en cuenta en establecer un horario entre las 8 a.m. y las 5 p.m. entre dias habiles
                        </div>
                    </div>
                    <div class="card-footer">
                        <asp:Label runat="server" ID="lblErrorSubsanacion" Text="" ForeColor="Red" Style="font-size: 17px;"></asp:Label>
                        <br />
                        <br />
                        <br />
                        <asp:Button CssClass="boton" runat="server" ID="btnGuardarSubsanacion" Text="Aceptar" OnClick="btnGuardarSubsanacion_Click" />
                        <asp:Button CssClass="boton" runat="server" ID="btnCancelarSubsanacion" Text="Cancelar" />
                    </div>
                </div>

            </ContentTemplate>

        </asp:UpdatePanel>
    </asp:Panel>
</asp:Content>
