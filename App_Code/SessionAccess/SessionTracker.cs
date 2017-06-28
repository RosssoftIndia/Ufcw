using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.UI;

 
/// <summary>
/// Summary description for SessionTracker
/// </summary>
public class SessionTracker : System.Web.UI.Page
{
	public SessionTracker()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    # region System.Web.UI.Page Members

    /// 
    /// Raises the  event at the beginning of page initialization.
    /// 
    /// <param name="e">An  that contains the event data.
    protected override void OnPreInit(EventArgs e)
    {
        base.OnPreInit(e);

       // this.Theme = "MyTheme"; // adding theme
    }

    /// 
    /// Raises the  event.
    /// 
    /// <param name="e">The  object that contains the event data.
    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
     
    }


    /// 
    /// Raises the  event after postback data is loaded into the page server controls but before the  event.
    /// 
    /// <param name="e">An  that contains the event data.
    protected override void OnPreLoad(EventArgs e)
    {
        base.OnPreLoad(e);

        //this.Title = "MyApp"; // set page title

        //this.AddCSSLink("~/style.css"); // adding css link on head section

        //this.AddJavaScriptHeaderLink("~/script.js"); // adding js link on header section

        AuthenticateUser(); // validate user       
    }

    # endregion

    # region Links

    public virtual void AddCSSLink(string url)
    {
        HtmlLink link = new HtmlLink();

        link.Attributes.Add("type", "text/css");
        link.Attributes.Add("rel", "stylesheet");
        link.Href = link.ResolveUrl(url);

        Page.Header.Controls.Add(link);
    }

    public virtual void AddGenericLink(string relation, string title, string href)
    {
        HtmlLink link = new HtmlLink();
        link.Attributes["rel"] = relation;
        link.Attributes["title"] = title;
        link.Attributes["href"] = href;

        Page.Header.Controls.Add(link);
    }

    public virtual void AddGenericLink(string type, string relation, string title, string href)
    {
        HtmlLink link = new HtmlLink();
        link.Attributes["type"] = type;
        link.Attributes["rel"] = relation;
        link.Attributes["title"] = title;
        link.Attributes["href"] = href;

        Page.Header.Controls.Add(link);
    }

    public virtual void AddJavaScriptHeaderLink(string url)
    {
        HtmlGenericControl script = new HtmlGenericControl("script");

        script.Attributes.Add("type", "text/javascript");
        script.Attributes.Add("src", script.ResolveUrl(url));

        Page.Header.Controls.Add(script);
    }

    public virtual void AddJavaScriptBodyLink(string keyName, string url)
    {
        ClientScriptManager cs = Page.ClientScript;

        // Check to see if the include script exists already.
        if (!cs.IsClientScriptIncludeRegistered(this.GetType(), keyName))
        {
            cs.RegisterClientScriptInclude(this.GetType(), keyName, ResolveUrl(url));
        }
    }

    # endregion

    #region Authentication

    public virtual void AuthenticateUser()
    {
        Repository.Session.CheckSession("Ufcw_Userinfo");         
      
    }

    #endregion

}
