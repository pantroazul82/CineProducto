<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmResponsable.aspx.cs" Inherits="CineProducto.frmResponsable" %>

<%@ Register assembly="DevExpress.Web.v17.2, Version=17.2.13.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>


<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    

    
    <asp:HyperLink runat="server" ID="linkRegresar" NavigateUrl="~/Lista.aspx" Text="<< Regresar a Lista de Proyectos"> </asp:HyperLink>

    <h3>Asignacion de Responsable al proyecto</h3>

    <asp:Label ID="lblProjectId" runat="server" Text=""/>
    <h5>Informacion del proyecto</h5>
    <table class="table" border="0" width="80%">
        <tr>
            <td><b>Nombre: </b></td>
            <td><asp:Label ID="lblNombre" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr>
            <td><b>Sinopsis: </b></td>
            <td><asp:Label ID="lblSinopsis" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr>
            <td><b>Tipo: </b></td>
            <td><asp:Label ID="lblTipo" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr>
            <td><b>Genero: </b></td>
            <td><asp:Label ID="lblGenero" runat="server" Text=""></asp:Label></td>
        </tr>
         <tr>
            <td><b>Duracion:&nbsp;&nbsp;&nbsp;&nbsp;</b></td>
            <td><asp:Label ID="lblDuracion" runat="server" Text=""></asp:Label></td>
        </tr>
    </table>
    


    <h5>Asignacion</h5>
    <table class="table" border="0" width="600px">
        <tr>
            <td><b>Asignar a: </b></td>
            <td ><asp:DropDownList ID="cmbUsuario" runat="server" DataSourceID="SqlDataSourceUsuarios" DataTextField="nombre" DataValueField="idusuario" AppendDataBoundItems="true">
                
                <Items>
                   <asp:ListItem Enabled="true" Selected="True" Value="-1" Text="Seleccione..."></asp:ListItem>
               </Items>
                
                </asp:DropDownList>
                <asp:SqlDataSource ID="SqlDataSourceUsuarios" runat="server" ConnectionString="<%$ ConnectionStrings:cineConnectionString %>" SelectCommand="select 
                        usuario.idusuario,
                        nombres +' ' + apellidos as nombre
                        from dboPrd.usuario
						join dboPrd.role_assignment on role_assignment.idusuario=usuario.idusuario
                        where es_responsable = 1 and activo = 1
                        order by nombres +' ' + apellidos
                    
                    "></asp:SqlDataSource>
            </td>
            <td width="65%"></td>
        </tr>
        <tr>
            <td></td>
            <td ><asp:Button runat="server" ID="btnGuardar"  Text="Guardar" OnClick="btnGuardar_Click"/>
                <asp:Label runat="server" ID="lblMsgEstado" Text="No es posible reasignar en el estado actual." Visible="false"></asp:Label>

            </td>            
            <td width="65%"></td>
        </tr>
        <tr>
            <td></td>
            <td><asp:Label ID="lblError" runat="server" Text=""></asp:Label></td>            
            <td width="65%"></td>
        </tr>
        
    </table>
    

    <h5>Historial de Asignaciones</h5>

    <dx:ASPxGridView ID="grdDevDatos" Width="80%" runat="server" EnableTheming="True" Theme="DevEx" AutoGenerateColumns="true" DataSourceID="SqlDataSource1">
         
      <SettingsPager ShowEmptyDataRows="True" PageSize="10"></SettingsPager>

         <Settings ShowFilterRow="True" />
         <SettingsDataSecurity AllowDelete="False" AllowEdit="False" AllowInsert="False" />
         <SettingsText  EmptyDataRow="No hay registros que cumplan con las condiciones de la búsqueda o que requieran su atención." />
         
         <Columns>                         

             <dx:GridViewDataDateColumn FieldName="fecha" Caption="Fecha Asignacion" VisibleIndex="0">
             </dx:GridViewDataDateColumn>
             <dx:GridViewDataTextColumn FieldName="Responsable" ReadOnly="True" VisibleIndex="1">
             </dx:GridViewDataTextColumn>
             <dx:GridViewDataTextColumn FieldName="Asignado_Por" ReadOnly="True" VisibleIndex="2">
             </dx:GridViewDataTextColumn>                         

         </Columns>
       
     </dx:ASPxGridView>



    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:cineConnectionString %>" SelectCommand="select 
project_responsable.fecha,
r.nombres+' ' + r.apellidos as Responsable, 
a.nombres+' ' + a.apellidos as Asignado_Por,
project_responsable.project_id
from dboPrd.project_responsable
join dboPrd.usuario r on r.idusuario = project_responsable.responsable
join dboPrd.usuario a on a.idusuario = project_responsable.asignado_por
where project_responsable.project_id=@idProject
order by project_responsable.fecha desc
        ">
        <SelectParameters>
            <asp:ControlParameter ControlID="lblProjectId" DefaultValue="0" Name="idProject" PropertyName="Text" />
        </SelectParameters>
    </asp:SqlDataSource>



</asp:Content>
