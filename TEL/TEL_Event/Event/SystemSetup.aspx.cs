using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using TEL.Event.Lab.Method;

public partial class Event_SystemSetup : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetDefaultData();
        }

        //postback後，會將前端 ddlCategoryColor 顏色還原，故在postback時，再設定一次ddlCategoryColor 顏色
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Guid.NewGuid().ToString(), "ddlCategoryColorOnChange();", true);
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Guid.NewGuid().ToString(), "Menu(null, true);", true);
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        //登入檢查
        //若不為系統管理者則導到Denied頁面
        TEL.Event.Lab.Method.SystemInfo gm = new TEL.Event.Lab.Method.SystemInfo();
        if (gm.IsManager(Page.Session["EmpID"].ToString()) != 3)
            Response.Redirect("Denied.aspx");
    }

    private void GetDefaultData()
    {
        GetGridEventCategory();
        GetGridEventManager();
        GetGridMailGroup();
        GetGridHealthGroup();
    }

    /// <summary>
    ///  活動分類 bind grid
    /// </summary>
    private void GetGridEventCategory()
    {
        SystemSetup systemSetup = new SystemSetup();
        DataTable dt = systemSetup.GetEventCategory(string.Empty);

        this.gridEventCategory.DataSource = dt;
        this.gridEventCategory.DataBind();
    }

    /// <summary>
    ///  常態活動管理 bind grid
    /// </summary>
    private void GetGridEventManager()
    {
        SystemSetup systemSetup = new SystemSetup();
        DataTable dt = systemSetup.GetEventManager(string.Empty);

        this.gridEventManager.DataSource = dt;
        this.gridEventManager.DataBind();
    }
    /// <summary>
    ///  郵件群組 bind grid
    /// </summary>
    private void GetGridMailGroup()
    {
        SystemSetup systemSetup = new SystemSetup();
        DataTable dt = systemSetup.GetEventMailGroup(string.Empty);

        this.gridMailGroup.DataSource = dt;
        this.gridMailGroup.DataBind();
    }

    /// <summary>
    ///  員工報名健檢組別  bind grid
    /// </summary>
    private void GetGridHealthGroup()
    {
        SystemSetup systemSetup = new SystemSetup();
        DataTable dt = systemSetup.GetHealthGroup();

        this.gridHealthGroup.DataSource = dt;
        this.gridHealthGroup.DataBind();
    }

    #region Category
    /// <summary>
    /// 新增活動類別
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button_AddCategory_Click(object sender, EventArgs e)
    {
        string name = tbCategoryName.Text;
        string color = ddlCategoryColor.SelectedValue;
        string enabled = ddlIsEnableCategory.SelectedValue;


        StringBuilder sb = new StringBuilder();

        //分類名稱必填
        if (string.IsNullOrEmpty(name))
        {
            sb.Append(string.Format(lblRequired.Text, lblCategoryName.Text));
            sb.Append("<br />");
        }
        //分類顏色必填
        if (string.IsNullOrEmpty(color))
        {
            sb.Append(string.Format(lblRequired.Text, lblCategoryColor.Text));
            sb.Append("<br />");
        }

        if (!string.IsNullOrEmpty(sb.ToString()))
        {
            lblRequiredMsg.Text = sb.ToString();
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Guid.NewGuid().ToString(), "ShowDialogRequired();", true);
        }
        else
        {
            SystemSetup systemSetup = new SystemSetup();

            DataTable dt = systemSetup.GetEventCategory(name);
            if (dt.Rows.Count > 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Guid.NewGuid().ToString(), "ShowDialogMsg('" + $"{lblEventCategory.Text} {lblExist.Text}" + "');", true);
            }
            else
            {
                string result = systemSetup.AddEventCategory(name, color, enabled, Page.Session["EmpID"].ToString());
                if (string.IsNullOrEmpty(result))
                {
                    //新增成功
                    GetGridEventCategory();

                }
                else
                {
                    //新增失敗
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Guid.NewGuid().ToString(), "ShowDialogFailed();", true);

                }
            }

        }
    }

    /// <summary>
    /// 儲存活動類別
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button_SaveCategory_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
        TextBox txtCategoryName = (TextBox)row.FindControl("txtCategoryName");
        DropDownList gridDdlCategoryColor = (DropDownList)row.FindControl("gridDdlCategoryColor");
        DropDownList gridDdlIsEnableCategory = (DropDownList)row.FindControl("gridDdlIsEnableCategory");

        string id = btn.CommandArgument.ToString();
        string name = txtCategoryName.Text;
        string color = gridDdlCategoryColor.SelectedValue;
        string enabled = gridDdlIsEnableCategory.SelectedValue;

        StringBuilder sb = new StringBuilder();

        //分類名稱必填
        if (string.IsNullOrEmpty(name))
        {
            sb.Append(string.Format(lblRequired.Text, lblCategoryName.Text));
            sb.Append("<br />");
        }
        //分類顏色必填
        if (string.IsNullOrEmpty(color))
        {
            sb.Append(string.Format(lblRequired.Text, lblCategoryColor.Text));
            sb.Append("<br />");
        }

        if (!string.IsNullOrEmpty(sb.ToString()))
        {
            lblRequiredMsg.Text = sb.ToString();
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Guid.NewGuid().ToString(), "ShowDialogRequired();", true);

            return;
        }

        SystemSetup systemSetup = new SystemSetup();
        DataTable dt = systemSetup.GetEventCategoryWithoutSelf(name, id);
        if (dt.Rows.Count > 0)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Guid.NewGuid().ToString(), "ShowDialogMsg('" + $"{lblEventCategory.Text} {lblExist.Text}" + "');", true);
        }
        else
        {
            string result = systemSetup.SaveEventCategory(id, name, color, enabled, Page.Session["EmpID"].ToString());
            if (string.IsNullOrEmpty(result))
            {
                //成功
                GetGridEventCategory();
            }
            else
            {
                //失敗
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Guid.NewGuid().ToString(), "ShowDialogFailed();", true);
            }
        }
    }

    /// <summary>
    /// 刪除活動類別
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button_DeleteCategory_Click(object sender, EventArgs e)
    {
        //Button btn = (Button)sender;
        //string id = btn.CommandArgument.ToString();

        //SystemSetup systemSetup = new SystemSetup();
        //string result = systemSetup.DeleteEventCategory(id);
        //if (string.IsNullOrEmpty(result))
        //{
        //    //成功
        //    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Guid.NewGuid().ToString(), "ShowDialogSuccess();", true);
        //    GetGridEventCategory();
        //}
        //else
        //{
        //    //失敗
        //    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Guid.NewGuid().ToString(), "ShowDialogFailed();", true);
        //}
    }

    /// <summary>
    /// 刪除活動類別
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [WebMethod]
    public static string DeleteCategory(string id)
    {
        Event ev = new Event();
        DataTable dt = new DataTable();

        dt = ev.GetEventInfo(string.Empty, string.Empty, id, string.Empty, string.Empty, string.Empty, string.Empty);

        if (dt.Rows.Count == 0)
        {
            SystemSetup systemSetup = new SystemSetup();
            string result = systemSetup.DeleteEventCategory(id);

            if (!string.IsNullOrEmpty(result))
            {
                //失敗
                throw new Exception("Failed");
            }
        }
        else
        {
            return "BeUsedCategory";
        }
        return "SuccessCategory";
    }

    /// <summary>
    /// 刪除分類後 重新reload grid
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnReloadCategoryGrid_Click(object sender, EventArgs e)
    {
        GetGridEventCategory();
    }

    protected void gridEventCategory_RowDataBound(object sender, GridViewRowEventArgs e)
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

            DataRowView dv = (DataRowView)e.Row.DataItem;
            TextBox txtCategoryName = (TextBox)e.Row.FindControl("txtCategoryName");
            txtCategoryName.Text = dv["name"].ToString(); ;

            //分類顏色
            DropDownList gridDdlCategoryColor = (DropDownList)e.Row.FindControl("gridDdlCategoryColor");
            gridDdlCategoryColor.SelectedValue = dv["color"].ToString();
            gridDdlCategoryColor.CssClass = "QueryField " + GetColorCss(dv["color"].ToString());

            //分類是否啟用
            DropDownList gridDdlIsEnableCategory = (DropDownList)e.Row.FindControl("gridDdlIsEnableCategory");
            if (dv["enabled"].ToString().ToUpper() == "Y")
            {
                gridDdlIsEnableCategory.SelectedValue = "1";
            }
            else
                gridDdlIsEnableCategory.SelectedValue = "0";
        }
    }

    //GridView換頁
    protected void gridEventCategory_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gridEventCategory.PageIndex = e.NewPageIndex;
        GetGridEventCategory();
    }

    /// <summary>
    /// GridView 分類顏色下拉選單 Changed 事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gridDdlCategoryColor_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddl = (DropDownList)sender;
        GridViewRow row = (GridViewRow)ddl.NamingContainer;
        DropDownList gridDdlCategoryColor = (DropDownList)row.FindControl("gridDdlCategoryColor");

        //替換下拉選單底色
        gridDdlCategoryColor.CssClass = "QueryField " + GetColorCss(gridDdlCategoryColor.SelectedValue);
    }

    /// <summary>
    /// 取得色碼css
    /// </summary>
    /// <param name="color"></param>
    /// <returns></returns>
    private string GetColorCss(string color)
    {
        switch (color)
        {
            case "#00A9E0":
                return "ddlColor1";
            case "#71C5E8":
                return "ddlColor2";
            case "#00629B":
                return "ddlColor3";
            case "#78BE20":
                return "ddlColor4";
            case "#B7DD79":
                return "ddlColor5";
            case "#658D1B":
                return "ddlColor6";
            case "#DA1884":
                return "ddlColor7";
            case "#F395C7":
                return "ddlColor8";
            case "#A50050":
                return "ddlColor9";
            case "#00B2A9":
                return "ddlColor10";
            case "#9CDBD9":
                return "ddlColor11";
            case "#007367":
                return "ddlColor12";
            case "#8031A7":
                return "ddlColor13";
            case "#CAA2DD":
                return "ddlColor14";
            case "#572C5F":
                return "ddlColor15";
            case "#EEDC00":
                return "ddlColor16";
            case "#F0EC74":
                return "ddlColor17";
            case "#BBA600":
                return "ddlColor18";
            case "#FF6A13":
                return "ddlColor19";
            case "#FAAA8D":
                return "ddlColor20";
            case "#A65523":
                return "ddlColor21";
            default:
                return string.Empty;
        }
    }
    #endregion

    #region Event Admin
    /// <summary>
    /// 新增常態活動管理者
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAddManager_Click(object sender, EventArgs e)
    {
        string empid = tbEmpid.Text;

        if (string.IsNullOrEmpty(empid))
        {
            lblRequiredMsg.Text = string.Format(lblRequired.Text, lblEmpid.Text);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Guid.NewGuid().ToString(), "ShowDialogRequired();", true);
        }
        else
        {
            UserInfo userInfo = new UserInfo(empid);
            if (string.IsNullOrEmpty(userInfo.EmpID))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Guid.NewGuid().ToString(), "ShowDialogMsg('" + lblInvalidEmpid.Text+"');", true);
            }
            else
            {
                SystemSetup systemSetup = new SystemSetup();

                DataTable dt = systemSetup.GetEventManager(empid);
                if (dt.Rows.Count > 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Guid.NewGuid().ToString(), "ShowDialogMsg('" + $"{lblEventManager.Text} {lblExist.Text}" + "');", true);
                }
                else
                {

                    string result = systemSetup.AddEventAdmin(empid, Page.Session["EmpID"].ToString());
                    if (string.IsNullOrEmpty(result))
                    {
                        //新增成功
                        GetGridEventManager();
                    }
                    else
                    {
                        //新增失敗
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Guid.NewGuid().ToString(), "ShowDialogFailed();", true);

                    }
                }

            }

        }
    }

    /// <summary>
    /// 刪除常態活動管理者
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button_DeleteManager_Click(object sender, EventArgs e)
    {
        //Button btn = (Button)sender;
        //string empID = btn.CommandArgument.ToString();

        //SystemSetup systemSetup = new SystemSetup();
        //string result = systemSetup.DeleteEventAdmin(empID);
        //if (string.IsNullOrEmpty(result))
        //{
        //    //成功
        //    GetGridEventManager();

        //}
        //else
        //{
        //    //失敗
        //    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Guid.NewGuid().ToString(), "ShowDialogFailed();", true);

        //}
    }

    /// <summary>
    /// 刪除活動類別
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [WebMethod]
    public static string DeleteManager(string id)
    {
        SystemSetup systemSetup = new SystemSetup();
        string result = systemSetup.DeleteEventAdmin(id);

        if (!string.IsNullOrEmpty(result))
        {
            //失敗
            throw new Exception("Failed");
        }

        return "SuccessManager";
    }

    /// <summary>
    /// 刪除分類後 重新reload grid
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnReloadManagerGrid_Click(object sender, EventArgs e)
    {
        GetGridEventManager();
    }

    protected void gridEventManager_RowDataBound(object sender, GridViewRowEventArgs e)
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

    //GridView換頁

    protected void gridEventManager_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gridEventCategory.PageIndex = e.NewPageIndex;
        GetGridEventManager();
    }
    #endregion

    #region MailGroup
    /// <summary>
    /// 新增郵件群組
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button_AddMailGroup_Click(object sender, EventArgs e)
    {
        string name = tbMailGroup.Text;
        string enabled = ddlIsEnableMailGroup.SelectedValue;


        if (string.IsNullOrEmpty(name))
        {
            lblRequiredMsg.Text = string.Format(lblRequired.Text, lblMailGroup.Text);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Guid.NewGuid().ToString(), "ShowDialogRequired();", true);
        }
        else
        {
            SystemSetup systemSetup = new SystemSetup();
            DataTable dt = systemSetup.GetEventMailGroup(name);
            if (dt.Rows.Count > 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Guid.NewGuid().ToString(), "ShowDialogMsg('" + $"{lblMailGroup.Text} {lblExist.Text}" + "');", true);
            }
            else
            {
                MailGroup mg = new MailGroup();
                bool isMailGroupInvalid = true;
                isMailGroupInvalid = mg.IsMailGroupExist(name);
                if (!isMailGroupInvalid)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Guid.NewGuid().ToString(), "ShowDialogMsg('" + lblInvalidMailGroup.Text + "');", true);
                }
                else
                {
                    string result = systemSetup.AddMailGroup(name, enabled, Page.Session["EmpID"].ToString());
                    if (string.IsNullOrEmpty(result))
                    {
                        //新增成功
                        GetGridMailGroup();

                    }
                    else
                    {
                        //新增失敗
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Guid.NewGuid().ToString(), "ShowDialogFailed();", true);

                    }
                }
            }
        }
    }

    /// <summary>
    /// 儲存郵件群組
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button_SaveMailGroup_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
        DropDownList gridDdlIsEnableMailGroup = (DropDownList)row.FindControl("gridDdlIsEnableMailGroup");

        string id = btn.CommandArgument.ToString();
        string enabled = gridDdlIsEnableMailGroup.SelectedValue;

        SystemSetup systemSetup = new SystemSetup();
        string result = systemSetup.SaveMailGroup(id, enabled, Page.Session["EmpID"].ToString());
        if (string.IsNullOrEmpty(result))
        {
            //成功
            GetGridMailGroup();
        }
        else
        {
            //失敗
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Guid.NewGuid().ToString(), "ShowDialogFailed();", true);
        }
    }

    /// <summary>
    /// 刪除郵件群組
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button_DeleteMailGroup_Click(object sender, EventArgs e)
    {
        //Button btn = (Button)sender;
        //string id = btn.CommandArgument.ToString();

        //SystemSetup systemSetup = new SystemSetup();
        //string result = systemSetup.DeleteMailGroup(id);
        //if (string.IsNullOrEmpty(result))
        //{
        //    //成功
        //    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Guid.NewGuid().ToString(), "ShowDialogSuccess();", true);
        //    GetGridMailGroup();
        //}
        //else
        //{
        //    //失敗
        //    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Guid.NewGuid().ToString(), "ShowDialogFailed();", true);
        //}
    }

    /// <summary>
    /// 刪除活動類別
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [WebMethod]
    public static string DeleteMailGroup(string id, string mailgroup)
    {
        Event ev = new Event();
        DataTable dt = new DataTable();

        dt = ev.GetEventPermissionMailGroup(mailgroup);

        if (dt.Rows.Count == 0)
        {
            SystemSetup systemSetup = new SystemSetup();
            string result = systemSetup.DeleteMailGroup(id);

            if (!string.IsNullOrEmpty(result))
            {
                //失敗
                throw new Exception("Failed");
            }
        }
        else
        {
            return "BeUsedeMailGroup";
        }
        

        return "SuccessMailGroup";
    }

    /// <summary>
    /// 刪除分類後 重新reload grid
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnReloadMailGroupGrid_Click(object sender, EventArgs e)
    {
        GetGridMailGroup();
    }

    protected void gridMailGroup_RowDataBound(object sender, GridViewRowEventArgs e)
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

            DataRowView dv = (DataRowView)e.Row.DataItem;

            //郵件群組是否啟用
            DropDownList gridDdlIsEnableMailGroup = (DropDownList)e.Row.FindControl("gridDdlIsEnableMailGroup");
            if (dv["enabled"].ToString().ToUpper() == "Y")
            {
                gridDdlIsEnableMailGroup.SelectedValue = "1";
            }
            else
                gridDdlIsEnableMailGroup.SelectedValue = "0";
        }
    }


    //GridView換頁
    protected void gridMailGroup_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gridEventCategory.PageIndex = e.NewPageIndex;
        GetGridMailGroup();
    }
    #endregion



    protected void btnImportHealthGroup_Click(object sender, EventArgs e)
    {
        tbImportMsg.Text = string.Empty;
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Guid.NewGuid().ToString(), "ShowDialogFileUpload();", true);
    }

    protected void btnImport_Click(object sender, EventArgs e)
    {
        try
        {
            if (FileUpload1.HasFile)
            {
                List<UserHealthGroup> listUserHealthGroup = new List<UserHealthGroup>();
                listUserHealthGroup = ExcelToList();

                //
                var duplicateList = listUserHealthGroup.AsEnumerable().GroupBy(x => new { x.empid })
                     .Select(group => new
                     {
                         group.Key.empid,
                         Count = group.Count()
                     }).Where(a => a.Count > 1).
                     Select(g => g.empid).ToList();

                string InvalidEmpid = string.Empty;
                foreach (var UserHealthGroup in listUserHealthGroup)
                {
                    UserInfo userInfo = new UserInfo(UserHealthGroup.empid);
                    if (string.IsNullOrEmpty(userInfo.EmpID))
                        InvalidEmpid += UserHealthGroup.empid + ",";
                }

                StringBuilder sb = new StringBuilder();

                if (!string.IsNullOrEmpty(InvalidEmpid))
                {
                    sb.Append($"{InvalidEmpid.Substring(0, InvalidEmpid.Length - 1)} {lblInvalidEmpid2.Text}");
                    sb.AppendLine();
                    sb.AppendLine();
                    sb.AppendLine();
                    sb.Append(lblReimport.Text);
                    tbImportMsg.Text = sb.ToString();
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Guid.NewGuid().ToString(), "ShowDialogFileUpload();", true);
                }
                else if (duplicateList.Count > 0)
                {

                    sb.Append(lblDuplicate.Text);
                    sb.AppendLine();

                    string empinList = string.Empty;
                    foreach (string empid in duplicateList)
                    {
                        empinList += empid + ",";
                    }
                    sb.Append(empinList.Substring(0, empinList.Length - 1));
                    sb.AppendLine();
                    sb.AppendLine();
                    sb.AppendLine();
                    sb.Append(lblReimport.Text);
                    tbImportMsg.Text = sb.ToString();
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Guid.NewGuid().ToString(), "ShowDialogFileUpload();", true);

                }
                else
                {
                    //寫入DB
                    SystemSetup systemSetup = new SystemSetup();
                    string result = systemSetup.AddUserHealthGroup(listUserHealthGroup, Page.Session["EmpID"].ToString());
                    if (string.IsNullOrEmpty(result))
                    {
                        //成功
                        tbImportMsg.Text = lblImportSuccess.Text;
                        GetGridHealthGroup();
                    }
                    else
                    {
                        //失敗
                        sb.Append(lblImportFailed.Text);
                        sb.AppendLine();
                        sb.AppendLine();
                        sb.Append(lblImportFailedMsg.Text);
                        sb.AppendLine();
                        sb.Append(result);


                        tbImportMsg.Text = sb.ToString();
                    }

                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Guid.NewGuid().ToString(), "ShowDialogFileUpload();", true);

                }
            }
        }
        catch (Exception ex)
        {

        }
    }

    


    /// <summary>
    /// Excel匯入成Datable
    /// </summary>
    /// <param name="file">匯入路徑(包含檔名與副檔名)</param>
    /// <returns></returns>
    public List<UserHealthGroup> ExcelToList()
    {
        DataTable dt = new DataTable();
        IWorkbook workbook;
        string fileExt = Path.GetExtension(FileUpload1.FileName).ToLower();
        Stream uploadFileStream = FileUpload1.PostedFile.InputStream;

        //XSSFWorkbook 適用XLSX格式，HSSFWorkbook 適用XLS格式
        if (fileExt == ".xlsx")
        {
            workbook = new XSSFWorkbook(uploadFileStream);
        }
        else if (fileExt == ".xls")
        {
            workbook = new HSSFWorkbook(uploadFileStream);
        }
        else
        {
            workbook = null;
        }
        if (workbook == null) { return null; }

        ISheet sheet = workbook.GetSheetAt(0);

        //表頭  
        IRow header = sheet.GetRow(sheet.FirstRowNum);
        List<int> columns = new List<int>();
        for (int i = 0; i < header.LastCellNum; i++)
        {
            object obj = GetValueType(header.GetCell(i));
            if (obj == null || obj.ToString() == string.Empty)
            {
                dt.Columns.Add(new DataColumn("Columns" + i.ToString()));
            }
            else
                dt.Columns.Add(new DataColumn(obj.ToString()));
            columns.Add(i);
        }
        //資料  
        for (int i = sheet.FirstRowNum + 1; i <= sheet.LastRowNum; i++)
        {
            UserHealthGroup group = new UserHealthGroup();
            DataRow dr = dt.NewRow();
            bool hasValue = false;
            foreach (int j in columns)
            {
                dr[j] = GetValueType(sheet.GetRow(i).GetCell(j));
                if (dr[j] != null && dr[j].ToString() != string.Empty)
                {
                    hasValue = true;
                }
            }
            if (hasValue)
            {
                dt.Rows.Add(dr);
            }
        }

        List<UserHealthGroup> list = new List<UserHealthGroup>();
        list = (from DataRow dr in dt.Rows
                       select new UserHealthGroup()
                       {
                           empid = dr["工號"].ToString(),
                           groupid = dr["員工健檢報名組別"].ToString()
                       }).ToList();


        return list;
    }

    /// <summary>
    /// 獲取單元格型別
    /// </summary>
    /// <param name="cell"></param>
    /// <returns></returns>
    private static object GetValueType(ICell cell)
    {
        if (cell == null)
            return null;
        switch (cell.CellType)
        {
            case CellType.Blank: //BLANK:  
                return null;
            case CellType.Boolean: //BOOLEAN:  
                return cell.BooleanCellValue;
            case CellType.Numeric: //NUMERIC:  
                return cell.NumericCellValue;
            case CellType.String: //STRING:  
                return cell.StringCellValue;
            case CellType.Error: //ERROR:  
                return cell.ErrorCellValue;
            case CellType.Formula: //FORMULA:  
            default:
                return "=" + cell.CellFormula;
        }
    }

    protected void gridHealthGroup_RowDataBound(object sender, GridViewRowEventArgs e)
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


    //GridView換頁
    protected void gridHealthGroup_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gridEventCategory.PageIndex = e.NewPageIndex;
        GetGridHealthGroup();
    }


}