<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DateInput.ascx.cs" Inherits="narsShop.DateInput" ClientIDMode="Predictable" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajax_dateinput" %>
    <script type="text/javascript" language="javascript">
        
        function <%=this.ClientID %>_di_textvalidator() {

            var c_id = document.getElementById("<%= DI_TextBox.ClientID %>").value;
    
                narsweb.ChatServices.DI_chekvaliddate(c_id, <%=this.ClientID %>_di_DisplayResult);


            return;
        }

        function <%=this.ClientID %>_di_DisplayResult(result) {
            if (result == "1") {
                document.getElementById("<%= DI_TextBox.ClientID %>").style.color = "black"
                document.getElementById("<%= DI_TextBox.ClientID %>").setCustomValidity('');
            } else {
                document.getElementById("<%= DI_TextBox.ClientID %>").style. color = "red";
                document.getElementById("<%= DI_TextBox.ClientID %>").setCustomValidity('تاریخ نامعتبر')
                document.getElementById("<%= DI_TextBox.ClientID %>").focus();
            }

            }
        

    </script>

<!--    <table dir="rtl" cellpadding=0 cellspacing=0 bgcolor="Gainsboro"><tr>
        <td style="border-top-width: thin; border-bottom-width: thin; border-color: #808080; border-style: inset inset inset inset; border-right-width: thin;border-left-width: thin;">
        
        <span dir="ltr"> --><div style="width:100%">
<asp:TextBox id="DI_TextBox" runat="server" type="text" CausesValidation="true" 
                 class="form form-control" onchange="<%=this.ClientID %>_cs_textvalidator()"
                 ClientIDMode="Predictable" style="width:auto"></asp:TextBox>
<!--                 </span> -->
<ajax_dateinput:MaskedEditExtender ID="DI_ME2"  AutoComplete="false" Mask="9999/99/99" MessageValidatorTip="true"  MaskType="Date"  
    InputDirection="LeftToRight" AcceptNegative="Left"  runat="server"  DisplayMoney="None" TargetControlID="DI_TextBox" ClientIDMode="Predictable" />
            
    </div>


<!--    </td>
        
    </tr>
    </table> -->
