<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="CineProducto.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">



    <asp:DropDownList ID="cmbDepto" runat="server" DataSourceID="dsDepto" DataTextField="localization_name" DataValueField="localization_id"></asp:DropDownList>

    
    <asp:SqlDataSource ID="dsDepto" runat="server" ConnectionString="<%$ ConnectionStrings:cineConnectionString %>" SelectCommand="select * from dboPrd.localization where localization_father_id=0"></asp:SqlDataSource>

    <br />

    <asp:DropDownList ID="cmbCiudad" runat="server" DataSourceID="dsCiudad" DataTextField="localization_name" DataValueField="localization_id">
    </asp:DropDownList>
    <asp:SqlDataSource ID="dsCiudad" runat="server" ConnectionString="<%$ ConnectionStrings:cineConnectionString %>" SelectCommand="select * from dboPrd.localization where localization_father_id=@id_padre">
        <SelectParameters>
            <asp:ControlParameter ControlID="cmbDepto" DefaultValue="05" Name="id_padre" PropertyName="SelectedValue" />
        </SelectParameters>
    </asp:SqlDataSource>




     <div class="field_input">
                            <asp:DropDownList ID="cmbFormatoPadre" AutoPostBack="true" CssClass="user-input" runat="server" name="cmbFormatoPadre" AppendDataBoundItems="True" DataSourceID="SqlDataSourceFormatoPadre" DataTextField="format_name" DataValueField="format_id">
                                <asp:ListItem Text="Seleccione..." Value="0"></asp:ListItem>
                            </asp:DropDownList>                            
                            <asp:SqlDataSource ID="SqlDataSourceFormatoPadre" runat="server" ConnectionString="<%$ ConnectionStrings:cineConnectionString %>" SelectCommand="select format_id, format_name  from dboPrd.format where format_type_id = 1 and format_id in (22,23) order by format_name"></asp:SqlDataSource>
                            
                            <asp:DropDownList ID="cmbFormatoRodaje"  class="user-input" runat="server" name="cmbFormatoRodaje" AppendDataBoundItems="false" DataSourceID="SqlDataSourceFormatos" DataTextField="format_name" DataValueField="format_id">
                                <asp:ListItem Text="Seleccione..." Value="0"></asp:ListItem>
                            </asp:DropDownList>                            
                            <asp:SqlDataSource ID="SqlDataSourceFormatos" runat="server" ConnectionString="<%$ ConnectionStrings:cineConnectionString %>" SelectCommand="select format_id, format_name  from dboPrd.format where format_type_id = 1 and format_padre = @format_padre order by format_name
">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="cmbFormatoPadre" DefaultValue="-1" Name="format_padre" PropertyName="SelectedValue" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                             
                        </div>      



</asp:Content>
