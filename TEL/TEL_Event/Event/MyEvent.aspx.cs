using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using TEL.Event.Lab.Method;
using System.Drawing;

public partial class Event_MyEvent : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GeneratedCategoryItem();
            QueryData();
        }
    }

    protected void Button_Query_Click(object sender, EventArgs e)
    {
        QueryData();
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

            //報名資料Button控制
            string registerend = DataBinder.Eval(e.Row.DataItem, "registerend").ToString();
            string registermodel = DataBinder.Eval(e.Row.DataItem, "registermodel").ToString();
            string registerid = DataBinder.Eval(e.Row.DataItem, "registerid").ToString();

            if (DateTime.Parse(registerend) < DateTime.Now)
            {
                Button bt = (Button)e.Row.FindControl("Button_RegisterEdit");
                bt.Visible = false;

                Button bt1 = (Button)e.Row.FindControl("Button_RegisterView");
                bt1.OnClientClick = "window.ShowModalDialog('/Event/Event_RegisterModel" + registermodel + "_View.aspx?id=" + registerid + "'); event.returnValue=false;";
            }
            else
            {
                Button bt = (Button)e.Row.FindControl("Button_RegisterView");
                bt.Visible = false;
            }

            //問卷資料Button控制
            string surveystartdate = DataBinder.Eval(e.Row.DataItem, "surveystartdate").ToString();
            string surveyid = DataBinder.Eval(e.Row.DataItem, "surveyid").ToString();

            if (!string.IsNullOrEmpty(surveystartdate))
            {
                //已發送問卷
                if (!string.IsNullOrEmpty(surveyid))
                {
                    //已填寫問卷，隱藏檢視按鍵
                    Button bt = (Button)e.Row.FindControl("Button_SurveyCreate");
                    bt.Visible = false;
                }
                else
                {
                    //尚未填寫問卷，隱藏填寫按鍵
                    Button bt = (Button)e.Row.FindControl("Button_SurveyView");
                    bt.Visible = false;
                }
            }
            else
            {
                //尚未發送問卷
                //隱藏填寫跟檢視按鍵
                Button bt1 = (Button)e.Row.FindControl("Button_SurveyCreate");
                bt1.Visible = false;

                Button bt2 = (Button)e.Row.FindControl("Button_SurveyView");
                bt2.Visible = false;
            }
        }
    }

    //GridView換頁
    protected void FIELD_Result_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        FIELD_Result.PageIndex = e.NewPageIndex;
        QueryData();
    }

    //報名資料編輯按鍵導頁
    protected void Button_RegisterEdit_Click(object sender, EventArgs e)
    {
        //si[0]=活動ID
        //si[1]=Survey Model
        //si[2]=報名ID
        string[] si = ((Button)sender).CommandArgument.ToString().Split('_');

        switch (si[1])
        {
            case "1":
                Response.Redirect("/Event/Event_RegisterModel1_Edit.aspx?id=" + si[2]);
                break;
            case "2":
                Response.Redirect("/Event/Event_RegisterModel2_Edit.aspx?id=" + si[2]);
                break;
            case "3":
                Response.Redirect("/Event/Event_RegisterModel3_Edit.aspx?id=" + si[2]);
                break;
            case "4":
                Response.Redirect("/Event/Event_RegisterModel4_Edit.aspx?id=" + si[2]);
                break;
            case "5":
                Response.Redirect("/Event/Event_RegisterModel5_Edit.aspx?id=" + si[2]);
                break;
            case "6":
                Response.Redirect("/Event/Event_RegisterModel6_Edit.aspx?id=" + si[2]);
                break;
            default:
                Response.Redirect("/Event/MyEvent.aspx");
                break;
        }
    }

    //問卷資料填寫按鍵導頁
    protected void Button_SurveyCreate_Click(object sender, EventArgs e)
    {
        //si[0]=活動ID
        //si[1]=Survey Model
        string[] si = ((Button)sender).CommandArgument.ToString().Split('_');

        switch (si[1])
        {
            case "1":
                Response.Redirect("/Event/Event_SurveyModel1_Create.aspx?id=" + si[0]);
                break;
            case "2":
                Response.Redirect("/Event/Event_SurveyModel2_Create.aspx?id=" + si[0]);
                break;
            case "3":
                Response.Redirect("/Event/Event_SurveyModel3_Create.aspx?id=" + si[0]);
                break;
            case "4":
                Response.Redirect("/Event/Event_SurveyModel4_Create.aspx?id=" + si[0]);
                break;
            default:
                Response.Redirect("/Event/MyEvent.aspx");
                break;
        }
    }


    // 取得活動分類選項
    protected void GeneratedCategoryItem()
    {
        this.FIELD_EventCategory.Items.Clear();

        ListItem li = new ListItem();
        li.Text = this.GetLocalResourceObject("item_all").ToString();
        li.Value = "";

        this.FIELD_EventCategory.Items.Add(li);

        SystemInfo si = new SystemInfo();
        DataTable dt = si.GetEventCategory("All");

        foreach (DataRow rs in dt.Rows)
        {
            ListItem li1 = new ListItem();
            li1.Text = rs["name"].ToString();
            li1.Value = rs["id"].ToString();

            this.FIELD_EventCategory.Items.Add(li1);
        }
    }

    //查詢資料
    protected void QueryData()
    {
        Event ev = new Event();
        DataTable dt = ev.GetMyEvent(this.FIELD_EventName.Text.Trim(), this.FIELD_EventCategory.SelectedValue, this.FIELD_EventStatus.SelectedValue, Page.Session["EmpID"].ToString());

        this.FIELD_Result.DataSource = dt;
        this.FIELD_Result.DataBind();
    }
}