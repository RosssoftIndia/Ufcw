using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Web.UI.HtmlControls;

public partial class Common_menu : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
        
       
    }

    public void GenerateMenu(string title)
    {
        DataSet ds = Repository.Menu.GetMenu_sp();
        if (ds.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
            {
                HtmlGenericControl li = new HtmlGenericControl("li");               
                HyperLink lnk = new HyperLink();
                if (ds.Tables[0].Rows[i]["Name"].ToString() == title)
                {
                    lnk.Attributes.Add("class", "active");
                }
                lnk.NavigateUrl = ds.Tables[0].Rows[i]["Url"].ToString();
                lnk.Text = "<i class='" + "icon-file" + "'></i>" + ds.Tables[0].Rows[i]["Name"].ToString();
                li.Controls.Add(lnk);
                menutag.Controls.Add(li);
            }

        }       
    }

    
    
}
