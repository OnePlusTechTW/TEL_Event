using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TEL.Event.Lab.Method;

public partial class Event_Event_RegisterModel4_View : System.Web.UI.Page
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

        bool mealEnable = false;
        if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings.Get("RegisterModel4MealEnable")))
        {
            mealEnable = Convert.ToBoolean(ConfigurationManager.AppSettings.Get("RegisterModel4MealEnable"));
        }
        lblMeal.Visible = mealEnable;
        txtMeal.Visible = mealEnable;
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
        dt = ev.GetRegisterModel4(registerid);
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
            txtExamineename2.Text = dt.Rows[0]["examineename2"].ToString();

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

            txtAddress.Text = dt.Rows[0]["address"].ToString();
            txtMeal.Text = dt.Rows[0]["meal"].ToString();
            txtOptional.Text = dt.Rows[0]["optional"].ToString();
            txtNeedhotel.Text = dt.Rows[0]["needhotel"].ToString();
            txtCheckininfo.Text = dt.Rows[0]["checkininfo"].ToString();
            txtComment.Text = dt.Rows[0]["feedback"].ToString();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Guid.NewGuid().ToString(), "ShowNoRegisterInfo();", true);
        }

    }

    //private void BindddlArea(string eventid, string hosipital)
    //{
    //    Event ev = new Event();

    //    //地區
    //    this.ddlArea.Items.Clear();

    //    ListItem li = new ListItem();
    //    li.Text = lblUnselect.Text;
    //    li.Value = string.Empty;
    //    li.Selected = true;
    //    this.ddlArea.Items.Add(li);


    //    DataTable dt = new DataTable();
    //    dt = ev.GetAreaOption(eventid, ddlHosipital.SelectedValue);

    //    foreach (DataRow rs in dt.Rows)
    //    {
    //        ListItem li1 = new ListItem();
    //        li1.Text = rs["area"].ToString();
    //        li1.Value = rs["area"].ToString();

    //        this.ddlArea.Items.Add(li1);
    //    }
    //}

    //private void BindddlSolution(string eventid, string hosipital, string aea)
    //{
    //    Event ev = new Event();

    //    this.ddlSolution.Items.Clear();

    //    ListItem li = new ListItem();
    //    li.Text = lblUnselect.Text;
    //    li.Value = string.Empty;
    //    li.Selected = true;
    //    this.ddlSolution.Items.Add(li);


    //    DataTable dt = new DataTable();
    //    dt = ev.GetSolutionOption(eventid, hosipital, aea);

    //    foreach (DataRow rs in dt.Rows)
    //    {
    //        ListItem li1 = new ListItem();
    //        li1.Text = rs["description"].ToString();
    //        li1.Value = rs["description"].ToString();

    //        this.ddlSolution.Items.Add(li1);
    //    }
    //}
    //private void BindddlGender(string eventid, string hosipital, string aea, string solution)
    //{
    //    Event ev = new Event();

    //    //受診者性別
    //    this.ddlGender.Items.Clear();

    //    ListItem li = new ListItem();
    //    li.Text = lblUnselect.Text;
    //    li.Value = string.Empty;
    //    li.Selected = true;
    //    this.ddlGender.Items.Add(li);


    //    DataTable dt = new DataTable();
    //    dt = ev.GetGenderOption(eventid, hosipital, aea, solution);

    //    foreach (DataRow rs in dt.Rows)
    //    {
    //        ListItem li1 = new ListItem();
    //        li1.Text = rs["gender"].ToString();
    //        li1.Value = rs["gender"].ToString();

    //        this.ddlGender.Items.Add(li1);
    //    }
    //}
    //private void BindOrderHealthGroupDLL(string eventid, string hosipital, string aea, string solution, string gender)
    //{
    //    Event ev = new Event();

    //    this.ddlExpectdate.Items.Clear();
    //    this.ddlSeconddate.Items.Clear();
    //    this.ddlSecondsolution1.Items.Clear();
    //    this.ddlSecondsolution2.Items.Clear();
    //    this.ddlSecondsolution3.Items.Clear();

    //    ListItem li = new ListItem();
    //    li.Text = lblUnselect.Text;
    //    li.Value = string.Empty;
    //    li.Selected = true;
    //    this.ddlExpectdate.Items.Add(li);
    //    this.ddlSeconddate.Items.Add(li);
    //    this.ddlSecondsolution1.Items.Add(li);
    //    this.ddlSecondsolution2.Items.Add(li);
    //    this.ddlSecondsolution3.Items.Add(li);
    //    DataTable dt = new DataTable();
    //    dt = ev.GetExpectdateOption(eventid, hosipital, aea, solution, gender);

    //    foreach (DataRow rs in dt.Rows)
    //    {
    //        ListItem li1 = new ListItem();
    //        li1.Text = rs["avaliabledate"].ToString();
    //        li1.Value = rs["avaliabledate"].ToString();

    //        this.ddlExpectdate.Items.Add(li1);
    //    }

    //    foreach (DataRow rs in dt.Rows)
    //    {
    //        ListItem li1 = new ListItem();
    //        li1.Text = rs["avaliabledate"].ToString();
    //        li1.Value = rs["avaliabledate"].ToString();

    //        this.ddlSeconddate.Items.Add(li1);
    //    }

    //    DataTable dtSecondsolution1 = new DataTable();
    //    dtSecondsolution1 = ev.GetSecondoption1Option(eventid, hosipital, aea, solution, gender);

    //    foreach (DataRow rs in dtSecondsolution1.Rows)
    //    {
    //        ListItem li1 = new ListItem();
    //        li1.Text = rs["secondoption1"].ToString();
    //        li1.Value = rs["secondoption1"].ToString();

    //        this.ddlSecondsolution1.Items.Add(li1);
    //    }

    //    DataTable dtSecondsolution2 = new DataTable();
    //    dtSecondsolution2 = ev.GetSecondoption2Option(eventid, hosipital, aea, solution, gender);

    //    foreach (DataRow rs in dtSecondsolution2.Rows)
    //    {
    //        ListItem li1 = new ListItem();
    //        li1.Text = rs["secondoption2"].ToString();
    //        li1.Value = rs["secondoption2"].ToString();

    //        this.ddlSecondsolution2.Items.Add(li1);
    //    }

    //    DataTable dtSecondsolution3 = new DataTable();
    //    dtSecondsolution3 = ev.GetSecondoption3Option(eventid, hosipital, aea, solution, gender);

    //    foreach (DataRow rs in dtSecondsolution3.Rows)
    //    {
    //        ListItem li1 = new ListItem();
    //        li1.Text = rs["secondoption3"].ToString();
    //        li1.Value = rs["secondoption3"].ToString();

    //        this.ddlSecondsolution3.Items.Add(li1);
    //    }
    //}
}