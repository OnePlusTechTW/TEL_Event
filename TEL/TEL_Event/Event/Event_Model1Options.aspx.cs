﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using TEL.Event.Lab.Method;

public partial class Event_Event_Model1Options : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            SetDefaultGridView();
        }
    }

    private void SetDefaultGridView()
    {
        DataTable dt = new DataTable();
        Event ev = new Event();
        string eventid = string.Empty;
        eventid = Request.QueryString["id"];
        dt = ev.GetRegisterOption1(eventid);
        this.gridModel1Options.DataSource = dt;
        this.gridModel1Options.DataBind();
    }

    protected void gridModel1Options_RowDataBound(object sender, GridViewRowEventArgs e)
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

    protected void gridModel1Options_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gridModel1Options.PageIndex = e.NewPageIndex;
        SetDefaultGridView();
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        string content = txtContent.Text;
        string limit = txtLimit.Text;

        string errFields = string.Empty;

        if (string.IsNullOrEmpty(content) && string.IsNullOrEmpty(limit))
        {
            errFields = $"{lblContent.Text}、{lblLimit.Text}";
        }
        else if (string.IsNullOrEmpty(content))
        {
            errFields = lblContent.Text;

        }
        else if (string.IsNullOrEmpty(limit))
        {
            errFields = lblLimit.Text;

        }

        if (!string.IsNullOrEmpty(errFields))
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Guid.NewGuid().ToString(), "ShowDialogRequired('Category','" + errFields + "');", true);
        }
        else
        {
            string eventid = string.Empty;
            eventid = Request.QueryString["id"];
            Event ev = new Event();
            if (!string.IsNullOrEmpty(eventid))
            {
                string result = ev.AddRegisterOption1(eventid, content, limit, Page.Session["EmpID"].ToString());

                if (string.IsNullOrEmpty(result))
                {
                    SetDefaultGridView();
                }
                else
                {
                    //新增失敗
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Guid.NewGuid().ToString(), "ShowDialogFailed();", true);
                }
            }
        }
    }

    protected void btnReloadGridView_Click(object sender, EventArgs e)
    {
        SetDefaultGridView();

    }

    protected void btnfinish_Click(object sender, EventArgs e)
    {
        Response.Redirect("Event.aspx");
    }

    /// <summary>
    /// 刪除欲參加內容
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [WebMethod]
    public static string DeleteRegisterOption1(string id)
    {
        Event ev = new Event();
        string result = ev.DeleteRegisterOption1(id);

        if (!string.IsNullOrEmpty(result))
        {
            //失敗
            throw new Exception("Failed");
        }

        return "SuccessCategory";
    }

    
}