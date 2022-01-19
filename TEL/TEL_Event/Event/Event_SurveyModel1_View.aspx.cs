using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TEL.Event.Lab.Method;
using System.Data;
using RadioButton = System.Web.UI.WebControls.RadioButton;
using CheckBox = System.Web.UI.WebControls.CheckBox;

public partial class Event_Event_SurveyModel1_View : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Load_EmpData();
        Load_SurveyData();
    }

    //Load員工相關資料
    protected void Load_EmpData()
    {
        UserInfo ui = new UserInfo(Page.Session["EmpID"].ToString());
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

        DataTable WMTB = sv.GetSurveyData(this.Request.QueryString["id"], "1");

        if (WMTB.Rows.Count > 0)
        {
            for (int i = 1; i <= 6; i++)
            {
                CheckBox cb = (CheckBox)this.Page.FindControl("FIELD_Q1_" + i);

                if (WMTB.Rows[0]["q1"].ToString().IndexOf(cb.Text) > -1)
                    cb.Checked = true;
            }
        }

        this.FIELD_Q1Other.Text = WMTB.Rows[0]["q1other"].ToString();

        for (int i = 1; i <= 5; i++)
        {
            RadioButton rb = (RadioButton)this.Page.FindControl("FIELD_Q2_" + i);

            if (WMTB.Rows[0]["q2"].ToString()==rb.Text)
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

        for (int i = 1; i <= 5; i++)
        {
            RadioButton rb = (RadioButton)this.Page.FindControl("FIELD_Q5_" + i);

            if (WMTB.Rows[0]["q5"].ToString() == rb.Text)
                rb.Checked = true;
        }

        for (int i = 1; i <= 5; i++)
        {
            RadioButton rb = (RadioButton)this.Page.FindControl("FIELD_Q6_" + i);

            if (WMTB.Rows[0]["q6"].ToString() == rb.Text)
                rb.Checked = true;
        }

        for (int i = 1; i <= 2; i++)
        {
            RadioButton rb = (RadioButton)this.Page.FindControl("FIELD_Q7_" + i);

            if (WMTB.Rows[0]["q7"].ToString() == rb.Text)
                rb.Checked = true;
        }

        this.FIELD_Q7Reason.Text = WMTB.Rows[0]["q7reason"].ToString();
        this.FIELD_Q8.Text = WMTB.Rows[0]["q8"].ToString();
        this.FIELD_Q9.Text = WMTB.Rows[0]["q9"].ToString();
        this.FIELD_Q10.Text = WMTB.Rows[0]["q10"].ToString();
    }
}