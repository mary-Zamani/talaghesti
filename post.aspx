<%@ Page Language="C#" MasterPageFile="~/mst.Master" AutoEventWireup="true" CodeBehind="post.aspx.cs" Inherits="narsShop.post" %>
<%@ MasterType VirtualPath="~/mst.Master" %> 




    <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
        <div class="card"  style="text-align:right">
  <asp:Image CssClass="card-img-top" runat="server"  ID="imgnews"/>
  <div class="card-body">
    <h5 class="card-title"><asp:Label runat="server" ID="onvan"></asp:Label></h5>
    <div class="card-text"><asp:Label runat="server" ID="sharh"></asp:Label></div>   
  </div>
    <div class="card-footer">
        <asp:Label runat="server" ID="noe">    </asp:Label>
    </div>

</div>
           
</asp:Content>