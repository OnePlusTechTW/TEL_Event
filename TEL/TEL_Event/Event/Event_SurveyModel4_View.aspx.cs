﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TEL.Event.Lab.Method;
using System.Data;
using RadioButton = System.Web.UI.WebControls.RadioButton;

public partial class Event_SurveyModel4_View : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            Load_SurveyData();
    }

    //Load員工相關資料
    protected void Load_EmpData(string empid)
    {
        UserInfo ui = new UserInfo(empid);
        this.FIELD_Empid.Text = ui.EmpID;
        this.FIELD_EmpNameCH.Text = ui.FullNameCH;
        this.FIELD_EmpNameEN.Text = ui.FullNameEN;
        this.FIELD_UnitName.Text = ui.UnitName;
        this.FIELD_Station.Text = ui.Station;
    }

    //Load問卷資料
    protected void Load_SurveyData()
    {
        Survey sv = new Survey();

        DataTable WMTB = sv.GetSurveyData(this.Request.QueryString["id"], "4");

        if (WMTB.Rows.Count > 0)
        {
            UC_EventDescription.setViewDefault(WMTB.Rows[0]["eventid"].ToString());
            Load_EmpData(WMTB.Rows[0]["empid"].ToString());

            for (int i = 1; i <= 5; i++)
            {
                RadioButton rb = (RadioButton)this.Page.FindControl("FIELD_Q2_" + i);

                if (WMTB.Rows[0]["q2"].ToString() == rb.Text)
                    rb.Checked = true;
            }

            for (int i = 1; i <= 5; i++)
            {
                RadioButton rb = (RadioButton)this.Page.FindControl("FIELD_Q3_" + i);

                if (WMTB.Rows[0]["q3"].ToString() == rb.Text)
                    rb.Checked = true;
            }

            for (int i = 1; i <= 5; i++)
            {
                RadioButton rb = (RadioButton)this.Page.FindControl("FIELD_Q4_" + i);

                if (WMTB.Rows[0]["q4"].ToString() == rb.Text)
                    rb.Checked = true;
            }

            this.FIELD_Q5.Text = WMTB.Rows[0]["q5"].ToString();
        }
    }
}