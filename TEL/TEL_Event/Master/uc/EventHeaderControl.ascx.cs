using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using TEL.Event.Lab.Method;
using System.Text;

public partial class Master_uc_EventHeaderControl : System.Web.UI.UserControl
{
    protected void Page_Init(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadMenu();

            UserInfo User = new UserInfo(Session["EmpID"].ToString());
            LABEL_Username.Text = User.FullNameEN;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    #region Methods

    protected void LoadMenu()
    {
        try
        {
            //Session["EmpID"] = "123456";

            // 取得使用者的身分
            TEL.Event.Lab.Method.SystemInfo gm = new TEL.Event.Lab.Method.SystemInfo();
            int i = gm.IsManager(Session["EmpID"].ToString());

            // i=3 => 使用者是系統管理者, 功能選單會增加管理網站、管理活動、建立活動的選項
            // i=2 => 使用者是常態活動管理者, 功能選單會增加管理活動、建立活動的選項
            // i=1 => 使用者是其他活動管理者, 功能選單會增加管理活動的選項
            // i=0 => 使用者是一般使用者, 功能選單不會增加選項
            if (i == 3)
            {
                LinkCreateEvents();
                LinkManageEvents();
                LinkManageSite();

            }
            else if (i == 2)
            {
                LinkCreateEvents();
                LinkManageEvents();
            }
            else if (i == 1)
            {
                LinkManageEvents();
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.ToString());
        }
    }

    // 管理網站連結
    protected void LinkManageSite()
    {
        MenuItem mi1 = new MenuItem();
        mi1.Text = this.GetLocalResourceObject("Link_ManageSite.Text").ToString();
        mi1.Value = "管理網站";
        mi1.ImageUrl = "~/Master/images/Link_ManageSite.png";
        mi1.NavigateUrl = "/Event/SystemSetup.aspx";
        this.FIELD_MenuBar.Items[0].ChildItems.AddAt(this.FIELD_MenuBar.Items[0].ChildItems.Count, mi1);
    }

    // 建立活動連結
    protected void LinkCreateEvents()
    {
        MenuItem mi1 = new MenuItem();
        mi1.Text = this.GetLocalResourceObject("Link_CreateEvents.Text").ToString();
        mi1.Value = "建立活動";
        mi1.ImageUrl = "~/Master/images/Link_CreateEvents.png";
        mi1.NavigateUrl = "/Event/Event_Create.aspx";
        this.FIELD_MenuBar.Items[0].ChildItems.AddAt(this.FIELD_MenuBar.Items[0].ChildItems.Count, mi1);
    }

    // 管理活動連結
    protected void LinkManageEvents()
    {
        MenuItem mi1 = new MenuItem();
        mi1.Text = this.GetLocalResourceObject("Link_ManageEvents.Text").ToString();
        mi1.Value = "管理活動";
        mi1.ImageUrl = "~/Master/images/Link_ManageEvents.png";
        mi1.NavigateUrl = "/Event/Event.aspx";
        this.FIELD_MenuBar.Items[0].ChildItems.AddAt(this.FIELD_MenuBar.Items[0].ChildItems.Count, mi1);
    }

    #endregion
}