<%@ Page Title="" Language="C#" MasterPageFile="~/SiteModal.Master" AutoEventWireup="true" CodeBehind="frmEditarPersonal.aspx.cs" Inherits="CineProducto.frmEditarPersonal" %>
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

  

     <table class="table table-striped table-bordered table-hover dataTable align-content-center" style="width:96%">                                              
        <tr>
            <td class="LabelCampo"><h3>Personal</h3> </td>                                                   
            </tr>        
    </table>
<div class="table table-striped table-bordered table-hover dataTable align-content-center" style="width:96%">                                              
  <br />
                            <div class="form-horizontal center" style="width:100%;padding:5px">                                
                                 <asp:Label runat="server" ID="lblIdProjectStaff" visible="false"></asp:Label>
                                 <asp:Label runat="server" ID="lblStaffOptionId" visible="false"></asp:Label>
                                <asp:Label runat="server" ID="lblProjectId" visible="false"></asp:Label>
                                
                                 <asp:ScriptManager ID="ScriptManager1" runat="server" />
                                <asp:Panel runat="server" ID="pnCargo" >
                                    
                
                           <asp:UpdatePanel runat="server" id="UpdatePanel1"   updatemode="Conditional" >
                               <Triggers>
                                      <asp:AsyncPostBackTrigger controlid="cmbCargo" eventname="selectedindexchanged" />              
                                    </Triggers>
                                    <ContentTemplate>
                                         
                                <div class ="row">
                                    <div class="form-group col-sm-6">
                                        <label for="cmbCargo" class="control-label">Cargo *:</label>
                                         <asp:DropDownList runat="server" AutoPostBack="true" AppendDataBoundItems="True" CssClass="form-control" ID="cmbCargo" DataSourceID="SqlDataSourcePosition" DataTextField="position_name" DataValueField="position_id"  >
                                             <asp:ListItem Text="Seleccione..." Value=""></asp:ListItem>
                                         </asp:DropDownList>                                        
                                        <asp:SqlDataSource ID="SqlDataSourcePosition" runat="server" ConnectionString="<%$ ConnectionStrings:cineConnectionString %>" SelectCommand="
                                             SELECT position_id, position_name, position_description 
                                   FROM position 
                                   WHERE position_father_id = @staffOptionId AND 
                                   position_id not in (	select
                                                        project_staff.project_staff_position_id
                                                        from project_staff  
                                                        WHERE project_staff.project_staff_project_id= @paramProjectId
                                                        and project_staff.project_staff_id != @IdProjectStaff
                                                      ) and 
                                   position_deleted='0' order by position_name ">
                                            <SelectParameters>
                                                <asp:ControlParameter ControlID="lblStaffOptionId" DefaultValue="0" Name="staffOptionId" PropertyName="Text" />
                                                <asp:ControlParameter ControlID="lblProjectId" DefaultValue="0" Name="paramProjectId" PropertyName="Text" />
                                                <asp:ControlParameter ControlID="lblIdProjectStaff" DefaultValue="0" Name="IdProjectStaff" PropertyName="Text" />
                                                
                                            </SelectParameters>
                                        </asp:SqlDataSource>
                                        
                                        <asp:RequiredFieldValidator SetFocusOnError="true"  ID="RequiredFieldValidator3" runat="server" ErrorMessage="Este campo es obligatorio" ControlToValidate="cmbCargo" ></asp:RequiredFieldValidator>
                                    </div>                                
                                    <div class="form-group col-sm-6">
                                       <label for="cmbEspecialidad" class="">Especialidad:</label>
                                         <asp:DropDownList runat="server" CssClass="form-control" ID="cmbEspecialidad" AppendDataBoundItems="false" DataSourceID="SqlDataSourceEspecialidad" DataTextField="nombre" DataValueField="id_especialidad_cargo">
                                                    <asp:ListItem Text="Seleccione..." Value=""></asp:ListItem>
                                         </asp:DropDownList> 

                                        <asp:SqlDataSource ID="SqlDataSourceEspecialidad" runat="server" ConnectionString="<%$ ConnectionStrings:cineConnectionString %>" SelectCommand="select * from especialidad_cargo where position_id = @position_padre_especialidad">
                                            <SelectParameters>
                                                <asp:ControlParameter ControlID="cmbCargo" Name="position_padre_especialidad" PropertyName="SelectedValue" />
                                            </SelectParameters>
                                        </asp:SqlDataSource>

                                    </div>  
                                </div>

                                        </ContentTemplate>
                        </asp:UpdatePanel>

                                    </asp:Panel>


                              
                                   <div class ="row">
                                       <div class="form-group col-sm-6">
                                        <label for="cmbTipoDocumentoRep" class="">Tipo de documento *:</label>
                                         <asp:DropDownList runat="server" CssClass="form-control" ID="cmbTipoDocumentoRep" AppendDataBoundItems="True" DataSourceID="SqlDataSourceTipoIdentificacion" DataTextField="identification_type_name" DataValueField="identification_type_id">
                                                    <asp:ListItem Text="Seleccione..." Value=""></asp:ListItem>
                                         </asp:DropDownList> 
                                        <asp:RequiredFieldValidator SetFocusOnError="true"  ID="RequiredFieldValidator7" runat="server" ErrorMessage="Este campo es obligatorio" ControlToValidate="cmbTipoDocumentoRep" ForeColor="Red"></asp:RequiredFieldValidator>
                                        <asp:SqlDataSource ID="SqlDataSourceTipoIdentificacion" runat="server" ConnectionString="<%$ ConnectionStrings:cineConnectionString %>" SelectCommand="SELECT [identification_type_id], [identification_type_name] FROM [identification_type] where [identification_type_id] != 2"></asp:SqlDataSource>
                                      </div>

                                        <div class="form-group col-sm-6">                                            
                                            <asp:Label runat="server" ID="lblCedulaCiudadania" Text="Número de cédula de ciudadanía *:"></asp:Label>
                                              <div class="">
                                                <asp:TextBox CssClass="form-control" runat="server" ID="txtCedulaCiudadania"></asp:TextBox>
                                                <asp:RequiredFieldValidator SetFocusOnError="true"  ID="RequiredFieldValidatorCC" runat="server" ErrorMessage="Este campo es obligatorio" ControlToValidate="txtCedulaCiudadania" ForeColor="Red"></asp:RequiredFieldValidator>
                                                <asp:RangeValidator ID="RangeValidator1" runat="server" ErrorMessage="Debe escribir un numero" ControlToValidate="txtCedulaCiudadania" MaximumValue="90000000000" MinimumValue="0" Type="Double"></asp:RangeValidator>  
                                            </div>
                                        </div>
                                        
                                    </div>
                                      
                                      <div class="panel-body">
                                        <div class ="row">
                                    <div class="form-group col-sm-6">
                                        <label for="txtPrimerNombre" class="">Primer Nombre *:</label>
                                         <asp:TextBox CssClass="form-control" runat="server" ID="txtPrimerNombre"></asp:TextBox>
                                        <asp:RequiredFieldValidator SetFocusOnError="true"  ID="RequiredFieldValidator4" runat="server" ErrorMessage="Este campo es obligatorio" ControlToValidate="txtPrimerNombre" ForeColor="Red"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-sm-6">
                                        <label for="txtSegundoNombre" class="control-label">Segundo Nombre:</label>
                                        <div class="">
                                            <asp:TextBox CssClass="form-control" runat="server" ID="txtSegundoNombre"></asp:TextBox>
                                        </div>
                                    </div>
                                    </div>
                                    <div class ="row">
                                        <div class="form-group col-sm-6">
                                            <label for="txtPrimerApellido" class="control-label">Primer Apellido *:</label>
                                             <div class="">
                                                <asp:TextBox CssClass="form-control" runat="server" ID="txtPrimerApellido"></asp:TextBox>
                                                 <asp:RequiredFieldValidator SetFocusOnError="true"  ID="RequiredFieldValidatorPA" runat="server" ErrorMessage="Este campo es obligatorio" ControlToValidate="txtPrimerApellido" ForeColor="Red"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="form-group col-sm-6">
                                            <label for="txtSegundoApellido" class="control-label">Segundo Apellido:</label>
                                            <div class="">
                                                <asp:TextBox CssClass="form-control" runat="server" ID="txtSegundoApellido"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    
                                    <div class ="row">
                                        <div class="form-group col-sm-6">
                                            <label for="txtFechaNacimiento" class="control-label"> Fecha de nacimiento :</label>
                                            <div class="">
                                                <dx:ASPxDateEdit ID="txtFechaNacimiento" runat="server" EditFormat="Date" Date="" Width="190" >                                                   
                                                    <CalendarProperties>
                                                        <FastNavProperties DisplayMode="Inline" />
                                                    </CalendarProperties>
                                                </dx:ASPxDateEdit>
                                                <asp:RequiredFieldValidator SetFocusOnError="true"  ID="RequiredFieldValidatorFn" runat="server" ErrorMessage="Este campo es obligatorio" ControlToValidate="txtFechaNacimiento" ForeColor="Red"></asp:RequiredFieldValidator>
                                                
                                            </div>
                                        </div>
                                        
                                    </div>

                                   
                                    <div class ="row">
                                        <div class="form-group col-sm-6">
                                            <label for="cmbGrupoPoblacional" class="control-label"> Grupo Poblacional *:</label>
                                            <div class="">
                                                 <asp:DropDownList ID="cmbGrupoPoblacional" runat="server" CssClass="form-control" name="cmbGrupoPoblacional" AppendDataBoundItems="True" DataSourceID="SqlDataSourceGrupoPoblacional" DataTextField="nombre" DataValueField="id_grupo_poblacional">
                                            <asp:ListItem Text="Seleccione..." Value=""></asp:ListItem>
                                        </asp:DropDownList> 
                                                <asp:RequiredFieldValidator SetFocusOnError="true"  ID="RequiredFieldValidator1" runat="server" ErrorMessage="Este campo es obligatorio" ControlToValidate="cmbGrupoPoblacional" ForeColor="Red"></asp:RequiredFieldValidator>

                                        <asp:SqlDataSource ID="SqlDataSourceGrupoPoblacional"  runat="server" ConnectionString="<%$ ConnectionStrings:cineConnectionString %>" SelectCommand="SELECT [id_grupo_poblacional], [nombre] FROM [grupo_poblacional]"></asp:SqlDataSource>
                            
                                            </div>
                                        </div>
                                        <div class="form-group col-sm-6">
                                            <label for="cmbGrupoEtnico" class="control-label"> Grupo étnico *:</label>
                                            <div class="">
                                                <asp:DropDownList ID="cmbEtnia" runat="server" CssClass="form-control" name="cmbEtnia" AppendDataBoundItems="True" DataSourceID="SqlDataSourceEtnia" DataTextField="nombre" DataValueField="id_etnia">
                                            <asp:ListItem Text="Seleccione..." Value=""></asp:ListItem>
                                        </asp:DropDownList>    
                                                <asp:RequiredFieldValidator SetFocusOnError="true"  ID="RequiredFieldValidator5" runat="server" ErrorMessage="Este campo es obligatorio" ControlToValidate="cmbEtnia" ForeColor="Red"></asp:RequiredFieldValidator>
                                        <asp:SqlDataSource ID="SqlDataSourceEtnia" runat="server" ConnectionString="<%$ ConnectionStrings:cineConnectionString %>" SelectCommand="SELECT [id_etnia], [nombre] FROM [etnia]"></asp:SqlDataSource>
                              
                                            </div>
                                        </div>
                                    </div>

                                          <div class ="row">
                                              <div class="form-group col-sm-6">
                                                <label for="cmbGenero" class="control-label">Género *:</label>
                                                <div class="">
                                                    <asp:DropDownList ID="cmbGenero" CssClass="form-control" runat="server" name="cmbGenero" AppendDataBoundItems="True" DataSourceID="SqlDataSourceGenero" DataTextField="nombre" DataValueField="id_genero">
                                                <asp:ListItem Text="Seleccione..." Value=""></asp:ListItem>
                                                </asp:DropDownList>    
                                                        <asp:RequiredFieldValidator SetFocusOnError="true"  ID="RequiredFieldValidatorGen" runat="server" ErrorMessage="Este campo es obligatorio" ControlToValidate="cmbGenero" ForeColor="Red"></asp:RequiredFieldValidator>
                                                <asp:SqlDataSource ID="SqlDataSourceGenero" runat="server" ConnectionString="<%$ ConnectionStrings:cineConnectionString %>" SelectCommand="SELECT [id_genero], [nombre] FROM [genero]"></asp:SqlDataSource>
                            
                                                </div>
                                            </div>
                                          </div>
                                  

                                      </div>
   
                                <h3 class="panel-title">Datos de Origen / Nacimiento</h3>
                                

                                
                           <asp:UpdatePanel runat="server" id="upDatosOrigenNal"   updatemode="Conditional" >
                               <Triggers>
              <asp:AsyncPostBackTrigger controlid="cmbDepartamento"
                    eventname="selectedindexchanged" />
            </Triggers>
            <ContentTemplate>
                
                             <div class ="row">                                    
                                    <div class="form-group col-sm-6">
                                        <label for="cmbDepartamento" class="control-label">Departamento *:</label>
                                        <div class="">
                                            <asp:DropDownList runat="server" CssClass="form-control" AutoPostBack="true" ID="cmbDepartamento" AppendDataBoundItems="true" DataSourceID="SqlDataSourceDeptos" DataTextField="localization_name" DataValueField="localization_id" OnSelectedIndexChanged="cmbDepartamento_SelectedIndexChanged" >
                                            <asp:ListItem Text="Seleccione..." Value="" />
                                            </asp:DropDownList>   
                                            <asp:RequiredFieldValidator SetFocusOnError="true"  ID="RequiredFieldValidator15" runat="server" ErrorMessage="Este campo es obligatorio" ControlToValidate="cmbDepartamento" ForeColor="Red"></asp:RequiredFieldValidator>
                                         <asp:SqlDataSource ID="SqlDataSourceDeptos" runat="server" ConnectionString="<%$ ConnectionStrings:cineConnectionString %>" SelectCommand="select localization_id, localization_name from localization where localization_father_id=0"></asp:SqlDataSource>

                                        </div>
                                    </div>
                                    <div class="form-group col-sm-6">
                                        <label for="cmbMunicipio" class="control-label">Municipio *:</label>
                                        <div class="">
                                            <asp:DropDownList runat="server" CssClass="form-control" ID="cmbMunicipio" AppendDataBoundItems="false" DataSourceID="SqlDataSourceCitys" DataTextField="localization_name" DataValueField="localization_id">
                                            <asp:ListItem Text="Seleccione..." Value="" />
                                             </asp:DropDownList>  
                                            <asp:RequiredFieldValidator SetFocusOnError="true"  ID="RequiredFieldValidator16" runat="server" ErrorMessage="Este campo es obligatorio" ControlToValidate="cmbMunicipio" ForeColor="Red"></asp:RequiredFieldValidator>
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

                                <h3 class="panel-title">Datos de Contacto</h3>
                                <div class ="row">
                                    <div class="form-group col-sm-6">
                                        <label for="txtTelefono" class="control-label">Teléfono *:</label>
                                         <asp:TextBox CssClass="form-control" runat="server" ID="txtTelefono"></asp:TextBox>
                                        <asp:RequiredFieldValidator SetFocusOnError="true"  ID="RequiredFieldValidator19" runat="server" ErrorMessage="Este campo es obligatorio" ControlToValidate="txtTelefono" ForeColor="Red"></asp:RequiredFieldValidator>
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
                                <div class ="row">
                                    <div class="form-group col-sm-6">
                                        <label for="txtEmail" class="control-label">Correo electrónico *:</label>
                                         <asp:TextBox CssClass="form-control" runat="server" ID="txtEmail"></asp:TextBox>
                                        <asp:RequiredFieldValidator SetFocusOnError="true"  ID="RequiredFieldValidator20" runat="server" ErrorMessage="Este campo es obligatorio" ControlToValidate="txtEmail" ForeColor="Red"></asp:RequiredFieldValidator>
                                        
                                     <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Debe escribir un email válido" ControlToValidate="txtEmail"   ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>  

                                    </div>
                                   
                                </div>   
                                
                                   
                                
                            </div>
