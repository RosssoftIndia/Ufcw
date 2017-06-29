using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using Reports;

public partial class Common_Reports_MemberBenefitsSearch : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

            Repository.Benefits.GetBenefits_chkbox(chklst_Benefits);
            Rd_btnMode.SelectedValue = "Any";
        }
    }
    protected void Search_btn_Click(object sender, EventArgs e)
    {
        string Mode = Rd_btnMode.SelectedValue.ToString();
        string RecordID = "";
        int count = 0;
        //string clause = "";

        foreach (ListItem Item in chklst_Benefits.Items)
        {
            if (Item.Selected)
            {
                RecordID = RecordID + "," + Item.Value.ToString();
                count++;
            }
        }

        if (RecordID != "")
        {
            RecordID = RecordID.TrimStart(',');

            var objectDataSource = new Telerik.Reporting.ObjectDataSource();
            //Getting Datasouce
            SqlConnection con = new SqlConnection(Repository.Connection.DBConnectionString());

            SqlCommand mySqlCommand = con.CreateCommand();
            mySqlCommand.CommandText = "ReportMemberBenefitsSearch_sp";
            mySqlCommand.Parameters.Add("@Mode", SqlDbType.NVarChar, 30).Value = Mode;
            mySqlCommand.Parameters.Add("@WhereClause", SqlDbType.NVarChar, -1).Value = RecordID;
            mySqlCommand.Parameters.Add("@Count", SqlDbType.NVarChar, 400).Value = count;


            mySqlCommand.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter mySqlDataAdapter = new SqlDataAdapter();
            mySqlDataAdapter.SelectCommand = mySqlCommand;
            DataTable dt = new DataTable();
            con.Open();
            mySqlDataAdapter.Fill(dt);


            //Report
            objectDataSource.DataSource = dt;


            MemberBenefitsSearch Member = new MemberBenefitsSearch();
            Member.DataSource = objectDataSource;

            Telerik.Reporting.InstanceReportSource reportSource = new Telerik.Reporting.InstanceReportSource();
            reportSource.ReportDocument = Member;


            ReportViewer1.ReportSource = reportSource;
        }
        

    }
}
