<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Add.aspx.cs" MasterPageFile="~/Admin/Admin.master"   Inherits="Admin_Menu_Add" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<%@ Register src="~/Admin/menu.ascx" tagname="menu" tagprefix="uc1" %>
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
<div class="bodycontainer animate_fadeInDownBig">
 <div class="tabwrapper" >
            <div class="tabContent" >
    <table class="outter full">
    <tr>
        <td>
            <table class="section" border="0" cellpadding="4" cellspacing="3" width="100%">
                <tr height="40px">
                    <td colspan="2" class="Heading">Create Menu</td>
                </tr>
                <tr>
                              <td colspan="2" class="note"><span style="color:#FF0000">Note:</span><br />
             <ul>
             <li>Field marked with <span style="color:#FF0000">*</span> are compulsory fields</li>                     
             </ul>
                <asp:ValidationSummary ID="ValidationSummary1" runat="server"  />
             </td>
                </tr>
                <tr height="10px">
                    <td colspan="2"></td>
                </tr>
                <tr>
                    <td align="right"  width="32%">Name<span style="color:#FF0000">*</span></td>
                    <td width="68%"><asp:TextBox ID="txtname" class="input"  runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="txtnameValidator" runat="server" Display="Dynamic"
ControlToValidate="txtname" ErrorMessage="Name Required!">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                   <td align="right" >Url<span style="color:#FF0000">*</span></td>
                   <td><asp:TextBox ID="txturl" class="input"  runat="server"></asp:TextBox>
                   <asp:RequiredFieldValidator ID="txturlValidator" runat="server" Display="Dynamic"
ControlToValidate="txturl" ErrorMessage="Url Required!">*</asp:RequiredFieldValidator>
</td>
                </tr>
                <tr>
                    <td align="right" >Role<span style="color:#FF0000">*</span></td>
                    <td>      <telerik:RadComboBox ID="Drp_Role" Width="220px" runat="server"  AutoPostBack="True" onselectedindexchanged="Drp_Role_SelectedIndexChanged" ></telerik:RadComboBox>     
                     <asp:CompareValidator runat="server" ID="RoleValidator" ValueToCompare="Select"  Operator="NotEqual" ControlToValidate="Drp_Role" Text="*"  ErrorMessage="Role Required!" />                               
 
</td>
                </tr>   
                <tr>
                   <td align="right" >Priority<span style="color:#FF0000">*</span></td>
                   <td><asp:TextBox ID="txtpriority" class="input"  runat="server"></asp:TextBox>
                   <%--<asp:RequiredFieldValidator ID="txtpriorityValidator" runat="server" Display="Dynamic"
ControlToValidate="txtpriority" ErrorMessage="Priority Required!">*</asp:RequiredFieldValidator>--%>
</td>
                </tr>
                              
            </table>
        </td>
    </tr>
</table>
<div class="btn_bar">
<asp:Button ID="btn_Submit" CssClass="btnsubmit" runat="server" Text="Submit" 
onclick="btn_Submit_Click"  />
</div> 
</div>
</div>
</div>
</asp:Content> 
