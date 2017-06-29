using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Reports;

public partial class Common_Reports_MembersLabel : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        menu.GenerateMenu("Reports");
        Submenu.GenerateMenu("MembersLabel");
        if (!IsPostBack)
        {

            Repository.Shops.GetShopReport_cb(Drp_shop);

        }
    }
    
    protected void btnsearch_Click(object sender, EventArgs e)
    {
        if (Drp_shop.SelectedValue.ToString() != "0")
        {
            if (Drp_Style.SelectedValue.ToString() == "1")
            {
                MembersLabel report = new MembersLabel();
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
            else if (Drp_Style.SelectedValue.ToString() == "2")
            {
                MembersLabelMin report = new MembersLabelMin();
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
   
}
