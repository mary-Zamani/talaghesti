<%@ Page Language="C#" MasterPageFile="~/admin/mysite0.Master" AutoEventWireup="true" CodeBehind="list_akhbar_add.aspx.cs" Inherits="narsShop.admin.list_akhbar_add" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>

<%@ MasterType VirtualPath="~/admin/mysite0.Master" %> 


    <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
<table width="100%">
<tr>
    <td>نوع</td>
    
<td>
    <asp:DropDownList ID="Drp_grouh" runat="server" CssClass="form-control">
        <asp:ListItem Text="بلاگ" Value="B"></asp:ListItem>
        <asp:ListItem Text="درباره ما" Value="U"></asp:ListItem>
        <asp:ListItem Text="قوانین و مقررات" Value="L"></asp:ListItem>
        <asp:ListItem Text="راهنمای کاربران" Value="H"></asp:ListItem>
        
    </asp:DropDownList>
    </td>
</tr>
<tr>
    <td>عنوان</td>
<td>
    <asp:TextBox ID="Txt_onvan" runat="server" MaxLength="100" TabIndex="2"  Width="457px" CssClass="form-control"></asp:TextBox>
    </td>
</tr>
<tr>
    <td>شرح</td>
    </tr>
    <tr>
<td colspan="2"> 
    <telerik:RadEditor ID="RadEditor1" Runat="server" Width="100%">
        <FontSizes>
            <telerik:EditorFontSize Value="8" />
            <telerik:EditorFontSize Value="10" />
            <telerik:EditorFontSize Value="12" />
            <telerik:EditorFontSize Value="14" />
        </FontSizes>
        <FontNames>
            <telerik:EditorFont Value="IranSans" />
            <telerik:EditorFont Value="Tahoma" />
            <telerik:EditorFont Value="Arial" />
        </FontNames>
        <CssClasses>
            <telerik:EditorCssClass Name="primary" Value="" />
            <telerik:EditorCssClass Name="warning" Value="" />
            <telerik:EditorCssClass Name="danger" Value="" />
        </CssClasses>
        <Tools>
            <telerik:EditorToolGroup Tag="MainToolbar">
                <telerik:EditorTool Name="XhtmlValidator" />
                <telerik:EditorTool Name="PageProperties" />
                <telerik:EditorTool Name="StyleBuilder" />
                <telerik:EditorTool Name="FormatCodeBlock" />
                <telerik:EditorSeparator />
                <telerik:EditorTool Name="InsertImage" />
                <telerik:EditorTool Name="InsertLink" />
                <telerik:EditorTool Name="InsertTableLight" />
                <telerik:EditorSeparator />
                <telerik:EditorToolStrip Name="InsertFormElement">
                </telerik:EditorToolStrip>
                <telerik:EditorTool Name="InsertFormForm" />
                <telerik:EditorTool Name="InsertFormButton" />
                <telerik:EditorTool Name="InsertFormCheckbox" />
                <telerik:EditorTool Name="InsertFormHidden" />
                <telerik:EditorTool Name="InsertFormPassword" />
                <telerik:EditorTool Name="InsertFormRadio" />
                <telerik:EditorTool Name="InsertFormReset" />
                <telerik:EditorTool Name="InsertFormSelect" />
                <telerik:EditorTool Name="InsertFormSubmit" />
                <telerik:EditorTool Name="InsertFormTextarea" />
                <telerik:EditorTool Name="InsertFormText" />
                <telerik:EditorSeparator />
                <telerik:EditorTool Name="StripAll" />
                <telerik:EditorTool Name="StripCss" />
                <telerik:EditorTool Name="StripFont" />
                <telerik:EditorTool Name="StripSpan" />
                <telerik:EditorTool Name="StripWord" />
                <telerik:EditorToolStrip Name="FormatStripper">
                </telerik:EditorToolStrip>
            </telerik:EditorToolGroup>
            <telerik:EditorToolGroup Tag="InsertToolbar">
                <telerik:EditorTool Name="AjaxSpellCheck" />
                <telerik:EditorTool Name="ImageManager" ShortCut="CTRL+M" />
                <telerik:EditorTool Name="SetImageProperties" />
                <telerik:EditorTool Name="ImageMapDialog" />
                <telerik:EditorSeparator />
                <telerik:EditorTool Name="FlashManager" />
                <telerik:EditorTool Name="MediaManager" />
                <telerik:EditorTool Name="InsertExternalVideo" />
                <telerik:EditorSeparator />
                <telerik:EditorTool Name="DocumentManager" />
                <telerik:EditorTool Name="TemplateManager" />
                <telerik:EditorTool Name="SilverlightManager" />
                <telerik:EditorSeparator />
                <telerik:EditorToolStrip Name="InsertTable">
                </telerik:EditorToolStrip>
                <telerik:EditorTool Name="InsertRowAbove" />
                <telerik:EditorTool Name="InsertRowBelow" />
                <telerik:EditorTool Name="DeleteRow" />
                <telerik:EditorTool Name="InsertColumnLeft" />
                <telerik:EditorTool Name="InsertColumnRight" />
                <telerik:EditorTool Name="DeleteColumn" />
                <telerik:EditorSeparator />
                <telerik:EditorTool Name="MergeColumns" />
                <telerik:EditorTool Name="MergeRows" />
                <telerik:EditorSeparator />
                <telerik:EditorTool Name="SplitCell" />
                <telerik:EditorTool Name="SplitCellHorizontal" />
                <telerik:EditorTool Name="DeleteCell" />
                <telerik:EditorTool Name="SetCellProperties" />
                <telerik:EditorTool Name="SetTableProperties" />
                <telerik:EditorSeparator />
                <telerik:EditorSplitButton Name="InsertSymbol">
                </telerik:EditorSplitButton>
            </telerik:EditorToolGroup>
            <telerik:EditorToolGroup>
                <telerik:EditorSplitButton Name="Undo">
                </telerik:EditorSplitButton>
                <telerik:EditorSplitButton Name="Redo">
                </telerik:EditorSplitButton>
                <telerik:EditorSeparator />
                <telerik:EditorTool Name="Cut" />
                <telerik:EditorTool Name="Copy" />
                <telerik:EditorTool Name="Paste" ShortCut="CTRL+!" />
                <telerik:EditorTool Name="PasteMarkdown" />
                <telerik:EditorSeparator />
                <telerik:EditorTool Name="PasteFromWord" />
                <telerik:EditorTool Name="PasteFromWordNoFontsNoSizes" />
                <telerik:EditorTool Name="PastePlainText" />
                <telerik:EditorTool Name="PasteAsHtml" />
                <telerik:EditorTool Name="PasteHtml" />
                <telerik:EditorSeparator />
                <telerik:EditorTool Name="Print" />
                <telerik:EditorTool Name="FindAndReplace" />
                <telerik:EditorTool Name="SelectAll" />
                <telerik:EditorSeparator />
                <telerik:EditorTool Name="InsertGroupbox" />
                <telerik:EditorTool Name="InsertParagraph" />
                <telerik:EditorTool Name="InsertHorizontalRule" />
                <telerik:EditorSplitButton Name="InsertSnippet">
                </telerik:EditorSplitButton>
                <telerik:EditorSeparator />
                <telerik:EditorTool Name="InsertDate" />
                <telerik:EditorTool Name="InsertTime" />
                <telerik:EditorSeparator />
                <telerik:EditorTool Name="AboutDialog" />
                <telerik:EditorTool Name="Help" />
                <telerik:EditorTool Name="ToggleScreenMode" />
                <telerik:EditorTool Name="CSDialog" />
            </telerik:EditorToolGroup>
            <telerik:EditorToolGroup Tag="Formatting">
                <telerik:EditorTool Name="Bold" />
                <telerik:EditorTool Name="Italic" />
                <telerik:EditorTool Name="Underline" />
                <telerik:EditorTool Name="StrikeThrough" />
                <telerik:EditorSplitButton Name="ForeColor">
                </telerik:EditorSplitButton>
                <telerik:EditorSplitButton Name="BackColor">
                </telerik:EditorSplitButton>
                <telerik:EditorTool Name="FormatPainter" />
                <telerik:EditorSeparator />
                <telerik:EditorTool Name="JustifyLeft" />
                <telerik:EditorTool Name="JustifyCenter" />
                <telerik:EditorTool Name="JustifyRight" />
                <telerik:EditorTool Name="JustifyFull" />
                <telerik:EditorTool Name="JustifyNone" />
                <telerik:EditorSeparator />
                <telerik:EditorTool Name="Superscript" />
                <telerik:EditorTool Name="Subscript" />
                <telerik:EditorSeparator />
                <telerik:EditorTool Name="ConvertToLower" />
                <telerik:EditorTool Name="ConvertToUpper" />
                <telerik:EditorTool Name="Indent" />
                <telerik:EditorTool Name="Outdent" />
                <telerik:EditorTool Name="InsertOrderedList" />
                <telerik:EditorTool Name="InsertUnorderedList" />
                <telerik:EditorTool Name="AbsolutePosition" />
                <telerik:EditorTool Name="LinkManager" />
                <telerik:EditorTool Name="Unlink" />
                <telerik:EditorTool Name="SetLinkProperties" />
                <telerik:EditorTool Name="ToggleTableBorder" />
            </telerik:EditorToolGroup>
            <telerik:EditorToolGroup Tag="DropdownToolbar">
                <telerik:EditorDropDown Name="FontName">
                </telerik:EditorDropDown>
                <telerik:EditorDropDown Name="FontSize">
                </telerik:EditorDropDown>
                <telerik:EditorDropDown Name="RealFontSize">
                </telerik:EditorDropDown>
                <telerik:EditorDropDown Name="ApplyClass">
                </telerik:EditorDropDown>
                <telerik:EditorDropDown Name="InsertCustomLink">
                </telerik:EditorDropDown>
                <telerik:EditorDropDown Name="FormatBlock">
                </telerik:EditorDropDown>
                <telerik:EditorTool Name="FormatSets" />
                <telerik:EditorDropDown Name="Zoom">
                </telerik:EditorDropDown>
            </telerik:EditorToolGroup>
        </Tools>
        <Content>
</Content>

<TrackChangesSettings CanAcceptTrackChanges="False"></TrackChangesSettings>
    </telerik:RadEditor>
    </td>
</tr>

<tr >
    <td>تصویر</td>
<td>
    </td>
</tr>


    <tr>
        <td>
            <asp:Button ID="Button10" runat="server" Text="ذخیره" 
                onclick="Btn_save_Click" />
        </td>
        <td>
            <asp:Button ID="Button11" runat="server" Text="صرف نظر" OnClick="Button11_Click" />
        </td>
<td >
        &nbsp;</td>
    <td >
        &nbsp;</td>
</tr>
</table>





</asp:Content>