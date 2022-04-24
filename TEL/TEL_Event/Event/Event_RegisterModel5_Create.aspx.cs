using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TEL.Event.Lab.Method;

public partial class Event_Event_RegisterModel5_Create : System.Web.UI.Page
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
    }

    protected void btnSummit_Click(object sender, EventArgs e)
    {
        Event ev = new Event();
        string eventid = Request.QueryString["id"];
        string modifiedby = Page.Session["EmpID"].ToString();

        //上傳附件1 必填
        if (!FileUpload1.HasFile)
        {
            lblMsg.Text = string.Format(lblRequired.Text, txtFileUpload1.Text);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Guid.NewGuid().ToString(), "ShowDialogMsg();", true);

            return;
        }

        //人數上限檢查
        EventInfo evinfo = new EventInfo(eventid);

        if (evinfo.EventLimit != "")
        {
            int option1Limit = Convert.ToInt16(evinfo.EventLimit);
            int registerCount = Convert.ToInt16(ev.GetEvnetRegisterCount(eventid, evinfo.EventRegisterModel));

            if (registerCount >= option1Limit)
            {
                lblMsg.Text = lblLimitReached.Text;
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Guid.NewGuid().ToString(), "ShowDialogMsg();", true);

                return;
            }
        }

        Dictionary<string, string> Data = new Dictionary<string, string>();

        string id = Guid.NewGuid().ToString();
        Data.Add("id", id);
        Data.Add("eventid", eventid);
        Data.Add("empid", txtEmpid.Text);
        Data.Add("registerdate", DateTime.Now.ToString("yyyy/MM/dd HH:mm"));//報名日期為當下時間

        string FileUpload1ID = string.Empty;
        string FileUpload1Name = string.Empty;
        if (this.FileUpload1.HasFile)
        {
            string fileExtension = System.IO.Path.GetExtension(this.FileUpload1.FileName).ToLower();

            string path = ConfigurationManager.AppSettings.Get("EventThumbnailPath");
            if (string.IsNullOrEmpty(path))
                path = "~/Event/EventThumbnail";

            FileUpload1ID = $"{ Guid.NewGuid().ToString() }{ fileExtension }";
            FileUpload1Name = this.FileUpload1.PostedFile.FileName;


            path = Path.Combine(Server.MapPath(".." + path.Substring(1)), FileUpload1ID);
            this.FileUpload1.PostedFile.SaveAs(path);

        }

        Data.Add("attachment1", FileUpload1ID);//存入的檔案
        Data.Add("attachment1_name", FileUpload1Name);//原檔名
        Data.Add("description1", txtDescription1.Text);//原檔名

        string FileUpload2ID = string.Empty;
        string FileUpload2Name = string.Empty;
        if (this.FileUpload2.HasFile)
        {
            string fileExtension = System.IO.Path.GetExtension(this.FileUpload2.FileName).ToLower();

            string path = ConfigurationManager.AppSettings.Get("EventThumbnailPath");
            if (string.IsNullOrEmpty(path))
                path = "~/Event/EventThumbnail";

            FileUpload2ID = $"{ Guid.NewGuid().ToString() }{ fileExtension }";
            FileUpload2Name = this.FileUpload2.PostedFile.FileName;


            path = Path.Combine(Server.MapPath(".." + path.Substring(1)), FileUpload2ID);
            this.FileUpload2.PostedFile.SaveAs(path);

        }

        Data.Add("attachment2", FileUpload2ID);//存入的檔案
        Data.Add("attachment2_name", FileUpload2Name);//原檔名
        Data.Add("description2", txtDescription2.Text);//原檔名

        string FileUpload3ID = string.Empty;
        string FileUpload3Name = string.Empty;
        if (this.FileUpload3.HasFile)
        {
            string fileExtension = System.IO.Path.GetExtension(this.FileUpload3.FileName).ToLower();

            string path = ConfigurationManager.AppSettings.Get("EventThumbnailPath");
            if (string.IsNullOrEmpty(path))
                path = "~/Event/EventThumbnail";

            FileUpload3ID = $"{ Guid.NewGuid().ToString() }{ fileExtension }";
            FileUpload3Name = this.FileUpload3.PostedFile.FileName;


            path = Path.Combine(Server.MapPath(".." + path.Substring(1)), FileUpload3ID);
            this.FileUpload3.PostedFile.SaveAs(path);

        }

        Data.Add("attachment3", FileUpload3ID);//存入的檔案
        Data.Add("attachment3_name", FileUpload3Name);//原檔名
        Data.Add("description3", txtDescription3.Text);//原檔名

        Data.Add("feedback", txtComment.Text);//原檔名

        string result = ev.AddRegisterModel5(Data, modifiedby);

        if (string.IsNullOrEmpty(result))
        {
            //寄送報名完成通知信給員工
            SendRegisterSuccessMail(id);
        }
        else
        {
            lblErrMsg.Text = lblRegisterErrMsg.Text;


            string errMsg = $@"發生錯誤:{Environment.NewLine} 新增模板5報名資料發生錯誤。 {Environment.NewLine}" + result;
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
        txtDepartment.Text = userInfo.UnitName;
        txtStation.Text = userInfo.Station;
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