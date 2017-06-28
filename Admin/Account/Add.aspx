<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Add.aspx.cs" MasterPageFile="~/Admin/Admin.master"   Inherits="Admin_Account_Add" %>

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
                    <td colspan="2" class="Heading">Create Account</td>
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
                    <td align="right"  width="32%">First Name </td>
                    <td width="68%"><asp:TextBox ID="txtfirstname" class="input"  runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                   <td align="right" >Last Name </td>
                   <td><asp:TextBox ID="txtlastname" class="input"  runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                   <td align="right" >E-Mail ID </td>
                   <td><asp:TextBox ID="txtemail" class="input"  runat="server"></asp:TextBox></td>
                </tr> 
                  <tr>
                    <td align="right" >Username<span style="color:#FF0000">*</span></td>
                    <td>
                        <asp:TextBox ID="txtusername" runat="server" ></asp:TextBox>
                        <asp:RequiredFieldValidator ID="txtusernameValidator" runat="server" Display="Dynamic"
ControlToValidate="txtusername" ErrorMessage="Username Required!">*</asp:RequiredFieldValidator></td>
                </tr>
                 <tr>
                    <td align="right" >Password<span style="color:#FF0000">*</span></td>
                    <td><telerik:RadTextBox ID="txtpassword" runat="server" Height="25px" 
                            TextMode="Password" LabelWidth="98px" Width="270px">
    <PasswordStrengthSettings ShowIndicator="true" IndicatorWidth="50px" />
</telerik:RadTextBox>
<asp:RequiredFieldValidator ID="txtpasswordValidator" runat="server" Display="Dynamic"
ControlToValidate="txtpassword" ErrorMessage="Password Required!">*</asp:RequiredFieldValidator>
</td>
                </tr>   
                 <tr>
                    <td align="right" >BirthDate </td>
                    <td> 
                        <telerik:RadDatePicker ID="Birthdate" runat="server" Width="245px" Height="25px"    ShowPopupOnFocus="True">
<Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"></Calendar>

<DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>

<DateInput DisplayDateFormat="M/d/yyyy" DateFormat="M/d/yyyy" LabelWidth="40%"></DateInput>
                        </telerik:RadDatePicker>
                    </td>
                </tr>   
                 <tr>
                    <td align="right" >Role<span style="color:#FF0000">*</span></td>
                    <td>
                      <telerik:RadComboBox ID="Drp_Role" Width="220px" runat="server"></telerik:RadComboBox>     
                     <asp:CompareValidator runat="server" ID="RoleValidator" ValueToCompare="Select"  Operator="NotEqual" ControlToValidate="Drp_Role" Text="*"  ErrorMessage="Role Required!" />                               
 
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
