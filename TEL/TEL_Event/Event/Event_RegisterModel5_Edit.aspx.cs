﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using TEL.Event.Lab.Method;

public partial class Event_Event_RegisterModel5_Edit : System.Web.UI.Page
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
        string registerid = Request.QueryString["id"];
        string empid = Page.Session["EmpID"].ToString();

        UC_EventDescription.setViewDefault(eventid);
        InitFormValues(registerid);
    }

    protected void btnFileUpload1Maintain_Click(object sender, EventArgs e)
    {
        hlnkFileUpload1.Visible = false;
        btnFileUpload1Maintain.Visible = false;
        FileUpload1.Visible = true;
    }

    protected void btnFileUpload2Maintain_Click(object sender, EventArgs e)
    {
        hlnkFileUpload2.Visible = false;
        btnFileUpload2Maintain.Visible = false;
        FileUpload2.Visible = true;
    }

    protected void btnFileUpload3Maintain_Click(object sender, EventArgs e)
    {
        hlnkFileUpload3.Visible = false;
        btnFileUpload3Maintain.Visible = false;
        FileUpload3.Visible = true;
    }

    protected void btnSummit_Click(object sender, EventArgs e)
    {
        Event ev = new Event();
        string eventid = Request.QueryString["eventid"];
        string registerid = Request.QueryString["id"];

        string modifiedby = Page.Session["EmpID"].ToString();

        List<string> deleteFilePathList = new List<string>();
        Dictionary<string, string> Data = new Dictionary<string, string>();

        Data.Add("id", registerid);
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

            if (!string.IsNullOrEmpty(hlnkFileUpload1.NavigateUrl))
                deleteFilePathList.Add(hlnkFileUpload1.NavigateUrl);

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

            if (!string.IsNullOrEmpty(hlnkFileUpload2.NavigateUrl))
                deleteFilePathList.Add(hlnkFileUpload2.NavigateUrl);

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

            if (!string.IsNullOrEmpty(hlnkFileUpload3.NavigateUrl))
                deleteFilePathList.Add(hlnkFileUpload3.NavigateUrl);
        }

        Data.Add("attachment3", FileUpload3ID);//存入的檔案
        Data.Add("attachment3_name", FileUpload3Name);//原檔名
        Data.Add("description3", txtDescription3.Text);//原檔名

        Data.Add("feedback", txtComment.Text);//原檔名

        string result = ev.UpdateRegisterModel5(Data, modifiedby);

        if (string.IsNullOrEmpty(result))
        {
            //寄送報名完成通知信給員工
            SendRegisterSuccessMail();

            foreach (string path in deleteFilePathList)
            {
                File.Delete(Server.MapPath(path));
            }
        }
        else
        {
            //失敗
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Guid.NewGuid().ToString(), "ShowDialogFailed();", true);
        }

    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        Event ev = new Event();
        string id = Request.QueryString["id"];

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

    /// <summary>
    /// 初始表單
    /// </summary>
    /// <param name="empid"></param>
    private void InitFormValues(string registerid)
    {
        Event ev = new Event();
        DataTable dt = new DataTable();
        dt = ev.GetRegisterModel5(registerid);
        if (dt.Rows.Count > 0)
        {
            UserInfo userInfo = new UserInfo(dt.Rows[0]["empid"].ToString());
            txtEmpid.Text = dt.Rows[0]["empid"].ToString();
            txtCName.Text = userInfo.FullNameCH;
            txtEName.Text = userInfo.FullNameEN;
            txtDepartment.Text = $"{userInfo.UnitCode}-{userInfo.UnitName}";
            txtStation.Text = userInfo.Station;

            string path = ConfigurationManager.AppSettings.Get("EventThumbnailPath");
            if (string.IsNullOrEmpty(path))
                path = "~/App_Data/EventThumbnail";

            if (!string.IsNullOrEmpty(dt.Rows[0]["attachment1"].ToString()))
            {
                btnFileUpload1Maintain.Visible = true;
                hlnkFileUpload1.Visible = true;
                hlnkFileUpload1.NavigateUrl = Path.Combine(path, dt.Rows[0]["attachment1"].ToString());//"~/Sample/Import_HealthGroup.xlsx";
                hlnkFileUpload1.Text = dt.Rows[0]["attachment1_name"].ToString();
                
            }
            else
            {
                FileUpload1.Visible = true;
            }
            txtDescription1.Text = dt.Rows[0]["description1"].ToString();


            if (!string.IsNullOrEmpty(dt.Rows[0]["attachment2"].ToString()))
            {
                btnFileUpload2Maintain.Visible = true;
                hlnkFileUpload2.Visible = true;
                hlnkFileUpload2.NavigateUrl = Path.Combine(path, dt.Rows[0]["attachment2"].ToString());//"~/Sample/Import_HealthGroup.xlsx";
                hlnkFileUpload2.Text = dt.Rows[0]["attachment2_name"].ToString();

            }
            else
            {
                FileUpload2.Visible = true;
            }
            txtDescription2.Text = dt.Rows[0]["description2"].ToString();

            if (!string.IsNullOrEmpty(dt.Rows[0]["attachment3"].ToString()))
            {
                btnFileUpload3Maintain.Visible = true;
                hlnkFileUpload3.Visible = true;
                hlnkFileUpload3.NavigateUrl = Path.Combine(path, dt.Rows[0]["attachment3"].ToString());//"~/Sample/Import_HealthGroup.xlsx";
                hlnkFileUpload3.Text = dt.Rows[0]["attachment3_name"].ToString();

            }
            else
            {
                FileUpload3.Visible = true;
            }
            txtDescription3.Text = dt.Rows[0]["description3"].ToString();

        }
        else
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Guid.NewGuid().ToString(), "ShowNoRegisterInfo();", true);
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
        string result = ev.DeleteRegisterModel5(id);

        if (!string.IsNullOrEmpty(result))
        {
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