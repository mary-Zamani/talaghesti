<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="salelist.aspx.cs" Inherits="narsShop.salelist"  MasterPageFile="~/anj.Master"%>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ MasterType VirtualPath="~/anj.Master" %> 


    <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     
            <script type="text/javascript" language="javascript">

                function addtobasket(etiket) {

                    __doPostBack("btn_add", etiket);

                   
                }

            </script>

<section class="women-banner spad">
  <div class="container-fluid">
      <div class="row">       
          <div class="col-lg-12 offset-lg-1">
              <div class="filter-control">
<h2>محصولات</h2>
              </div>

<asp:Label runat="server" ID="lbl_productrow1" ClientIDMode="Static" />
    </div>
          
     
      </div>
  </div>
</section>

<asp:Button ID="btn_add" runat="server" />

</asp:Content>
