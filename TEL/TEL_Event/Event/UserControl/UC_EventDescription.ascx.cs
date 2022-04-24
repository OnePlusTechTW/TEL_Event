using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
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

        //this.category.BgColor = dt.Rows[0]["categorycolor"].ToString();
        this.lblCategoryName.Text = dt.Rows[0]["categoryname"].ToString();
        this.lblCategoryName.ForeColor = transColors(dt.Rows[0]["categorycolor"].ToString());

        this.lblEventName.Text = dt.Rows[0]["eventname"].ToString();

        string path = ConfigurationManager.AppSettings.Get("EventThumbnailPath");
        if (string.IsNullOrEmpty(path))
            path = "~/App_Data/EventThumbnail";

        if (string.IsNullOrEmpty(dt.Rows[0]["image2"].ToString()))
        {
            Image1.Visible = false;
        }
        else
        {
            Image1.Visible = true;
            Image1.ImageUrl = Path.Combine(path, dt.Rows[0]["image2"].ToString());
        }

        lblEventDateValue.Text = $"{dt.Rows[0]["eventstart"].ToString()}~{dt.Rows[0]["eventend"].ToString()}";
        lblSignupDateValue.Text = $"{Convert.ToDateTime(dt.Rows[0]["registerstart"].ToString()).ToString("yyyy/MM/dd")}~{Convert.ToDateTime( dt.Rows[0]["registerend"].ToString()).ToString("yyyy/MM/dd")}";


        this.content.InnerHtml = HttpUtility.HtmlDecode(dt.Rows[0]["description"].ToString());
    }

    private Color transColors(string colorCode)
    {
        switch (colorCode)
        {
            case "#00A9E0":
                return Color.FromArgb(0, 169, 224);
            case "#71C5E8":
                return Color.FromArgb(113, 197, 232);
            case "#00629B":
                return Color.FromArgb(0, 98, 155);
            case "#78BE20":
                return Color.FromArgb(120, 190, 32);
            case "#B7DD79":
                return Color.FromArgb(183, 221, 121);
            case "#658D1B":
                return Color.FromArgb(101, 141, 27);
            case "#DA1884":
                return Color.FromArgb(218, 24, 132);
            case "#F395C7":
                return Color.FromArgb(243, 149, 199);
            case "#A50050":
                return Color.FromArgb(165, 0, 80);
            case "#00B2A9":
                return Color.FromArgb(0, 178, 169);
            case "#9CDBD9":
                return Color.FromArgb(156, 219, 217);
            case "#007367":
                return Color.FromArgb(0, 115, 103);
            case "#8031A7":
                return Color.FromArgb(128, 49, 167);
            case "#CAA2DD":
                return Color.FromArgb(202, 162, 221);
            case "#572C5F":
                return Color.FromArgb(87, 44, 95);
            case "#EEDC00":
                return Color.FromArgb(238, 220, 0);
            case "#F0EC74":
                return Color.FromArgb(240, 236, 116);
            case "#BBA600":
                return Color.FromArgb(187, 166, 0);
            case "#FF6A13":
                return Color.FromArgb(255, 106, 19);
            case "#FAAA8D":
                return Color.FromArgb(250, 170, 141);
            case "#A65523":
                return Color.FromArgb(166, 85, 35);
            default:
                return Color.FromArgb(0, 0, 0);

        }
    }
}