<%@ Page Language="C#" MasterPageFile="~/admin/mysite0.Master" AutoEventWireup="true" CodeBehind="list_akhbar.aspx.cs" Inherits="narsShop.admin.list_akhbar" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<%@ MasterType VirtualPath="~/admin/mysite0.Master" %> 


    <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        
     <asp:Button ID="Button1" runat="server" Text="جدید" OnClick="Button1_Click" />
    
	    <br />
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="table table-advance table-bordered" >
        <Columns>
            
        <asp:HyperLinkField DataNavigateUrlFields="s_id" DataNavigateUrlFormatString="..\post.aspx?s_id={0}" DataTextField="s_id" HeaderText="نمایش" />
        <asp:BoundField DataField="senddate" HeaderText="تاريخ"   ReadOnly="True"   ItemStyle-CssClass="numeric" SortExpression="senddate"/>
        <asp:BoundField DataField="onvan" HeaderText="شرح" ReadOnly="true" />
        <asp:BoundField DataField="eghd_date" HeaderText="تاريخ نمایش" ReadOnly="true" ItemStyle-CssClass="numeric"/>
        <asp:BoundField DataField="pdate" HeaderText="تاریخ انقضا"  ReadOnly="True" />
        <asp:HyperLinkField DataNavigateUrlFields="s_id" DataNavigateUrlFormatString="list_akhbar_add.aspx?s_id={0}" DataTextField="s_id" HeaderText="اصلاح" />


        </Columns>
        </asp:GridView>
     
        
        




</asp:Content>