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

public partial class Event_SurveyModel4_Create : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["id"] == null || string.IsNullOrEmpty(Request.QueryString["id"]))
            Response.Redirect("/Event/MyEvent.aspx");

        UC_EventDescription.setViewDefault(Request.QueryString["id"]);

        Load_EmpData();
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

        if (!this.FIELD_Q1_1.Checked && !this.FIELD_Q1_2.Checked && !this.FIELD_Q1_3.Checked && !this.FIELD_Q1_4.Checked && !this.FIELD_Q1_5.Checked)
        {
            if (!string.IsNullOrEmpty(ErrorMsg))
                  ErrorMsg += "<BR>";
            ErrorMsg += this.GetLocalResourceObject("ErrorMsg_MustFill_Q1").ToString();
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
        string q2 = "";
        string q3 = "";
        string q4 = "";
        string q5 = "";

        #region Q1

        for (int i = 1; i <= 5; i++)
        {
            RadioButton rb = (RadioButton)this.Master.FindControl("ContentPlaceHolder1").FindControl("FIELD_Q1_" + i);

            if (rb.Checked)
                q1 = rb.Text;
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

        errormsg = sv.SaveSurveyDataMModel4(this.Request.QueryString["id"], Page.Session["EmpID"].ToString(), q1, q2, q3, q4, this.FIELD_Q5.Text.Trim());

        if (errormsg != "")
        {
            flag = false;
            MessageBox.Show(errormsg);
        }

        return flag;
    }
}
