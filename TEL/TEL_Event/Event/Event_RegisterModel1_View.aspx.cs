using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TEL.Event.Lab.Method;

public partial class Event_Event_RegisterModel1_View : System.Web.UI.Page
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
        string empid = Page.Session["EmpID"].ToString();
        string eventid = Request.QueryString["eventid"];
        string id = Request.QueryString["id"].ToString();


        UC_EventDescription.setViewDefault(eventid);
        InitFormValues(empid, id);
    }

    

    /// <summary>
    /// 初始表單
    /// </summary>
    /// <param name="empid"></param>
    private void InitFormValues(string empid, string id)
    {
        UserInfo userInfo = new UserInfo(empid);
        //user info
        txtEmpid.Text = empid;
        txtCName.Text = userInfo.FullNameCH;
        txtEName.Text = userInfo.FullNameEN;
        txtDepartment.Text = $"{userInfo.UnitCode}-{userInfo.UnitName}";
        txtStation.Text = userInfo.Station;

        //form info
        Event ev = new Event();
        DataTable dt = new DataTable();
        dt = ev.GetRegisterModel1(id);
        if (dt.Rows.Count > 0)
        {
            txtAttendContent.Text = dt.Rows[0]["selectedoption"].ToString();
            txtComment.Text = dt.Rows[0]["feedback"].ToString();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Guid.NewGuid().ToString(), "ShowRegisterSccessDialog();", true);
        }
    }
}