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

public partial class Event_SurveyModel3_Create : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["id"] == null || string.IsNullOrEmpty(Request.QueryString["id"]))
            Response.Redirect("/Event/MyEvent.aspx");

        if (!IsPostBack)
            GeneratedHosipitalItem();

        Load_EmpData();
        this.Button_Submit.Attributes.Add("onclick", "javascrip:return confirm('問卷送出後即不可修正，您確定要送出嗎？')");  
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (this.FIELD_Q2_1.Checked)
            this.FIELD_Q2Reason.Enabled = true;
        else
            this.FIELD_Q2Reason.Enabled = false;

        if (this.FIELD_Q3_3.Checked)
            this.FIELD_Q3Reason.Enabled = true;
        else
            this.FIELD_Q3Reason.Enabled = false;

        if (this.FIELD_Q4_3.Checked)
            this.FIELD_Q4Reason.Enabled = true;
        else
            this.FIELD_Q4Reason.Enabled = false;

        if (this.FIELD_Q5_3.Checked)
            this.FIELD_Q5Reason.Enabled = true;
        else
            this.FIELD_Q5Reason.Enabled = false;

        if (this.FIELD_Q6_3.Checked)
            this.FIELD_Q6Reason.Enabled = true;
        else
            this.FIELD_Q6Reason.Enabled = false;

        if (this.FIELD_Q7_3.Checked)
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

    //Load健檢中心資料
    protected void GeneratedHosipitalItem()
    {
        this.FIELD_Q1.Items.Clear();

        ListItem li = new ListItem();
        li.Text = this.GetLocalResourceObject("FIELD_Q1item_none").ToString();
        li.Value = "";

        this.FIELD_Q1.Items.Add(li);

        Event ev = new Event();
        DataTable dt = ev.GetHosipital(this.Request.QueryString["id"]);

        foreach (DataRow rs in dt.Rows)
        {
            ListItem li1 = new ListItem();
            li1.Text = rs["hosipital"].ToString();
            li1.Value = rs["hosipital"].ToString();

            this.FIELD_Q1.Items.Add(li1);
        }
    }

    //儲存前必填檢查
    protected bool Check_Input()
    {
        bool flag = true;

        string ErrorMsg = "";

        if (string.IsNullOrEmpty(this.FIELD_Q1.SelectedValue))
        {
            if (!string.IsNullOrEmpty(ErrorMsg))
                ErrorMsg += "\n";
            ErrorMsg += this.GetLocalResourceObject("ErrorMsg_MustFill_Q1").ToString();
        }

        if (!this.FIELD_Q2_1.Checked && !this.FIELD_Q2_2.Checked)
        {
            if (!string.IsNullOrEmpty(ErrorMsg))
                ErrorMsg += "\n";
            ErrorMsg += this.GetLocalResourceObject("ErrorMsg_MustFill_Q2").ToString();
        }
        else if (this.FIELD_Q2_1.Checked && string.IsNullOrEmpty(this.FIELD_Q2Reason.Text.Trim()))
        {
            if (!string.IsNullOrEmpty(ErrorMsg))
                ErrorMsg += "\n";
            ErrorMsg += this.GetLocalResourceObject("ErrorMsg_MustFill_Q2Reason").ToString();
        }

        if (!this.FIELD_Q3_1.Checked && !this.FIELD_Q3_2.Checked && !this.FIELD_Q3_3.Checked)
        {
            if (!string.IsNullOrEmpty(ErrorMsg))
                ErrorMsg += "\n";
            ErrorMsg += this.GetLocalResourceObject("ErrorMsg_MustFill_Q3").ToString();
        }
        else if (this.FIELD_Q3_3.Checked && string.IsNullOrEmpty(this.FIELD_Q3Reason.Text.Trim()))
        {
            if (!string.IsNullOrEmpty(ErrorMsg))
                ErrorMsg += "\n";
            ErrorMsg += this.GetLocalResourceObject("ErrorMsg_MustFill_Q3Reason").ToString();
        }

        if (!this.FIELD_Q4_1.Checked && !this.FIELD_Q4_2.Checked && !this.FIELD_Q4_3.Checked)
        {
            if (!string.IsNullOrEmpty(ErrorMsg))
                ErrorMsg += "\n";
            ErrorMsg += this.GetLocalResourceObject("ErrorMsg_MustFill_Q4").ToString();
        }
        else if (this.FIELD_Q4_3.Checked && string.IsNullOrEmpty(this.FIELD_Q4Reason.Text.Trim()))
        {
            if (!string.IsNullOrEmpty(ErrorMsg))
                ErrorMsg += "\n";
            ErrorMsg += this.GetLocalResourceObject("ErrorMsg_MustFill_Q4Reason").ToString();
        }

        if (!this.FIELD_Q5_1.Checked && !this.FIELD_Q5_2.Checked && !this.FIELD_Q5_3.Checked)
        {
            if (!string.IsNullOrEmpty(ErrorMsg))
                ErrorMsg += "\n";
            ErrorMsg += this.GetLocalResourceObject("ErrorMsg_MustFill_Q5").ToString();
        }
        else if (this.FIELD_Q5_3.Checked && string.IsNullOrEmpty(this.FIELD_Q5Reason.Text.Trim()))
        {
            if (!string.IsNullOrEmpty(ErrorMsg))
                ErrorMsg += "\n";
            ErrorMsg += this.GetLocalResourceObject("ErrorMsg_MustFill_Q5Reason").ToString();
        }

        if (!this.FIELD_Q6_1.Checked && !this.FIELD_Q6_2.Checked && !this.FIELD_Q6_3.Checked)
        {
            if (!string.IsNullOrEmpty(ErrorMsg))
                ErrorMsg += "\n";
            ErrorMsg += this.GetLocalResourceObject("ErrorMsg_MustFill_Q6").ToString();
        }
        else if (this.FIELD_Q6_3.Checked && string.IsNullOrEmpty(this.FIELD_Q6Reason.Text.Trim()))
        {
            if (!string.IsNullOrEmpty(ErrorMsg))
                ErrorMsg += "\n";
            ErrorMsg += this.GetLocalResourceObject("ErrorMsg_MustFill_Q6Reason").ToString();
        }

        if (!this.FIELD_Q7_1.Checked && !this.FIELD_Q7_2.Checked && !this.FIELD_Q7_3.Checked)
        {
            if (!string.IsNullOrEmpty(ErrorMsg))
                ErrorMsg += "\n";
            ErrorMsg += this.GetLocalResourceObject("ErrorMsg_MustFill_Q7").ToString();
        }
        else if (this.FIELD_Q7_3.Checked && string.IsNullOrEmpty(this.FIELD_Q7Reason.Text.Trim()))
        {
            if (!string.IsNullOrEmpty(ErrorMsg))
                ErrorMsg += "\n";
            ErrorMsg += this.GetLocalResourceObject("ErrorMsg_MustFill_Q7Reason").ToString();
        }

        if (string.IsNullOrEmpty(this.FIELD_Q8.Text.Trim()))
        {
            if (!string.IsNullOrEmpty(ErrorMsg))
                ErrorMsg += "\n";
            ErrorMsg += this.GetLocalResourceObject("ErrorMsg_MustFill_Q8").ToString();
        }

        if (!this.FIELD_Q9_1.Checked && !this.FIELD_Q9_2.Checked)
        {
            if (!string.IsNullOrEmpty(ErrorMsg))
                ErrorMsg += "\n";
            ErrorMsg += this.GetLocalResourceObject("ErrorMsg_MustFill_Q9").ToString();
        }

        if (!string.IsNullOrEmpty(ErrorMsg))
        {
            MessageBox.Show(ErrorMsg, "錯誤訊息");
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

        string q2 = "";
        string q2reason = "";
        string q3 = "";
        string q3reason = "";
        string q4 = "";
        string q4reason = "";
        string q5 = "";
        string q5reason = "";
        string q6 = "";
        string q6reason = "";
        string q7 = "";
        string q7reason = "";
        string q9 = "";

        #region Q2

        for (int i = 1; i <= 2; i++)
        {
            RadioButton rb = (RadioButton)this.Master.FindControl("ContentPlaceHolder1").FindControl("FIELD_Q2_" + i);

            if (rb.Checked)
                q2 = rb.Text;
        }

        if (this.FIELD_Q2_1.Checked)
        {
            q2reason = this.FIELD_Q2Reason.Text.Trim();
        }

        #endregion

        #region Q3

        for (int i = 1; i <= 3; i++)
        {
            RadioButton rb = (RadioButton)this.Master.FindControl("ContentPlaceHolder1").FindControl("FIELD_Q3_" + i);

            if (rb.Checked)
                q3 = rb.Text;
        }

        if (this.FIELD_Q3_3.Checked)
        {
            q3reason = this.FIELD_Q3Reason.Text.Trim();
        }

        #endregion

        #region Q4

        for (int i = 1; i <= 3; i++)
        {
            RadioButton rb = (RadioButton)this.Master.FindControl("ContentPlaceHolder1").FindControl("FIELD_Q4_" + i);

            if (rb.Checked)
                q4 = rb.Text;
        }

        if (this.FIELD_Q4_3.Checked)
        {
            q4reason = this.FIELD_Q4Reason.Text.Trim();
        }

        #endregion

        #region Q5

        for (int i = 1; i <= 3; i++)
        {
            RadioButton rb = (RadioButton)this.Master.FindControl("ContentPlaceHolder1").FindControl("FIELD_Q5_" + i);

            if (rb.Checked)
                q5 = rb.Text;
        }

        if (this.FIELD_Q5_3.Checked)
        {
            q5reason = this.FIELD_Q5Reason.Text.Trim();
        }

        #endregion

        #region Q6

        for (int i = 1; i <= 3; i++)
        {
            RadioButton rb = (RadioButton)this.Master.FindControl("ContentPlaceHolder1").FindControl("FIELD_Q6_" + i);

            if (rb.Checked)
                q6 = rb.Text;
        }

        if (this.FIELD_Q6_3.Checked)
        {
            q6reason = this.FIELD_Q6Reason.Text.Trim();
        }

        #endregion

        #region Q7

        for (int i = 1; i <= 3; i++)
        {
            RadioButton rb = (RadioButton)this.Master.FindControl("ContentPlaceHolder1").FindControl("FIELD_Q7_" + i);

            if (rb.Checked)
                q7 = rb.Text;
        }

        if (this.FIELD_Q7_3.Checked)
        {
            q7reason = this.FIELD_Q7Reason.Text.Trim();
        }

        #endregion

        #region Q9

        for (int i = 1; i <= 2; i++)
        {
            RadioButton rb = (RadioButton)this.Master.FindControl("ContentPlaceHolder1").FindControl("FIELD_Q9_" + i);

            if (rb.Checked)
                q9 = rb.Text;
        }

        #endregion

        errormsg = sv.SaveSurveyDataMModel3(this.Request.QueryString["id"], Page.Session["EmpID"].ToString(), this.FIELD_Q1.SelectedValue, q2, q2reason, q3, q3reason, q4, q4reason, q5, q5reason, q6, q6reason, q7, q7reason, this.FIELD_Q8.Text.Trim(), q9);

        if (errormsg != "")
        {
            flag = false;
            MessageBox.Show(errormsg);
        }

        return flag;
    }
}
