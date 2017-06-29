<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EditSSN.aspx.cs" Inherits="Editmember" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        html, body, form
        {
            padding: 0;
            margin: 0;
            height: 100%;
            background: #f2f2de;
        }
        
        body
        {
            font: normal 11px Arial, Verdana, Sans-serif;
        }
        
        fieldset
        {
            height: 150px;
        }
        
        * + html fieldset
        {
            height: 154px;
            width: 268px;
        }
    </style>
</head>
<body>
    <form id="Form1" method="post" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <script type="text/javascript">
        function GetRadWindow()
        {
            var oWindow = null;
            if (window.radWindow) oWindow = window.radWindow;
            else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
            return oWindow;
        }
      
        //fix for Chrome/Safari due to absolute positioned popup not counted as part of the content page layout
        function ChromeSafariFix(oWindow)
        {
            var iframe = oWindow.get_contentFrame();
            var body = iframe.contentWindow.document.body;

            setTimeout(function ()
            {
                var height = body.scrollHeight;
                var width = body.scrollWidth;

                var iframeBounds = $telerik.getBounds(iframe);
                var heightDelta = height - iframeBounds.height;
                var widthDelta = width - iframeBounds.width;

                if (heightDelta > 0) oWindow.set_height(oWindow.get_height() + heightDelta);
                if (widthDelta > 0) oWindow.set_width(oWindow.get_width() + widthDelta);
                oWindow.center();

            }, 310);
        }
        function returnToParentcancel() {
            //create the argument that will be returned to the parent page
            var oArg = new Object();
            var oWnd = GetRadWindow();

                oWnd.close();
          }
        function returnToParent()
        {
            //create the argument that will be returned to the parent page
            var oArg = new Object();

            //get the city's name
            oArg.SSN = document.getElementById("SSN").value;

            //get a reference to the current RadWindow
            var oWnd = GetRadWindow();


            //Close the RadWindow and send the argument to the parent page
            if (oArg.SSN) {

                alert("call");

                alert(oArg.SSN);
                if (oArg.SSN.indexOf(" ") != -1) {
                    alert("Enter Only Numbers");
                }
                else {
                    alert("close");
                    oWnd.close(oArg);
                }
            }

        }

    </script>
    <div style="width: 268px; height: 193px;">
        <fieldset id="fld1">
            <div style="margin: 20px 0 0 0;">
                <div style="float: left; margin: 6px 0 0 18px;">
                    Enter Member ID:</div>
                    <%--<asp:TextBox ID="SSN" runat="server"  MaxLength="9" CausesValidation="true" ValidationGroup="valid"></asp:TextBox>--%>
                    <telerik:RadMaskedTextBox Width="220px" height="25px" runat="server" ID="SSN" BorderStyle="None" Mask="###-##-####" ValidationGroup="Tab1" ></telerik:RadMaskedTextBox>
                  <asp:RequiredFieldValidator ID="SSNValidator" runat="server" ControlToValidate="SSN" InitialValue="" Text="*" ErrorMessage="SSN Required!" ValidationGroup="Tab1" ></asp:RequiredFieldValidator>
            </div>
            <asp:ValidationSummary ID="ValidationSummary1"  CssClass="message error close"  runat="server" HeaderText="<h2>Error!</h2>" />
        </fieldset>
        <div style="margin-top: 4px; text-align: right;">
            <button title="Submit" id="close" onclick="returnToParent(); return false;" validationgroup="valid" causesvalidation="true">
                Submit</button>
                <button title="Submit" id="Button1" onclick="returnToParentcancel(); return false;" validationgroup="valid" causesvalidation="true">
                Cancel</button>
        </div>
    </div>
    </form>
</body>
</html>
