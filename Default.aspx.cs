using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;



public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
       
    }
    protected void btn_Login_Click(object sender, EventArgs e)
    {
        lblmsg.Text = Repository.Users.Isvalid(txt_Username.Text, txt_Password.Text);     //AccountRepository.Login(txt_Username.Text,txt_Password.Text);  
    }
}
