<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="successed.aspx.cs" Inherits="narsShop.successed"  MasterPageFile="~/mst.Master"%>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ MasterType VirtualPath="~/mst.Master" %> 


    <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


        <br />
<div class="navbar navbar-nav">
<p class="btn-warning"></p>
    </div>

<!-- Page Container -->
<!-- The Grid -->
 <div class="row mb-f justify-content-between">
 
    <!-- Left Column -->
  

              <!-- Right Column -->
    <div class="col mb-6 mb-md-5" style="text-align:right">
        
                      <div class="card  border-light shadow-soft">
                      <div class="card-body">
<h5><asp:Label ID="lbl_title" runat="server" /></h5>


          <table width="100%" style="max-width:500px">
              <tr>
                  <td><asp:Label ID="lbl_subtit" runat="server" /></td>
                  </tr>
              <tr><td><asp:Button Text="بازگشت به صفحه حساب من" ID="gotoaccount" runat="server" CssClass="btn btn-info" OnClick="gotoaccount_Click" /></td></tr>
</table>
           

<asp:Label runat="server" ID="lbl_notif" CssClass="label label-danger" />


    </div>
</div>

    </div>


 
</div>
    







</asp:Content>
