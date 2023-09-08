<%@ Page Title="" Language="C#" MasterPageFile="~/SiteModal.Master" AutoEventWireup="true" CodeBehind="frmEditarCoproductor.aspx.cs" Inherits="CineProducto.frmEditarCoproductor" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.13.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>


<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">         
        function onFileUploadComplete(s, e) {

            //alert('El archivo se cargo correctamente!');
            if (e.isValid)
                __doPostBack('<%= upAdjuntos.ClientID %>', '');
        }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row">
        <div class="form-group col-sm-1">
        </div>
        <div class="form-group col-sm-11">



            <table class="table table-striped table-bordered table-hover dataTable align-content-center" style="width: 96%">
                <tr>
                    <td class="LabelCampo">
                        <h3>Coproductor</h3>
                    </td>
                </tr>
            </table>
            <div class="table table-striped table-bordered table-hover dataTable align-content-center" style="width: 96%">
                <br />
                <div class="form-horizontal center" style="width: 100%; padding: 5px">
                    <asp:Label runat="server" ID="lblViewProductorSeleccionado" Visible="false"></asp:Label>
                    <asp:Label runat="server" ID="lblIdProducer" Visible="false"></asp:Label>
                    <div class="row">
                        <div class="form-group col-sm-6">
                            <label for="cmbTipoProductor" class="control-label">Tipo de productor *:</label>
                            <asp:DropDownList runat="server" AutoPostBack="true" AppendDataBoundItems="true" CssClass="form-control" ID="cmbTipoProductor" DataSourceID="SqlDataSourceTipoPersona" DataTextField="person_type_name" DataValueField="person_type_id" onfocus="this.setAttribute('PrvSelectedValue',this.value);" onchange="if(confirm('Se eliminaran los adjuntos. Por favor confirme?')==false){ this.value=this.getAttribute('PrvSelectedValue');return false; }" OnSelectedIndexChanged="cmbTipoProductor_SelectedIndexChanged">
                                <asp:ListItem Text="Seleccione..." Value=""></asp:ListItem>
                            </asp:DropDownList>
                            <asp:SqlDataSource ID="SqlDataSourceTipoPersona" runat="server" ConnectionString="<%$ ConnectionStrings:cineConnectionString %>" SelectCommand="SELECT [person_type_id], [person_type_name] FROM [person_type]"></asp:SqlDataSource>
                            <asp:RequiredFieldValidator SetFocusOnError="true" ID="RequiredFieldValidator3" runat="server" ErrorMessage="Este campo es obligatorio" ControlToValidate="cmbTipoProductor"></asp:RequiredFieldValidator>
                        </div>
                        <div class="form-group col-sm-6">
                            <label for="txtPorcentajeParticipacion" class="control-label">
                                Porcentaje de Participacion *:                                            
                            </label>
                            &nbsp;<div class="">
                                <asp:TextBox CssClass="form-control" runat="server" ID="txtPorcentajeParticipacion"></asp:TextBox>
                                <asp:RequiredFieldValidator SetFocusOnError="true" ID="RequiredFieldValidator2" runat="server" ErrorMessage="Este campo es obligatorio" ControlToValidate="txtPorcentajeParticipacion" ForeColor="Red"></asp:RequiredFieldValidator>
                                <asp:RangeValidator ID="RangeValidator2" runat="server" SetFocusOnError="true" ErrorMessage="Debe escribir un numero entre 1 y 100" ControlToValidate="txtPorcentajeParticipacion" MaximumValue="100" MinimumValue="0.01" Type="Double"></asp:RangeValidator>

                            </div>
                        </div>
                    </div>


                    <asp:Panel runat="server" ID="pnPersonaNatural" Visible="false">



                        <h3 class="panel-title">Datos Persona Natural</h3>

                        <div class="panel-body">
                            <div class="row">
                                <div class="form-group col-sm-6">
                                    <label for="txtPrimerNombre" class="">Primer Nombre *:</label>
                                    <asp:TextBox CssClass="form-control" runat="server" ID="txtPrimerNombre"></asp:TextBox>
                                    <asp:RequiredFieldValidator SetFocusOnError="true" ID="RequiredFieldValidator4" runat="server" ErrorMessage="Este campo es obligatorio" ControlToValidate="txtPrimerNombre" ForeColor="Red"></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group col-sm-6">
                                    <label for="txtSegundoNombre" class="control-label">Segundo Nombre:</label>
                                    <div class="">
                                        <asp:TextBox CssClass="form-control" runat="server" ID="txtSegundoNombre"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="form-group col-sm-6">
                                    <label for="txtPrimerApellido" class="control-label">Primer Apellido *:</label>
                                    <div class="">
                                        <asp:TextBox CssClass="form-control" runat="server" ID="txtPrimerApellido"></asp:TextBox>
                                        <asp:RequiredFieldValidator SetFocusOnError="true" ID="RequiredFieldValidatorPA" runat="server" ErrorMessage="Este campo es obligatorio" ControlToValidate="txtPrimerApellido" ForeColor="Red"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="form-group col-sm-6">
                                    <label for="txtSegundoApellido" class="control-label">Segundo Apellido:</label>
                                    <div class="">
                                        <asp:TextBox CssClass="form-control" runat="server" ID="txtSegundoApellido"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="form-group col-sm-6">
                                    <asp:Label runat="server" ID="lblCedulaCiudadania" Text="Número de cédula de ciudadanía *:"></asp:Label>
                                    <div class="">
                                        <asp:TextBox CssClass="form-control" runat="server" ID="txtCedulaCiudadania" ToolTip="Este número corresponde a la identificación de persona natural extranjera, verifique que este coincida con el relacionado en el contrato de cooproducción"></asp:TextBox>
                                        <asp:RequiredFieldValidator SetFocusOnError="true" ID="RequiredFieldValidatorCC" runat="server" ErrorMessage="Este campo es obligatorio" ControlToValidate="txtCedulaCiudadania" ForeColor="Red"></asp:RequiredFieldValidator>
                                        <asp:RangeValidator ID="rangeCedulaCiudadania" SetFocusOnError="true" runat="server" ErrorMessage="Debe escribir un numero" ControlToValidate="txtCedulaCiudadania" MaximumValue="90000000000" MinimumValue="1" Type="Double"></asp:RangeValidator>
                                        <cc1:FilteredTextBoxExtender runat="server" ID="filterDocumento" FilterMode="ValidChars"
                                            ValidChars="0123456789" TargetControlID="txtCedulaCiudadania">
                                        </cc1:FilteredTextBoxExtender>
                                    </div>
                                </div>
                                <div class="form-group col-sm-6">
                                    <label for="cmbGenero" class="control-label">Género *:</label>
                                    <div class="">
                                        <asp:DropDownList ID="cmbGenero" CssClass="form-control" runat="server" name="cmbGenero" AppendDataBoundItems="True" DataSourceID="SqlDataSourceGenero" DataTextField="nombre" DataValueField="id_genero">
                                            <asp:ListItem Text="Seleccione..." Value=""></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator SetFocusOnError="true" ID="RequiredFieldValidatorGen" runat="server" ErrorMessage="Este campo es obligatorio" ControlToValidate="cmbGenero" ForeColor="Red"></asp:RequiredFieldValidator>
                                        <asp:SqlDataSource ID="SqlDataSourceGenero" runat="server" ConnectionString="<%$ ConnectionStrings:cineConnectionString %>" SelectCommand="SELECT [id_genero], [nombre] FROM [genero]"></asp:SqlDataSource>

                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="form-group col-sm-6">
                                    <label for="txtFechaNacimiento" class="control-label">Fecha de nacimiento :</label>
                                    <div class="">
                                        <dx:ASPxDateEdit ID="txtFechaNacimiento" runat="server" EditFormat="Date" Date="" Width="190">
                                            <CalendarProperties>
                                                <FastNavProperties DisplayMode="Inline" />
                                            </CalendarProperties>
                                        </dx:ASPxDateEdit>
                                        <asp:RequiredFieldValidator SetFocusOnError="true" ID="RequiredFieldValidatorFn" runat="server" ErrorMessage="Este campo es obligatorio" ControlToValidate="txtFechaNacimiento" ForeColor="Red"></asp:RequiredFieldValidator>
                                    </div>
                                </div>

                            </div>

                            <asp:Panel runat="server" ID="pnPNaturalNal">
                                <div class="row">
                                    <div class="form-group col-sm-6">
                                        <label for="cmbGrupoPoblacional" class="control-label">Grupo Poblacional *:</label>
                                        <div class="">
                                            <asp:DropDownList ID="cmbGrupoPoblacional" runat="server" CssClass="form-control" name="cmbGrupoPoblacional" AppendDataBoundItems="True" DataSourceID="SqlDataSourceGrupoPoblacional" DataTextField="nombre" DataValueField="id_grupo_poblacional">
                                                <asp:ListItem Text="Seleccione..." Value=""></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator SetFocusOnError="true" ID="RequiredFieldValidator1" runat="server" ErrorMessage="Este campo es obligatorio" ControlToValidate="cmbGrupoPoblacional" ForeColor="Red"></asp:RequiredFieldValidator>

                                            <asp:SqlDataSource ID="SqlDataSourceGrupoPoblacional" runat="server" ConnectionString="<%$ ConnectionStrings:cineConnectionString %>" SelectCommand="SELECT [id_grupo_poblacional], [nombre] FROM [grupo_poblacional]"></asp:SqlDataSource>

                                        </div>
                                    </div>
                                    <div class="form-group col-sm-6">
                                        <label for="cmbGrupoEtnico" class="control-label">Grupo étnico *:</label>
                                        <div class="">
                                            <asp:DropDownList ID="cmbEtnia" runat="server" CssClass="form-control" name="cmbEtnia" AppendDataBoundItems="True" DataSourceID="SqlDataSourceEtnia" DataTextField="nombre" DataValueField="id_etnia">
                                                <asp:ListItem Text="Seleccione..." Value=""></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator SetFocusOnError="true" ID="RequiredFieldValidator5" runat="server" ErrorMessage="Este campo es obligatorio" ControlToValidate="cmbEtnia" ForeColor="Red"></asp:RequiredFieldValidator>
                                            <asp:SqlDataSource ID="SqlDataSourceEtnia" runat="server" ConnectionString="<%$ ConnectionStrings:cineConnectionString %>" SelectCommand="SELECT [id_etnia], [nombre] FROM [etnia]"></asp:SqlDataSource>

                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>

                        </div>


                    </asp:Panel>

                    <asp:Panel runat="server" ID="pnPersonaJuridica" Visible="false">



                        <h3 class="panel-title">Datos Persona Jurídica</h3>

                        <div class="panel-body">
                            <div class="row">
                                <div class="form-group col-sm-6">
                                    <label for="txtRazonSocial" class="">Nombre o Razón Social *:</label>
                                    <asp:TextBox CssClass="form-control" runat="server" ID="txtRazonSocial"></asp:TextBox>
                                    <asp:RequiredFieldValidator SetFocusOnError="true" ID="RequiredFieldValidatorRS" runat="server" ErrorMessage="Este campo es obligatorio" ControlToValidate="txtRazonSocial" ForeColor="Red"></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group col-sm-6">
                                    <label for="txtAbreviatura" class="control-label">Abreviatura:</label>
                                    <div class="">
                                        <asp:TextBox CssClass="form-control" runat="server" ID="txtAbreviatura"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="form-group col-sm-6">
                                    <asp:Label runat="server" ID="lblNit" Text="NIT *:"></asp:Label>
                                    <div class="row">
                                        <div class="col-sm-8">
                                            <asp:TextBox CssClass="form-control" runat="server" ID="txtNIT" ToolTip="Este número corresponde a la identificación de persona juridica extranjera, verifique que este coincida con el relacionado en el contrato de cooproducción"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-1">
                                            <asp:Label runat="server" ID="lblSeparadorNIt" Text="-"></asp:Label>
                                        </div>
                                        <div class="col-sm-2">
                                            <asp:TextBox CssClass="form-control" runat="server" ID="txtNitDigVerificacion"></asp:TextBox>
                                        </div>
                                        <asp:RequiredFieldValidator SetFocusOnError="true" ID="RequiredFieldValidator6" runat="server" ErrorMessage="Este campo es obligatorio" ControlToValidate="txtNIT" ForeColor="Red"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator SetFocusOnError="true" ID="RequiredFieldValidatorNitDigVerif" runat="server" ErrorMessage=" - Este campo es obligatorio" ControlToValidate="txtNitDigVerificacion" ForeColor="Red"></asp:RequiredFieldValidator>
                                        <asp:RangeValidator ID="RangeValidator3" SetFocusOnError="true" runat="server" ErrorMessage="Debe escribir un numero" ControlToValidate="txtNIT" MaximumValue="90000000000" MinimumValue="0" Type="Double"></asp:RangeValidator>
                                        <asp:RangeValidator ID="RangeValidator8" SetFocusOnError="true" runat="server" ErrorMessage="Debe escribir un numero" ControlToValidate="txtNitDigVerificacion" MaximumValue="9" MinimumValue="0" Type="Double"></asp:RangeValidator>
                                    </div>
                                </div>
                                <div class="form-group col-sm-6">
                                    <label for="cmbTipoEmpresa" class="control-label">Tipo de Empresa *:</label>
                                    <div class="">
                                        <asp:DropDownList runat="server" CssClass="form-control" ID="cmbTipoEmpresa" AutoPostBack="true" AppendDataBoundItems="true" OnSelectedIndexChanged="cmbTipoEmpresa_SelectedIndexChanged" onfocus="this.setAttribute('PrvSelectedValue',this.value);" onchange="if(confirm('Se eliminaran los adjuntos. Por favor confirme?')==false){ this.value=this.getAttribute('PrvSelectedValue');return false; }">
                                            <asp:ListItem Text="Seleccione..." Value=""></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator SetFocusOnError="true" ID="RequiredFieldValidatorTe" runat="server" ErrorMessage="Este campo es obligatorio" ControlToValidate="cmbTipoEmpresa" ForeColor="Red"></asp:RequiredFieldValidator>

                                    </div>
                                </div>
                            </div>

                            <h4 class="panel-title">Datos Representante Legal</h4>

                            <div class="row">
                                <div class="form-group col-sm-6">
                                    <label for="cmbTipoDocumentoRep" class="">Tipo de documento Rep Legal *:</label>
                                    <asp:DropDownList runat="server" CssClass="form-control" ID="cmbTipoDocumentoRep" AppendDataBoundItems="True" DataSourceID="SqlDataSourceTipoIdentificacion" DataTextField="identification_type_name" DataValueField="identification_type_id" onchange="validarTipoDocumentoRepLegal()">
                                        <asp:ListItem Text="Seleccione..." Value=""></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator SetFocusOnError="true" ID="RequiredFieldValidator7" runat="server" ErrorMessage="Este campo es obligatorio" ControlToValidate="cmbTipoDocumentoRep" ForeColor="Red"></asp:RequiredFieldValidator>
                                    <asp:SqlDataSource ID="SqlDataSourceTipoIdentificacion" runat="server" ConnectionString="<%$ ConnectionStrings:cineConnectionString %>" SelectCommand="SELECT [identification_type_id], [identification_type_name] FROM [identification_type]"></asp:SqlDataSource>
                                </div>
                                <div class="form-group col-sm-6">
                                    <asp:Label runat="server" ID="lblNumDocumentoRepLegal" Text="Número de documento Rep Legal*:"></asp:Label>
                                    <div class="">
                                        <asp:TextBox CssClass="form-control" runat="server" ID="txtNumDocumentoRepLegal"></asp:TextBox>
                                        <asp:RequiredFieldValidator SetFocusOnError="true" ID="RequiredFieldValidator8" runat="server" ErrorMessage="Este campo es obligatorio" ControlToValidate="txtNumDocumentoRepLegal" ForeColor="Red"></asp:RequiredFieldValidator>
                                        <asp:RangeValidator SetFocusOnError="true" ID="RangeValidatorNumDocRepLegal" runat="server" ErrorMessage="Debe escribir un numero" ControlToValidate="txtNumDocumentoRepLegal" MaximumValue="90000000000" MinimumValue="0" Type="Double" ValidationExpression="^\d+$|^[a-zA-Z0-9]+$"></asp:RangeValidator>
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="form-group col-sm-6">
                                    <label for="txtPrimerNombreRep" class="">Primer Nombre Rep Legal*:</label>
                                    <asp:TextBox CssClass="form-control" runat="server" ID="txtPrimerNombreRep"></asp:TextBox>
                                    <asp:RequiredFieldValidator SetFocusOnError="true" ID="RequiredFieldValidator9" runat="server" ErrorMessage="Este campo es obligatorio" ControlToValidate="txtPrimerNombreRep" ForeColor="Red"></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group col-sm-6">
                                    <label for="txtSegundoNombreRep" class="control-label">Segundo Nombre  Rep Legal:</label>
                                    <div class="">
                                        <asp:TextBox CssClass="form-control" runat="server" ID="txtSegundoNombreRep"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="form-group col-sm-6">
                                    <label for="txtPrimerApellidoRep" class="control-label">Primer Apellido  Rep Legal*:</label>
                                    <div class="">
                                        <asp:TextBox CssClass="form-control" runat="server" ID="txtPrimerApellidoRep"></asp:TextBox>
                                        <asp:RequiredFieldValidator SetFocusOnError="true" ID="RequiredFieldValidator10" runat="server" ErrorMessage="Este campo es obligatorio" ControlToValidate="txtPrimerApellidoRep" ForeColor="Red"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="form-group col-sm-6">
                                    <label for="txtSegundoApellidoRep" class="control-label">Segundo Apellido  Rep Legal:</label>
                                    <div class="">
                                        <asp:TextBox CssClass="form-control" runat="server" ID="txtSegundoApellidoRep"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <asp:Label ID="lblErrorCoproductor" runat="server" Visible="false" ForeColor="Red"></asp:Label>
                            <h4 class="panel-title">Datos Representante Legal Suplente</h4>
                            <div class="row">
                                <div class="form-group col-sm-6">
                                    <label for="cmbTipoDocumentoRepLegalSup" class="">Tipo de documento Rep Legal Suplente:</label>
                                    <asp:DropDownList runat="server" CssClass="form-control" ID="cmbTipoDocumentoRepLegalSup" AppendDataBoundItems="True" DataSourceID="SqlDataSourceTipoIdentificacion" DataTextField="identification_type_name" DataValueField="identification_type_id">
                                        <asp:ListItem Text="Seleccione..." Value=""></asp:ListItem>
                                    </asp:DropDownList>

                                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:cineConnectionString %>" SelectCommand="SELECT [identification_type_id], [identification_type_name] FROM [identification_type]"></asp:SqlDataSource>
                                </div>
                                <div class="form-group col-sm-6">
                                    <asp:Label runat="server" ID="lblNumDocumentoRepLegalSup" Text=">Número de documento Rep Legal Suplente:"></asp:Label>
                                    <div class="">
                                        <asp:TextBox CssClass="form-control" runat="server" ID="txtNumDocumentoRepLegalSup"></asp:TextBox>

                                        <asp:RangeValidator ID="RangeValidator5" runat="server" ErrorMessage="Debe escribir un numero" ControlToValidate="txtNumDocumentoRepLegalSup" MaximumValue="90000000000" MinimumValue="0" Type="Double"></asp:RangeValidator>
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="form-group col-sm-6">
                                    <label for="txtPrimerNombreRepSup" class="">Primer Nombre Rep Legal Suplente:</label>
                                    <asp:TextBox CssClass="form-control" runat="server" ID="txtPrimerNombreRepSup"></asp:TextBox>

                                </div>
                                <div class="form-group col-sm-6">
                                    <label for="txtSegundoNombreRepSup" class="control-label">Segundo Nombre  Rep Legal Suplente:</label>
                                    <div class="">
                                        <asp:TextBox CssClass="form-control" runat="server" ID="txtSegundoNombreRepSup"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="form-group col-sm-6">
                                    <label for="txtPrimerApellidoRepSup" class="control-label">Primer Apellido  Rep Legal Suplente:</label>
                                    <div class="">
                                        <asp:TextBox CssClass="form-control" runat="server" ID="txtPrimerApellidoRepSup"></asp:TextBox>

                                    </div>
                                </div>
                                <div class="form-group col-sm-6">
                                    <label for="txtSegundoApellidoRepSup" class="control-label">Segundo Apellido  Rep Legal Suplente:</label>
                                    <div class="">
                                        <asp:TextBox CssClass="form-control" runat="server" ID="txtSegundoApellidoRepSup"></asp:TextBox>
                                    </div>
                                </div>
                            </div>


                        </div>


                    </asp:Panel>

                    <asp:Panel ID="pnlDepMunNacimiento" runat="server">
                    <h3 class="panel-title">Datos de Origen / Nacimiento</h3>


                    <asp:ScriptManager ID="ScriptManager1" runat="server" />
                    <asp:UpdatePanel runat="server" ID="upDatosOrigenNal" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="cmbDepartamento"
                                EventName="selectedindexchanged" />
                        </Triggers>
                        <ContentTemplate>

                            <div class="row">
                                
                                    <div class="form-group col-sm-6">
                                    <label for="cmbDepartamento" class="control-label">Departamento *:</label>
                                    <div class="">
                                        <asp:DropDownList runat="server" CssClass="form-control" AutoPostBack="true" ID="cmbDepartamento" AppendDataBoundItems="true" DataSourceID="SqlDataSourceDeptos" DataTextField="localization_name" DataValueField="localization_id" OnSelectedIndexChanged="cmbDepartamento_SelectedIndexChanged">
                                            <asp:ListItem Text="Seleccione..." Value="" />
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator SetFocusOnError="true" ID="RequiredFieldValidator15" runat="server" ErrorMessage="Este campo es obligatorio" ControlToValidate="cmbDepartamento" ForeColor="Red"></asp:RequiredFieldValidator>
                                        <asp:SqlDataSource ID="SqlDataSourceDeptos" runat="server" ConnectionString="<%$ ConnectionStrings:cineConnectionString %>" SelectCommand="select localization_id, localization_name from localization where localization_father_id=0  order by localization_name"></asp:SqlDataSource>

                                    </div>
                                </div>
                                    <div class="form-group col-sm-6">
                                    <label for="cmbMunicipio" class="control-label">Municipio *:</label>
                                    <div class="">
                                        <asp:DropDownList runat="server" CssClass="form-control" ID="cmbMunicipio" AppendDataBoundItems="false" DataSourceID="SqlDataSourceCitys" DataTextField="localization_name" DataValueField="localization_id">
                                            <asp:ListItem Text="Seleccione..." Value="" />
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator SetFocusOnError="true" ID="RequiredFieldValidator16" runat="server" ErrorMessage="Este campo es obligatorio" ControlToValidate="cmbMunicipio" ForeColor="Red"></asp:RequiredFieldValidator>
                                        <asp:SqlDataSource ID="SqlDataSourceCitys" runat="server" ConnectionString="<%$ ConnectionStrings:cineConnectionString %>" SelectCommand="select localization_id, localization_name from localization where localization_father_id=@localizacion_id">
                                            <SelectParameters>
                                                <asp:ControlParameter ControlID="cmbDepartamento" Name="localizacion_id" PropertyName="SelectedValue" />
                                            </SelectParameters>
                                        </asp:SqlDataSource>
                                    </div>
                                </div>
                                
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                        </asp:Panel>
                    <asp:Panel runat="server" ID="pnDatosOrigenExt">
                        <h3 class="panel-title">Datos de Origen / Nacimiento</h3>
                        <div class="row">
                            <div class="form-group col-sm-6">

                                <label for="cmbPais" class="control-label">Pais *:</label>
                                <div class="">

                                    <asp:DropDownList runat="server" CssClass="form-control" AutoPostBack="false" ID="cmbPais" AppendDataBoundItems="true" DataSourceID="SqlDataSourcePais" DataTextField="localization_name" DataValueField="localization_name">
                                        <asp:ListItem Text="Seleccione..." Value="" />
                                    </asp:DropDownList>

                                    <asp:SqlDataSource ID="SqlDataSourcePais" runat="server" ConnectionString="<%$ ConnectionStrings:cineConnectionString %>" SelectCommand="select localization_id, localization_name from localization where localization_father_id=-2 order by localization_name"></asp:SqlDataSource>

                                </div>
                            </div>
                            <div class="form-group col-sm-6">
                                <label for="txtCiudad" class="control-label">Ciudad *:</label>
                                <div class="">
                                    <asp:TextBox CssClass="form-control" runat="server" ID="txtCiudad"></asp:TextBox>
                                    <asp:RequiredFieldValidator SetFocusOnError="true" ID="RequiredFieldValidator18" runat="server" ErrorMessage="Este campo es obligatorio" ControlToValidate="txtCiudad" ForeColor="Red"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>


                    <h3 class="panel-title">Datos de Contacto</h3>
                    <div class="row">
                        <div class="form-group col-sm-6">
                            <label for="txtTelefono" class="control-label">Teléfono *:</label>
                            <asp:TextBox CssClass="form-control" runat="server" ID="txtTelefono"></asp:TextBox>
                            <asp:RequiredFieldValidator SetFocusOnError="true" ID="RequiredFieldValidator19" runat="server" ErrorMessage="Este campo es obligatorio" ControlToValidate="txtTelefono" ForeColor="Red"></asp:RequiredFieldValidator>
                            <asp:RangeValidator ID="RangeValidator7" runat="server" ErrorMessage="Debe escribir un numero valido" ControlToValidate="txtTelefono" MaximumValue="9000000000000" MinimumValue="0" Type="Double"></asp:RangeValidator>

                        </div>
                        <div class="form-group col-sm-6">
                            <label for="txtTelefonoAlternativo" class="control-label">Teléfono Alternativo:</label>
                            <div class="">
                                <asp:TextBox CssClass="form-control" runat="server" ID="txtTelefonoAlternativo"></asp:TextBox>
                                <asp:RangeValidator ID="RangeValidator6" runat="server" ErrorMessage="Debe escribir un numero valido" ControlToValidate="txtTelefonoAlternativo" MaximumValue="90000000000000" MinimumValue="0" Type="Double"></asp:RangeValidator>

                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="form-group col-sm-6">
                            <label for="txtEmail" class="control-label">Correo electrónico *:</label>
                            <asp:TextBox CssClass="form-control" runat="server" ID="txtEmail"></asp:TextBox>


                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" SetFocusOnError="true" ErrorMessage="Debe escribir un email válido" ControlToValidate="txtEmail" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>

                        </div>
                        <div class="form-group col-sm-6">
                            <label for="txtSitioWeb" class="control-label">Sitio web :</label>
                            <div class="">
                                <asp:TextBox CssClass="form-control" runat="server" ID="txtSitioWeb"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <asp:Panel runat="server" ID="pnlCiudadContacto" Visible="false">
                        <div class="row">
                            <div class="form-group col-sm-6">
                                <label for="cmbDepartamentoContacto" class="control-label">Departamento contacto*:</label>
                                <div class="">
                                    <asp:DropDownList runat="server" CssClass="form-control" AutoPostBack="true" ID="cmbDepartamentoContacto" AppendDataBoundItems="true" DataSourceID="SqlDataSourceDeptosContacto" DataTextField="localization_name" DataValueField="localization_id" OnSelectedIndexChanged="cmbDepartamento_SelectedIndexChanged">
                                        <asp:ListItem Text="Seleccione..." Value="" />
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator SetFocusOnError="true" ID="RequiredFieldValidator11" runat="server" ErrorMessage="Este campo es obligatorio" ControlToValidate="cmbDepartamentoContacto" ForeColor="Red"></asp:RequiredFieldValidator>
                                    <asp:SqlDataSource ID="SqlDataSourceDeptosContacto" runat="server" ConnectionString="<%$ ConnectionStrings:cineConnectionString %>" SelectCommand="select localization_id, localization_name from localization where localization_father_id=0  order by localization_name"></asp:SqlDataSource>

                                </div>
                            </div>
                            <div class="form-group col-sm-6">
                                <label for="cmbMunicipioContacto" class="control-label">Municipio contacto*:</label>
                                <div class="">
                                    <asp:DropDownList runat="server" CssClass="form-control" ID="cmbMunicipioContacto" AppendDataBoundItems="false" DataSourceID="SqlDataSourceCitysContacto" DataTextField="localization_name" DataValueField="localization_id">
                                        <asp:ListItem Text="Seleccione..." Value="" />
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator SetFocusOnError="true" ID="RequiredFieldValidator12" runat="server" ErrorMessage="Este campo es obligatorio" ControlToValidate="cmbMunicipioContacto" ForeColor="Red"></asp:RequiredFieldValidator>
                                    <asp:SqlDataSource ID="SqlDataSourceCitysContacto" runat="server" ConnectionString="<%$ ConnectionStrings:cineConnectionString %>" SelectCommand="select localization_id, localization_name from localization where localization_father_id=@localizacion_id">
                                        <SelectParameters>
                                            <asp:ControlParameter ControlID="cmbDepartamentoContacto" Name="localizacion_id" PropertyName="SelectedValue" />
                                        </SelectParameters>
                                    </asp:SqlDataSource>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>

                    <asp:Panel ID="pnlPaisContacto" runat="server">
                        <div class="row">
                            <div class="form-group col-sm-6">

                                <label for="cmbPaisContacto" class="control-label">Pais contacto*:</label>
                                <div class="">

                                    <asp:DropDownList runat="server" CssClass="form-control" AutoPostBack="false" ID="cmbPaisContacto" AppendDataBoundItems="true" DataSourceID="SqlDataSourcePaisContacto" DataTextField="localization_name" DataValueField="localization_name">
                                        <asp:ListItem Text="Seleccione..." Value="" />
                                    </asp:DropDownList>

                                    <asp:SqlDataSource ID="SqlDataSourcePaisContacto" runat="server" ConnectionString="<%$ ConnectionStrings:cineConnectionString %>" SelectCommand="select localization_id, localization_name from localization where localization_father_id=-2 order by localization_name"></asp:SqlDataSource>

                                </div>
                            </div>
                            <div class="form-group col-sm-6">
                                <label for="txtCiudadContacto" class="control-label">Ciudad contacto*:</label>
                                <div class="">
                                    <asp:TextBox CssClass="form-control" runat="server" ID="txtCiudadContacto"></asp:TextBox>
                                    <asp:RequiredFieldValidator SetFocusOnError="true" ID="RequiredFieldValidator13" runat="server" ErrorMessage="Este campo es obligatorio" ControlToValidate="txtCiudadContacto" ForeColor="Red"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                </div>
                <br />
            </div>

            <asp:UpdatePanel runat="server" ID="upAdjuntos" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="cmbTipoEmpresa" EventName="selectedindexchanged" />

                </Triggers>
                <ContentTemplate>
                    <div class="row">

                        <br />
                        <div class="col-md-12 ">
                            <div class="col-md-1"></div>
                            <div class="col-md-11">

                                <table class="table table-striped table-bordered table-hover dataTable align-content-center" style="width: 100%">
                                    <tr>
                                        <td class="LabelCampo">
                                            <h4>Documentos Adjuntos</h4>
                                        </td>
                                    </tr>
                                </table>

                                <dx:ASPxGridView ID="ASPxGridView1" CssClass="" Settings-ShowGroupPanel="false" Width="100%" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDSAdjuntos" KeyFieldName="project_attachment_id">

                                    <Columns>
                                        <dx:GridViewDataTextColumn FieldName="project_attachment_id" Visible="false" ReadOnly="True" VisibleIndex="0">
                                            <EditFormSettings Visible="False" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="Descripcion" Width="200px" VisibleIndex="1">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="Ruta" Visible="false" VisibleIndex="2">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataColumn Caption="Documento" Width="200px" VisibleIndex="4">
                                            <DataItemTemplate>
                                                <asp:HyperLink ID="hyperlink1" Target="_blank" NavigateUrl='<%# Eval("ruta") %>' Text='<%# Eval("nombre_original") %>' runat="server" />
                                            </DataItemTemplate>
                                        </dx:GridViewDataColumn>

                                        <dx:GridViewDataTextColumn FieldName="Aprobado" ReadOnly="True" VisibleIndex="5">
                                        </dx:GridViewDataTextColumn>

                                        <dx:GridViewDataTextColumn FieldName="project_attachment_attachment_id" Visible="false" VisibleIndex="6">
                                        </dx:GridViewDataTextColumn>

                                        <dx:GridViewDataColumn Caption="#" Width="100px" VisibleIndex="7">
                                            <DataItemTemplate>
                                                <dx:ASPxUploadControl ShowTextBox="false" AutoStartUpload="true" ID="upload" runat="server" AmazonSettings-AccountName='<%# Eval("project_attachment_id") %>' Width="280px"
                                                    Visible='<%# Eval("project_attachment_approved").ToString() == "1" ? false : true %>'
                                                    ClientInstanceName="upload" FileUploadMode="OnPageLoad" OnFileUploadComplete="upload_FileUploadComplete">
                                                    <ClientSideEvents FileUploadComplete="onFileUploadComplete" />
                                                    <ValidationSettings AllowedFileExtensions=".pdf,.PDF" MaxFileSize="10000000" ErrorStyle-CssClass="validationMessage" MaxFileCountErrorText="Archivos maximo de 10 Mb" NotAllowedFileExtensionErrorText="Solo se permiten archivos pdf"></ValidationSettings>
                                                    <BrowseButton Text="Seleccionar..." />
                                                </dx:ASPxUploadControl>

                                                <%if (project_state != 9 && project_state != 10 && (user_role == 4 || user_role == 2 || user_role == 5))
                                                    { %>
                                                <asp:LinkButton ID="checkAdjunto" Visible='<%# Eval("project_attachment_approved").ToString() == "1" ? false : true %>' OnClick="checkAdjunto_Click" CommandArgument='<%# Eval("project_attachment_id") %>' runat="server" Text="Aprobar" AutoPostBack="true" UseSubmitBehavior="false" Width="20%">
                                                </asp:LinkButton>

                                                <asp:LinkButton ID="checkAdjuntoRechazar" Visible='<%# Eval("project_attachment_approved").ToString() == "0" ? false : true %>' OnClick="checkAdjuntoRechazar_Click" CommandArgument='<%# Eval("project_attachment_id") %>' runat="server" Text="Rechazar" AutoPostBack="true" UseSubmitBehavior="false" Width="20%">
                                                </asp:LinkButton>
                                                <% } %>
                                            </DataItemTemplate>
                                        </dx:GridViewDataColumn>
                                    </Columns>
                                </dx:ASPxGridView>




                                <asp:SqlDataSource ID="SqlDSAdjuntos" runat="server" ConnectionString="<%$ ConnectionStrings:cineConnectionString %>" SelectCommand="
SELECT 
project_attachment_id, 
attachment.attachment_name as nombre,
        attachment.attachment_description as Descripcion,
project_attachment_path as Ruta,
project_attachment_date as Fecha,
case when project_attachment_approved = 1 then 'Si' else 'No' end as Aprobado,
nombre_original,
project_attachment_attachment_id,
    project_attachment_approved
FROM project_attachment 
left join attachment on attachment.attachment_id=project_attachment.project_attachment_attachment_id
WHERE project_attachment_producer_id=@IdProductorAdjuntos">
                                    <SelectParameters>
                                        <asp:ControlParameter ControlID="lblIdProducer" DefaultValue="0" Name="IdProductorAdjuntos" PropertyName="Text" />
                                    </SelectParameters>
                                </asp:SqlDataSource>
                                <br />
                                <br />

                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>




            <br />
            <div class="row">
                <div class="form-group col-sm-3">
                </div>
                <div class="form-group col-sm-4">
                    <asp:Button runat="server" Text="Guardar" CssClass="form-control alert-primary" ID="btnGuardar" OnClick="btnGuardar_Click" />
                </div>
            </div>
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
</asp:Content>
