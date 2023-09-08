<%@ Page Title="Edici&oacute;n de una solicitud" Language="C#" MasterPageFile="~/Site.master" ValidateRequest="false"
    AutoEventWireup="true" CodeBehind="DatosProyecto.aspx.cs" Inherits="CineProducto.DatosProyecto" %>

<%@ Register src="usercontrols/cargando.ascx" tagname="cargando" tagprefix="uc1" %>


<asp:Content ID="TitleContent" runat="server" ContentPlaceHolderID="TitleContent"> Trámite Reconocimiento Como Obra Nacional - Datos de la Obra - Mincultura</asp:Content>
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
    <script type="text/javaScript">


        $(document).ready(function () {
            scroll();
            autosize($('#<%=project_synopsis.ClientID %>'));
            autosize($('#<%=project_recording_sites.ClientID %>'));
            autosize($('#<%=project_filming_date_obs.ClientID %>'));
            autosize($('#<%=project_development_lab_info.ClientID %>'));
            autosize($('#<%=project_preprint_store_info.ClientID %>'));
            autosize($('#<%=solicitud_aclaraciones.ClientID %>'));
            autosize($('#<%=informacion_correcta.ClientID %>'));
            autosize($('#<%=producer_clarifications_field.ClientID %>'));

            autosize($('#<%=comentarios_adicionales.ClientID %>'));

            habilitarTracker();
            /**
            * Incluimos codigo js para el proceso de adjuntos de la seccion datos del proyecto
            */
            <asp:Repeater id="AttachmentRepeater2" runat="server">
                <ItemTemplate>

                    $(function () {

                        $('#FileUpload<%# Eval("attachment_id")%>').fileupload({
                        url: 'FileUploadHandler.ashx?upload=start&folder=<%= Page.ResolveClientUrl("~/uploads")%>' +'/<%=project_id %>' +'&attachment_id=<%# Eval("attachment_id")%>',
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
                                url: '<%=Page.ResolveClientUrl("~/Default.aspx/getAttachmentStatus") %>',
                                    data: '{pAttachment_id:<%# Eval("attachment_id")%>,idproyecto:<%=project_id%>,uploadedfilename:"' + response + '",producer_id:0}',
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
            /* Fin adjuntos js */

            $('.currencyformat').formatCurrency({ roundToDecimalPlace: 0 });
            $('.percentageformat').change(function () {
                $(this).val($(this).val() + '%');
            });

            $('#<%=projectTypeDDL.ClientID %>').change(function () {
                if ($(this).val() == 3) {
                    $('.field_text_message').html("");
                    $('.field_text_message').removeClass("active_message");
                    alert("Recuerde que debe entregar una copia en DVD de la obra en la Dirección de Audiovisuales, Cine y Medios Interactivos.");
                } else {
                    $('.field_text_message').addClass("active_message");
                    $('.field_text_message').html("Recuerde que debe programar una cita para visionar la pelicula en la Dirección de Audiovisuales, Cine y Medios Interactivos")
                }
            });
            $('#<%=productionTypeDDL.ClientID %>').change(function () {
                $('#submit').val('combo');
                $('#submit').click();
            });
            $('.currencyformat').blur(function () {
                $('.currencyformat').formatCurrency({ roundToDecimalPlace: 0 });
                calculateTotalCostWithoutPromotion();
            });

   //     $('#<%=project_filming_start_date.ClientID %>').datepicker({dateFormat: "yy-mm-dd",changeMonth: true,changeYear: true,}).datepicker("setDate", "0");
   //     $('#<%=project_filming_end_date.ClientID %>').datepicker({dateFormat: "yy-mm-dd",changeMonth: true,changeYear: true,}).datepicker("setDate", "0");

            $('#<%=project_filming_start_date.ClientID %>').datepicker({ dateFormat: "yy-mm-dd", changeMonth: true, changeYear: true, });
            $('#<%=project_filming_end_date.ClientID %>').datepicker({ dateFormat: "yy-mm-dd", changeMonth: true, changeYear: true, });
            /* Calcula el costo total y lo presenta cuando se carga la pagina */
            calculateTotalCostWithoutPromotion();

            /* Verifica el tipo de producción y presenta o esconde el campo de cantidad de productores extranjeros */
            setProducersQtyVisibility();
            setPremioVisibility();
            /* Crea la función de presentación del tooltip en todos los campos */
            $('#<%=project_name.ClientID %>').tooltip({ bodyHandler: function () { return '<asp:Literal id="tooltip_project_name" runat="server"></asp:Literal>'; }, showURL: false });
            $('#<%=project_provisional_name.ClientID %>').tooltip({ bodyHandler: function () { return '<asp:Literal id="tooltip_project_provisional_name" runat="server"></asp:Literal>'; }, showURL: false });
            $('#<%=productionTypeDDL.ClientID %>').tooltip({ bodyHandler: function () { return '<asp:Literal id="tooltip_productionTypeDDL" runat="server"></asp:Literal>'; }, showURL: false });
            $('#<%=project_domestic_producer_qty.ClientID %>').tooltip({ bodyHandler: function () { return '<asp:Literal id="tooltip_project_domestic_producer_qty" runat="server"></asp:Literal>'; }, showURL: false });
            $('#<%=project_foreign_producer_qty.ClientID %>').tooltip({ bodyHandler: function () { return '<asp:Literal id="tooltip_project_foreign_producer_qty" runat="server"></asp:Literal>'; }, showURL: false });
            $('#<%=project_total_cost_desarrollo.ClientID %>').tooltip({ bodyHandler: function () { return '<asp:Literal id="tooltip_project_total_cost_desarrollo" runat="server"></asp:Literal>'; }, showURL: false });
            $('#<%=project_total_cost_preproduccion.ClientID %>').tooltip({ bodyHandler: function () { return '<asp:Literal id="tooltip_project_total_cost_preproduccion" runat="server"></asp:Literal>'; }, showURL: false });
            $('#<%=project_total_cost_produccion.ClientID %>').tooltip({ bodyHandler: function () { return '<asp:Literal id="tooltip_project_total_cost_produccion" runat="server"></asp:Literal>'; }, showURL: false });
            $('#<%=project_total_cost_posproduccion.ClientID %>').tooltip({ bodyHandler: function () { return '<asp:Literal id="tooltip_project_total_cost_posproduccion" runat="server"></asp:Literal>'; }, showURL: false });
            $('#<%=project_total_cost_promotion.ClientID %>').tooltip({ bodyHandler: function () { return '<asp:Literal id="tooltip_project_total_cost_promotion" runat="server"></asp:Literal>'; }, showURL: false });
            $('#<%=projectTypeDDL.ClientID %>').tooltip({ bodyHandler: function () { return '<asp:Literal id="tooltip_projectTypeDDL" runat="server"></asp:Literal>'; }, showURL: false });
            $('#<%=projectGenreDDL.ClientID %>').tooltip({ bodyHandler: function () { return '<asp:Literal id="tooltip_projectGenreDDL" runat="server"></asp:Literal>'; }, showURL: false });
            $('#<%=project_synopsis.ClientID %>').tooltip({ bodyHandler: function () { return '<asp:Literal id="tooltip_project_synopsis" runat="server"></asp:Literal>'; }, showURL: false });
            $('#<%=project_recording_sites.ClientID %>').tooltip({ bodyHandler: function () { return '<asp:Literal id="tooltip_project_recording_sites" runat="server"></asp:Literal>'; }, showURL: false });
            $('#<%=project_duration_minutes.ClientID %>').tooltip({ bodyHandler: function () { return '<asp:Literal id="tooltip_project_duration_minutes" runat="server"></asp:Literal>'; }, showURL: false });
            $('#<%=project_duration_seconds.ClientID %>').tooltip({ bodyHandler: function () { return '<asp:Literal id="tooltip_project_duration_seconds" runat="server"></asp:Literal>'; }, showURL: false });
            $('#<%=project_filming_start_date.ClientID %>').tooltip({ bodyHandler: function () { return '<asp:Literal id="tooltip_project_filming_start_date" runat="server"></asp:Literal>'; }, showURL: false });
            $('#<%=project_filming_end_date.ClientID %>').tooltip({ bodyHandler: function () { return '<asp:Literal id="tooltip_project_filming_end_date" runat="server"></asp:Literal>'; }, showURL: false });
            $('#<%=project_filming_date_obs.ClientID %>').tooltip({ bodyHandler: function () { return '<asp:Literal id="tooltip_project_filming_date_obs" runat="server"></asp:Literal>'; }, showURL: false });
            $('#<%=grdFormatosRodaje.ClientID %>').tooltip({ bodyHandler: function () { return '<asp:Literal id="tooltip_projectShootingFormatCBL" runat="server"></asp:Literal>'; }, showURL: false });
            $('#<%=projectExhibitionFormatCBL.ClientID %>').tooltip({ bodyHandler: function () { return '<asp:Literal id="tooltip_projectExhibitionFormatCBL" runat="server"></asp:Literal>'; }, showURL: false });
            $('#<%=project_development_lab_info.ClientID %>').tooltip({ bodyHandler: function () { return '<asp:Literal id="tooltip_project_development_lab_info" runat="server"></asp:Literal>'; }, showURL: false });
            $('#<%=project_preprint_store_info.ClientID %>').tooltip({ bodyHandler: function () { return '<asp:Literal id="tooltip_project_preprint_store_info" runat="server"></asp:Literal>'; }, showURL: false });
            $('#<%=project_legal_deposit_yes.ClientID %>').tooltip({ bodyHandler: function () { return '<asp:Literal id="tooltip_project_legal_deposit_yes" runat="server"></asp:Literal>'; }, showURL: false });
            $('#<%=project_legal_deposit_no.ClientID %>').tooltip({ bodyHandler: function () { return '<asp:Literal id="tooltip_project_legal_deposit_no" runat="server"></asp:Literal>'; }, showURL: false });
            $('#<%=project_percentage.ClientID %>').tooltip({ bodyHandler: function () { return '<asp:Literal id="tooltip_project_percentage" runat="server"></asp:Literal>'; }, showURL: false });

            /* Agrega la clase user-input a todos los input que diligencia el usuario con el fin de poderlos desactivar y activar */
            $('#<%=project_name.ClientID %>').addClass("user-input");
            $('#<%=project_provisional_name.ClientID %>').addClass("user-input");
            $('#<%=productionTypeDDL.ClientID %>').addClass("user-input");
            $('#<%=cmbIdiomaPrincipal.ClientID %>').addClass("user-input");
            $('#<%=cmbPremio.ClientID %>').addClass("user-input");
            $('#<%=premioTxt.ClientID %>').addClass("user-input");

        $('#<%=project_percentage.ClientID %>').addClass("user-input");        
        $('#<%=txtOtro_formato_exibicion_detail.ClientID %>').addClass("user-input");

        $('#<%=project_domestic_producer_qty.ClientID %>').addClass("user-input");
        $('#<%=project_foreign_producer_qty.ClientID %>').addClass("user-input");
        $('#<%=project_total_cost_desarrollo.ClientID %>').addClass("user-input");
        $('#<%=project_total_cost_preproduccion.ClientID %>').addClass("user-input");
        $('#<%=project_total_cost_produccion.ClientID %>').addClass("user-input");
        $('#<%=project_total_cost_posproduccion.ClientID %>').addClass("user-input");
        $('#<%=project_total_cost_promotion.ClientID %>').addClass("user-input");
        $('#<%=projectTypeDDL.ClientID %>').addClass("user-input");
        $('#<%=projectGenreDDL.ClientID %>').addClass("user-input");
        $('#<%=project_synopsis.ClientID %>').addClass("user-input");
        $('#<%=project_recording_sites.ClientID %>').addClass("user-input");
        $('#<%=project_duration_minutes.ClientID %>').addClass("user-input");
        $('#<%=project_duration_seconds.ClientID %>').addClass("user-input");
        $('#<%=project_filming_start_date.ClientID %>').addClass("user-input");
        $('#<%=project_filming_end_date.ClientID %>').addClass("user-input");
        $('#<%=project_filming_date_obs.ClientID %>').addClass("user-input");
        $('#<%=grdFormatosRodaje.ClientID %>').addClass("user-input");
        $('#<%=projectExhibitionFormatCBL.ClientID %>').addClass("user-input");
        $('#<%=project_development_lab_info.ClientID %>').addClass("user-input");
        $('#<%=project_preprint_store_info.ClientID %>').addClass("user-input");
        $('#<%=project_legal_deposit_yes.ClientID %>').addClass("user-input");
        $('#<%=project_legal_deposit_no.ClientID %>').addClass("user-input");
            $('.tooltip').tooltipster();
            $('#<%=productionTypeDDL.ClientID %>').change(function () {
                
                $('#loading').show();
                setProducersQtyVisibility();
            });

            $('#<%=cmbPremio.ClientID %>').change(function () {
                
                setPremioVisibility();
            });

            

            $('#<%=projectTypeDDL.ClientID %>').change(function () {
                $('#loading').show();
                checkShowOtherFilmingFormat();
                $('#loading').hide();
        });

            $('#<%=projectGenreDDL.ClientID %>').change(function () {
                $('#loading').show();
                labelAnimacion();
                checkProjectTypeOptions();
                $('#loading').hide();
        });
        
        //$('#otro_formato_rodaje').change(function () {
        //    checkShowOtherFilmingFormat();
        //});

        /* Hace la verificación inicial de presentación de los textarea inferiores */
        checkShowOtherFilmingFormat();
        checkPreprintStoreInfoVisibilty();
        checkProjectTypeOptions();
        labelAnimacion();
        <%
        if (showAdvancedForm || (project_state_id == 5 && section_state_id != 10)
            || ((project_state_id == 2 || project_state_id == 3 || project_state_id == 4 
            || project_state_id == 6 || project_state_id == 7 || 
            project_state_id >= 8) && user_role <= 1))
        { %>
            DisableEnableFormCustom(true, 'desactivar');
      
           
        <%
        }
        %>    

            if ($("#hdHabilitarForm").val() == "Activo") {
                DisableEnableFormCustom(false, 'activar');
               $("#hdHabilitarForm").val("");
            }

            $('#loading').hide();
    });

        function validateAprobar(){
            alert('no no no');
            return false;
        }

        function DisableEnableFormCustom(accion, cambio){
            DisableEnableForm(accion,cambio);
            if ( cambio == 'desactivar' ) {
                document.getElementById('<%=txtOtro_formato_exibicion_detail.ClientID %>').setAttribute("disabled", "disabled");
                
                document.getElementById('<%=chkOtroFormatoExhibicion.ClientID %>').setAttribute("disabled", "disabled");

                var GridView = document.getElementById('<%=grdFormatosRodaje.ClientID %>');
                if (GridView.rows.length > 0) {
                    for (Row = 0; Row < GridView.rows.length; Row++) {
                        var control=GridView.rows[Row].cells[0].childNodes[1].childNodes[1].children[0].children[0].childNodes[1];
                        control.setAttribute("disabled", "disabled");
                        var c1 =control.parentElement.nextSibling.nextSibling;
                        var c2 =c1.childNodes[1];
                        c2.setAttribute("disabled", "disabled");
                        var c3 =c1.childNodes[3];
                        c3.setAttribute("disabled", "disabled");
                    }
                }
            } else {
                document.getElementById('<%=txtOtro_formato_exibicion_detail.ClientID %>').removeAttribute("disabled");
                document.getElementById('<%=chkOtroFormatoExhibicion.ClientID %>').removeAttribute("disabled");
                var GridView = document.getElementById('<%=grdFormatosRodaje.ClientID %>');
                if (GridView.rows.length > 0) {
                    for (Row = 0; Row < GridView.rows.length; Row++) {
                        var control=GridView.rows[Row].cells[0].childNodes[1].childNodes[1].children[0].children[0].childNodes[1];
                        control.removeAttribute("disabled");
                        var c1 =control.parentElement.nextSibling.nextSibling;
                        var c2 =c1.childNodes[1];
                        c2.removeAttribute("disabled");
                        var c3 =c1.childNodes[3];
                        c3.removeAttribute("disabled");
                    }
                }
            }


        }


    function calculateTotalCostWithoutPromotion() {
        /* Calculo del costo total */
        var total = $('#<%=project_total_cost_desarrollo.ClientID %>').asNumber() +
                    $('#<%=project_total_cost_preproduccion.ClientID %>').asNumber() +
                    $('#<%=project_total_cost_produccion.ClientID %>').asNumber() +
                    $('#<%=project_total_cost_posproduccion.ClientID %>').asNumber();
        $('#valor_total').html(total).formatCurrency({ roundToDecimalPlace: 0 });
    }

    function revertCurrencyFormat() {
        $('.currencyformat').formatCurrency({ roundToDecimalPlace: 0 });
        $('.currencyformat').toNumber();
    }

    function setProducersQtyVisibility() {
        if ($('#<%=productionTypeDDL.ClientID %>').val() == 2) {
            $('#cantidad_productores_extranjeros_block').show();
        }
        else {
            $('#cantidad_productores_extranjeros_block').hide();
        }
    }

        function setPremioVisibility() {
            if ($('#<%=cmbPremio.ClientID %>').val() == 1) {
                $('#DivPremioTxt').show();
            }
            else {
                $('#DivPremioTxt').hide();
            }
        }

 

    function checkShowOtherFilmingFormat() 
    {
        /* Verifica si la obra es largometraje o cortometraje */
        var combo= document.getElementById('<%=projectTypeDDL.ClientID %>');
        

        var project_type = parseInt(combo.options[combo.selectedIndex].value);

        /* Si el tipo de proyecto es cortometraje se oculta el checkbox y el input text
           de la opción de otro formato de rodaje.*/
        //if (project_type != 3) {
        //    $('#otro_formato_rodaje').hide();
        //    $('#otro_formato_rodaje_label').hide();
        //    $('#otro_formato_rodaje_detail').hide();
        //}
        //else {
        //    if ($('#otro_formato_rodaje:checked').val() == '19') 
        //    {
        //        $('#otro_formato_rodaje').show();
        //        $('#otro_formato_rodaje_label').show();
        //        $('#otro_formato_rodaje_detail').show();
        //    }
        //    else 
        //    {
        //        $('#otro_formato_rodaje').show();
        //        $('#otro_formato_rodaje_label').show();
        //        $('#otro_formato_rodaje_detail').hide();
        //    }
        //}
    }

        function checkPreprintStoreInfoVisibilty() 
        {
            var shootingFormatCounter = 0;
            var laboratorio = 0;
            var GridView = document.getElementById('<%=grdFormatosRodaje.ClientID %>');
                        
            if (GridView.rows.length > 0) {
                for (Row = 0; Row < GridView.rows.length; Row++) {
                    var control=GridView.rows[Row].cells[0].childNodes[1].childNodes[1].children[0].children[0].childNodes[1];
                    var dis="none";
                    if (control.checked ){   
                        dis="";
                        shootingFormatCounter++;
                        s=control.nextElementSibling.innerHTML;
                        if(s =="Fotoquímico (celuloide)"){
                            laboratorio++;
                        }
                    }
                    //
                    var c1 =control.parentElement.nextSibling.nextSibling;
                    var c2 =c1.childNodes[1];
                    c2.style.display=dis;
                    var c3 =c1.childNodes[3];
                    c3.style.display=dis;
                }
            }

            if (shootingFormatCounter==0) 
            {
                $('#project_preprint_store_info_container').hide();
            }else {
                $('#project_preprint_store_info_container').show();
            }

            if (laboratorio ==0) 
            {
                $('#project_development_lab_info_container').hide();
            }else {
                $('#project_development_lab_info_container').show();
            }
        }

        function checkOtroFormatoExhibicionChecked() 
        {
            var ch = document.getElementById('<%=chkOtroFormatoExhibicion.ClientID %>');
            if (!ch.checked) 
            {
                $('#<%=lblOtro_formato_exibicion_label.ClientID %>').hide();
                $('#<%=txtOtro_formato_exibicion_detail.ClientID %>').hide();
            }else{
                $('#<%=lblOtro_formato_exibicion_label.ClientID %>').show();
                $('#<%=txtOtro_formato_exibicion_detail.ClientID %>').show();
            }
        }



    /* Función que controla la validación de valores ingresados en el campo
    segundos de conjunto de campos de duración de la obra */
    function checkSecondsFieldValues()
    {
        var registeredValue = $('#<%=project_duration_seconds.ClientID %>').val();
        if(registeredValue < 0 || registeredValue >= 60)
        {
            alert("Por favor registre un número entre 0 y 59 en el campo de segundos, el valor registrado "+registeredValue+" no es válido");
            $('#<%=project_duration_seconds.ClientID %>').val('0');
        }
    }

        function labelAnimacion(){
            var a= $('#<%=projectGenreDDL.ClientID %>').val();

            if(a == '3')
            {
                $('#lblFechainicioRodaje').html('Fecha de inicio de producción:');
                $('#lblFechaFinRodaje').html('Fecha de fin de producción:');
                $('#lblFechaObsRodaje').html('Observaciones sobre las fechas de producción:');
            }else{
                $('#lblFechainicioRodaje').html('Fecha de inicio de rodaje:');
                $('#lblFechaFinRodaje').html('Fecha de fin de rodaje:');
                $('#lblFechaObsRodaje').html('Observaciones sobre las fechas de rodaje:');
            }
        }

    /* Función que elimina la opción de largometraje de 52 minutos cuando se selecciona un genero
    diferente a documental */
    function checkProjectTypeOptions()
    {
            var combo= document.getElementById('<%=projectTypeDDL.ClientID %>');
            var project_type = parseInt(combo.options[combo.selectedIndex].value);
        if($('#<%=projectGenreDDL.ClientID %>').val() == 2)
        {

            $('#<%=projectTypeDDL.ClientID %>').find("option[value='2']").show();
        }
        else
        {
         

            $('#<%=projectTypeDDL.ClientID %>').find("option[value='2']").hide();
         
        }
        $('#<%=projectTypeDDL.ClientID %>').val(project_type);
        }

        function runPostback() {
            $('#submit').val('combo');
            $("#submit").click();
            //alert('probando');
            //document.forms["form1"].submit();
        }

    </script>
    <div id="cine">
             
        <!-- Bloque de información contextual -->
        <div id="informacion-contextual">
            <div class="bloque">
                <strong>
                    <asp:Label ID="nombre_proyecto" runat="server"></asp:Label></strong></div>
            <div class="bloque">
                <strong>
                    <asp:Label ID="tipo_produccion" runat="server"></asp:Label></strong><br />
                <asp:Label ID="tipo_proyecto" runat="server"></asp:Label>
            </div>
            <div class="bloque">
                <strong>Productor:</strong><br />
                <asp:Label ID="nombre_productor" runat="server"></asp:Label></div>
            <div class="bloque">
                <asp:Label ID="opciones_adicionales" runat="server"></asp:Label><asp:Label runat="server" ID="lblCodProyecto" Visible="false"></asp:Label></div>
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
                 <li class="<%=tab_datos_formato_personal_css_class %>"><a href="DatosFormatoPersonal.aspx">Registro de personal <br />artístico y técnico   <%=tab_datos_formato_personal_revision_mark_image %></a></li>
                <!-- <li class="<%--=tab_datos_adjuntos_css_class --%>"><a href="DatosAdjuntos.aspx">Adjuntos<%--=tab_datos_adjuntos_revision_mark_image --%></a></li> -->
                <li class="<%=tab_datos_finalizacion_css_class %>"><a href="Finalizacion.aspx">Finalizaci&oacute;n</a></li>
            </ul>
        </div>
        <!-- End of Nav Div -->
        <form name="datos_proyecto" method="post" action="DatosProyecto.aspx" onsubmit="return validateAprobar();">
        <input type="hidden" id="shootingFormatMM" runat="server" />
        <uc1:cargando ID="cargando1" runat="server" />
               

            
  <asp:Panel runat="server" ID="pnlMensajeVisible" Visible="false">
      <asp:HiddenField runat="server" ClientIDMode="Static" ID="hdHabilitarForm" Value=""></asp:HiddenField>
      <div style="clear:left;"></div>
     <div class="warning">
            <p>
             Si modifica Tipo de Producción, Porcentajes de participación, Tipo de obra o Duración de la obra, las pestañas: Coproductores y Personal podrian sufrir cambios. Del mismo modo podria ser necesario, volver a cargar los respectivos adjuntos.  </p>
        </div>
      <div style="clear:left;"></div>
        </asp:Panel>

        <div id='Proyecto'>
            <fieldset>
                <legend>Datos de la obra</legend>
                <ul>
                    <li>
                        <div class="field_label">
                            T&iacute;tulo de la obra:<span class="required_field_text">*</span></div>
                        <div class="field_input">
                            <input type="text" id="project_name" name="project_name" runat="server" class="inputLargo user-input" />
                            
                        </div>
                    </li>

                    <li>
                        <div class="field_label">
                            T&iacute;tulos anteriores:</div>
                        <div class="field_input">
                            <input type="text" id="project_provisional_name" name="project_provisional_name" runat="server" class="inputLargo user-input" />
                        </div>
                    </li>

                    <li>
                        <div class="field_label">
                            Tipo de Producci&oacute;n:<span class="required_field_text">*</span></div>
                        <div class="field_input">
                            <asp:DropDownList ID="productionTypeDDL" runat="server" name="production_type" >
                            </asp:DropDownList>
                        </div>
                    </li>
                    <asp:Panel runat="server" ID="pnlTipoPersonal" Visible="false">
                    <li>
                        <div class="field_label">
                            Tipo de personal:<span class="required_field_text">*</span></div>
                        <div class="field_input">
                            <asp:DropDownList ID="personalTypeDDL" runat="server" name="personal_type" >
                            </asp:DropDownList>
                        </div>
                    </li>
                        </asp:Panel>
                    <li>
                        <div class="field_label">
                            Cantidad de productores nacionales:<span class="required_field_text">*</span></div>
                        <div class="field_input">
                            <input type="text" onchange ="runPostback();" id="project_domestic_producer_qty" name="project_domestic_producer_qty"
                                runat="server" /></div>
                    </li>
                 
                    <li id="cantidad_productores_extranjeros_block">
                        <div class="field_label">
                            Cantidad de productores extranjeros:<span class="required_field_text">*</span></div>
                        <div class="field_input">
                            <input type="text" onchange ="runPostback();" id="project_foreign_producer_qty" name="project_foreign_producer_qty"
                                runat="server" /></div>
                    </li>
                    <li>
                        <fieldset class="subgrupo-campos">
                            <legend>Costo discriminado de la obra</legend>
                            <ul>
                                <li>
                                    <div class="field_label">
                                        Valor etapa desarrollo:<span class="required_field_text">*</span></div>
                                    <div class="field_input">
                                        <input type="text" class="currencyformat" id="project_total_cost_desarrollo" name="project_total_cost_desarrollo"
                                            runat="server" /></div>
                                </li>
                                <li>
                                    <div class="field_label">
                                        Valor etapa preproducci&oacute;n:<span class="required_field_text">*</span></div>
                                    <div class="field_input">
                                        <input type="text" class="currencyformat" id="project_total_cost_preproduccion" name="project_total_cost_preproduccion"
                                            runat="server" /></div>
                                </li>
                                <li>
                                    <div class="field_label">
                                        Valor etapa producci&oacute;n:<span class="required_field_text">*</span></div>
                                    <div class="field_input">
                                        <input type="text" class="currencyformat" id="project_total_cost_produccion" name="project_total_cost_produccion"
                                            runat="server" /></div>
                                </li>
                                <li>
                                    <div class="field_label">
                                        Valor etapa posproducci&oacute;n:<span class="required_field_text">*</span></div>
                                    <div class="field_input">
                                        <input type="text" class="currencyformat" id="project_total_cost_posproduccion" name="project_total_cost_posproduccion"
                                            runat="server" /></div>
                                </li>
                                <li>
                                    <div class="field_label">
                                        <strong>Valor total (sin promoci&oacute;n):</strong></div>
                                    <div id="valor_total">
                                    </div>
                                </li>
                                
                                <li>
                                    <div class="field_label">
                                        Valor estimado promoción:<span class="required_field_text">*</span></div>
                                    <div class="field_input">
                                        <input type="text" class="currencyformat" id="project_total_cost_promotion" name="project_total_cost_promotion"
                                            runat="server" /></div>
                                </li>
                                <li>
                                    <div class="field_label">
                                        Porcentaje de participación colombiana:<span class="required_field_text">*</span></div>
                                    <div class="field_input">
                                        <input type="text" maxlength="7" class="percentageformat" id="project_percentage" name="project_percentage"
                                            runat="server" /></div>
                                </li>
                            </ul>
                        </fieldset>
                    </li>

                     <li>
                        <div class="field_label">
                         Idioma principal   <span class="required_field_text">*</span></div>
                        <div class="field_input">
                            <asp:DropDownList ID="cmbIdiomaPrincipal" runat="server" name="cmbIdiomaPrincipal" AppendDataBoundItems="True" DataSourceID="SqlDataSourceIdioma" DataTextField="nombre_idioma" DataValueField="cod_idioma">
                                <asp:ListItem Text="Seleccione" Value="0"></asp:ListItem>
                            </asp:DropDownList>
                            
                            
                            <asp:SqlDataSource ID="SqlDataSourceIdioma" runat="server" ConnectionString="<%$ ConnectionStrings:cineConnectionString %>" SelectCommand="SELECT [cod_idioma], [nombre_idioma] FROM [idioma]"></asp:SqlDataSource>
                            
                            
                         </div>
                    </li>

                    <li>
                        <div class="field_label">
                            Genero de obra<span class="required_field_text">*</span></div>
                        <div class="field_input">
                            <asp:DropDownList ID="projectGenreDDL" runat="server" name="project_genre">
                            </asp:DropDownList>                            
                            
                         </div>
                    </li>
                    <li>
                        <div class="field_label">
                            Tipo de obra<span class="required_field_text">*</span></div>
                        <div class="field_input">                            
                           <br />
                            <asp:DropDownList ID="projectTypeDDL" runat="server" name="project_type">
                            </asp:DropDownList>                            
                         </div>
                    </li>
                    <li>
                        <div class="field_label">
                            Sinopsis:<span class="required_field_text">*</span><br />
                            (M&aacute;ximo 3.000 caracteres,<br />
                            aproximadamente una p&aacute;gina)</div>
                        <div class="field_input">
                            <textarea name="project_synopsis" id="project_synopsis" class="user-input" rows="5"
                                cols="60" runat="server"></textarea></div>
                    </li>
                    <li>
                        <div class="field_label">
                            Tiene estímulos o premios de financiación (en cualquier etapa):<span class="required_field_text">*</span></div>
                         <div class="field_input">
                            <asp:DropDownList ID="cmbPremio" runat="server" name="cmbPremio">
                                <asp:ListItem Text="" Value="-1" />
                                <asp:ListItem Text="Si" Value="1" />
                                <asp:ListItem Text="No" Value="2" />
                            </asp:DropDownList>
                         </div>
                        </li>
                    <li>
                        <div id="DivPremioTxt">
                            <div class="field_label">
                            Estímulo o premios de financiación:<span class="required_field_text">*</span></div>
                        <div class="field_input">
                            <textarea name="premioTxt" id="premioTxt" class="user-input"
                                rows="3" cols="60" runat="server"></textarea></div>
                        </div>
                    </li>
                    <li>
                        <div class="field_label">
                            Lugares de filmaci&oacute;n:<span class="required_field_text">*</span></div>
                        <div class="field_input">
                            <textarea name="project_recording_sites" id="project_recording_sites" class="user-input"
                                rows="3" cols="60" runat="server"></textarea></div>
                    </li>
                    <li>
                        <div class="field_label">
                            Duraci&oacute;n de la obra:<span class="required_field_text">*</span></div>
                        <div class="field_input">
                            <input type="text" id="project_duration_minutes" name="project_duration_minutes"
                                class="input_duracion user-input" size="2" runat="server" />
                            Minutos:<input type="text" id="project_duration_seconds" name="project_duration_seconds"
                                onchange="checkSecondsFieldValues();" class="input_duracion" size="2" runat="server" />
                            Segundos</div><br /><br /><span class="field_text_message"></span>
                    </li>
                    <li>
                        <div class="field_label">
                           <label id="lblFechainicioRodaje">Fecha de inicio de rodaje:</label>
                             <span class="required_field_text">*</span></div>
                        <div class="field_input">
                            <input type="text" name="project_filming_start_date" id="project_filming_start_date"
                                class="user-input" runat="server" /></div>
                    </li>
                    <li>
                        <div class="field_label">
                            <label id="lblFechaFinRodaje">Fecha de fin de rodaje:</label>
                            <span class="required_field_text">*</span></div>
                        <div class="field_input">
                            <input type="text" name="project_filming_end_date" id="project_filming_end_date"
                                class="user-input" runat="server" /></div>
                    </li>
                    <li>
                        <div class="field_label">
                            <label id="lblFechaObsRodaje">Observaciones sobre las fechas de rodaje:</label></div>
                        <div class="field_input">
                            <textarea name="project_filming_date_obs" id="project_filming_date_obs" class="user-input"
                                rows="3" cols="60" runat="server"></textarea></div>
                    </li>
                    <li>
                        <div class="field_label">
                            Formato(s) de rodaje:<span class="required_field_text">*</span></div>
                        <div class="field_input">
                            <div id="formato_rodaje_input" style="padding: 0 0 20px 0;" runat="server">

                                <asp:GridView   ShowHeader="false" runat="server" ID="grdFormatosRodaje" 
                                    AutoGenerateColumns="false">
                
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <table>
                                                    <tr>
                                                        <td style="width:170px;">
                                <asp:CheckBox runat="server" id="chkFormatoRodaje"
                                     ValidationGroup='<%# Eval("format_id") %>'
                                    Text='<%# Eval("format_name") %>'
                                    onclick="checkPreprintStoreInfoVisibilty()"  />
                                                        </td>
                                                        <td>
                                <asp:Label runat="server" ID="Span1" style="display:none;font-style:normal;" Text="&nbsp;&nbsp;Especifique"></asp:Label>
                                <asp:TextBox style="display:none"  id="txtDetalle" runat="server" />
                                                        </td>
                                                    </tr>
                                             </table>            
                                                </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <br />
                       <asp:Panel runat="server" Visible="false">
                                <input type="checkbox" id="otro_formato_rodaje" name="otro_formato_rodaje" value="19"
                                    onclick="checkPreprintStoreInfoVisibilty()" <%if (otherFilmingFormatChecked){ %>checked<%} %> />
                                <span id="otro_formato_rodaje_label">Otro formato</span>
                                <input type="text" id="otro_formato_rodaje_detail" name="otro_formato_rodaje_detail"
                                    value="<%=otherFilmingFormatDetail %>" />
                           </asp:Panel>
                            </div>
                        </div>
                    </li>
                    <li>
                        <div class="field_label">
                            Formato(s) de exhibici&oacute;n:<span class="required_field_text">*</span></div>
                        <div class="field_input">
                            <asp:CheckBoxList ID="projectExhibitionFormatCBL" runat="server" CssClass="user-input-cbl">
                            </asp:CheckBoxList>
                           
                                <asp:CheckBox runat="server" id="chkOtroFormatoExhibicion" Text="Otro formato de exhibición"  
                                    onclick="checkOtroFormatoExhibicionChecked()" />
                                 <span id="Span2"></span>
                                <asp:Label id="lblOtro_formato_exibicion_label" text="&nbsp;&nbsp;Especifique" runat="server"/>
                                <asp:TextBox runat="server" id="txtOtro_formato_exibicion_detail" />
                           <br /><br />

                        </div>
                        <br />
                    </li>
                    <li id="project_development_lab_info_container" >
                        <div class="field_label">
                            Nombre y direcci&oacute;n del laboratorio de revelado:<span class="required_field_text">*</span></div>
                        <div class="field_input">
                            <textarea name="project_development_lab_info" id="project_development_lab_info" rows="3"
                                cols="60" runat="server"></textarea></div>
                    </li>
                    <li id="project_preprint_store_info_container">
                        <div class="field_label">
                            Lugar de depósito del soporte físico de la obra:<span class="required_field_text">*</span>
                            <asp:Image runat="server" ID="imgLugarDeposito" ImageUrl="~/images/icon-help.png" 
                              class="ketchup tooltip"  ToolTip="Se refiere al lugar donde reposan los elementos de tiraje o matriz de la obra. Debe incluir ciudad y país." />
                        </div>
                        <div class="field_input">
                            <textarea name="project_preprint_store_info" id="project_preprint_store_info" rows="3"
                                cols="60" runat="server"></textarea></div>
                    </li>
                    <li>
                        <div class="field_label">
                            Ha realizado dep&oacute;sito legal:<span class="required_field_text">*</span>
                            <asp:Image runat="server" ID="imgDepositoLegal" ImageUrl="~/images/icon-help.png" 
                                ToolTip="Obligación de entregar una copia de la película y materiales conexos en la Biblioteca Nacional dentro del término máximo de sesenta (60) días siguientes a su estreno."
                                class="ketchup tooltip"
                                 />
                        </div>
                        <div class="field_input">
                            <input type="radio" id="project_legal_deposit_yes" name="project_legal_deposit" value="1"
                                runat="server" />Sí<br />
                            <input type="radio" id="project_legal_deposit_no" name="project_legal_deposit" value="0"
                                runat="server" />No
                        </div>
                    </li>
                    <li>
                         <div class="field_label">Comentarios adicionales:<span class="required_field_text"></span>
                            
                        </div>
                        <div class="field_input">
                          <textarea name="comentarios_adicionales" id="comentarios_adicionales" rows="3" class="user-input"
                                cols="60" runat="server"></textarea>
                        </div>
                    </li>
                    <% if (user_role < 6)
                        { %>
                    <li>
                        <div id="attachment-box" >
                            <asp:Repeater ID="AttachmentRepeater" runat="server">
                                <HeaderTemplate>
                                    <fieldset>
                                        <legend>Adjuntos</legend>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <ul>
                            
                                       
                                         <li>
                                            <%# Eval("attachment_render") %>                         
                                        </li>
                                    </ul>
                                </ItemTemplate>
                                <FooterTemplate>
                                    </fieldset></FooterTemplate>
                            </asp:Repeater>
                        </div>
                    </li>
                    <% } %>

                    <%
                    if (showAdvancedForm)
                    { %>
           
                    <li>

                    </li>
                    <li>
                        <div id="admin-form">
                            <h3>
                                Formulario de gestión de la solicitud</h3>
                            <div id="admin-form-left">
                                <%if(project_state_id != 6 && project_state_id != 7 && project_state_id != 8) {%>
                                <ul>
                                    <li>
                                        <input type="radio" name="gestion_realizada" id="gestion_realizada_sin_revisar" value="none"
                                            runat="server" /><label for="gestion-realizada-sin-revisar">Sin revisar</label></li>
                                    <li>
                                        <input type="radio" name="gestion_realizada" id="gestion_realizada_solicitar_aclaraciones"
                                            value="solicitar-aclaraciones" runat="server" /><label for="gestion-realizada-solicitar-aclaraciones">Solicitar
                                                aclaraciones</label></li>
                                    <li>
                                        <input type="radio" name="gestion_realizada" id="gestion_realizada_informacion_correcta"
                                            class="depending-box"
                                            value="informacion-correcta"  runat="server" /><label for="gestion-realizada-informacion-correcta">Informaci&oacute;n
                                                correcta</label></li>
                                </ul>
                                <%}
                                  if (project_state_id == 6 || project_state_id == 7 || project_state_id == 8)
                                   {%>
                                <fieldset>
                                    <ul>
                                        <li>
                                            <input type="radio" name="estado_revision" id="estado_revision_sin_revisar" value="none"
                                                runat="server" /><label for="estado_revision_sin_revisar">Sin revisar</label></li>
                                        <li>
                                            <input type="radio" name="estado_revision" id="estado_revision_revisado" value="revisado"
                                                runat="server" /><label for="estado_revision_revisado">No Cumple</label></li>
                                        <li>
                                            <input type="radio" name="estado_revision" id="estado_revision_aprobado" value="aprobado" class="depending-box" 
                                                runat="server" /><label for="estado_revision_aprobado">Cumple</label></li>
                                    </ul>
                                </fieldset>
                                <%} %>
                            </div>
                            <%if (project_state_id >= 6 ) 
                          { %>
                          <div id="admin-form-center">
                            <ul>
                                <li>
                                    <h3>
                                        <%--Formulario de registro de aclaraciones--%>

                                    </h3>
                                    <div id="Div2">
                                        <h3>
                                           <b> Solicitud de aclaraciones </b></h3>
                                        <div>
                                            <asp:Literal ID="clarification_request_summary" runat="server">No se han solicitado aclaraciones sobre esta pestaña</asp:Literal></div>
                                    </div>
                                </li>
                                <li>
                                    <div id="Div4">
                                        <h4>
                                           <b> Respuesta del productor</b></h4>
                                        <div>
                                            <asp:Literal ID="producer_clarification_summary" runat="server">No se ha respondido nada a la aclaración solicitada</asp:Literal></div>
                                    </div>
                                </li>
                            
                        <%}else { %>
                            <div id="admin-form-center">
                                <ul>
                                    <li>
                                        <h3>
                                           <b> Solicitud de aclaraciones </b></h3>
                                        <textarea name="solicitud_aclaraciones" style="width:620px;min-height:200px;" id="solicitud_aclaraciones" rows="5" cols="40"
                                            runat="server"></textarea></li>
                                    
                                
                            <%} %>
                                <li>
                                    <h3>Observaciones</h3>
                                    <textarea name="informacion_correcta" style="width:620px;min-height:200px;"  id="informacion_correcta" rows="5" cols="40" runat="server"></textarea>
                                </li>
                                </ul>
                            </div>
                            <div id="admin-form-right">
                                <a href="/SolicitudAclaraciones.aspx" target="_blank">Previsualizaci&oacute;n de la
                                    solicitud de aclaraciones</a>
                            </div>
                            <div id="link">
                                <div style='margin: 0; text-align: left; text-decoration: underline; cursor: pointer;
                                    padding: 0 0 20px 0;' onclick='DisableEnableFormCustom(true,"desactivar")'>
                                    Desactivar el formulario</div>
                            </div>
                        </div>
                        <% 
                            }
                            if (showLogging)
                            {
                        %>
                            <asp:Panel runat="server" ID="pnlBitacora" Visible="false">
                        <div>
                            <a href="VerBitacora.aspx" target="_blank">Ver hist&oacute;rico de cambios en esta solicitud.</a>
                        </div>
                                </asp:Panel>
                        <% 
                            }
                        %>
                    </li>
                    <li>
                        <%
                            /* Si el estado del proyecto es "Aclaraciones solicitadas" y el estado de la sección es "rechazado" se presenta el formulario de registro de aclaraciones para el productor */
                            if ((project_state_id >= 5 && section_state_id == 10) )//
                            { %>
                        <div id="registro_aclaraciones_form">
                              <%  if (user_role <=1)  { %>
                            <ul>
                                <li>
                                    <h3>
                                        <%--Formulario de registro de aclaraciones--%>

                                    </h3>
                                    <div id="static_info">
                                         <h3>
                                           <b> Solicitud de aclaraciones </b></h3>
                                        <div>
                                            <asp:Literal ID="clarification_request" runat="server">No se han solicitado aclaraciones sobre esta pestaña</asp:Literal></div>
                                    </div>
                                </li>
                            </ul>
                            <div id="input_info">
                                <ul>
                                    <li>
                                        <h4>
                                            Escriba sus aclaraciones a continuación</h4>
                                        <textarea name="producer_clarifications_field" maxlength="4000" class="user-input"  style="width:620px;min-height:200px;"  id="producer_clarifications_field"
                                            rows="5" cols="80" runat="server" ></textarea>
                                    </li>
                                </ul>
                            </div>
                            <% } %>
                        </div>
                        <% 
                            } %>
                    </li>
                    <li>
                        <div class="field_input">
                            <input type="submit" id="submit" name="submit_project_data" class="boton" value="Guardar" onclick='$("#loading").show();DisableEnableFormCustom(false,"activar");revertCurrencyFormat();' /></div>
                    </li>
                </ul>
            </fieldset>
        </div>
        </form>
    </div>
</asp:Content>
