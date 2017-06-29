using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using Telerik.Web.UI;
using System.Web.Security;
using Reports;
using System.IO; 

public partial class Common_Reports_Percapita_active : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        menu.GenerateMenu("Reports");
        Submenu.GenerateMenu("Percapita-Active");
        if (!IsPostBack)
        {

            Noreportlbl.Visible = false;

            ReportPercapita report = new ReportPercapita();
            
            ReportViewer1.Visible = true;
            ReportViewer1.Report = report; 
            
           
        }
        datelbl.Style["display"] = "none";
        statuslbl.Style["display"] = "none";
        datelbl2.Style["display"] = "none";

    }
    public void BindDataset(string type,string date,string date2)
    {

        var objectDataSource = new Telerik.Reporting.ObjectDataSource();
        //Getting Datasouce
        SqlConnection con = new SqlConnection(Repository.Connection.DBConnectionString());
        

        SqlCommand mySqlCommand = con.CreateCommand();
        switch (type)
        {
            case "1":
                mySqlCommand.CommandText = "Active_Percaptia_Search_sp";
                mySqlCommand.Parameters.Add("@Mode", SqlDbType.NVarChar, 30).Value = "DOB";
                mySqlCommand.Parameters.Add("@StartDate", SqlDbType.DateTime).Value = Convert.ToDateTime(date);
                mySqlCommand.Parameters.Add("@EndDate", SqlDbType.DateTime).Value = Convert.ToDateTime(date2);
                break;
            case "2":
                mySqlCommand.CommandText = "Active_Percaptia_N_sp";
                break;
            default:
                mySqlCommand.CommandText = "Active_Percaptia_N_sp";
                break;
        }
        
        mySqlCommand.CommandType = CommandType.StoredProcedure;

        SqlDataAdapter mySqlDataAdapter = new SqlDataAdapter();
        mySqlDataAdapter.SelectCommand = mySqlCommand;
        DataTable dt = new DataTable();
        con.Open();
        mySqlDataAdapter.Fill(dt);


        //Report
        objectDataSource.DataSource = dt;


        ReportPercapita Percapita = new ReportPercapita();
        Percapita.DataSource = objectDataSource;

        Telerik.Reporting.InstanceReportSource reportSource = new Telerik.Reporting.InstanceReportSource();
        reportSource.ReportDocument = Percapita;


        ReportViewer1.ReportSource = reportSource;

    }

    protected void Percapitabtn_Click(object sender, EventArgs e)
    {
       // bool sp = false;
        SqlConnection con = new SqlConnection(Repository.Connection.DBConnectionString());
       // con.Open();
      //  SqlCommand cmd = new SqlCommand("Active_Percaptia_N_sp", con);
        //cmd.CommandType = CommandType.StoredProcedure;

        SqlCommand mySqlCommand = con.CreateCommand();
        switch (statuslbl.Text.ToString())
        {
            case "1":
                mySqlCommand.CommandText = "Active_Percaptia_Search_sp";
                mySqlCommand.Parameters.Add("@Mode", SqlDbType.NVarChar, 30).Value = "DOB";
                mySqlCommand.Parameters.Add("@StartDate", SqlDbType.DateTime).Value = Convert.ToDateTime(datelbl.Text.ToString());
                mySqlCommand.Parameters.Add("@EndDate", SqlDbType.DateTime).Value = Convert.ToDateTime(datelbl2.Text.ToString());
                break;
            case "2":
        mySqlCommand.CommandText = "Active_Percaptia_N_sp";
                break;
            default:
                mySqlCommand.CommandText = "Active_Percaptia_N_sp";
                break;
        }

        mySqlCommand.CommandType = CommandType.StoredProcedure;
      
        
        SqlDataAdapter mySqlDataAdapter = new SqlDataAdapter();
        mySqlDataAdapter.SelectCommand = mySqlCommand;
        DataSet myDataSet = new DataSet();
        con.Open();
        mySqlDataAdapter.Fill(myDataSet);
        string path = Server.MapPath("~/App_Data/TempExport/Users.txt"); 
        DataTable2CSV(myDataSet.Tables[0],path);
   
        string filepath = path;
        FileInfo file = new FileInfo(filepath);
        if (file.Exists)
        {
           // Response.Redirect(filepath);

            Response.ClearContent();
            Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
            Response.AddHeader("Content-Length", file.Length.ToString());
            Response.ContentType = file.Extension.ToLower();
            Response.TransmitFile(file.FullName);
            Response.End();
        }



       
    }   

    public void DataTable2CSV(DataTable table, string filename)
    {
        DataTable2CSV(table, filename, System.Environment.NewLine);
    }
    public void DataTable2CSV(DataTable table, string filename, string sepChar)
    {

        string Count_table = table.Rows.Count.ToString();
        if (Count_table.Length >= 6)
        {
            Count_table = Count_table.Substring(1, 6);
        }
        else
        {
            Count_table = Count_table.PadLeft(6, '0');
        }
        DateTime dt = DateTime.Now;

        string year = dt.Year.ToString();
        string month = dt.Month.ToString();
        //string date = dt.Day.ToString();
        string Headdate = year + month.PadLeft(2,'0');
        string Header = "0312   "+Headdate+"MEMBERFILE          ";
        string Footer = "0312   "+Headdate+"MEMBERFILE          " + Count_table;
        System.IO.StreamWriter writer = null;
        try
        {
            writer = new System.IO.StreamWriter(filename);

            // first write a line with the columns name
            string sep = "";
            System.Text.StringBuilder builder = new System.Text.StringBuilder();
            foreach (DataColumn col in table.Columns)
            {
              //  builder.Append(sep).Append(col.ColumnName);
             //   builder.Append(col.ColumnName);
                sep = sepChar;
            }
         //   writer.WriteLine(builder.ToString());
            writer.WriteLine(Header);
            // then write all the rows
            foreach (DataRow row in table.Rows)
            {
                sep = "";
                builder = new System.Text.StringBuilder();

                foreach (DataColumn col in table.Columns)
                {
                   // builder.Append(sep).Append(row[col.ColumnName]);
                    builder.Append(row[col.ColumnName]);
                    sep = sepChar;
                }
                writer.WriteLine(builder.ToString());
            }
            writer.WriteLine(Footer);
        }
        finally
        {
            if ((writer != null))
                writer.Close();
        }
    }
    protected void drp_search_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        if (drp_search.SelectedValue != "")
        {
            switch (drp_search.SelectedValue.ToString())
            {
                case "1":
                    Date1.Enabled = true;
                    Date2.Enabled = true;
                    break;
                case "2":
                    Date1.Enabled = false;
                    Date2.Enabled = false;
                    Date1.SelectedDate=null;
                    Date2.SelectedDate = null;
                    break;

            }
        }
    }
    protected void btnsearch_Click(object sender, EventArgs e)
    {
        if (drp_search.SelectedValue != "")
        {
            switch (drp_search.SelectedValue.ToString())
            {
                case "1":
                    if (Date1.SelectedDate.ToString() != "" && Date2.SelectedDate.ToString()!="")
                    {
                        BindDataset(drp_search.SelectedValue.ToString(), Date1.SelectedDate.ToString(),Date2.SelectedDate.ToString());
                        statuslbl.Text = "1";
                        datelbl.Text = Date1.SelectedDate.ToString();
                        datelbl2.Text = Date2.SelectedDate.ToString();
                    }
                    else
                    {
                        string radalertscript = "<script language='javascript'>function f(){radalert('Please select date!', 330, 210); Sys.Application.remove_load(f);}; Sys.Application.add_load(f);</script>";
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "radalert", radalertscript);
                    }
                    break;
                case "2":
                    BindDataset(drp_search.SelectedValue.ToString(), "","");
                    statuslbl.Text = "2";
                    break;
                default:
                    BindDataset("", "","");
                    statuslbl.Text = "2";
                    break;
            }

        }
        else
        {
            string radalertscript = "<script language='javascript'>function f(){radalert('Please select a Search category!', 330, 210); Sys.Application.remove_load(f);}; Sys.Application.add_load(f);</script>";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "radalert", radalertscript);
        }
    }
}
