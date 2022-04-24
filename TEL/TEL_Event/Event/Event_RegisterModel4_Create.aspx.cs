using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TEL.Event.Lab.Method;

public partial class Event_Event_RegisterModel4_Create : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["id"] == null || string.IsNullOrEmpty(Request.QueryString["id"]))
            Response.Redirect("Default.aspx");

        bool mealEnable = false;
        if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings.Get("RegisterModel4MealEnable")))
        {
            mealEnable = Convert.ToBoolean(ConfigurationManager.AppSettings.Get("RegisterModel4MealEnable"));
        }
        lblMeal.Visible = mealEnable;
        ddlMeal.Visible = mealEnable;

        if (!IsPostBack)
            SetDefaultValues();
    }

    private void SetDefaultValues()
    {
        string eventid = Request.QueryString["id"];
        string empid = Page.Session["EmpID"].ToString();

        if (!string.IsNullOrEmpty(Request.QueryString["EmpID"]))
        {
            empid = Request.QueryString["EmpID"];
        }

        UC_EventDescription.setViewDefault(eventid);
        InitDDLValues(eventid);
        InitRBLValues(eventid);
        InitFormValues(empid);
    }

    protected void btnSummit_Click(object sender, EventArgs e)
    {
        StringBuilder sb = new StringBuilder();
        CheckFormat cf = new CheckFormat();

        //受診者身份別 必填
        if (string.IsNullOrEmpty(ddlIdentity.SelectedValue))
        {
            sb.AppendLine(string.Format(lblRequired.Text, lblIdentity.Text));
            sb.AppendLine("<br />");
        }

        //受診者姓名 必填
        if (string.IsNullOrEmpty(txtExamineename.Text))
        {
            sb.AppendLine(string.Format(lblRequired.Text, lblExamineename.Text));
            sb.AppendLine("<br />");
        }

        //受診者拼音 必填
        if (string.IsNullOrEmpty(txtExamineename2.Text))
        {
            sb.AppendLine(string.Format(lblRequired.Text, lblExamineename2.Text));
            sb.AppendLine("<br />");
        }

        //受診者居留證字號 必填
        if (string.IsNullOrEmpty(txtExamineeidno.Text))
        {
            sb.AppendLine(string.Format(lblRequired.Text, lblExamineeidno.Text));
            sb.AppendLine("<br />");
        }
        else
        {
            //受診者居留證字號 格式
            if (!cf.CheckIdnoNew(txtExamineeidno.Text))
            {
                sb.AppendLine(string.Format(lblFormatError.Text, lblExamineeidno.Text));
                sb.AppendLine("<br />");
            }
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
        else
        {
            //受診者手機 格式檢查
            if (!cf.CheckMobile(txtExamineemobile.Text))
            {
                sb.AppendLine(string.Format(lblFormatError.Text, lblExamineemobile.Text));
                sb.AppendLine("<br />");
            }
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

        //期望受檢日不可與備用受檢日相同
        if (!string.IsNullOrEmpty(ddlExpectdate.SelectedValue) && !string.IsNullOrEmpty(ddlSeconddate.SelectedValue))
        {
            if (ddlExpectdate.SelectedValue == ddlSeconddate.SelectedValue)
            {
                sb.AppendLine("期望受檢日不可與備用受檢日相同");
                sb.AppendLine("<br />");
            }
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

        //住宿人名單 必填
        if (rblNeedhotel.SelectedValue == "是")
        {
            if (string.IsNullOrEmpty(txtCheckininfo.Text))
            {
                sb.AppendLine(string.Format(lblRequired.Text, lblCheckininfo.Text));
                sb.AppendLine("<br />");
            }
        }

        if (!string.IsNullOrEmpty(sb.ToString()))
        {
            lblMsg.Text = sb.ToString();
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Guid.NewGuid().ToString(), "ShowDialogMsg();", true);

            return;
        }

        //依照使用者選擇的健檢醫院、地區、費用&方案、受診者性別、期望受檢日，在TEL_Event_RegisterOption4維護的人數上限來檢查，是否報名人數已達上限，如果已達上限，則顯示(此方案報名人數已達上限，請重新選擇其他方案)
        Event ev = new Event();
        string eventid = Request.QueryString["id"];
        string modifiedby = Page.Session["EmpID"].ToString();

        int option1Limit = ev.GetRegisterOption4Limit(eventid, ddlHosipital.SelectedValue, ddlArea.SelectedValue, ddlSolution.SelectedValue, ddlGender.SelectedValue, ddlExpectdate.SelectedValue);
        int registerCount = ev.GetRegisterOption4Count(eventid, ddlHosipital.SelectedValue, ddlArea.SelectedValue, ddlSolution.SelectedValue, ddlGender.SelectedValue, ddlExpectdate.SelectedValue, string.Empty);

        if (registerCount >= option1Limit)
        {

            lblMsg.Text = lblLimitReached.Text;
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Guid.NewGuid().ToString(), "ShowDialogMsg();", true);
            return;
        }

        Dictionary<string, string> Data = new Dictionary<string, string>();
        string id = Guid.NewGuid().ToString();
        Data.Add("id", id);
        Data.Add("eventid", eventid);
        Data.Add("empid", txtEmpid.Text);
        Data.Add("registerdate", DateTime.Now.ToString("yyyy/MM/dd HH:mm"));
        Data.Add("examineeidentity", ddlIdentity.SelectedValue);
        Data.Add("examineename", txtExamineename.Text);
        Data.Add("examineename2", txtExamineename2.Text);
        Data.Add("examineeidno", txtExamineeidno.Text.ToUpper());
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

        string result = ev.AddRegisterModel4(Data, modifiedby);

        if (string.IsNullOrEmpty(result))
        {
            //寄送報名完成通知信給員工
            SendRegisterSuccessMail(id);
        }
        else
        {
            lblErrMsg.Text = lblRegisterErrMsg.Text;


            string errMsg = $@"發生錯誤:{Environment.NewLine} 新增模板4報名資料發生錯誤。 {Environment.NewLine}" + result;
            LogHelper.WriteLog(errMsg);
            //失敗
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Guid.NewGuid().ToString(), "ShowDialogFailed();", true);
        }

    }

    protected void btnCannel_Click(object sender, EventArgs e)
    {
        string returnPage = "Default";
        string eventid = Request.QueryString["id"];

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
        ListItem li = new ListItem();
        li.Text = lblUnselect.Text;
        li.Value = string.Empty;
        li.Selected = true;

        this.ddlArea.Items.Clear();
        this.ddlArea.Enabled = false;
        this.ddlArea.Items.Add(li);

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

        string eventid = Request.QueryString["id"];

        if (!string.IsNullOrEmpty(ddlHosipital.SelectedValue))
            BindddlArea(eventid, ddlHosipital.SelectedValue);
    }

    protected void ddlArea_SelectedIndexChanged(object sender, EventArgs e)
    {
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

        string eventid = Request.QueryString["id"];

        if (!string.IsNullOrEmpty(ddlArea.SelectedValue))
            BindddlSolution(eventid, ddlHosipital.SelectedValue, ddlArea.SelectedValue);
    }

    protected void ddlSolution_SelectedIndexChanged(object sender, EventArgs e)
    {
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

        string eventid = Request.QueryString["id"];

        if (!string.IsNullOrEmpty(ddlSolution.SelectedValue))
            BindddlGender(eventid, ddlHosipital.SelectedValue, ddlArea.SelectedValue, ddlSolution.SelectedValue);
    }

    protected void ddlGender_SelectedIndexChanged(object sender, EventArgs e)
    {
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

        string eventid = Request.QueryString["id"];

        if (!string.IsNullOrEmpty(ddlGender.SelectedValue))
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

    /// <summary>
    /// 回到原始頁面
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnGoBackPage_Click(object sender, EventArgs e)
    {
        string returnPage = "Default";
        string eventid = Request.QueryString["id"];

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

    private void InitFormValues(string empid)
    {
        UserInfo userInfo = new UserInfo(empid);
        txtEmpid.Text = empid;
        txtCName.Text = userInfo.FullNameCH;
        txtEName.Text = userInfo.FullNameEN;
        txtDepartment.Text = userInfo.UnitName;
        txtStation.Text = userInfo.Station;
        txtHealthGroup.Text = userInfo.HealthGroup;
    }

    private void BindddlArea(string eventid, string hosipital)
    {
        this.ddlArea.Enabled = true;
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

        if (dt.Rows.Count == 1)
        {
            this.ddlArea.SelectedIndex = 1;

            BindddlSolution(eventid, ddlHosipital.SelectedValue, ddlArea.SelectedValue);
        }
    }
    private void BindddlSolution(string eventid, string hosipital, string aea)
    {
        this.ddlSolution.Enabled = true;
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

        if (dt.Rows.Count == 1)
        {
            this.ddlSolution.SelectedIndex = 1;

            BindddlGender(eventid, ddlHosipital.SelectedValue, ddlArea.SelectedValue, ddlSolution.SelectedValue);
        }
    }
    private void BindddlGender(string eventid, string hosipital, string aea, string solution)
    {
        ddlGender.Enabled = true;
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

        if (dt.Rows.Count == 1)
        {
            this.ddlGender.SelectedIndex = 1;

            BindOrderHealthGroupDLL(eventid, ddlHosipital.SelectedValue, ddlArea.SelectedValue, ddlSolution.SelectedValue, ddlGender.SelectedValue);
        }
    }
    private void BindOrderHealthGroupDLL(string eventid, string hosipital, string aea, string solution, string gender)
    {
        this.ddlExpectdate.Enabled = true;
        this.ddlSeconddate.Enabled = true;
        this.ddlSecondsolution1.Enabled = true;
        this.ddlSecondsolution2.Enabled = true;
        this.ddlSecondsolution3.Enabled = true;
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

        if (dt.Rows.Count == 1)
        {
            this.ddlExpectdate.SelectedIndex = 1;
            this.ddlSeconddate.SelectedIndex = 1;
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

        if (dtSecondsolution1.Rows.Count == 1)
        {
            this.ddlSecondsolution1.SelectedIndex = 1;
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

        if (dtSecondsolution2.Rows.Count == 1)
        {
            this.ddlSecondsolution2.SelectedIndex = 1;
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

        if (dtSecondsolution3.Rows.Count == 1)
        {
            this.ddlSecondsolution3.SelectedIndex = 1;
        }
    }

    private void SendRegisterSuccessMail(string id)
    {
        Event ev = new Event();
        string eventid = Request.QueryString["id"];
        EventInfo eventInfo = new EventInfo(eventid);

        ev.GetEventInfo(eventid);

        string strSender = "FiestaSystem@tel.com";
        string strSubject = string.Format(lblEmailSubject.Text, eventInfo.EventName);
        string strDisplay = "Fiesta System";
        StringBuilder sbBody = new StringBuilder();
        DataTable dtRecipient = new DataTable();

        string registerEditLink = HttpContext.Current.Request.Url.AbsoluteUri.Replace($"/Event/Event_RegisterModel5_Create.aspx{HttpContext.Current.Request.Url.Query}", $"/Event/MyEvent.aspx?name={HttpUtility.UrlEncode(eventInfo.EventName)}&eventid={eventid}&id={id}");
        string registerDefaultLink = HttpContext.Current.Request.Url.AbsoluteUri.Replace($"/Event/Event_RegisterModel5_Create.aspx{HttpContext.Current.Request.Url.Query}", $"/Event/Default.aspx");
        sbBody.Append("<div>");
        sbBody.Append("<div>" + lblEmailContent1.Text + "</div>");
        sbBody.Append("<div><br></div>");
        sbBody.Append($"<div>{string.Format(lblEmailContent2.Text, registerEditLink, eventInfo.EventName)}</div>");
        sbBody.Append("<div><br></div>");
        sbBody.Append($"<div>{string.Format(lblEmailContent3.Text, registerDefaultLink)}</div>");
        sbBody.Append($"<div>{lblEmailContent4.Text}</div>");
        sbBody.Append("<div><br></div>");
        sbBody.Append($"<div><span style='color: #595959;'>{lblEmailContent5.Text}</span></div>");
        sbBody.Append("</div>");

        string empid = Page.Session["EmpID"].ToString();
        UserInfo userInfo = new UserInfo(empid);

        if (SenMail.SendMail(strSender, userInfo.EMail, strSubject, sbBody.ToString(), strDisplay))
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