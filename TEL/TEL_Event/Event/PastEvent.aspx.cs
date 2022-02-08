using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TEL.Event.Lab.Method;

public partial class Event_PastEvent : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GeneratedCategoryItem();
            SetEventsGrid();
        }
    }
    private void SetEventsGrid()
    {
        Event ev = new Event();
        DataTable dt = ev.GetEventInfo(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "F", "Y");

        this.gridEvents.DataSource = dt;
        this.gridEvents.DataBind();
    }

    // 取得活動分類選項
    protected void GeneratedCategoryItem()
    {
        this.ddlEventCategory.Items.Clear();

        ListItem li = new ListItem();
        li.Text = item_all.Text;
        li.Value = "";

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

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string aa = sDate.Text;
        Event ev = new Event();
        DataTable dt = ev.GetEventInfo(string.Empty, tbEventName.Text, ddlEventCategory.SelectedValue, sDate.Text, eDate.Text, "F", "Y");

        this.gridEvents.DataSource = dt;
        this.gridEvents.DataBind();
    }

    protected void gridEvent_RowDataBound(object sender, GridViewRowEventArgs e)
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

    protected void gridEvent_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gridEvents.PageIndex = e.NewPageIndex;
        SetEventsGrid();
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        string id = btn.CommandArgument.ToString();

        this.UC_EventDescription.setViewDefault(id);
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Guid.NewGuid().ToString(), "ShowDialogView();", true);
    }
}