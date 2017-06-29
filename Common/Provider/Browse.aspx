<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Browse.aspx.cs" MasterPageFile="~/Common/Common.master"   Inherits="Common_Provider_Browse" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<%@ Register src="~/Common/menu.ascx" tagname="menu" tagprefix="uc1" %>

<asp:Content ContentPlaceHolderID="MenuPlaceHolder"  ID="Menublk" runat="server">   
<uc1:menu ID="menu" runat="server" />
</asp:Content> 


<asp:Content ContentPlaceHolderID="ContentPlaceHolder"  ID="Contentblk" runat="server">
    <telerik:RadStyleSheetManager ID="RadStyleSheetManager1" runat="server">
</telerik:RadStyleSheetManager>
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
   <td class="pgHeading" >Browse Providers</td>
  <td>
    <div class="buttons">   
    <asp:LinkButton ID="btnAdd" runat="server" onclick="Pageaction_Click"> 
       <asp:Image ID="addimg" runat="server" ImageUrl="~/images/Add.ico"  /> 
     <b>Add</b>        
        </asp:LinkButton>
 
</div>     
  </td>
  </tr>
  </table>           
     <div class="tabwrapper" >
            <div class="tabContent" >
            <table class="outter full">             
            <tr><td>
            <telerik:RadGrid ID="BrowseProviders" runat="server" CellSpacing="0" 
GridLines="None"  AllowPaging="True" PageSize="10" 
                    onneeddatasource="BrowseProviders_NeedDataSource"  ondeletecommand="BrowseProviders_DeleteCommand"              >
                     <PagerStyle Mode="NextPrevAndNumeric" Position="TopAndBottom"  ></PagerStyle>
<ExportSettings HideStructureColumns="true" Excel-Format="ExcelML"  ExportOnlyData="true" IgnorePaging="true"  FileName="ActiveMembers" ></ExportSettings>
<ClientSettings EnableRowHoverStyle="true"></ClientSettings> 
<MasterTableView AutoGenerateColumns="False" DataKeyNames="RecordID" TableLayout="Fixed" CommandItemDisplay="Top" EditMode="InPlace"  CommandItemSettings-ShowAddNewRecordButton="False">
<CommandItemSettings ShowExportToExcelButton="true"></CommandItemSettings>
<Columns>
<telerik:GridBoundColumn DataField="RecordID" DataType="System.Int32" 
FilterControlAltText="Filter RecordID column" HeaderText="RecordID" 
ReadOnly="True" SortExpression="RecordID" UniqueName="RecordID" Visible="false" >
</telerik:GridBoundColumn> 
<telerik:GridBoundColumn DataField="Name" ReadOnly="True"
FilterControlAltText="Filter Name column" HeaderText="Name" 
SortExpression="Name" UniqueName="Name" >
</telerik:GridBoundColumn>  
<telerik:GridBoundColumn DataField="Pri_City" ReadOnly="True"
FilterControlAltText="Filter Pri_City column" HeaderText="City" 
SortExpression="Pri_City" UniqueName="Pri_City">
</telerik:GridBoundColumn>
<telerik:GridBoundColumn DataField="Pri_State" ReadOnly="True"
FilterControlAltText="Filter Pri_State column" HeaderText="State" 
SortExpression="Pri_State" UniqueName="Pri_State">
</telerik:GridBoundColumn> 
<telerik:GridBoundColumn DataField="Pri_Zip" ReadOnly="True"
FilterControlAltText="Filter Pri_Zip column" HeaderText="ZipCode" 
SortExpression="Pri_Zip" UniqueName="Pri_Zip">
</telerik:GridBoundColumn> 
<telerik:GridTemplateColumn DataField="Pri_Phone" HeaderText="Phone" ReadOnly="true" FilterControlAltText="Filter Pri_Phone column" SortExpression="Pri_Phone" UniqueName="Pri_Phone">
<ItemTemplate>
<telerik:RadMaskedTextBox ID="Pri_Phone" runat="server" 
                    Mask="(###) ###-####" BorderStyle="None" Text='<%#Eval("Pri_Phone") %>' Enabled="false"></telerik:RadMaskedTextBox>
</ItemTemplate>
</telerik:GridTemplateColumn>
<telerik:GridTemplateColumn HeaderText="Action" >
<HeaderStyle Width="100px" CssClass="text-center" />
<ItemStyle Width="100px" CssClass="text-center" />
<ItemTemplate>
<asp:HyperLink ID="lnkview" NavigateUrl='<%#"~/Common/Provider/View.aspx?Rid="+DataBinder.Eval(Container.DataItem,"RecordID") %>' class="view-icon"   runat="server"></asp:HyperLink>
<asp:HyperLink ID="lnkedit" NavigateUrl='<%#"~/Common/Provider/Edit.aspx?Rid="+DataBinder.Eval(Container.DataItem,"RecordID") %>' class="edit-icon"   runat="server"></asp:HyperLink>
<asp:LinkButton ID="btndelete" Visible="false" runat="server" class="del-icon" CommandName="Delete" OnClientClick='<%# "javascript:return confirm(\"Are you sure want to delete this Provider and Benefits Related to this Provider ?\\n" + Eval("Name")+ "\");"%>' > </asp:LinkButton>
</ItemTemplate>

</telerik:GridTemplateColumn> 
</Columns>
</MasterTableView>
</telerik:RadGrid>
</td>
</tr>
</table>
</div>
</div>

             
</div> 


</asp:Content> 