<br />
     </div> 




         <asp:UpdatePanel runat="server" id="upAdjuntos"   updatemode="Conditional" >
                               <Triggers>
              
            </Triggers>
            <ContentTemplate>
        <div class="row">      

        <br />
        <div class="col-md-12 "><div class="col-md-1"></div>   
           <div class="col-md-11">

    <table class="table table-striped table-bordered table-hover dataTable align-content-center" style="width:100%">                                              
    <tr>
        <td class="LabelCampo"><h4> Documentos Adjuntos</h4>
        </td>                                                    
       </tr>
    </table>
               
    <dx:ASPxGridView ID="ASPxGridView1" CssClass=""  Settings-ShowGroupPanel="false" Width="100%" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDSAdjuntos" KeyFieldName="project_attachment_id">
      
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
                     <asp:HyperLink id="hyperlink1" Target="_blank"  NavigateUrl='<%# Eval("ruta") %>'     Text='<%# Eval("nombre_original") %>'    runat="server"/>                        
                 </DataItemTemplate>
             </dx:GridViewDataColumn>

            <dx:GridViewDataTextColumn FieldName="Aprobado" ReadOnly="True" VisibleIndex="5">
            </dx:GridViewDataTextColumn>
            
            <dx:GridViewDataTextColumn FieldName="project_attachment_attachment_id" Visible="false" VisibleIndex="6">
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataColumn Caption="#" Width="100px" VisibleIndex="7">
                    <DataItemTemplate>        
                     <dx:ASPxUploadControl ShowTextBox="false" AutoStartUpload="true"  ID="upload" runat="server" AmazonSettings-AccountName='<%# Eval("project_attachment_id") %>' Width="280px" 
                         ClientInstanceName="upload" FileUploadMode="OnPageLoad" OnFileUploadComplete="upload_FileUploadComplete" 
                         Visible='<%# Eval("project_attachment_approved").ToString() == "1" ? false : true %>'
                         >
                         <ClientSideEvents FileUploadComplete="onFileUploadComplete" />
                         <ValidationSettings AllowedFileExtensions=".pdf,.PDF" MaxFileSize="10000000" ErrorStyle-CssClass="validationMessage" MaxFileCountErrorText="Archivos maximo de 10 Mb" NotAllowedFileExtensionErrorText ="Solo se permiten archivos pdf"></ValidationSettings>
                         <BrowseButton Text="Seleccionar..." />                         
                     </dx:ASPxUploadControl> 
                        
                         <%if (project_state !=9 && project_state != 10 && (user_role == 4 || user_role == 2  || user_role == 5 )){ %>
                       <asp:LinkButton ID="checkAdjunto" Visible='<%# Eval("project_attachment_approved").ToString() == "1" ? false : true %>' OnClick="checkAdjunto_Click" CommandArgument='<%# Eval("project_attachment_id") %>'  runat="server" Text="Aprobar" AutoPostBack="true" UseSubmitBehavior="false" Width="20%">
                        </asp:LinkButton>

                        <asp:LinkButton ID="checkAdjuntoRechazar" Visible='<%# Eval("project_attachment_approved").ToString() == "0" ? false : true %>' OnClick="checkAdjuntoRechazar_Click" CommandArgument='<%# Eval("project_attachment_id") %>'  runat="server" Text="Rechazar" AutoPostBack="true" UseSubmitBehavior="false" Width="20%">
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
WHERE project_staff_id=@IdProjectStaff order by 1 desc">
        <SelectParameters>
            <asp:ControlParameter ControlID="lblIdProjectStaff" DefaultValue="0" Name="IdProjectStaff" PropertyName="Text" />
        </SelectParameters>
    </asp:SqlDataSource>
<br /><br />
 
</div>
</div>
        </div>

 </ContentTemplate>
</asp:UpdatePanel>
         
        <br />
        <div class ="row">
                                    <div class="form-group col-sm-3">
                                        
                                    </div>
                                    <div class="form-group col-sm-4">
                                        <asp:Button runat="server" Text="Guardar" CssClass="form-control alert-primary" ID="btnGuardar" OnClick="btnGuardar_Click"/>
                                    </div>
                                </div>   
        <br /><br />
    <br /><br /><br /><br />  

</asp:Content>
