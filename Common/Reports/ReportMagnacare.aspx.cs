using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Reports;

public partial class ReportMagnacare : SessionTracker 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        menu.GenerateMenu("Reports");
        Submenu.GenerateMenu("Magnacare");
        if (!IsPostBack)
        {

            Repository.Benefits.GetBenefitsMagnacare_cb(Drp_Benefits);
            
                string category = Drp_Benefits.SelectedItem.Text;
                //string type = RadioButtonList1.SelectedValue;
                if (category == "All")
                {
                    MagnaCareReport MagnaReport = new MagnaCareReport();
                    ReportViewer1.Report = MagnaReport;

                }
            

        }
        
        
    }
    
    protected void Drp_shop_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
    {
       
            string category = Drp_Benefits.SelectedItem.Text;
            if (category == "All")
            {
                MagnaCareReport MagnaReport = new MagnaCareReport();
                ReportViewer1.Report = MagnaReport;

            }
            else
            {
                BindDataset(category);
            }
            //string type = RadioButtonList1.SelectedValue;
            
      
    }

    public void BindDataset(string Category)
    {

        var objectDataSource = new Telerik.Reporting.ObjectDataSource();
        string selectCommandText = "";
        //Getting Datasouce
        string connectionString = ConfigurationManager.ConnectionStrings["Reports.Properties.Settings.UFCWEntire"].ConnectionString;
        //const string connectionString =
        //    "Data Source=.;Initial Catalog=UFCW_Entire;Integrated Security=True";

        if (Category != "All")
        {
            selectCommandText = "SELECT (select Name from dbo.Benefits where Name='" + Category + "') as Benefits,(select COUNT(*) as Total from Members_Benefits where BenefitID in (select RecordID from Benefits where Name='" + Category + "')) as MembersCount,((select COUNT(*) as Total from Members_Benefits where BenefitID in (select RecordID from Benefits where Name='" + Category + "')) + (select COUNT(*) as Total from Members_Benefits a inner join Members_Dependence b on a.ReferenceID=b.ReferenceID where BenefitID in (select RecordID from Benefits where Name='" + Category + "'))) as MemDepCount";
        }
                 
        SqlDataAdapter adapter = new SqlDataAdapter(selectCommandText, connectionString);

        DataTable dt = new DataTable();
        adapter.Fill(dt);   

        objectDataSource.DataSource = dt;



        MagnaCareReport MagnaReport = new MagnaCareReport();
        MagnaReport.DataSource = objectDataSource;
        
        Telerik.Reporting.InstanceReportSource reportSource = new Telerik.Reporting.InstanceReportSource();
        reportSource.ReportDocument = MagnaReport;

     
        ReportViewer1.ReportSource = reportSource;
     
    }
    //protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (Drp_Benefits.SelectedIndex != 0)
    //    {
    //        string category = Drp_Benefits.SelectedItem.Text;
    //        string type = RadioButtonList1.SelectedValue;
    //        BindDataset(category, type);
    //    }
       
    //}
}
