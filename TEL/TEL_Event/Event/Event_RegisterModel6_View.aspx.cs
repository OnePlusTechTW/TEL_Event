using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TEL.Event.Lab.Method;

public partial class Event_Event_RegisterModel6_View : System.Web.UI.Page
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
        string empid = Page.Session["EmpID"].ToString();
        string registerid = Request.QueryString["id"];


        UC_EventDescription.setViewDefault(eventid);
        InitFormValues(empid, eventid, registerid);
    }

    protected void btnGoBackPage_Click(object sender, EventArgs e)
    {
        string returnPage = "Default";

        if (Request.QueryString["page"] != null && !string.IsNullOrEmpty(Request.QueryString["page"]))
            returnPage = Request.QueryString["page"].ToString();

        Response.Redirect($"{returnPage}.aspx");
    }

    private void InitFormValues(string empid, string eventid, string registerid)
    {
        UserInfo userInfo = new UserInfo(empid);
        txtEmpid.Text = empid;
        txtCName.Text = userInfo.FullNameCH;
        txtEName.Text = userInfo.FullNameEN;
        txtDepartment.Text = $"{userInfo.UnitCode}-{userInfo.UnitName}";
        txtStation.Text = userInfo.Station;

        Event ev = new Event();
        DataTable dt = new DataTable();
        dt = ev.GetRegisterModel6(registerid);
        if (dt.Rows.Count > 0)
        {
            txtArea.Text = dt.Rows[0]["changearea"].ToString();
            txtAvaliabledate.Text = dt.Rows[0]["changedate"].ToString();

        }
        else
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Guid.NewGuid().ToString(), "ShowNoRegisterInfo();", true);
        }
    }
}