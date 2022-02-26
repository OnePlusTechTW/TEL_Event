using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TEL.Event.Lab.Method;

public partial class Event_Event_RegisterModel3_View : System.Web.UI.Page
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
        InitFormValues(eventid, registerid);
    }

    /// <summary>
    /// 回到原始頁面
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnGoBackPage_Click(object sender, EventArgs e)
    {
        string returnPage = "Default";

        if (Request.QueryString["page"] != null && !string.IsNullOrEmpty(Request.QueryString["page"]))
            returnPage = Request.QueryString["page"].ToString();

        Response.Redirect($"{returnPage}.aspx");
    }


    private void InitFormValues(string eventid, string registerid)
    {
        Event ev = new Event();
        DataTable dt = new DataTable();
        dt = ev.GetRegisterModel3(registerid);
        if (dt.Rows.Count > 0)
        {
            UserInfo userInfo = new UserInfo(dt.Rows[0]["empid"].ToString());
            txtEmpid.Text = dt.Rows[0]["empid"].ToString();
            txtCName.Text = userInfo.FullNameCH;
            txtEName.Text = userInfo.FullNameEN;
            txtDepartment.Text = $"{userInfo.UnitCode}-{userInfo.UnitName}";
            txtStation.Text = userInfo.Station;
            txtHealthGroup.Text = userInfo.HealthGroup;

            txtIdentity.Text = dt.Rows[0]["examineeidentity"].ToString();
            txtExamineename.Text = dt.Rows[0]["examineename"].ToString();
            txtExamineeidno.Text = dt.Rows[0]["examineeidno"].ToString();
            txtExamineebirthday.Text = dt.Rows[0]["examineebirthday"].ToString();
            txtExamineemobile.Text = dt.Rows[0]["examineemobile"].ToString();

            txtHosipital.Text = dt.Rows[0]["hosipital"].ToString();
            txtArea.Text = dt.Rows[0]["area"].ToString();
            txtSolution.Text = dt.Rows[0]["solution"].ToString();
            txtGender.Text = dt.Rows[0]["gender"].ToString();

            txtExpectdate.Text = Convert.ToDateTime(dt.Rows[0]["expectdate"].ToString()).ToString("yyyy/MM/mm");
            txtSeconddate.Text = Convert.ToDateTime(dt.Rows[0]["seconddate"].ToString()).ToString("yyyy/MM/dd");
            txtSecondsolution1.Text = dt.Rows[0]["secondsolution1"].ToString();
            txtSecondsolution2.Text = dt.Rows[0]["secondsolution2"].ToString();
            txtSecondsolution3.Text = dt.Rows[0]["secondsolution3"].ToString();
            txtOptional.Text = dt.Rows[0]["optional"].ToString();

            txtAddress.Text = dt.Rows[0]["address"].ToString();
            txtMeal.Text = dt.Rows[0]["meal"].ToString();
            txtOptional.Text = dt.Rows[0]["optional"].ToString();
            txtComment.Text = dt.Rows[0]["feedback"].ToString();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Guid.NewGuid().ToString(), "ShowNoRegisterInfo();", true);
        }

    }
   
}