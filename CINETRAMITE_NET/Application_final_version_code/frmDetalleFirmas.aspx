<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmDetalleFirmas.aspx.cs" Inherits="CineProducto.frmDetalleFirmas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

    <fieldset>
        <legend>Configuración Firmas</legend>

        <div class="field_label">
                            Nombre:<span class="required_field_text">*</span>
        </div>
        <div class="field_input">
                            <asp:TextBox ID="txtNombre" runat="server"  >
                            </asp:TextBox>
        </div>

        
        <div class="field_label">
                            Cargo:
        </div>
        <div class="field_input">
                            <asp:TextBox ID="txtCargo" runat="server"  >
                            </asp:TextBox>
        </div>

        
        <div class="field_label">
            <asp:CheckBox runat="server" ID="chkActivo" Text="Activo" />                
            
        </div>
        <div class="field_input">
              
        </div>

        <div class="field_label">
            <asp:Label runat="server" ID="lblError" ForeColor="Red" Text="" />                
            
        </div>
        <div class="field_input">
              <asp:Button runat="server" ID="btnGuardar" Text="Guardar" OnClick="btnGuardar_Click" />
            <asp:Button runat="server" ID="btnCancelar" Text="Cancelar" OnClick="btnCancelar_Click" />
        </div>




    </fieldset>
    
    
        

    
</asp:Content>
