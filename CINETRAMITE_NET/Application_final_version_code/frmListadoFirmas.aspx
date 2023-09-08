<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmListadoFirmas.aspx.cs" Inherits="CineProducto.frmListadoFirmas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
 
   <div class="row" style="width:98%">
          <div class="col-12">
     <b>Configuración Firmas tramite </b><asp:Button ID="btnAgregar" runat="server" OnClick="btnAgregar_Click" Text="Agregar Firma" />
       </div>
    </div>
    
  
      <div class="row">
        <div class="col-12">
  
    &nbsp;<asp:GridView ID="grdDatos" runat="server" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="cod_firma_tramite" DataSourceID="SqlDataSourceFirmas" ForeColor="#333333" GridLines="None">
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <Columns>
            <asp:BoundField DataField="cod_firma_tramite" HeaderText="Cod" InsertVisible="False" ReadOnly="True" SortExpression="cod_firma_tramite" />
            <asp:BoundField DataField="nombre_firma_tramite" HeaderText="Nombre" SortExpression="nombre_firma_tramite" />
            <asp:BoundField DataField="cargo_firma_tramite" HeaderText="Cargo" SortExpression="cargo_firma_tramite" />
            <asp:CheckBoxField DataField="activo" HeaderText="activo" SortExpression="activo" />
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Button runat="server" Text="Editar" CommandArgument='<%# Eval("cod_firma_tramite") %>' ID="btnEditar" OnClick="btnEditar_Click" />
                    <asp:Button runat="server" Text="Eliminar" OnClientClick="return confirm('Esta seguro de eliminar el registro?'); " 
                        CommandArgument='<%# Eval("cod_firma_tramite") %>' ID="btnEliminar" OnClick="btnEliminar_Click" />
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
    </div>
              </div>
    <asp:SqlDataSource ID="SqlDataSourceFirmas" runat="server" ConnectionString="<%$ ConnectionStrings:cineConnectionString %>" SelectCommand="SELECT [cod_firma_tramite], [nombre_firma_tramite], [cargo_firma_tramite], [activo] FROM [firma_tramite] where cod_firma_tramite not in (2,4,6) "></asp:SqlDataSource>
</asp:Content>
