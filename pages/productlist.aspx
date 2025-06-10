<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="productlist.aspx.cs" Inherits="narsShop.pages.productlist" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <div class="sub-file">

<asp:Label runat="server" ID="lbl_productrow1" ClientIDMode="Static" />

        </div>

<asp:Button ID="btn_add" runat="server" style="visibility:hidden"/>
</asp:Content>
