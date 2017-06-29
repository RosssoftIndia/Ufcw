using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Reporting;
using Telerik.ReportViewer;
using Reports;

public partial class GPP : SessionTracker 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        menu.GenerateMenu("Reports");
        Submenu.GenerateMenu("GPP");
        if (!IsPostBack)
        {

            Repository.Shops.GetShopReport_cb(Drp_shop);

        }
      
       
    }
    protected void Drp_shop_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
    {
        if (Drp_shop.SelectedValue.ToString() != "0")
        {
            ReportGPP report = new ReportGPP();
            string search = Drp_shop.SelectedItem.Text;
            if (Drp_shop.SelectedValue.ToString() != "")
            {
            Telerik.Reporting.Filter filter1 = new Telerik.Reporting.Filter();

            filter1.Operator = Telerik.Reporting.FilterOperator.Equal;
            filter1.Expression = "=Fields.Shop";
            filter1.Value = search;
            report.Filters.Add(filter1);
            }
            ReportViewer1.Visible = true;
            ReportViewer1.Report = report;
        }


    }
   
}
