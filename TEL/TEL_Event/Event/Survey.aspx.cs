using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TEL.Event.Lab.Method;
using System.Data;
using System.Drawing;

public partial class Event_Survey : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadDate();
            QueryData();
        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        //登入檢查
        //若為一般使用者則導到Denied頁面
        TEL.Event.Lab.Method.System gm = new TEL.Event.Lab.Method.System();
        if (gm.IsDenied(Session["EmpID"].ToString(), Request.QueryString["eventid"]) < 1)
            Response.Redirect("/Event/Denied.aspx");
    }

    protected void Button_Query_Click(object sender, EventArgs e)
    {
        QueryData();
    }

    protected void Button_Delete_Click(object sender, EventArgs e)
    {
        //刪除問卷資料
        //si[0]=Survey ID
        //si[1]=Survey Model
        string[] si = ((Button)sender).CommandArgument.ToString().Split('_');
        Survey sv = new Survey();
        sv.DeleteSurveyData(si[0], si[1]);

        //重新Load Data & Query Data
        LoadDate();
        QueryData();
    }

    protected void Button_ExportExcel_Click(object sender, EventArgs e)
    {
        string eventid = Request.QueryString["eventid"];
        EventInfo ev = new EventInfo(eventid);
        ExportExcel ep = new ExportExcel();

        if (ev.EventSurveyModel == "1")
            ep.ExportSurveyModel1(Request.QueryString["eventid"]);
        else if (ev.EventSurveyModel == "2")
            ep.ExportSurveyModel2(Request.QueryString["eventid"]);
        else if (ev.EventSurveyModel == "3")
            ep.ExportSurveyModel3(Request.QueryString["eventid"]);
        else if (ev.EventSurveyModel == "4")
            ep.ExportSurveyModel4(Request.QueryString["eventid"]);
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
        string eventid = Request.QueryString["eventid"];
        EventInfo ev = new EventInfo(eventid);
        Survey sv = new Survey();

        this.TD_Category.Style["background"] = ev.EventCategoryColor;
        this.FIELD_Category.Text = ev.EventCategory;
        this.FIELD_EventName.Text = ev.EventName;
        this.FIELD_Count.Text = sv.GetSurveyFillinCount(eventid, ev.EventRegisterModel, ev.EventSurveyModel);
    }

    //查詢資料
    protected void QueryData()
    {
        EventInfo ev = new EventInfo(Request.QueryString["eventid"]);
        Survey sv = new Survey();
        DataTable dt = sv.GetSurvey(Request.QueryString["eventid"], ev.EventSurveyModel, this.FIELD_EmpName.Text.Trim());

        this.FIELD_Result.DataSource = dt;
        this.FIELD_Result.DataBind();
    }
}
