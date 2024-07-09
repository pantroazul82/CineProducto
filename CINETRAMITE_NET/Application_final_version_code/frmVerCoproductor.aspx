<%@ Page Title="" Language="C#" MasterPageFile="~/SiteModal.Master" AutoEventWireup="true" CodeBehind="frmVerCoproductor.aspx.cs" Inherits="CineProducto.frmVerCoproductor" %>

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
                      
            <asp:Label runat="server" ID="lblViewProductorSeleccionado" visible="false"></asp:Label>
            <asp:Label runat="server" ID="lblIdProducer" visible="false"></asp:Label>

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
                                                    <td class="LabelCampo"><h3> Datos Coproductor</h3>
                                                    </td>
                                                    
                                                    </tr>
                                                <tr>
                                         </table>

                                        <div style="width: 100%">
                                            
                                            <table class="table table-striped table-bordered table-hover dataTable align-content-center">                                              
                                                <tr>
                                                    <td class="LabelCampo">Tipo Persona:&nbsp;
                                                    </td>
                                                    <td class="value">
                                                        <dx:ASPxLabel ID="ASPxLabel5" runat="server" Text='<%# Eval("Tipo_Persona") %>' />
                                                    </td>
                                                    </tr>
                                                <tr>
                                                    <td class="LabelCampo">Procentaje de participacion:&nbsp;
                                                    </td>
                                                    <td class="value">
                                                        <dx:ASPxLabel ID="lblFirstName" runat="server" Text='<%# Eval("project_producer_participation_percentage") %>' />
                                                    </td>
                                               
                                               <asp:Panel runat="server" Visible='<%# Eval("person_type_id").ToString() == "1" ? false : true %>'>
                                                   <tr>
                                                    <td class="LabelCampo">Abreviatura:&nbsp;
                                                    </td>
                                                    <td class="value">
                                                        <dx:ASPxLabel ID="ASPxLabel19" runat="server" Text='<%# Eval("abreviatura") %>' />
                                                    </td>
                                                    </tr>
                                                   <tr>
                                                    <td class="LabelCampo">Tipo de empresa:&nbsp;
                                                    </td>
                                                    <td class="value">
                                                        <dx:ASPxLabel ID="ASPxLabel20" runat="server" Text='<%# Eval("producer_company_type_name") %>' />
                                                    </td>
                                                    </tr>
                                                <tr>
                                                   </asp:Panel>

                                                </tr>
                                                <tr>
                                                    <td class="LabelCampo">Nombre:&nbsp;
                                                    </td>
                                                    <td class="value">
                                                        <dx:ASPxLabel ID="lblLastName" runat="server" Text='<%# Eval("Nombre") %>' />
                                                    </td>
                                                    </tr>
                                                <tr>
                                                    <td class="LabelCampo">Numero de identificacion:&nbsp;
                                                    </td>
                                                    <td class="value">
                                                        <dx:ASPxLabel ID="lblTitle" runat="server" Text='<%# Eval("Identificacion") %>' />
                                                    </td>
                                                </tr>

                                                

                                                <asp:Panel runat="server" Visible='<%# Eval("person_type_id").ToString() == "1" ? false : true %>'>
                                                  <tr>
                                                    <td class="LabelCampo">Nombre Representante Legal:&nbsp;
                                                    </td>
                                                    <td class="value">
                                                        <dx:ASPxLabel ID="ASPxLabel14" runat="server" Text='<%# Eval("Nombre_Rep") %>' />
                                                    </td>
                                                  </tr>
                                                  <tr>
                                                    <td class="LabelCampo">Tipo Identificación Representante Legal:&nbsp;
                                                    </td>
                                                    <td class="value">
                                                        <dx:ASPxLabel ID="ASPxLabel18" runat="server" Text='<%# Eval("Tipo_Identificacion_Rep") %>' />
                                                    </td>
                                                  </tr>
                                                  <tr>
                                                    <td class="LabelCampo">Identificación Representante Legal:&nbsp;
                                                    </td>
                                                    <td class="value">
                                                        <dx:ASPxLabel ID="ASPxLabel16" runat="server" Text='<%# Eval("Identificacion_Rep") %>' />
                                                    </td>
                                                  </tr>
                                                    
                                                  <tr>
                                                    <td class="LabelCampo">Nombre Representante Legal Suplente:&nbsp;
                                                    </td>
                                                    <td class="value">
                                                        <dx:ASPxLabel ID="ASPxLabel15" runat="server" Text='<%# Eval("Nombre_Rep_Sup") %>' />
                                                    </td>
                                                  </tr>
                                                    <tr>
                                                    <td class="LabelCampo">Identificación Representante Legal Suplente:&nbsp;
                                                    </td>
                                                    <td class="value">
                                                        <dx:ASPxLabel ID="ASPxLabel17" runat="server" Text='<%# Eval("Identificacion_Rep_Sup") %>' />
                                                    </td>
                                                  </tr>
                                                </asp:Panel>

                                                <asp:Panel runat="server" Visible='<%# Eval("person_type_id").ToString() == "2" ? false : true %>'>
                                                
                                                 <tr>
                                                    <td class="LabelCampo">Fecha de Nacimiento:&nbsp;
                                                    </td>
                                                    <td class="value">
                                                        <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text='<%# Eval("fecha_nacimiento") %>' />
                                                    </td>
                                                     </tr>
                                                <tr>
                                                    <td class="LabelCampo">Grupo Poblacional:&nbsp;
                                                    </td>
                                                    <td class="value">
                                                        <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text='<%# Eval("Grupo_Poblacional") %>' />
                                                    </td>
                                                </tr>

                                                <tr>
                                                    <td class="LabelCampo">Género:&nbsp;
                                                    </td>
                                                    <td class="value">
                                                        <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text='<%# Eval("genero") %>' />
                                                    </td>
                                                    </tr>
                                                <tr>
                                                    <td class="LabelCampo">Grupo étnico:&nbsp;
                                                    </td>
                                                    <td class="value">
                                                        <dx:ASPxLabel ID="ASPxLabel4" runat="server" Text='<%# Eval("etnia") %>' />
                                                    </td>
                                                </tr>
                                                    </asp:Panel>
                                                <tr>
                                                    <td class="LabelCampo">Pais de Origen:&nbsp;
                                                    </td>
                                                    <td class="value">
                                                        <dx:ASPxLabel ID="ASPxLabel6" runat="server" Text='<%# Eval("Pais_origen") %>' />
                                                    </td>
                                                </tr>

                                                
                                                <asp:Panel runat="server" Visible='<%# Eval("producer_type_id").ToString() == "2" ? false : true %>'>
                                                <tr>
                                                    <td class="LabelCampo">Departamento Origen:&nbsp;
                                                    </td>
                                                    <td class="value">
                                                        <dx:ASPxLabel ID="ASPxLabel13" runat="server" Text='<%# Eval("Departamento_Origen") %>' />
                                                    </td>
                                                </tr>
                                                    </asp:Panel>


                                                <tr>
                                                    <td class="LabelCampo">Ciudad de Origen:&nbsp;
                                                    </td>
                                                    <td class="value">
                                                        <dx:ASPxLabel ID="ASPxLabel7" runat="server" Text='<%# Eval("Ciudad_Origen") %>' />
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
                                                        <dx:ASPxLabel ID="ASPxLabel9" runat="server" Text='<%# Eval("producer_movil") %>' />
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
                                                    <td class="LabelCampo">Abreviatura:&nbsp;
                                                    </td>
                                                    <td class="value">
                                                        <dx:ASPxLabel ID="ASPxLabel11" runat="server" Text='<%# Eval("abreviatura") %>' />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="LabelCampo">Sitio Web:&nbsp;
                                                    </td>
                                                    <td class="value">
                                                        <dx:ASPxLabel ID="ASPxLabel12" runat="server" Text='<%# Eval("producer_website") %>' />
                                                        
                                                    </td>
                                                </tr>
                                               
                                            </table>
                                        </div>
                                    </DataRow>
                                </Templates>
    </dx:ASPxGridView>

    
   
    <asp:SqlDataSource ID="SqlDSViewProductor" runat="server" ConnectionString="<%$ ConnectionStrings:cineConnectionString %>" SelectCommand="
