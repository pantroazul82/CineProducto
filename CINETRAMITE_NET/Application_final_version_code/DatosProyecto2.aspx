<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DatosProyecto2.aspx.cs" Inherits="CineProducto.DatosProyecto2" %>


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

        function validaNumericos(event) {
            if (event.charCode >= 48 && event.charCode <= 57) {
                return true;
            }
            return false;
        }

        function verificaPaste(n) {
            var nombreControl = "";
            if (n == "project_percentage") {
                n = <%=project_percentage.ClientID %>;
            }            

            permitidos = /^\d{5}$/; //     /[^0-9]/;
            if (permitidos.test(n.value)) {
                alert("Solo se puedeingresar numeros. Corregir: " + n.value);
                n.value = "";
                n.focus();
                return false;
            }
            if (n.value < 10 || n.value > 100) {
                alert("Solo se puedeingresar numeros del 10 al 100. Corregir: " + n.value);
                n.value = "";
                n.focus();
                return false;
            }
            return true;

        }

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
                })
              });

            </ItemTemplate>
        </asp:Repeater>
            /* Fin adjuntos js */


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
            /**/

         $('#<%=municipioDDL.ClientID %>').change(function () {
             $('#selectedMunicipio').val($('#<%=municipioDDL.ClientID %>').val());
             if ($('#<%=municipioDDL.ClientID %>').val() == "ZA001") {
                 $('#contenedorOtroMunicipio').show();
             }
             else {
                 $('#contenedorOtroMunicipio').hide();
             }
         });       
        
        $('.currencyformat').formatCurrency({ roundToDecimalPlace: 0 });
        $('.percentageformat').change(function(){
            $(this).val($(this).val()+'%');
        });

        $('#<%=projectTypeDDL.ClientID %>').change(function(){
            if($(this).val()==3){
            $('.field_text_message').html("");
            $('.field_text_message').removeClass("active_message");
                alert("Recuerde que debe proporcionar en la pestaña finalización el link protegido de la obra.");
            }else{
                $('.field_text_message').addClass("active_message");
                //$('.field_text_message').html("Recuerde que debe programar una cita para visionar la pelicula en la Dirección de Audiovisuales, Cine y Medios Interactivos")
            }
        });
         $('#<%=productionTypeDDL.ClientID %>').change(function(){
             $('#submit').val('combo');
             $('#submit').click();
         });
            $('#<%=cmbFormatoPadre.ClientID %>').change(function () {
                $('#submit').val('combo');
                $('#submit').click();
            });
        $('.currencyformat').blur(function () {
            $('.currencyformat').formatCurrency({ roundToDecimalPlace: 0 });
            calculateTotalCostWithoutPromotion();
        });

   //     $('#<%=project_filming_start_date.ClientID %>').datepicker({dateFormat: "yy-mm-dd",changeMonth: true,changeYear: true,}).datepicker("setDate", "0");
   //     $('#<%=project_filming_end_date.ClientID %>').datepicker({dateFormat: "yy-mm-dd",changeMonth: true,changeYear: true,}).datepicker("setDate", "0");

            $('#<%=project_filming_start_date.ClientID %>').datepicker({ dateFormat: "yy-mm-dd", changeMonth: true, changeYear: true, maxDate: '0'});
            $('#<%=project_filming_end_date.ClientID %>').datepicker({ dateFormat: "yy-mm-dd", changeMonth: true, changeYear: true, maxDate: '0'});
        /* Calcula el costo total y lo presenta cuando se carga la pagina */
        calculateTotalCostWithoutPromotion();

        /* Verifica el tipo de producción y presenta o esconde el campo de cantidad de productores extranjeros */
            setProducersQtyVisibility();
            setPremioVisibility();
            setExhibidaVisibility();
            setReconocimientoVisibility(); 
            setIbermediaVisibility();
            setFDCVisibility();

        /* Crea la función de presentación del tooltip en todos los campos */
        $('#<%=project_name.ClientID %>').tooltip({ bodyHandler: function () { return '<asp:Literal id="tooltip_project_name" runat="server"></asp:Literal>'; }, showURL: false });
        $('#<%=project_provisional_name.ClientID %>').tooltip({ bodyHandler: function () { return '<asp:Literal id="tooltip_project_provisional_name" runat="server"></asp:Literal>'; }, showURL: false });
        $('#<%=txtPaginaFacebook.ClientID %>').tooltip({ bodyHandler: function () { return '<asp:Literal id="tooltip_txtPaginaFacebook" runat="server"></asp:Literal>'; }, showURL: false });
        $('#<%=txtPaginaWeb.ClientID %>').tooltip({ bodyHandler: function () { return '<asp:Literal id="tooltip_txtPaginaWeb" runat="server"></asp:Literal>'; }, showURL: false });
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
            $('#<%=cmbOtrosIdiomas.ClientID %>').addClass("user-input");

            $('#<%=esColombiaLugar.ClientID %>').addClass("user-input");
            $('#<%=cmbDeptoLugar.ClientID %>').addClass("user-input");
            $('#<%=cmbCiudadLugar.ClientID %>').addClass("user-input");
            $('#<%=cmbPais.ClientID %>').addClass("user-input");
            $('#<%=txtCiudadLugar.ClientID %>').addClass("user-input");
            $('#<%=txtNombreLugar.ClientID %>').addClass("user-input");
            $('#<%=btnAgregarLugar.ClientID %>').addClass("user-input");
            $('#<%=btnLimpiarLugaresFilm.ClientID %>').addClass("user-input");
            $('#<%=cmbTipoEstimulo.ClientID %>').addClass("user-input");
            $('#<%=txtNombreEstimulo.ClientID %>').addClass("user-input");
            $('#<%=txtValorEstimulo.ClientID %>').addClass("user-input");
            $('#<%=txtBeneficiarioEstimulo.ClientID %>').addClass("user-input");
            $('#<%=btnAdicionarEstimulo.ClientID %>').addClass("user-input");            
            
            $('#<%=cmbPremio.ClientID %>').addClass("user-input");
            $('#<%=cmbExhibida.ClientID %>').addClass("user-input");
            

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
            $('#<%=cmbExhibida.ClientID %>').change(function () {

                setExhibidaVisibility();
            });

            $('#<%=cmbIbermedia.ClientID %>').change(function () {

                setIbermediaVisibility();
            });

            $('#<%=cmbFDC.ClientID %>').change(function () {

                setFDCVisibility();
            });

            $('#<%=cmbReconocimientoNal.ClientID %>').change(function () {

                setReconocimientoVisibility();
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

            <%  if (user_role == 6)  { %>
            DisableEnableFormCustom(true, 'desactivar');
            <%} %>  

            if ($("#hdHabilitarForm").val() == "Activo") {
                DisableEnableFormCustom(false, 'activar');
                $("#hdHabilitarForm").val("");
            }

            $('#loading').hide();
            if ($('#<%=municipioDDL.ClientID %>').val() == "ZA001") {
                $('#contenedorOtroMunicipio').show();
            }
            else {
                $('#contenedorOtroMunicipio').hide();
            }
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
            } else {
                control.empty();
                control.append($("<option></option>").val("0").html("Seleccione"));
            }
        }

        function validateAprobar(){
            alert('no no no');
            return false;
        }

        function DisableEnableFormCustom(accion, cambio){
            DisableEnableForm(accion,cambio);
            if ( cambio == 'desactivar' ) {
                document.getElementById('<%=txtOtro_formato_exibicion_detail.ClientID %>').setAttribute("disabled", "disabled");
                
                document.getElementById('<%=chkOtroFormatoExhibicion.ClientID %>').setAttribute("disabled", "disabled");

                /*var GridView = document.getElementById('<%=grdFormatosRodaje.ClientID %>');
                /*if (GridView.rows.length > 0) {
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
                */
            } else {
                document.getElementById('<%=txtOtro_formato_exibicion_detail.ClientID %>').removeAttribute("disabled");
                document.getElementById('<%=chkOtroFormatoExhibicion.ClientID %>').removeAttribute("disabled");
                //var GridView = document.getElementById('<%=grdFormatosRodaje.ClientID %>');
                /*if (GridView.rows.length > 0) {
                    for (Row = 0; Row < GridView.rows.length; Row++) {
                        var control=GridView.rows[Row].cells[0].childNodes[1].childNodes[1].children[0].children[0].childNodes[1];
                        control.removeAttribute("disabled");
                        var c1 =control.parentElement.nextSibling.nextSibling;
                        var c2 =c1.childNodes[1];
                        c2.removeAttribute("disabled");
                        var c3 =c1.childNodes[3];
                        c3.removeAttribute("disabled");
                    }
                }*/
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
        if ($('#<%=productionTypeDDL.ClientID %>').val() >= 2) {
            $('#cantidad_productores_extranjeros_block').show();
        }
        else {
            $('#cantidad_productores_extranjeros_block').hide();
        }
    }

        function setIbermediaVisibility() {
            if ($('#<%=cmbIbermedia.ClientID %>').val() == 'Si') {
                $('#DivIbermedia').show();
            }
            else {
                $('#DivIbermedia').hide();
            }
        }

        function setFDCVisibility() {
            if ($('#<%=cmbFDC.ClientID %>').val() == 'Si') {
                $('#DivFDC').show();
            }
            else {
                $('#DivFDC').hide();
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

        function setExhibidaVisibility() {
            if ($('#<%=cmbExhibida.ClientID %>').val() == 1) {
                $('#DivExhibidaTxt').show();
                $('#DivNoExhibidaTxt').hide();
                
            }
            else if($('#<%=cmbExhibida.ClientID %>').val() == 2) {
                $('#DivExhibidaTxt').hide();
                $('#DivNoExhibidaTxt').show();

            }
            else {
                $('#DivExhibidaTxt').hide();
                $('#DivNoExhibidaTxt').hide();
            }
        }

        function setReconocimientoVisibility() {
            if ($('#<%=cmbReconocimientoNal.ClientID %>').val() == 'Si') {
                 $('#DivReconocimiento').show();
             }
            else
            {
                  $('#DivReconocimiento').hide();
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
            //var GridView = document.getElementById('<%=grdFormatosRodaje.ClientID %>');
                        
            /*if (GridView.rows.length > 0) {
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
            }*/

            //if (shootingFormatCounter==0) 
            //{
            //    $('#project_preprint_store_info_container').hide();
            //}else {
            //    $('#project_preprint_store_info_container').show();
            //}

            var formato = document.getElementById('<%=cmbFormatoPadre.ClientID %>');

            if (formato.selectedIndex == 2)
               laboratorio++;
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


        
        function showDuracion() {
            var registeredValue = $('#<%=projectTypeDDL.ClientID %>').val();
            //alert("Escogio " + registeredValue);
            if(registeredValue == 1)
            {
                var lblDuracionPermitida = document.getElementById('<%=lblDuracionPermitida.ClientID %>');
                lblDuracionPermitida.innerHTML = "Minimo 70 minutos";
                
                
            }
            if (registeredValue == 2) {
                var lblDuracionPermitida = document.getElementById('<%=lblDuracionPermitida.ClientID %>');
                lblDuracionPermitida.innerHTML = "Minimimo 52 minutos";

            }
            if (registeredValue == 3) {
                var lblDuracionPermitida = document.getElementById('<%=lblDuracionPermitida.ClientID %>');
                lblDuracionPermitida.innerHTML = "Minimo 7 minutos y maximo 69 minutos";

            }
        }

        function adicionarOtrosIdiomas() {

            var combo = document.getElementById('<%=cmbOtrosIdiomas.ClientID %>');
            var selected = combo.options[combo.selectedIndex].text;
            var valSelected = combo.options[combo.selectedIndex].value;
            if (valSelected != 0)
            {
                var txtOI = document.getElementById('<%=txtOtrosIdiomas.ClientID %>');
                txtOI.innerHTML += selected + ', ';
            }
            
        }

        function borrarOtrosIdiomas() {


           
            
            var txtOI = document.getElementById('<%=txtOtrosIdiomas.ClientID %>');
            txtOI.innerHTML = '';

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
            $('#<%=projectTypeDDL.ClientID %>').find("option[value='2']").show(); //hide()         
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
                <div class="pull-right" >
                <asp:Label ID="opciones_adicionales" runat="server"></asp:Label><asp:Label runat="server" ID="lblCodProyecto" Visible="false"></asp:Label></div>
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
      
      <div style="clear:left;"></div>
     <div class="warning">
            <p>
             Si modifica Tipo de Producción, Porcentajes de participación, Tipo de obra o Duración de la obra, las pestañas: Coproductores y Personal podrian sufrir cambios. Del mismo modo podria ser necesario, volver a cargar los respectivos adjuntos.  </p>
        </div>
      <div style="clear:left;"></div>
        </asp:Panel>
            <asp:HiddenField runat="server" ClientIDMode="Static" ID="hdHabilitarForm" Value=""></asp:HiddenField>
        <div id='Proyecto'>            
             <%if (project_state_id != 9 && project_state_id != 10  && showAdvancedForm){ %>
                                    <div id="link">
                                        <div style='margin: 0; text-align: left; text-decoration: underline; cursor: pointer;
                                            padding: 0 0 20px 0;' onclick='DisableEnableFormCustom(true,"desactivar")'>
                                            Desactivar el formulario</div>
                                    </div>
                                <%} %>
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
                        <br /><br />
                        <div class="warning" style="width:80%">Produccion es aquella en la cual el o los productores son todos colombianos<br />
                             En una coproduccion nacional debe haber al menos un productor colombiano y uno extranjero <br />
                            Coproducción financiera: En el marco del Acuerdo Iberoamericano de Coproducción Cinematográfica (Artículo V) cuando se trata únicamente de un aporte económico por parte del coproductor nacional.
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
                            N&uacute;mero de productores nacionales:<span class="required_field_text">*</span></div>
                        <div class="field_input">
                            <input type="text" onchange ="runPostback();" id="project_domestic_producer_qty" name="project_domestic_producer_qty"
                                runat="server" /></div>
                    </li>
                 
                    <li id="cantidad_productores_extranjeros_block">
                        <div class="field_label">
                            N&uacute;mero de productores extranjeros:<span class="required_field_text">*</span></div>
                        <div class="field_input">
                            <input type="text" onchange ="runPostback();" id="project_foreign_producer_qty" name="project_foreign_producer_qty"
                                runat="server" /></div>
                    </li>
                    <li>
                                    <div class="field_label">
                                        Porcentaje de participación colombiana:<span class="required_field_text">*</span></div>
                                    <div class="field_input">
                                        <input type="text" maxlength="5" size="4" class="percentageformat" id="project_percentage" name="project_percentage"
                                            runat="server" onchange="return verificaPaste('project_percentage')"  />
                                        <br />
                                        <span class="required_field_text"><asp:label runat="server" ID="lblErrorPorcentaje" Text=""></asp:label></span>
                                    </div>

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
                                
                                <!--li>
                                    <div class="field_label">
                                        Valor estimado promoción:<span class="required_field_text">*</span></div>
                                    <div class="field_input">
                                        <input type="text" class="currencyformat" id="project_total_cost_promotion" name="project_total_cost_promotion"
                                            runat="server" /></div>
                                </li-->
                                
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
                            
                            
                            <asp:SqlDataSource ID="SqlDataSourceIdioma" runat="server" ConnectionString="<%$ ConnectionStrings:cineConnectionString %>" SelectCommand="SELECT [cod_idioma], [nombre_idioma] FROM [idioma] order by [nombre_idioma]"></asp:SqlDataSource>
                            
                            
                         </div>
                    </li>


                    <li>
                        
                        <div class="field_label">
                         Otros idiomas   <span class="required_field_text"></span></div>
                        <div class="field_input">
                            <div style="border:1px solid red; width:552px">
                                <br />
                                <b>Adicionar el Idioma:</b>
                            <asp:DropDownList ID="cmbOtrosIdiomas" runat="server" name="cmbOtrosIdiomas" AppendDataBoundItems="True" DataSourceID="SqlDataSourceIdioma" DataTextField="nombre_idioma" DataValueField="cod_idioma"  onchange="adicionarOtrosIdiomas();">
                                <asp:ListItem Text="Seleccione" Value="0"></asp:ListItem>
                            </asp:DropDownList>   
                            <br /><br />                            
                             
                           <div class="field_input">
                            <textarea name="txtOtrosIdiomas" id="txtOtrosIdiomas" style="height:70px" readonly="readonly" class="user-input" rows="5" cols="55" runat="server"></textarea>
                               </div>
                             <br /> 
                             <button type="button" onclick="borrarOtrosIdiomas()" class="user-input" id="btnLimpiarOtrosIdiomas" >Limpiar otros idiomas!</button> 
                            <br /> <br />
                                </div>
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
                            <asp:DropDownList ID="projectTypeDDL" runat="server" name="project_type" onchange="showDuracion();">
                            </asp:DropDownList>                            
                         </div>
                    </li>

                    <li>
                        <div class="field_label">
                            Duraci&oacute;n de la obra:<span class="required_field_text">*</span></div>
                        <div class="field_input">
                            <input type="text" id="project_duration_minutes" name="project_duration_minutes"
                                class="input_duracion user-input" size="2" maxlength="3" runat="server" onkeypress='return validaNumericos(event)' />
                            Minutos:<input type="text" id="project_duration_seconds" name="project_duration_seconds"
                                onchange="checkSecondsFieldValues();" class="input_duracion" maxlength="2" size="2" runat="server" onkeypress='return validaNumericos(event)' />
                            Segundos
                            <br />
                            <label runat="server" id="lblDuracionPermitida" Text=""></label>
                            <span class="required_field_text"><asp:label runat="server" ID="lblErrorMinutes" Text=""></asp:label></span>
                        </div><br /><br />
                        
                           
                    </li>

                    <li>
                        <div class="field_label">
                            Sinopsis:<span class="required_field_text">*</span><br />
                            (M&aacute;ximo 3.000 caracteres incluyendo espacios,aproximadamente una p&aacute;gina)</div>
                        <div class="field_input">
                            <textarea name="project_synopsis" id="project_synopsis" class="user-input" rows="5"
                                cols="60" runat="server"></textarea></div>
                    </li>
                    <li>
                        <div class="field_label">
                            ¿Realizó reconocimiento como proyecto nacional?:<span class="required_field_text">*</span><br />
                          </div>
                        <div class="field_input">
                            <asp:DropDownList ID="cmbReconocimientoNal" runat="server" name="cmbReconocimientoNal" class="user-input" ToolTip="Aquellas peliculas que cuenten con resolucion de proyecto expedido por la DACMI">
                                <asp:ListItem Text="Seleccione..." Value="" />
                                <asp:ListItem Text="Si" Value="Si" />
                                <asp:ListItem Text="No" Value="No" />
                            </asp:DropDownList>
                            </div>
                     </li>
                    <li>
                        <div class="field_label"> </div>
                           <div id="DivReconocimiento" class="field_input">
                               <div>
                               <div class="field_label"> Año de Resolución <span class="required_field_text">*</span>:  </div>   
                                <asp:TextBox ID="txtAnoResolucion" runat="server" Font-Names="txtAnoResolucion" MaxLength="4" class="user-input"></asp:TextBox>
                                    </div>
                               <div>
                               <div class="field_label">Número de Resolución <span class="required_field_text">*</span>:</div>
                               <asp:TextBox ID="txtNumeroResolucion" runat="server" Font-Names="txtNumeroResolucion" MaxLength="20" class="user-input"></asp:TextBox>
                                </div>    
                            </div>
                        
                    </li>
                    <li>
                        <div class="field_label">
                            Tiene estímulos o premios de financiación (en cualquier etapa):<span class="required_field_text">*</span></div>
                         <div class="field_input">
                            <asp:DropDownList ID="cmbPremio" runat="server" name="cmbPremio">
                                <asp:ListItem Text="Seleccione..." Value="-1" />
                                <asp:ListItem Text="Si" Value="1" />
                                <asp:ListItem Text="No" Value="2" />
                            </asp:DropDownList>
                         </div>
                        </li>
                    <li>
                         <div class="field_label"> </div>                        


                        <div id="DivPremioTxt" class="field_input" style="border:1px solid red; width:552px">
                            
                              
                           
          <asp:ScriptManager ID="ScriptManager1" runat="server" />
                            
        <asp:UpdatePanel runat="server" id="UpdatePanel"   updatemode="Conditional"  >
            <Triggers>
              <asp:AsyncPostBackTrigger controlid="btnAdicionarEstimulo"
                    eventname="Click" />
            </Triggers>
            <ContentTemplate>
 <br />
                            
                            <br />
                           <table>
                               <tr><th style="text-align:right">Tipo de Estimulo<span class="required_field_text">*</span>:</td>
                                   <td>
                                       <asp:DropDownList ID="cmbTipoEstimulo" runat="server" AppendDataBoundItems="true" DataSourceID="SqlDataSourceTipoEstimulo" DataTextField="nombre" DataValueField="id_tipo_estimulo">
                                        <asp:ListItem Text="Seleccione..." Value="-1" />
                                    </asp:DropDownList>
                                    <asp:SqlDataSource ID="SqlDataSourceTipoEstimulo" runat="server" ConnectionString="<%$ ConnectionStrings:cineConnectionString %>" SelectCommand="SELECT * FROM [tipo_estimulo]"></asp:SqlDataSource>
                                    </td>
                               </tr>
                               <tr><th style="text-align:right">Nombre del estimulo<span class="required_field_text">*</span>:</th><td><asp:TextBox ID="txtNombreEstimulo" runat="server"></asp:TextBox></td></tr>
                               <tr><th style="text-align:right">Valor del estimulo<span class="required_field_text">*</span>:</th><td><asp:TextBox ID="txtValorEstimulo" runat="server" onkeypress='return validaNumericos(event)'></asp:TextBox></td></tr>
                               <tr><th style="text-align:right">Beneficiario<span class="required_field_text">*</span>:</th><td><asp:TextBox ID="txtBeneficiarioEstimulo" runat="server"></asp:TextBox></td></tr>
                               
                               <tr><td></td><td colspan="2"><asp:Button runat="server" id="btnAdicionarEstimulo" Text ="Adicionar" OnClick="btnAdicionarEstimulo_Click" /></td></tr>
                           </table>
                           
                              
                            

                <asp:Label runat="server" id="lblErrorEstimulo" ForeColor="Red" />   
                <asp:GridView ID="GridViewEstimulos" runat="server" ShowHeaderWhenEmpty="True" EmptyDataText="No ha definido los estimulos." DataSourceID="SqlDataSourceEstimulos" AutoGenerateColumns="False" DataKeyNames="id_estimulo" CellPadding="4" ForeColor="#333333" GridLines="None"  OnRowCommand="GridViewEstimulos_RowCommand">
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <Columns>
                        <asp:BoundField DataField="id_estimulo" HeaderText="Codigo" InsertVisible="False" Visible="false" ReadOnly="True" SortExpression="id_estimulo" />
                        <asp:BoundField DataField="tipo_estimulo" HeaderText="Tipo" SortExpression="tipo_estimulo" />
                        <asp:BoundField DataField="nombre" HeaderText="Nombre" SortExpression="nombre" />
                        <asp:BoundField DataField="valor" HeaderText="Valor" SortExpression="valor" />
                        <asp:BoundField DataField="beneficiario" HeaderText="Beneficiario" SortExpression="beneficiario" />
                        <asp:TemplateField HeaderText="Opciones">  
                        <ItemTemplate>                              
                            <asp:ImageButton runat="server" ID="EliminarEstimulo" OnClientClick="return confirm('Esta seguro de eliminar el estimulo?')"  CssClass="user-input" AlternateText="Eliminar" ImageUrl="~/img/web/Message_Error.png" CommandArgument='<%#Eval("id_estimulo")%>'  />                            
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
                <asp:SqlDataSource ID="SqlDataSourceEstimulos" runat="server" ConnectionString="<%$ ConnectionStrings:cineConnectionString %>" SelectCommand="select estimulo.id_estimulo, tipo_estimulo.nombre as tipo_estimulo, estimulo.nombre, estimulo.valor, estimulo.beneficiario from estimulo
join tipo_estimulo on tipo_estimulo.id_tipo_estimulo = estimulo.id_tipo_estimulo
where estimulo.project_id = @project_id">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="lblCodProyecto" DefaultValue="0" Name="project_id" PropertyName="Text" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </ContentTemplate>
        </asp:UpdatePanel>

                            <!--
                            <div class="field_label">Ibermedia:  
                              <asp:DropDownList ID="cmbIbermedia" runat="server" name="cmbIbermedia" class="user-input">
                                <asp:ListItem Text="Seleccione..." Value="" />
                                <asp:ListItem Text="Si" Value="Si" />
                                <asp:ListItem Text="No" Value="No" />
                            </asp:DropDownList></div> 
                            <div id="DivIbermedia" class="field_label"  style="width: auto;"> Especifique el estimulo obtenido:    
                                <asp:TextBox ID="txtIberrmediaEspecificacion" runat="server" name="txtIberrmediaEspecificacion" class="user-input"></asp:TextBox>
                            </div>

                            <div class="field_label"> FDC:     
                            <asp:DropDownList ID="cmbFDC" runat="server" name="cmbFDC" class="user-input">
                                <asp:ListItem Text="Seleccione..." Value="" />
                                <asp:ListItem Text="Si" Value="Si" />
                                <asp:ListItem Text="No" Value="No" />
                            </asp:DropDownList></div> 
                            <div id="DivFDC" class="field_label"  style="width: auto;"> Especifique el estimulo obtenido: 
                                <asp:TextBox ID="txtFdcEspecificiacion" runat="server" name="txtFdcEspecificiacion" class="user-input"></asp:TextBox>
                             </div> 

                            <div class="field_label"  style="width: auto;">Otro estimulo o premio obtenido (Especifique)<asp:TextBox ID="txtOtrosEspecifique" runat="server" name="txtOtrosEspecifique" class="user-input"></asp:TextBox>                                                        
                            </div>
                            <div class="field_label"><br />                             
                               
                            </div>
                            -->
                        </div>

                        
                        
                    </li>
                    
                    <li><br />
                        <div class="field_label">
                            Lugares de filmaci&oacute;n:<span class="required_field_text">*</span></div>
                        <div class="field_input">
                            <div class="field_input" style="border:1px solid red; width:552px">                           

                                <asp:UpdatePanel runat="server" id="upLugares"   updatemode="Conditional"        >
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger controlid="btnAgregarLugar"     eventname="Click" />
                                      
                                    </Triggers>
                            <ContentTemplate>
                                <table>
                                    <!--tr><th>En colombia?</th><td><asp:CheckBox ID="esColombiaLugar" AutoPostBack="true" runat="server" OnCheckedChanged="esColombiaLugar_CheckedChanged"  />
                                     </td></tr-->
                                    <tr><th>Pais <span class="required_field_text">*</span>:</th>
                                        <td>
                                            <asp:DropDownList runat="server" CssClass="" AutoPostBack="true" ID="cmbPais" AppendDataBoundItems="true" DataSourceID="SqlDataSourcePais" DataTextField="localization_name" DataValueField="localization_name" OnSelectedIndexChanged="cmbPais_SelectedIndexChanged" >
                                             <asp:ListItem Text="Seleccione..." Value="" />
                                            </asp:DropDownList>   
                                            
                                         <asp:SqlDataSource ID="SqlDataSourcePais" runat="server" ConnectionString="<%$ ConnectionStrings:cineConnectionString %>" SelectCommand="select localization_id, localization_name from localization where localization_father_id=-2 order by localization_name"></asp:SqlDataSource>
                                           
                                        </td>
                                    </tr>                                   
                                
                                
                                <asp:Panel runat="server" ID="pnLugarEnColombia" Visible="false">
                                    <tr>
                                        <th>Departamento <span class="required_field_text">*</span>: </th>
                                        <td><asp:DropDownList runat="server" AutoPostBack="true" ID="cmbDeptoLugar" AppendDataBoundItems="true" DataSourceID="SqlDataSourceDeptos" DataTextField="localization_name" DataValueField="localization_id" OnSelectedIndexChanged="cmbDeptoLugar_SelectedIndexChanged">
                                            <asp:ListItem Text="Seleccione..." Value="-1" />
                                            </asp:DropDownList>          
                                         <asp:SqlDataSource ID="SqlDataSourceDeptos" runat="server" ConnectionString="<%$ ConnectionStrings:cineConnectionString %>" SelectCommand="select localization_id, localization_name from localization where localization_father_id=0"></asp:SqlDataSource>

                                        </td>
                                    </tr>
                                    <tr>
                                        <th>Municipio <span class="required_field_text">*</span>:</th>
                                        <td> <asp:DropDownList runat="server" ID="cmbCiudadLugar" AppendDataBoundItems="false" DataSourceID="SqlDataSourceCitys" DataTextField="localization_name" DataValueField="localization_id">
                                            <asp:ListItem Text="Seleccione..." Value="-1" />
                                             </asp:DropDownList>          
                                            <asp:SqlDataSource ID="SqlDataSourceCitys" runat="server" ConnectionString="<%$ ConnectionStrings:cineConnectionString %>" SelectCommand="select localization_id, localization_name from localization where localization_father_id=@localizacion_id">
                                                <SelectParameters>
                                                    <asp:ControlParameter ControlID="cmbDeptoLugar" Name="localizacion_id" PropertyName="SelectedValue" />
                                                </SelectParameters>
                                            </asp:SqlDataSource>
                                        </td>
                                    </tr>
                                   
                               

                                </asp:Panel>
                                <asp:Panel ID="pnFueraColombia" runat="server" Visible="true">
                                    
                                    <tr>
                                        <th>Ciudad <span class="required_field_text">*</span></th>
                                        <td><asp:TextBox runat="server" ID="txtCiudadLugar"></asp:TextBox></td>
                                    </tr>
                                </asp:Panel>

                                 <tr>
                                        <th>Nombre Lugar</th>
                                        <td> <asp:TextBox runat="server" ID="txtNombreLugar"></asp:TextBox></td>
                                    </tr>

                                    <tr>
                                        <td></td>
                                        <td><asp:Button runat="server" ID="btnAgregarLugar" Text="Adicionar Lugar" OnClick="btnAgregarLugar_Click"/></td>
                                    </tr>
                               
                                    </table>
                                <asp:Label runat="server" ID="lblErrorLugar" ForeColor="Red" ></asp:Label>
                                <asp:TextBox runat="server" ID="project_recording_sites" TextMode="MultiLine" Height="120" ReadOnly="true" Rows="3" Columns="60" ></asp:TextBox>
                                <asp:Button runat="server" ID="btnLimpiarLugaresFilm" Text="Borrar lugares de filmacion!" OnClick="btnLimpiarLugaresFilm_Click" />
                            </ContentTemplate>
                        </asp:UpdatePanel>

                                
                                
                                <!--
                            <textarea name="project_recording_sites2" id="project_recording_sites2" class="user-input"
                                rows="3" cols="60" runat="server"></textarea>
                                -->
                            </div>
                        </div>
                    </li>
                    
                    <li>
                        <div class="field_label">
                           <label id="lblFechainicioRodaje">Fecha de inicio de rodaje:</label>
                             <span class="required_field_text">*</span></div>
                        <div class="field_input">
                            <input type="text" autocomplete="off"  name="project_filming_start_date" id="project_filming_start_date"
                                class="user-input" runat="server" /></div>
                    </li>
                    <li>
                        <div class="field_label">
                            <label id="lblFechaFinRodaje">Fecha de fin de rodaje:</label>
                            <span class="required_field_text">*</span></div>
                        <div class="field_input">
                            <input type="text" autocomplete="off" name="project_filming_end_date" id="project_filming_end_date"
                                class="user-input" runat="server" /></div>
                    </li>
                    <li>
                        <div class="field_label">
                            <label id="lblFechaObsRodaje">Observaciones sobre las fechas de rodaje:</label></div>
                        <div class="field_input">
                            <textarea name="project_filming_date_obs" id="project_filming_date_obs" class="user-input"
                                rows="3" cols="60" runat="server"></textarea></div>
                    </li>

                     <li id="departamento_field">
                        <div class="field_label">
                            Formato de captura:<span class="required_field_text">*</span></div>
                        <div class="field_input">
                            <asp:DropDownList ID="cmbFormatoPadre" onclick="checkPreprintStoreInfoVisibilty()" CssClass="user-input" runat="server" name="cmbFormatoPadre" 
                                AppendDataBoundItems="true" DataSourceID="SqlDataSourceFormatoPadre" DataTextField="format_name" DataValueField="format_id" OnSelectedIndexChanged="cmbFormatoPadre_SelectedIndexChanged">
                                <asp:ListItem Text="Seleccione..." Value="0"></asp:ListItem>
                            </asp:DropDownList>                            
                            <asp:SqlDataSource ID="SqlDataSourceFormatoPadre" runat="server" ConnectionString="<%$ ConnectionStrings:cineConnectionString %>" SelectCommand="select format_id, format_name  from format where format_type_id = 1 and format_id in (22,23) order by format_name"></asp:SqlDataSource>
                            
                            <asp:DropDownList ID="cmbFormatoRodaje"  class="user-input" runat="server" name="cmbFormatoRodaje" DataTextField="format_name" DataValueField="format_id">
                                <asp:ListItem Text="Seleccione..." Value="0"></asp:ListItem>
                            </asp:DropDownList>                            
                           
                             
                        </div>      
                    </li>                   

                    
                    <!--li>
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
                    </li-->
                    <li>
                        <div class="field_label">
                            Formato(s) de exhibici&oacute;n:<span class="required_field_text">*</span></div>
                        <div class="field_input" style="width:auto">
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
                    <div id="project_development_lab_info_container">
                         <li>                            
                        <div class="field_label">
                            Nombre del laboratorio de revelado:<span class="required_field_text">*</span></div>
                        <div class="field_input">
                            <input type="text" id="txtNombreLabRev" name="txtNombreLabRev" runat="server" class="inputLargo user-input" />
                        </div>
                        </li>
                         <li id="departamento_field">
                        <div class="field_label">
                            Departamento del laboratorio de revelado:<span class="required_field_text">*</span></div>
                        <div class="field_input">
                            <asp:DropDownList ID="departamentoDDL" runat="server" name="departamentoDDL">
                            </asp:DropDownList>
                        </div>
                    </li>
                    <li id="municipio_field">
                        <div class="field_label">
                            Municipio del laboratorio de revelado:<span class="required_field_text">*</span>
                            <input type="hidden" name="selectedMunicipio" id="selectedMunicipio" value="0" /></div>
                        <div class="field_input">
                            <asp:DropDownList ID="municipioDDL" runat="server" name="municipioDDL">
                            </asp:DropDownList>
                        </div>
                    </li>

                        <li id="contenedorOtroMunicipio">
                            <div class="field_label">
                               especifique:<span class="required_field_text"></span>
                            </div>
                            <div class="field_input">
                                <asp:TextBox runat="server" ID="txtMunicipioOtro" class="inputLargo user-input" />
                            </div>
                        </li>
                        <li>
                            <div class="field_label">
                                Direcci&oacute;n del laboratorio de revelado:<span class="required_field_text">*</span>
                            </div>
                            <div class="field_input">
                                <textarea name="project_development_lab_info" id="project_development_lab_info" rows="3"
                                    cols="60" runat="server"></textarea>
                            </div>
                        </li>

                    </div>

                   


                    <li id="project_preprint_store_info_container">
                        <div class="field_label">
                            Lugar de depósito del soporte físico de la obra:<span class="required_field_text">*</span>
                            <asp:Image runat="server" ID="imgLugarDeposito" ImageUrl="~/images/icon-help.png" 
                              class=""  ToolTip="Se refiere al lugar donde reposan los elementos de tiraje o matriz de la obra. Debe incluir ciudad y país." />
                        </div>
                        <div class="field_input">
                            <textarea name="project_preprint_store_info" id="project_preprint_store_info" rows="3"
                                cols="60" runat="server"></textarea></div>
                    </li>
                    

                    <li>
                        <div class="field_label">
                              ¿Esta obra ha sido exhibida publicamente?:<span class="required_field_text">*</span></div>
                         <div class="field_input">
                            <asp:DropDownList ID="cmbExhibida" runat="server" name="cmbExhibida">
                                <asp:ListItem Text="Seleccione..." Value="-1" />
                                <asp:ListItem Text="Si" Value="1" />
                                <asp:ListItem Text="No" Value="2" />
                            </asp:DropDownList>
                         </div>
                        </li>
                    <li>
                         <div class="field_label"> </div>
                        <div id="DivExhibidaTxt" class="field_input">  
                            <div class="field_label"  style="width: auto; height:50px;">Número de acta del depósito *: 
                                <asp:TextBox ID="no_acta_deposito" runat="server" name="no_acta_deposito" class="user-input"></asp:TextBox>                                                                                         
                            </div>                           
                        </div>
						<div id="DivNoExhibidaTxt" class="field_input">  
                            <div class="field_label"  style="width: auto;">
							  <font style="color:red">Recuerde realizar el deposito legal  </font>
                            </div>                           
                        </div>
                       
                    </li>
                    <li>
                        <div class="field_label">
                            Ha realizado dep&oacute;sito legal:<span class="required_field_text">*</span>
                            <asp:Image runat="server" ID="imgDepositoLegal" ImageUrl="~/images/icon-help.png" 
                                ToolTip="Obligación de entregar una copia de la película y materiales conexos en la Biblioteca Nacional dentro del término máximo de sesenta (60) días siguientes a su estreno."
                                class=""
                                 />
                            <br />
                            <a href="https://bibliotecanacional.gov.co/es-co/servicios/profesionales-del-libro/deposito-legal/deposito-de-obras" target="_blank">Mas informacion del deposito legal</a>
                        </div>
                        <div class="field_input">
                            <input type="radio" id="project_legal_deposit_yes" name="project_legal_deposit" value="1"
                                runat="server" />Sí<br />
                            <input type="radio" id="project_legal_deposit_no" name="project_legal_deposit" value="0"
                                runat="server" />No
                        </div>
                    </li>
                    
                     
                     <li>
                        <div class="field_label">
                            P&aacute;gina web de la obra:<span class="required_field_text"></span></div>
                        <div class="field_input">
                             <asp:TextBox runat="server" id="txtPaginaWeb" class="inputLargo user-input" /> 
                        </div>
                    </li>
                    <li>
                        <div class="field_label">
                            Redes Sociales:<span class="required_field_text"></span></div>
                        <div class="field_input">
                             <asp:TextBox runat="server" TextMode="MultiLine" Rows="3" cols="60" id="txtPaginaFacebook" class="inputLargo user-input" />
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
                                 <%if(project_state_id == 9 ) {%>
                                <ul>                                                                        
                                    <li>
                                        <label>Información corecta!</label></li>                                  

                                </ul>
                                <%}%>
                                <%if(project_state_id != 6 && project_state_id != 7 && project_state_id != 8 && project_state_id != 9 ) {%>
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
                                            class="depending-box"  checked="true"
                                            value="informacion-correcta"  runat="server" />
                                        <label for="gestion-realizada-informacion-correcta">Informaci&oacute;n
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
                                    <textarea name="informacion_correcta"  style="width:620px;min-height:200px;"  id="informacion_correcta" rows="5" cols="40" runat="server"></textarea>
                                </li>
                                </ul>
                            </div>
                            <div id="admin-form-right">
                                <a href="/SolicitudAclaraciones.aspx" target="_blank">Previsualizaci&oacute;n de la
                                    solicitud de aclaraciones</a>
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

                    <%if (project_state_id != 9 && project_state_id != 10  && user_role != 6)                        { %>

                    <div class="alert alert-warning" style="position: fixed; right: 50px;z-index:9999; margin-top: 60px;  min-height: 60px;    width: 250px;    text-align: center;    word-wrap: break-word;"   >
                       <div class="alert alert-warning">
                        Guardar tu información!
                        
                            <input type="submit" id="submit" name="submit_project_data" class="boton" value="Guardar" onclick='$("#loading").show();DisableEnableFormCustom(false,"activar");revertCurrencyFormat();' />
                           </div>
                    </div>

                   
                    
                    <%} %>
                </ul>
            </fieldset>
        </div>
        </form>
    </div>
</asp:Content>
