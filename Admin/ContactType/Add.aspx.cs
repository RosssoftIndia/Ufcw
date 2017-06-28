using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class Admin_ContactType_Add : SessionTracker 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        menu.GenerateMenu("ContactType");
    }
   
    protected void btn_Submit_Click(object sender, EventArgs e)
    {
        Repository.Struct.SpResultset resultset = Repository.ContType.Add_ContType(txtname.Text);
        if (resultset.Isresult)
        {

        }
    }
}