select 
producer.producer_type_id,
project_producer.project_producer_id,
project_producer.project_producer_participation_percentage,
producer.producer_id,
person_type.person_type_id,
person_type.person_type_name as Tipo_Persona,
(case when producer.person_type_id =2 then 
producer.producer_name 
else producer.producer_firstname +' '+ isnull(producer.producer_firstname2,'') +' '+producer.producer_lastname+' '+isnull(producer.producer_lastname2,'') end) as Nombre, 
case when producer.person_type_id =2 then 
producer.producer_nit + '-' + cast(producer.producer_nit_dig_verif as varchar)
else producer.producer_identification_number end as Identificacion,
producer.producer_phone as Telefono, 
producer.producer_movil,
producer.producer_email as Email,
case
when producer.producer_type_id=1  then 'Colombia' 
when (producer_country is not null and producer_country !='') then producer_country
else PRODUCTOR_PAIS_CONTACTO
end as Pais_origen,

CASE
when producer.producer_type_id=1 AND (localization2.localization_name IS NOT NULL AND localization2.localization_name !='')  then localization2.localization_name 
when producer.producer_type_id=1 AND (localization4.localization_name IS NOT NULL AND localization4.localization_name !='')  then localization4.localization_name 
when (producer_city is not null and producer_city !='') then producer_city
else ''
end as Departamento_Origen,   

