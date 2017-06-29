<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Add.aspx.cs" Inherits="Common_Members_Add" MasterPageFile="~/Common/Common.master" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<%@ Register src="~/Common/menu.ascx" tagname="menu" tagprefix="uc1" %>

<asp:Content ContentPlaceHolderID="MenuPlaceHolder"  ID="Menublk" runat="server">   
    <uc1:menu ID="menu" runat="server" />
</asp:Content> 


<asp:Content ContentPlaceHolderID="ContentPlaceHolder"  ID="Contentblk" runat="server">
 <script type="text/javascript">

     function DateSelected(sender, eventArgs) {
         var datepickerH = $find("<%=Hireddate.ClientID%>");
         var date = datepickerH.get_selectedDate();
         date.setDate(date.getDate() + 31);
        var datepicker = $find("<%=AffDate.ClientID%>");
        //var todayTriplet = [date.getFullYear(), date.getMonth() , date.getDate()];
        datepicker.set_selectedDate(date);
     }

     function OnClientEntryAddingHandler(sender, eventArgs) {
         alert("call");
         if (sender.get_entries().get_count() > 0) {
             eventArgs.set_cancel(true);
             alert("You can select only one entry");
         }

     }
     
</script>
    <script type="text/javascript">
	function onClientTabSelecting(sender, eventArgs)
	{
	    eventArgs.set_cancel(true);
	}
</script>

    <telerik:RadStyleSheetManager ID="RadStyleSheetManager1" runat="server">
</telerik:RadStyleSheetManager>
<asp:ScriptManager ID="ToolkitScriptManager1" runat="server">  
</asp:ScriptManager>
   <%-- <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        <Scripts>
<asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.Core.js"></asp:ScriptReference>
<asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQuery.js"></asp:ScriptReference>
<asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQueryInclude.js"></asp:ScriptReference>
<asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.Core.js"></asp:ScriptReference>
<asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQuery.js"></asp:ScriptReference>
<asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQueryInclude.js"></asp:ScriptReference>
</Scripts>
    </telerik:RadScriptManager>  --%>
    
    
 <div class="bodycontainer">
  <table class="submenu_wrapper" >
  <tr>
  <td class="pgHeading" >Add Members</td>
  <td>
    <div class="buttons">           
      <asp:LinkButton ID="btnBrowse" runat="server" onclick="Pageaction_Click"> 
        <asp:Image ID="browseimg" runat="server" ImageUrl="~/images/Browse.ico"  /> 
        <b>Browse</b>
        
        </asp:LinkButton>
         <asp:LinkButton ID="btnBack" runat="server" onclick="Pageaction_Click"> 
        <asp:Image ID="backimg" runat="server" ImageUrl="~/images/back.ico"  /> 
        <b>Back</b>
        </asp:LinkButton>     
</div>     
  </td>
  </tr>
  </table>  
  <telerik:RadTabStrip ID="RadTabStrip1" runat="server" Skin="Black" MultiPageID="RadMultiPage1"
            SelectedIndex="0" CssClass="tabStrip" OnClientTabSelecting="onClientTabSelecting">
            <Tabs>
              <telerik:RadTab Text="Information" >
                </telerik:RadTab>                                     
                  <telerik:RadTab Text="Benefits" >
                </telerik:RadTab>   
                   <telerik:RadTab Text="Dependents" >
                </telerik:RadTab>    
                <telerik:RadTab Text="Initiation Fee" >
                </telerik:RadTab>    
                 <telerik:RadTab Text="Authorization">
                </telerik:RadTab>        
            </Tabs>
        </telerik:RadTabStrip>
 <div class="tabwrapper" >
            <div class="tabContent" >                
               <div id="btnNotes" runat="server" style="float:right;margin:10px;" ><a href="#" onclick="openRadWin(); return false;" ><img alt="" class="note-icon"/>Note</a> </div>   
        <telerik:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="0" CssClass="multiPage">                  
            <telerik:RadPageView ID="RadPageView1" runat="server" > 
              <table class="outter full">  
            <tr>
            <td class="note"><span style="color:#FF0000">Note:</span><br />
             <ul>
             <li>Field marked with <span style="color:#FF0000">*</span> are compulsory fields</li>
             <li>Field marked with <span style="color:#00F500">*</span> are Automated fields</li>
             <li>Submit Information Tab to proceed with other tabs.</li>             
             </ul>
                <asp:ValidationSummary ID="ValidationSummary1" runat="server"  
                     ValidationGroup="Tab1" />
             </td>
            </tr>  
            <tr><td class="space" >
            
            </td></tr>  
            <tr>
            <td>
            <table class="section">	               
		    <tr><td style="width:100px">Members-ID<span style="color:#00F500">*</span></td><td>
		    <telerik:RadNumericTextBox ID="MemberID"  Width="220px" height="25px" runat="server" Type="Number" MinValue="0"  NumberFormat-DecimalDigits="0" NumberFormat-GroupSeparator="" Enabled="false" >
                            </telerik:RadNumericTextBox>
		    </td> 
		    </tr> 
		    <tr><td>SSN <span style="color:#FF0000">*</span></td>
            <td><telerik:RadMaskedTextBox Width="220px" height="25px" runat="server" ID="SSN" 
                    BorderStyle="None" Mask="###-##-####" AutoPostBack="true" ValidationGroup="Tab1" 
                    ontextchanged="SSN_TextChanged"></telerik:RadMaskedTextBox>
              <asp:RequiredFieldValidator ID="SSNValidator" runat="server" ControlToValidate="SSN" InitialValue="" Text="*" ErrorMessage="SSN Required!" ValidationGroup="Tab1" ></asp:RequiredFieldValidator>
               </td> </tr> 		
		    <tr><td> First Name <span style="color:#FF0000">*</span></td><td><asp:TextBox ID="firstname"  runat="server" Width="210px" CssClass="uptext" ></asp:TextBox>
		     <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="firstname" Text="*" ErrorMessage="First Name Required!" ValidationGroup="Tab1" ></asp:RequiredFieldValidator>
		    </td></tr>
			<tr><td> Last Name<span style="color:#FF0000">*</span></td>
			<td><asp:TextBox ID="lastname"  runat="server" Width="210px" CssClass="uptext" ></asp:TextBox>
			<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="lastname" Text="*" ErrorMessage="Last Name Required!" ValidationGroup="Tab1" ></asp:RequiredFieldValidator>
		
			</td></tr>	
			<tr><td>Initial</td>
			<td><asp:TextBox ID="initial"  runat="server" Width="20px" CssClass="uptext" ></asp:TextBox></td>
			</tr>
			<tr><td>DOB</td>                
            <td>
            <telerik:RadDatePicker ID="DOB" runat="server" Width="245px" Height="25px"    ShowPopupOnFocus="True" MinDate="1/1/1900">
<Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"></Calendar>

<DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>

<DateInput DisplayDateFormat="M/d/yyyy" DateFormat="M/d/yyyy" LabelWidth="40%"></DateInput>
                        </telerik:RadDatePicker>
            </td>

		</tr>
		<tr><td> Gender</td><td>
            <asp:RadioButtonList ID="Genderoption" runat="server" 
                RepeatDirection="Horizontal" CssClass="removestyle">
            <asp:ListItem Value="0" Selected="True">Male</asp:ListItem>  
            <asp:ListItem Value="1">Female</asp:ListItem>  
            </asp:RadioButtonList>
        </td></tr> 
        <tr><td style="width:100px">Shop<span style="color:#FF0000">*</span></td><td>
        
        <%--<input type="text" id="txtSearch" name="txtSearch" class="autosuggest" runat="server" />--%>
        <%--<telerik:RadAutoCompleteBox ID="Auto_Shop" runat="server"  InputType="Text" Width="280" DropDownWidth="280" Delimiter="" >        
        </telerik:RadAutoCompleteBox>--%>
        
		   <%--<telerik:RadComboBox ID="Drp_shop" Width="220px" runat="server" EnableAutomaticLoadOnDemand="True" ItemsPerRequest="10"
                                   ShowMoreResultsBox="true" EnableVirtualScrolling="true"></telerik:RadComboBox>--%>
		     <telerik:RadComboBox ID="Drp_shop" runat="server" Width="220px" Height="150px"
                                   EmptyMessage="Select"  DataTextField="Name"
                                   DataValueField="RecordID" EnableAutomaticLoadOnDemand="True" ItemsPerRequest="10"
                                   ShowMoreResultsBox="true" EnableVirtualScrolling="true">
                              </telerik:RadComboBox>
                              <asp:RequiredFieldValidator ID="ShopReq" runat="server" ControlToValidate="Drp_shop" InitialValue="Select" Text="*" ValidationGroup="Tab1" ErrorMessage="Shop Required!"></asp:RequiredFieldValidator>
		    
<%--<asp:TextBox runat="server" ID="txtSearch" CssClass="autosuggest" />--%>



		<%-- <ajaxToolkit:AutoCompleteExtender
                runat="server" 
                BehaviorID="AutoCompleteEx"
                ID="frm2_autoComplete1" 
                TargetControlID="Auto_txtShop"
                ServicePath="../City.asmx" 
                ServiceMethod="GetCompletionList"
                MinimumPrefixLength="1" 
                CompletionInterval="30"
                EnableCaching="false"
                CompletionSetCount="20"
                CompletionListCssClass="autocomplete_completionListElement" 
                CompletionListItemCssClass="autocomplete_listItem" 
                CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"                
                ><Animations>
                     <OnShow>
                        <Sequence>
                            
                            <OpacityAction Opacity="100" />
                            <HideAction Visible="true" />
                            
                            
                            <ScriptAction Script="
                                // Cache the size and setup the initial size
                                var behavior = $find('AutoCompleteEx');
                                if (!behavior._height) {
                                    var target = behavior.get_completionList();
                                    behavior._height = target.offsetHeight - 2;
                                    target.style.height = '0px';
                                }" />
                            
                            
                            <Parallel Duration=".4">
                                <FadeIn />
                                <Length PropertyKey="height" StartValue="0" EndValueScript="$find('AutoCompleteEx')._height" />
                            </Parallel>
                        </Sequence>
                    </OnShow>
                    <OnHide>
                        
                        <Parallel Duration=".4">
                            <FadeOut />
                            <Length PropertyKey="height" StartValueScript="$find('AutoCompleteEx')._height" EndValue="0" />
                        </Parallel>
                    </OnHide></Animations></ajaxToolkit:AutoCompleteExtender>--%>

            <%--<asp:CompareValidator runat="server" ID="shopValidator" ValueToCompare="Select"  Operator="NotEqual" ControlToValidate="Drp_shop" Text="*" ValidationGroup="Tab1" ErrorMessage="Shop Required!" /> --%>
		      </td> 
		    </tr>
		    <tr>
		    <td>
		    Hired Date<span style="color:#FF0000">*</span>
		    </td>
		    <td>
		     <telerik:RadDatePicker ID="Hireddate" runat="server" Width="245px" 
                    Height="25px"    ShowPopupOnFocus="True" MinDate="1/1/1900" 
                     >
                     <ClientEvents OnDateSelected="DateSelected" />
<Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x" ></Calendar>

<DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>

<DateInput DisplayDateFormat="M/d/yyyy" DateFormat="M/d/yyyy" LabelWidth="40%"></DateInput>
                        </telerik:RadDatePicker>
                        
		        <asp:Label ID="OrigHiredDate"  CssClass="OrigDate" runat="server" ></asp:Label>
                     <asp:RequiredFieldValidator ID="dateValidator" runat="server" ControlToValidate="Hireddate" Text="*" ErrorMessage="Hireddate Required!" ValidationGroup="Tab1" ></asp:RequiredFieldValidator>
		    </td>
		    </tr> 
		    
		     <tr>
		    <td>
		    Affiliation Date
		    </td>
		    <td>
		     <telerik:RadDatePicker ID="AffDate" runat="server" Width="245px" Height="25px"    ShowPopupOnFocus="True" MinDate="1/1/1900">
<Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"></Calendar>

<DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>

