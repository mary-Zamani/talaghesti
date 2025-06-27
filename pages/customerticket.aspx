<%@ Register TagPrefix="uc" TagName="CustomerSidebar" Src="~/UDC/CustomerSidebar.ascx" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="customerticket.aspx.cs" Inherits="narsShop.pages.customerticket" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <!-- Dashboard Section Start -->
    <div class="dashboard-section section-padding fix">
        <div class="container">
            <div class="row">
             <uc:CustomerSidebar ID="CustomerSidebar1" runat="server" />
                <div class="col-xl-9">
                     
                 <div class="order-history2">
                            <h3> <asp:Label ID="lbl_title1" runat="server" Text="ثبت تیکت پشتیبانی:" /></h3>
                        <h5>
                            <asp:Label ID="lbl_subtit" runat="server" Text="ثبت تیکت پشتیبانی:" /></h5>
                        <table width="100%" >
              <tr>
                  <td><asp:Label ID="Label1" runat="server" /></td>
                  </tr>
          
              <tr>
                      <td><asp:TextBox TextMode="MultiLine" runat="server" ID="txt_cnt" CssClass="form-control" /></td>
              </tr>
              <tr style="text-align:center">
                  <td><asp:Button ID="Btn_save"  runat="server" Text=" ارسال " OnClick="Btn_save_Click" CssClass="theme-btn style6"  /> </td>

                 </tr>
                  <tr><td>
                      <asp:Label ID="lbl_tbl" runat="server"></asp:Label>
        </td></tr>
          </table>
           

<asp:Label runat="server" ID="lbl_notif" CssClass="label label-danger" />
                     </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