case
when producer.producer_type_id=1 AND (localization.localization_name IS NOT NULL AND localization.localization_name !='')  then localization.localization_name 
when producer.producer_type_id=1 AND (localization3.localization_name IS NOT NULL AND localization3.localization_name !='')  then localization3.localization_name 
when (producer_city is not null and producer_city !='') then producer_city
else PRODUCTOR_CIUDAD_CONTACTO
end as Ciudad_Origen,

producer_website,
producer.fecha_nacimiento,
etnia.nombre as etnia,
genero.nombre as genero,
grupo_poblacional.nombre as Grupo_Poblacional,
producer.producer_firstname +' '+ isnull(producer.producer_firstname2,'') +' '+producer.producer_lastname+' '+isnull(producer.producer_lastname2,'')  as Nombre_Rep,
producer.primer_nombre_sup +' '+ isnull(producer.segundo_nombre_sup,'') +' '+producer.primer_apellido_sup+' '+isnull(producer.segundo_apellido_sup,'') as Nombre_Rep_Sup,
producer.abreviatura,
producer.producer_identification_number as Identificacion_Rep,
producer.num_id_sup as Identificacion_Rep_Sup,
producer_company_type.producer_company_type_name, 
producer_company_type.producer_company_type_id,
identification_type.identification_type_name as Tipo_Identificacion_Rep
from dboPrd.project_producer 
left join dboPrd.producer on producer.producer_id = project_producer.producer_id
left join dboPrd.project on project.project_id = project_producer.project_id
left join dboPrd.state on project.state_id = state.state_id 
left join dboPrd.person_type on person_type.person_type_id = producer.person_type_id
left join dboPrd.etnia on etnia.id_etnia=producer.id_etnia
left join dboPrd.genero on genero.id_genero=producer.id_genero
left join dboPrd.grupo_poblacional on grupo_poblacional.id_grupo_poblacional=producer.id_grupo_poblacional
left join dboPrd.localization on producer.producer_localization_id=localization.localization_id
left join dboPrd.localization localization2 on localization2.localization_id=localization.localization_father_id
left join dboPrd.localization localization3 on producer.PRODUCTOR_LOCALIZACION_CONTACTO_ID=localization3.localization_id
            left join dboPrd.localization localization4 on localization4.localization_id=localization3.localization_father_id


left join dboPrd.producer_company_type on producer_company_type.producer_company_type_id=producer.producer_company_type_id
left join dboPrd.identification_type on identification_type.identification_type_id=producer.identification_type_id
where project_producer.project_producer_id=@producer_id_view">
        <SelectParameters>
            <asp:ControlParameter ControlID="lblViewProductorSeleccionado" DefaultValue="0" Name="producer_id_view" PropertyName="Text" />
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
FROM dboPrd.project_attachment 
left join dboPrd.attachment on attachment.attachment_id=project_attachment.project_attachment_attachment_id
WHERE project_attachment_producer_id=@IdProductorAdjuntos">
        <SelectParameters>
            <asp:ControlParameter ControlID="lblIdProducer" DefaultValue="0" Name="IdProductorAdjuntos" PropertyName="Text" />
        </SelectParameters>
    </asp:SqlDataSource>



                    <br /><br /><br /><br /><br /><br />
 
</div>
</div>
        </div>
</asp:Content>
