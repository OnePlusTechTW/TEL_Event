using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using TEL.Event.Lab.Method;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Button = System.Web.UI.WebControls.Button;

public partial class Event_Survey : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //登入檢查
        //若為一般使用者則導到Denied頁面
        TEL.Event.Lab.Method.SystemInfo gm = new TEL.Event.Lab.Method.SystemInfo();
        if (gm.IsManager(Page.Session["EmpID"].ToString()) == 0)
            Response.Redirect("Denied.aspx");

        //需有活動ID檢查
        if (Request.QueryString["id"] == null || string.IsNullOrEmpty(Request.QueryString["id"]))
            Response.Redirect("/Event/Event.aspx");

        if (!IsPostBack)
        {
            LoadDate();
            QueryData();
        }
    }

    protected void Button_Query_Click(object sender, EventArgs e)
    {
        QueryData();
    }

    protected void Button_ExportExcel_Click(object sender, EventArgs e)
    {
        string eventid = Request.QueryString["id"];
        EventInfo ev = new EventInfo(eventid);
        ExportExcel ep = new ExportExcel();

        if (ev.EventSurveyModel == "1")
            ep.ExportSurveyModel1(Request.QueryString["id"]);
        else if (ev.EventSurveyModel == "2")
            ep.ExportSurveyModel2(Request.QueryString["id"]);
        else if (ev.EventSurveyModel == "3")
            ep.ExportSurveyModel3(Request.QueryString["id"]);
        else if (ev.EventSurveyModel == "4")
            ep.ExportSurveyModel4(Request.QueryString["id"]);
    }

    protected void Button_SurveyView_Click(object sender, EventArgs e)
    {
        //si[0]=Survey ID
        //si[1]=Survey Model
        string[] si = ((Button)sender).CommandArgument.ToString().Split('_');

        switch (si[1])
        {
            case "1":
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Guid.NewGuid().ToString(), "ShowDialogLoadPage('/Event/Event_SurveyModel1_View.aspx?id=" + si[0] + "',750,1000);", true);
                break;
            case "2":
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Guid.NewGuid().ToString(), "ShowDialogLoadPage('/Event/Event_SurveyModel2_View.aspx?id=" + si[0] + "',750,1000);", true);
                break;
            case "3":
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Guid.NewGuid().ToString(), "ShowDialogLoadPage('/Event/Event_SurveyModel3_View.aspx?id=" + si[0] + "',750,1000);", true);
                break;
            case "4":
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Guid.NewGuid().ToString(), "ShowDialogLoadPage('/Event/Event_SurveyModel4_View.aspx?id=" + si[0] + "',750,1000);", true);
                break;
            default:
                Response.Redirect("/Event/MyEvent.aspx");
                break;
        }
    }

    protected void FIELD_Result_RowDataBound(object sender, GridViewRowEventArgs e)
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
                Button bt = (Button)e.Row.FindControl("Button_Delete");
                bt.Attributes.Add("onclick", "javascrip:return confirm('您確定要刪除此筆資料?')");
            }
        }
    }

    //GridView換頁
    protected void FIELD_Result_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        FIELD_Result.PageIndex = e.NewPageIndex;
        QueryData();
    }

    //讀取Event資料
    protected void LoadDate()
    {
        string eventid = Request.QueryString["id"];
        EventInfo ev = new EventInfo(eventid);
        Survey sv = new Survey();

        this.FIELD_Category.ForeColor = ColorTranslator.FromHtml(ev.EventCategoryColor);
        this.FIELD_Category.Text = "．" + ev.EventCategory;
        this.FIELD_EventName.Text = ev.EventName;
        this.FIELD_Count.Text = sv.GetSurveyFillinCount(eventid, ev.EventRegisterModel, ev.EventSurveyModel);
    }

    //查詢資料
    protected void QueryData()
    {
        EventInfo ev = new EventInfo(Request.QueryString["id"]);
        Survey sv = new Survey();
        DataTable dt = sv.GetSurvey(Request.QueryString["id"], ev.EventSurveyModel, this.FIELD_EmpName.Text.Trim());

        this.FIELD_Result.DataSource = dt;
        this.FIELD_Result.DataBind();
    }

    [WebMethod]
    public static string DeleteSurveyData(string id)
    {
        //刪除問卷資料
        //si[0]=Survey ID
        //si[1]=Survey Model
        string[] si = id.Split('_');
        string errormsg = "";
        HttpContext.Current.Response.Write(HttpContext.Current.Request.QueryString["id"] + "--eventid<BR>");
        HttpContext.Current.Response.Write(si[0] + "--surveyid<BR>");
        HttpContext.Current.Response.Write(si[1] + "--model<BR>");
        HttpContext.Current.Response.Write(HttpContext.Current.Session["EmpID"].ToString() + "--empid<BR>");
        Survey sv = new Survey();
        errormsg = sv.DeleteSurveyData(HttpContext.Current.Request.QueryString["id"], si[0], si[1], HttpContext.Current.Session["EmpID"].ToString());

        if (!string.IsNullOrEmpty(errormsg))
        {
            //失敗
            throw new Exception(errormsg);
        }

        return "Success";
    }

    protected void btnReloadSurveyData_Click(object sender, EventArgs e)
    {
        LoadDate();
        QueryData();
    }
}
