using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Reporting;
using Telerik.ReportViewer;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Reports;

public partial class Common_Reports_CensusHartford : SessionTracker
{
    protected void Page_Load(object sender, EventArgs e)
    {
        menu.GenerateMenu("Reports");
        Submenu.GenerateMenu("Census Hartford");
        if (!IsPostBack)
        {
            Repository.Shops.GetShopReport_cb(Drp_shop);

        }
    }
    protected void ReportViewer1_DataBinding(object sender, EventArgs e)
    {

    }
    protected void Drp_shop_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
    {
        if (Drp_shop.SelectedValue.ToString() != "0")
        {
            CensusHartford report = new CensusHartford();
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
            Monthstartlbl.Text = "";
            Monthendlbl.Text = "";

        }

    }
    protected void Button1_Click(object sender, EventArgs e)
    {

        BindDataset();

    }
    public void BindDataset()
    {

        var objectDataSource = new Telerik.Reporting.ObjectDataSource();
        string selectCommandText = "";
        string Start = Monthstartlbl.Text.ToString();
        string End = Monthendlbl.Text.ToString();
        //Getting Datasouce
        SqlConnection con = new SqlConnection(Repository.Connection.DBConnectionString());
        SqlCommand mySqlCommand = con.CreateCommand();
        mySqlCommand.CommandText = "ReportHartford_byDate_sp";
        mySqlCommand.Parameters.Add("@Startdate", SqlDbType.NVarChar, -1).Value = Start;
        mySqlCommand.Parameters.Add("@Enddate", SqlDbType.NVarChar, -1).Value = End;
        mySqlCommand.CommandType = CommandType.StoredProcedure;

        SqlDataAdapter mySqlDataAdapter = new SqlDataAdapter();
        mySqlDataAdapter.SelectCommand = mySqlCommand;
        DataTable dt = new DataTable();
        con.Open();
        mySqlDataAdapter.Fill(dt);

        objectDataSource.DataSource = dt;


        CensusHartford HartReport = new CensusHartford();
        HartReport.DataSource = objectDataSource;

        Telerik.Reporting.InstanceReportSource reportSource = new Telerik.Reporting.InstanceReportSource();
        reportSource.ReportDocument = HartReport;


        ReportViewer1.ReportSource = reportSource;

    }
    protected void Monthyear_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
    {

        DateTime Start = new DateTime(e.NewDate.Value.Year, e.NewDate.Value.Month, 1);
        DateTime End = Start.AddMonths(1).AddDays(-1);
        Monthstartlbl.Text = Start.ToShortDateString();
        Monthendlbl.Text = End.ToShortDateString();
        Drp_shop.SelectedValue = "0";
        BindDataset();

    }
}
