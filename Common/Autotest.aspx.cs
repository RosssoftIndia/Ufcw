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
using System.Web.Services;

public partial class Common_Autotest : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }


    public static string DBConnectionString()
    {
        string strConn = ConfigurationManager.ConnectionStrings["UfcwConnectionString"].ConnectionString;
        return strConn;
    }
    [WebMethod]
    public static List<string> GetAutoCompleteData(string username)
    {
        List<string> result = new List<string>();
        using (SqlConnection con = new SqlConnection(DBConnectionString()))
        {
            using (SqlCommand cmd = new SqlCommand("SELECT [Name] FROM [dbo].[Shops] where Name Like '%'+@SearchText+'%'", con))
            {
                con.Open();
                cmd.Parameters.AddWithValue("@SearchText", username);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    result.Add(dr["Name"].ToString());
                }
                return result;
            }
        }
    }
}
