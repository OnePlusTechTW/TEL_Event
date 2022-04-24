using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TEL.Event.Lab.Method;

public partial class Event_Event_RegisterModel2_View : System.Web.UI.Page
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
        string registerid = Request.QueryString["id"].ToString();


        UC_EventDescription.setViewDefault(eventid);
        InitFormValues(registerid);
        InitGridRegisterModel2family(registerid);
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

    protected void gridRegisterModel2family_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        string registerid = Request.QueryString["id"].ToString();

        gridRegisterModel2family.PageIndex = e.NewPageIndex;
        InitGridRegisterModel2family(registerid);
    }

    protected void gridRegisterModel2family_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //調整GridView資料行底色
            if (e.Row.RowIndex > -1)
            {
                //奇數行
                if (e.Row.RowIndex % 2 == 0)
                    e.Row.BackColor = Color.FromArgb(225, 225, 225);
                else
                    e.Row.BackColor = Color.FromArgb(245, 245, 245);
            }
        }
    }

    /// <summary>
    /// 初始表單
    /// </summary>
    /// <param name="empid"></param>
    private void InitFormValues(string registerid)
    {
        Event ev = new Event();
        DataTable dt = new DataTable();
        dt = ev.GetRegisterModel2(registerid);
        if (dt.Rows.Count > 0)
        {
            UserInfo userInfo = new UserInfo(dt.Rows[0]["empid"].ToString());
            txtEmpid.Text = dt.Rows[0]["empid"].ToString();
            txtCName.Text = userInfo.FullNameCH;
            txtEName.Text = userInfo.FullNameEN;
            txtDepartment.Text = userInfo.UnitName;
            txtStation.Text = userInfo.Station;
            txtID.Text = userInfo.NationalID;
            txtBDay.Text = userInfo.Birthday;
            txtGender.Text = userInfo.Gender;
            txtEmail.Text = userInfo.EMail;

            txtAttendContent.Text = dt.Rows[0]["selectedoption"].ToString();
            txtPhone.Text = dt.Rows[0]["mobile"].ToString();
            txtTransportation.Text = dt.Rows[0]["traffic"].ToString();
            txtMeal.Text = dt.Rows[0]["meal"].ToString();
            txtComment.Text = dt.Rows[0]["feedback"].ToString();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Guid.NewGuid().ToString(), "ShowNoRegisterInfo();", true);
        }
    }

    private void InitGridRegisterModel2family(string id)
    {
        Event ev = new Event();
        DataTable dt = new DataTable();
        dt = ev.GetRegisterModel2family(id);
        this.gridRegisterModel2family.DataSource = dt;
        this.gridRegisterModel2family.DataBind();
    }
}