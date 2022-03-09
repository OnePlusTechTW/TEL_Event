using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TEL.Event.Lab.Method;
using System.Data;
using RadioButton = System.Web.UI.WebControls.RadioButton;

public partial class Event_SurveyModel3_View : System.Web.UI.Page
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
        this.FIELD_UnitName.Text = ui.UnitCode+" - "+ui.UnitName;
        this.FIELD_Station.Text = ui.Station;
    }

    //Load問卷資料
    protected void Load_SurveyData()
    {
        Survey sv = new Survey();

        DataTable WMTB = sv.GetSurveyData(this.Request.QueryString["id"], "3");
        
        if (WMTB.Rows.Count > 0)
        {
            UC_EventDescription.setViewDefault(WMTB.Rows[0]["eventid"].ToString());
            Load_EmpData(WMTB.Rows[0]["empid"].ToString());

            this.FIELD_Q1.Text = WMTB.Rows[0]["q1"].ToString();

            for (int i = 1; i <= 2; i++)
            {
                RadioButton rb = (RadioButton)this.Page.FindControl("FIELD_Q2_" + i);

                if (WMTB.Rows[0]["q2"].ToString() == rb.Text)
                    rb.Checked = true;
            }
            this.FIELD_Q2Reason.Text = WMTB.Rows[0]["q2reason"].ToString();

            for (int i = 1; i <= 3; i++)
            {
                RadioButton rb = (RadioButton)this.Page.FindControl("FIELD_Q3_" + i);

                if (WMTB.Rows[0]["q3"].ToString() == rb.Text)
                    rb.Checked = true;
            }
            this.FIELD_Q3Reason.Text = WMTB.Rows[0]["q3reason"].ToString();

            for (int i = 1; i <= 3; i++)
            {
                RadioButton rb = (RadioButton)this.Page.FindControl("FIELD_Q4_" + i);

                if (WMTB.Rows[0]["q4"].ToString() == rb.Text)
                    rb.Checked = true;
            }
            this.FIELD_Q4Reason.Text = WMTB.Rows[0]["q4reason"].ToString();

            for (int i = 1; i <= 3; i++)
            {
                RadioButton rb = (RadioButton)this.Page.FindControl("FIELD_Q5_" + i);

                if (WMTB.Rows[0]["q5"].ToString() == rb.Text)
                    rb.Checked = true;
            }
            this.FIELD_Q5Reason.Text = WMTB.Rows[0]["q5reason"].ToString();

            for (int i = 1; i <= 3; i++)
            {
                RadioButton rb = (RadioButton)this.Page.FindControl("FIELD_Q6_" + i);

                if (WMTB.Rows[0]["q6"].ToString() == rb.Text)
                    rb.Checked = true;
            }
            this.FIELD_Q6Reason.Text = WMTB.Rows[0]["q6reason"].ToString();

            for (int i = 1; i <= 3; i++)
            {
                RadioButton rb = (RadioButton)this.Page.FindControl("FIELD_Q7_" + i);

                if (WMTB.Rows[0]["q7"].ToString() == rb.Text)
                    rb.Checked = true;
            }
            this.FIELD_Q7Reason.Text = WMTB.Rows[0]["q7reason"].ToString();

            this.FIELD_Q8.Text = WMTB.Rows[0]["q8"].ToString();

            for (int i = 1; i <= 2; i++)
            {
                RadioButton rb = (RadioButton)this.Page.FindControl("FIELD_Q9_" + i);

                if (WMTB.Rows[0]["q9"].ToString() == rb.Text)
                    rb.Checked = true;
            }
        }
    }
}