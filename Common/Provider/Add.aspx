<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Add.aspx.cs" MasterPageFile="~/Common/Common.master"   Inherits="Common_Provider_Add" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<%@ Register src="~/Common/menu.ascx" tagname="menu" tagprefix="uc1" %>
<asp:Content ContentPlaceHolderID="MenuPlaceHolder"  ID="Menublk" runat="server">   
<uc1:menu ID="menu" runat="server" />
</asp:Content> 

<asp:Content ContentPlaceHolderID="ContentPlaceHolder"  ID="Contentblk" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        <Scripts>
<asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.Core.js"></asp:ScriptReference>
<asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQuery.js"></asp:ScriptReference>
<asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQueryInclude.js"></asp:ScriptReference>
<asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.Core.js"></asp:ScriptReference>
<asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQuery.js"></asp:ScriptReference>
<asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQueryInclude.js"></asp:ScriptReference>
</Scripts>
    </telerik:RadScriptManager>  
<div class="bodycontainer">

 <table class="submenu_wrapper" >
  <tr>
  <td class="pgHeading" >Add Providers</td>
  <td>
    <div class="buttons">      
       
         <asp:LinkButton ID="btnBrowse" runat="server" onclick="Pageaction_Click"> 
        <asp:Image ID="browseimg" runat="server" ImageUrl="~/images/Browse.ico"  /> 
        <b>Browse</b>
        
        </asp:LinkButton>
         </div>     
  </td>
  </tr>
  </table>  

 <div class="tabwrapper" >
            <div class="tabContent" >
    <table class="outter full">
    <tr>
        <td>
            <table class="section" border="0" cellpadding="4" cellspacing="3" width="100%">
               <%-- <tr height="40px">
                    <td colspan="2" class="Heading">Create Provider</td>
                </tr>--%>
                <tr>
                   <td colspan="2" class="note"><span style="color:#FF0000">Note:</span><br />
             <ul>
             <li>Field marked with <span style="color:#FF0000">*</span> are compulsory fields</li>                     
             </ul>
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="required" />
             </td>
                </tr>               
            <tr><td align="right" width="32%">Name<span style="color:#FF0000">*</span></td><td width="68%">
            <asp:TextBox ID="txtname" class="input"  runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="txtnameValidator" runat="server" Display="Dynamic" ControlToValidate="txtname" ErrorMessage="Name Required!" ValidationGroup="required">*</asp:RequiredFieldValidator>
            </td></tr>
            <tr><td align="right">Address</td><td><asp:TextBox ID="Pri_Address" runat="server" Width="350px"></asp:TextBox></td></tr>
            <tr><td align="right">City</td><td><asp:TextBox ID="Pri_City" runat="server" ></asp:TextBox></td></tr>           
            <tr><td align="right">State</td><td><asp:TextBox ID="Pri_State" runat="server" CssClass="uptext" Width="20px"></asp:TextBox></td></tr>
            <tr><td align="right">Zip</td><td><asp:TextBox ID="Pri_Zip" runat="server" Width="40px"></asp:TextBox></td></tr>
            <tr><td align="right">Zip+4</td><td><asp:TextBox ID="Pri_Zip_Plus4" runat="server" Width="40px"></asp:TextBox></td></tr>
                                
            <tr><td align="right">Phone</td><td><telerik:RadMaskedTextBox ID="Pri_Phone" runat="server" 
                    Mask="(###) ###-####" BorderStyle="None"></telerik:RadMaskedTextBox></td></tr>
            <tr><td align="right">Fax</td><td>  <telerik:RadMaskedTextBox ID="Pri_Fax" runat="server" 
                    Mask="(###) ###-####" BorderStyle="None"></telerik:RadMaskedTextBox></td></tr>
            <tr><td align="right">Email</td><td><asp:TextBox ID="Pri_Email" runat="server"></asp:TextBox></td></tr>
            </table>                  
        </td>
    </tr>
</table>
<div class="btn_bar">
<asp:Button ID="btn_Submit" CssClass="btnsubmit" runat="server" Text="Submit" CausesValidation="true" ValidationGroup="required" onclick="btn_Submit_Click"  />
</div> 
</div>
</div>
</div>
</asp:Content> 
