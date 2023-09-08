<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmPruebaPivoteProduccion.aspx.cs" Inherits="CineProducto.frmPruebaPivoteProduccion" %>
<%@ Register assembly="DevExpress.Web.ASPxPivotGrid.v17.2, Version=17.2.13.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxPivotGrid" tagprefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <p>
        Prueba Pivote</p>
    <p>
        <dx:ASPxPivotGrid ID="ASPxPivotGrid1" runat="server" ClientIDMode="AutoID" DataSourceID="SqlDataSource1" EnableTheming="True" Theme="DevEx">
            <Fields>
                <dx:PivotGridField ID="fieldEstado" Area="RowArea" AreaIndex="0" FieldName="Estado" Name="fieldEstado">
                </dx:PivotGridField>
                <dx:PivotGridField ID="fieldProductor" Area="ColumnArea" AreaIndex="0" FieldName="Productor" Name="fieldProductor">
                </dx:PivotGridField>
                <dx:PivotGridField ID="fieldcnt" Area="DataArea" AreaIndex="0" FieldName="cnt" Name="fieldcnt">
                </dx:PivotGridField>
                <dx:PivotGridField ID="fieldstateid" AreaIndex="0" FieldName="state_id" Name="fieldstateid">
                </dx:PivotGridField>
                <dx:PivotGridField ID="fieldprojectid" AreaIndex="1" FieldName="project_id" Name="fieldprojectid">
                </dx:PivotGridField>
                <dx:PivotGridField ID="fieldlogin" AreaIndex="2" FieldName="login" Name="fieldlogin">
                </dx:PivotGridField>
                <dx:PivotGridField ID="fieldObra" AreaIndex="3" FieldName="Obra" Name="fieldObra">
                </dx:PivotGridField>
                <dx:PivotGridField ID="fieldfechaenvio" AreaIndex="4" FieldName="fecha_envio" Name="fieldfechaenvio">
                </dx:PivotGridField>
                <dx:PivotGridField ID="fieldhoraenvio" AreaIndex="5" FieldName="hora_envio" Name="fieldhoraenvio">
                </dx:PivotGridField>
                <dx:PivotGridField ID="fieldfechasolicitudaclaraciones" AreaIndex="6" FieldName="fecha_solicitud_aclaraciones" Name="fieldfechasolicitudaclaraciones">
                </dx:PivotGridField>
                <dx:PivotGridField ID="fieldhorasolicitudaclaraciones" AreaIndex="7" FieldName="hora_solicitud_aclaraciones" Name="fieldhorasolicitudaclaraciones">
                </dx:PivotGridField>
                <dx:PivotGridField ID="fieldfechaenvioaclaraciones" AreaIndex="8" FieldName="fecha_envio_aclaraciones" Name="fieldfechaenvioaclaraciones">
                </dx:PivotGridField>
                <dx:PivotGridField ID="fieldhoraenvioaclaraciones" AreaIndex="9" FieldName="hora_envio_aclaraciones" Name="fieldhoraenvioaclaraciones">
                </dx:PivotGridField>
                <dx:PivotGridField ID="fieldfecharesolucion" AreaIndex="10" FieldName="fecha_resolucion" Name="fieldfecharesolucion">
                </dx:PivotGridField>
                <dx:PivotGridField ID="fieldfechanotificacion" AreaIndex="11" FieldName="fecha_notificacion" Name="fieldfechanotificacion">
                </dx:PivotGridField>
                <dx:PivotGridField ID="fieldprojectnotification2date" AreaIndex="12" FieldName="project_notification2_date" Name="fieldprojectnotification2date">
                </dx:PivotGridField>
                <dx:PivotGridField ID="fieldresolutionpath" AreaIndex="13" FieldName="resolution_path" Name="fieldresolutionpath">
                </dx:PivotGridField>
                <dx:PivotGridField ID="fieldresolutionpath2" AreaIndex="14" FieldName="resolution_path2" Name="fieldresolutionpath2">
                </dx:PivotGridField>
            </Fields>
        </dx:ASPxPivotGrid>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:cineConnectionString %>" SelectCommand="select   1cnt,
p.state_id,p.project_id, 
s.state_name  Estado, 
case when person_type_id =2 then 
producer.producer_name 
else producer.producer_firstname +' '+producer.producer_lastname end
'Productor',
usuario.username 'login' ,
p.project_name Obra, 
cast(p.project_request_date as date)  fecha_envio,cast(p.project_request_date as time) hora_envio,
cast(p.project_clarification_request_date as date)  fecha_solicitud_aclaraciones,cast(p.project_clarification_request_date as time) hora_solicitud_aclaraciones,
cast(p.project_clarification_response_date as date)  fecha_envio_aclaraciones,cast(p.project_clarification_response_date as time) hora_envio_aclaraciones,


p.project_resolution_date 'fecha_resolucion', 

cast(p.project_notification_date as date) fecha_notificacion,
project_notification2_date,
r.resolution_path,
resolution_path2


 from project p 
 left join resolution  r on r.project_id= p.project_id 
 left join state s on p.state_id = s.state_id 
 join usuario on usuario.idusuario = p.project_idusuario
left join project_producer on project_producer.project_id = p.project_id and project_producer.project_producer_requester=1
left join producer on producer.producer_id = project_producer.producer_id"></asp:SqlDataSource>
    </p>
    <p>
        &nbsp;</p>
</asp:Content>
