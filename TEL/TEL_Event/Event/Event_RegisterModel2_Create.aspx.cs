using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TEL.Event.Lab.Method;

public partial class Event_Event_RegisterModel2_Create : System.Web.UI.Page
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
        InitGridRegisterModel2family();
    }

    /// <summary>
    /// 家屬資料新增
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnFAdd_Click(object sender, EventArgs e)
    {
        StringBuilder sb = new StringBuilder();
        CheckFormat cf = new CheckFormat();

        //家屬姓名 必填
        if (string.IsNullOrEmpty(txtFName.Text))
        {
            sb.AppendLine(string.Format(lblRequired.Text, lblFName.Text));
            sb.AppendLine("<br />");
        }

        //家屬身分證字號 必填
        if (string.IsNullOrEmpty(txtFID.Text))
        {
            sb.AppendLine(string.Format(lblRequired.Text, lblFID.Text));
            sb.AppendLine("<br />");
        }
        else
        {
            //家屬身分證字號 格式
            if (!cf.CheckIdnoNew(txtFID.Text))
            {
                sb.AppendLine(string.Format(lblFormatError.Text, lblFID.Text));
                sb.AppendLine("<br />");
            }
        }

        //家屬生日年月日 必填
        if (string.IsNullOrEmpty(txtFBDay.Text))
        {
            sb.AppendLine(string.Format(lblRequired.Text, lblFBDay.Text));
            sb.AppendLine("<br />");
        }

        //	家屬性別 必填
        if (string.IsNullOrEmpty(ddlGender.SelectedValue))
        {
            sb.AppendLine(string.Format(lblRequired.Text, lblFGender.Text));
            sb.AppendLine("<br />");
        }

        //餐點內容 必填
        if (string.IsNullOrEmpty(ddlFMeal.SelectedValue))
        {
            sb.AppendLine(string.Format(lblRequired.Text, lblFMeal.Text));
            sb.AppendLine("<br />");
        }

        if (!string.IsNullOrEmpty(sb.ToString()))
        {
            lblMsg.Text = sb.ToString();
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Guid.NewGuid().ToString(), "ShowDialogMsg();", true);

            return;
        }

        DataTable dt = GetGridViewToDatatable();
        //dt.Columns.Add("家屬姓名");
        //dt.Columns.Add("家屬身分證字號");
        //dt.Columns.Add("家屬生日年月日");
        //dt.Columns.Add("家屬性別");
        //dt.Columns.Add("餐點內容");
        DataRow dr = dt.NewRow();
        dr["name"] = txtFName.Text;
        dr["idno"] = txtFID.Text.ToUpper();
        dr["birthday"] = txtFBDay.Text;
        dr["gender"] = ddlGender.SelectedValue;
        dr["meal"] = ddlFMeal.SelectedValue;

        dt.Rows.Add(dr);

        this.gridRegisterModel2family.DataSource = dt;
        this.gridRegisterModel2family.DataBind();

        this.txtFName.Text = "";
        this.txtFID.Text = "";
        this.txtFBDay.Text="";
        this.ddlGender.SelectedValue = "";
        this.ddlFMeal.SelectedValue = "";
    }

    protected void Button_Delete_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        GridViewRow gvr = (GridViewRow)btn.NamingContainer;

        int rowindex = gvr.RowIndex;

        DataTable dt = GetGridViewToDatatable();
        DataRow dr = dt.Rows[rowindex];
        dr.Delete();
        dt.AcceptChanges();

        this.gridRegisterModel2family.DataSource = dt;
        this.gridRegisterModel2family.DataBind();
    }

    /// <summary>
    /// 送出
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSummit_Click(object sender, EventArgs e)
    {
        string eventid = Request.QueryString["id"];
        string modifiedby = Page.Session["EmpID"].ToString();
        CheckFormat cf = new CheckFormat();

        StringBuilder sb = new StringBuilder();

        //欲參加的內容 必填
        if (string.IsNullOrEmpty(this.ddlAttendContent.SelectedValue))
        {
            sb.AppendLine(string.Format(lblRequired.Text, lblAttendContent.Text));
            sb.AppendLine("<br />");
        }

        //手機 必填
        if (string.IsNullOrEmpty(txtPhone.Text))
        {
            sb.AppendLine(string.Format(lblRequired.Text, lblPhone.Text));
            sb.AppendLine("<br />");
        }
        else
        {
            //手機 格式檢查
            if (!cf.CheckMobile(txtPhone.Text))
            {
                sb.AppendLine(string.Format(lblFormatError.Text, lblPhone.Text));
                sb.AppendLine("<br />");
            }
        }

        //交通車 必填
        if (string.IsNullOrEmpty(ddlTransportation.SelectedValue))
        {
            sb.AppendLine(string.Format(lblRequired.Text, lblTransportation.Text));
            sb.AppendLine("<br />");
        }

        //餐點內容 必填
        if (string.IsNullOrEmpty(ddlMeal.SelectedValue))
        {
            sb.AppendLine(string.Format(lblRequired.Text, lblMeal.Text));
            sb.AppendLine("<br />");
        }

        if (!string.IsNullOrEmpty(sb.ToString()))
        {
            lblMsg.Text = sb.ToString();
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Guid.NewGuid().ToString(), "ShowDialogMsg();", true);

            return;
        }

        //在TEL_Event_RegisterOption1維護的人數上限來檢查，是否報名人數已達上限，如果已達上限，則顯示(此方案報名人數已達上限，請重新選擇其他方案)
        Event ev = new Event();
        DataTable dtGridView = GetGridViewToDatatable();
        int option1Limit = ev.GetRegisterOption1Limit(eventid, ddlAttendContent.SelectedValue);

        //registerCount = 已報名員工 + 已報名家屬 + 此報名表欲報名家屬 + 此報名表欲報名員工
        int registerCount = ev.GetOption1RegisterCountByRegisterModel2(eventid, ddlAttendContent.SelectedValue, string.Empty) + ev.GetOption1RegisterCountByRegisterModel2family(eventid, ddlAttendContent.SelectedValue, string.Empty) + dtGridView.Rows.Count + 1;

        if (registerCount > option1Limit)
        {
            lblMsg.Text = lblLimitReached.Text;
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Guid.NewGuid().ToString(), "ShowDialogMsg();", true);

            return;
        }

        Dictionary<string, string> EventsData = new Dictionary<string, string>();
        string id = Guid.NewGuid().ToString();
        EventsData.Add("id", id);
        EventsData.Add("eventid", eventid);
        EventsData.Add("empid", txtEmpid.Text);
        EventsData.Add("registerdate", DateTime.Now.ToString("yyyy/MM/dd HH:mm"));//報名日期為當下時間
        EventsData.Add("selectedoption", ddlAttendContent.SelectedValue);
        EventsData.Add("mobile", txtPhone.Text);
        EventsData.Add("traffic", ddlTransportation.SelectedValue);
        EventsData.Add("meal", ddlMeal.SelectedValue);
        EventsData.Add("feedback", txtComment.Text);

        string result = ev.AddRegisterModel2(EventsData, GetGridViewToDatatable(), modifiedby);

        if (string.IsNullOrEmpty(result))
        {
            //寄送報名完成通知信給員工
            SendRegisterSuccessMail(id);
        }
        else
        {
            lblErrMsg.Text = lblRegisterErrMsg.Text;


            string errMsg = $@"發生錯誤:{Environment.NewLine} 新增模板2報名資料發生錯誤。 {Environment.NewLine}" + result;
            LogHelper.WriteLog(errMsg);

            //失敗
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Guid.NewGuid().ToString(), "ShowDialogFailed();", true);
        }
    }

    /// <summary>
    /// 取消
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
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


    protected void gridRegisterModel2family_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gridRegisterModel2family.PageIndex = e.NewPageIndex;
        this.gridRegisterModel2family.DataSource = GetGridViewToDatatable();
        this.gridRegisterModel2family.DataBind();
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
    private void InitFormValues(string empid)
    {
        UserInfo userInfo = new UserInfo(empid);
        txtEmpid.Text = empid;
        txtCName.Text = userInfo.FullNameCH;
        txtEName.Text = userInfo.FullNameEN;
        txtDepartment.Text = userInfo.UnitName;
        txtStation.Text = userInfo.Station;
        txtID.Text = userInfo.NationalID;
        txtBDay.Text = userInfo.Birthday;
        txtGender.Text = userInfo.Gender;
        txtEmail.Text = userInfo.EMail;
    }

    /// <summary>
    /// 初始下拉選單值
    /// </summary>
    /// <param name="eventid"></param>
    private void InitDDLValues(string eventid)
    {
        //欲參加的內容
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

        //交通車
        this.ddlTransportation.Items.Clear();
        ListItem liTransportation = new ListItem();
        liTransportation.Text = lblUnselect.Text;
        liTransportation.Value = string.Empty;
        liTransportation.Selected = true;
        this.ddlTransportation.Items.Add(liTransportation);

        DataTable dtTransportation = new DataTable();
        dtTransportation = ev.GetTransportation(eventid);

        foreach (DataRow rs in dtTransportation.Rows)
        {
            ListItem li1 = new ListItem();
            li1.Text = rs["description"].ToString();
            li1.Value = rs["description"].ToString();

            this.ddlTransportation.Items.Add(li1);
        }

        //餐點內容
        this.ddlMeal.Items.Clear();
        this.ddlFMeal.Items.Clear();

        ListItem liMeal = new ListItem();
        liMeal.Text = lblUnselect.Text;
        liMeal.Value = string.Empty;
        liMeal.Selected = true;
        this.ddlMeal.Items.Add(li);
        this.ddlFMeal.Items.Add(li);


        DataTable dtMeal = new DataTable();
        dtMeal = ev.GetMeal(eventid);

        foreach (DataRow rs in dtMeal.Rows)
        {
            ListItem li1 = new ListItem();
            li1.Text = rs["description"].ToString();
            li1.Value = rs["description"].ToString();

            this.ddlMeal.Items.Add(li1);
            this.ddlFMeal.Items.Add(li1);

        }
    }

    private void InitGridRegisterModel2family()
    {
        this.gridRegisterModel2family.DataSource = GetGridViewToDatatable();
        this.gridRegisterModel2family.DataBind();
    }
    private DataTable GetGridViewToDatatable()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("name");
        dt.Columns.Add("idno");
        dt.Columns.Add("birthday");
        dt.Columns.Add("gender");
        dt.Columns.Add("meal");

        foreach (GridViewRow row in gridRegisterModel2family.Rows)
        {
            DataRow dr;
            dr = dt.NewRow();

            for (int i = 0; i < row.Cells.Count - 1; i++)
            {
                dr[i] = row.Cells[i].Text.Replace(" ", "");
            }
            dt.Rows.Add(dr);
        }

        return dt;
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