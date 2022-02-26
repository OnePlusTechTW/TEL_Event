using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TEL.Event.Lab.Method;

public partial class Event_Register_PreCreate : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["id"] == null || string.IsNullOrEmpty(Request.QueryString["id"]))
            Response.Redirect("Register.aspx");
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        //登入檢查
        //若為一般使用者則導到Denied頁面
        TEL.Event.Lab.Method.SystemInfo gm = new TEL.Event.Lab.Method.SystemInfo();
        if (gm.IsDenied(Page.Session["EmpID"].ToString(), Request.QueryString["id"]) < 1)
            Response.Redirect("Denied.aspx");
    }

    protected void btnNextStep_Click(object sender, EventArgs e)
    {
        //需做工號檢查，確認有此員工才導到活動報名新增頁面，若錯誤則顯示錯誤訊息(查無此員工)
        UserInfo userinfo = new UserInfo(txtEmpID.Text);
        if (string.IsNullOrEmpty(userinfo.EmpID))
        {
            lblDialogMsg.Text = lblNoEmp.Text;
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Guid.NewGuid().ToString(), "ShowDialogMsg();", true);

            return;
        }

        string eventid = Request.QueryString["id"];

        EventInfo evInfo = new EventInfo(eventid);

        Response.Redirect($"Event_RegisterModel{evInfo.EventRegisterModel}_Create.aspx?id={eventid}&EmpID={userinfo.EmpID}&page=Register");

    }
}