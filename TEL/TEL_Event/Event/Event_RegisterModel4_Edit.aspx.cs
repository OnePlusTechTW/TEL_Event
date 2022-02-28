using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using TEL.Event.Lab.Method;

public partial class Event_Event_RegisterModel4_Edit : System.Web.UI.Page
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
        InitDDLValues(eventid);
        InitRBLValues(eventid);
        InitFormValues(eventid, registerid);
    }

    protected void btnSummit_Click(object sender, EventArgs e)
    {
        StringBuilder sb = new StringBuilder();
        //受診者身份別 必填
        if (string.IsNullOrEmpty(ddlIdentity.SelectedValue))
        {
            sb.AppendLine(string.Format(lblRequired.Text, lblIdentity.Text));
            sb.AppendLine("<br />");
        }

        //受診者中文姓名 必填
        if (string.IsNullOrEmpty(txtExamineename.Text))
        {
            sb.AppendLine(string.Format(lblRequired.Text, lblExamineename.Text));
            sb.AppendLine("<br />");
        }

        //受診者身分證字號 必填
        if (string.IsNullOrEmpty(txtExamineeidno.Text))
        {
            sb.AppendLine(string.Format(lblRequired.Text, lblExamineeidno.Text));
            sb.AppendLine("<br />");
        }

        //受診者出生年月日 必填
        if (string.IsNullOrEmpty(txtExamineebirthday.Text))
        {
            sb.AppendLine(string.Format(lblRequired.Text, lblExamineebirthday.Text));
            sb.AppendLine("<br />");
        }

        //受診者手機 必填
        if (string.IsNullOrEmpty(txtExamineemobile.Text))
        {
            sb.AppendLine(string.Format(lblRequired.Text, lblExamineemobile.Text));
            sb.AppendLine("<br />");
        }

        //健檢醫院 必填
        if (string.IsNullOrEmpty(ddlHosipital.SelectedValue))
        {
            sb.AppendLine(string.Format(lblRequired.Text, lblHosipital.Text));
            sb.AppendLine("<br />");
        }

        //地區 必填
        if (string.IsNullOrEmpty(ddlArea.SelectedValue))
        {
            sb.AppendLine(string.Format(lblRequired.Text, lblArea.Text));
            sb.AppendLine("<br />");
        }

        //費用&方案 必填
        if (string.IsNullOrEmpty(ddlSolution.SelectedValue))
        {
            sb.AppendLine(string.Format(lblRequired.Text, lblSolution.Text));
            sb.AppendLine("<br />");
        }

        //受診者性別 必填
        if (string.IsNullOrEmpty(ddlGender.SelectedValue))
        {
            sb.AppendLine(string.Format(lblRequired.Text, lblGender.Text));
            sb.AppendLine("<br />");
        }

        //期望受檢日 必填
        if (string.IsNullOrEmpty(ddlExpectdate.SelectedValue))
        {
            sb.AppendLine(string.Format(lblRequired.Text, lblExpectdate.Text));
            sb.AppendLine("<br />");
        }

        //備用受檢日 必填
        if (string.IsNullOrEmpty(ddlSeconddate.SelectedValue))
        {
            sb.AppendLine(string.Format(lblRequired.Text, lblSeconddate.Text));
            sb.AppendLine("<br />");
        }

        //健檢次方案1 必填
        if (string.IsNullOrEmpty(ddlSecondsolution1.SelectedValue))
        {
            sb.AppendLine(string.Format(lblRequired.Text, lblSecondsolution1.Text));
            sb.AppendLine("<br />");
        }

        //健檢次方案2 必填
        if (string.IsNullOrEmpty(ddlSecondsolution2.SelectedValue))
        {
            sb.AppendLine(string.Format(lblRequired.Text, lblSecondsolution2.Text));
            sb.AppendLine("<br />");
        }

        //健檢次方案3 必填
        if (string.IsNullOrEmpty(ddlSecondsolution3.SelectedValue))
        {
            sb.AppendLine(string.Format(lblRequired.Text, lblSecondsolution3.Text));
            sb.AppendLine("<br />");
        }

        //健檢包寄送地點 必填
        if (string.IsNullOrEmpty(rblAddress.SelectedValue) && rbtnOrther.Checked == false)
        {
            sb.AppendLine(string.Format(lblRequired.Text, lblAddress.Text));
            sb.AppendLine("<br />");
        }

        if (!string.IsNullOrEmpty(sb.ToString()))
        {
            lblMsg.Text = sb.ToString();
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Guid.NewGuid().ToString(), "ShowDialogMsg();", true);

            return;
        }

        bool flag = Regex.IsMatch(txtExamineeidno.Text, @"^[A-Za-z]{1}[1-2]{1}[0-9]{8}$");//先判定是否符合一個大寫字母+1或2開頭的1個數字+8個數字

        if (!flag)
        {
            lblMsg.Text = lblIDFormatErr.Text;
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Guid.NewGuid().ToString(), "ShowDialogMsg();", true);

            return;
        }

        Event ev = new Event();
        string eventid = Request.QueryString["eventid"];
        string modifiedby = Page.Session["EmpID"].ToString();
        string registerid = Request.QueryString["id"];

        int option1Limit = ev.GetRegisterOption4Limit(eventid, ddlHosipital.SelectedValue, ddlArea.SelectedValue, ddlSolution.SelectedValue, ddlGender.SelectedValue, ddlExpectdate.SelectedValue);
        int registerCount = ev.GetRegisterOption4Count(eventid, ddlHosipital.SelectedValue, ddlArea.SelectedValue, ddlSolution.SelectedValue, ddlGender.SelectedValue, ddlExpectdate.SelectedValue, registerid);

        if (registerCount >= option1Limit)
        {
            lblMsg.Text = lblLimitReached.Text;
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Guid.NewGuid().ToString(), "ShowDialogMsg();", true);

            return;
        }

        Dictionary<string, string> Data = new Dictionary<string, string>();
        Data.Add("id", registerid);
        Data.Add("eventid", eventid);
        Data.Add("empid", txtEmpid.Text);
        Data.Add("registerdate", DateTime.Now.ToString("yyyy/MM/dd HH:mm"));
        Data.Add("examineeidentity", ddlIdentity.SelectedValue);
        Data.Add("examineename", txtExamineename.Text);
        Data.Add("examineename2", txtExamineename2.Text);
        Data.Add("examineeidno", txtExamineeidno.Text);
        Data.Add("examineebirthday", txtExamineebirthday.Text);
        Data.Add("examineemobile", txtExamineemobile.Text);
        Data.Add("hosipital", ddlHosipital.SelectedValue);
        Data.Add("area", ddlArea.SelectedValue);
        Data.Add("solution", ddlSolution.SelectedValue);
        Data.Add("gender", ddlGender.SelectedValue);
        Data.Add("expectdate", ddlExpectdate.SelectedValue);
        Data.Add("seconddate", ddlSeconddate.SelectedValue);
        Data.Add("secondsolution1", ddlSecondsolution1.SelectedValue);
        Data.Add("secondsolution2", ddlSecondsolution2.SelectedValue);
        Data.Add("secondsolution3", ddlSecondsolution3.SelectedValue);
        Data.Add("optional", txtOptional.Text);

        string address = string.Empty;
        if (!string.IsNullOrEmpty(rblAddress.SelectedValue))
            address = rblAddress.SelectedValue;
        else if (rbtnOrther.Checked)
            address = txtOrther.Text;
        Data.Add("address", address);

        Data.Add("meal", ddlMeal.SelectedValue);
        Data.Add("needhotel", rblNeedhotel.SelectedValue);
        Data.Add("checkininfo", txtCheckininfo.Text);
        Data.Add("feedback", txtComment.Text);

        string result = ev.UpdateRegisterModel4(Data, modifiedby);

        if (string.IsNullOrEmpty(result))
        {
            //寄送報名完成通知信給員工
            SendRegisterSuccessMail();
        }
        else
        {
            lblErrMsg.Text = lblUpdateErrMsg.Text;

            string errMsg = $@"發生錯誤:{Environment.NewLine} 更新模板4報名資料發生錯誤。 {Environment.NewLine}" + result;
            LogHelper.WriteLog(errMsg);
            //失敗
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Guid.NewGuid().ToString(), "ShowDialogFailed();", true);
        }

    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        Event ev = new Event();
        string id = Request.QueryString["id"] + "|" + Page.Session["EmpID"].ToString();

        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Guid.NewGuid().ToString(), "ShowDialogDelete('" + id + "');", true);
    }

    protected void btnCannel_Click(object sender, EventArgs e)
    {
        string returnPage = "Default";
        string eventid = Request.QueryString["eventid"];

        if (Request.QueryString["page"] != null && !string.IsNullOrEmpty(Request.QueryString["page"]))
            returnPage = Request.QueryString["page"].ToString();

        if (returnPage == "Register")
        {
            returnPage = $"{returnPage}.aspx?id={eventid}";
        }
        else
        {
            returnPage = $"{returnPage}.aspx";
        }

        Response.Redirect(returnPage);
    }

    /// <summary>
    /// 回到原始頁面
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnGoBackPage_Click(object sender, EventArgs e)
    {
        string returnPage = "Default";
        string eventid = Request.QueryString["eventid"];

        if (Request.QueryString["page"] != null && !string.IsNullOrEmpty(Request.QueryString["page"]))
            returnPage = Request.QueryString["page"].ToString();

        if (returnPage == "Register")
        {
            returnPage = $"{returnPage}.aspx?id={eventid}";
        }
        else
        {
            returnPage = $"{returnPage}.aspx";
        }

        Response.Redirect(returnPage);
    }

    protected void ddlHosipital_SelectedIndexChanged(object sender, EventArgs e)
    {
        string eventid = Request.QueryString["eventid"];

        this.ddlArea.Enabled = true;
        BindddlArea(eventid, ddlHosipital.SelectedValue);

        ListItem li = new ListItem();
        li.Text = lblUnselect.Text;
        li.Value = string.Empty;
        li.Selected = true;

        this.ddlSolution.Items.Clear();
        this.ddlSolution.Enabled = false;
        this.ddlSolution.Items.Add(li);

        this.ddlGender.Items.Clear();
        this.ddlGender.Enabled = false;
        this.ddlGender.Items.Add(li);

        this.ddlExpectdate.Items.Clear();
        this.ddlExpectdate.Enabled = false;
        this.ddlExpectdate.Items.Add(li);

        this.ddlSeconddate.Items.Clear();
        this.ddlSeconddate.Enabled = false;
        this.ddlSeconddate.Items.Add(li);

        this.ddlSecondsolution1.Items.Clear();
        this.ddlSecondsolution1.Enabled = false;
        this.ddlSecondsolution1.Items.Add(li);

        this.ddlSecondsolution2.Items.Clear();
        this.ddlSecondsolution2.Enabled = false;
        this.ddlSecondsolution2.Items.Add(li);

        this.ddlSecondsolution3.Items.Clear();
        this.ddlSecondsolution3.Enabled = false;
        this.ddlSecondsolution3.Items.Add(li);
    }

    protected void ddlArea_SelectedIndexChanged(object sender, EventArgs e)
    {
        string eventid = Request.QueryString["eventid"];
        this.ddlSolution.Enabled = true;

        BindddlSolution(eventid, ddlHosipital.SelectedValue, ddlArea.SelectedValue);


        ListItem li = new ListItem();
        li.Text = lblUnselect.Text;
        li.Value = string.Empty;
        li.Selected = true;

        this.ddlGender.Items.Clear();
        this.ddlGender.Enabled = false;
        this.ddlGender.Items.Add(li);

        this.ddlExpectdate.Items.Clear();
        this.ddlExpectdate.Enabled = false;
        this.ddlExpectdate.Items.Add(li);

        this.ddlSeconddate.Items.Clear();
        this.ddlSeconddate.Enabled = false;
        this.ddlSeconddate.Items.Add(li);

        this.ddlSecondsolution1.Items.Clear();
        this.ddlSecondsolution1.Enabled = false;
        this.ddlSecondsolution1.Items.Add(li);

        this.ddlSecondsolution2.Items.Clear();
        this.ddlSecondsolution2.Enabled = false;
        this.ddlSecondsolution2.Items.Add(li);

        this.ddlSecondsolution3.Items.Clear();
        this.ddlSecondsolution3.Enabled = false;
        this.ddlSecondsolution3.Items.Add(li);
    }

    protected void ddlSolution_SelectedIndexChanged(object sender, EventArgs e)
    {
        string eventid = Request.QueryString["eventid"];
        ddlGender.Enabled = true;
        BindddlGender(eventid, ddlHosipital.SelectedValue, ddlArea.SelectedValue, ddlSolution.SelectedValue);


        ListItem li = new ListItem();
        li.Text = lblUnselect.Text;
        li.Value = string.Empty;
        li.Selected = true;

        this.ddlExpectdate.Items.Clear();
        this.ddlExpectdate.Enabled = false;
        this.ddlExpectdate.Items.Add(li);

        this.ddlSeconddate.Items.Clear();
        this.ddlSeconddate.Enabled = false;
        this.ddlSeconddate.Items.Add(li);

        this.ddlSecondsolution1.Items.Clear();
        this.ddlSecondsolution1.Enabled = false;
        this.ddlSecondsolution1.Items.Add(li);

        this.ddlSecondsolution2.Items.Clear();
        this.ddlSecondsolution2.Enabled = false;
        this.ddlSecondsolution2.Items.Add(li);

        this.ddlSecondsolution3.Items.Clear();
        this.ddlSecondsolution3.Enabled = false;
        this.ddlSecondsolution3.Items.Add(li);
    }

    protected void ddlGender_SelectedIndexChanged(object sender, EventArgs e)
    {
        string eventid = Request.QueryString["eventid"];

        //受診者性別
        this.ddlExpectdate.Enabled = true;
        this.ddlSeconddate.Enabled = true;
        this.ddlSecondsolution1.Enabled = true;
        this.ddlSecondsolution2.Enabled = true;
        this.ddlSecondsolution3.Enabled = true;

        BindOrderHealthGroupDLL(eventid, ddlHosipital.SelectedValue, ddlArea.SelectedValue, ddlSolution.SelectedValue, ddlGender.SelectedValue);

    }

    protected void rblAddress_SelectedIndexChanged(object sender, EventArgs e)
    {
        rbtnOrther.Checked = false;
        txtOrther.Text = string.Empty;
        txtOrther.Enabled = false;
    }

    protected void rbtnOrther_CheckedChanged(object sender, EventArgs e)
    {
        txtOrther.Enabled = true;

        if (rblAddress.SelectedItem != null)
            rblAddress.SelectedItem.Selected = false;
    }

    protected void rblNeedhotel_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rblNeedhotel.SelectedValue == "是")
            txtCheckininfo.Enabled = true;
        else
        {
            txtCheckininfo.Enabled = false;
            txtCheckininfo.Text = string.Empty;
        }
    }

    private void InitRBLValues(string eventid)
    {
        Event ev = new Event();

        //健檢包寄送地點
        this.rblAddress.Items.Clear();

        DataTable dt = new DataTable();
        dt = ev.GetHealthAddressOption(eventid);

        foreach (DataRow rs in dt.Rows)
        {
            ListItem li1 = new ListItem();
            li1.Text = rs["description"].ToString();
            li1.Value = rs["description"].ToString();

            this.rblAddress.Items.Add(li1);
        }
    }

    private void InitDDLValues(string eventid)
    {
        Event ev = new Event();

        //健檢醫院
        this.ddlHosipital.Items.Clear();

        ListItem li = new ListItem();
        li.Text = lblUnselect.Text;
        li.Value = string.Empty;
        li.Selected = true;
        this.ddlHosipital.Items.Add(li);

        DataTable dtHosipital = new DataTable();
        dtHosipital = ev.GetHosipitalOption(eventid);

        foreach (DataRow rs in dtHosipital.Rows)
        {
            ListItem li1 = new ListItem();
            li1.Text = rs["hosipital"].ToString();
            li1.Value = rs["hosipital"].ToString();

            this.ddlHosipital.Items.Add(li1);
        }

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

            ddlIdentity.SelectedValue = dt.Rows[0]["examineeidentity"].ToString();
            txtExamineename.Text = dt.Rows[0]["examineename"].ToString();
            txtExamineename2.Text = dt.Rows[0]["examineename2"].ToString();

            txtExamineeidno.Text = dt.Rows[0]["examineeidno"].ToString();
            txtExamineebirthday.Text = dt.Rows[0]["examineebirthday"].ToString();
            txtExamineemobile.Text = dt.Rows[0]["examineemobile"].ToString();

            ddlHosipital.SelectedValue = dt.Rows[0]["hosipital"].ToString();
            BindddlArea(eventid, dt.Rows[0]["hosipital"].ToString());

            ddlArea.SelectedValue = dt.Rows[0]["area"].ToString();
            BindddlSolution(eventid, dt.Rows[0]["hosipital"].ToString(), dt.Rows[0]["area"].ToString());

            ddlSolution.SelectedValue = dt.Rows[0]["solution"].ToString();
            BindddlGender(eventid, dt.Rows[0]["hosipital"].ToString(), dt.Rows[0]["area"].ToString(), dt.Rows[0]["solution"].ToString());

            ddlGender.SelectedValue = dt.Rows[0]["gender"].ToString();
            BindOrderHealthGroupDLL(eventid, dt.Rows[0]["hosipital"].ToString(), dt.Rows[0]["area"].ToString(), dt.Rows[0]["solution"].ToString(), dt.Rows[0]["gender"].ToString());

            string a = Convert.ToDateTime(dt.Rows[0]["expectdate"].ToString()).ToString("yyyy/MM/mm");
            ddlExpectdate.SelectedValue = Convert.ToDateTime(dt.Rows[0]["expectdate"].ToString()).ToString("yyyy/MM/dd");
            ddlSeconddate.SelectedValue = Convert.ToDateTime(dt.Rows[0]["seconddate"].ToString()).ToString("yyyy/MM/dd");
            ddlSecondsolution1.SelectedValue = dt.Rows[0]["secondsolution1"].ToString();
            ddlSecondsolution2.SelectedValue = dt.Rows[0]["secondsolution2"].ToString();
            ddlSecondsolution3.SelectedValue = dt.Rows[0]["secondsolution3"].ToString();
            txtOptional.Text = dt.Rows[0]["optional"].ToString();

            if (rblAddress.Items.FindByValue(dt.Rows[0]["address"].ToString()) == null)
            {
                rbtnOrther.Checked = true;
                txtOrther.Text = dt.Rows[0]["address"].ToString();
            }
            else
                rblAddress.Items.FindByValue(dt.Rows[0]["address"].ToString()).Selected = true;

            ddlMeal.SelectedValue = dt.Rows[0]["meal"].ToString();
            rblNeedhotel.SelectedValue = dt.Rows[0]["needhotel"].ToString();

            if (dt.Rows[0]["needhotel"].ToString() == "是")
            {
                txtCheckininfo.Enabled = true;
                txtCheckininfo.Text = dt.Rows[0]["checkininfo"].ToString();
            }
            else
                txtCheckininfo.Enabled = false;

            txtComment.Text = dt.Rows[0]["feedback"].ToString();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Guid.NewGuid().ToString(), "ShowNoRegisterInfo();", true);
        }

    }

    private void BindddlArea(string eventid, string hosipital)
    {
        Event ev = new Event();

        //地區
        this.ddlArea.Items.Clear();

        ListItem li = new ListItem();
        li.Text = lblUnselect.Text;
        li.Value = string.Empty;
        li.Selected = true;
        this.ddlArea.Items.Add(li);


        DataTable dt = new DataTable();
        dt = ev.GetAreaOption(eventid, ddlHosipital.SelectedValue);

        foreach (DataRow rs in dt.Rows)
        {
            ListItem li1 = new ListItem();
            li1.Text = rs["area"].ToString();
            li1.Value = rs["area"].ToString();

            this.ddlArea.Items.Add(li1);
        }
    }

    private void BindddlSolution(string eventid, string hosipital, string aea)
    {
        Event ev = new Event();

        this.ddlSolution.Items.Clear();

        ListItem li = new ListItem();
        li.Text = lblUnselect.Text;
        li.Value = string.Empty;
        li.Selected = true;
        this.ddlSolution.Items.Add(li);


        DataTable dt = new DataTable();
        dt = ev.GetSolutionOption(eventid, hosipital, aea);

        foreach (DataRow rs in dt.Rows)
        {
            ListItem li1 = new ListItem();
            li1.Text = rs["description"].ToString();
            li1.Value = rs["description"].ToString();

            this.ddlSolution.Items.Add(li1);
        }
    }
    private void BindddlGender(string eventid, string hosipital, string aea, string solution)
    {
        Event ev = new Event();

        //受診者性別
        this.ddlGender.Items.Clear();

        ListItem li = new ListItem();
        li.Text = lblUnselect.Text;
        li.Value = string.Empty;
        li.Selected = true;
        this.ddlGender.Items.Add(li);


        DataTable dt = new DataTable();
        dt = ev.GetGenderOption(eventid, hosipital, aea, solution);

        foreach (DataRow rs in dt.Rows)
        {
            ListItem li1 = new ListItem();
            li1.Text = rs["gender"].ToString();
            li1.Value = rs["gender"].ToString();

            this.ddlGender.Items.Add(li1);
        }
    }
    private void BindOrderHealthGroupDLL(string eventid, string hosipital, string aea, string solution, string gender)
    {
        Event ev = new Event();

        this.ddlExpectdate.Items.Clear();
        this.ddlSeconddate.Items.Clear();
        this.ddlSecondsolution1.Items.Clear();
        this.ddlSecondsolution2.Items.Clear();
        this.ddlSecondsolution3.Items.Clear();

        ListItem li = new ListItem();
        li.Text = lblUnselect.Text;
        li.Value = string.Empty;
        li.Selected = true;
        this.ddlExpectdate.Items.Add(li);
        this.ddlSeconddate.Items.Add(li);
        this.ddlSecondsolution1.Items.Add(li);
        this.ddlSecondsolution2.Items.Add(li);
        this.ddlSecondsolution3.Items.Add(li);
        DataTable dt = new DataTable();
        dt = ev.GetExpectdateOption(eventid, hosipital, aea, solution, gender);

        foreach (DataRow rs in dt.Rows)
        {
            ListItem li1 = new ListItem();
            li1.Text = rs["avaliabledate"].ToString();
            li1.Value = rs["avaliabledate"].ToString();

            this.ddlExpectdate.Items.Add(li1);
        }

        foreach (DataRow rs in dt.Rows)
        {
            ListItem li1 = new ListItem();
            li1.Text = rs["avaliabledate"].ToString();
            li1.Value = rs["avaliabledate"].ToString();

            this.ddlSeconddate.Items.Add(li1);
        }

        DataTable dtSecondsolution1 = new DataTable();
        dtSecondsolution1 = ev.GetSecondoption1Option(eventid, hosipital, aea, solution, gender);

        foreach (DataRow rs in dtSecondsolution1.Rows)
        {
            ListItem li1 = new ListItem();
            li1.Text = rs["secondoption1"].ToString();
            li1.Value = rs["secondoption1"].ToString();

            this.ddlSecondsolution1.Items.Add(li1);
        }

        DataTable dtSecondsolution2 = new DataTable();
        dtSecondsolution2 = ev.GetSecondoption2Option(eventid, hosipital, aea, solution, gender);

        foreach (DataRow rs in dtSecondsolution2.Rows)
        {
            ListItem li1 = new ListItem();
            li1.Text = rs["secondoption2"].ToString();
            li1.Value = rs["secondoption2"].ToString();

            this.ddlSecondsolution2.Items.Add(li1);
        }

        DataTable dtSecondsolution3 = new DataTable();
        dtSecondsolution3 = ev.GetSecondoption3Option(eventid, hosipital, aea, solution, gender);

        foreach (DataRow rs in dtSecondsolution3.Rows)
        {
            ListItem li1 = new ListItem();
            li1.Text = rs["secondoption3"].ToString();
            li1.Value = rs["secondoption3"].ToString();

            this.ddlSecondsolution3.Items.Add(li1);
        }
    }

    /// <summary>
    /// 刪除報名資料
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [WebMethod]
    public static string DeleteRegisterModel(string id)
    {
        Event ev = new Event();
        //Request.QueryString["id"] + "|" + Page.Session["EmpID"].ToString();
        string registerid = id.Split('|')[0];
        string modifiedby = id.Split('|')[1];

        string result = ev.DeleteRegisterModel4(registerid, modifiedby);

        if (!string.IsNullOrEmpty(result))
        {
            string errMsg = $@"發生錯誤:{Environment.NewLine} 刪除模板4報名資料發生錯誤。 {Environment.NewLine}" + result;
            LogHelper.WriteLog(errMsg);

            //失敗
            throw new Exception("Failed");
        }

        return "Success";
    }

    private void SendRegisterSuccessMail()
    {
        Event ev = new Event();
        string eventid = Request.QueryString["eventid"];
        EventInfo eventInfo = new EventInfo(eventid);

        ev.GetEventInfo(eventid);

        string strSender = "FiestaSystem@tel.com";
        string strSubject = $"【通知】活動填寫完成_{eventInfo.EventName}";
        string strDisplay = "Fiesta System";
        StringBuilder sbBody = new StringBuilder();
        DataTable dtRecipient = new DataTable();

        string registerEditLink = HttpContext.Current.Request.Url.AbsoluteUri.Replace("/Event/Event.aspx", $"/Event/Event_RegisterModel{eventInfo.EventRegisterModel}_Edit.aspx?id={eventid}&page=Default");
        string registerDefaultLink = HttpContext.Current.Request.Url.AbsoluteUri.Replace("/Event/Event.aspx", $"/Event/Default.aspx");
        sbBody.Append("<div>");
        sbBody.Append("<div>您好:</div>");
        sbBody.Append("<div><br></div>");
        sbBody.Append($"<div>此封信件為通知您參與了『<a href='{registerEditLink}'>{eventInfo.EventName}（超連結）</a>』，並完成報名。</div>");
        sbBody.Append("<div><br></div>");
        sbBody.Append($"<div>相關報名資訊，可以至網站『<a href='{registerDefaultLink}'>我的活動（超連結）</a>』頁面中查看！</div>");
        sbBody.Append("<div>如果有任何問題請聯絡活動單位負責人，謝謝。</div>");
        sbBody.Append("<div><br></div>");
        sbBody.Append("<div><span style='color: #595959;'>※此信件為系統發送通知使用，請勿直接回覆。</span></div>");
        sbBody.Append("</div>");

        if (SenMail.SendMail(strSender, dtRecipient, strSubject, sbBody.ToString(), strDisplay))
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Guid.NewGuid().ToString(), "ShowRegisterSccessDialog();", true);
        }
        else
        {
            lblRegisterSccess.Text = $"{lblRegisterSccess.Text}，{lblSendMailFailed.Text}";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Guid.NewGuid().ToString(), "ShowRegisterSccessDialog();", true);
        }
    }
}