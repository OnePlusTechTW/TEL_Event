using Microsoft.Security.Application;
using System;
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

public partial class Event_Event_Create : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CheckEventThumbnailPathExist();
            GeneratedCategoryItem();
        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        //登入檢查
        //若不為系統管理常態活動、系統管理者者則導到Denied頁面
        TEL.Event.Lab.Method.SystemInfo gm = new TEL.Event.Lab.Method.SystemInfo();
        if (gm.IsManager(Page.Session["EmpID"].ToString()) < 2)
            Response.Redirect("Denied.aspx");
    }

    protected void ddlSignupTemplate_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(ddlSignupTemplate.SelectedValue))
        {
            imgTemplate.ImageUrl = "~/Sample/Img/Register_Model" + ddlSignupTemplate.SelectedValue + ".jpg";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Guid.NewGuid().ToString(), "ShowDialogTemplate();", true);
        }
    }

    protected void ddlQuestionnaireTemplate_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(ddlQuestionnaireTemplate.SelectedValue))
        {
            imgTemplate.ImageUrl = "~/Sample/Img/Survey_Model" + ddlQuestionnaireTemplate.SelectedValue + ".jpg";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Guid.NewGuid().ToString(), "ShowDialogTemplate();", true);
        }
    }

    protected void rblEventMember_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.rblEventMember.SelectedValue == "C")
        {
            cblCustMember.Visible = true;
            tbCustMember.Visible = true;
            lblCustMember.Visible = true;

            GeneratedCustMemberItem();
        }
        else
        {
            cblCustMember.Visible = false;
            tbCustMember.Visible = false;
            lblCustMember.Visible = false;

        }
    }

    protected void btnUploadThumbnail_Click(object sender, EventArgs e)
    {
        hfUploadType.Value = "Thumbnail";
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Guid.NewGuid().ToString(), "ShowDialogFileUpload();", true);
    }

    protected void btnUploadPicture_Click(object sender, EventArgs e)
    {
        hfUploadType.Value = "Picture";
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Guid.NewGuid().ToString(), "ShowDialogFileUpload();", true);
    }

    protected void btnImport_Click(object sender, EventArgs e)
    {
        StringBuilder sb = new StringBuilder();

        #region 圖片副檔名檢查
        //縮圖
        if (this.FileUploadThumbnail.HasFile)
        {
            string fileExtension = System.IO.Path.GetExtension(this.FileUploadThumbnail.FileName).ToLower();
            bool fileExtensionIsValid = IsExtensionValid(fileExtension);

            if (!fileExtensionIsValid)
            {
                sb.Append(hfUploadType.Value == "Thumbnail" ? lblThumbnail1.Text : lblPicture1.Text);
                sb.Append(lblExtension.Text);
            }
            else
            {
                if (hfUploadType.Value == "Thumbnail")
                {
                    lblThumbnailName.Text = this.FileUploadThumbnail.FileName;
                    Session["ThumbnailFileBytes"] = this.FileUploadThumbnail.FileBytes;
                }

                if (hfUploadType.Value == "Picture")
                {
                    lblPictureName.Text = this.FileUploadThumbnail.FileName;
                    Session["ThumbnailPictureFileBytes"] = this.FileUploadThumbnail.FileBytes;
                }
            }
        }

        if (!string.IsNullOrEmpty(sb.ToString()))
        {
            lblMsg.Text = sb.ToString();

            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Guid.NewGuid().ToString(), "ShowDialogFileUpload();", true);


            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Guid.NewGuid().ToString(), "ShowDialogMsg();", true);

            return;
        }
        #endregion
    }

    protected void btnNextStep_Click(object sender, EventArgs e)
    {
        StringBuilder sb = new StringBuilder();

        #region 必填
        if (string.IsNullOrEmpty(tbEventName.Text))
        {
            sb.Append(string.Format(lblRequired.Text, lblEventName.Text));
            sb.Append("<br />");
        }

        if (string.IsNullOrEmpty(ddlEventCategory.SelectedValue))
        {
            sb.Append(string.Format(lblRequired.Text, lblEventCategory.Text));
            sb.Append("<br />");
        }

        if (string.IsNullOrEmpty(tbEventSDate.Text))
        {
            sb.Append(string.Format(lblRequired.Text, lblEventSDate.Text));
            sb.Append("<br />");
        }

        if (string.IsNullOrEmpty(tbEventSDate.Text))
        {
            sb.Append(string.Format(lblRequired.Text, lblEventEDate.Text));
            sb.Append("<br />");
        }

        if (string.IsNullOrEmpty(tbSignupSDate.Text))
        {
            sb.Append(string.Format(lblRequired.Text, lblSignupSDate.Text));
            sb.Append("<br />");
        }

        if (string.IsNullOrEmpty(tbSignupEDate.Text))
        {
            sb.Append(string.Format(lblRequired.Text, lblSignupEDate.Text));
            sb.Append("<br />");
        }

        if (string.IsNullOrEmpty(ddlSignupTemplate.SelectedValue))
        {
            sb.Append(string.Format(lblRequired.Text, lblSignupTemplate.Text));
            sb.Append("<br />");
        }

        if (rblEventMember.SelectedValue == "C")
            if (string.IsNullOrEmpty(cblCustMember.SelectedValue) && string.IsNullOrEmpty(tbCustMember.Text))
            {
                sb.Append(string.Format(lblRequired.Text, lblEventMember.Text));
                sb.Append("<br />");
            }

        if (string.IsNullOrEmpty(txtEditor.Text))
        {
            sb.Append(string.Format(lblRequired.Text, lblEventDescription.Text));
            sb.Append("<br />");
        }

        if (!string.IsNullOrEmpty(sb.ToString()))
        {
            lblMsg.Text = sb.ToString();

            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Guid.NewGuid().ToString(), "ShowDialogMsg();", true);

            return;
        }
        #endregion

        #region 工號正確性
        string[] OrtherEventManagerEmpid = tbOrtherEventManager.Text.Split(',');

        foreach (string empid in OrtherEventManagerEmpid)
        {
            UserInfo userInfo = new UserInfo(empid);
            if (string.IsNullOrEmpty(userInfo.EmpID))
            {
                if (!string.IsNullOrEmpty(sb.ToString()))
                    sb.Append("、");

                sb.Append(empid);
            }
        }

        if (!string.IsNullOrEmpty(sb.ToString()))
        {
            sb.Append(lblInvalidEmpid.Text);

            lblMsg.Text = $"{lblOrtherEventManager.Text}:{sb.ToString()}";

            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Guid.NewGuid().ToString(), "ShowDialogMsg();", true);

            return;
        }

        if (!string.IsNullOrEmpty(tbCustMember.Text))
        {
            string[] CustMemberEmpid = tbCustMember.Text.Split(',');

            foreach (string empid in CustMemberEmpid)
            {
                UserInfo userInfo = new UserInfo(empid);
                if (string.IsNullOrEmpty(userInfo.EmpID))
                {
                    if (!string.IsNullOrEmpty(sb.ToString()))
                        sb.Append("、");

                    sb.Append(empid);
                }
            }
        }

        if (!string.IsNullOrEmpty(sb.ToString()))
        {
            sb.Append(lblInvalidEmpid.Text);

            lblMsg.Text = $"{lblEventMember.Text}:{sb.ToString()}";


            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Guid.NewGuid().ToString(), "ShowDialogMsg();", true);

            return;
        }
        #endregion

        #region 報名開始日期時間不可以晚於等於報名結束日期時間檢查
        DateTime registerSDate = Convert.ToDateTime(tbSignupSDate.Text);
        DateTime registerEDate = Convert.ToDateTime(tbSignupEDate.Text);

        if (registerSDate.CompareTo(registerEDate) == 0 || registerSDate.CompareTo(registerEDate) > 0)
        {
            lblMsg.Text = lblRegisterDateErr.Text;

            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Guid.NewGuid().ToString(), "ShowDialogMsg();", true);

            return;
        }

        #endregion

        Dictionary<string, string> EventsData = new Dictionary<string, string>();
        Dictionary<string, string> EventAdminData = new Dictionary<string, string>();

        string id = Request.QueryString["id"];

        EventsData.Add("id", Guid.NewGuid().ToString());//活動名稱
        EventsData.Add("name", tbEventName.Text);//活動名稱
        EventsData.Add("categoryid", ddlEventCategory.SelectedValue);//活動分類
        EventsData.Add("eventstart", tbEventSDate.Text);//活動開始日期
        EventsData.Add("eventend", tbEventEDate.Text);//活動結束日期
        EventsData.Add("limit", tbPeopleLimit.Text);//人數限制
        EventsData.Add("registerstart", tbSignupSDate.Text);//活動開始日期
        EventsData.Add("registerend", tbSignupEDate.Text);//活動結束日期
        EventsData.Add("registermodel", ddlSignupTemplate.SelectedValue);//報名表模板
        EventsData.Add("surveymodel", ddlQuestionnaireTemplate.SelectedValue);//問卷模板
        EventsData.Add("enabled", rblPublis.SelectedValue);//是否上架
        EventsData.Add("duplicated", rblDuplicated.SelectedValue);//是否允許重覆報名
        EventsData.Add("member", rblEventMember.SelectedValue);//活動成員

        string mailgroup = string.Empty;
        foreach (ListItem item in cblCustMember.Items)
        {
            if (item.Selected)
            {
                if (!string.IsNullOrEmpty(mailgroup))
                    mailgroup += ",";

                mailgroup += item.Value;
            }
        }
        EventsData.Add("mailgroup", rblEventMember.SelectedValue == "A" ? string.Empty : mailgroup);//活動成員 郵件群組

        EventsData.Add("mailgroupother", rblEventMember.SelectedValue == "A" ? string.Empty : tbCustMember.Text);//活動成員 郵件群組自填

        EventsData.Add("description", Microsoft.Security.Application.Encoder.HtmlEncode(txtEditor.Text));//活動內容

        //圖檔上傳檔名用guid，並用該guid存入DB
        //縮圖
        string ThumbnailFileID = string.Empty;
        string ThumbnailFileName = string.Empty;
        if (Session["ThumbnailFileBytes"] != null)
        {
            ThumbnailFileName = lblThumbnailName.Text;
            string fileExtension = System.IO.Path.GetExtension(ThumbnailFileName).ToLower();

            string path = ConfigurationManager.AppSettings.Get("EventThumbnailPath");
            if (string.IsNullOrEmpty(path))
                path = "~/Event/EventThumbnail";

            ThumbnailFileID = $"{ Guid.NewGuid().ToString() }{ fileExtension }";

            path = Path.Combine(Server.MapPath(".." + path.Substring(1)), ThumbnailFileID);

            byte[] bytes = (byte[])Session["ThumbnailFileBytes"];
            File.WriteAllBytes(path, bytes);

        }

        EventsData.Add("image1", ThumbnailFileID);//活動縮圖
        EventsData.Add("image1_name", ThumbnailFileName);//活動縮圖

        //大圖
        string PictureFileID = string.Empty;
        string PictureFileName = string.Empty;
        if (Session["ThumbnailPictureFileBytes"] != null)
        {
            PictureFileName = lblPictureName.Text;
            string fileExtension = System.IO.Path.GetExtension(PictureFileName).ToLower();

            string path = ConfigurationManager.AppSettings.Get("EventThumbnailPath");
            if (string.IsNullOrEmpty(path))
                path = "~/Event/EventThumbnail";

            PictureFileID = $"{ Guid.NewGuid().ToString() }{ fileExtension }";

            path = Path.Combine(Server.MapPath(".." + path.Substring(1)), PictureFileID);

            byte[] bytes = (byte[])Session["ThumbnailPictureFileBytes"];
            File.WriteAllBytes(path, bytes);

        }

        EventsData.Add("image2", PictureFileID);//活動縮圖
        EventsData.Add("image2_name", PictureFileName);//活動縮圖


        //TEL_Event_EventAdmin
        EventAdminData.Add("eventid", EventsData["id"]);//其他活動管理者 
        EventAdminData.Add("empid", tbOrtherEventManager.Text);//其他活動管理者 TEL_Event_EventAdmin

        Event ev = new Event();
        string result = string.Empty;

        //新增活動
        result = ev.CreateEvent(EventsData, EventAdminData, Page.Session["EmpID"].ToString());

        if (string.IsNullOrEmpty(result))
        {
            //成功
            switch (ddlSignupTemplate.SelectedValue)
            {
                case "1":
                    Response.Redirect($"Event_Model1Options.aspx?id={EventsData["id"]}");
                    break;
                case "2":
                    Response.Redirect($"Event_Model2Options.aspx?id={EventsData["id"]}");
                    break;
                case "3":
                case "4":
                    Response.Redirect($"Event_Model3Options.aspx?id={EventsData["id"]}");
                    break;
                case "5":
                    Response.Redirect("Event.aspx");
                    break;
                default:
                    Response.Redirect($"Event_Model6Options.aspx?id={EventsData["id"]}");

                    break;
            }
        }
        else
        {
            //失敗
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Guid.NewGuid().ToString(), "ShowDialogFailed();", true);
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("Event.aspx");
    }

    protected void btnGoBackEventPage_Click(object sender, EventArgs e)
    {
        Response.Redirect("Event.aspx");
    }

    // 取得活動分類選項
    private void GeneratedCategoryItem()
    {
        this.ddlEventCategory.Items.Clear();

        ListItem li = new ListItem();
        li.Text = lblUnselect.Text;
        li.Value = string.Empty;
        li.Selected = true;
        this.ddlEventCategory.Items.Add(li);

        SystemInfo si = new SystemInfo();
        DataTable dt = si.GetEventCategory("All");

        foreach (DataRow rs in dt.Rows)
        {
            ListItem li1 = new ListItem();
            li1.Text = rs["name"].ToString();
            li1.Value = rs["id"].ToString();

            this.ddlEventCategory.Items.Add(li1);
        }
    }

    /// <summary>
    /// 確認活動圖檔目錄是否存在
    /// </summary>
    private void CheckEventThumbnailPathExist()
    {
        string path = ConfigurationManager.AppSettings.Get("EventThumbnailPath");
        if (string.IsNullOrEmpty(path))
            path = "~/Event/EventThumbnail";

        var folder = Server.MapPath(path);
        if (!Directory.Exists(folder))
        {
            Directory.CreateDirectory(folder);
        }
    }

    /// <summary>
    /// 活動圖檔副檔名是否有效
    /// </summary>
    /// <param name="fileExtension"></param>
    /// <returns></returns>
    private bool IsExtensionValid(string fileExtension)
    {
        string[] restrictExtension = { ".gif", ".jpg", ".jpeg", ".png" };

        //判斷檔案型別是否符合要求 
        for (int i = 0; i < restrictExtension.Length; i++)
        {
            if (fileExtension == restrictExtension[i])
                return true;
        }

        return false;
    }

    /// <summary>
    /// 取得自訂活動成員選項
    /// </summary>
    private void GeneratedCustMemberItem()
    {
        this.cblCustMember.Items.Clear();

        SystemSetup systemSetup = new SystemSetup();
        DataTable dt = systemSetup.GetEventMailGroup(string.Empty);

        foreach (DataRow rs in dt.Rows)
        {
            ListItem li1 = new ListItem();
            li1.Text = rs["name"].ToString();
            li1.Value = rs["name"].ToString();

            this.cblCustMember.Items.Add(li1);
        }
    }

    /// <summary>
    /// 刪除活動
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [WebMethod]
    public static string DeleteEvent(string id)
    {
        Event ev = new Event();
        string result = ev.DeleteEvent(id);

        if (!string.IsNullOrEmpty(result))
        {
            //失敗
            throw new Exception("Failed");
        }

        return "Success";
    }

    
}