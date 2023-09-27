<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ListaHojaTransferencia.aspx.cs" Inherits="CineProducto.ListaHojaTransferencia" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="usercontrols/cargando.ascx" TagName="cargando" TagPrefix="uc1" %>

<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.13.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<asp:Content ID="TitleContent" runat="server" ContentPlaceHolderID="TitleContent">Trámite Reconocimiento Como Obra Nacional - Listado de Solicitudes - Mincultura</asp:Content>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <script src="blueimp/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="blueimp/jquery.iframe-transport.js" type="text/javascript"></script>
    <script src="blueimp/jquery.fileupload.js" type="text/javascript"></script>
    <script src="Scripts/js/footable.min.js"></script>
    <link href="Styles/css/footable.standalone.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">

    <script type="text/javaScript">

        $(document).ready(function () {
            $('#loading').hide();
            //---

        });
    </script>

    <asp:ScriptManager runat="server" ID="scManager" EnableScriptGlobalization="true" EnableScriptLocalization="true"></asp:ScriptManager>


    <asp:Label runat="server" ID="lblSort" Visible="false" Text="project_request_date"></asp:Label>
    <asp:Label runat="server" ID="lblSortDirection" Visible="false" Text="desc"></asp:Label>


    <div class="row">

        <div class="col-12">
            <asp:Label runat="server" ID="lbltitulo" Font-Size="15px" Font-Bold="true" Text="Listado de Solicitudes"></asp:Label>
        </div>
        <div class="col-12">
            <asp:Panel runat="server" DefaultButton="btnFiltrar">
                <table>
                    <tr>
                        <td>Título Obra<asp:CheckBox runat="server" ID="chkOprimioBoton" Checked="false" Visible="false" /></td>
                        <td>
                            <asp:TextBox ID="txtTitulo" placeholder="Ingrese el título de la obra" runat="server" Width="250px"></asp:TextBox></td>
                        <td>&nbsp;</td>

                    </tr>
                    <tr>
                        <td>Productor</td>
                        <td>
                            <asp:TextBox ID="txtProductor" runat="server" placeholder="Ingrese el nombre del productor" Width="250px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>Solicitudes Enviadas Desde</td>
                        <td>
                            <asp:TextBox ID="txtInicio" runat="server" Width="220px" OnTextChanged="txtInicio_TextChanged"></asp:TextBox>
                            <cc1:CalendarExtender ID="txtInicio_CalendarExtender" runat="server" BehaviorID="txtInicio_CalendarExtender" TargetControlID="txtInicio">
                            </cc1:CalendarExtender>
                            <asp:Button ID="btnLimpiarDesde" runat="server" OnClick="btnLimpiarDesde_Click" Text="&lt;" />
                        </td>
                        <td>Solicitudes Enviadas Hasta</td>
                        <td>
                            <asp:TextBox ID="txtFin" runat="server" OnTextChanged="txtFin_TextChanged" Width="220px"></asp:TextBox>
                            <cc1:CalendarExtender ID="txtFin_CalendarExtender" runat="server" BehaviorID="txtFin_CalendarExtender" TargetControlID="txtFin" />
                            <asp:Button ID="btnLimpiarHasta" runat="server" OnClick="btnLimpiarHasta_Click" Text="&lt;" />
                        </td>
                    </tr>

                    <tr>
                        <td colspan="2">
                            <asp:Label ID="lblError" runat="server" Text="" ForeColor="Red"></asp:Label>
                        </td>
                        <td>&nbsp;</td>
                        <td>&nbsp;
               <asp:Button ID="btnFiltrar" runat="server" CssClass="boton" Font-Size="12pt" OnClick="btnFiltrar_Click" OnClientClick="$('#loading').show();" Text="Filtrar" /></td>
                    </tr>
                </table>
            </asp:Panel>

        </div>

        <div class="col-12">
            <asp:Button ID="btnExportarExcel" runat="server" CssClass="boton" Font-Size="12pt" OnClick="btnExportarExcel_Click" Text="Exportar Excel" />

            <dx:ASPxGridView ID="grdDevDatos" runat="server" AutoGenerateColumns="False" EnableTheming="True" KeyFieldName="project_id" Theme="DevEx">

                <SettingsPager ShowEmptyDataRows="True" PageSize="50"></SettingsPager>

                <Settings ShowFilterRow="True" />
                <SettingsDataSecurity AllowDelete="False" AllowEdit="False" AllowInsert="False" />
                <SettingsText EmptyDataRow="No hay registros que cumplan con las condiciones de la búsqueda o que requieran su atención." />

                <Columns>
                    <dx:GridViewDataColumn Settings-AutoFilterCondition="Contains" Caption="Cod" FieldName="project_id" VisibleIndex="1">
                        <DataItemTemplate>
                            <%# Eval("project_id") %>
                        </DataItemTemplate>
                    </dx:GridViewDataColumn>

                    <dx:GridViewDataTextColumn Settings-AutoFilterCondition="Contains" FieldName="Productor" ReadOnly="True" VisibleIndex="3" Caption="Productor">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataColumn Settings-AutoFilterCondition="Contains" Caption="Título obra" FieldName="Obra" VisibleIndex="4">
                        <DataItemTemplate>
                            <asp:HyperLink runat="server" ID="lnkProyecto" Text='<%# Eval("Obra") %>'
                                NavigateUrl='<%# "DatosProyecto.aspx?project_id="+Eval("project_id") %>'></asp:HyperLink>
                        </DataItemTemplate>
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn Settings-AutoFilterCondition="Contains" Caption="Hoja De Tansferencia" FieldName="HojaTransferencia" VisibleIndex="5">
                        <DataItemTemplate>
                            <asp:Label runat="server" ID="HojaTransferencia" Text='<%# Eval("HOJA_TRANSFERENCIA") %>'></asp:Label>
                        </DataItemTemplate>
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataDateColumn Settings-AutoFilterCondition="Contains" PropertiesDateEdit-DisplayFormatString="dd/MM/yyyy hh:mm:ss" FieldName="Fecha_y_Hora_de_Solicitud" Caption="Fecha solicitud" ReadOnly="True" VisibleIndex="6">
                    </dx:GridViewDataDateColumn>
                    <dx:GridViewDataTextColumn Settings-AutoFilterCondition="Contains" FieldName="hora_envio" Visible="false" Caption="Hora solicitud" VisibleIndex="7">
                    </dx:GridViewDataTextColumn>

                </Columns>

            </dx:ASPxGridView>



        </div>

    </div>

    <br />


    <br />

    <uc1:cargando ID="cargando1" runat="server" />
</asp:Content>
