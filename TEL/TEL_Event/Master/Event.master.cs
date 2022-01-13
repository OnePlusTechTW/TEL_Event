using System;
using TEL.Event.Lab.Method;

public partial class Master_Event : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Boolean NoSession = true;

        if (Session["EmpID"] != null)
        {
            if (!string.IsNullOrEmpty(Session["EmpID"].ToString()))
            {
                NoSession = false;
            }
        }

        if (NoSession)
        {
            //取得人事帳號資料
            UserInfo tempUser = new UserInfo(GetLogEID());

            if (string.IsNullOrEmpty(tempUser.EmpID))
            {
                //沒有帳號資料
                Session.Clear();
            }
            else
            {
                //取得帳號資料  ex:Session 暫存資料
                Session["EmpID"] = tempUser.EmpID;
            }
        }
    }

    public string GetLogEID()
    {
        string[] tempD = { "\\" };
        string[] s = Request.LogonUserIdentity.Name.Split(tempD, StringSplitOptions.RemoveEmptyEntries);

        return s[s.Length - 1];
    }

}
