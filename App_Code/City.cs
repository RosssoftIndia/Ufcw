// (c) Copyright Microsoft Corporation.
// This source is subject to the Microsoft Permissive License.
// See http://www.microsoft.com/resources/sharedsource/licensingbasics/sharedsourcelicenses.mspx.
// All other rights reserved.


using System;
using System.Collections.Generic;
using System.Web.Services;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Data;
using System.Configuration;


 
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[System.Web.Script.Services.ScriptService]
public class City : WebService
{
    public City()
    {
    }

    [WebMethod]
    public string[] GetCompletionList(string prefixText)
    {       
        string strSQL = "SELECT city FROM cities WHERE (city like '" + prefixText + "%')";                        
         DataSet ds = GetDataSet(strSQL);
         List<string> items = new List<string>(ds.Tables[0].Rows.Count);
         if (ds.Tables[0].Rows.Count > 0)
         {             
             for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
             {
                 
                 items.Add(ds.Tables[0].Rows[i]["city"].ToString());
             }
            
         }
         return items.ToArray();
        
    }

    private static DataSet GetDataSet(string strSQL)
    {
        SqlDataAdapter sdpPrd = new SqlDataAdapter(strSQL, DBConnectionString());
        DataSet ds = new DataSet();
        sdpPrd.Fill(ds);
        return ds;
    }
    public static string DBConnectionString()
    {
        string strConn = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        return strConn;
    }
}