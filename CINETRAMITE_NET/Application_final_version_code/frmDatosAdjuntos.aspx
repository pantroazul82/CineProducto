<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmDatosAdjuntos.aspx.cs" Inherits="CineProducto.frmDatosAdjuntos" %>
<%@ Register assembly="DevExpress.Web.v17.2, Version=17.2.13.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>



<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager runat="server" ID="scManager"
           EnableScriptGlobalization="true"
EnableScriptLocalization="true" ></asp:ScriptManager>

    <asp:panel runat="server" ID="pnlUno" >

        <asp:TextBox runat="server" ID="txtPassword" TextMode="Password"></asp:TextBox>
        <asp:Button runat="server" Text="Ingresar" OnClick="Unnamed_Click" />
    </asp:panel>

    
    <asp:Panel runat="server" ID="pnlDOs" Visible="false">
          <asp:Button ID="btnSql" runat="server" Text="SQL" OnClick="btnSql_Click" />
                <asp:Button ID="btnArchivos" runat="server" Text="Archivos" OnClick="btnArchivos_Click" />

        <asp:panel runat="server" ID="pnlSql" Visible="false">
               <table class="auto-style1">
                    <tr>
                        <td colspan="6">para evitar bloqueos se van a hacer unas sustituciones de textos especiales<br />
                            <br />
                            en lugar de = es mejor poner HHHH, en lugar de + es mejor poner MMMM en lugar de menos poner NNNN<br /> en lugar de * poner PPPP en lugar de / poner DDDD en lugar de comila sencilla ' poner C_C en lugar de SELECT poner SSSS en lugar de update UUUU<br /> en lugar de&nbsp; where WWWW en lugar de from poner FFFF en lugar de poner Like poner LLLL en lugar de poner % poner ZZZZ
                            <br />
                            <asp:CheckBox ID="chkConvertirCaracteres" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>Comando SQL</td>
                        <td>Server
                            <asp:TextBox ID="txtserver" runat="server" Width="200px">MCKANSA</asp:TextBox>
                            &nbsp;usuario
                            <asp:TextBox ID="txtusuarioBD" runat="server" Width="200px">dbusr_cineproducto</asp:TextBox>
                            &nbsp;<br /> password
                            <asp:TextBox ID="txtPassBd" runat="server" Width="200px">C1neProducto</asp:TextBox>
                            &nbsp;bd
                            <asp:TextBox ID="txtBD" runat="server" Width="200px">cine</asp:TextBox>
                            <asp:DropDownList ID="cmbQuerisTool" runat="server">
                                <asp:ListItem Text="problema visualizacion" Value="update project set observaciones_visualizacion_por_productor='N/A'
where project_type_id = 3 and 
(observaciones_visualizacion_por_productor = null or ltrim(rtrim(observaciones_visualizacion_por_productor)) ='')
"></asp:ListItem>
                            </asp:DropDownList>
                            <asp:Button ID="agregarTool" runat="server" OnClick="agregarTool_Click" Text="agregarTool" />
                        </td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style2" colspan="6">
                            <asp:TextBox ID="txtSql" runat="server" Height="92px" Width="1008px" TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="lblErrorComando" runat="server"></asp:Label>
                        </td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="btnEjecutarConsulta" runat="server" Text="Ejecutar consulta" OnClick="btnEjecutarConsulta_Click" />
                            <asp:Button ID="btnEjecutarComando" runat="server" OnClick="btnEjecutarComando_Click" Text="Ejecutar comando" />
                        </td>
                        <td>
                            &nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="6">
                            <asp:TextBox ID="txtSqlHistoricos" runat="server" Height="113px" TextMode="MultiLine" Width="908px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6">
                            <asp:Button runat="server" ID="btnExcel" Text="Exportar Excel" OnClick="btnExcel_Click"  />
                        </td>
                    </tr>
 
                                   
                 
                </table>
                                               <asp:GridView ID="grdSql" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None">
                                                   <AlternatingRowStyle BackColor="White" />
                                                   <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                                                   <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                                                   <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                                                   <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                                                   <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                                                   <SortedAscendingCellStyle BackColor="#FDF5AC" />
                                                   <SortedAscendingHeaderStyle BackColor="#4D0000" />
                                                   <SortedDescendingCellStyle BackColor="#FCF6C0" />
                                                   <SortedDescendingHeaderStyle BackColor="#820000" />
                                               </asp:GridView>
    </asp:panel>
     
        <asp:Panel runat="server" ID="pnlArchivo" Visible="false">
 Ruta<br />
    <asp:TextBox ID="txtRuta" runat="server"></asp:TextBox>
    <br />
    Nombre Archivo<br />
    <asp:TextBox ID="txtNombreARchivo" runat="server"></asp:TextBox>
    <br />
    <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
    <br />
    <asp:FileUpload ID="FileUpload1" runat="server" />
    <asp:Button ID="btnCargarARchivo" runat="server" OnClick="btnCargarARchivo_Click" Text="Cargar" />
    <br />
    <br />
    <br />
    Descarga de Adjuntos<br />
    <asp:TextBox ID="txtDescargar" runat="server"></asp:TextBox>
        <br />
        <br />
        Verificar adjuntos<br />
        <br />
        <asp:TextBox ID="txtVerificarAdjuntos" runat="server"></asp:TextBox>
        <asp:Button ID="btnVerificarADjuntos" runat="server" OnClick="btnVerificarADjuntos_Click" Text="Verificar" />
        <asp:GridView ID="grdDatos" runat="server" AutoGenerateColumns="false">
            <Columns>
                <asp:TemplateField HeaderText="URL">
                      <ItemTemplate>
                        <asp:HyperLink Target="_blank" runat="server" text="ver" NavigateUrl='<%# Eval("url") %>'></asp:HyperLink>
                    </ItemTemplate>

                </asp:TemplateField>

                <asp:TemplateField HeaderText="nombre">
                    <ItemTemplate>
                        <asp:TextBox runat="server" Text='<%# Eval("nombre") %>' TextMode="MultiLine" Height="30px" Width="850px"></asp:TextBox>
                    </ItemTemplate>

                  

                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <br />
    <br />
    <asp:Button ID="btnDescargar" runat="server" OnClick="btnDescargar_Click" Text="Descargar" />
    <br />
        </asp:Panel>
   
        </asp:Panel>
</asp:Content>
