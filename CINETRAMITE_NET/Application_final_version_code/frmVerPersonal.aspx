<%@ Page Title="" Language="C#" MasterPageFile="~/SiteModal.Master" AutoEventWireup="true" CodeBehind="frmVerPersonal.aspx.cs" Inherits="CineProducto.frmVerPersonal" %>


<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.13.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>


<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

    
    

    <br /><br />
    <div class="row">      

        <br />
        <div class="col-md-12 ">
                <div class="col-md-1"></div>   
                <div class="col-md-11">
                      
            
            <asp:Label runat="server" ID="lblIdProjectStaff" visible="false"></asp:Label>

    <dx:ASPxGridView ID="ASPxGridView3"  Settings-ShowGroupPanel="false" Width="100%" runat="server" DataSourceID="SqlDSViewProductor" AutoGenerateColumns="False">
       <Settings ShowTitlePanel="false" />
        <Settings ShowGroupPanel="false" />
        <Settings  ShowColumnHeaders="false" />
              
        <Columns>
           <dx:GridViewDataTextColumn FieldName="producer_id" Visible="false" VisibleIndex="0">
                </dx:GridViewDataTextColumn>
       </Columns>

        <Templates>
                                    <DataRow>

                                         <table class="table table-striped table-bordered table-hover dataTable align-content-center">                                              
                                                <tr>
                                                    <td class="LabelCampo"><h3> Datos Personal</h3>
                                                    </td>
                                                    
                                                    </tr>
                                                <tr>
                                         </table>

                                        <div style="width: 100%">
                                            
                                            <table class="table table-striped table-bordered table-hover dataTable align-content-center">                                              
                                               
                                               <tr>
                                                    <td class="LabelCampo">Tipo de cargo:&nbsp;
                                                    </td>
                                                    <td class="value">
                                                        <dx:ASPxLabel ID="ASPxLabel6" runat="server" Text='<%# Eval("TipoCargo") %>' />
                                                    </td>
                                                  </tr>
                                                 <tr>
                                                    <td class="LabelCampo">Cargo:&nbsp;
                                                    </td>
                                                    <td class="value">
                                                        <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text='<%# Eval("Cargo") %>' />
                                                    </td>
                                                  </tr>
                                                <tr>
                                                    <td class="LabelCampo">Tipo de documento de identidad:&nbsp;
                                                    </td>
                                                    <td class="value">
                                                        <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text='<%# Eval("tipo_identificacion") %>' />
                                                    </td>
                                                  </tr>

                                                
                                                  <tr>
                                                    <td class="LabelCampo">Nombre:&nbsp;
                                                    </td>
                                                    <td class="value">
                                                        <dx:ASPxLabel ID="ASPxLabel14" runat="server" Text='<%# Eval("Nombre") %>' />
                                                    </td>
                                                  </tr>
                                                
                                                  <tr>
                                                    <td class="LabelCampo">Identificación:&nbsp;
                                                    </td>
                                                    <td class="value">
                                                        <dx:ASPxLabel ID="ASPxLabel16" runat="server" Text='<%# Eval("Identificacion") %>' />
                                                    </td>
                                                  </tr>
                                                <tr>
                                                    <td class="LabelCampo">Fecha de Nacimiento:&nbsp;
                                                    </td>
                                                    <td class="value">
                                                        <dx:ASPxLabel ID="ASPxLabel11" runat="server" Text='<%# Eval("fecha_nacimiento") %>' />
                                                    </td>
                                                  </tr>

                                                
                                                                                                   

                                                
                                                <tr>
                                                    <td class="LabelCampo">Departamento Origen:&nbsp;
                                                    </td>
                                                    <td class="value">
                                                        <dx:ASPxLabel ID="ASPxLabel13" runat="server" Text='<%# Eval("Deartamento") %>' />
                                                    </td>
                                                </tr>
                                                   

                                                <tr>
                                                    <td class="LabelCampo">Ciudad de Origen:&nbsp;
                                                    </td>
                                                    <td class="value">
                                                        <dx:ASPxLabel ID="ASPxLabel7" runat="server" Text='<%# Eval("Ciudad") %>' />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="LabelCampo">Telefono:&nbsp;
                                                    </td>
                                                    <td class="value">
                                                        <dx:ASPxLabel ID="ASPxLabel8" runat="server" Text='<%# Eval("Telefono") %>' />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="LabelCampo">Telefono alternativo:&nbsp;
                                                    </td>
                                                    <td class="value">
                                                        <dx:ASPxLabel ID="ASPxLabel9" runat="server" Text='<%# Eval("Celular") %>' />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="LabelCampo">Correo electrónico:&nbsp;
                                                    </td>
                                                    <td class="value">
                                                        <dx:ASPxLabel ID="ASPxLabel10" runat="server" Text='<%# Eval("Email") %>' />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="LabelCampo">Genero:&nbsp;
                                                    </td>
                                                    <td class="value">
                                                        <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text='<%# Eval("Genero") %>' />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="LabelCampo">Grupo étnico:&nbsp;
                                                    </td>
                                                    <td class="value">
                                                        <dx:ASPxLabel ID="ASPxLabel4" runat="server" Text='<%# Eval("Etnia") %>' />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="LabelCampo">Grupo poblacional:&nbsp;
                                                    </td>
                                                    <td class="value">
                                                        <dx:ASPxLabel ID="ASPxLabel5" runat="server" Text='<%# Eval("grupo_poblacional") %>' />
                                                    </td>
                                                </tr>
                                             
                                               
                                               
                                            </table>
                                        </div>
                                    </DataRow>
                                </Templates>
    </dx:ASPxGridView>

    
   
    <asp:SqlDataSource ID="SqlDSViewProductor" runat="server" ConnectionString="<%$ ConnectionStrings:cineConnectionString %>" SelectCommand="
