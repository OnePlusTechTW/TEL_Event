using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TEL.Event.Lab.Method;
using System.Data;
using System.Windows.Forms;
using RadioButton = System.Web.UI.WebControls.RadioButton;
using CheckBox = System.Web.UI.WebControls.CheckBox;

public partial class Event_SurveyModel1_Create : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["id"] == null || string.IsNullOrEmpty(Request.QueryString["id"]))
            Response.Redirect("/Event/MyEvent.aspx");

        Load_EmpData();
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (this.FIELD_Q7_2.Checked)
            this.FIELD_Q7Reason.Enabled = true;
        else
            this.FIELD_Q7Reason.Enabled = false;
    }

    protected void Button_Cancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("/Event/MyEvent.aspx");
    }

    protected void Button_Submit_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Guid.NewGuid().ToString(), "ShowDialogConfirm();", true);
    }

    protected void Button_AddConfirm_Click(object sender, EventArgs e)
    {
        if (Check_Input())
            if (Save_Data())
                Response.Redirect("/Event/MyEvent.aspx");
    }

    //Load員工相關資料
    protected void Load_EmpData()
    {
        UserInfo ui = new UserInfo(Page.Session["EmpID"].ToString());
        this.FIELD_Empid.Text = ui.EmpID;
        this.FIELD_EmpNameCH.Text = ui.FullNameCH;
        this.FIELD_EmpNameEN.Text = ui.FullNameEN;
        this.FIELD_UnitName.Text = ui.UnitCode + " - " + ui.UnitName;
        this.FIELD_Station.Text = ui.Station;
    }

    //儲存前必填檢查
    protected bool Check_Input()
    {
        bool flag = true;

        string ErrorMsg = "";

        if (!this.FIELD_Q1_1.Checked && !this.FIELD_Q1_2.Checked && !this.FIELD_Q1_3.Checked && !this.FIELD_Q1_4.Checked && !this.FIELD_Q1_5.Checked && !this.FIELD_Q1_6.Checked)
        {
            if (!string.IsNullOrEmpty(ErrorMsg))
                 ErrorMsg += "<BR>";
            ErrorMsg += this.GetLocalResourceObject("ErrorMsg_MustFill_Q1").ToString();
        }
        else if (this.FIELD_Q1_6.Checked && string.IsNullOrEmpty(this.FIELD_Q1Other.Text.Trim()))
        {
            if (!string.IsNullOrEmpty(ErrorMsg))
                  ErrorMsg += "<BR>";
            ErrorMsg += this.GetLocalResourceObject("ErrorMsg_MustFill_Q1Other").ToString();
        }

        if (!this.FIELD_Q2_1.Checked && !this.FIELD_Q2_2.Checked && !this.FIELD_Q2_3.Checked && !this.FIELD_Q2_4.Checked && !this.FIELD_Q2_5.Checked)
        {
            if (!string.IsNullOrEmpty(ErrorMsg))
                  ErrorMsg += "<BR>";
            ErrorMsg += this.GetLocalResourceObject("ErrorMsg_MustFill_Q2").ToString();
        }

        if (!this.FIELD_Q3_1.Checked && !this.FIELD_Q3_2.Checked && !this.FIELD_Q3_3.Checked && !this.FIELD_Q3_4.Checked && !this.FIELD_Q3_5.Checked)
        {
            if (!string.IsNullOrEmpty(ErrorMsg))
                  ErrorMsg += "<BR>";
            ErrorMsg += this.GetLocalResourceObject("ErrorMsg_MustFill_Q3").ToString();
        }

        if (!this.FIELD_Q4_1.Checked && !this.FIELD_Q4_2.Checked && !this.FIELD_Q4_3.Checked && !this.FIELD_Q4_4.Checked && !this.FIELD_Q4_5.Checked)
        {
            if (!string.IsNullOrEmpty(ErrorMsg))
                  ErrorMsg += "<BR>";
            ErrorMsg += this.GetLocalResourceObject("ErrorMsg_MustFill_Q4").ToString();
        }

        if (!this.FIELD_Q5_1.Checked && !this.FIELD_Q5_2.Checked && !this.FIELD_Q5_3.Checked && !this.FIELD_Q5_4.Checked && !this.FIELD_Q5_5.Checked)
        {
            if (!string.IsNullOrEmpty(ErrorMsg))
                 ErrorMsg += "<BR>";
            ErrorMsg += this.GetLocalResourceObject("ErrorMsg_MustFill_Q5").ToString();
        }

        if (!this.FIELD_Q6_1.Checked && !this.FIELD_Q6_2.Checked && !this.FIELD_Q6_3.Checked && !this.FIELD_Q6_4.Checked && !this.FIELD_Q6_5.Checked)
        {
            if (!string.IsNullOrEmpty(ErrorMsg))
                  ErrorMsg += "<BR>";
            ErrorMsg += this.GetLocalResourceObject("ErrorMsg_MustFill_Q6").ToString();
        }

        if (!this.FIELD_Q7_1.Checked && !this.FIELD_Q7_2.Checked)
        {
            if (!string.IsNullOrEmpty(ErrorMsg))
                  ErrorMsg += "<BR>";
            ErrorMsg += this.GetLocalResourceObject("ErrorMsg_MustFill_Q7").ToString();
        }
        else if (this.FIELD_Q7_2.Checked && string.IsNullOrEmpty(this.FIELD_Q7Reason.Text.Trim()))
        {
            if (!string.IsNullOrEmpty(ErrorMsg))
                  ErrorMsg += "<BR>";
            ErrorMsg += this.GetLocalResourceObject("ErrorMsg_MustFill_Q7Reason").ToString();
        }
        if (string.IsNullOrEmpty(this.FIELD_Q8.Text.Trim()))
        {
            if (!string.IsNullOrEmpty(ErrorMsg))
                  ErrorMsg += "<BR>";
            ErrorMsg += this.GetLocalResourceObject("ErrorMsg_MustFill_Q8").ToString();
        }

        if (!string.IsNullOrEmpty(ErrorMsg))
        {
            lblFiledName.Text = ErrorMsg;
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Guid.NewGuid().ToString(), "ShowDialogRequired();", true);
            flag = false;
        }

        return flag;
    }

    //儲存問卷資料
    protected bool Save_Data()
    {
        bool flag = true;
        string errormsg = "";
        Survey sv = new Survey();

        string q1 = "";
        string q1other = "";
        string q2 = "";
        string q3 = "";
        string q4 = "";
        string q5 = "";
        string q6 = "";
        string q7 = "";
        string q7reason = "";

        #region Q1

        for (int i = 1; i <= 5; i++)
        {
            CheckBox cb = (CheckBox)this.Master.FindControl("ContentPlaceHolder1").FindControl("FIELD_Q1_" + i);

            if (cb.Checked)
            {
                if (!string.IsNullOrEmpty(q1))
                    q1 += ",";
                q1 += cb.Text;
            }
        }

        if (this.FIELD_Q1_6.Checked)
        {
            if (!string.IsNullOrEmpty(q1))
                q1 += ",";
            q1 += this.FIELD_Q1_6.Text + "(" + this.FIELD_Q1Other.Text.Trim() + ")";
            q1other = this.FIELD_Q1Other.Text.Trim();
        }

        #endregion

        #region Q2

        for (int i = 1; i <= 5; i++)
        {
            RadioButton rb = (RadioButton)this.Master.FindControl("ContentPlaceHolder1").FindControl("FIELD_Q2_" + i);

            if (rb.Checked)
                q2 = rb.Text;
        }

        #endregion

        #region Q3

        for (int i = 1; i <= 5; i++)
        {
            RadioButton rb = (RadioButton)this.Master.FindControl("ContentPlaceHolder1").FindControl("FIELD_Q3_" + i);

            if (rb.Checked)
                q3 = rb.Text;
        }

        #endregion

        #region Q4

        for (int i = 1; i <= 5; i++)
        {
            RadioButton rb = (RadioButton)this.Master.FindControl("ContentPlaceHolder1").FindControl("FIELD_Q4_" + i);

            if (rb.Checked)
                q4 = rb.Text;
        }

        #endregion

        #region Q5

        for (int i = 1; i <= 5; i++)
        {
            RadioButton rb = (RadioButton)this.Master.FindControl("ContentPlaceHolder1").FindControl("FIELD_Q5_" + i);

            if (rb.Checked)
                q5 = rb.Text;
        }

        #endregion

        #region Q6

        for (int i = 1; i <= 5; i++)
        {
            RadioButton rb = (RadioButton)this.Master.FindControl("ContentPlaceHolder1").FindControl("FIELD_Q6_" + i);

            if (rb.Checked)
                q6 = rb.Text;
        }

        #endregion

        #region Q7

        if (this.FIELD_Q7_1.Checked)
        {
            q7 = FIELD_Q7_1.Text;
        }
        else
        {
            q7 = FIELD_Q7_2.Text;
            q7reason = this.FIELD_Q7Reason.Text.Trim();
        }

        #endregion

        errormsg = sv.SaveSurveyDataMModel1(this.Request.QueryString["id"], Page.Session["EmpID"].ToString(), q1, q1other, q2, q3, q4, q5, q6, q7, q7reason, this.FIELD_Q8.Text.Trim(), this.FIELD_Q9.Text.Trim(), this.FIELD_Q10.Text.Trim());

        if (!string.IsNullOrEmpty(errormsg))
        {
            flag = false;
            MessageBox.Show(errormsg);
        }

        return flag;
    }

}
