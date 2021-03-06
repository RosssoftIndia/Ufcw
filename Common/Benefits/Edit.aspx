﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Edit.aspx.cs" Inherits="Common_Benefits_Edit" MasterPageFile="~/Common/Common.master"  %>

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
   <td class="pgHeading" >Edit Benefits</td>
  <td>
 <div class="buttons">   
    <asp:LinkButton ID="btnview" runat="server" onclick="Pageaction_Click"> 
        <asp:Image ID="viewimg" runat="server" ImageUrl="~/images/view.ico"  /> 
        <b>view</b>
        </asp:LinkButton>   
        
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
                <%--<tr height="40px">
                    <td colspan="2" class="Heading">Create Account</td>
                </tr>--%>
                <tr>
                              <td colspan="2" class="note"><span style="color:#FF0000">Note:</span><br />
             <ul>
             <li>Field marked with <span style="color:#FF0000">*</span> are compulsory fields</li>                     
             </ul>
                <asp:ValidationSummary ID="ValidationSummary1" runat="server"  ValidationGroup="required" />
             </td>
                </tr>
                <tr height="10px">
                    <td colspan="2"></td>
                </tr>
                <tr>
                    <td align="right"  width="32%">Name</td>
                    <td width="68%"><asp:TextBox ID="txtname" class="input"  runat="server"></asp:TextBox></td>
                </tr>
                 <tr>
                    <td align="right" >Provider<span style="color:#FF0000">*</span></td>
                    <td>
                     <telerik:RadComboBox ID="Drp_Provider" Width="220px" runat="server"></telerik:RadComboBox>     
                     <asp:CompareValidator runat="server" ID="ProviderValidator" ValueToCompare="Select"  Operator="NotEqual" ControlToValidate="Drp_Provider" ErrorMessage="Provider Required!" ValidationGroup="required" />                               
</td>
                </tr>    
                <tr>
                   <td align="right" valign="top" >Description</td>
                   <td><asp:TextBox ID="txtdescription" class="input" TextMode="MultiLine" Width="300px"  Height="100px"   runat="server"></asp:TextBox></td>
                </tr>                
                     
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
