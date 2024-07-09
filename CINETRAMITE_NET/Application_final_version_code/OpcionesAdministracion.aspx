<%@ Page Title="Listado de proyectos" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="OpcionesAdministracion.aspx.cs" Inherits="CineProducto.OpcionesAdministracion" %>

<asp:Content ID="TitleContent" runat="server" ContentPlaceHolderID="TitleContent">Trámite Reconocimiento Como Obra Nacional - Opciones de Administración - Mincultura</asp:Content>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <%
    if (currentForm == "administracionopcionespersonal")
    {
    %>
        <script src="Scripts/administracionopcionespersonal_grid.js" type="text/javascript"></script>
    <%
    } 
    %>
    <%
    if (currentForm == "administracionadjuntos")
    {
    %>
        <script src="Scripts/administracionadjuntos_grid.js" type="text/javascript"></script>
    <%
    } 
    %>
    <%
    if (currentForm == "administracionvalidacionadjuntos")
    {
    %>
        <script src="Scripts/administracionvalidacionadjuntos_grid.js" type="text/javascript"></script>
    <%
    } 
    %>
    <%
    if (currentForm == "administracionroles")
    {
    %>
        <script src="Scripts/administracionroles_grid.js" type="text/javascript"></script>
    <%
    } 
    %>
    <%
    if (currentForm == "administracionpermisos")
    {
    %>
        <script src="Scripts/administracionpermisos_grid.js" type="text/javascript"></script>
    <%
    } 
    %>
    <%
 
    %>
    <%if (currentForm == "cargospersonal" )
    {
    %>
        <script src="Scripts/cargosPersonal.js" type="text/javascript"></script>
    <%
    } 
    %>
    <%
    if (currentForm == "administracionFormatos")
    {
    %>
        <script src="Scripts/administracionFormatos_grid.js" type="text/javascript"></script>
    <%
    } 
    %>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        Opciones de administración
    </h2>
    <p>
        <a href="~/Lista.aspx" runat="server" type="text/asp"><< Volver al listado de solicitudes</a>
    </p>
    
    <%
    if (currentForm == "configuraciongeneral")
    { %>
        <div>Interfaz de configuración general de la aplicación<br />
            <asp:GridView ID="GridViewConfiguracionGeneral" runat="server" AllowPaging="True" 
                AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="configuration_id" 
                DataSourceID="SqlDataSourceConfiguracionGeneral" BackColor="White" 
                BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
                GridLines="Horizontal">
                <AlternatingRowStyle BackColor="#F7F7F7" />
                <Columns>
                    <asp:BoundField DataField="configuration_id" HeaderText="ID" 
                        InsertVisible="False" ReadOnly="True" SortExpression="configuration_id" />
                    <asp:BoundField DataField="configuration_name" HeaderText="Nombre variable" 
                        SortExpression="configuration_name" />
                    <asp:BoundField DataField="configuration_description" 
                        HeaderText="Descripción" 
                        SortExpression="configuration_description" />
                    <asp:BoundField DataField="configuration_value" HeaderText="Valor" />
                    <asp:CommandField ShowEditButton="True" ButtonType="Button" 
                        ShowInsertButton="True" />
                </Columns>
                <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
                <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
                <RowStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" />
                <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                <SortedAscendingCellStyle BackColor="#F4F4FD" />
                <SortedAscendingHeaderStyle BackColor="#5A4C9D" />
                <SortedDescendingCellStyle BackColor="#D8D8F0" />
                <SortedDescendingHeaderStyle BackColor="#3E3277" />
            </asp:GridView>
            <asp:SqlDataSource ID="SqlDataSourceConfiguracionGeneral" runat="server" 
                ConnectionString="<%$ ConnectionStrings:cineConnectionString %>" 
                DeleteCommand="DELETE FROM dboPrd.[configuration] WHERE [configuration_id] = @configuration_id" 
                InsertCommand="INSERT INTO dboPrd.[configuration] ([configuration_name], [configuration_description], [configuration_value]) VALUES (@configuration_name, @configuration_description, @configuration_value)" 
                SelectCommand="SELECT * FROM dboPrd.[configuration] ORDER BY [configuration_name]" 
                UpdateCommand="UPDATE dboPrd.[configuration] SET [configuration_name] = @configuration_name, [configuration_description] = @configuration_description, [configuration_value] = @configuration_value WHERE [configuration_id] = @configuration_id">
                <DeleteParameters>
                    <asp:Parameter Name="configuration_id" Type="Int32" />
                </DeleteParameters>
                <InsertParameters>
                    <asp:Parameter Name="configuration_name" Type="String" />
                    <asp:Parameter Name="configuration_description" Type="String" />
                    <asp:Parameter Name="configuration_value" Type="String" />
                </InsertParameters>
                <UpdateParameters>
                    <asp:Parameter Name="configuration_name" Type="String" />
                    <asp:Parameter Name="configuration_description" Type="String" />
                    <asp:Parameter Name="configuration_value" Type="String" />
                    <asp:Parameter Name="configuration_id" Type="Int32" />
                </UpdateParameters>
            </asp:SqlDataSource>
    </div>

    <% 
    }
    if (currentForm == "modificaciontextosayuda")
    {%>
    <div><h3>Interfaz de configuración general de los textos de ayuda (tootips)</h3>
        <asp:GridView ID="GridViewConfiguracionTooltips" runat="server" AllowPaging="True" 
                AllowSorting="True" AutoGenerateColumns="False" BackColor="White" 
                BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
                DataKeyNames="tooltip_id" DataSourceID="SqlDataSourceTooltips" 
                GridLines="Horizontal" PageSize="30">
                <AlternatingRowStyle BackColor="#F7F7F7" />
                <Columns>
                    <asp:BoundField DataField="tooltip_id" HeaderText="tooltip_id" 
                        InsertVisible="False" ReadOnly="True" SortExpression="tooltip_id" 
                        Visible="False" />
                    <asp:BoundField DataField="tooltip_name" HeaderText="Identificador del tooltip" 
                        SortExpression="tooltip_name" ReadOnly="True" >
                    <HeaderStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="Texto que se presentará al usuario" 
                        SortExpression="tooltip_text">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("tooltip_text") %>' 
                                Height="36px" Width="500px"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("tooltip_text") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:CommandField ShowEditButton="True" EditText="Cambiar texto" />
                </Columns>
                <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
                <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
                <RowStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" />
                <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                <SortedAscendingCellStyle BackColor="#F4F4FD" />
                <SortedAscendingHeaderStyle BackColor="#5A4C9D" />
                <SortedDescendingCellStyle BackColor="#D8D8F0" />
                <SortedDescendingHeaderStyle BackColor="#3E3277" />
            </asp:GridView>
            <asp:SqlDataSource ID="SqlDataSourceTooltips" runat="server" 
                ConnectionString="<%$ ConnectionStrings:cineConnectionString %>" 
                DeleteCommand="DELETE FROM dboPrd.[tooltip] WHERE [tooltip_id] = @tooltip_id" 
                InsertCommand="INSERT INTO dboPrd.[tooltip] ([tooltip_name], [tooltip_text]) VALUES (@tooltip_name, @tooltip_text)" 
                SelectCommand="SELECT * FROM dboPrd.[tooltip] ORDER BY [tooltip_name]" 
                UpdateCommand="UPDATE dboPrd.[tooltip] SET [tooltip_text] = @tooltip_text WHERE [tooltip_id] = @tooltip_id">
                <DeleteParameters>
                    <asp:Parameter Name="tooltip_id" Type="Int32" />
                </DeleteParameters>
                <InsertParameters>
                    <asp:Parameter Name="tooltip_name" Type="String" />
                    <asp:Parameter Name="tooltip_text" Type="String" />
                </InsertParameters>
                <UpdateParameters>
                    <asp:Parameter Name="tooltip_text" Type="String" />
                    <asp:Parameter Name="tooltip_id" Type="Int32" />
                </UpdateParameters>
            </asp:SqlDataSource>
        </div>
    <% 
    }
    if (currentForm == "administracionopcionespersonal")
    {
    %>
     </div>
     <div style="width=200px">
        Version: <asp:DropDownList ID="cmbVersion2" AutoPostBack="true" runat="server" OnSelectedIndexChanged="cmbVersion2_SelectedIndexChanged" >
            <asp:ListItem Text="1" Value="1"></asp:ListItem>
            <asp:ListItem Text="2" Value="2"></asp:ListItem>
                 </asp:DropDownList>
      </div>
   
        <table id="grid"></table>
        <div id="pager"></div>
    <%
    } 
    %>
    <%
    if (currentForm == "administracionadjuntos")
    {
    %>  </div>
        <table id="grid_attachment"></table>
        <div id="pager_attachment"></div>
    <%
    } 
    %>
    <%
    if (currentForm == "administracionvalidacionadjuntos")
    {
    %>  </div>
        <table id="grid_validationattachment"></table>
        <div id="pager_validationattachment"></div>
    <%
    } 
    %>
    <%
    if (currentForm == "administracionroles")
    {
    %>  </div>
        <table id="grid_role"></table>
        <div id="pager_role"></div>
    <%
    } 
    %>
    <%
    if (currentForm == "administracionpermisos")
    {
    %>  </div>
        <table id="grid_permission"></table>
        <div id="pager_permission"></div>
    <%
    } 
    %>

    <%
    if (currentForm == "cargospersonal")
    {
    %>  </div>
        <table id="grid_cargospersonal"></table>
        <div id="pager_cargospersonal"></div>
    <%
    } 
    %>
    <%
    if (currentForm == "administracionFormatos")
    {
        
    %>  </div><table id="grid_formatos"></table>
        <div id="pager_formatos"></div>
    <%
    } 
    %>

</asp:Content>