<DateInput DisplayDateFormat="M/d/yyyy" DateFormat="M/d/yyyy" LabelWidth="40%"></DateInput>
                        </telerik:RadDatePicker>
                        
	        
		    </td>
		    </tr> 
		    
		    </table> 	    
            </td>
            <td></td>
            </tr>  
             <tr>
            <td class="break" ></td>
            </tr>   
            <tr>
            <td>        
                <table class="section half"  >
            <tr><td colspan="2">Primary Address</td></tr>
            <tr><td style="width:100px">Address</td><td><asp:TextBox ID="Pri_Address" runat="server" Width="350px" CssClass="uptext"></asp:TextBox></td></tr>
            <tr><td>City</td><td><asp:TextBox ID="Pri_City" runat="server" CssClass="uptext" ></asp:TextBox></td></tr>           
            <tr><td colspan="2" style="padding-left: 0px;">
            <table class="section">
            <tr><td style="width:100px"></td><td>State</td><td>Zip</td><td>Zip+4</td></tr>
            <tr>
            <td></td>
            <td><asp:TextBox ID="Pri_State" runat="server" CssClass="uptext" Width="20px" ></asp:TextBox></td>
            <td><asp:TextBox ID="Pri_Zip" runat="server" Width="40px"></asp:TextBox></td>
            <td><asp:TextBox ID="Pri_Zip_Plus4" runat="server" Width="40px"></asp:TextBox></td>
            </tr>  
            </table> 
            </td></tr>           
            <tr><td>Phone</td><td><telerik:RadMaskedTextBox ID="Pri_Phone" runat="server" 
                    Mask="(###) ###-####" BorderStyle="None"></telerik:RadMaskedTextBox></td><td>Extn</td><td><asp:TextBox ID="Pri_Extn" runat="server"></asp:TextBox></td></tr>
            <tr><td>Alternate Phone</td><td><telerik:RadMaskedTextBox ID="Sec_Phone" runat="server" 
                    Mask="(###) ###-####" BorderStyle="None"></telerik:RadMaskedTextBox></td><td>Extn</td><td><asp:TextBox ID="Sec_Extn" runat="server"></asp:TextBox></td></tr>
            <tr style="visibility:hidden"><td>Fax</td><td>  <telerik:RadMaskedTextBox ID="Pri_Fax" runat="server" 
                    Mask="(###) ###-####" BorderStyle="None"></telerik:RadMaskedTextBox></td></tr>
            <tr><td>Email</td><td><asp:TextBox ID="Pri_Email" runat="server"></asp:TextBox></td></tr>
            </table>
                     
            <table class="section half" style="visibility:hidden">
            <tr><td colspan="2">Secondary Address</td></tr>
            <tr><td style="width:100px">Address</td><td><asp:TextBox ID="Sec_Address" runat="server" Width="350px"></asp:TextBox></td></tr>
            <tr><td>City</td><td><asp:TextBox ID="Sec_City" runat="server" ></asp:TextBox></td></tr>
            <tr><td colspan="2" style="padding-left: 0px;">
            <table class="section">
            <tr><td style="width:100px"></td><td>State</td><td>Zip</td><td>Zip+4</td></tr>
            <tr>
            <td></td>
            <td><asp:TextBox ID="Sec_State" runat="server" CssClass="uptext" Width="20px"></asp:TextBox></td>
            <td><asp:TextBox ID="Sec_Zip" runat="server" Width="40px"></asp:TextBox></td>
            <td><asp:TextBox ID="Sec_Zip_Plus4" runat="server" Width="40px"></asp:TextBox></td>          
            </tr>  
            </table>  
            </td> 
            </tr>    
            <tr><td>Phone</td><td><telerik:RadMaskedTextBox ID="Sec_Phone1" runat="server" 
                    Mask="(###) ###-####" BorderStyle="None"></telerik:RadMaskedTextBox></td></tr>
            <tr><td>Fax</td><td><telerik:RadMaskedTextBox ID="Sec_Fax" runat="server" 
                    Mask="(###) ###-####" BorderStyle="None"></telerik:RadMaskedTextBox></td></tr>
            <tr><td>EMail</td><td ><asp:TextBox ID="Sec_Email" runat="server"></asp:TextBox></td> </tr>               
            </table>
                    
            </td>
            </tr>  
               </table>              
              <div class="btn_bar">                                      
                    <asp:Button ID="btn_tab1" CssClass="btnsubmit" runat="server" Text="Next"  ValidationGroup="Tab1" CausesValidation="true" onclick="btn_tab1_Click"/>               
               </div>            
            </telerik:RadPageView>                
            <telerik:RadPageView ID="RadPageView2" runat="server" CssClass="pageViewEducation">             
                  <table class="outter full"> 
                  <tr>
                  <td>
                  <table width="100%" >
                  <tr>
                  <td style="width:350px;">
                  <b>Type:</b>
                  <asp:RadioButtonList ID="BenefitType" runat="server" 
                          RepeatDirection="Horizontal" CssClass="removestyle" AutoPostBack="true" 
                          onselectedindexchanged="BenefitType_SelectedIndexChanged"  >                      
                          <asp:ListItem Value="1">Full Benefits</asp:ListItem>
                           <asp:ListItem Value="2">Partial Benefits</asp:ListItem>                      
                          <asp:ListItem Value="0" Selected="True">No Benefits</asp:ListItem>                      
                      </asp:RadioButtonList>
                  </td>
                  <td></td>
                  <td style="width:150px;">
                 <b>Status:</b>
                       <asp:RadioButtonList ID="ApplicableTo" runat="server" 
                          RepeatDirection="Horizontal" CssClass="removestyle">                      
                          <asp:ListItem Value="0" Selected="True">Single</asp:ListItem>                                              
                          <asp:ListItem Value="1">Family</asp:ListItem>                      
                      </asp:RadioButtonList>                  
                  </td>
                  </tr>
                  
                  </table>             
                     
                      <br />
                  </td></tr>                       
               <tr><td>                 
                   <telerik:RadGrid runat="server" ID="Benefits" ShowStatusBar="True"    
                       AutoGenerateColumns="False"  GridLines="None" 
                      Width="100%" CellSpacing="0" onneeddatasource="Benefits_NeedDataSource" 
                       ondatabound="Benefits_DataBound">
                       <ClientSettings EnableRowHoverStyle="true"></ClientSettings> 
                       <MasterTableView AllowMultiColumnSorting="True"  Width="100%">         
            <CommandItemSettings ExportToPdfText="Export to PDF" />
            <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column" 
                Visible="True">
            </RowIndicatorColumn>
            <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column" 
                Visible="True">
            </ExpandCollapseColumn>
           
            <Columns>          
            <telerik:GridTemplateColumn DataField="Name" FilterControlAltText="Filter Name column"  SortExpression="Name" UniqueName="Name">
            <ItemTemplate>             
                <asp:CheckBox ID="Chk" runat="server" />
                  <asp:Label ID="lblBenefitID" runat="server" Text='<%#Eval("RecordID") %>' Visible="false" ></asp:Label>
            </ItemTemplate>           
            </telerik:GridTemplateColumn>  
            <telerik:GridTemplateColumn DataField="Name" FilterControlAltText="Filter Name column" HeaderText="Name" SortExpression="Name" UniqueName="Name">
            <ItemTemplate>             
            <asp:Label ID="lblName" runat="server" Text='<%#Eval("Name") %>'></asp:Label>
            </ItemTemplate>           
            </telerik:GridTemplateColumn>  
              <telerik:GridTemplateColumn DataField="Description" FilterControlAltText="Filter Name column" HeaderText="Description" SortExpression="Description" UniqueName="Description">
            <ItemTemplate>             
            <asp:Label ID="lblDescription" runat="server" Text='<%#Eval("Description") %>'></asp:Label>
            </ItemTemplate>          
            </telerik:GridTemplateColumn>  
            <telerik:GridTemplateColumn DataField="providedby" FilterControlAltText="Filter Name column" HeaderText="Provider" SortExpression="providedby" UniqueName="Description">
            <ItemTemplate>             
            <asp:Label ID="lblprovider" runat="server" Text='<%#Eval("providedby") %>'></asp:Label>
            </ItemTemplate>           
            </telerik:GridTemplateColumn>  
           <telerik:GridTemplateColumn DataField="Single_Partial" FilterControlAltText="Filter Single_Partial column" HeaderText="Single Partial" SortExpression="Single_Partial" UniqueName="Single_Partial">
            <ItemTemplate>  
             <telerik:RadNumericTextBox ID="WaitingPeriod"  Width="160px" height="25px" runat="server" Type="Number" MinValue="0"  NumberFormat-DecimalDigits="0" NumberFormat-GroupSeparator="">
            </telerik:RadNumericTextBox>  
            </ItemTemplate>         
            </telerik:GridTemplateColumn>   
            <telerik:GridTemplateColumn DataField="Single_FullTime" FilterControlAltText="Filter Single_FullTime column" HeaderText="Single FullTime" SortExpression="Single_FullTime" UniqueName="Single_FullTime">
            <ItemTemplate>             
            <telerik:RadNumericTextBox ID="WaitingPeriod1"  Width="160px" height="25px" runat="server" Type="Number" MinValue="0"  NumberFormat-DecimalDigits="0" NumberFormat-GroupSeparator="">
            </telerik:RadNumericTextBox> 
            </ItemTemplate>         
            </telerik:GridTemplateColumn>   
            <telerik:GridTemplateColumn DataField="Family_Partial" FilterControlAltText="Filter Family_Partial column" HeaderText="Family Partial" SortExpression="Family_Partial" UniqueName="Family_Partial">
            <ItemTemplate>             
            <telerik:RadNumericTextBox ID="WaitingPeriod2"  Width="160px" height="25px" runat="server" Type="Number" MinValue="0"  NumberFormat-DecimalDigits="0" NumberFormat-GroupSeparator="">
            </telerik:RadNumericTextBox> 
            </ItemTemplate>         
            </telerik:GridTemplateColumn>   
            <telerik:GridTemplateColumn DataField="Family_FullTime" FilterControlAltText="Filter Family_FullTime column" HeaderText="Family FullTime" SortExpression="Family_FullTime" UniqueName="Family_FullTime">
            <ItemTemplate>             
            <telerik:RadNumericTextBox ID="WaitingPeriod3"  Width="160px" height="25px" runat="server" Type="Number" MinValue="0"  NumberFormat-DecimalDigits="0" NumberFormat-GroupSeparator="">
            </telerik:RadNumericTextBox> 
            </ItemTemplate>         
            </telerik:GridTemplateColumn>     
            </Columns> 
            </MasterTableView>   
                   </telerik:RadGrid>
        </td></tr> <tr >
                  <td  style="padding-left:20px;" >
                  <hr />
                  <br />
                  <asp:Label ID="lblNA" runat="server" Text="N/A"></asp:Label> </td>
                  </tr>
                  <tr>
                   <td style="padding:0px;" >
                  <table id="Ratesection" runat="server"  class="outter full">                  
         <tr>
            <td class="break" ></td>
            </tr>  
                   <tr>
            <td class="break" ></td>
            </tr> 
               <tr><td>  <b>Rate:</b><br /></td></tr> 
            <tr>
            <td>
             <telerik:radgrid runat="server" ID="rategrid" ShowStatusBar="True"    AutoGenerateColumns="False"  GridLines="None" 
                      Width="100%" CellSpacing="0" onneeddatasource="rategrid_NeedDataSource" 
                       oninsertcommand="rategrid_InsertCommand" 
                       onupdatecommand="rategrid_UpdateCommand" 
                       ondeletecommand="rategrid_DeleteCommand" 
                       onitemdatabound="rategrid_ItemDataBound" >
         <PagerStyle Mode="NumericPages"></PagerStyle>
         <MasterTableView AllowMultiColumnSorting="True"  Width="100%"  EditMode="InPlace" CommandItemDisplay="Top" CommandItemSettings-ShowRefreshButton="False">         
        
            <CommandItemSettings ExportToPdfText="Export to PDF" />
            <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column" 
                Visible="True">
            </RowIndicatorColumn>
            <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column" 
                Visible="True">
            </ExpandCollapseColumn>
            <Columns> 
            <telerik:GridTemplateColumn HeaderText="Sno" UniqueName="Sno" Visible="false" >
            <ItemTemplate>           
             <asp:Label ID="lblReferenceID" runat="server" Text='<%#Eval("RecordID") %>' Visible="false" ></asp:Label>
            <asp:Label ID="lblSno" runat="server"></asp:Label>
            </ItemTemplate>
            <EditItemTemplate>        
            <asp:Label ID="lblRecordID" runat="server" Text='<%#Eval("RecordID") %>'  Visible="false" ></asp:Label>
             <asp:Label ID="lblReferenceID" runat="server"  Visible="false"></asp:Label>
            <asp:Label ID="lblSno" runat="server"></asp:Label>
            </EditItemTemplate> 
            </telerik:GridTemplateColumn> 
                <telerik:GridTemplateColumn SortExpression="Rate" HeaderText="Rate" HeaderButtonType="TextButton"
                    DataField="Rate" UniqueName="Rate">
                     <ItemTemplate>
            <asp:Label ID="lblDue_FullTime" runat="server" Text='<%#Eval("Rate") %>'></asp:Label>
            </ItemTemplate>
            <EditItemTemplate>
            <telerik:RadNumericTextBox ID="txtDue_FullTime"  Width="100px" height="25px" Text='<%#Eval("Rate") %>' runat="server" Type="Number" MinValue="0"  NumberFormat-DecimalDigits="2" NumberFormat-GroupSeparator=""></telerik:RadNumericTextBox>                                          
            </EditItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn SortExpression="Family" HeaderText="Family Rate" HeaderButtonType="TextButton"
                    DataField="Family" UniqueName="Family">
                     <ItemTemplate>
            <asp:Label ID="lblDue_PartTime" runat="server" Text='<%#Eval("Family") %>'></asp:Label>
            </ItemTemplate>
            <EditItemTemplate>
            <telerik:RadNumericTextBox ID="txtDue_PartTime"  Width="100px" height="25px" Text='<%#Eval("Family") %>' runat="server" Type="Number" MinValue="0"  NumberFormat-DecimalDigits="2" NumberFormat-GroupSeparator=""></telerik:RadNumericTextBox>                                                      
            </EditItemTemplate>
                    </telerik:GridTemplateColumn>
                      <telerik:GridTemplateColumn SortExpression="Fringe" HeaderText="Fringe Rate" HeaderButtonType="TextButton"
                    DataField="Fringe" UniqueName="Fringe">
                     <ItemTemplate>  
                       <asp:Label ID="lblInit_FullTime" runat="server" Text='<%#Eval("Fringe") %>'></asp:Label>                      
            </ItemTemplate>
            <EditItemTemplate>            
            <telerik:RadNumericTextBox ID="txtInit_FullTime"  Width="100px" height="25px" Text='<%#Eval("Fringe") %>' runat="server" Type="Number" MinValue="0"  NumberFormat-DecimalDigits="2" NumberFormat-GroupSeparator=""></telerik:RadNumericTextBox>                              
            </EditItemTemplate>
                </telerik:GridTemplateColumn>                
                    <telerik:GridTemplateColumn SortExpression="Effective_Date" HeaderText="Effective From" HeaderButtonType="TextButton"
                    DataField="Effective_Date" UniqueName="Effective_Date">
                     <ItemTemplate>
            <asp:Label ID="lblEffective_Date" runat="server" Text='<%#Eval("Effective_Date") %>'></asp:Label>
            </ItemTemplate>
            <EditItemTemplate>
                <telerik:RadDatePicker ID="txtEffective_Date" runat="server" MinDate="1/1/1900"  Height="25px"  DbSelectedDate='<%# Bind("Effective_Date") %>'  ShowPopupOnFocus="True">
<Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"></Calendar>

<DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>

<DateInput DisplayDateFormat="M/d/yyyy" DateFormat="M/d/yyyy" LabelWidth="40%"></DateInput>
                        </telerik:RadDatePicker>          
            </EditItemTemplate>
                    </telerik:GridTemplateColumn>
                       <telerik:GridTemplateColumn HeaderText="Action" >
        <ItemStyle Width="50px" />       
              <ItemTemplate>                  
                  <asp:LinkButton ID="btnEdit" CommandName="Edit"  CssClass="edit-icon"   runat="server"></asp:LinkButton>
                  <asp:LinkButton ID="btnDelete" CommandName="Delete" CssClass="del-icon"  OnClientClick='<%# "javascript:return confirm(\"Are you sure want to delete this Record ?"+ "\");"%>'  runat="server"></asp:LinkButton>
              </ItemTemplate> 
              <EditItemTemplate>
               <asp:LinkButton ID="btnEdit" CommandName="Update"   CssClass="ok-icon"  runat="server"></asp:LinkButton>
               <asp:LinkButton ID="btnCancel" CommandName="CancelAll" CssClass="cancel-icon"  runat="server"></asp:LinkButton>
              </EditItemTemplate> 
              <InsertItemTemplate>
               <asp:LinkButton ID="btnEdit" CommandName="PerformInsert"  CssClass="ok-icon"  runat="server"></asp:LinkButton>
               <asp:LinkButton ID="btnCancel" CommandName="CancelAll" CssClass="cancel-icon"  runat="server"></asp:LinkButton>
              </InsertItemTemplate>               
              </telerik:GridTemplateColumn> 
            </Columns>
            <EditFormSettings>
                <EditColumn FilterControlAltText="Filter EditCommandColumn1 column" 
                    UniqueName="EditCommandColumn1">
                </EditColumn>
            </EditFormSettings>
        </MasterTableView>
        
                <FilterMenu EnableImageSprites="False">
                </FilterMenu>
        
        </telerik:radgrid>    
            
            
            </td>            
            </tr>                
                  
                  </table>
                  </td> 
                   </tr> 
         </table> 
      <div class="btn_bar">                                    
                        <asp:Button ID="btn_tab2" CssClass="btnsubmit" runat="server" Text="Next" 
                            onclick="btn_tab2_Click" />             
               </div>                  
            </telerik:RadPageView>         
            <telerik:RadPageView ID="RadPageView3" runat="server" CssClass="pageViewEducation"> 
                      <table class="outter full">                  
               <tr><td>                 
                 <telerik:radgrid runat="server" ID="dependencygrid" ShowStatusBar="True"    AutoGenerateColumns="False"  GridLines="None" 
                      Width="100%" CellSpacing="0"  onneeddatasource="dependencygrid_NeedDataSource"  
                       oninsertcommand="dependencygrid_InsertCommand" 
                       onupdatecommand="dependencygrid_UpdateCommand" 
                       ondeletecommand="dependencygrid_DeleteCommand" 
                       onitemdatabound="dependencygrid_ItemDataBound">
         <PagerStyle Mode="NumericPages"></PagerStyle>
        <MasterTableView AllowMultiColumnSorting="True"  Width="100%"  EditMode="InPlace" CommandItemDisplay="Top" CommandItemSettings-ShowRefreshButton="False">         
            <CommandItemSettings ExportToPdfText="Export to PDF" />
            <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column" 
                Visible="True">
            </RowIndicatorColumn>
            <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column" 
                Visible="True">
            </ExpandCollapseColumn>
            <Columns> 
            <telerik:GridTemplateColumn HeaderText="Sno" Visible="false"  UniqueName="Sno"  >
            <ItemTemplate>           
             <asp:Label ID="lblReferenceID" runat="server" Text='<%#Eval("RecordID") %>' Visible="false" ></asp:Label>
            <asp:Label ID="lblSno" runat="server"></asp:Label>
            </ItemTemplate>
            <EditItemTemplate>        
            <asp:Label ID="lblRecordID" runat="server" Text='<%#Eval("RecordID") %>'  Visible="false" ></asp:Label>
             <asp:Label ID="lblReferenceID" runat="server"  Visible="false"></asp:Label>
            <asp:Label ID="lblSno" runat="server"></asp:Label>
            </EditItemTemplate> 
            </telerik:GridTemplateColumn>                   
            <telerik:GridTemplateColumn DataField="FirstName" FilterControlAltText="Filter FirstName column" HeaderText="FirstName" SortExpression="FirstName" UniqueName="FirstName">
            <ItemTemplate>             
            <asp:Label ID="lblFirstName" runat="server" Text='<%#Eval("FirstName") %>'></asp:Label>
            </ItemTemplate>
            <EditItemTemplate>           
            <asp:TextBox ID="txtFirstName" Width="160px" runat="server" CssClass="uptext" Text='<%#Eval("FirstName") %>'></asp:TextBox>
            </EditItemTemplate>
            </telerik:GridTemplateColumn>
            <telerik:GridTemplateColumn DataField="LastName" FilterControlAltText="Filter LastName column" HeaderText="LastName" SortExpression="LastName" UniqueName="LastName">
            <ItemTemplate>             
            <asp:Label ID="lblLastName" runat="server" Text='<%#Eval("LastName") %>'></asp:Label>
            </ItemTemplate>
            <EditItemTemplate>           
            <asp:TextBox ID="txtLastName" Width="160px" runat="server" CssClass="uptext" Text='<%#Eval("LastName") %>'></asp:TextBox>
            </EditItemTemplate>
            </telerik:GridTemplateColumn>
            <telerik:GridTemplateColumn DataField="Gender" FilterControlAltText="Filter Gender column" HeaderText="Gender" SortExpression="Gender" UniqueName="Gender">
            <ItemTemplate>
                      <asp:Label ID="lblGender" runat="server" Text='<%#Eval("Gender") %>' Visible="false"></asp:Label>
                <telerik:RadComboBox ID="drpGender" Width="160px" Enabled="false" runat="server">
               <Items>
               <telerik:RadComboBoxItem Value="0" Text="Select" Selected="true"></telerik:RadComboBoxItem > 
               <telerik:RadComboBoxItem Value="1" Text="Male"></telerik:RadComboBoxItem > 
               <telerik:RadComboBoxItem Value="2" Text="Female"></telerik:RadComboBoxItem > 
               </Items> 
                </telerik:RadComboBox>                                            
                        </ItemTemplate>
            <EditItemTemplate>
             <asp:Label ID="lblGender" runat="server" Text='<%#Eval("Gender") %>' Visible="false"></asp:Label>
              <telerik:RadComboBox ID="drpGender" Width="160px" runat="server">
               <Items>
               <telerik:RadComboBoxItem Value="0" Text="Select" Selected="true"></telerik:RadComboBoxItem > 
               <telerik:RadComboBoxItem Value="1" Text="Male"></telerik:RadComboBoxItem > 
               <telerik:RadComboBoxItem Value="2" Text="Female"></telerik:RadComboBoxItem > 
               </Items> 
                </telerik:RadComboBox>  
                     <asp:CompareValidator runat="server" ID="reqgender" ValueToCompare="Select"  Operator="NotEqual" ControlToValidate="drpGender" ErrorMessage="Required!"   ValidationGroup="Required"/>           
            </EditItemTemplate>
            </telerik:GridTemplateColumn>
               <telerik:GridTemplateColumn DataField="SSN" 
                    FilterControlAltText="Filter SSN column" HeaderText="SSN" 
                    SortExpression="SSN" UniqueName="SSN">
                      <ItemTemplate>
                      <telerik:RadMaskedTextBox ID="lblSSN" runat="server" Width="100px"
                    Mask="###-##-####" BorderStyle="None" Text='<%#Eval("SSN") %>' Enabled="false"></telerik:RadMaskedTextBox>
            <%--<asp:Label ID="lblSSN" runat="server" Text = '<%# Bind("SSN", "{0:###-##-####}") %>'></asp:Label>--%>
            </ItemTemplate>
            <EditItemTemplate>
                <telerik:RadMaskedTextBox ID="txtSSN" runat="server" Width="100px"
                    Mask="###-##-####" BorderStyle="None" Text='<%#Eval("SSN") %>'></telerik:RadMaskedTextBox>
            </EditItemTemplate>
                </telerik:GridTemplateColumn> 
            <telerik:GridTemplateColumn SortExpression="BirthDate" HeaderText="DOB" HeaderButtonType="TextButton"
                    DataField="BirthDate" UniqueName="BirthDate">
                     <ItemTemplate>
            <asp:Label ID="lblBirthDate" runat="server" Text='<%#Eval("BirthDate") %>'></asp:Label>
            </ItemTemplate>
            <EditItemTemplate>
                <telerik:RadDatePicker ID="txtBirthDate" runat="server"  Height="25px"  DbSelectedDate='<%# Bind("BirthDate") %>'  ShowPopupOnFocus="True" MinDate="1/1/1900">
<Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"></Calendar>

<DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>

<DateInput DisplayDateFormat="M/d/yyyy" DateFormat="M/d/yyyy" LabelWidth="40%"></DateInput>
                        </telerik:RadDatePicker>          
            </EditItemTemplate>
                    </telerik:GridTemplateColumn>
            <telerik:GridTemplateColumn DataField="Relationship"  FilterControlAltText="Filter Relationship column" HeaderText="Relationship"  SortExpression="Relationship" UniqueName="Relationship">
                    <ItemTemplate>
                      <asp:Label ID="lblRelationship" runat="server" Text='<%#Eval("Relationship") %>' Visible="false"></asp:Label>
          <telerik:RadComboBox ID="drpRelationship" Width="160px" Enabled="false"   runat="server"   ></telerik:RadComboBox>                                            
                        </ItemTemplate>
            <EditItemTemplate>
             <asp:Label ID="lblRelationship" runat="server" Text='<%#Eval("Relationship") %>' Visible="false"></asp:Label>
                <telerik:RadComboBox ID="drpRelationship" Width="160px" runat="server"  ValidationGroup="Required"></telerik:RadComboBox>     
                     <asp:CompareValidator runat="server" ID="reqcategory" ValueToCompare="Select"  Operator="NotEqual" ControlToValidate="drpRelationship" ErrorMessage="Required!"   ValidationGroup="Required"/>           
            </EditItemTemplate>
            </telerik:GridTemplateColumn>      
               
              <telerik:GridTemplateColumn DataField="Beneficiary"  FilterControlAltText="Filter Beneficiary column" HeaderText="Beneficiary"  SortExpression="Beneficiary" UniqueName="Beneficiary">
                    <ItemTemplate>
                      <asp:CheckBox ID="Beneficiary_chk" runat="server" Enabled="false" Checked='<%# bool.Parse((Eval("Beneficiary")).ToString()) %>' />                                          
                        </ItemTemplate>
            <EditItemTemplate>
            <asp:Label ID="lblBeneficiary" runat="server" Text='<%#Eval("Beneficiary") %>' Visible="false"></asp:Label>
             <asp:CheckBox ID="Beneficiary_chk" runat="server"  />
            </EditItemTemplate>
            </telerik:GridTemplateColumn> 
               
                      <telerik:GridTemplateColumn HeaderText="Attachment" UniqueName="Attachment" ItemStyle-Width="100">
                      <ItemTemplate>
                         <asp:Label ID="lblupload" runat="server"  Text='<%#Eval("Upload") %>' Visible="false" ></asp:Label>
                         <asp:HyperLink ID="lbtncertificate" Target="_blank" NavigateUrl='<%#"~/Common/Resource/Docs/Dependence/"+DataBinder.Eval(Container.DataItem,"RecordID")+".pdf"%>'  runat="server">			
                    <asp:Image ID="Image3" ImageUrl="~/images/pdficon.png" Width="30px" Height="39px"   runat="server" /></asp:HyperLink>
            </ItemTemplate>          
                </telerik:GridTemplateColumn>                             
      <telerik:GridTemplateColumn HeaderText="Action" >
        <ItemStyle Width="50px" />       
              <ItemTemplate>                  
                  <asp:LinkButton ID="btnEdit" CommandName="Edit"  CssClass="edit-icon"   runat="server"></asp:LinkButton>
                  <asp:LinkButton ID="btnDelete" CommandName="Delete" CssClass="del-icon"  OnClientClick='<%# "javascript:return confirm(\"Are you sure want to delete this Record ?\\n" + ((string)Eval("FirstName"))+ "\");"%>'  runat="server"></asp:LinkButton>
              </ItemTemplate> 
              <EditItemTemplate>
               <asp:LinkButton ID="btnEdit" CommandName="Update" CausesValidation="true" ValidationGroup="Required"  CssClass="ok-icon"  runat="server"></asp:LinkButton>
               <asp:LinkButton ID="btnCancel" CommandName="CancelAll" CssClass="cancel-icon"  runat="server"></asp:LinkButton>
              </EditItemTemplate> 
              <InsertItemTemplate>
               <asp:LinkButton ID="btnEdit" CommandName="PerformInsert" CausesValidation="true" ValidationGroup="Required" CssClass="ok-icon"  runat="server"></asp:LinkButton>
               <asp:LinkButton ID="btnCancel" CommandName="CancelAll" CssClass="cancel-icon"  runat="server"></asp:LinkButton>
              </InsertItemTemplate>               
              </telerik:GridTemplateColumn> 
              
            </Columns>            
           <EditFormSettings>
