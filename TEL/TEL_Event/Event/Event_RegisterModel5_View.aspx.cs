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

public partial class Event_Event_RegisterModel5_View : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["id"] == null || string.IsNullOrEmpty(Request.QueryString["id"]))
            Response.Redirect("Default.aspx");

        if (!IsPostBack)
            SetDefaultValues();
    }
    private void SetDefaultValues()
    {
        string eventid = Request.QueryString["eventid"];
        string registerid = Request.QueryString["id"];
        string empid = Page.Session["EmpID"].ToString();

        UC_EventDescription.setViewDefault(eventid);
        InitFormValues(registerid);
    }

    protected void btnGoBackPage_Click(object sender, EventArgs e)
    {
        string returnPage = "Default";

        if (Request.QueryString["page"] != null && !string.IsNullOrEmpty(Request.QueryString["page"]))
            returnPage = Request.QueryString["page"].ToString();

        Response.Redirect($"{returnPage}.aspx");
    }

    /// <summary>
    /// 初始表單
    /// </summary>
    /// <param name="empid"></param>
    private void InitFormValues(string registerid)
    {
        Event ev = new Event();
        DataTable dt = new DataTable();
        dt = ev.GetRegisterModel5(registerid);
        if (dt.Rows.Count > 0)
        {
            UserInfo userInfo = new UserInfo(dt.Rows[0]["empid"].ToString());
            txtEmpid.Text = dt.Rows[0]["empid"].ToString();
            txtCName.Text = userInfo.FullNameCH;
            txtEName.Text = userInfo.FullNameEN;
            txtDepartment.Text = $"{userInfo.UnitCode}-{userInfo.UnitName}";
            txtStation.Text = userInfo.Station;

            string path = ConfigurationManager.AppSettings.Get("EventThumbnailPath");
            if (string.IsNullOrEmpty(path))
                path = "~/App_Data/EventThumbnail";

            if (!string.IsNullOrEmpty(dt.Rows[0]["attachment1"].ToString()))
            {
                hlnkFileUpload1.Visible = true;
                hlnkFileUpload1.NavigateUrl = Path.Combine(path, dt.Rows[0]["attachment1"].ToString());//"~/Sample/Import_HealthGroup.xlsx";
                hlnkFileUpload1.Text = dt.Rows[0]["attachment1_name"].ToString();

            }

            txtDescription1.Text = dt.Rows[0]["description1"].ToString();


            if (!string.IsNullOrEmpty(dt.Rows[0]["attachment2"].ToString()))
            {
                hlnkFileUpload2.Visible = true;
                hlnkFileUpload2.NavigateUrl = Path.Combine(path, dt.Rows[0]["attachment2"].ToString());//"~/Sample/Import_HealthGroup.xlsx";
                hlnkFileUpload2.Text = dt.Rows[0]["attachment2_name"].ToString();

            }
            txtDescription2.Text = dt.Rows[0]["description2"].ToString();

            if (!string.IsNullOrEmpty(dt.Rows[0]["attachment3"].ToString()))
            {
                hlnkFileUpload3.Visible = true;
                hlnkFileUpload3.NavigateUrl = Path.Combine(path, dt.Rows[0]["attachment3"].ToString());//"~/Sample/Import_HealthGroup.xlsx";
                hlnkFileUpload3.Text = dt.Rows[0]["attachment3_name"].ToString();

            }
            txtDescription3.Text = dt.Rows[0]["description3"].ToString();

        }
        else
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Guid.NewGuid().ToString(), "ShowNoRegisterInfo();", true);
        }
    }
}