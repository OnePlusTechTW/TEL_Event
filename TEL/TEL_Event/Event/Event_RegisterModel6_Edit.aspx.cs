﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using TEL.Event.Lab.Method;

public partial class Event_Event_RegisterModel6_Edit : System.Web.UI.Page
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
        InitFormValues(eventid, registerid);
    }

    protected void btnSummit_Click(object sender, EventArgs e)
    {
        StringBuilder sb = new StringBuilder();
        //地點 必填
        if (string.IsNullOrEmpty(ddlArea.SelectedValue))
        {
            sb.AppendLine(string.Format(lblRequired.Text, lblChangeArea.Text));
            sb.AppendLine("<br />");
        }

        //日期時間 必填
        if (string.IsNullOrEmpty(ddlAvaliabledate.SelectedValue))
        {
            sb.AppendLine(string.Format(lblRequired.Text, lblChangeDate.Text));
            sb.AppendLine("<br />");
        }

        if (!string.IsNullOrEmpty(sb.ToString()))
        {
            lblMsg.Text = sb.ToString();
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Guid.NewGuid().ToString(), "ShowDialogMsg();", true);

            return;
        }

        Event ev = new Event();
        string eventid = Request.QueryString["eventid"];
        string registerid = Request.QueryString["id"];
        string modifiedby = Page.Session["EmpID"].ToString();

        int option1Limit = ev.GetRegisterOption6Limit(eventid, ddlArea.SelectedValue, ddlAvaliabledate.SelectedValue);
        int registerCount = ev.GetRegisterOption6Count(eventid, ddlArea.SelectedValue, ddlAvaliabledate.SelectedValue, registerid);

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
        Data.Add("changearea", ddlArea.SelectedValue);
        Data.Add("changedate", ddlAvaliabledate.SelectedValue);
        Data.Add("feedback", txtComment.Text);


        string result = ev.UpdateRegisterModel6(Data, modifiedby);

        if (string.IsNullOrEmpty(result))
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Guid.NewGuid().ToString(), "ShowRegisterSccessDialog();", true);
        }
        else
        {
            lblErrMsg.Text = lblUpdateErrMsg.Text;

            string errMsg = $@"發生錯誤:{Environment.NewLine} 更新模板6報名資料發生錯誤。 {Environment.NewLine}" + result;
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

    protected void ddlArea_SelectedIndexChanged(object sender, EventArgs e)
    {
        string eventid = Request.QueryString["eventid"];
        BindDDLAvaliabledate(eventid, ddlArea.SelectedValue);
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

    private void InitFormValues(string eventid, string registerid)
    {
        Event ev = new Event();
        DataTable dt = new DataTable();
        dt = ev.GetRegisterModel6(registerid);
        if (dt.Rows.Count > 0)
        {
            UserInfo userInfo = new UserInfo(dt.Rows[0]["empid"].ToString());
            txtEmpid.Text = dt.Rows[0]["empid"].ToString();
            txtCName.Text = userInfo.FullNameCH;
            txtEName.Text = userInfo.FullNameEN;
            txtDepartment.Text = userInfo.UnitName;
            txtStation.Text = userInfo.Station;

            ddlArea.SelectedValue = dt.Rows[0]["changearea"].ToString();
            BindDDLAvaliabledate(eventid, ddlArea.SelectedValue);
            ddlAvaliabledate.SelectedValue = dt.Rows[0]["changedate"].ToString();

        }
        else
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Guid.NewGuid().ToString(), "ShowNoRegisterInfo();", true);
        }
    }

    private void InitDDLValues(string eventid)
    {
        Event ev = new Event();

        //地點
        this.ddlArea.Items.Clear();

        ListItem li = new ListItem();
        li.Text = lblUnselect.Text;
        li.Value = string.Empty;
        li.Selected = true;
        this.ddlArea.Items.Add(li);
        this.ddlAvaliabledate.Items.Add(li);

        DataTable dtArea = new DataTable();
        dtArea = ev.GetAreaOption6(eventid);

        foreach (DataRow rs in dtArea.Rows)
        {
            ListItem li1 = new ListItem();
            li1.Text = rs["area"].ToString();
            li1.Value = rs["area"].ToString();

            this.ddlArea.Items.Add(li1);
        }
    }

    private void BindDDLAvaliabledate(string eventid, string selectedValue)
    {
        Event ev = new Event();

        //地區
        this.ddlAvaliabledate.Enabled = true;
        this.ddlAvaliabledate.Items.Clear();

        ListItem li = new ListItem();
        li.Text = lblUnselect.Text;
        li.Value = string.Empty;
        li.Selected = true;
        this.ddlAvaliabledate.Items.Add(li);

        if (!string.IsNullOrEmpty(ddlArea.SelectedValue))
        {
            DataTable dt = new DataTable();
            dt = ev.GetAvaliableDatOption(eventid, ddlArea.SelectedValue);

            foreach (DataRow rs in dt.Rows)
            {
                ListItem li1 = new ListItem();
                li1.Text = rs["avaliabledate"].ToString();
                li1.Value = rs["avaliabledate"].ToString();

                this.ddlAvaliabledate.Items.Add(li1);
            }
        }
        else
        {
            this.ddlAvaliabledate.Enabled = false;
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

        string result = ev.DeleteRegisterModel6(registerid, modifiedby);

        if (!string.IsNullOrEmpty(result))
        {
            string errMsg = $@"發生錯誤:{Environment.NewLine} 刪除模板6報名資料發生錯誤。 {Environment.NewLine}" + result;
            LogHelper.WriteLog(errMsg);

            //失敗
            throw new Exception("Failed");
        }

        return "Success";
    }

    private void SendRegisterSuccessMail(string id)
    {
        Event ev = new Event();
        string eventid = Request.QueryString["eventid"];
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