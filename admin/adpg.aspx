<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="adpg.aspx.cs" Inherits="narsShop.admin.adpg"  MasterPageFile="~/admin/mysite0.Master" %>
<%@ MasterType VirtualPath="~/admin/mysite0.Master" %> 


    <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <div class="row">
        <div class="col"><asp:Button runat="server" ID="updatekcodes" Text="بروزآوری کالاها" OnClick="updatekcodes_Click" /></div>

            <div class="col"><asp:Button runat="server" ID="updatekcodepics" Text="بروزآوری عکس کالاها" OnClick="updatekcodepics_Click" /></div>


            <div class="col"><asp:Button runat="server" ID="updatecategories" Text="بروزآوری کتگوریها" OnClick="updatecategories_Click" /></div>

            <div class="col"><asp:Button runat="server" ID="updatecategorypics" Text="بروزآوری عکس کتگوریها" OnClick="updatecategorypics_Click" /></div>
        </div>
        <div class="row" style="height:20px"></div>

        <div class="row">
<div class="col"><asp:TextBox ID="minpishpart" runat="server" TextMode="Number" step=".1" ></asp:TextBox></div>
            <div class="col"><asp:TextBox ID="minpass" runat="server" TextMode="Password" ></asp:TextBox></div>
            <div class="col"><asp:Button ID="sbtminpish" runat="server" OnClick="sbtminpish_Click"  Text="ذخیره"></asp:Button></div>
        </div>

</asp:Content>