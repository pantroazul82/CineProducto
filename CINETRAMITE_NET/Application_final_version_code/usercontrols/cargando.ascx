<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="cargando.ascx.cs" Inherits="CineProducto.usercontrols.cargando" %>
  

<div id="loading" style="position:fixed;top:0; left:0;right:0;bottom:0; 
background-color: rgba(248,248,248,0.9);vertical-align:middle;">
                 <center>
                        <div style="position:absolute;z-index:10000;left:45%;
                        top:30%"> 
                            <div style="width:100%;">
   <asp:Image Width="150px" runat="server" ID="imgCargando" ImageUrl="~/images/loading.gif"  ></asp:Image>
                          
                            </div>
                            <div style="width:100%;"> 
                                <asp:Label runat="server" ID="lblLoading" Visible="false" Font-Bold="true"
                                    Font-Size="25px" Text="Cargando"
                                    ></asp:Label>

                            </div>
                                  
                            </div>
                             </center>
       </div>