<EditColumn UniqueName="EditCommandColumn1" FilterControlAltText="Filter EditCommandColumn1 column"></EditColumn>
</EditFormSettings>
        </MasterTableView>
                    <FilterMenu EnableImageSprites="False">
                    </FilterMenu>
  
        </telerik:radgrid>   
        </td></tr>  
        <tr>
         <td class="break" ></td>
        </tr>
        <tr>
        <td>        
       <table class="section"  >
            <tr><td colspan="2">Attachments</td></tr>
            <tr><td style="width:100px">Dependents</td><td><telerik:RadComboBox ID="dpdependence" runat="server"></telerik:RadComboBox>
             <asp:CompareValidator runat="server" ID="CompareValidator1" ValueToCompare="Select"  Operator="NotEqual" ControlToValidate="dpdependence" Text="*" ValidationGroup="depupload" ErrorMessage="Dependence Required!" /> 
            </td></tr>
            <tr><td>File</td><td>
                <asp:FileUpload ID="dependenceUpload" runat="server" />                
            </td>
            </tr>
            <tr><td></td><td><div style="float:left;"><asp:Button ID="btnupload" CssClass="btnsubmit" runat="server" CausesValidation="true"  ValidationGroup="depupload"  Text="Upload" onclick="btnupload_Click"  /></div> </td></tr>
            <tr><td></td> <td>
                <asp:Label ID="lbluploadmsg" runat="server" Text=""></asp:Label><asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="dependenceUpload" ErrorMessage="Select a PDF file" ValidationExpression="(.*\.([pP][dD][fF])$)" ValidationGroup="depupload" ></asp:RegularExpressionValidator></td></tr> 
        </table> 
        </td>
        </tr>
        </table> 
        <div class="btn_bar">                                    
                        <asp:Button ID="btn_tab3" CssClass="btnsubmit" runat="server" Text="Next" 
                            onclick="btn_tab3_Click"  />             
               </div>                 
            </telerik:RadPageView>  
              <telerik:RadPageView ID="RadPageView4" runat="server" CssClass="pageViewEducation"> 
                      <table class="outter full">    
                        <tr>
                  <td>       
                     <asp:RadioButtonList ID="Feeoption" runat="server" 
                          RepeatDirection="Horizontal" CssClass="removestyle" AutoPostBack="true" 
                          onselectedindexchanged="Feeoption_SelectedIndexChanged"  >  
                          <asp:ListItem Value="0" Selected="True">Unpaid</asp:ListItem>                     
                          <asp:ListItem Value="1">Paid In full</asp:ListItem>
                           <asp:ListItem Value="2">Partial Paid</asp:ListItem>                      
                          <asp:ListItem Value="3">Waiver</asp:ListItem>                      
                      </asp:RadioButtonList>
                      <br />
                  </td></tr>     
                <tr>
                  <td>
                  <table style="width:100%;">
                  <tr>
                  <td style="width:90%"><asp:TextBox ID="txtpartial" runat="server" Visible="false" ></asp:TextBox>
                 <asp:TextBox ID="txtoutstanding"  runat="server" Visible="false"></asp:TextBox></td>
                  <td>Initiation Fee</td>
                  <td><asp:TextBox ID="txtFee" Width="50px" Enabled="false" runat="server"></asp:TextBox></td>
                  </tr> 
                  <tr id="waivertab" runat="server">
                  <td colspan="3"  style="padding-left:20px;" >
                  <hr />
                  <br />
                  Initiation fee Exempted</td>
                  </tr>                    
                  </table>
                  </td>
                  </tr>                                            
               <tr><td>                  
                                             
                 <telerik:radgrid runat="server" ID="Initiationgrid" ShowStatusBar="True"    AutoGenerateColumns="False"  GridLines="None" 
                      Width="100%" CellSpacing="0"  onneeddatasource="Initiationgrid_NeedDataSource"  
                       oninsertcommand="Initiationgrid_InsertCommand" 
                       onupdatecommand="Initiationgrid_UpdateCommand" 
                       ondeletecommand="Initiationgrid_DeleteCommand" 
                       onitemdatabound="Initiationgrid_ItemDataBound">
         <PagerStyle Mode="NumericPages"></PagerStyle>
        <MasterTableView AllowMultiColumnSorting="True"  Width="100%"  EditMode="InPlace"  CommandItemSettings-ShowRefreshButton="False">         
            <CommandItemSettings ExportToPdfText="Export to PDF" />
            <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column" 
                Visible="True">
            </RowIndicatorColumn>
            <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column" 
                Visible="True">
            </ExpandCollapseColumn>
            <Columns> 
            <telerik:GridTemplateColumn HeaderText="Sno" UniqueName="Sno" Visible="false" >
            <ItemTemplate>           
             <asp:Label ID="lblReferenceID" runat="server" Text='<%#Eval("RecordID") %>' Visible="false" ></asp:Label>
            <asp:Label ID="lblSno" runat="server"></asp:Label>
            </ItemTemplate>
            <EditItemTemplate>        
            <asp:Label ID="lblRecordID" runat="server" Text='<%#Eval("RecordID") %>'  Visible="false" ></asp:Label>
             <asp:Label ID="lblReferenceID" runat="server"  Visible="false"></asp:Label>
            <asp:Label ID="lblSno" runat="server"></asp:Label>
            </EditItemTemplate> 
            </telerik:GridTemplateColumn>                   
            <telerik:GridTemplateColumn DataField="Mode" FilterControlAltText="Filter Mode column" HeaderText="Mode" SortExpression="Mode" UniqueName="Mode">
            <ItemTemplate>             
            <asp:Label ID="lblMode" runat="server" Text='<%#Eval("Mode") %>'></asp:Label>
            </ItemTemplate>
            <EditItemTemplate>           
            <asp:TextBox ID="txtMode" Width="160px" runat="server" Text='<%#Eval("Mode") %>'></asp:TextBox>
            </EditItemTemplate>
            </telerik:GridTemplateColumn>
            <telerik:GridTemplateColumn DataField="Amount" FilterControlAltText="Filter Amount column" HeaderText="Amount" SortExpression="Amount" UniqueName="Amount">
            <ItemTemplate>             
            <asp:Label ID="lblAmount" runat="server" Text='<%#Eval("Amount") %>'></asp:Label>
            </ItemTemplate>             
            <EditItemTemplate>
            <telerik:RadNumericTextBox ID="txtAmount" runat="server" Culture="English (United States)" DbValue='<%#Eval("Amount") %>'  DbValueFactor="1" LabelWidth="64px" Type="Currency" Width="160px" Height="25px" OnTextChanged="onchange_TextChanged" AutoPostBack="true" ValidationGroup="Required"><NumberFormat ZeroPattern="$n"></NumberFormat></telerik:RadNumericTextBox>                      
            <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="Entered Amount is Greater than Required Amount" OnServerValidate="CustomValidator1_ServerValidate" ValidationGroup="Required"></asp:CustomValidator>
            </EditItemTemplate>                                     
            </telerik:GridTemplateColumn>          
            <telerik:GridTemplateColumn SortExpression="Date" HeaderText="Date" HeaderButtonType="TextButton" DataField="Date" UniqueName="Date">
            <ItemTemplate>
            <asp:Label ID="lblDate" runat="server" Text='<%#Eval("Date") %>'></asp:Label>
            </ItemTemplate>
            <EditItemTemplate>
                <telerik:RadDatePicker ID="txtDate" runat="server"  Height="25px"  DbSelectedDate='<%# Bind("Date") %>'  ShowPopupOnFocus="True" ValidationGroup="Required" MinDate="1/1/1900">
<Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"></Calendar>

<DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>

<DateInput DisplayDateFormat="M/d/yyyy" DateFormat="M/d/yyyy" LabelWidth="40%"></DateInput>
                        </telerik:RadDatePicker>  
            <asp:RequiredFieldValidator ID="txtDateValidator" runat="server" ControlToValidate="txtDate" ErrorMessage="Required!" ValidationGroup="Required"></asp:RequiredFieldValidator>      
            </EditItemTemplate>
                    </telerik:GridTemplateColumn>                                     
      <telerik:GridTemplateColumn HeaderText="Action" >
        <ItemStyle Width="50px" />       
              <ItemTemplate>                  
                  <asp:LinkButton ID="btnEdit" CommandName="Edit"  CssClass="edit-icon"   runat="server"></asp:LinkButton>
                  <asp:LinkButton ID="btnDelete" CommandName="Delete" CssClass="del-icon"  runat="server"></asp:LinkButton>
              </ItemTemplate> 
              <EditItemTemplate>
               <asp:LinkButton ID="btnEdit" CommandName="Update" CausesValidation="true" ValidationGroup="Required"  CssClass="ok-icon"  runat="server"></asp:LinkButton>
               <asp:LinkButton ID="btnCancel" CommandName="CancelAll" CssClass="cancel-icon"  runat="server"></asp:LinkButton>
              </EditItemTemplate> 
              <InsertItemTemplate>
               <asp:LinkButton ID="btnEdit" CommandName="PerformInsert" CausesValidation="true" ValidationGroup="Required" CssClass="ok-icon"  runat="server"></asp:LinkButton>
               <asp:LinkButton ID="btnCancel" CommandName="CancelAll" CssClass="cancel-icon"  runat="server"></asp:LinkButton>
              </InsertItemTemplate>               
              </telerik:GridTemplateColumn>              
            </Columns>                        
           <EditFormSettings>
