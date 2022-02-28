using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TEL.Event.Lab.Method;

public partial class Event_Event_RegisterModel1_Create : System.Web.UI.Page
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
        string eventid = Request.QueryString["id"];
        string empid = Page.Session["EmpID"].ToString();

        if (!string.IsNullOrEmpty(Request.QueryString["EmpID"]))
        {
            empid = Request.QueryString["EmpID"];
        }

        UC_EventDescription.setViewDefault(eventid);
        InitFormValues(empid);
        InitDDLValues(eventid);
    }

    /// <summary>
    /// 初始表單
    /// </summary>
    /// <param name="empid"></param>
    private void InitFormValues(string empid)
    {
        UserInfo userInfo = new UserInfo(empid);
        txtEmpid.Text = empid;
        txtCName.Text = userInfo.FullNameCH;
        txtEName.Text = userInfo.FullNameEN;
        txtDepartment.Text = $"{userInfo.UnitCode}-{userInfo.UnitName}";
        txtStation.Text = userInfo.Station;
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

    protected void btnSummit_Click(object sender, EventArgs e)
    {
        string eventid = Request.QueryString["id"];
        string modifiedby = Page.Session["EmpID"].ToString();
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
        int registerCount = ev.GetEvnetRegisterOption1RegisterCount(eventid, ddlAttendContent.SelectedValue, string.Empty);

        if (registerCount >= option1Limit)
        {
            lblMsg.Text = lblLimitReached.Text;
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Guid.NewGuid().ToString(), "ShowDialogMsg();", true);

            return;
        }

        Dictionary<string, string> EventsData = new Dictionary<string, string>();
        EventsData.Add("id", Guid.NewGuid().ToString());
        EventsData.Add("eventid", eventid);
        EventsData.Add("empid", txtEmpid.Text);
        EventsData.Add("registerdate", DateTime.Now.ToString("yyyy/MM/dd HH:mm"));//報名日期為當下時間
        EventsData.Add("selectedoption", ddlAttendContent.SelectedValue);
        EventsData.Add("feedback", txtComment.Text);

        string result = ev.AddRegisterModel1(EventsData, modifiedby);

        if (string.IsNullOrEmpty(result))
        {
            //寄送報名完成通知信給員工
            SendRegisterSuccessMail();
        }
        else
        {
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

    private void SendRegisterSuccessMail()
    {
        Event ev = new Event();
        string eventid = Request.QueryString["id"];
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
}