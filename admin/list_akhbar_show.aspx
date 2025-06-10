<%@ Page Language="C#" MasterPageFile="~/niceadmin.Master" AutoEventWireup="true" CodeBehind="list_akhbar_show.aspx.cs" Inherits="narswebadmin.list_akhbar_show" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register Src="~/UDC/DateInput.ascx" TagPrefix="uc1" TagName="DateInput" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<%@ MasterType VirtualPath="~/niceadmin.Master" %> 
<asp:Content ID="Content1" ContentPlaceHolderID="headPlaceHolder" runat="server">

    
   

    </asp:Content>



    <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
        <div class="card" >
  <asp:Image CssClass="card-img-top" runat="server"  ID="imgnews"/>
  <div class="card-body">
    <h5 class="card-title"><asp:Label runat="server" ID="onvan"></asp:Label></h5>
    <div class="card-text"><asp:Label runat="server" ID="sharh"></asp:Label></div>   
  </div>
    <div class="card-footer">
        <asp:Label runat="server" ID="noe">    </asp:Label>
    </div>

</div>


            <asp:Button ID="Btn_close" runat="server" Text="بستن" CssClass="btn btn-primary"/>
</asp:Content>