<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="product_old.aspx.cs" Inherits="narsShop.product_old"  MasterPageFile="~/mst.Master"%>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ MasterType VirtualPath="~/mst.Master" %> 


    <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <script type="text/javascript" language="javascript">


       function rs_change(pishpardakht, id) {
           var mabkol = document.getElementById("kol_" + id).innerHTML;
           document.getElementById("pishp_" + id).innerText = Number(pishpardakht).toLocaleString();
           document.getElementById("ghest_" + id).innerText = (((mabkol - Number(pishpardakht)) * 1.25) / 5).toLocaleString();
           
           document.getElementById("hid_pishp").value = Number(pishpardakht);
           document.getElementById("hid_ghest").value = (((mabkol - pishpardakht) * 1.25) / 5);

       }
   </script>

<section class="women-banner spad">
  <div class="container-fluid">
      <div class="row">       
                    <div class="col-lg-5 offset-lg-1">
                        <div class="pi-pic">
                        <asp:Image runat="server" ID="productimage" />
                            <asp:label runat="server" ID="firstetiket" />
                            </div>
 </div>
          <div class="col-lg-6 col-sm-12" style="overflow:scroll">
              <div class="filter-control" style="text-align:center">
<h2><asp:Label runat="server" ID="Lbl_title" /></h2>
                  <table width="100%">
                      <tr>
                          <td>انتخاب سایز</td>
                          <td>انتخاب رنگ</td>
                          <td>تعداد</td>
                      </tr>
                      <tr>
                          <td><asp:DropDownList runat="server" ID="Drp_size"   CssClass="form-control dropdown dropdown-backdrop" AutoPostBack="true" OnSelectedIndexChanged="drp_size_SelectedIndexChanged"/></td>
                          <td><asp:DropDownList runat="server" ID="Drp_color"  CssClass="form-control dropdown dropdown-backdrop" AutoPostBack="true" OnSelectedIndexChanged="drp_size_SelectedIndexChanged"/></td>
                          <td><asp:DropDownList runat="server" ID="Drp_count"  CssClass="form-control dropdown dropdown-backdrop" AutoPostBack="true" OnSelectedIndexChanged="drp_size_SelectedIndexChanged"/></td>
                      </tr></table>
              </div>
<asp:Label runat="server" ID="lbl_product" />
    </div>
       

      </div>
  </div>
</section>
     
<asp:HiddenField ID="hid_pishp" runat="server" ClientIDMode="Static" />
<asp:HiddenField ID="hid_ghest" runat="server" ClientIDMode="Static" />


<asp:Button ID="btn_add" runat="server" Visible="false" />


</asp:Content>
