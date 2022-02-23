using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Event_Event_View : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["id"] == null || string.IsNullOrEmpty(Request.QueryString["id"]))
            Response.Redirect("Event.aspx");

        if (!IsPostBack)
        {
            string id = Request.QueryString["id"];
            if (!string.IsNullOrEmpty(id))
            {
                this.UC_EventDescription.setViewDefault(id);
            }
        }
    }
}