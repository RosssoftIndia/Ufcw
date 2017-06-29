using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Reports;

public partial class Common_Reports_ShopLable : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        menu.GenerateMenu("Reports");
        Submenu.GenerateMenu("ShopLabel");

        
    }

    protected void btnsearch_Click(object sender, EventArgs e)
    {
        if (Drp_Style.SelectedValue.ToString() == "1")
        {
            ShopLable report = new ShopLable();
            ReportViewer1.Report = report;
        }
        else if (Drp_Style.SelectedValue.ToString() == "2")
        {
            ShopLableMin report = new ShopLableMin();
            ReportViewer1.Report = report;
        }
    }
}