set dateformat dmy
select
project_staff_id,
position.position_name 'Cargo',
p2.position_name 'TipoCargo',
project_staff_firstname + ' ' + isnull(project_staff_firstname2,'')+' '+project_staff_lastname+' '+project_staff_lastname2  as 'Nombre',
identification_type.identification_type_name as tipo_identificacion,
[project_staff_identification_number] 'Identificacion',
l2.localization_name as 'Deartamento' ,
localization.localization_name as 'Ciudad' ,
fecha_nacimiento,
project_staff_address 'Direccion', 
project_staff_phone 'Telefono', 
project_staff_movil 'Celular', 
project_staff_email 'Email', 
project_staff.project_staff_project_id,
genero.nombre as genero,
etnia.nombre as etnia,
grupo_poblacional.nombre as grupo_poblacional
from project_staff  
join position on position.position_id =  project_staff.project_staff_position_id
left join position p2 on position.position_father_id =  p2.position_id
left join genero on genero.id_genero = project_staff.id_genero
left join etnia on etnia.id_etnia = project_staff.id_etnia
left join grupo_poblacional on grupo_poblacional.id_grupo_poblacional = project_staff.id_grupo_poblacional
left join identification_type on identification_type.identification_type_id = project_staff.identification_type_id
left join localization on localization.localization_id = project_staff_localization_id
left join localization l2 on l2.localization_id=localization.localization_father_id
WHERE  project_staff_id=@project_staff_id">
        <SelectParameters>
            <asp:ControlParameter ControlID="lblIdProjectStaff" DefaultValue="0" Name="project_staff_id" PropertyName="Text" />
        </SelectParameters>
    </asp:SqlDataSource>

        </div>


                             </div>
                    </div>




    <br /><br />
    <div class="row">      

        <br />
        <div class="col-md-12 ">
                <div class="col-md-1"></div>   
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
            <dx:GridViewDataTextColumn FieldName="Descripcion" Width="300px" VisibleIndex="1">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="Ruta"  Visible="false" VisibleIndex="2">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataDateColumn FieldName="Fecha" Width="100px" VisibleIndex="3">
            </dx:GridViewDataDateColumn>

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
WHERE project_staff_id=@project_staff_id">
        <SelectParameters>
            
            <asp:ControlParameter ControlID="lblIdProjectStaff" DefaultValue="0" Name="project_staff_id" PropertyName="Text" />
        </SelectParameters>
    </asp:SqlDataSource>



                    <br /><br /><br /><br /><br /><br />
 
</div>
</div>
        </div>
</asp:Content>