<EditColumn UniqueName="EditCommandColumn1" FilterControlAltText="Filter EditCommandColumn1 column"></EditColumn>
</EditFormSettings>
        </MasterTableView>        
                    <FilterMenu EnableImageSprites="False">
                    </FilterMenu>
  
        </telerik:radgrid>  
        </td></tr> 
         </table> 
        <div class="btn_bar">                                    
                        <asp:Button ID="btn_tab4" CssClass="btnsubmit" runat="server" Text="Next" 
                            onclick="btn_tab4_Click"  />             
               </div>                 
            </telerik:RadPageView>  
            <telerik:RadPageView ID="RadPageView5" runat="server" CssClass="pageViewEducation"> 
                      <table class="outter full">                  
               <tr><td>                 
               <table class="section" >
        <tr><td>Authorized</td><td>
            <asp:CheckBox ID="Authorize" runat="server" /></td></tr>
			<tr><td width="150px">Authorization Letter</td>
			<td> 
                <asp:FileUpload ID="Authorizationuploader" runat="server" />
                <asp:RegularExpressionValidator ID="AuthorizationuploaderValidator" runat="server" ControlToValidate="Authorizationuploader"
            Display="Dynamic" ErrorMessage="Select a PDF file" SetFocusOnError="True" ValidationExpression="(.*\.([pP][dD][fF])$)" ValidationGroup="tab6"
            ></asp:RegularExpressionValidator>
             </td>
             </tr>                     
        </table>
        </td></tr>  </table>        
         <div class="btn_bar">                                      
                    <asp:Button ID="btn_tab5" CssClass="btnsubmit" runat="server" Text="Submit" onclick="btn_tab5_Click" />               
               </div>              
            </telerik:RadPageView>  
        </telerik:RadMultiPage>
        <telerik:RadFormDecorator runat="server" ID="RadFormDecorator1" DecoratedControls="Textarea">
        </telerik:RadFormDecorator>
               
            </div>
            </div>
</div>
  <telerik:RadWindowManager ID="RadWindowManager" runat="server">
</telerik:RadWindowManager>
<telerik:RadWindow ID="RadWindow" runat="server"  NavigateUrl="Notes.aspx" Width="850" Height="650" Header="Notes" Modal="true"  >
</telerik:RadWindow>
    <script type="text/javascript">
        //<![CDATA[
        function openRadWin() {

            var oWnd = $find("<%= RadWindow.ClientID %>");             
            oWnd.show();
        }
        //]]>
    </script>
</asp:Content> 