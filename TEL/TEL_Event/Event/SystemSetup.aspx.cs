using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
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

    private void GetDefaultData()
    {
        GetGridEventCategory();
        GetGridEventManager();
        GetGridMailGroup();
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
        DataTable dt = systemSetup.GetMailGroup(string.Empty);

        this.gridMailGroup.DataSource = dt;
        this.gridMailGroup.DataBind();
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

        if (string.IsNullOrEmpty(name))
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Guid.NewGuid().ToString(), "ShowDialogRequired('CategoryName');", true);
        }
        else
        {
            SystemSetup systemSetup = new SystemSetup();

            DataTable dt = systemSetup.GetEventCategory(name);
            if (dt.Rows.Count > 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Guid.NewGuid().ToString(), "ShowDialogExist('CategoryName');", true);
            }
            else
            {
                string result = systemSetup.AddEventCategory(name, color, enabled, Page.Session["EmpID"].ToString());
                if (string.IsNullOrEmpty(result))
                {
                    //新增成功
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Guid.NewGuid().ToString(), "ShowDialogSuccess();", true);
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
        DropDownList gridDdlCategoryColor = (DropDownList)row.FindControl("gridDdlCategoryColor");
        DropDownList gridDdlIsEnableCategory = (DropDownList)row.FindControl("gridDdlIsEnableCategory");

        string id = btn.CommandArgument.ToString();
        string color = gridDdlCategoryColor.SelectedValue;
        string enabled = gridDdlIsEnableCategory.SelectedValue;

        SystemSetup systemSetup = new SystemSetup();
        string result = systemSetup.SaveEventCategory(id, color, enabled, Page.Session["EmpID"].ToString());
        if (string.IsNullOrEmpty(result))
        {
            //成功
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Guid.NewGuid().ToString(), "ShowDialogSuccess();", true);
            GetGridEventCategory();
        }
        else
        {
            //失敗
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Guid.NewGuid().ToString(), "ShowDialogFailed();", true);
        }
    }

    /// <summary>
    /// 刪除活動類別
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button_DeleteCategory_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        string id = btn.CommandArgument.ToString();

        SystemSetup systemSetup = new SystemSetup();
        string result = systemSetup.DeleteEventCategory(id);
        if (string.IsNullOrEmpty(result))
        {
            //成功
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Guid.NewGuid().ToString(), "ShowDialogSuccess();", true);
            GetGridEventCategory();
        }
        else
        {
            //失敗
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Guid.NewGuid().ToString(), "ShowDialogFailed();", true);
        }
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

            //分類顏色
            DropDownList gridDdlCategoryColor = (DropDownList)e.Row.FindControl("gridDdlCategoryColor");
            gridDdlCategoryColor.SelectedValue = dv["color"].ToString();
            gridDdlCategoryColor.CssClass = "QueryField " + GetColorCss(dv["color"].ToString());

            //分類是否啟用
            DropDownList gridDdlIsEnableCategory = (DropDownList)e.Row.FindControl("gridDdlIsEnableCategory");
            gridDdlIsEnableCategory.SelectedValue = dv["enabled"].ToString();
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
            case "00A9E0":
                return "ddlColor1";
            case "71C5E8":
                return "ddlColor2";
            case "00629B":
                return "ddlColor3";
            case "78BE20":
                return "ddlColor4";
            case "B7DD79":
                return "ddlColor5";
            case "658D1B":
                return "ddlColor6";
            case "DA1884":
                return "ddlColor7";
            case "F395C7":
                return "ddlColor8";
            case "A50050":
                return "ddlColor9";
            case "00B2A9":
                return "ddlColor10";
            case "9CDBD9":
                return "ddlColor11";
            case "007367":
                return "ddlColor12";
            case "8031A7":
                return "ddlColor13";
            case "CAA2DD":
                return "ddlColor14";
            case "572C5F":
                return "ddlColor15";
            case "EEDC00":
                return "ddlColor16";
            case "F0EC74":
                return "ddlColor17";
            case "BBA600":
                return "ddlColor18";
            case "FF6A13":
                return "ddlColor19";
            case "FAAA8D":
                return "ddlColor20";
            case "A65523":
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
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Guid.NewGuid().ToString(), "ShowDialogRequired('Empid');", true);
        }
        else
        {
            UserInfo userInfo = new UserInfo(empid);
            if (string.IsNullOrEmpty(userInfo.EmpID))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Guid.NewGuid().ToString(), "ShowDialogEmpidErr('Empid');", true);
            }
            else
            {
                SystemSetup systemSetup = new SystemSetup();

                DataTable dt = systemSetup.GetEventManager(empid);
                if (dt.Rows.Count > 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Guid.NewGuid().ToString(), "ShowDialogExist('Empid');", true);
                }
                else
                {

                    string result = systemSetup.AddEventAdmin(empid, Page.Session["EmpID"].ToString());
                    if (string.IsNullOrEmpty(result))
                    {
                        //新增成功
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Guid.NewGuid().ToString(), "ShowDialogSuccess();", true);
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
        Button btn = (Button)sender;
        string empID = btn.CommandArgument.ToString();

        SystemSetup systemSetup = new SystemSetup();
        string result = systemSetup.DeleteEventAdmin(empID);
        if (string.IsNullOrEmpty(result))
        {
            //成功
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Guid.NewGuid().ToString(), "ShowDialogSuccess();", true);
            GetGridEventManager();

        }
        else
        {
            //失敗
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Guid.NewGuid().ToString(), "ShowDialogFailed();", true);

        }
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
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Guid.NewGuid().ToString(), "ShowDialogRequired('MailGroup');", true);
        }
        else
        {
            SystemSetup systemSetup = new SystemSetup();
            DataTable dt = systemSetup.GetMailGroup(name);
            if (dt.Rows.Count > 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Guid.NewGuid().ToString(), "ShowDialogExist('MailGroup');", true);
            }
            else
            {
                string result = systemSetup.AddMailGroup(name, enabled, Page.Session["EmpID"].ToString());
                if (string.IsNullOrEmpty(result))
                {
                    //新增成功
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Guid.NewGuid().ToString(), "ShowDialogSuccess();", true);
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
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Guid.NewGuid().ToString(), "ShowDialogSuccess();", true);
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
        Button btn = (Button)sender;
        string id = btn.CommandArgument.ToString();

        SystemSetup systemSetup = new SystemSetup();
        string result = systemSetup.DeleteMailGroup(id);
        if (string.IsNullOrEmpty(result))
        {
            //成功
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Guid.NewGuid().ToString(), "ShowDialogSuccess();", true);
            GetGridMailGroup();
        }
        else
        {
            //失敗
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Guid.NewGuid().ToString(), "ShowDialogFailed();", true);
        }
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
            DropDownList gridDdlIsEnableCategory = (DropDownList)e.Row.FindControl("gridDdlIsEnableMailGroup");
            gridDdlIsEnableCategory.SelectedValue = dv["enabled"].ToString();
        }
    }


    //GridView換頁
    protected void gridMailGroup_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gridEventCategory.PageIndex = e.NewPageIndex;
        GetGridMailGroup();
    }
    #endregion
}