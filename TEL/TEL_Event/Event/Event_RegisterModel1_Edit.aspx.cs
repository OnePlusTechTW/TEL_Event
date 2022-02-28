using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using TEL.Event.Lab.Method;

public partial class Event_Event_RegisterModel1_Edit : System.Web.UI.Page
{
    ///Event_RegisterModel1_Edit.aspx?eventid=6e54d90b-65af-4952-985f-8dfa239d3e51&id=4753BB1D-91B1-4312-B3FB-6CFA6A44B159&page=Register
    ///
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["id"] == null || string.IsNullOrEmpty(Request.QueryString["id"]))
            Response.Redirect("Register.aspx");

        if (!IsPostBack)
            SetDefaultValues();
    }

    private void SetDefaultValues()
    {
        string empid = Page.Session["EmpID"].ToString();
        string eventid = Request.QueryString["eventid"];
        string id = Request.QueryString["id"].ToString();


        UC_EventDescription.setViewDefault(eventid);
        InitDDLValues(eventid);
        InitFormValues(id);
    }
    

    protected void btnSummit_Click(object sender, EventArgs e)
    {
        string eventid = Request.QueryString["eventid"];
        string modifiedby = Page.Session["EmpID"].ToString();
        string registerid = Request.QueryString["id"].ToString();

        //欲參加的內容 必填
        if (string.IsNullOrEmpty(this.ddlAttendContent.SelectedValue))
        {
            lblMsg.Text = string.Format(lblRequired.Text, lblAttendContent.Text);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Guid.NewGuid().ToString(), "ShowDialogMsg();", true);

            return;
        }

        //在TEL_Event_RegisterOption1維護的人數上限來檢查，是否報名人數已達上限，如果已達上限，則顯示(此方案報名人數已達上限，請重新選擇其他方案)
        Event ev = new Event();
        int option1Limit = ev.GetRegisterOption1Limit(eventid, ddlAttendContent.SelectedValue);
        int registerCount = ev.GetEvnetRegisterOption1RegisterCount(eventid, ddlAttendContent.SelectedValue, registerid);

        if (registerCount >= option1Limit)
        {
            lblMsg.Text = lblLimitReached.Text;
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Guid.NewGuid().ToString(), "ShowDialogMsg();", true);

            return;
        }

        Dictionary<string, string> EventsData = new Dictionary<string, string>();
        EventsData.Add("id", registerid);
        EventsData.Add("eventid", eventid);
        EventsData.Add("empid", txtEmpid.Text);
        EventsData.Add("registerdate", DateTime.Now.ToString("yyyy/MM/dd HH:mm"));//報名日期為當下時間
        EventsData.Add("selectedoption", ddlAttendContent.SelectedValue);
        EventsData.Add("feedback", txtComment.Text);


        string result = ev.UpdateRegisterModel1(EventsData, modifiedby);


        if (string.IsNullOrEmpty(result))
        {
            //寄送報名完成通知信給員工
            SendRegisterSuccessMail();
        }
        else
        {
            lblErrMsg.Text = lblUpdateErrMsg.Text;


            string errMsg = $@"發生錯誤:{Environment.NewLine} 更新模板1報名資料發生錯誤。 {Environment.NewLine}" + result;
            LogHelper.WriteLog(errMsg);
            //失敗
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Guid.NewGuid().ToString(), "ShowDialogFailed();", true);
        }
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

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        Event ev = new Event();
        string id = Request.QueryString["id"] + "|" + Page.Session["EmpID"].ToString();

        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Guid.NewGuid().ToString(), "ShowDialogDelete('" + id + "');", true);
    }

    /// <summary>
    /// 初始下拉選單值
    /// </summary>
    /// <param name="eventid"></param>
    private void InitDDLValues(string eventid)
    {
        this.ddlAttendContent.Items.Clear();
        ListItem li = new ListItem();
        li.Text = lblUnselect.Text;
        li.Value = string.Empty;
        li.Selected = true;
        this.ddlAttendContent.Items.Add(li);

        Event ev = new Event();
        DataTable dt = new DataTable();
        dt = ev.GetRegisterOption1(eventid);

        foreach (DataRow rs in dt.Rows)
        {
            ListItem li1 = new ListItem();
            li1.Text = rs["description"].ToString();
            li1.Value = rs["description"].ToString();

            this.ddlAttendContent.Items.Add(li1);
        }
    }

    /// <summary>
    /// 初始表單
    /// </summary>
    private void InitFormValues(string id)
    {
        Event ev = new Event();
        DataTable dt = new DataTable();
        dt = ev.GetRegisterModel1(id);
        //form info
        if (dt.Rows.Count > 0)
        {
            UserInfo userInfo = new UserInfo(dt.Rows[0]["empid"].ToString());
            //user info
            txtEmpid.Text = userInfo.EmpID;
            txtCName.Text = userInfo.FullNameCH;
            txtEName.Text = userInfo.FullNameEN;
            txtDepartment.Text = $"{userInfo.UnitCode}-{userInfo.UnitName}";
            txtStation.Text = userInfo.Station;

            ddlAttendContent.SelectedValue = dt.Rows[0]["selectedoption"].ToString();
            txtComment.Text = dt.Rows[0]["feedback"].ToString();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Guid.NewGuid().ToString(), "ShowNoRegisterInfo();", true);
        }
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

    /// <summary>
    /// 刪除活動報名
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [WebMethod]
    public static string DeleteRegisterModel1(string id)
    {
        Event ev = new Event();
        //Request.QueryString["id"] + "|" + Page.Session["EmpID"].ToString();
        string registerid = id.Split('|')[0];
        string modifiedby = id.Split('|')[1];

        string result = ev.DeleteRegisterModel1(registerid, modifiedby);

        if (!string.IsNullOrEmpty(result))
        {
            string errMsg = $@"發生錯誤:{Environment.NewLine} 刪除模板1報名資料發生錯誤。 {Environment.NewLine}" + result;
            LogHelper.WriteLog(errMsg);

            //失敗
            throw new Exception("Failed");
        }

        return "Success";
    }
}