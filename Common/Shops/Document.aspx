<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Document.aspx.cs" Inherits="Common_Shops_Document" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="../../css/bootstrap.min.css"/>
    <link rel="stylesheet" type="text/css" href="../../css/style.css"/>
</head>
<body>
    <form id="form1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        <Scripts>
            <asp:ScriptReference Assembly="Telerik.Web.UI" 
                Name="Telerik.Web.UI.Common.Core.js">
            </asp:ScriptReference>
            <asp:ScriptReference Assembly="Telerik.Web.UI" 
                Name="Telerik.Web.UI.Common.jQuery.js">
            </asp:ScriptReference>
            <asp:ScriptReference Assembly="Telerik.Web.UI" 
                Name="Telerik.Web.UI.Common.jQueryInclude.js">
            </asp:ScriptReference>
        </Scripts>
    </telerik:RadScriptManager>
    <div>
      <table style="width:100%;padding:10px;" >
      <tr>
   <td class="pgHeading" >Documents</td>
   </tr>
  <td>
      <tr>
       <td style="margin:5px;">
       <telerik:RadGrid ID="Doc_Grid" runat="server" AllowSorting="false" 
                          AllowPaging="false" AutoGenerateColumns="false" 
                          onneeddatasource="Doc_Grid_NeedDataSource" OnItemDataBound="Doc_Grid_ItemDataBound">
                  <MasterTableView>
                  <Columns>
                    <telerik:GridTemplateColumn DataField="ReferenceID" UniqueName="ReferenceID" HeaderText="ReferenceID" Visible="false">
                    <ItemTemplate>
                    <asp:Label ID="ReferenceID" runat="server" Text='<%#Eval("ReferenceID")%>'></asp:Label>
                    </ItemTemplate>
                    </telerik:GridTemplateColumn>
                 
                  <telerik:GridBoundColumn DataField="Document" UniqueName="Document" HeaderText="FileName">
                  </telerik:GridBoundColumn>
                  <telerik:GridBoundColumn DataField="CreateDate" UniqueName="CreateDate" HeaderText="Date">
                  </telerik:GridBoundColumn>
                  <telerik:GridTemplateColumn UniqueName="RecordID" HeaderText="File">
                  <ItemTemplate>
                  <asp:HyperLink ID="File" ToolTip='<%#Eval("RecordID")%>' runat="server" ><span class="subitem"><asp:ImageMap ID="ImgHyper" runat="server"></asp:ImageMap></span></asp:HyperLink>
                  </ItemTemplate>
                  </telerik:GridTemplateColumn>
                  <telerik:GridTemplateColumn Visible="false" UniqueName="Extension">
                 <ItemTemplate>
                 <asp:Label ID="FileExt" runat="server" Text='<%#Eval("Extension")%>'></asp:Label>
                 </ItemTemplate>
                  </telerik:GridTemplateColumn>
                  </Columns>
                  </MasterTableView>             
                  </telerik:RadGrid>
       </td>
       </tr>
       </table>
       </div>
    </form>
</body>
</html>
