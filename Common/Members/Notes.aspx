<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Notes.aspx.cs" Inherits="Common_Members_Notes" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
       <link rel="stylesheet" type="text/css" href="../../css/bootstrap.min.css"/>
    <link rel="stylesheet" type="text/css" href="../../css/style.css"/>
    <script type="text/javascript">
        function OnRowSelected(sender, eventArgs) {
            var grid = sender;
            var MasterTable = grid.get_masterTableView(); var row = MasterTable.get_dataItems()[eventArgs.get_itemIndexHierarchical()];
            var cell = MasterTable.getCellByColumnUniqueName(row, "Note");
            document.getElementById('txtNotes').innerHTML = cell.innerHTML;
            document.getElementById('txtNotes').setAttribute('disabled', true);
            document.getElementById('submit').setAttribute('disabled', true);
            var Checkbox = $telerik.findElement(grid.MasterTableView.get_dataItems()[eventArgs.get_itemIndexHierarchical()].get_element(), "chk_Flag").checked;
            document.getElementById("chkFlag").checked = Checkbox;
            document.getElementById("chkFlag").setAttribute('disabled', true);
            
        }
    
</script>
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
      <table style="width:800px;height:600px;padding:10px" >
      <tr>
       <td style="margin:5px;">
                <asp:TextBox ID="txtNotes" runat="server" TextMode="MultiLine" Width="99%" Height="200px"  ></asp:TextBox>
                <asp:CheckBox ID="chkFlag" runat="server" Text="Flag" TextAlign="Right"></asp:CheckBox>
                 <asp:Button ID="submit" CssClass="btnsubmit" runat="server" Text="Submit" 
                    onclick="submit_Click" />
                    <asp:Button ID="clear" CssClass="btnsubmit" runat="server" Text="Clear" 
                    onclick="clear_Click" />
                </td>
                
      </tr>
                <tr>
                <td style="margin:5px;vertical-align:top">                
                     <telerik:RadGrid ID="BrowsegridNotes" runat="server" CellSpacing="0" Height="200px" AllowAutomaticDeletes="true"
                        GridLines="None" onneeddatasource="BrowsegridNotes_NeedDataSource" ondeletecommand="BrowsegridNotes_DeleteCommand">
                        <ClientSettings EnableRowHoverStyle="true">
            <Scrolling AllowScroll="true" UseStaticHeaders="true" />
            <ClientEvents OnRowSelected="OnRowSelected" />
            <Selecting AllowRowSelect="true" />
        </ClientSettings>
                    <MasterTableView AutoGenerateColumns="False" TableLayout="Fixed" >
                        <CommandItemSettings ExportToPdfText="Export to PDF" />
                        <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column" 
                            Visible="True">
                            <HeaderStyle Width="20px" />
                        </RowIndicatorColumn>
                        <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column" 
                            Visible="True">
                            <HeaderStyle Width="20px" />
                        </ExpandCollapseColumn>
                        <Columns>
                        <telerik:GridTemplateColumn HeaderText="RecordID" Visible="false">
                            <ItemTemplate>
                            <asp:Label ID="lblNoteRecord" runat="server" Text='<%#Eval("RecordID") %>'></asp:Label>
                            </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn DataField="Note" 
                                FilterControlAltText="Filter Note column" HeaderText="Note" 
                                SortExpression="Note" UniqueName="Note">
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn HeaderText="Flag">
                            <ItemTemplate>
                            <asp:CheckBox ID="chk_Flag" runat="server" Checked='<%# bool.Parse((Eval("Flag")).ToString()) %>' />
                            </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn DataField="CreatedBy" 
                                FilterControlAltText="Filter CreatedBy column" HeaderText="CreatedBy" 
                                SortExpression="CreatedBy" UniqueName="CreatedBy">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CreateDate" DataType="System.DateTime" 
                                FilterControlAltText="Filter CreateDate column" HeaderText="Date" 
                                SortExpression="CreateDate" UniqueName="CreateDate">
                            </telerik:GridBoundColumn>
                            <telerik:GridButtonColumn UniqueName="DeleteCol" Text="Delete"  ButtonType="ImageButton" ButtonCssClass="del-icon" ImageUrl="~/img/glyphicons-halflings.png"  CommandName="Delete" ></telerik:GridButtonColumn> 
                        </Columns>
                        <EditFormSettings>
                            <EditColumn FilterControlAltText="Filter EditCommandColumn column">
                            </EditColumn>
                        </EditFormSettings>
                    </MasterTableView>
                    <FilterMenu EnableImageSprites="False">
                    </FilterMenu>
                </telerik:RadGrid>                   
                </td>               
                </tr>
                <tr>
                <td><asp:Button ID="Update_btn" CssClass="btnsubmit" runat="server" Text="Update" 
                    onclick="Update_btn_Click" /></td>
                </tr>
                </table>
                    
    </div>
    </form>
</body>
</html>
