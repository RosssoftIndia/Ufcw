<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Autotest.aspx.cs" Inherits="Common_Autotest" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.1/themes/base/jquery-ui.css" rel="stylesheet" type="text/css"/>
<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.4.2/jquery.min.js"></script>
<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.1/jquery-ui.min.js"></script> 
<script type="text/javascript">
    $(function() {
        $(".autotext").autocomplete({
            source: function(request, response) {
                $.ajax({
                url: "Autotest.aspx/GetAutoCompleteData",
                    data: "{ 'name': '" + request.term + "' }",
                    dataType: "json",
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    dataFilter: function(data) { return data; },
                    success: function(data) {
                        response($.map(data.d, function(item) {
                            return {
                                value: item.CountryName
                            }
                        }))
                    },
                    error: function(XMLHttpRequest, textStatus, errorThrown) {
                        alert(textStatus);
                    }
                });
            },
            minLength: 2
        });
    });
</script>
</head>
<body>
    <form id="form1" runat="server">
    <telerik:RadScriptManager runat="server" ID="RadScriptManager1" />
    
     <div class="qsf-demo-canvas">
          <telerik:RadComboBox runat="server" ID="RadComboBox1" Height="100px" EnableLoadOnDemand="true"
               ShowMoreResultsBox="true" EnableVirtualScrolling="true" EmptyMessage="Type here ...">
               <WebServiceSettings Path="ComboBoxWcfService.svc" Method="LoadData" />
          </telerik:RadComboBox>
     </div>
     </form>
</body>

</html>
