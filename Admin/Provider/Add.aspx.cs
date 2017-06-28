using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class Admin_Provider_Add : SessionTracker 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        menu.GenerateMenu("Provider");
        if (!Page.IsPostBack)
        {
           
              
        }
    }
   
    protected void btn_Submit_Click(object sender, EventArgs e)
    {
        Repository.Struct.SpResultset resultset = Repository.Providers.Add_Providers(txtname.Text,Pri_Address.Text, Pri_City.Text, Pri_State.Text, Pri_Zip.Text, Pri_Zip_Plus4.Text, Pri_Phone.Text, Pri_Fax.Text, Pri_Email.Text);
        if (resultset.Isresult)
        {

        }

    }
   
}
