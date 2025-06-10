<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="productlist_old.aspx.cs" Inherits="narsShop.productlist_old"  MasterPageFile="~/mst.Master"%>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ MasterType VirtualPath="~/mst.Master" %> 


    <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     

    <div class="sub-file">

<asp:Label runat="server" ID="lbl_productrow1" ClientIDMode="Static" />

        </div>



<asp:Button ID="btn_add" runat="server" style="visibility:hidden"/>

</asp:Content>
