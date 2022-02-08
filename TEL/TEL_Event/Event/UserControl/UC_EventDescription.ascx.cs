using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TEL.Event.Lab.Method;

public partial class Event_UserControl_UC_EventDescription : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    public void setViewDefault(string txt)
    {
        Event ev = new Event();
        DataTable dt = ev.GetEventInfo(txt);

        this.category.BgColor = dt.Rows[0]["categorycolor"].ToString();
        this.lblCategoryName.Text = dt.Rows[0]["categoryname"].ToString();

        this.lblEventName.Text = dt.Rows[0]["eventname"].ToString();

        string path = ConfigurationManager.AppSettings.Get("EventThumbnailPath");
        if (string.IsNullOrEmpty(path))
            path = "~/App_Data/EventThumbnail";

        if (string.IsNullOrEmpty(dt.Rows[0]["image2"].ToString()))
            Image1.Visible = false;
        else
        {
            Image1.Visible = true;
            Image1.ImageUrl = Path.Combine(path, dt.Rows[0]["image2"].ToString());
        }

        lblEventDateValue.Text = $"{dt.Rows[0]["eventstart"].ToString()}~{dt.Rows[0]["eventend"].ToString()}";
        lblSignupDateValue.Text = $"{Convert.ToDateTime(dt.Rows[0]["registerstart"].ToString()).ToString("yyyy/MM/dd HH:mm")}~{Convert.ToDateTime( dt.Rows[0]["registerend"].ToString()).ToString("yyyy/MM/dd HH:mm")}";


        this.content.InnerHtml = HttpUtility.HtmlDecode(dt.Rows[0]["description"].ToString());
    }
}