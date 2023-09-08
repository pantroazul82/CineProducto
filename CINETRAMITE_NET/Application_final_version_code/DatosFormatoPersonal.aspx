<%@ Page Title="Edici&oacute;n de una solicitud" Language="C#" MasterPageFile="~/Site.master" ValidateRequest="false"
    AutoEventWireup="True" CodeBehind="DatosFormatoPersonal.aspx.cs" Inherits="CineProducto.DatosFormatoPersonal" 
    EnableEventValidation="false" %>
<%@ Register src="usercontrols/cargando.ascx" tagname="cargando" tagprefix="uc1" %>

<asp:Content ID="TitleContent" runat="server" ContentPlaceHolderID="TitleContent">Trámite Reconocimiento Como Obra Nacional - Datos del Formato de Personal - Mincultura</asp:Content>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
<script src="<%= Page.ResolveClientUrl("~/Scripts/cine.js")%>" type="text/javascript"></script>
<script type = "text/javaScript">
    $(document).ready(function () {

        habilitarTracker();
        /* Agrega la clase user-input a todos los input que diligencia el usuario con el fin de poderlos desactivar y activar */
        $('#<%=director_name.ClientID %>').addClass("user-input tooltip-name");
        $('#<%=director_identification_number.ClientID %>').addClass("user-input tooltip-identification-number");
        $('#<%=director_address.ClientID %>').addClass("user-input tooltip-address");
        $('#<%=director_email.ClientID %>').addClass("user-input tooltip-email");
        $('#<%=director_phone.ClientID %>').addClass("user-input tooltip-phone");
        $('#<%=director_movil.ClientID %>').addClass("user-input tooltip-movil");
        $('#<%=director_municipioDDL.ClientID %>').addClass("user-input");
        $('#<%=director_departamentoDDL.ClientID %>').addClass("user-input");
      

        $('#<%=writer_name.ClientID %>').addClass("user-input tooltip-name");
        $('#<%=writer_identification_number.ClientID %>').addClass("user-input tooltip-identification-number");
        $('#<%=writer_address.ClientID %>').addClass("user-input tooltip-address");
        $('#<%=writer_email.ClientID %>').addClass("user-input tooltip-email");
        $('#<%=writer_phone.ClientID %>').addClass("user-input tooltip-phone");
        $('#<%=writer_movil.ClientID %>').addClass("user-input tooltip-movil");
        $('#<%=writer_municipioDDL.ClientID %>').addClass("user-input");
        $('#<%=writer_departamentoDDL.ClientID %>').addClass("user-input");

        $('#<%=photography_director_name.ClientID %>').addClass("user-input tooltip-name");
        $('#<%=photography_director_identification_number.ClientID %>').addClass("user-input tooltip-identification-number");
        $('#<%=photography_director_address.ClientID %>').addClass("user-input tooltip-address");
        $('#<%=photography_director_email.ClientID %>').addClass("user-input tooltip-email");
        $('#<%=photography_director_phone.ClientID %>').addClass("user-input tooltip-phone");
        $('#<%=photography_director_movil.ClientID %>').addClass("user-input tooltip-movil");
        $('#<%=photography_director_municipioDDL.ClientID %>').addClass("user-input");
        $('#<%=photography_director_departamentoDDL.ClientID %>').addClass("user-input");

        $('#<%=art_director_name.ClientID %>').addClass("user-input tooltip-name");
        $('#<%=art_director_identification_number.ClientID %>').addClass("user-input tooltip-identification-number");
        $('#<%=art_director_address.ClientID %>').addClass("user-input tooltip-address");
        $('#<%=art_director_email.ClientID %>').addClass("user-input tooltip-email");
        $('#<%=art_director_phone.ClientID %>').addClass("user-input tooltip-phone");
        $('#<%=art_director_movil.ClientID %>').addClass("user-input tooltip-movil");
        $('#<%=art_director_municipioDDL.ClientID %>').addClass("user-input");
        $('#<%=art_director_departamentoDDL.ClientID %>').addClass("user-input");

        $('#<%=music_author_name.ClientID %>').addClass("user-input tooltip-name");
        $('#<%=music_author_identification_number.ClientID %>').addClass("user-input tooltip-identification-number");
        $('#<%=music_author_address.ClientID %>').addClass("user-input tooltip-address");
        $('#<%=music_author_email.ClientID %>').addClass("user-input tooltip-email");
        $('#<%=music_author_phone.ClientID %>').addClass("user-input tooltip-phone");
        $('#<%=music_author_movil.ClientID %>').addClass("user-input tooltip-movil");
        $('#<%=music_author_municipioDDL.ClientID %>').addClass("user-input");
        $('#<%=music_author_departamentoDDL.ClientID %>').addClass("user-input");

        $('#<%=editor_name.ClientID %>').addClass("user-input tooltip-name");
        $('#<%=editor_identification_number.ClientID %>').addClass("user-input tooltip-identification-number");
        $('#<%=editor_address.ClientID %>').addClass("user-input tooltip-address");
        $('#<%=editor_email.ClientID %>').addClass("user-input tooltip-email");
        $('#<%=editor_phone.ClientID %>').addClass("user-input tooltip-phone");
        $('#<%=editor_movil.ClientID %>').addClass("user-input tooltip-movil");
        $('#<%=editor_municipioDDL.ClientID %>').addClass("user-input");
        $('#<%=editor_departamentoDDL.ClientID %>').addClass("user-input");

        $('#<%=cameraman_name.ClientID %>').addClass("user-input tooltip-name");
        $('#<%=cameraman_identification_number.ClientID %>').addClass("user-input tooltip-identification-number");
        $('#<%=cameraman_address.ClientID %>').addClass("user-input tooltip-address");
        $('#<%=cameraman_email.ClientID %>').addClass("user-input tooltip-email");
        $('#<%=cameraman_phone.ClientID %>').addClass("user-input tooltip-phone");
        $('#<%=cameraman_movil.ClientID %>').addClass("user-input tooltip-movil");
        $('#<%=cameraman_municipioDDL.ClientID %>').addClass("user-input");
        $('#<%=cameraman_departamentoDDL.ClientID %>').addClass("user-input");

        $('#<%=makeup_artist_name.ClientID %>').addClass("user-input tooltip-name");
        $('#<%=makeup_artist_identification_number.ClientID %>').addClass("user-input tooltip-identification-number");
        $('#<%=makeup_artist_address.ClientID %>').addClass("user-input tooltip-address");
        $('#<%=makeup_artist_email.ClientID %>').addClass("user-input tooltip-email");
        $('#<%=makeup_artist_phone.ClientID %>').addClass("user-input tooltip-phone");
        $('#<%=makeup_artist_movil.ClientID %>').addClass("user-input tooltip-movil");
        $('#<%=makeup_artist_municipioDDL.ClientID %>').addClass("user-input");
        $('#<%=makeup_artist_departamentoDDL.ClientID %>').addClass("user-input");

        $('#<%=costume_name.ClientID %>').addClass("user-input tooltip-name");
        $('#<%=costume_identification_number.ClientID %>').addClass("user-input tooltip-identification-number");
        $('#<%=costume_address.ClientID %>').addClass("user-input tooltip-address");
        $('#<%=costume_email.ClientID %>').addClass("user-input tooltip-email");
        $('#<%=costume_phone.ClientID %>').addClass("user-input tooltip-phone");
        $('#<%=costume_movil.ClientID %>').addClass("user-input tooltip-movil");
        $('#<%=costume_municipioDDL.ClientID %>').addClass("user-input");
        $('#<%=costume_departamentoDDL.ClientID %>').addClass("user-input");

        $('#<%=dresser_name.ClientID %>').addClass("user-input tooltip-name");
        $('#<%=dresser_identification_number.ClientID %>').addClass("user-input tooltip-identification-number");
        $('#<%=dresser_address.ClientID %>').addClass("user-input tooltip-address");
        $('#<%=dresser_email.ClientID %>').addClass("user-input tooltip-email");
        $('#<%=dresser_phone.ClientID %>').addClass("user-input tooltip-phone");
        $('#<%=dresser_movil.ClientID %>').addClass("user-input tooltip-movil");
        $('#<%=dresser_municipioDDL.ClientID %>').addClass("user-input");
        $('#<%=dresser_departamentoDDL.ClientID %>').addClass("user-input");

        $('#<%=casting_name.ClientID %>').addClass("user-input tooltip-name");
        $('#<%=casting_identification_number.ClientID %>').addClass("user-input tooltip-identification-number");
        $('#<%=casting_address.ClientID %>').addClass("user-input tooltip-address");
        $('#<%=casting_email.ClientID %>').addClass("user-input tooltip-email");
        $('#<%=casting_phone.ClientID %>').addClass("user-input tooltip-phone");
        $('#<%=casting_movil.ClientID %>').addClass("user-input tooltip-movil");
        $('#<%=casting_municipioDDL.ClientID %>').addClass("user-input");
        $('#<%=casting_departamentoDDL.ClientID %>').addClass("user-input");

        $('#<%=script_name.ClientID %>').addClass("user-input tooltip-name");
        $('#<%=script_identification_number.ClientID %>').addClass("user-input tooltip-identification-number");
        $('#<%=script_address.ClientID %>').addClass("user-input tooltip-address");
        $('#<%=script_email.ClientID %>').addClass("user-input tooltip-email");
        $('#<%=script_phone.ClientID %>').addClass("user-input tooltip-phone");
        $('#<%=script_movil.ClientID %>').addClass("user-input tooltip-movil");
        $('#<%=script_municipioDDL.ClientID %>').addClass("user-input");
        $('#<%=script_departamentoDDL.ClientID %>').addClass("user-input");

        $('#<%=soundman_name.ClientID %>').addClass("user-input tooltip-name");
        $('#<%=soundman_identification_number.ClientID %>').addClass("user-input tooltip-identification-number");
        $('#<%=soundman_address.ClientID %>').addClass("user-input tooltip-address");
        $('#<%=soundman_email.ClientID %>').addClass("user-input tooltip-email");
        $('#<%=soundman_phone.ClientID %>').addClass("user-input tooltip-phone");
        $('#<%=soundman_movil.ClientID %>').addClass("user-input tooltip-movil");
        $('#<%=soundman_municipioDDL.ClientID %>').addClass("user-input");
        $('#<%=soundman_departamentoDDL.ClientID %>').addClass("user-input");

        $('#<%=cargo14_name.ClientID %>').addClass("user-input tooltip-name");
        $('#<%=cargo14_identification_number.ClientID %>').addClass("user-input tooltip-identification-number");
        $('#<%=cargo14_address.ClientID %>').addClass("user-input tooltip-address");
        $('#<%=cargo14_email.ClientID %>').addClass("user-input tooltip-email");
        $('#<%=cargo14_phone.ClientID %>').addClass("user-input tooltip-phone");
        $('#<%=cargo14_movil.ClientID %>').addClass("user-input tooltip-movil");
        $('#<%=cargo14_municipioDDL.ClientID %>').addClass("user-input");
        $('#<%=cargo14_departamentoDDL.ClientID %>').addClass("user-input");

        $('#<%=cargo15_name.ClientID %>').addClass("user-input tooltip-name");
        $('#<%=cargo15_identification_number.ClientID %>').addClass("user-input tooltip-identification-number");
        $('#<%=cargo15_address.ClientID %>').addClass("user-input tooltip-address");
        $('#<%=cargo15_email.ClientID %>').addClass("user-input tooltip-email");
        $('#<%=cargo15_phone.ClientID %>').addClass("user-input tooltip-phone");
        $('#<%=cargo15_movil.ClientID %>').addClass("user-input tooltip-movil");
        $('#<%=cargo15_municipioDDL.ClientID %>').addClass("user-input");
        $('#<%=cargo15_departamentoDDL.ClientID %>').addClass("user-input");

        $('#<%=cargo16_name.ClientID %>').addClass("user-input tooltip-name");
        $('#<%=cargo16_identification_number.ClientID %>').addClass("user-input tooltip-identification-number");
        $('#<%=cargo16_address.ClientID %>').addClass("user-input tooltip-address");
        $('#<%=cargo16_email.ClientID %>').addClass("user-input tooltip-email");
        $('#<%=cargo16_phone.ClientID %>').addClass("user-input tooltip-phone");
        $('#<%=cargo16_movil.ClientID %>').addClass("user-input tooltip-movil");
        $('#<%=cargo16_municipioDDL.ClientID %>').addClass("user-input");
        $('#<%=cargo16_departamentoDDL.ClientID %>').addClass("user-input");

        $('#<%=cargo17_name.ClientID %>').addClass("user-input tooltip-name");
        $('#<%=cargo17_identification_number.ClientID %>').addClass("user-input tooltip-identification-number");
        $('#<%=cargo17_address.ClientID %>').addClass("user-input tooltip-address");
        $('#<%=cargo17_email.ClientID %>').addClass("user-input tooltip-email");
        $('#<%=cargo17_phone.ClientID %>').addClass("user-input tooltip-phone");
        $('#<%=cargo17_movil.ClientID %>').addClass("user-input tooltip-movil");
        $('#<%=cargo17_municipioDDL.ClientID %>').addClass("user-input");
        $('#<%=cargo17_departamentoDDL.ClientID %>').addClass("user-input");

        $('#<%=cargo18_name.ClientID %>').addClass("user-input tooltip-name");
        $('#<%=cargo18_identification_number.ClientID %>').addClass("user-input tooltip-identification-number");
        $('#<%=cargo18_address.ClientID %>').addClass("user-input tooltip-address");
        $('#<%=cargo18_email.ClientID %>').addClass("user-input tooltip-email");
        $('#<%=cargo18_phone.ClientID %>').addClass("user-input tooltip-phone");
        $('#<%=cargo18_movil.ClientID %>').addClass("user-input tooltip-movil");
        $('#<%=cargo18_municipioDDL.ClientID %>').addClass("user-input");
        $('#<%=cargo18_departamentoDDL.ClientID %>').addClass("user-input");

        $('#<%=cargo19_name.ClientID %>').addClass("user-input tooltip-name");
        $('#<%=cargo19_identification_number.ClientID %>').addClass("user-input tooltip-identification-number");
        $('#<%=cargo19_address.ClientID %>').addClass("user-input tooltip-address");
        $('#<%=cargo19_email.ClientID %>').addClass("user-input tooltip-email");
        $('#<%=cargo19_phone.ClientID %>').addClass("user-input tooltip-phone");
        $('#<%=cargo19_movil.ClientID %>').addClass("user-input tooltip-movil");
        $('#<%=cargo19_municipioDDL.ClientID %>').addClass("user-input");
        $('#<%=cargo19_departamentoDDL.ClientID %>').addClass("user-input");

        $('#<%=cargo20_name.ClientID %>').addClass("user-input tooltip-name");
        $('#<%=cargo20_identification_number.ClientID %>').addClass("user-input tooltip-identification-number");
        $('#<%=cargo20_address.ClientID %>').addClass("user-input tooltip-address");
        $('#<%=cargo20_email.ClientID %>').addClass("user-input tooltip-email");
        $('#<%=cargo20_phone.ClientID %>').addClass("user-input tooltip-phone");
        $('#<%=cargo20_movil.ClientID %>').addClass("user-input tooltip-movil");
        $('#<%=cargo20_municipioDDL.ClientID %>').addClass("user-input");
        $('#<%=cargo20_departamentoDDL.ClientID %>').addClass("user-input");

        $('#<%=cargo21_name.ClientID %>').addClass("user-input tooltip-name");
        $('#<%=cargo21_identification_number.ClientID %>').addClass("user-input tooltip-identification-number");
        $('#<%=cargo21_address.ClientID %>').addClass("user-input tooltip-address");
        $('#<%=cargo21_email.ClientID %>').addClass("user-input tooltip-email");
        $('#<%=cargo21_phone.ClientID %>').addClass("user-input tooltip-phone");
        $('#<%=cargo21_movil.ClientID %>').addClass("user-input tooltip-movil");
        $('#<%=cargo21_municipioDDL.ClientID %>').addClass("user-input");
        $('#<%=cargo21_departamentoDDL.ClientID %>').addClass("user-input");

        $('#<%=cargo22_name.ClientID %>').addClass("user-input tooltip-name");
        $('#<%=cargo22_identification_number.ClientID %>').addClass("user-input tooltip-identification-number");
        $('#<%=cargo22_address.ClientID %>').addClass("user-input tooltip-address");
        $('#<%=cargo22_email.ClientID %>').addClass("user-input tooltip-email");
        $('#<%=cargo22_phone.ClientID %>').addClass("user-input tooltip-phone");
        $('#<%=cargo22_movil.ClientID %>').addClass("user-input tooltip-movil");
        $('#<%=cargo22_municipioDDL.ClientID %>').addClass("user-input");
        $('#<%=cargo22_departamentoDDL.ClientID %>').addClass("user-input");

        $('#<%=cargo23_name.ClientID %>').addClass("user-input tooltip-name");
        $('#<%=cargo23_identification_number.ClientID %>').addClass("user-input tooltip-identification-number");
        $('#<%=cargo23_address.ClientID %>').addClass("user-input tooltip-address");
        $('#<%=cargo23_email.ClientID %>').addClass("user-input tooltip-email");
        $('#<%=cargo23_phone.ClientID %>').addClass("user-input tooltip-phone");
        $('#<%=cargo23_movil.ClientID %>').addClass("user-input tooltip-movil");
        $('#<%=cargo23_municipioDDL.ClientID %>').addClass("user-input");
        $('#<%=cargo23_departamentoDDL.ClientID %>').addClass("user-input");

        $('#<%=cargo24_name.ClientID %>').addClass("user-input tooltip-name");
        $('#<%=cargo24_identification_number.ClientID %>').addClass("user-input tooltip-identification-number");
        $('#<%=cargo24_address.ClientID %>').addClass("user-input tooltip-address");
        $('#<%=cargo24_email.ClientID %>').addClass("user-input tooltip-email");
        $('#<%=cargo24_phone.ClientID %>').addClass("user-input tooltip-phone");
        $('#<%=cargo24_movil.ClientID %>').addClass("user-input tooltip-movil");
        $('#<%=cargo24_municipioDDL.ClientID %>').addClass("user-input");
        $('#<%=cargo24_departamentoDDL.ClientID %>').addClass("user-input");

        $('#<%=cargo25_name.ClientID %>').addClass("user-input tooltip-name");
        $('#<%=cargo25_identification_number.ClientID %>').addClass("user-input tooltip-identification-number");
        $('#<%=cargo25_address.ClientID %>').addClass("user-input tooltip-address");
        $('#<%=cargo25_email.ClientID %>').addClass("user-input tooltip-email");
        $('#<%=cargo25_phone.ClientID %>').addClass("user-input tooltip-phone");
        $('#<%=cargo25_movil.ClientID %>').addClass("user-input tooltip-movil");
        $('#<%=cargo25_municipioDDL.ClientID %>').addClass("user-input");
        $('#<%=cargo25_departamentoDDL.ClientID %>').addClass("user-input");

        $('#<%=cargo26_name.ClientID %>').addClass("user-input tooltip-name");
        $('#<%=cargo26_identification_number.ClientID %>').addClass("user-input tooltip-identification-number");
        $('#<%=cargo26_address.ClientID %>').addClass("user-input tooltip-address");
        $('#<%=cargo26_email.ClientID %>').addClass("user-input tooltip-email");
        $('#<%=cargo26_phone.ClientID %>').addClass("user-input tooltip-phone");
        $('#<%=cargo26_movil.ClientID %>').addClass("user-input tooltip-movil");
        $('#<%=cargo26_municipioDDL.ClientID %>').addClass("user-input");
        $('#<%=cargo26_departamentoDDL.ClientID %>').addClass("user-input");


        $('#<%=mixer_name.ClientID %>').addClass("user-input tooltip-name");
        $('#<%=mixer_identification_number.ClientID %>').addClass("user-input tooltip-identification-number");
        $('#<%=mixer_address.ClientID %>').addClass("user-input tooltip-address");
        $('#<%=mixer_email.ClientID %>').addClass("user-input tooltip-email");
        $('#<%=mixer_phone.ClientID %>').addClass("user-input tooltip-phone");
        $('#<%=mixer_movil.ClientID %>').addClass("user-input tooltip-movil");
        $('#<%=mixer_municipioDDL.ClientID %>').addClass("user-input");
        $('#<%=mixer_departamentoDDL.ClientID %>').addClass("user-input");

        /* Crea la función de presentación del tooltip en todos los campos */
        $('.tooltip-name').tooltip({ bodyHandler: function () { return '<asp:Literal id="tooltip_name" runat="server"></asp:Literal>'; }, showURL: false });
        $('.tooltip-identification-number').tooltip({ bodyHandler: function () { return '<asp:Literal id="tooltip_identification_number" runat="server"></asp:Literal>'; }, showURL: false });
        $('.tooltip-address').tooltip({ bodyHandler: function () { return '<asp:Literal id="tooltip_address" runat="server"></asp:Literal>'; }, showURL: false });
        $('.tooltip-email').tooltip({ bodyHandler: function () { return '<asp:Literal id="tooltip_email" runat="server"></asp:Literal>'; }, showURL: false });
        $('.tooltip-phone').tooltip({ bodyHandler: function () { return '<asp:Literal id="tooltip_phone" runat="server"></asp:Literal>'; }, showURL: false });
        $('.tooltip-movil').tooltip({ bodyHandler: function () { return '<asp:Literal id="tooltip_movil" runat="server"></asp:Literal>'; }, showURL: false });
    
        <%
        if (showAdvancedForm || (project_state_id == 5 && section_state_id != 10) 
            || (project_state_id == 5 && section_state_id != 10) || ((project_state_id == 2 
            || project_state_id == 3 || project_state_id == 4 || project_state_id == 6 
            || project_state_id == 7 || project_state_id >= 8) && user_role <= 1))
        { %>
            DisableEnableForm(true, 'desactivar');
        <%
        }
        %>

        /* Oculta o muestra los campos de ubicación segun la seleccion del checkbox que
        indica si el productor esta en colombia o fuera de colombia */
        checkDirectorLocalizationFields();
        checkWriterLocalizationFields();
        checkPhotographyDirectorLocalizationFields();
        checkArtDirectorLocalizationFields();
        checkMusicAuthorLocalizationFields();
        checkEditorLocalizationFields();
        checkCameramanLocalizationFields();
        checkMakeupArtistLocalizationFields();
        checkCostumeLocalizationFields();
        checkDresserLocalizationFields();
        checkCastingLocalizationFields();
        checkScriptLocalizationFields();
        checkSoundmanLocalizationFields();
        checkcargo14LocalizationFields();
        checkcargo15LocalizationFields();
        checkcargo16LocalizationFields();
        checkcargo17LocalizationFields();
        checkcargo18LocalizationFields();
        checkcargo19LocalizationFields();
        checkcargo20LocalizationFields();
        checkcargo21LocalizationFields();
        checkcargo22LocalizationFields();
        checkcargo23LocalizationFields();
        checkcargo24LocalizationFields();
        checkcargo25LocalizationFields();
        checkcargo26LocalizationFields();
        checkMixerLocalizationFields();
        $('#<%=director_localization_out_of_colombia.ClientID %>').change(function () {
            checkDirectorLocalizationFields();
        });
        $('#<%=writer_localization_out_of_colombia.ClientID %>').change(function () {
            checkWriterLocalizationFields();
        });
        $('#<%=photography_director_localization_out_of_colombia.ClientID %>').change(function () {
            checkPhotographyDirectorLocalizationFields();
        });
        $('#<%=art_director_localization_out_of_colombia.ClientID %>').change(function () {
            checkArtDirectorLocalizationFields();
        });
        $('#<%=music_author_localization_out_of_colombia.ClientID %>').change(function () {
            checkMusicAuthorLocalizationFields();
        });
        $('#<%=editor_localization_out_of_colombia.ClientID %>').change(function () {
            checkEditorLocalizationFields();
        });
        $('#<%=cameraman_localization_out_of_colombia.ClientID %>').change(function () {
            checkCameramanLocalizationFields();
        });
        $('#<%=makeup_artist_localization_out_of_colombia.ClientID %>').change(function () {
            checkMakeupArtistLocalizationFields();
        });
        $('#<%=costume_localization_out_of_colombia.ClientID %>').change(function () {
            checkCostumeLocalizationFields();
        });
        $('#<%=dresser_localization_out_of_colombia.ClientID %>').change(function () {
            checkDresserLocalizationFields();
        });
        $('#<%=casting_localization_out_of_colombia.ClientID %>').change(function () {
            checkCastingLocalizationFields();
        });
        $('#<%=script_localization_out_of_colombia.ClientID %>').change(function () {
            checkScriptLocalizationFields();
        });
        $('#<%=soundman_localization_out_of_colombia.ClientID %>').change(function () {
            checkSoundmanLocalizationFields();
        });
        $('#<%=cargo14_localization_out_of_colombia.ClientID %>').change(function () {
            checkcargo14LocalizationFields();
        });
        $('#<%=cargo15_localization_out_of_colombia.ClientID %>').change(function () {
            checkcargo15LocalizationFields();
        });
        $('#<%=cargo16_localization_out_of_colombia.ClientID %>').change(function () {
            checkcargo16LocalizationFields();
        });
        $('#<%=cargo17_localization_out_of_colombia.ClientID %>').change(function () {
            checkcargo17LocalizationFields();
        });
        $('#<%=cargo18_localization_out_of_colombia.ClientID %>').change(function () {
            checkcargo18LocalizationFields();
        });
        $('#<%=cargo19_localization_out_of_colombia.ClientID %>').change(function () {
            checkcargo19LocalizationFields();
        });
        $('#<%=cargo20_localization_out_of_colombia.ClientID %>').change(function () {
            checkcargo20LocalizationFields();
        });
        $('#<%=cargo21_localization_out_of_colombia.ClientID %>').change(function () {
            checkcargo21LocalizationFields();
        });
        $('#<%=cargo22_localization_out_of_colombia.ClientID %>').change(function () {
            checkcargo22LocalizationFields();
        });
        $('#<%=cargo23_localization_out_of_colombia.ClientID %>').change(function () {
            checkcargo23LocalizationFields();
        });
        $('#<%=cargo24_localization_out_of_colombia.ClientID %>').change(function () {
            checkcargo24LocalizationFields();
        });
        $('#<%=cargo25_localization_out_of_colombia.ClientID %>').change(function () {
            checkcargo25LocalizationFields();
        });
        $('#<%=cargo26_localization_out_of_colombia.ClientID %>').change(function () {
            checkcargo26LocalizationFields();
        });
        $('#<%=mixer_localization_out_of_colombia.ClientID %>').change(function () {
            checkMixerLocalizationFields();
        });

        /* Cada vez que cambia el valor del select de municipios se almacena el
        valor en una variable oculta para poder recuperar el valor en el momento
        del procesamiento del formulario */
        $('#director_selectedMunicipio').val($('#<%=director_municipioDDL.ClientID %>').val());
        $('#writer_selectedMunicipio').val($('#<%=writer_municipioDDL.ClientID %>').val());
        $('#photography_director_selectedMunicipio').val($('#<%=photography_director_municipioDDL.ClientID %>').val());
        $('#art_director_selectedMunicipio').val($('#<%=art_director_municipioDDL.ClientID %>').val());
        $('#music_author_selectedMunicipio').val($('#<%=music_author_municipioDDL.ClientID %>').val());
        $('#editor_selectedMunicipio').val($('#<%=editor_municipioDDL.ClientID %>').val());
        $('#cameraman_selectedMunicipio').val($('#<%=cameraman_municipioDDL.ClientID %>').val());
        $('#makeup_artist_selectedMunicipio').val($('#<%=makeup_artist_municipioDDL.ClientID %>').val());
        $('#costume_selectedMunicipio').val($('#<%=costume_municipioDDL.ClientID %>').val());
        $('#dresser_selectedMunicipio').val($('#<%=dresser_municipioDDL.ClientID %>').val());
        $('#casting_selectedMunicipio').val($('#<%=casting_municipioDDL.ClientID %>').val());
        $('#script_selectedMunicipio').val($('#<%=script_municipioDDL.ClientID %>').val());
        $('#soundman_selectedMunicipio').val($('#<%=soundman_municipioDDL.ClientID %>').val());
        $('#cargo14_selectedMunicipio').val($('#<%=cargo14_municipioDDL.ClientID %>').val());
        $('#cargo15_selectedMunicipio').val($('#<%=cargo15_municipioDDL.ClientID %>').val());
        $('#cargo16_selectedMunicipio').val($('#<%=cargo16_municipioDDL.ClientID %>').val());
        $('#cargo17_selectedMunicipio').val($('#<%=cargo17_municipioDDL.ClientID %>').val());
        $('#cargo18_selectedMunicipio').val($('#<%=cargo18_municipioDDL.ClientID %>').val());
        $('#cargo19_selectedMunicipio').val($('#<%=cargo19_municipioDDL.ClientID %>').val());
        $('#cargo20_selectedMunicipio').val($('#<%=cargo20_municipioDDL.ClientID %>').val());
        $('#cargo21_selectedMunicipio').val($('#<%=cargo21_municipioDDL.ClientID %>').val());
        $('#cargo22_selectedMunicipio').val($('#<%=cargo22_municipioDDL.ClientID %>').val());
        $('#cargo23_selectedMunicipio').val($('#<%=cargo23_municipioDDL.ClientID %>').val());
        $('#cargo24_selectedMunicipio').val($('#<%=cargo24_municipioDDL.ClientID %>').val());
        $('#cargo25_selectedMunicipio').val($('#<%=cargo25_municipioDDL.ClientID %>').val());
        $('#cargo26_selectedMunicipio').val($('#<%=cargo26_municipioDDL.ClientID %>').val());
        $('#mixer_selectedMunicipio').val($('#<%=mixer_municipioDDL.ClientID %>').val());

        $('#<%=director_municipioDDL.ClientID %>').change(function () {
            $('#director_selectedMunicipio').val($('#<%=director_municipioDDL.ClientID %>').val());
        });
        $('#<%=writer_municipioDDL.ClientID %>').change(function () {
            $('#writer_selectedMunicipio').val($('#<%=writer_municipioDDL.ClientID %>').val());
        });
        $('#<%=photography_director_municipioDDL.ClientID %>').change(function () {
            $('#photography_director_selectedMunicipio').val($('#<%=photography_director_municipioDDL.ClientID %>').val());
        });
        $('#<%=art_director_municipioDDL.ClientID %>').change(function () {
            $('#art_director_selectedMunicipio').val($('#<%=art_director_municipioDDL.ClientID %>').val());
        });
        $('#<%=music_author_municipioDDL.ClientID %>').change(function () {
            $('#music_author_selectedMunicipio').val($('#<%=music_author_municipioDDL.ClientID %>').val());
        });
        $('#<%=editor_municipioDDL.ClientID %>').change(function () {
            $('#editor_selectedMunicipio').val($('#<%=editor_municipioDDL.ClientID %>').val());
        });
        $('#<%=cameraman_municipioDDL.ClientID %>').change(function () {
            $('#cameraman_selectedMunicipio').val($('#<%=cameraman_municipioDDL.ClientID %>').val());
        });
        $('#<%=makeup_artist_municipioDDL.ClientID %>').change(function () {
            $('#makeup_artist_selectedMunicipio').val($('#<%=makeup_artist_municipioDDL.ClientID %>').val());
        });
        $('#<%=costume_municipioDDL.ClientID %>').change(function () {
            $('#costume_selectedMunicipio').val($('#<%=costume_municipioDDL.ClientID %>').val());
        });
        $('#<%=dresser_municipioDDL.ClientID %>').change(function () {
            $('#dresser_selectedMunicipio').val($('#<%=dresser_municipioDDL.ClientID %>').val());
        });
        $('#<%=casting_municipioDDL.ClientID %>').change(function () {
            $('#casting_selectedMunicipio').val($('#<%=casting_municipioDDL.ClientID %>').val());
        });
        $('#<%=script_municipioDDL.ClientID %>').change(function () {
            $('#script_selectedMunicipio').val($('#<%=script_municipioDDL.ClientID %>').val());
        });
        $('#<%=soundman_municipioDDL.ClientID %>').change(function () {
            $('#soundman_selectedMunicipio').val($('#<%=soundman_municipioDDL.ClientID %>').val());
        });
        $('#<%=cargo14_municipioDDL.ClientID %>').change(function () {
            $('#cargo14_selectedMunicipio').val($('#<%=cargo14_municipioDDL.ClientID %>').val());
        });
        $('#<%=cargo15_municipioDDL.ClientID %>').change(function () {
            $('#cargo15_selectedMunicipio').val($('#<%=cargo15_municipioDDL.ClientID %>').val());
        });
        $('#<%=cargo16_municipioDDL.ClientID %>').change(function () {
            $('#cargo16_selectedMunicipio').val($('#<%=cargo16_municipioDDL.ClientID %>').val());
        });
        $('#<%=cargo17_municipioDDL.ClientID %>').change(function () {
            $('#cargo17_selectedMunicipio').val($('#<%=cargo17_municipioDDL.ClientID %>').val());
        });
        $('#<%=cargo18_municipioDDL.ClientID %>').change(function () {
            $('#cargo18_selectedMunicipio').val($('#<%=cargo18_municipioDDL.ClientID %>').val());
        });
        $('#<%=cargo19_municipioDDL.ClientID %>').change(function () {
            $('#cargo19_selectedMunicipio').val($('#<%=cargo19_municipioDDL.ClientID %>').val());
        });
        $('#<%=cargo20_municipioDDL.ClientID %>').change(function () {
            $('#cargo20_selectedMunicipio').val($('#<%=cargo20_municipioDDL.ClientID %>').val());
        });
        $('#<%=cargo21_municipioDDL.ClientID %>').change(function () {
            $('#cargo21_selectedMunicipio').val($('#<%=cargo21_municipioDDL.ClientID %>').val());
        });
        $('#<%=cargo22_municipioDDL.ClientID %>').change(function () {
            $('#cargo22_selectedMunicipio').val($('#<%=cargo22_municipioDDL.ClientID %>').val());
        });
        $('#<%=cargo23_municipioDDL.ClientID %>').change(function () {
            $('#cargo23_selectedMunicipio').val($('#<%=cargo23_municipioDDL.ClientID %>').val());
        });
        $('#<%=cargo24_municipioDDL.ClientID %>').change(function () {
            $('#cargo24_selectedMunicipio').val($('#<%=cargo24_municipioDDL.ClientID %>').val());
        });
        $('#<%=cargo25_municipioDDL.ClientID %>').change(function () {
            $('#cargo25_selectedMunicipio').val($('#<%=cargo25_municipioDDL.ClientID %>').val());
        });
        $('#<%=cargo26_municipioDDL.ClientID %>').change(function () {
            $('#cargo26_selectedMunicipio').val($('#<%=cargo26_municipioDDL.ClientID %>').val());
        });
        $('#<%=mixer_municipioDDL.ClientID %>').change(function () {
            $('#mixer_selectedMunicipio').val($('#<%=mixer_municipioDDL.ClientID %>').val());
        });

        // Solo se ejecuta cuando seleccionamos alguna opcion.
        $('#<%=director_departamentoDDL.ClientID %>').change(function () {
            $('#<%=director_municipioDDL.ClientID %>').empty().append('<option selected="selected" value="0">Cargando...</option>');
            $.ajax({
                type: "POST",
                url: "Default.aspx/obtenerMunicipios",
                data: '{departamento: "' + $('#<%=director_departamentoDDL.ClientID%>').val() + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnDirectorMunicipiosPopulated,
                failure: function (response) {
                    alert(response.d);
                }
            });
        });
        $('#<%=writer_departamentoDDL.ClientID %>').change(function () {
            $('#<%=writer_municipioDDL.ClientID %>').empty().append('<option selected="selected" value="0">Cargando...</option>');
            $.ajax({
                type: "POST",
                url: "Default.aspx/obtenerMunicipios",
                data: '{departamento: "' + $('#<%=writer_departamentoDDL.ClientID%>').val() + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnWriterMunicipiosPopulated,
                failure: function (response) {
                    alert(response.d);
                }
            });
        });
        $('#<%=photography_director_departamentoDDL.ClientID %>').change(function () {
            $('#<%=photography_director_municipioDDL.ClientID %>').empty().append('<option selected="selected" value="0">Cargando...</option>');
            $.ajax({
                type: "POST",
                url: "Default.aspx/obtenerMunicipios",
                data: '{departamento: "' + $('#<%=photography_director_departamentoDDL.ClientID%>').val() + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnPhotographyDirectorMunicipiosPopulated,
                failure: function (response) {
                    alert(response.d);
                }
            });
        });
        $('#<%=art_director_departamentoDDL.ClientID %>').change(function () {
            $('#<%=art_director_municipioDDL.ClientID %>').empty().append('<option selected="selected" value="0">Cargando...</option>');
            $.ajax({
                type: "POST",
                url: "Default.aspx/obtenerMunicipios",
                data: '{departamento: "' + $('#<%=art_director_departamentoDDL.ClientID%>').val() + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnArtDirectorMunicipiosPopulated,
                failure: function (response) {
                    alert(response.d);
                }
            });
        });
        $('#<%=music_author_departamentoDDL.ClientID %>').change(function () {
            $('#<%=music_author_municipioDDL.ClientID %>').empty().append('<option selected="selected" value="0">Cargando...</option>');
            $.ajax({
                type: "POST",
                url: "Default.aspx/obtenerMunicipios",
                data: '{departamento: "' + $('#<%=music_author_departamentoDDL.ClientID%>').val() + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnMusicAuthorMunicipiosPopulated,
                failure: function (response) {
                    alert(response.d);
                }
            });
        });
        $('#<%=editor_departamentoDDL.ClientID %>').change(function () {
            $('#<%=editor_municipioDDL.ClientID %>').empty().append('<option selected="selected" value="0">Cargando...</option>');
            $.ajax({
                type: "POST",
                url: "Default.aspx/obtenerMunicipios",
                data: '{departamento: "' + $('#<%=editor_departamentoDDL.ClientID%>').val() + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnEditorMunicipiosPopulated,
                failure: function (response) {
                    alert(response.d);
                }
            });
        });
        $('#<%=cameraman_departamentoDDL.ClientID %>').change(function () {
            $('#<%=cameraman_municipioDDL.ClientID %>').empty().append('<option selected="selected" value="0">Cargando...</option>');
            $.ajax({
                type: "POST",
                url: "Default.aspx/obtenerMunicipios",
                data: '{departamento: "' + $('#<%=cameraman_departamentoDDL.ClientID%>').val() + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnCameramanMunicipiosPopulated,
                failure: function (response) {
                    alert(response.d);
                }
            });
        });
        $('#<%=makeup_artist_departamentoDDL.ClientID %>').change(function () {
            $('#<%=makeup_artist_municipioDDL.ClientID %>').empty().append('<option selected="selected" value="0">Cargando...</option>');
            $.ajax({
                type: "POST",
                url: "Default.aspx/obtenerMunicipios",
                data: '{departamento: "' + $('#<%=makeup_artist_departamentoDDL.ClientID%>').val() + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnMakeupArtistMunicipiosPopulated,
                failure: function (response) {
                    alert(response.d);
                }
            });
        });
        $('#<%=costume_departamentoDDL.ClientID %>').change(function () {
            $('#<%=costume_municipioDDL.ClientID %>').empty().append('<option selected="selected" value="0">Cargando...</option>');
            $.ajax({
                type: "POST",
                url: "Default.aspx/obtenerMunicipios",
                data: '{departamento: "' + $('#<%=costume_departamentoDDL.ClientID%>').val() + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnCostumeMunicipiosPopulated,
                failure: function (response) {
                    alert(response.d);
                }
            });
        });
        $('#<%=dresser_departamentoDDL.ClientID %>').change(function () {
            $('#<%=dresser_municipioDDL.ClientID %>').empty().append('<option selected="selected" value="0">Cargando...</option>');
            $.ajax({
                type: "POST",
                url: "Default.aspx/obtenerMunicipios",
                data: '{departamento: "' + $('#<%=dresser_departamentoDDL.ClientID%>').val() + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnDresserMunicipiosPopulated,
                failure: function (response) {
                    alert(response.d);
                }
            });
        });
        $('#<%=casting_departamentoDDL.ClientID %>').change(function () {
            $('#<%=casting_municipioDDL.ClientID %>').empty().append('<option selected="selected" value="0">Cargando...</option>');
            $.ajax({
                type: "POST",
                url: "Default.aspx/obtenerMunicipios",
                data: '{departamento: "' + $('#<%=casting_departamentoDDL.ClientID%>').val() + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnCastingMunicipiosPopulated,
                failure: function (response) {
                    alert(response.d);
                }
            });
        });
        $('#<%=script_departamentoDDL.ClientID %>').change(function () {
            $('#<%=script_municipioDDL.ClientID %>').empty().append('<option selected="selected" value="0">Cargando...</option>');
            $.ajax({
                type: "POST",
                url: "Default.aspx/obtenerMunicipios",
                data: '{departamento: "' + $('#<%=script_departamentoDDL.ClientID%>').val() + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnScriptMunicipiosPopulated,
                failure: function (response) {
                    alert(response.d);
                }
            });
        });
        $('#<%=soundman_departamentoDDL.ClientID %>').change(function () {
            $('#<%=soundman_municipioDDL.ClientID %>').empty().append('<option selected="selected" value="0">Cargando...</option>');
            $.ajax({
                type: "POST",
                url: "Default.aspx/obtenerMunicipios",
                data: '{departamento: "' + $('#<%=soundman_departamentoDDL.ClientID%>').val() + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnSoundmanMunicipiosPopulated,
                failure: function (response) {
                    alert(response.d);
                }
            });
        });
        $('#<%=cargo14_departamentoDDL.ClientID %>').change(function () {
            $('#<%=cargo14_municipioDDL.ClientID %>').empty().append('<option selected="selected" value="0">Cargando...</option>');
            $.ajax({
                type: "POST",
                url: "Default.aspx/obtenerMunicipios",
                data: '{departamento: "' + $('#<%=cargo14_departamentoDDL.ClientID%>').val() + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: Oncargo14MunicipiosPopulated,
                failure: function (response) {
                    alert(response.d);
                }
            });
        });
        $('#<%=cargo15_departamentoDDL.ClientID %>').change(function () {
            $('#<%=cargo15_municipioDDL.ClientID %>').empty().append('<option selected="selected" value="0">Cargando...</option>');
            $.ajax({
                type: "POST",
                url: "Default.aspx/obtenerMunicipios",
                data: '{departamento: "' + $('#<%=cargo15_departamentoDDL.ClientID%>').val() + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: Oncargo15MunicipiosPopulated,
                failure: function (response) {
                    alert(response.d);
                }
            });
        });
        $('#<%=cargo16_departamentoDDL.ClientID %>').change(function () {
            $('#<%=cargo16_municipioDDL.ClientID %>').empty().append('<option selected="selected" value="0">Cargando...</option>');
            $.ajax({
                type: "POST",
                url: "Default.aspx/obtenerMunicipios",
                data: '{departamento: "' + $('#<%=cargo16_departamentoDDL.ClientID%>').val() + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: Oncargo16MunicipiosPopulated,
                failure: function (response) {
                    alert(response.d);
                }
            });
        });
        $('#<%=cargo17_departamentoDDL.ClientID %>').change(function () {
            $('#<%=cargo17_municipioDDL.ClientID %>').empty().append('<option selected="selected" value="0">Cargando...</option>');
            $.ajax({
                type: "POST",
                url: "Default.aspx/obtenerMunicipios",
                data: '{departamento: "' + $('#<%=cargo17_departamentoDDL.ClientID%>').val() + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: Oncargo17MunicipiosPopulated,
                failure: function (response) {
                    alert(response.d);
                }
            });
        });
        $('#<%=cargo18_departamentoDDL.ClientID %>').change(function () {
            $('#<%=cargo18_municipioDDL.ClientID %>').empty().append('<option selected="selected" value="0">Cargando...</option>');
            $.ajax({
                type: "POST",
                url: "Default.aspx/obtenerMunicipios",
                data: '{departamento: "' + $('#<%=cargo18_departamentoDDL.ClientID%>').val() + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: Oncargo18MunicipiosPopulated,
                failure: function (response) {
                    alert(response.d);
                }
            });
        });
        $('#<%=cargo19_departamentoDDL.ClientID %>').change(function () {
            $('#<%=cargo19_municipioDDL.ClientID %>').empty().append('<option selected="selected" value="0">Cargando...</option>');
            $.ajax({
                type: "POST",
                url: "Default.aspx/obtenerMunicipios",
                data: '{departamento: "' + $('#<%=cargo19_departamentoDDL.ClientID%>').val() + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: Oncargo19MunicipiosPopulated,
                failure: function (response) {
                    alert(response.d);
                }
            });
        });
        $('#<%=cargo20_departamentoDDL.ClientID %>').change(function () {
            $('#<%=cargo20_municipioDDL.ClientID %>').empty().append('<option selected="selected" value="0">Cargando...</option>');
            $.ajax({
                type: "POST",
                url: "Default.aspx/obtenerMunicipios",
                data: '{departamento: "' + $('#<%=cargo20_departamentoDDL.ClientID%>').val() + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: Oncargo20MunicipiosPopulated,
                failure: function (response) {
                    alert(response.d);
                }
            });
        });
        $('#<%=cargo21_departamentoDDL.ClientID %>').change(function () {
            $('#<%=cargo21_municipioDDL.ClientID %>').empty().append('<option selected="selected" value="0">Cargando...</option>');
            $.ajax({
                type: "POST",
                url: "Default.aspx/obtenerMunicipios",
                data: '{departamento: "' + $('#<%=cargo21_departamentoDDL.ClientID%>').val() + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: Oncargo21MunicipiosPopulated,
                failure: function (response) {
                    alert(response.d);
                }
            });
        });
        $('#<%=cargo22_departamentoDDL.ClientID %>').change(function () {
            $('#<%=cargo22_municipioDDL.ClientID %>').empty().append('<option selected="selected" value="0">Cargando...</option>');
            $.ajax({
                type: "POST",
                url: "Default.aspx/obtenerMunicipios",
                data: '{departamento: "' + $('#<%=cargo22_departamentoDDL.ClientID%>').val() + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: Oncargo22MunicipiosPopulated,
                failure: function (response) {
                    alert(response.d);
                }
            });
        });
        $('#<%=cargo23_departamentoDDL.ClientID %>').change(function () {
            $('#<%=cargo23_municipioDDL.ClientID %>').empty().append('<option selected="selected" value="0">Cargando...</option>');
            $.ajax({
                type: "POST",
                url: "Default.aspx/obtenerMunicipios",
                data: '{departamento: "' + $('#<%=cargo23_departamentoDDL.ClientID%>').val() + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: Oncargo23MunicipiosPopulated,
                failure: function (response) {
                    alert(response.d);
                }
            });
        });
        $('#<%=cargo24_departamentoDDL.ClientID %>').change(function () {
            $('#<%=cargo24_municipioDDL.ClientID %>').empty().append('<option selected="selected" value="0">Cargando...</option>');
            $.ajax({
                type: "POST",
                url: "Default.aspx/obtenerMunicipios",
                data: '{departamento: "' + $('#<%=cargo24_departamentoDDL.ClientID%>').val() + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: Oncargo24MunicipiosPopulated,
                failure: function (response) {
                    alert(response.d);
                }
            });
        });
        $('#<%=cargo25_departamentoDDL.ClientID %>').change(function () {
            $('#<%=cargo25_municipioDDL.ClientID %>').empty().append('<option selected="selected" value="0">Cargando...</option>');
            $.ajax({
                type: "POST",
                url: "Default.aspx/obtenerMunicipios",
                data: '{departamento: "' + $('#<%=cargo25_departamentoDDL.ClientID%>').val() + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: Oncargo25MunicipiosPopulated,
                failure: function (response) {
                    alert(response.d);
                }
            });
        });
        $('#<%=cargo26_departamentoDDL.ClientID %>').change(function () {
            $('#<%=cargo26_municipioDDL.ClientID %>').empty().append('<option selected="selected" value="0">Cargando...</option>');
            $.ajax({
                type: "POST",
                url: "Default.aspx/obtenerMunicipios",
                data: '{departamento: "' + $('#<%=cargo26_departamentoDDL.ClientID%>').val() + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: Oncargo26MunicipiosPopulated,
                failure: function (response) {
                    alert(response.d);
                }
            });
        });
        $('#<%=mixer_departamentoDDL.ClientID %>').change(function () {
            $('#<%=mixer_municipioDDL.ClientID %>').empty().append('<option selected="selected" value="0">Cargando...</option>');
            $.ajax({
                type: "POST",
                url: "Default.aspx/obtenerMunicipios",
                data: '{departamento: "' + $('#<%=mixer_departamentoDDL.ClientID%>').val() + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnMixerMunicipiosPopulated,
                failure: function (response) {
                    alert(response.d);
                }
            });
        });

        function OnDirectorMunicipiosPopulated(response) {
            PopulateControl(response.d, $("#<%=director_municipioDDL.ClientID %>"));
        }
        function OnWriterMunicipiosPopulated(response) {
            PopulateControl(response.d, $("#<%=writer_municipioDDL.ClientID %>"));
        }
        function OnPhotographyDirectorMunicipiosPopulated(response) {
            PopulateControl(response.d, $("#<%=photography_director_municipioDDL.ClientID %>"));
        }
        function OnArtDirectorMunicipiosPopulated(response) {
            PopulateControl(response.d, $("#<%=art_director_municipioDDL.ClientID %>"));
        }
        function OnMusicAuthorMunicipiosPopulated(response) {
            PopulateControl(response.d, $("#<%=music_author_municipioDDL.ClientID %>"));
        }
        function OnEditorMunicipiosPopulated(response) {
            PopulateControl(response.d, $("#<%=editor_municipioDDL.ClientID %>"));
        }
        function OnCameramanMunicipiosPopulated(response) {
            PopulateControl(response.d, $("#<%=cameraman_municipioDDL.ClientID %>"));
        }
        function OnMakeupArtistMunicipiosPopulated(response) {
            PopulateControl(response.d, $("#<%=makeup_artist_municipioDDL.ClientID %>"));
        }
        function OnCostumeMunicipiosPopulated(response) {
            PopulateControl(response.d, $("#<%=costume_municipioDDL.ClientID %>"));
        }
        function OnDresserMunicipiosPopulated(response) {
            PopulateControl(response.d, $("#<%=dresser_municipioDDL.ClientID %>"));
        }
        function OnCastingMunicipiosPopulated(response) {
            PopulateControl(response.d, $("#<%=casting_municipioDDL.ClientID %>"));
        }
        function OnScriptMunicipiosPopulated(response) {
            PopulateControl(response.d, $("#<%=script_municipioDDL.ClientID %>"));
        }
        function OnSoundmanMunicipiosPopulated(response) {
            PopulateControl(response.d, $("#<%=soundman_municipioDDL.ClientID %>"));
        }
        function Oncargo14MunicipiosPopulated(response) {
            PopulateControl(response.d, $("#<%=cargo14_municipioDDL.ClientID %>"));
        }
        function Oncargo15MunicipiosPopulated(response) {
            PopulateControl(response.d, $("#<%=cargo15_municipioDDL.ClientID %>"));
        }
        function Oncargo16MunicipiosPopulated(response) {
            PopulateControl(response.d, $("#<%=cargo16_municipioDDL.ClientID %>"));
        }
        function Oncargo17MunicipiosPopulated(response) {
            PopulateControl(response.d, $("#<%=cargo17_municipioDDL.ClientID %>"));
        }
        function Oncargo18MunicipiosPopulated(response) {
            PopulateControl(response.d, $("#<%=cargo17_municipioDDL.ClientID %>"));
        }
        function Oncargo18MunicipiosPopulated(response) {
            PopulateControl(response.d, $("#<%=cargo18_municipioDDL.ClientID %>"));
        }
        function Oncargo19MunicipiosPopulated(response) {
            PopulateControl(response.d, $("#<%=cargo19_municipioDDL.ClientID %>"));
        }
        function Oncargo20MunicipiosPopulated(response) {
            PopulateControl(response.d, $("#<%=cargo20_municipioDDL.ClientID %>"));
        }
        function Oncargo21MunicipiosPopulated(response) {
            PopulateControl(response.d, $("#<%=cargo21_municipioDDL.ClientID %>"));
        }
        function Oncargo22MunicipiosPopulated(response) {
            PopulateControl(response.d, $("#<%=cargo22_municipioDDL.ClientID %>"));
        }
        function Oncargo23MunicipiosPopulated(response) {
            PopulateControl(response.d, $("#<%=cargo23_municipioDDL.ClientID %>"));
        }
        function Oncargo24MunicipiosPopulated(response) {
            PopulateControl(response.d, $("#<%=cargo24_municipioDDL.ClientID %>"));
        }
        function Oncargo25MunicipiosPopulated(response) {
            PopulateControl(response.d, $("#<%=cargo25_municipioDDL.ClientID %>"));
        }
        function Oncargo26MunicipiosPopulated(response) {
            PopulateControl(response.d, $("#<%=cargo26_municipioDDL.ClientID %>"));
        }
        function OnMixerMunicipiosPopulated(response) {
            PopulateControl(response.d, $("#<%=mixer_municipioDDL.ClientID %>"));
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

         $('#loading').hide();
    });
    function checkDirectorLocalizationFields() 
    {
        if ($('#<%=director_localization_out_of_colombia.ClientID %>').is(':checked')) 
        {
            $('#director_departamento_field').hide();
            $('#director_municipio_field').hide();
            $('#director_pais_field').show();
            $('#director_ciudad_field').show();
        }
        else 
        {
            $('#director_departamento_field').show();
            $('#director_municipio_field').show();
            $('#director_pais_field').hide();
            $('#director_ciudad_field').hide();
        }
    }
    function checkWriterLocalizationFields() 
    {
        if ($('#<%=writer_localization_out_of_colombia.ClientID %>').is(':checked')) 
        {
            $('#writer_departamento_field').hide();
            $('#writer_municipio_field').hide();
            $('#writer_pais_field').show();
            $('#writer_ciudad_field').show();
        }
        else 
        {
            $('#writer_departamento_field').show();
            $('#writer_municipio_field').show();
            $('#writer_pais_field').hide();
            $('#writer_ciudad_field').hide();
        }
    }
    function checkPhotographyDirectorLocalizationFields() 
    {
        if ($('#<%=photography_director_localization_out_of_colombia.ClientID %>').is(':checked')) 
        {
            $('#photography_director_departamento_field').hide();
            $('#photography_director_municipio_field').hide();
            $('#photography_director_pais_field').show();
            $('#photography_director_ciudad_field').show();
        }
        else 
        {
            $('#photography_director_departamento_field').show();
            $('#photography_director_municipio_field').show();
            $('#photography_director_pais_field').hide();
            $('#photography_director_ciudad_field').hide();
        }
    }
    function checkArtDirectorLocalizationFields() 
    {
        if ($('#<%=art_director_localization_out_of_colombia.ClientID %>').is(':checked')) 
        {
            $('#art_director_departamento_field').hide();
            $('#art_director_municipio_field').hide();
            $('#art_director_pais_field').show();
            $('#art_director_ciudad_field').show();
        }
        else 
        {
            $('#art_director_departamento_field').show();
            $('#art_director_municipio_field').show();
            $('#art_director_pais_field').hide();
            $('#art_director_ciudad_field').hide();
        }
    }
    function checkMusicAuthorLocalizationFields() 
    {
        if ($('#<%=music_author_localization_out_of_colombia.ClientID %>').is(':checked')) 
        {
            $('#music_author_departamento_field').hide();
            $('#music_author_municipio_field').hide();
            $('#music_author_pais_field').show();
            $('#music_author_ciudad_field').show();
        }
        else 
        {
            $('#music_author_departamento_field').show();
            $('#music_author_municipio_field').show();
            $('#music_author_pais_field').hide();
            $('#music_author_ciudad_field').hide();
        }
    }
    function checkEditorLocalizationFields() 
    {
        if ($('#<%=editor_localization_out_of_colombia.ClientID %>').is(':checked')) 
        {
            $('#editor_departamento_field').hide();
            $('#editor_municipio_field').hide();
            $('#editor_pais_field').show();
            $('#editor_ciudad_field').show();
        }
        else 
        {
            $('#editor_departamento_field').show();
            $('#editor_municipio_field').show();
            $('#editor_pais_field').hide();
            $('#editor_ciudad_field').hide();
        }
    }
    function checkCameramanLocalizationFields() 
    {
        if ($('#<%=cameraman_localization_out_of_colombia.ClientID %>').is(':checked')) 
        {
            $('#cameraman_departamento_field').hide();
            $('#cameraman_municipio_field').hide();
            $('#cameraman_pais_field').show();
            $('#cameraman_ciudad_field').show();
        }
        else 
        {
            $('#cameraman_departamento_field').show();
            $('#cameraman_municipio_field').show();
            $('#cameraman_pais_field').hide();
            $('#cameraman_ciudad_field').hide();
        }
    }
    function checkMakeupArtistLocalizationFields() 
    {
        if ($('#<%=makeup_artist_localization_out_of_colombia.ClientID %>').is(':checked')) 
        {
            $('#makeup_artist_departamento_field').hide();
            $('#makeup_artist_municipio_field').hide();
            $('#makeup_artist_pais_field').show();
            $('#makeup_artist_ciudad_field').show();
        }
        else 
        {
            $('#makeup_artist_departamento_field').show();
            $('#makeup_artist_municipio_field').show();
            $('#makeup_artist_pais_field').hide();
            $('#makeup_artist_ciudad_field').hide();
        }
    }
    function checkCostumeLocalizationFields() 
    {
        if ($('#<%=costume_localization_out_of_colombia.ClientID %>').is(':checked')) 
        {
            $('#costume_departamento_field').hide();
            $('#costume_municipio_field').hide();
            $('#costume_pais_field').show();
            $('#costume_ciudad_field').show();
        }
        else 
        {
            $('#costume_departamento_field').show();
            $('#costume_municipio_field').show();
            $('#costume_pais_field').hide();
            $('#costume_ciudad_field').hide();
        }
    }
    function checkDresserLocalizationFields() 
    {
        if ($('#<%=dresser_localization_out_of_colombia.ClientID %>').is(':checked')) 
        {
            $('#dresser_departamento_field').hide();
            $('#dresser_municipio_field').hide();
            $('#dresser_pais_field').show();
            $('#dresser_ciudad_field').show();
        }
        else 
        {
            $('#dresser_departamento_field').show();
            $('#dresser_municipio_field').show();
            $('#dresser_pais_field').hide();
            $('#dresser_ciudad_field').hide();
        }
    }
    function checkCastingLocalizationFields() 
    {
        if ($('#<%=casting_localization_out_of_colombia.ClientID %>').is(':checked')) 
        {
            $('#casting_departamento_field').hide();
            $('#casting_municipio_field').hide();
            $('#casting_pais_field').show();
            $('#casting_ciudad_field').show();
        }
        else 
        {
            $('#casting_departamento_field').show();
            $('#casting_municipio_field').show();
            $('#casting_pais_field').hide();
            $('#casting_ciudad_field').hide();
        }
    }
    function checkScriptLocalizationFields() 
    {
        if ($('#<%=script_localization_out_of_colombia.ClientID %>').is(':checked')) 
        {
            $('#script_departamento_field').hide();
            $('#script_municipio_field').hide();
            $('#script_pais_field').show();
            $('#script_ciudad_field').show();
        }
        else 
        {
            $('#script_departamento_field').show();
            $('#script_municipio_field').show();
            $('#script_pais_field').hide();
            $('#script_ciudad_field').hide();
        }
    }
    function checkSoundmanLocalizationFields() 
    {
        if ($('#<%=soundman_localization_out_of_colombia.ClientID %>').is(':checked')) 
        {
            $('#soundman_departamento_field').hide();
            $('#soundman_municipio_field').hide();
            $('#soundman_pais_field').show();
            $('#soundman_ciudad_field').show();
        }
        else 
        {
            $('#soundman_departamento_field').show();
            $('#soundman_municipio_field').show();
            $('#soundman_pais_field').hide();
            $('#soundman_ciudad_field').hide();
        }
    }
    function checkcargo14LocalizationFields() {
        if ($('#<%=cargo14_localization_out_of_colombia.ClientID %>').is(':checked')) {
            $('#cargo14_departamento_field').hide();
            $('#cargo14_municipio_field').hide();
            $('#cargo14_pais_field').show();
            $('#cargo14_ciudad_field').show();
        }
        else {
            $('#cargo14_departamento_field').show();
            $('#cargo14_municipio_field').show();
            $('#cargo14_pais_field').hide();
            $('#cargo14_ciudad_field').hide();
        }
    }
    function checkcargo15LocalizationFields() {
        if ($('#<%=cargo15_localization_out_of_colombia.ClientID %>').is(':checked')) {
            $('#cargo15_departamento_field').hide();
            $('#cargo15_municipio_field').hide();
            $('#cargo15_pais_field').show();
            $('#cargo15_ciudad_field').show();
        }
        else {
            $('#cargo15_departamento_field').show();
            $('#cargo15_municipio_field').show();
            $('#cargo15_pais_field').hide();
            $('#cargo15_ciudad_field').hide();
        }
    }
    function checkcargo16LocalizationFields() {
        if ($('#<%=cargo16_localization_out_of_colombia.ClientID %>').is(':checked')) {
            $('#cargo16_departamento_field').hide();
            $('#cargo16_municipio_field').hide();
            $('#cargo16_pais_field').show();
            $('#cargo16_ciudad_field').show();
        }
        else {
            $('#cargo16_departamento_field').show();
            $('#cargo16_municipio_field').show();
            $('#cargo16_pais_field').hide();
            $('#cargo16_ciudad_field').hide();
        }
    }
    function checkcargo17LocalizationFields() {
        if ($('#<%=cargo17_localization_out_of_colombia.ClientID %>').is(':checked')) {
            $('#cargo17_departamento_field').hide();
            $('#cargo17_municipio_field').hide();
            $('#cargo17_pais_field').show();
            $('#cargo17_ciudad_field').show();
        }
        else {
            $('#cargo17_departamento_field').show();
            $('#cargo17_municipio_field').show();
            $('#cargo17_pais_field').hide();
            $('#cargo17_ciudad_field').hide();
        }
    }
    function checkcargo18LocalizationFields() {
        if ($('#<%=cargo18_localization_out_of_colombia.ClientID %>').is(':checked')) {
            $('#cargo18_departamento_field').hide();
            $('#cargo18_municipio_field').hide();
            $('#cargo18_pais_field').show();
            $('#cargo18_ciudad_field').show();
        }
        else {
            $('#cargo18_departamento_field').show();
            $('#cargo18_municipio_field').show();
            $('#cargo18_pais_field').hide();
            $('#cargo18_ciudad_field').hide();
        }
    }
    function checkcargo19LocalizationFields() {
        if ($('#<%=cargo19_localization_out_of_colombia.ClientID %>').is(':checked')) {
            $('#cargo19_departamento_field').hide();
            $('#cargo19_municipio_field').hide();
            $('#cargo19_pais_field').show();
            $('#cargo19_ciudad_field').show();
        }
        else {
            $('#cargo19_departamento_field').show();
            $('#cargo19_municipio_field').show();
            $('#cargo19_pais_field').hide();
            $('#cargo19_ciudad_field').hide();
        }
    }
    function checkcargo20LocalizationFields() {
        if ($('#<%=cargo20_localization_out_of_colombia.ClientID %>').is(':checked')) {
            $('#cargo20_departamento_field').hide();
            $('#cargo20_municipio_field').hide();
            $('#cargo20_pais_field').show();
            $('#cargo20_ciudad_field').show();
        }
        else {
            $('#cargo20_departamento_field').show();
            $('#cargo20_municipio_field').show();
            $('#cargo20_pais_field').hide();
            $('#cargo20_ciudad_field').hide();
        }
    }
    function checkcargo21LocalizationFields() {
        if ($('#<%=cargo21_localization_out_of_colombia.ClientID %>').is(':checked')) {
            $('#cargo21_departamento_field').hide();
            $('#cargo21_municipio_field').hide();
            $('#cargo21_pais_field').show();
            $('#cargo21_ciudad_field').show();
        }
        else {
            $('#cargo21_departamento_field').show();
            $('#cargo21_municipio_field').show();
            $('#cargo21_pais_field').hide();
            $('#cargo21_ciudad_field').hide();
        }
    }
    function checkcargo22LocalizationFields() {
        if ($('#<%=cargo22_localization_out_of_colombia.ClientID %>').is(':checked')) {
            $('#cargo22_departamento_field').hide();
            $('#cargo22_municipio_field').hide();
            $('#cargo22_pais_field').show();
            $('#cargo22_ciudad_field').show();
        }
        else {
            $('#cargo22_departamento_field').show();
            $('#cargo22_municipio_field').show();
            $('#cargo22_pais_field').hide();
            $('#cargo22_ciudad_field').hide();
        }
    }
    function checkcargo23LocalizationFields() {
        if ($('#<%=cargo23_localization_out_of_colombia.ClientID %>').is(':checked')) {
            $('#cargo23_departamento_field').hide();
            $('#cargo23_municipio_field').hide();
            $('#cargo23_pais_field').show();
            $('#cargo23_ciudad_field').show();
        }
        else {
            $('#cargo23_departamento_field').show();
            $('#cargo23_municipio_field').show();
            $('#cargo23_pais_field').hide();
            $('#cargo23_ciudad_field').hide();
        }
    }
    function checkcargo24LocalizationFields() {
        if ($('#<%=cargo24_localization_out_of_colombia.ClientID %>').is(':checked')) {
            $('#cargo24_departamento_field').hide();
            $('#cargo24_municipio_field').hide();
            $('#cargo24_pais_field').show();
            $('#cargo24_ciudad_field').show();
        }
        else {
            $('#cargo24_departamento_field').show();
            $('#cargo24_municipio_field').show();
            $('#cargo24_pais_field').hide();
            $('#cargo24_ciudad_field').hide();
        }
    }
    function checkcargo25LocalizationFields() {
        if ($('#<%=cargo25_localization_out_of_colombia.ClientID %>').is(':checked')) {
            $('#cargo25_departamento_field').hide();
            $('#cargo25_municipio_field').hide();
            $('#cargo25_pais_field').show();
            $('#cargo25_ciudad_field').show();
        }
        else {
            $('#cargo25_departamento_field').show();
            $('#cargo25_municipio_field').show();
            $('#cargo25_pais_field').hide();
            $('#cargo25_ciudad_field').hide();
        }
    }
    function checkcargo26LocalizationFields() {
        if ($('#<%=cargo26_localization_out_of_colombia.ClientID %>').is(':checked')) {
            $('#cargo26_departamento_field').hide();
            $('#cargo26_municipio_field').hide();
            $('#cargo26_pais_field').show();
            $('#cargo26_ciudad_field').show();
        }
        else {
            $('#cargo26_departamento_field').show();
            $('#cargo26_municipio_field').show();
            $('#cargo26_pais_field').hide();
            $('#cargo26_ciudad_field').hide();
        }
    }
    function checkMixerLocalizationFields() 
    {
        if ($('#<%=mixer_localization_out_of_colombia.ClientID %>').is(':checked')) 
        {
            $('#mixer_departamento_field').hide();
            $('#mixer_municipio_field').hide();
            $('#mixer_pais_field').show();
            $('#mixer_ciudad_field').show();
        }
        else 
        {
            $('#mixer_departamento_field').show();
            $('#mixer_municipio_field').show();
            $('#mixer_pais_field').hide();
            $('#mixer_ciudad_field').hide();
        }
    }
</script>
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

    <!-- Menu-->
    <div class="tabs">
        <ul id='menu'>
            <li class="<%=tab_datos_proyecto_css_class %>"><a href="DatosProyecto.aspx">Datos de<br />la Obra<%=tab_datos_proyecto_revision_mark_image %></a></li>
            <li class="<%=tab_datos_productor_css_class %>"><a href="DatosProductor.aspx">Datos del<br /> Productor<%=tab_datos_productor_revision_mark_image %></a></li>
            <li class="<%=tab_productores_adicionales_css_class %>"><a href="ProductoresAdicionales.aspx">Coproductores<%=tab_datos_productores_adicionales_revision_mark_image %></a></li>
            <li class="<%=tab_datos_personal_css_class %>"><a href="DatosPersonal.aspx">Personal<%=tab_datos_personal_revision_mark_image %></a></li>
            <li class="<%=tab_datos_formato_personal_css_class %>"><a href="DatosFormatoPersonal.aspx">Registro de personal <br />artístico y técnico   <%=tab_datos_formato_personal_revision_mark_image %></a></li>
            <!--<li class="<%--=tab_datos_adjuntos_css_class --%>"><a href="DatosAdjuntos.aspx">Adjuntos<%--=tab_datos_adjuntos_revision_mark_image --%></a></li>-->
            <li class="<%=tab_datos_finalizacion_css_class %>"><a href="Finalizacion.aspx">Finalizaci&oacute;n</a></li>
        </ul>
    </div>
    <!-- End of Nav Div -->
    <form name="datos_proyecto" method="post" action="DatosProyecto.aspx">
    <input type="hidden" id="shootingFormatMM" runat="server" />
    <div id='Proyecto'>
    <fieldset>
        <legend>Listado de Personal</legend>
        <p>
            Solo diligencie la información para los cargos con los que contó la película. No es necesario que repita la información ya cargada en la pestaña anterior. No llene la información si el cargo fue desempeñado por un ciudadano extranjero que no reside en Colombia. 
        </p>
        <fieldset>
            <legend>Datos Actor / Actriz principal <%--Protagónico (a)--%> </legend>
        
        <ul>
            <li>
                <div class="field_label">Nombre</div>
                <div class="field_input">
                    <input type="text" id="director_name" name="director_name" runat="server" />
                </div>
            </li>
            
            <li>
                <div class="field_label">Nro. de documento:</div>
                <div class="field_input"><input type="text" id="director_identification_number" name="director_identification_number" runat="server" /></div>
            </li>

            <li>
                <div class="field_label">Correo electr&oacute;nico:</div>
                <div class="field_input"><input type="text" id="director_email" name="director_email" runat="server" /></div>
            </li>

            <li>
                <div class="field_label">Tel&eacute;fono:</div>
                <div class="field_input"><input type="text" id="director_phone" name="director_phone" runat="server" /></div>
            </li>

            <li>
                <div class="field_label">M&oacute;vil:</div>
                <div class="field_input"><input type="text" id="director_movil" name="director_movil" runat="server" /></div>
            </li>

          
            <li>
                <div class="field_group">Datos de ubicación</div>
            </li>
            <li>
                <div class="field_label">Direcci&oacute;n:</div>
                <div class="field_input"><input type="text" id="director_address" name="director_address" runat="server" /></div>
            </li>
            <li id="director_localization_out_of_colombia_block">
                <div class="field_label">Marque esta casilla si es fuera de Colombia:</div>
                <div class="field_input"><input type="checkbox" name="director_localization_out_of_colombia" id="director_localization_out_of_colombia" runat="server"/></div>
            </li>
            <li id="director_departamento_field">
                <div class="field_label">Departamento:</div>
                <div class="field_input">
                    <asp:DropDownList ID="director_departamentoDDL" runat="server" name="director_departamentoDDL"></asp:DropDownList>
                </div>
            </li>
            <li id="director_municipio_field">
                <div class="field_label">Municipio:<input type="hidden" name="director_selectedMunicipio" id="director_selectedMunicipio" value="0"/></div>
                <div class="field_input">
                    <asp:DropDownList ID="director_municipioDDL" runat="server" name="director_municipioDDL"></asp:DropDownList>
                </div>
            </li>
            <li id="director_pais_field">
                <div class="field_label">País:</div>
                <div class="field_input"><input type="text" name="director_country" id="director_country" runat="server"/></div>
            </li>
            <li id="director_ciudad_field">
                <div class="field_label">Ciudad:</div>
                <div class="field_input"><input type="text" name="director_state" id="director_state" runat="server"/></div>
            </li>
          </ul>
         </fieldset>
        
         <fieldset>
            <legend>Datos Actor / Actriz principal <%--Protagónico (a)--%> </legend>
        
        <ul>
            <li>
                <div class="field_label">Nombre</div>
                <div class="field_input">
                    <input type="text" id="writer_name" name="writer_name" runat="server" />
                </div>
            </li>
            
            <li>
                <div class="field_label">Nro. de documento:</div>
                <div class="field_input"><input type="text" id="writer_identification_number" name="writer_identification_number" runat="server" /></div>
            </li>

            <li>
                <div class="field_label">Correo electr&oacute;nico:</div>
                <div class="field_input"><input type="text" id="writer_email" name="writer_email" runat="server" /></div>
            </li>

            <li>
                <div class="field_label">Tel&eacute;fono:</div>
                <div class="field_input"><input type="text" id="writer_phone" name="writer_phone" runat="server" /></div>
            </li>

            <li>
                <div class="field_label">M&oacute;vil:</div>
                <div class="field_input"><input type="text" id="writer_movil" name="writer_movil" runat="server" /></div>
            </li>

            

            <li>
                <div class="field_group">Datos de ubicación</div>
            </li>
            <li>
                <div class="field_label">Direcci&oacute;n:</div>
                <div class="field_input"><input type="text" id="writer_address" name="writer_address" runat="server" /></div>
            </li>
            <li id="writer_localization_out_of_colombia_block">
                <div class="field_label">Marque esta casilla si es fuera de Colombia:</div>
                <div class="field_input"><input type="checkbox" name="writer_localization_out_of_colombia" id="writer_localization_out_of_colombia" runat="server"/></div>
            </li>
            <li id="writer_departamento_field">
                <div class="field_label">Departamento:</div>
                <div class="field_input">
                    <asp:DropDownList ID="writer_departamentoDDL" runat="server" name="writer_departamentoDDL"></asp:DropDownList>
                </div>
            </li>
            <li id="writer_municipio_field">
                <div class="field_label">Municipio:<input type="hidden" name="writer_selectedMunicipio" id="writer_selectedMunicipio" value="0"/></div>
                <div class="field_input">
                    <asp:DropDownList ID="writer_municipioDDL" runat="server" name="writer_municipioDDL"></asp:DropDownList>
                </div>
            </li>
            <li id="writer_pais_field">
                <div class="field_label">País:</div>
                <div class="field_input"><input type="text" name="writer_country" id="writer_country" runat="server"/></div>
            </li>
            <li id="writer_ciudad_field">
                <div class="field_label">Ciudad:</div>
                <div class="field_input"><input type="text" name="writer_state" id="writer_state" runat="server"/></div>
            </li>
         </ul>   
         </fieldset>

         <fieldset>
            <legend>Datos Autor del Guión o Adaptador</legend>
        
        <ul>
            <li>
                <div class="field_label">Nombre</div>
                <div class="field_input">
                    <input type="text" id="photography_director_name" name="photography_director_name" runat="server" />
                </div>
            </li>
            
            <li>
                <div class="field_label">Nro. de documento:</div>
                <div class="field_input"><input type="text" id="photography_director_identification_number" name="photography_director_identification_number" runat="server" /></div>
            </li>
           
            <li>
                <div class="field_label">Correo electr&oacute;nico:</div>
                <div class="field_input"><input type="text" id="photography_director_email" name="photography_director_email" runat="server" /></div>
            </li>

            <li>
                <div class="field_label">Tel&eacute;fono:</div>
                <div class="field_input"><input type="text" id="photography_director_phone" name="photography_director_phone" runat="server" /></div>
            </li>

            <li>
                <div class="field_label">M&oacute;vil:</div>
                <div class="field_input"><input type="text" id="photography_director_movil" name="photography_director_movil" runat="server" /></div>
            </li>

           

            <li>
	            <div class="field_group">Datos de ubicación</div>
            </li>
            <li>
	            <div class="field_label">Direcci&oacute;n:</div>
	            <div class="field_input"><input type="text" id="photography_director_address" name="photography_director_address" runat="server" /></div>
            </li>
            <li id="photography_director_localization_out_of_colombia_block">
	            <div class="field_label">Marque esta casilla si es fuera de Colombia:</div>
	            <div class="field_input"><input type="checkbox" name="photography_director_localization_out_of_colombia" id="photography_director_localization_out_of_colombia" runat="server"/></div>
            </li>
            <li id="photography_director_departamento_field">
	            <div class="field_label">Departamento:</div>
	            <div class="field_input">
		            <asp:DropDownList ID="photography_director_departamentoDDL" runat="server" name="photography_director_departamentoDDL"></asp:DropDownList>
	            </div>
            </li>
            <li id="photography_director_municipio_field">
	            <div class="field_label">Municipio:<input type="hidden" name="photography_director_selectedMunicipio" id="photography_director_selectedMunicipio" value="0"/></div>
	            <div class="field_input">
		            <asp:DropDownList ID="photography_director_municipioDDL" runat="server" name="photography_director_municipioDDL"></asp:DropDownList>
	            </div>
            </li>
            <li id="photography_director_pais_field">
	            <div class="field_label">País:</div>
	            <div class="field_input"><input type="text" name="photography_director_country" id="photography_director_country" runat="server"/></div>
            </li>
            <li id="photography_director_ciudad_field">
	            <div class="field_label">Ciudad:</div>
	            <div class="field_input"><input type="text" name="photography_director_state" id="photography_director_state" runat="server"/></div>
            </li>
          </ul>
            
         </fieldset>

         <fieldset>
            <legend>Datos Autor de la Música Original</legend>
        
        <ul>
            <li>
                <div class="field_label">Nombre</div>
                <div class="field_input">
                    <input type="text" id="art_director_name" name="art_director_name" runat="server" />
                </div>
            </li>
            
            <li>
                <div class="field_label">Nro. de documento:</div>
                <div class="field_input"><input type="text" id="art_director_identification_number" name="art_director_identification_number" runat="server" /></div>
            </li>
           
            <li>
                <div class="field_label">Correo electr&oacute;nico:</div>
                <div class="field_input"><input type="text" id="art_director_email" name="art_director_email" runat="server" /></div>
            </li>

            <li>
                <div class="field_label">Tel&eacute;fono:</div>
                <div class="field_input"><input type="text" id="art_director_phone" name="art_director_phone" runat="server" /></div>
            </li>

            <li>
                <div class="field_label">M&oacute;vil:</div>
                <div class="field_input"><input type="text" id="art_director_movil" name="art_director_movil" runat="server" /></div>
            </li>

          
            <li>
	            <div class="field_group">Datos de ubicación</div>
            </li>
            <li>
	            <div class="field_label">Direcci&oacute;n:</div>
	            <div class="field_input"><input type="text" id="art_director_address" name="art_director_address" runat="server" /></div>
            </li>
            <li id="art_director_localization_out_of_colombia_block">
	            <div class="field_label">Marque esta casilla si es fuera de Colombia:</div>
	            <div class="field_input"><input type="checkbox" name="art_director_localization_out_of_colombia" id="art_director_localization_out_of_colombia" runat="server"/></div>
            </li>
            <li id="art_director_departamento_field">
	            <div class="field_label">Departamento:</div>
	            <div class="field_input">
		            <asp:DropDownList ID="art_director_departamentoDDL" runat="server" name="art_director_departamentoDDL"></asp:DropDownList>
	            </div>
            </li>
            <li id="art_director_municipio_field">
	            <div class="field_label">Municipio:<input type="hidden" name="art_director_selectedMunicipio" id="art_director_selectedMunicipio" value="0"/></div>
	            <div class="field_input">
		            <asp:DropDownList ID="art_director_municipioDDL" runat="server" name="art_director_municipioDDL"></asp:DropDownList>
	            </div>
            </li>
            <li id="art_director_pais_field">
	            <div class="field_label">País:</div>
	            <div class="field_input"><input type="text" name="art_director_country" id="art_director_country" runat="server"/></div>
            </li>
            <li id="art_director_ciudad_field">
	            <div class="field_label">Ciudad:</div>
	            <div class="field_input"><input type="text" name="art_director_state" id="art_director_state" runat="server"/></div>
            </li>
          </ul>
            
         </fieldset>

         <fieldset>
            <legend>Datos Actor Secundario</legend>
        
        <ul>
            <li>
                <div class="field_label">Nombre</div>
                <div class="field_input">
                    <input type="text" id="music_author_name" name="music_author_name" runat="server" />
                </div>
            </li>
            
            <li>
                <div class="field_label">Nro. de documento:</div>
                <div class="field_input"><input type="text" id="music_author_identification_number" name="music_author_identification_number" runat="server" /></div>
            </li>
           
            <li>
                <div class="field_label">Correo electr&oacute;nico:</div>
                <div class="field_input"><input type="text" id="music_author_email" name="music_author_email" runat="server" /></div>
            </li>

            <li>
                <div class="field_label">Tel&eacute;fono:</div>
                <div class="field_input"><input type="text" id="music_author_phone" name="music_author_phone" runat="server" /></div>
            </li>

            <li>
                <div class="field_label">M&oacute;vil:</div>
                <div class="field_input"><input type="text" id="music_author_movil" name="music_author_movil" runat="server" /></div>
            </li>

          
            <li>
	            <div class="field_group">Datos de ubicación</div>
            </li>
            <li>
	            <div class="field_label">Direcci&oacute;n:</div>
	            <div class="field_input"><input type="text" id="music_author_address" name="music_author_address" runat="server" /></div>
            </li>
            <li id="music_author_localization_out_of_colombia_block">
	            <div class="field_label">Marque esta casilla si es fuera de Colombia:</div>
	            <div class="field_input"><input type="checkbox" name="music_author_localization_out_of_colombia" id="music_author_localization_out_of_colombia" runat="server"/></div>
            </li>
            <li id="music_author_departamento_field">
	            <div class="field_label">Departamento:</div>
	            <div class="field_input">
		            <asp:DropDownList ID="music_author_departamentoDDL" runat="server" name="music_author_departamentoDDL"></asp:DropDownList>
	            </div>
            </li>
            <li id="music_author_municipio_field">
	            <div class="field_label">Municipio:<input type="hidden" name="music_author_selectedMunicipio" id="music_author_selectedMunicipio" value="0"/></div>
	            <div class="field_input">
		            <asp:DropDownList ID="music_author_municipioDDL" runat="server" name="music_author_municipioDDL"></asp:DropDownList>
	            </div>
            </li>
            <li id="music_author_pais_field">
	            <div class="field_label">País:</div>
	            <div class="field_input"><input type="text" name="music_author_country" id="music_author_country" runat="server"/></div>
            </li>
            <li id="music_author_ciudad_field">
	            <div class="field_label">Ciudad:</div>
	            <div class="field_input"><input type="text" name="music_author_state" id="music_author_state" runat="server"/></div>
            </li>
        </ul>
            
         </fieldset>

         <fieldset>
            <legend>Datos Director de Fotografía</legend>
        
        <ul>
            <li>
                <div class="field_label">Nombre</div>
                <div class="field_input">
                    <input type="text" id="editor_name" name="editor_name" runat="server" />
                </div>
            </li>
            
            <li>
                <div class="field_label">Nro. de documento:</div>
                <div class="field_input"><input type="text" id="editor_identification_number" name="editor_identification_number" runat="server" /></div>
            </li>
           
            <li>
                <div class="field_label">Correo electr&oacute;nico:</div>
                <div class="field_input"><input type="text" id="editor_email" name="editor_email" runat="server" /></div>
            </li>

            <li>
                <div class="field_label">Tel&eacute;fono:</div>
                <div class="field_input"><input type="text" id="editor_phone" name="editor_phone" runat="server" /></div>
            </li>

            <li>
                <div class="field_label">M&oacute;vil:</div>
                <div class="field_input"><input type="text" id="editor_movil" name="editor_movil" runat="server" /></div>
            </li>

          
            <li>
	            <div class="field_group">Datos de ubicación</div>
            </li>
            <li>
	            <div class="field_label">Direcci&oacute;n:</div>
	            <div class="field_input"><input type="text" id="editor_address" name="editor_address" runat="server" /></div>
            </li>
            <li id="editor_localization_out_of_colombia_block">
	            <div class="field_label">Marque esta casilla si es fuera de Colombia:</div>
	            <div class="field_input"><input type="checkbox" name="editor_localization_out_of_colombia" id="editor_localization_out_of_colombia" runat="server"/></div>
            </li>
            <li id="editor_departamento_field">
	            <div class="field_label">Departamento:</div>
	            <div class="field_input">
		            <asp:DropDownList ID="editor_departamentoDDL" runat="server" name="editor_departamentoDDL"></asp:DropDownList>
	            </div>
            </li>
            <li id="editor_municipio_field">
	            <div class="field_label">Municipio:<input type="hidden" name="editor_selectedMunicipio" id="editor_selectedMunicipio" value="0"/></div>
	            <div class="field_input">
		            <asp:DropDownList ID="editor_municipioDDL" runat="server" name="editor_municipioDDL"></asp:DropDownList>
	            </div>
            </li>
            <li id="editor_pais_field">
	            <div class="field_label">País:</div>
	            <div class="field_input"><input type="text" name="editor_country" id="editor_country" runat="server"/></div>
            </li>
            <li id="editor_ciudad_field">
	            <div class="field_label">Ciudad:</div>
	            <div class="field_input"><input type="text" name="editor_state" id="editor_state" runat="server"/></div>
            </li>
          </ul>  
         </fieldset>

         <fieldset>
            <legend>Datos Director de Arte o Diseñador de Producción</legend>
        
        <ul>
            <li>
                <div class="field_label">Nombre</div>
                <div class="field_input">
                    <input type="text" id="cameraman_name" name="cameraman_name" runat="server" />
                </div>
            </li>
            
            <li>
                <div class="field_label">Nro. de documento:</div>
                <div class="field_input"><input type="text" id="cameraman_identification_number" name="cameraman_identification_number" runat="server" /></div>
            </li>
           
            <li>
                <div class="field_label">Correo electr&oacute;nico:</div>
                <div class="field_input"><input type="text" id="cameraman_email" name="cameraman_email" runat="server" /></div>
            </li>

            <li>
                <div class="field_label">Tel&eacute;fono:</div>
                <div class="field_input"><input type="text" id="cameraman_phone" name="cameraman_phone" runat="server" /></div>
            </li>

            <li>
                <div class="field_label">M&oacute;vil:</div>
                <div class="field_input"><input type="text" id="cameraman_movil" name="cameraman_movil" runat="server" /></div>
            </li>

            
            <li>
	            <div class="field_group">Datos de ubicación</div>
            </li>
            <li>
	            <div class="field_label">Direcci&oacute;n:</div>
	            <div class="field_input"><input type="text" id="cameraman_address" name="cameraman_address" runat="server" /></div>
            </li>
            <li id="cameraman_localization_out_of_colombia_block">
	            <div class="field_label">Marque esta casilla si es fuera de Colombia:</div>
	            <div class="field_input"><input type="checkbox" name="cameraman_localization_out_of_colombia" id="cameraman_localization_out_of_colombia" runat="server"/></div>
            </li>
            <li id="cameraman_departamento_field">
	            <div class="field_label">Departamento:</div>
	            <div class="field_input">
		            <asp:DropDownList ID="cameraman_departamentoDDL" runat="server" name="cameraman_departamentoDDL"></asp:DropDownList>
	            </div>
            </li>
            <li id="cameraman_municipio_field">
	            <div class="field_label">Municipio:<input type="hidden" name="cameraman_selectedMunicipio" id="cameraman_selectedMunicipio" value="0"/></div>
	            <div class="field_input">
		            <asp:DropDownList ID="cameraman_municipioDDL" runat="server" name="cameraman_municipioDDL"></asp:DropDownList>
	            </div>
            </li>
            <li id="cameraman_pais_field">
	            <div class="field_label">País:</div>
	            <div class="field_input"><input type="text" name="cameraman_country" id="cameraman_country" runat="server"/></div>
            </li>
            <li id="cameraman_ciudad_field">
	            <div class="field_label">Ciudad:</div>
	            <div class="field_input"><input type="text" name="cameraman_state" id="cameraman_state" runat="server"/></div>
            </li>
          </ul>
            
         </fieldset>

         <fieldset>
            <legend>Datos Diseñador de Vestuario</legend>
        
        <ul>
            <li>
                <div class="field_label">Nombre</div>
                <div class="field_input">
                    <input type="text" id="makeup_artist_name" name="makeup_artist_name" runat="server" />
                </div>
            </li>
            
            <li>
                <div class="field_label">Nro. de documento:</div>
                <div class="field_input"><input type="text" id="makeup_artist_identification_number" name="makeup_artist_identification_number" runat="server" /></div>
            </li>
           
            <li>
                <div class="field_label">Correo electr&oacute;nico:</div>
                <div class="field_input"><input type="text" id="makeup_artist_email" name="makeup_artist_email" runat="server" /></div>
            </li>

            <li>
                <div class="field_label">Tel&eacute;fono:</div>
                <div class="field_input"><input type="text" id="makeup_artist_phone" name="makeup_artist_phone" runat="server" /></div>
            </li>

            <li>
                <div class="field_label">M&oacute;vil:</div>
                <div class="field_input"><input type="text" id="makeup_artist_movil" name="makeup_artist_movil" runat="server" /></div>
            </li>

            
            <li>
	            <div class="field_group">Datos de ubicación</div>
            </li>
            <li>
	            <div class="field_label">Direcci&oacute;n:</div>
	            <div class="field_input"><input type="text" id="makeup_artist_address" name="makeup_artist_address" runat="server" /></div>
            </li>
            <li id="makeup_artist_localization_out_of_colombia_block">
	            <div class="field_label">Marque esta casilla si es fuera de Colombia:</div>
	            <div class="field_input"><input type="checkbox" name="makeup_artist_localization_out_of_colombia" id="makeup_artist_localization_out_of_colombia" runat="server"/></div>
            </li>
            <li id="makeup_artist_departamento_field">
	            <div class="field_label">Departamento:</div>
	            <div class="field_input">
		            <asp:DropDownList ID="makeup_artist_departamentoDDL" runat="server" name="makeup_artist_departamentoDDL"></asp:DropDownList>
	            </div>
            </li>
            <li id="makeup_artist_municipio_field">
	            <div class="field_label">Municipio:<input type="hidden" name="makeup_artist_selectedMunicipio" id="makeup_artist_selectedMunicipio" value="0"/></div>
	            <div class="field_input">
		            <asp:DropDownList ID="makeup_artist_municipioDDL" runat="server" name="makeup_artist_municipioDDL"></asp:DropDownList>
	            </div>
            </li>
            <li id="makeup_artist_pais_field">
	            <div class="field_label">País:</div>
	            <div class="field_input"><input type="text" name="makeup_artist_country" id="makeup_artist_country" runat="server"/></div>
            </li>
            <li id="makeup_artist_ciudad_field">
	            <div class="field_label">Ciudad:</div>
	            <div class="field_input"><input type="text" name="makeup_artist_state" id="makeup_artist_state" runat="server"/></div>
            </li>
            </ul>
         </fieldset>

         <fieldset>
            <legend>Datos Sonidista</legend>
        
        <ul>
            <li>
                <div class="field_label">Nombre</div>
                <div class="field_input">
                    <input type="text" id="costume_name" name="costume_name" runat="server" />
                </div>
            </li>
            
            <li>
                <div class="field_label">Nro. de documento:</div>
                <div class="field_input"><input type="text" id="costume_identification_number" name="costume_identification_number" runat="server" /></div>
            </li>
           
            <li>
                <div class="field_label">Correo electr&oacute;nico:</div>
                <div class="field_input"><input type="text" id="costume_email" name="costume_email" runat="server" /></div>
            </li>

            <li>
                <div class="field_label">Tel&eacute;fono:</div>
                <div class="field_input"><input type="text" id="costume_phone" name="costume_phone" runat="server" /></div>
            </li>

            <li>
                <div class="field_label">M&oacute;vil:</div>
                <div class="field_input"><input type="text" id="costume_movil" name="costume_movil" runat="server" /></div>
            </li>

           
            <li>
	            <div class="field_group">Datos de ubicación</div>
            </li>
            <li>
	            <div class="field_label">Direcci&oacute;n:</div>
	            <div class="field_input"><input type="text" id="costume_address" name="costume_address" runat="server" /></div>
            </li>
            <li id="costume_localization_out_of_colombia_block">
	            <div class="field_label">Marque esta casilla si es fuera de Colombia:</div>
	            <div class="field_input"><input type="checkbox" name="costume_localization_out_of_colombia" id="costume_localization_out_of_colombia" runat="server"/></div>
            </li>
            <li id="costume_departamento_field">
	            <div class="field_label">Departamento:</div>
	            <div class="field_input">
		            <asp:DropDownList ID="costume_departamentoDDL" runat="server" name="costume_departamentoDDL"></asp:DropDownList>
	            </div>
            </li>
            <li id="costume_municipio_field">
	            <div class="field_label">Municipio:<input type="hidden" name="costume_selectedMunicipio" id="costume_selectedMunicipio" value="0"/></div>
	            <div class="field_input">
		            <asp:DropDownList ID="costume_municipioDDL" runat="server" name="costume_municipioDDL"></asp:DropDownList>
	            </div>
            </li>
            <li id="costume_pais_field">
	            <div class="field_label">País:</div>
	            <div class="field_input"><input type="text" name="costume_country" id="costume_country" runat="server"/></div>
            </li>
            <li id="costume_ciudad_field">
	            <div class="field_label">Ciudad:</div>
	            <div class="field_input"><input type="text" name="costume_state" id="costume_state" runat="server"/></div>
            </li>
        </ul>
            
         </fieldset>

         <fieldset>
            <legend>Datos Montajista</legend>
        
        <ul>
            <li>
                <div class="field_label">Nombre</div>
                <div class="field_input">
                    <input type="text" id="dresser_name" name="dresser_name" runat="server" />
                </div>
            </li>
           
            <li>
                <div class="field_label">Nro. de documento:</div>
                <div class="field_input"><input type="text" id="dresser_identification_number" name="dresser_identification_number" runat="server" /></div>
            </li>
           
            <li>
                <div class="field_label">Correo electr&oacute;nico:</div>
                <div class="field_input"><input type="text" id="dresser_email" name="dresser_email" runat="server" /></div>
            </li>

            <li>
                <div class="field_label">Tel&eacute;fono:</div>
                <div class="field_input"><input type="text" id="dresser_phone" name="dresser_phone" runat="server" /></div>
            </li>

            <li>
                <div class="field_label">M&oacute;vil:</div>
                <div class="field_input"><input type="text" id="dresser_movil" name="dresser_movil" runat="server" /></div>
            </li>

           
            <li>
	            <div class="field_group">Datos de ubicación</div>
            </li>
            <li>
	            <div class="field_label">Direcci&oacute;n:</div>
	            <div class="field_input"><input type="text" id="dresser_address" name="dresser_address" runat="server" /></div>
            </li>
            <li id="dresser_localization_out_of_colombia_block">
	            <div class="field_label">Marque esta casilla si es fuera de Colombia:</div>
	            <div class="field_input"><input type="checkbox" name="dresser_localization_out_of_colombia" id="dresser_localization_out_of_colombia" runat="server"/></div>
            </li>
            <li id="dresser_departamento_field">
	            <div class="field_label">Departamento:</div>
	            <div class="field_input">
		            <asp:DropDownList ID="dresser_departamentoDDL" runat="server" name="dresser_departamentoDDL"></asp:DropDownList>
	            </div>
            </li>
            <li id="dresser_municipio_field">
	            <div class="field_label">Municipio:<input type="hidden" name="dresser_selectedMunicipio" id="dresser_selectedMunicipio" value="0"/></div>
	            <div class="field_input">
		            <asp:DropDownList ID="dresser_municipioDDL" runat="server" name="dresser_municipioDDL"></asp:DropDownList>
	            </div>
            </li>
            <li id="dresser_pais_field">
	            <div class="field_label">País:</div>
	            <div class="field_input"><input type="text" name="dresser_country" id="dresser_country" runat="server"/></div>
            </li>
            <li id="dresser_ciudad_field">
	            <div class="field_label">Ciudad:</div>
	            <div class="field_input"><input type="text" name="dresser_state" id="dresser_state" runat="server"/></div>
            </li>
          </ul>  
         </fieldset>

         <fieldset>
            <legend>Datos Diseñador de Sonido, Editor de Sonido Jefe o Montajista de Sonido</legend>
        
        <ul>
            <li>
                <div class="field_label">Nombre</div>
                <div class="field_input">
                    <input type="text" id="casting_name" name="casting_name" runat="server" />
                </div>
            </li>
            
            <li>
                <div class="field_label">Nro. de documento:</div>
                <div class="field_input"><input type="text" id="casting_identification_number" name="casting_identification_number" runat="server" /></div>
            </li>
           
            <li>
                <div class="field_label">Correo electr&oacute;nico:</div>
                <div class="field_input"><input type="text" id="casting_email" name="casting_email" runat="server" /></div>
            </li>

            <li>
                <div class="field_label">Tel&eacute;fono:</div>
                <div class="field_input"><input type="text" id="casting_phone" name="casting_phone" runat="server" /></div>
            </li>

            <li>
                <div class="field_label">M&oacute;vil:</div>
                <div class="field_input"><input type="text" id="casting_movil" name="casting_movil" runat="server" /></div>
            </li>

           
            <li>
	            <div class="field_group">Datos de ubicación</div>
            </li>
            <li>
	            <div class="field_label">Direcci&oacute;n:</div>
	            <div class="field_input"><input type="text" id="casting_address" name="casting_address" runat="server" /></div>
            </li>
            <li id="casting_localization_out_of_colombia_block">
	            <div class="field_label">Marque esta casilla si es fuera de Colombia:</div>
	            <div class="field_input"><input type="checkbox" name="casting_localization_out_of_colombia" id="casting_localization_out_of_colombia" runat="server"/></div>
            </li>
            <li id="casting_departamento_field">
	            <div class="field_label">Departamento:</div>
	            <div class="field_input">
		            <asp:DropDownList ID="casting_departamentoDDL" runat="server" name="casting_departamentoDDL"></asp:DropDownList>
	            </div>
            </li>
            <li id="casting_municipio_field">
	            <div class="field_label">Municipio:<input type="hidden" name="casting_selectedMunicipio" id="casting_selectedMunicipio" value="0"/></div>
	            <div class="field_input">
		            <asp:DropDownList ID="casting_municipioDDL" runat="server" name="casting_municipioDDL"></asp:DropDownList>
	            </div>
            </li>
            <li id="casting_pais_field">
	            <div class="field_label">País:</div>
	            <div class="field_input"><input type="text" name="casting_country" id="casting_country" runat="server"/></div>
            </li>
            <li id="casting_ciudad_field">
	            <div class="field_label">Ciudad:</div>
	            <div class="field_input"><input type="text" name="casting_state" id="casting_state" runat="server"/></div>
            </li>
            </ul>
         </fieldset>

         <fieldset>
            <legend>Datos Operador de Cámara</legend>
        
        <ul>
            <li>
                <div class="field_label">Nombre</div>
                <div class="field_input">
                    <input type="text" id="script_name" name="script_name" runat="server" />
                </div>
            </li>
            
            <li>
                <div class="field_label">Nro. de documento:</div>
                <div class="field_input"><input type="text" id="script_identification_number" name="script_identification_number" runat="server" /></div>
            </li>
           
            <li>
                <div class="field_label">Correo electr&oacute;nico:</div>
                <div class="field_input"><input type="text" id="script_email" name="script_email" runat="server" /></div>
            </li>

            <li>
                <div class="field_label">Tel&eacute;fono:</div>
                <div class="field_input"><input type="text" id="script_phone" name="script_phone" runat="server" /></div>
            </li>

            <li>
                <div class="field_label">M&oacute;vil:</div>
                <div class="field_input"><input type="text" id="script_movil" name="script_movil" runat="server" /></div>
            </li>

          
            <li>
	            <div class="field_group">Datos de ubicación</div>
            </li>
            <li>
	            <div class="field_label">Direcci&oacute;n:</div>
	            <div class="field_input"><input type="text" id="script_address" name="script_address" runat="server" /></div>
            </li>
            <li id="script_localization_out_of_colombia_block">
	            <div class="field_label">Marque esta casilla si es fuera de Colombia:</div>
	            <div class="field_input"><input type="checkbox" name="script_localization_out_of_colombia" id="script_localization_out_of_colombia" runat="server"/></div>
            </li>
            <li id="script_departamento_field">
	            <div class="field_label">Departamento:</div>
	            <div class="field_input">
		            <asp:DropDownList ID="script_departamentoDDL" runat="server" name="script_departamentoDDL"></asp:DropDownList>
	            </div>
            </li>
            <li id="script_municipio_field">
	            <div class="field_label">Municipio:<input type="hidden" name="script_selectedMunicipio" id="script_selectedMunicipio" value="0"/></div>
	            <div class="field_input">
		            <asp:DropDownList ID="script_municipioDDL" runat="server" name="script_municipioDDL"></asp:DropDownList>
	            </div>
            </li>
            <li id="script_pais_field">
	            <div class="field_label">País:</div>
	            <div class="field_input"><input type="text" name="script_country" id="script_country" runat="server"/></div>
            </li>
            <li id="script_ciudad_field">
	            <div class="field_label">Ciudad:</div>
	            <div class="field_input"><input type="text" name="script_state" id="script_state" runat="server"/></div>
            </li>
            </ul>
         </fieldset>

         <fieldset>
            <legend>Datos Primer Asistente de Cámara o Foquista</legend>
        <ul>
            <li>
                <div class="field_label">Nombre</div>
                <div class="field_input">
                    <input type="text" id="soundman_name" name="soundman_name" runat="server" />
                </div>
            </li>
            <li>
                <div class="field_label">Nro. de documento:</div>
                <div class="field_input"><input type="text" id="soundman_identification_number" name="soundman_identification_number" runat="server" /></div>
            </li>
            <li>
                <div class="field_label">Correo electr&oacute;nico:</div>
                <div class="field_input"><input type="text" id="soundman_email" name="soundman_email" runat="server" /></div>
            </li>
            <li>
                <div class="field_label">Tel&eacute;fono:</div>
                <div class="field_input"><input type="text" id="soundman_phone" name="soundman_phone" runat="server" /></div>
            </li>
            <li>
                <div class="field_label">M&oacute;vil:</div>
                <div class="field_input"><input type="text" id="soundman_movil" name="soundman_movil" runat="server" /></div>
            </li>
            <li>
	            <div class="field_group">Datos de ubicación</div>
            </li>
            <li>
	            <div class="field_label">Direcci&oacute;n:</div>
	            <div class="field_input"><input type="text" id="soundman_address" name="soundman_address" runat="server" /></div>
            </li>
            <li id="soundman_localization_out_of_colombia_block">
	            <div class="field_label">Marque esta casilla si es fuera de Colombia:</div>
	            <div class="field_input"><input type="checkbox" name="soundman_localization_out_of_colombia" id="soundman_localization_out_of_colombia" runat="server"/></div>
            </li>
            <li id="soundman_departamento_field">
	            <div class="field_label">Departamento:</div>
	            <div class="field_input">
		            <asp:DropDownList ID="soundman_departamentoDDL" runat="server" name="soundman_departamentoDDL"></asp:DropDownList>
	            </div>
            </li>
            <li id="soundman_municipio_field">
	            <div class="field_label">Municipio:<input type="hidden" name="soundman_selectedMunicipio" id="soundman_selectedMunicipio" value="0"/></div>
	            <div class="field_input">
		            <asp:DropDownList ID="soundman_municipioDDL" runat="server" name="soundman_municipioDDL"></asp:DropDownList>
	            </div>
            </li>
            <li id="soundman_pais_field">
	            <div class="field_label">País:</div>
	            <div class="field_input"><input type="text" name="soundman_country" id="soundman_country" runat="server"/></div>
            </li>
            <li id="soundman_ciudad_field">
	            <div class="field_label">Ciudad:</div>
	            <div class="field_input"><input type="text" name="soundman_state" id="soundman_state" runat="server"/></div>
            </li>
          </ul>
            
         </fieldset>
 <%--       inicia--%>
         <fieldset>
            <legend>Datos Gaffer, Jefe de Eléctricos, Jefe de Luces o Jefe de Luminotécnicos</legend>
        <ul>
            <li>
                <div class="field_label">Nombre</div>
                <div class="field_input">
                    <input type="text" id="cargo14_name" name="cargo14_name" runat="server" />
                </div>
            </li>
            
            <li>
                <div class="field_label">Nro. de documento:</div>
                <div class="field_input"><input type="text" id="cargo14_identification_number" name="cargo14_identification_number" runat="server" /></div>
            </li>
           
            <li>
                <div class="field_label">Correo electr&oacute;nico:</div>
                <div class="field_input"><input type="text" id="cargo14_email" name="cargo14_email" runat="server" /></div>
            </li>

            <li>
                <div class="field_label">Tel&eacute;fono:</div>
                <div class="field_input"><input type="text" id="cargo14_phone" name="cargo14_phone" runat="server" /></div>
            </li>

            <li>
                <div class="field_label">M&oacute;vil:</div>
                <div class="field_input"><input type="text" id="cargo14_movil" name="cargo14_movil" runat="server" /></div>
            </li>

          

            <li>
	            <div class="field_group">Datos de ubicación</div>
            </li>
            <li>
	            <div class="field_label">Direcci&oacute;n:</div>
	            <div class="field_input"><input type="text" id="cargo14_address" name="cargo14_address" runat="server" /></div>
            </li>
            <li id="cargo14_localization_out_of_colombia_block">
	            <div class="field_label">Marque esta casilla si es fuera de Colombia:</div>
	            <div class="field_input"><input type="checkbox" name="cargo14_localization_out_of_colombia" id="cargo14_localization_out_of_colombia" runat="server"/></div>
            </li>
            <li id="cargo14_departamento_field">
	            <div class="field_label">Departamento:</div>
	            <div class="field_input">
		            <asp:DropDownList ID="cargo14_departamentoDDL" runat="server" name="cargo14_departamentoDDL"></asp:DropDownList>
	            </div>
            </li>
            <li id="cargo14_municipio_field">
	            <div class="field_label">Municipio:<input type="hidden" name="cargo14_selectedMunicipio" id="cargo14_selectedMunicipio" value="0"/></div>
	            <div class="field_input">
		            <asp:DropDownList ID="cargo14_municipioDDL" runat="server" name="cargo14_municipioDDL"></asp:DropDownList>
	            </div>
            </li>
            <li id="cargo14_pais_field">
	            <div class="field_label">País:</div>
	            <div class="field_input"><input type="text" name="cargo14_country" id="cargo14_country" runat="server"/></div>
            </li>
            <li id="cargo14_ciudad_field">
	            <div class="field_label">Ciudad:</div>
	            <div class="field_input"><input type="text" name="cargo14_state" id="cargo14_state" runat="server"/></div>
            </li>
          </ul>
            
         </fieldset>
         <fieldset>
            <legend>Datos Maquillador</legend>
        <ul>
            <li>
                <div class="field_label">Nombre</div>
                <div class="field_input">
                    <input type="text" id="cargo15_name" name="cargo15_name" runat="server" />
                </div>
            </li>
            
            <li>
                <div class="field_label">Nro. de documento:</div>
                <div class="field_input"><input type="text" id="cargo15_identification_number" name="cargo15_identification_number" runat="server" /></div>
            </li>
           
            <li>
                <div class="field_label">Correo electr&oacute;nico:</div>
                <div class="field_input"><input type="text" id="cargo15_email" name="cargo15_email" runat="server" /></div>
            </li>

            <li>
                <div class="field_label">Tel&eacute;fono:</div>
                <div class="field_input"><input type="text" id="cargo15_phone" name="cargo15_phone" runat="server" /></div>
            </li>

            <li>
                <div class="field_label">M&oacute;vil:</div>
                <div class="field_input"><input type="text" id="cargo15_movil" name="cargo15_movil" runat="server" /></div>
            </li>

          

            <li>
	            <div class="field_group">Datos de ubicación</div>
            </li>
            <li>
	            <div class="field_label">Direcci&oacute;n:</div>
	            <div class="field_input"><input type="text" id="cargo15_address" name="cargo15_address" runat="server" /></div>
            </li>
            <li id="cargo15_localization_out_of_colombia_block">
	            <div class="field_label">Marque esta casilla si es fuera de Colombia:</div>
	            <div class="field_input"><input type="checkbox" name="cargo15_localization_out_of_colombia" id="cargo15_localization_out_of_colombia" runat="server"/></div>
            </li>
            <li id="cargo15_departamento_field">
	            <div class="field_label">Departamento:</div>
	            <div class="field_input">
		            <asp:DropDownList ID="cargo15_departamentoDDL" runat="server" name="cargo15_departamentoDDL"></asp:DropDownList>
	            </div>
            </li>
            <li id="cargo15_municipio_field">
	            <div class="field_label">Municipio:<input type="hidden" name="cargo15_selectedMunicipio" id="cargo15_selectedMunicipio" value="0"/></div>
	            <div class="field_input">
		            <asp:DropDownList ID="cargo15_municipioDDL" runat="server" name="cargo15_municipioDDL"></asp:DropDownList>
	            </div>
            </li>
            <li id="cargo15_pais_field">
	            <div class="field_label">País:</div>
	            <div class="field_input"><input type="text" name="cargo15_country" id="cargo15_country" runat="server"/></div>
            </li>
            <li id="cargo15_ciudad_field">
	            <div class="field_label">Ciudad:</div>
	            <div class="field_input"><input type="text" name="cargo15_state" id="cargo15_state" runat="server"/></div>
            </li>
          </ul>
            
         </fieldset>
         <fieldset>
            <legend>Datos Vestuarista</legend>
        <ul>
            <li>
                <div class="field_label">Nombre</div>
                <div class="field_input">
                    <input type="text" id="cargo16_name" name="cargo16_name" runat="server" />
                </div>
            </li>
            
            <li>
                <div class="field_label">Nro. de documento:</div>
                <div class="field_input"><input type="text" id="cargo16_identification_number" name="cargo16_identification_number" runat="server" /></div>
            </li>
           
            <li>
                <div class="field_label">Correo electr&oacute;nico:</div>
                <div class="field_input"><input type="text" id="cargo16_email" name="cargo16_email" runat="server" /></div>
            </li>

            <li>
                <div class="field_label">Tel&eacute;fono:</div>
                <div class="field_input"><input type="text" id="cargo16_phone" name="cargo16_phone" runat="server" /></div>
            </li>

            <li>
                <div class="field_label">M&oacute;vil:</div>
                <div class="field_input"><input type="text" id="cargo16_movil" name="cargo16_movil" runat="server" /></div>
            </li>

          

            <li>
	            <div class="field_group">Datos de ubicación</div>
            </li>
            <li>
	            <div class="field_label">Direcci&oacute;n:</div>
	            <div class="field_input"><input type="text" id="cargo16_address" name="cargo16_address" runat="server" /></div>
            </li>
            <li id="cargo16_localization_out_of_colombia_block">
	            <div class="field_label">Marque esta casilla si es fuera de Colombia:</div>
	            <div class="field_input"><input type="checkbox" name="cargo16_localization_out_of_colombia" id="cargo16_localization_out_of_colombia" runat="server"/></div>
            </li>
            <li id="cargo16_departamento_field">
	            <div class="field_label">Departamento:</div>
	            <div class="field_input">
		            <asp:DropDownList ID="cargo16_departamentoDDL" runat="server" name="cargo16_departamentoDDL"></asp:DropDownList>
	            </div>
            </li>
            <li id="cargo16_municipio_field">
	            <div class="field_label">Municipio:<input type="hidden" name="cargo16_selectedMunicipio" id="cargo16_selectedMunicipio" value="0"/></div>
	            <div class="field_input">
		            <asp:DropDownList ID="cargo16_municipioDDL" runat="server" name="cargo16_municipioDDL"></asp:DropDownList>
	            </div>
            </li>
            <li id="cargo16_pais_field">
	            <div class="field_label">País:</div>
	            <div class="field_input"><input type="text" name="cargo16_country" id="cargo16_country" runat="server"/></div>
            </li>
            <li id="cargo16_ciudad_field">
	            <div class="field_label">Ciudad:</div>
	            <div class="field_input"><input type="text" name="cargo16_state" id="cargo16_state" runat="server"/></div>
            </li>
          </ul>
            
         </fieldset>
         <fieldset>
            <legend>Datos Ambientador o Utilero </legend>
        <ul>
            <li>
                <div class="field_label">Nombre</div>
                <div class="field_input">
                    <input type="text" id="cargo17_name" name="cargo17_name" runat="server" />
                </div>
            </li>
            
            <li>
                <div class="field_label">Nro. de documento:</div>
                <div class="field_input"><input type="text" id="cargo17_identification_number" name="cargo17_identification_number" runat="server" /></div>
            </li>
           
            <li>
                <div class="field_label">Correo electr&oacute;nico:</div>
                <div class="field_input"><input type="text" id="cargo17_email" name="cargo17_email" runat="server" /></div>
            </li>

            <li>
                <div class="field_label">Tel&eacute;fono:</div>
                <div class="field_input"><input type="text" id="cargo17_phone" name="cargo17_phone" runat="server" /></div>
            </li>

            <li>
                <div class="field_label">M&oacute;vil:</div>
                <div class="field_input"><input type="text" id="cargo17_movil" name="cargo17_movil" runat="server" /></div>
            </li>

          

            <li>
	            <div class="field_group">Datos de ubicación</div>
            </li>
            <li>
	            <div class="field_label">Direcci&oacute;n:</div>
	            <div class="field_input"><input type="text" id="cargo17_address" name="cargo17_address" runat="server" /></div>
            </li>
            <li id="cargo17_localization_out_of_colombia_block">
	            <div class="field_label">Marque esta casilla si es fuera de Colombia:</div>
	            <div class="field_input"><input type="checkbox" name="cargo17_localization_out_of_colombia" id="cargo17_localization_out_of_colombia" runat="server"/></div>
            </li>
            <li id="cargo17_departamento_field">
	            <div class="field_label">Departamento:</div>
	            <div class="field_input">
		            <asp:DropDownList ID="cargo17_departamentoDDL" runat="server" name="cargo17_departamentoDDL"></asp:DropDownList>
	            </div>
            </li>
            <li id="cargo17_municipio_field">
	            <div class="field_label">Municipio:<input type="hidden" name="cargo17_selectedMunicipio" id="cargo17_selectedMunicipio" value="0"/></div>
	            <div class="field_input">
		            <asp:DropDownList ID="cargo17_municipioDDL" runat="server" name="cargo17_municipioDDL"></asp:DropDownList>
	            </div>
            </li>
            <li id="cargo17_pais_field">
	            <div class="field_label">País:</div>
	            <div class="field_input"><input type="text" name="cargo17_country" id="cargo17_country" runat="server"/></div>
            </li>
            <li id="cargo17_ciudad_field">
	            <div class="field_label">Ciudad:</div>
	            <div class="field_input"><input type="text" name="cargo17_state" id="cargo17_state" runat="server"/></div>
            </li>
          </ul>
            
         </fieldset>
         <fieldset>
            <legend>Datos Script (Continuista)</legend>
        <ul>
            <li>
                <div class="field_label">Nombre</div>
                <div class="field_input">
                    <input type="text" id="cargo18_name" name="cargo18_name" runat="server" />
                </div>
            </li>
            
            <li>
                <div class="field_label">Nro. de documento:</div>
                <div class="field_input"><input type="text" id="cargo18_identification_number" name="cargo18_identification_number" runat="server" /></div>
            </li>
           
            <li>
                <div class="field_label">Correo electr&oacute;nico:</div>
                <div class="field_input"><input type="text" id="cargo18_email" name="cargo18_email" runat="server" /></div>
            </li>

            <li>
                <div class="field_label">Tel&eacute;fono:</div>
                <div class="field_input"><input type="text" id="cargo18_phone" name="cargo18_phone" runat="server" /></div>
            </li>

            <li>
                <div class="field_label">M&oacute;vil:</div>
                <div class="field_input"><input type="text" id="cargo18_movil" name="cargo18_movil" runat="server" /></div>
            </li>

          

            <li>
	            <div class="field_group">Datos de ubicación</div>
            </li>
            <li>
	            <div class="field_label">Direcci&oacute;n:</div>
	            <div class="field_input"><input type="text" id="cargo18_address" name="cargo18_address" runat="server" /></div>
            </li>
            <li id="cargo18_localization_out_of_colombia_block">
	            <div class="field_label">Marque esta casilla si es fuera de Colombia:</div>
	            <div class="field_input"><input type="checkbox" name="cargo18_localization_out_of_colombia" id="cargo18_localization_out_of_colombia" runat="server"/></div>
            </li>
            <li id="cargo18_departamento_field">
	            <div class="field_label">Departamento:</div>
	            <div class="field_input">
		            <asp:DropDownList ID="cargo18_departamentoDDL" runat="server" name="cargo18_departamentoDDL"></asp:DropDownList>
	            </div>
            </li>
            <li id="cargo18_municipio_field">
	            <div class="field_label">Municipio:<input type="hidden" name="cargo18_selectedMunicipio" id="cargo18_selectedMunicipio" value="0"/></div>
	            <div class="field_input">
		            <asp:DropDownList ID="cargo18_municipioDDL" runat="server" name="cargo18_municipioDDL"></asp:DropDownList>
	            </div>
            </li>
            <li id="cargo18_pais_field">
	            <div class="field_label">País:</div>
	            <div class="field_input"><input type="text" name="cargo18_country" id="cargo18_country" runat="server"/></div>
            </li>
            <li id="cargo18_ciudad_field">
	            <div class="field_label">Ciudad:</div>
	            <div class="field_input"><input type="text" name="cargo18_state" id="cargo18_state" runat="server"/></div>
            </li>
          </ul>
            
         </fieldset>
         <fieldset>
            <legend>Datos Asistente de Dirección</legend>
        <ul>
            <li>
                <div class="field_label">Nombre</div>
                <div class="field_input">
                    <input type="text" id="cargo19_name" name="cargo19_name" runat="server" />
                </div>
            </li>
            
            <li>
                <div class="field_label">Nro. de documento:</div>
                <div class="field_input"><input type="text" id="cargo19_identification_number" name="cargo19_identification_number" runat="server" /></div>
            </li>
           
            <li>
                <div class="field_label">Correo electr&oacute;nico:</div>
                <div class="field_input"><input type="text" id="cargo19_email" name="cargo19_email" runat="server" /></div>
            </li>

            <li>
                <div class="field_label">Tel&eacute;fono:</div>
                <div class="field_input"><input type="text" id="cargo19_phone" name="cargo19_phone" runat="server" /></div>
            </li>

            <li>
                <div class="field_label">M&oacute;vil:</div>
                <div class="field_input"><input type="text" id="cargo19_movil" name="cargo19_movil" runat="server" /></div>
            </li>

          

            <li>
	            <div class="field_group">Datos de ubicación</div>
            </li>
            <li>
	            <div class="field_label">Direcci&oacute;n:</div>
	            <div class="field_input"><input type="text" id="cargo19_address" name="cargo19_address" runat="server" /></div>
            </li>
            <li id="cargo19_localization_out_of_colombia_block">
	            <div class="field_label">Marque esta casilla si es fuera de Colombia:</div>
	            <div class="field_input"><input type="checkbox" name="cargo19_localization_out_of_colombia" id="cargo19_localization_out_of_colombia" runat="server"/></div>
            </li>
            <li id="cargo19_departamento_field">
	            <div class="field_label">Departamento:</div>
	            <div class="field_input">
		            <asp:DropDownList ID="cargo19_departamentoDDL" runat="server" name="cargo19_departamentoDDL"></asp:DropDownList>
	            </div>
            </li>
            <li id="cargo19_municipio_field">
	            <div class="field_label">Municipio:<input type="hidden" name="cargo19_selectedMunicipio" id="cargo19_selectedMunicipio" value="0"/></div>
	            <div class="field_input">
		            <asp:DropDownList ID="cargo19_municipioDDL" runat="server" name="cargo19_municipioDDL"></asp:DropDownList>
	            </div>
            </li>
            <li id="cargo19_pais_field">
	            <div class="field_label">País:</div>
	            <div class="field_input"><input type="text" name="cargo19_country" id="cargo19_country" runat="server"/></div>
            </li>
            <li id="cargo19_ciudad_field">
	            <div class="field_label">Ciudad:</div>
	            <div class="field_input"><input type="text" name="cargo19_state" id="cargo19_state" runat="server"/></div>
            </li>
          </ul>
            
         </fieldset>
         <fieldset>
            <legend>Datos Director de Casting</legend>
        <ul>
            <li>
                <div class="field_label">Nombre</div>
                <div class="field_input">
                    <input type="text" id="cargo20_name" name="cargo20_name" runat="server" />
                </div>
            </li>
            
            <li>
                <div class="field_label">Nro. de documento:</div>
                <div class="field_input"><input type="text" id="cargo20_identification_number" name="cargo20_identification_number" runat="server" /></div>
            </li>
           
            <li>
                <div class="field_label">Correo electr&oacute;nico:</div>
                <div class="field_input"><input type="text" id="cargo20_email" name="cargo20_email" runat="server" /></div>
            </li>

            <li>
                <div class="field_label">Tel&eacute;fono:</div>
                <div class="field_input"><input type="text" id="cargo20_phone" name="cargo20_phone" runat="server" /></div>
            </li>

            <li>
                <div class="field_label">M&oacute;vil:</div>
                <div class="field_input"><input type="text" id="cargo20_movil" name="cargo20_movil" runat="server" /></div>
            </li>

          

            <li>
	            <div class="field_group">Datos de ubicación</div>
            </li>
            <li>
	            <div class="field_label">Direcci&oacute;n:</div>
	            <div class="field_input"><input type="text" id="cargo20_address" name="cargo20_address" runat="server" /></div>
            </li>
            <li id="cargo20_localization_out_of_colombia_block">
	            <div class="field_label">Marque esta casilla si es fuera de Colombia:</div>
	            <div class="field_input"><input type="checkbox" name="cargo20_localization_out_of_colombia" id="cargo20_localization_out_of_colombia" runat="server"/></div>
            </li>
            <li id="cargo20_departamento_field">
	            <div class="field_label">Departamento:</div>
	            <div class="field_input">
		            <asp:DropDownList ID="cargo20_departamentoDDL" runat="server" name="cargo20_departamentoDDL"></asp:DropDownList>
	            </div>
            </li>
            <li id="cargo20_municipio_field">
	            <div class="field_label">Municipio:<input type="hidden" name="cargo20_selectedMunicipio" id="cargo20_selectedMunicipio" value="0"/></div>
	            <div class="field_input">
		            <asp:DropDownList ID="cargo20_municipioDDL" runat="server" name="cargo20_municipioDDL"></asp:DropDownList>
	            </div>
            </li>
            <li id="cargo20_pais_field">
	            <div class="field_label">País:</div>
	            <div class="field_input"><input type="text" name="cargo20_country" id="cargo20_country" runat="server"/></div>
            </li>
            <li id="cargo20_ciudad_field">
	            <div class="field_label">Ciudad:</div>
	            <div class="field_input"><input type="text" name="cargo20_state" id="cargo20_state" runat="server"/></div>
            </li>
          </ul>
            
         </fieldset>
         <fieldset>
            <legend>Datos Efectos especiales en escena (SFX) </legend>
        <ul>
            <li>
                <div class="field_label">Nombre</div>
                <div class="field_input">
                    <input type="text" id="cargo21_name" name="cargo21_name" runat="server" />
                </div>
            </li>
            
            <li>
                <div class="field_label">Nro. de documento:</div>
                <div class="field_input"><input type="text" id="cargo21_identification_number" name="cargo21_identification_number" runat="server" /></div>
            </li>
           
            <li>
                <div class="field_label">Correo electr&oacute;nico:</div>
                <div class="field_input"><input type="text" id="cargo21_email" name="cargo21_email" runat="server" /></div>
            </li>

            <li>
                <div class="field_label">Tel&eacute;fono:</div>
                <div class="field_input"><input type="text" id="cargo21_phone" name="cargo21_phone" runat="server" /></div>
            </li>

            <li>
                <div class="field_label">M&oacute;vil:</div>
                <div class="field_input"><input type="text" id="cargo21_movil" name="cargo21_movil" runat="server" /></div>
            </li>

          

            <li>
	            <div class="field_group">Datos de ubicación</div>
            </li>
            <li>
	            <div class="field_label">Direcci&oacute;n:</div>
	            <div class="field_input"><input type="text" id="cargo21_address" name="cargo21_address" runat="server" /></div>
            </li>
            <li id="cargo21_localization_out_of_colombia_block">
	            <div class="field_label">Marque esta casilla si es fuera de Colombia:</div>
	            <div class="field_input"><input type="checkbox" name="cargo21_localization_out_of_colombia" id="cargo21_localization_out_of_colombia" runat="server"/></div>
            </li>
            <li id="cargo21_departamento_field">
	            <div class="field_label">Departamento:</div>
	            <div class="field_input">
		            <asp:DropDownList ID="cargo21_departamentoDDL" runat="server" name="cargo21_departamentoDDL"></asp:DropDownList>
	            </div>
            </li>
            <li id="cargo21_municipio_field">
	            <div class="field_label">Municipio:<input type="hidden" name="cargo21_selectedMunicipio" id="cargo21_selectedMunicipio" value="0"/></div>
	            <div class="field_input">
		            <asp:DropDownList ID="cargo21_municipioDDL" runat="server" name="cargo21_municipioDDL"></asp:DropDownList>
	            </div>
            </li>
            <li id="cargo21_pais_field">
	            <div class="field_label">País:</div>
	            <div class="field_input"><input type="text" name="cargo21_country" id="cargo21_country" runat="server"/></div>
            </li>
            <li id="cargo21_ciudad_field">
	            <div class="field_label">Ciudad:</div>
	            <div class="field_input"><input type="text" name="cargo21_state" id="cargo21_state" runat="server"/></div>
            </li>
          </ul>
            
         </fieldset>
         <fieldset>
            <legend>Datos Efectos visuales (VFX / CGI) </legend>
        <ul>
            <li>
                <div class="field_label">Nombre</div>
                <div class="field_input">
                    <input type="text" id="cargo22_name" name="cargo22_name" runat="server" />
                </div>
            </li>
            
            <li>
                <div class="field_label">Nro. de documento:</div>
                <div class="field_input"><input type="text" id="cargo22_identification_number" name="cargo22_identification_number" runat="server" /></div>
            </li>
           
            <li>
                <div class="field_label">Correo electr&oacute;nico:</div>
                <div class="field_input"><input type="text" id="cargo22_email" name="cargo22_email" runat="server" /></div>
            </li>

            <li>
                <div class="field_label">Tel&eacute;fono:</div>
                <div class="field_input"><input type="text" id="cargo22_phone" name="cargo22_phone" runat="server" /></div>
            </li>

            <li>
                <div class="field_label">M&oacute;vil:</div>
                <div class="field_input"><input type="text" id="cargo22_movil" name="cargo22_movil" runat="server" /></div>
            </li>

          

            <li>
	            <div class="field_group">Datos de ubicación</div>
            </li>
            <li>
	            <div class="field_label">Direcci&oacute;n:</div>
	            <div class="field_input"><input type="text" id="cargo22_address" name="cargo22_address" runat="server" /></div>
            </li>
            <li id="cargo22_localization_out_of_colombia_block">
	            <div class="field_label">Marque esta casilla si es fuera de Colombia:</div>
	            <div class="field_input"><input type="checkbox" name="cargo22_localization_out_of_colombia" id="cargo22_localization_out_of_colombia" runat="server"/></div>
            </li>
            <li id="cargo22_departamento_field">
	            <div class="field_label">Departamento:</div>
	            <div class="field_input">
		            <asp:DropDownList ID="cargo22_departamentoDDL" runat="server" name="cargo22_departamentoDDL"></asp:DropDownList>
	            </div>
            </li>
            <li id="cargo22_municipio_field">
	            <div class="field_label">Municipio:<input type="hidden" name="cargo22_selectedMunicipio" id="cargo22_selectedMunicipio" value="0"/></div>
	            <div class="field_input">
		            <asp:DropDownList ID="cargo22_municipioDDL" runat="server" name="cargo22_municipioDDL"></asp:DropDownList>
	            </div>
            </li>
            <li id="cargo22_pais_field">
	            <div class="field_label">País:</div>
	            <div class="field_input"><input type="text" name="cargo22_country" id="cargo22_country" runat="server"/></div>
            </li>
            <li id="cargo22_ciudad_field">
	            <div class="field_label">Ciudad:</div>
	            <div class="field_input"><input type="text" name="cargo22_state" id="cargo22_state" runat="server"/></div>
            </li>
          </ul>
            
         </fieldset>
         <fieldset>
            <legend>Datos Colorista</legend>
        <ul>
            <li>
                <div class="field_label">Nombre</div>
                <div class="field_input">
                    <input type="text" id="cargo23_name" name="cargo23_name" runat="server" />
                </div>
            </li>
            
            <li>
                <div class="field_label">Nro. de documento:</div>
                <div class="field_input"><input type="text" id="cargo23_identification_number" name="cargo23_identification_number" runat="server" /></div>
            </li>
           
            <li>
                <div class="field_label">Correo electr&oacute;nico:</div>
                <div class="field_input"><input type="text" id="cargo23_email" name="cargo23_email" runat="server" /></div>
            </li>

            <li>
                <div class="field_label">Tel&eacute;fono:</div>
                <div class="field_input"><input type="text" id="cargo23_phone" name="cargo23_phone" runat="server" /></div>
            </li>

            <li>
                <div class="field_label">M&oacute;vil:</div>
                <div class="field_input"><input type="text" id="cargo23_movil" name="cargo23_movil" runat="server" /></div>
            </li>

          

            <li>
	            <div class="field_group">Datos de ubicación</div>
            </li>
            <li>
	            <div class="field_label">Direcci&oacute;n:</div>
	            <div class="field_input"><input type="text" id="cargo23_address" name="cargo23_address" runat="server" /></div>
            </li>
            <li id="cargo23_localization_out_of_colombia_block">
	            <div class="field_label">Marque esta casilla si es fuera de Colombia:</div>
	            <div class="field_input"><input type="checkbox" name="cargo23_localization_out_of_colombia" id="cargo23_localization_out_of_colombia" runat="server"/></div>
            </li>
            <li id="cargo23_departamento_field">
	            <div class="field_label">Departamento:</div>
	            <div class="field_input">
		            <asp:DropDownList ID="cargo23_departamentoDDL" runat="server" name="cargo23_departamentoDDL"></asp:DropDownList>
	            </div>
            </li>
            <li id="cargo23_municipio_field">
	            <div class="field_label">Municipio:<input type="hidden" name="cargo23_selectedMunicipio" id="cargo23_selectedMunicipio" value="0"/></div>
	            <div class="field_input">
		            <asp:DropDownList ID="cargo23_municipioDDL" runat="server" name="cargo23_municipioDDL"></asp:DropDownList>
	            </div>
            </li>
            <li id="cargo23_pais_field">
	            <div class="field_label">País:</div>
	            <div class="field_input"><input type="text" name="cargo23_country" id="cargo23_country" runat="server"/></div>
            </li>
            <li id="cargo23_ciudad_field">
	            <div class="field_label">Ciudad:</div>
	            <div class="field_input"><input type="text" name="cargo23_state" id="cargo23_state" runat="server"/></div>
            </li>
          </ul>
            
         </fieldset>
         <fieldset>
            <legend>Datos Microfonista</legend>
        <ul>
            <li>
                <div class="field_label">Nombre</div>
                <div class="field_input">
                    <input type="text" id="cargo24_name" name="cargo24_name" runat="server" />
                </div>
            </li>
            
            <li>
                <div class="field_label">Nro. de documento:</div>
                <div class="field_input"><input type="text" id="cargo24_identification_number" name="cargo24_identification_number" runat="server" /></div>
            </li>
           
            <li>
                <div class="field_label">Correo electr&oacute;nico:</div>
                <div class="field_input"><input type="text" id="cargo24_email" name="cargo24_email" runat="server" /></div>
            </li>

            <li>
                <div class="field_label">Tel&eacute;fono:</div>
                <div class="field_input"><input type="text" id="cargo24_phone" name="cargo24_phone" runat="server" /></div>
            </li>

            <li>
                <div class="field_label">M&oacute;vil:</div>
                <div class="field_input"><input type="text" id="cargo24_movil" name="cargo24_movil" runat="server" /></div>
            </li>

          

            <li>
	            <div class="field_group">Datos de ubicación</div>
            </li>
            <li>
	            <div class="field_label">Direcci&oacute;n:</div>
	            <div class="field_input"><input type="text" id="cargo24_address" name="cargo24_address" runat="server" /></div>
            </li>
            <li id="cargo24_localization_out_of_colombia_block">
	            <div class="field_label">Marque esta casilla si es fuera de Colombia:</div>
	            <div class="field_input"><input type="checkbox" name="cargo24_localization_out_of_colombia" id="cargo24_localization_out_of_colombia" runat="server"/></div>
            </li>
            <li id="cargo24_departamento_field">
	            <div class="field_label">Departamento:</div>
	            <div class="field_input">
		            <asp:DropDownList ID="cargo24_departamentoDDL" runat="server" name="cargo24_departamentoDDL"></asp:DropDownList>
	            </div>
            </li>
            <li id="cargo24_municipio_field">
	            <div class="field_label">Municipio:<input type="hidden" name="cargo24_selectedMunicipio" id="cargo24_selectedMunicipio" value="0"/></div>
	            <div class="field_input">
		            <asp:DropDownList ID="cargo24_municipioDDL" runat="server" name="cargo24_municipioDDL"></asp:DropDownList>
	            </div>
            </li>
            <li id="cargo24_pais_field">
	            <div class="field_label">País:</div>
	            <div class="field_input"><input type="text" name="cargo24_country" id="cargo24_country" runat="server"/></div>
            </li>
            <li id="cargo24_ciudad_field">
	            <div class="field_label">Ciudad:</div>
	            <div class="field_input"><input type="text" name="cargo24_state" id="cargo24_state" runat="server"/></div>
            </li>
          </ul>
            
         </fieldset>
         <fieldset>
            <legend>Datos Grabador o Artista de Foley</legend>
        <ul>
            <li>
                <div class="field_label">Nombre</div>
                <div class="field_input">
                    <input type="text" id="cargo25_name" name="cargo25_name" runat="server" />
                </div>
            </li>
            
            <li>
                <div class="field_label">Nro. de documento:</div>
                <div class="field_input"><input type="text" id="cargo25_identification_number" name="cargo25_identification_number" runat="server" /></div>
            </li>
           
            <li>
                <div class="field_label">Correo electr&oacute;nico:</div>
                <div class="field_input"><input type="text" id="cargo25_email" name="cargo25_email" runat="server" /></div>
            </li>

            <li>
                <div class="field_label">Tel&eacute;fono:</div>
                <div class="field_input"><input type="text" id="cargo25_phone" name="cargo25_phone" runat="server" /></div>
            </li>

            <li>
                <div class="field_label">M&oacute;vil:</div>
                <div class="field_input"><input type="text" id="cargo25_movil" name="cargo25_movil" runat="server" /></div>
            </li>

          

            <li>
	            <div class="field_group">Datos de ubicación</div>
            </li>
            <li>
	            <div class="field_label">Direcci&oacute;n:</div>
	            <div class="field_input"><input type="text" id="cargo25_address" name="cargo25_address" runat="server" /></div>
            </li>
            <li id="cargo25_localization_out_of_colombia_block">
	            <div class="field_label">Marque esta casilla si es fuera de Colombia:</div>
	            <div class="field_input"><input type="checkbox" name="cargo25_localization_out_of_colombia" id="cargo25_localization_out_of_colombia" runat="server"/></div>
            </li>
            <li id="cargo25_departamento_field">
	            <div class="field_label">Departamento:</div>
	            <div class="field_input">
		            <asp:DropDownList ID="cargo25_departamentoDDL" runat="server" name="cargo25_departamentoDDL"></asp:DropDownList>
	            </div>
            </li>
            <li id="cargo25_municipio_field">
	            <div class="field_label">Municipio:<input type="hidden" name="cargo25_selectedMunicipio" id="cargo25_selectedMunicipio" value="0"/></div>
	            <div class="field_input">
		            <asp:DropDownList ID="cargo25_municipioDDL" runat="server" name="cargo25_municipioDDL"></asp:DropDownList>
	            </div>
            </li>
            <li id="cargo25_pais_field">
	            <div class="field_label">País:</div>
	            <div class="field_input"><input type="text" name="cargo25_country" id="cargo25_country" runat="server"/></div>
            </li>
            <li id="cargo25_ciudad_field">
	            <div class="field_label">Ciudad:</div>
	            <div class="field_input"><input type="text" name="cargo25_state" id="cargo25_state" runat="server"/></div>
            </li>
          </ul>
            
         </fieldset>
         <fieldset>
            <legend>Datos Editor de Diálogos o Efectos</legend>
        <ul>
            <li>
                <div class="field_label">Nombre</div>
                <div class="field_input">
                    <input type="text" id="cargo26_name" name="cargo26_name" runat="server" />
                </div>
            </li>
            
            <li>
                <div class="field_label">Nro. de documento:</div>
                <div class="field_input"><input type="text" id="cargo26_identification_number" name="cargo26_identification_number" runat="server" /></div>
            </li>
           
            <li>
                <div class="field_label">Correo electr&oacute;nico:</div>
                <div class="field_input"><input type="text" id="cargo26_email" name="cargo26_email" runat="server" /></div>
            </li>

            <li>
                <div class="field_label">Tel&eacute;fono:</div>
                <div class="field_input"><input type="text" id="cargo26_phone" name="cargo26_phone" runat="server" /></div>
            </li>

            <li>
                <div class="field_label">M&oacute;vil:</div>
                <div class="field_input"><input type="text" id="cargo26_movil" name="cargo26_movil" runat="server" /></div>
            </li>

          

            <li>
	            <div class="field_group">Datos de ubicación</div>
            </li>
            <li>
	            <div class="field_label">Direcci&oacute;n:</div>
	            <div class="field_input"><input type="text" id="cargo26_address" name="cargo26_address" runat="server" /></div>
            </li>
            <li id="cargo26_localization_out_of_colombia_block">
	            <div class="field_label">Marque esta casilla si es fuera de Colombia:</div>
	            <div class="field_input"><input type="checkbox" name="cargo26_localization_out_of_colombia" id="cargo26_localization_out_of_colombia" runat="server"/></div>
            </li>
            <li id="cargo26_departamento_field">
	            <div class="field_label">Departamento:</div>
	            <div class="field_input">
		            <asp:DropDownList ID="cargo26_departamentoDDL" runat="server" name="cargo26_departamentoDDL"></asp:DropDownList>
	            </div>
            </li>
            <li id="cargo26_municipio_field">
	            <div class="field_label">Municipio:<input type="hidden" name="cargo26_selectedMunicipio" id="cargo26_selectedMunicipio" value="0"/></div>
	            <div class="field_input">
		            <asp:DropDownList ID="cargo26_municipioDDL" runat="server" name="cargo26_municipioDDL"></asp:DropDownList>
	            </div>
            </li>
            <li id="cargo26_pais_field">
	            <div class="field_label">País:</div>
	            <div class="field_input"><input type="text" name="cargo26_country" id="cargo26_country" runat="server"/></div>
            </li>
            <li id="cargo26_ciudad_field">
	            <div class="field_label">Ciudad:</div>
	            <div class="field_input"><input type="text" name="cargo26_state" id="cargo26_state" runat="server"/></div>
            </li>
          </ul>
            
         </fieldset>

       <%-- fin--%>
         <fieldset>
            <legend>Datos Mezclador </legend>
        
        <ul>
            <li>
                <div class="field_label">Nombre</div>
                <div class="field_input">
                    <input type="text" id="mixer_name" name="mixer_name" runat="server" />
                </div>
            </li>
           
            <li>
                <div class="field_label">Nro. de documento:</div>
                <div class="field_input"><input type="text" id="mixer_identification_number" name="mixer_identification_number" runat="server" /></div>
            </li>
           
            <li>
                <div class="field_label">Correo electr&oacute;nico:</div>
                <div class="field_input"><input type="text" id="mixer_email" name="mixer_email" runat="server" /></div>
            </li>

            <li>
                <div class="field_label">Tel&eacute;fono:</div>
                <div class="field_input"><input type="text" id="mixer_phone" name="mixer_phone" runat="server" /></div>
            </li>

            <li>
                <div class="field_label">M&oacute;vil:</div>
                <div class="field_input"><input type="text" id="mixer_movil" name="mixer_movil" runat="server" /></div>
            </li>

           
            <li>
	            <div class="field_group">Datos de ubicación</div>
            </li>
            <li>
	            <div class="field_label">Direcci&oacute;n:</div>
	            <div class="field_input"><input type="text" id="mixer_address" name="mixer_address" runat="server" /></div>
            </li>
            <li id="mixer_localization_out_of_colombia_block">
	            <div class="field_label">Marque esta casilla si es fuera de Colombia:</div>
	            <div class="field_input"><input type="checkbox" name="mixer_localization_out_of_colombia" id="mixer_localization_out_of_colombia" runat="server"/></div>
            </li>
            <li id="mixer_departamento_field">
	            <div class="field_label">Departamento:</div>
	            <div class="field_input">
		            <asp:DropDownList ID="mixer_departamentoDDL" runat="server" name="mixer_departamentoDDL"></asp:DropDownList>
	            </div>
            </li>
            <li id="mixer_municipio_field">
	            <div class="field_label">Municipio:<input type="hidden" name="mixer_selectedMunicipio" id="mixer_selectedMunicipio" value="0"/></div>
	            <div class="field_input">
		            <asp:DropDownList ID="mixer_municipioDDL" runat="server" name="mixer_municipioDDL"></asp:DropDownList>
	            </div>
            </li>
            <li id="mixer_pais_field">
	            <div class="field_label">País:</div>
	            <div class="field_input"><input type="text" name="mixer_country" id="mixer_country" runat="server"/></div>
            </li>
            <li id="mixer_ciudad_field">
	            <div class="field_label">Ciudad:</div>
	            <div class="field_input"><input type="text" name="mixer_state" id="mixer_state" runat="server"/></div>
            </li>
            <li>
                <%
                if (showAdvancedForm)
                { %>
                    <div id="admin-form">
                        <h3>Formulario de gestión de la solicitud</h3>
                        <div id="admin-form-left">
                         <%if(project_state_id != 6 && project_state_id != 7 && project_state_id != 8) {%>
                            <ul>
                                <li><input type="radio" name="gestion_realizada" id="gestion_realizada_sin_revisar" value="none" runat="server" /><label for="gestion-realizada-sin-revisar">Sin revisar</label></li>
                                <li><input type="radio" name="gestion_realizada" id="gestion_realizada_solicitar_aclaraciones" value="solicitar-aclaraciones" runat="server" /><label for="gestion-realizada-solicitar-aclaraciones">Solicitar aclaraciones</label></li>
                                <li><input type="radio" name="gestion_realizada" id="gestion_realizada_informacion_correcta" value="informacion-correcta" runat="server" /><label for="gestion-realizada-informacion-correcta">Informaci&oacute;n correcta</label></li>
                            </ul>
                            <%}
                             if (project_state_id == 6 || project_state_id == 7 || project_state_id == 8)
                              { %>
                            <fieldset>
                                <ul>
                                    <li><input type="radio" name="estado_revision" id="estado_revision_sin_revisar" value="none" runat="server" /><label for="estado_revision_sin_revisar">Sin revisar</label></li>
                                    <li><input type="radio" name="estado_revision" id="estado_revision_revisado" value="revisado" runat="server" /><label for="estado_revision_revisado">No cumple</label></li>
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
                                <li><h3>Solicitud de aclaraciones</h3><textarea  style="width:620px;min-height:200px;"  name="solicitud_aclaraciones" id="solicitud_aclaraciones" rows="5" cols="40" runat="server"></textarea></li>
                                
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
            </li>
            <li>
            <%
            /* Si el estado del proyecto es "Aclaraciones solicitadas" y el estado de la sección es "rechazado" se presenta el formulario de registro de aclaraciones para el productor */
            if (project_state_id >= 5 && section_state_id == 10)
            { %>
                <div id="registro_aclaraciones_form">
                     <%  if (user_role <=1)  { %>
                    <ul>
                        <li>
                            <h3>Formulario de registro de aclaraciones</h3>
                                <div id="static_info">
                                    <h4>Solicitud de aclaraciones</h4>
                                    <div><asp:Literal ID="clarification_request" runat="server">No se han solicitado aclaraciones sobre esta pestaña</asp:Literal></div>
                                </div>
                        </li>
                    </ul>
                    <div id="input_info">
                        <ul>
                            <li>
                                <h4>Escriba sus aclaraciones a continuación</h4>
                                <textarea class="user-input"  style="width:620px;min-height:200px;"  name="producer_clarifications_field" id="producer_clarifications_field" rows="5" cols="80" runat="server"></textarea>
                            </li>
                        </ul>
                    </div>
                       <% } %>
                </div>
            <% 
            } %>
            </li>
            <li>
            <%if (!(project_state_id == 5 && section_state_id != 10))
              { %>
                <div class="field_input"><input type="submit" id="submit" class="boton"  name="submit_technic_personal_data" value="Guardar" onclick='$("#loading").show();DisableEnableForm(false,"activar");' /></div>
                <%} %>
            </li>
         </ul>
         </fieldset>
    </fieldset>
    </div>

        
<uc1:cargando ID="cargando1" runat="server" />
    </form>
</div>
</asp:Content>