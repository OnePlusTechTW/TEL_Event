using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using TEL.Event.Lab.Method;

public partial class Event_Register : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["id"] == null || string.IsNullOrEmpty(Request.QueryString["id"]))
            Response.Redirect("Event.aspx");

        if (!IsPostBack)
        {
            LoadDate();
            BindRegisterGrid();
        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        //登入檢查
        //若為一般使用者則導到Denied頁面
        TEL.Event.Lab.Method.SystemInfo gm = new TEL.Event.Lab.Method.SystemInfo();
        if (gm.IsDenied(Page.Session["EmpID"].ToString(), Request.QueryString["id"]) < 1)
            Response.Redirect("Denied.aspx");
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        BindRegisterGrid();
    }

    protected void btnPreCreate_Click(object sender, EventArgs e)
    {
        string eventid = Request.QueryString["id"].ToString();

        Response.Redirect($"Register_PreCreate.aspx?id={eventid}");
    }

    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        string eventid = Request.QueryString["id"];

        EventInfo evinfo = new EventInfo(eventid);
        ExportExcel ep = new ExportExcel();

        if (evinfo.EventRegisterModel == "1")
            ep.ExportRegisterModel1(eventid, this.txtEmpName.Text.Trim());
        else if (evinfo.EventRegisterModel == "2")
            ep.ExportRegisterModel2(eventid, this.txtEmpName.Text.Trim());
        else if (evinfo.EventRegisterModel == "3")
            ep.ExportRegisterModel3(eventid, this.txtEmpName.Text.Trim());
        else if (evinfo.EventRegisterModel == "4")
            ep.ExportRegisterModel4(eventid, this.txtEmpName.Text.Trim());
        else if (evinfo.EventRegisterModel == "5")
            ep.ExportRegisterModel5(eventid, this.txtEmpName.Text.Trim());
        else if (evinfo.EventRegisterModel == "6")
            ep.ExportRegisterModel6(eventid, this.txtEmpName.Text.Trim());
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        //Event_RegisterModel6_Edit.aspx? eventid = 7A77F81C - 2DB4 - 423A - B513 - 63897E3C0FA7 & id = E8FEC8DE - AEB7 - 49AE - A1F1 - D8FDDEE6AD56 & page = Register
        string registerid = ((Button)sender).CommandArgument.ToString();
        string eventid = Request.QueryString["id"];
        EventInfo ev = new EventInfo(eventid);
        
        Response.Redirect($"Event_RegisterModel{ev.EventRegisterModel}_Edit.aspx?eventid={eventid}&id={registerid}&page=Register");
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        Button btnView = (Button)sender;
        if (btnView == null)
            return;

        string registerid = ((Button)sender).CommandArgument.ToString();
        string eventid = Request.QueryString["id"];
        EventInfo ev = new EventInfo(eventid);

        //Response.Redirect($"Event_RegisterModel{registermodel}_View.aspx?eventid={eventid}&id={id}");
        string page = $"Event_RegisterModel{ev.EventRegisterModel}_View.aspx?eventid={eventid}&id={registerid}";
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Guid.NewGuid().ToString(), $"ShowDialogView('" + page + "');", true);
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        Button btnView = (Button)sender;
        if (btnView == null)
            return;

        string eventid = Request.QueryString["id"];
        string registerid = ((Button)sender).CommandArgument.ToString();

        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Guid.NewGuid().ToString(), "ShowDialogDelete('" + eventid + "|"+ registerid + "');", true);
    }

    protected void btnReloadGridView_Click(object sender, EventArgs e)
    {
        BindRegisterGrid();
    }

    protected void gridRegister_RowDataBound(object sender, GridViewRowEventArgs e)
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

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //由於是連結按鈕所以宣告一個連結按鈕，根據實際情況變動
                //Button bt = (Button)e.Row.FindControl("Button_Delete");
                //bt.Attributes.Add("onclick", "javascrip:return confirm('您確定要刪除此筆資料?')");
            }
        }
    }

    //GridView換頁
    protected void gridRegister_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gridRegister.PageIndex = e.NewPageIndex;
        BindRegisterGrid();
    }

    //讀取Event資料
    protected void LoadDate()
    {
        string eventid = Request.QueryString["id"];
        EventInfo evinfo = new EventInfo(eventid);
        Event ev = new Event();

        this.lblCategory.ForeColor = ColorTranslator.FromHtml(evinfo.EventCategoryColor);
        this.lblCategory.Text = "．" + evinfo.EventCategory;
        this.lblEventName.Text = evinfo.EventName;
        this.lblCount.Text = $"{ev.GetEvnetRegisterCount(eventid, evinfo.EventRegisterModel)}/{(evinfo.EventLimit == string.Empty ? lblLimit.Text : evinfo.EventLimit)}";
    }

    //查詢資料
    protected void BindRegisterGrid()
    {
        string eventid = Request.QueryString["id"];
        EventInfo ev = new EventInfo(eventid);
        Register re = new Register();
        DataTable dt = new DataTable();
        
        dt = re.GetRegister(eventid, ev.EventRegisterModel, this.txtEmpName.Text.Trim());

        this.gridRegister.DataSource = dt;
        this.gridRegister.DataBind();
    }

    /// <summary>
    /// 刪除活動報名
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [WebMethod]
    public static string DeleteRegisterModel(string id)
    {
        string[] param = id.Split('|');
        string eventid = param[0];
        string registerid = param[1];

        EventInfo evinfo = new EventInfo(eventid);
        Event ev = new Event();

        string result = string.Empty;
        switch (evinfo.EventRegisterModel)
        {
            case "1":
                result = ev.DeleteRegisterModel1(registerid);
                break;
            case "2":
                result = ev.DeleteRegisterModel2(registerid);
                break;
            case "3":
                result = ev.DeleteRegisterModel3(registerid);
                break;
            case "4":
                result = ev.DeleteRegisterModel4(registerid);
                break;
            case "5":
                result = ev.DeleteRegisterModel5(registerid);
                break;
            case "6":
                result = ev.DeleteRegisterModel6(registerid);
                break;
            default:
                break;
        }

        if (!string.IsNullOrEmpty(result))
        {
            //失敗
            throw new Exception("Failed");
        }

        return "Success";
    }
}