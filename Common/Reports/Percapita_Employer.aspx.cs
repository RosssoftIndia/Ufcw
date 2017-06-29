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

public partial class Common_Reports_Percapita_Employer : SessionTracker 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        menu.GenerateMenu("Reports");
        Submenu.GenerateMenu("Percapita-Employer");
        if (!IsPostBack)
        {

            Noreportlbl.Visible = false;

            Percapita_EmployerFile report = new Percapita_EmployerFile();

            ReportViewer1.Visible = true;
            ReportViewer1.Report = report;


        }
    }
    protected void Percapitabtn_Click(object sender, EventArgs e)
    {
        // bool sp = false;
        SqlConnection con = new SqlConnection(Repository.Connection.DBConnectionString());
        // con.Open();
        //  SqlCommand cmd = new SqlCommand("Active_Percaptia_N_sp", con);
        //cmd.CommandType = CommandType.StoredProcedure;

        SqlCommand mySqlCommand = con.CreateCommand();
        mySqlCommand.CommandText = "ReportPercapita_EmployerFile_sp";
        mySqlCommand.CommandType = CommandType.StoredProcedure;


        SqlDataAdapter mySqlDataAdapter = new SqlDataAdapter();
        mySqlDataAdapter.SelectCommand = mySqlCommand;
        DataSet myDataSet = new DataSet();
        con.Open();
        mySqlDataAdapter.Fill(myDataSet);
        string path = Server.MapPath("~/App_Data/TempExport/EmployerFile.txt");
        DataTable2CSV(myDataSet, path);

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

    public void DataTable2CSV(DataSet Dataset, string filename)
    {
        DataTable2CSV(Dataset, filename, System.Environment.NewLine);
    }
    public void DataTable2CSV(DataSet Dataset, string filename, string sepChar)
    {
        if (Dataset.Tables.Count>0)
        {
            DataTable table = new DataTable();
            table = Dataset.Tables[0];
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
        string Headdate = year + month.PadLeft(2, '0');
        string Header = "0312   " + Headdate + "EMPLOYERFILE          ";
        string Footer = "0312   " + Headdate + "EMPLOYERFILE          " + Count_table;
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
        else
        {
            string Count_table = "000000";
            
            DateTime dt = DateTime.Now;

            string year = dt.Year.ToString();
            string month = dt.Month.ToString();
            //string date = dt.Day.ToString();
            string Headdate = year + month.PadLeft(2, '0');
            string Header = "0312   " + Headdate + "EMPLOYERFILE          ";
            string Footer = "0312   " + Headdate + "EMPLOYERFILE          " + Count_table;
            string Line_Feed = "\n";
            System.IO.StreamWriter writer = null;
            try
            {
                writer = new System.IO.StreamWriter(filename);
                writer.WriteLine(Header);
                //writer.WriteLine(Line_Feed);
                writer.WriteLine(Footer);
                // first write a line with the columns name
                //string sep = "";
                //System.Text.StringBuilder builder = new System.Text.StringBuilder();
                //foreach (DataColumn col in table.Columns)
                //{
                //    //  builder.Append(sep).Append(col.ColumnName);
                //    //   builder.Append(col.ColumnName);
                //    sep = sepChar;
                //}
                //   writer.WriteLine(builder.ToString());
                
                // then write all the rows
                
                
            }

        finally
        {
            if ((writer != null))
                writer.Close();
        }
    }
}
    
}
