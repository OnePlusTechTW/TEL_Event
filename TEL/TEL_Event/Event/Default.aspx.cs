using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TEL.Event.Lab.Method;

public partial class Event_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GeneratedCategoryItem();
        }

        SeDefaultValue();//需要再PostBack再次建立表格，否則按鈕後會消失
    }

    private void SeDefaultValue()
    {
        Event ev = new Event();
        DataTable dt = new DataTable();

        dt = ev.GetUserRegisterEventList(tbEventName.Text, ddlEventCategory.SelectedValue);
        CreateTable(dt); 

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Event ev = new Event();
        DataTable dt = new DataTable();

        dt = ev.GetUserRegisterEventList(tbEventName.Text, ddlEventCategory.SelectedValue);
        CreateTable(dt); //需要再PostBack再次建立表格，否則按鈕後會消失
    }

    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btn1 = (ImageButton)sender;
        if (btn1 == null)
            return;

        string id = btn1.CommandArgument;//可以自訂義參數
        this.UC_EventDescription.setViewDefault(id);
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Guid.NewGuid().ToString(), "ShowDialogEventView();", true);
    }

    protected void btn_Click(object sender, EventArgs e)
    {
        Button btn1 = (Button)sender;
        if (btn1 == null)
            return;

        string id = btn1.CommandArgument.Split(',')[0];//可以自訂義參數
        string registermodel = btn1.CommandArgument.Split(',')[1];
                

        Response.Redirect($"Event_RegisterModel{registermodel}_Create.aspx?id={id}");
        //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Guid.NewGuid().ToString(), "ShowDialogRegisterModel1_Create('" + id + "','"+ registermodel + "');", true);

    }

    private void CreateTable(DataTable dt)
    {

        double count = dt.Rows.Count;
        int estimateRows = Convert.ToInt16(Math.Ceiling(count / 3));

        DataTable dtEvent = dt.Clone();
        Table1.Rows.Clear();

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            dtEvent.ImportRow(dt.Rows[i]);
            if (dtEvent.Rows.Count == 3)
            {
                //GET ROW
                TableRow row = new TableRow();

                row = getEventRow(dtEvent);
                Table1.Rows.Add(row);

                dtEvent.Clear();
                estimateRows--;
            }
        }

        if (estimateRows > 0)
        {
            //GET ROW
            TableRow row = new TableRow();

            row = getEventRow(dtEvent);
            Table1.Rows.Add(row);
            dtEvent.Clear();
            estimateRows--;
        }

    }

    private TableRow getEventRow(DataTable dtEvent)
    {
        TableRow row = new TableRow();
        Event ev = new Event();
        for (int i = 0; i < dtEvent.Rows.Count; i++)
        {
            TableCell cell = new TableCell();
            cell.CssClass = "";

            Table tableContent = new Table();
            tableContent.CssClass = "tableContent";

            //活動圖
            TableRow trImg = new TableRow();
            TableCell tdImg = new TableCell();
            ImageButton img = new ImageButton();
            img.CssClass = "ListImg";
            string path = ConfigurationManager.AppSettings.Get("EventThumbnailPath");
            if (string.IsNullOrEmpty(path))
                path = "~/App_Data/EventThumbnail";

            if (!string.IsNullOrEmpty(dtEvent.Rows[i]["image1"].ToString()))
            {
                img.ImageUrl = Path.Combine(path, dtEvent.Rows[i]["image1"].ToString());
            }
            else
            {
                img.ImageUrl = "~/Master/images/Empty.jpg";
            }

            //img.ImageUrl = "~/Event/EventThumbnail/0fc25346-8d04-455a-88a3-b165e7227c4b.png";
            img.ID = "img" + dtEvent.Rows[i]["eventnid"];
            img.Click += new ImageClickEventHandler(ImageButton1_Click); //添加相應事件
            img.CommandArgument = dtEvent.Rows[i]["eventnid"].ToString();
            //自訂義參數

            tdImg.Controls.Add(img);
            trImg.Cells.Add(tdImg);
            tableContent.Rows.Add(trImg);

            //活動資訊的tr td
            TableRow trInfo = new TableRow();
            TableCell tdInfo = new TableCell();
            tdInfo.CssClass = "tableInfoContentTd";

            //在活動資訊td加入 Content table
            Table tableInfoContent = new Table();
            tableInfoContent.CellSpacing = 0;
            tableInfoContent.CellPadding = 0;
            tableInfoContent.CssClass = "tableInfoContent";

            //Content table 第一列 EventName
            TableRow trEventName = new TableRow();
            TableCell tdEventName = new TableCell();
            Label lblEventName = new Label();
            lblEventName.Text = "◆";
            lblEventName.CssClass = "tableInfoContentFontSize-Icon-large " + GetColorCss(dtEvent.Rows[i]["categorycolor"].ToString());

            lblEventName.ID = "lblEventName" + dtEvent.Rows[i]["eventnid"];
            tdEventName.Controls.Add(lblEventName);//在此欄加入按鈕

            Label lblEventName1 = new Label();
            lblEventName1.Text = dtEvent.Rows[i]["eventname"].ToString();
            lblEventName1.CssClass = "tableInfoContentFontSize-large";
            lblEventName1.ID = "lblEventName1" + dtEvent.Rows[i]["eventnid"];
            tdEventName.Controls.Add(lblEventName1);//在此欄加入按鈕

            trEventName.Cells.Add(tdEventName);
            tableInfoContent.Rows.Add(trEventName);

            //Content table 第二列 EventDate
            TableRow trEventDate = new TableRow();
            TableCell tdEventDate = new TableCell();

            Label lblEventDate = new Label();
            lblEventDate.Text = "◆";
            lblEventDate.CssClass = "tableInfoContentFontSize-Icon-large ";
            lblEventDate.ID = "lblEventDate" + dtEvent.Rows[i]["eventnid"];
            tdEventDate.Controls.Add(lblEventDate);//在此欄加入按鈕

            Label lblEventDate1 = new Label();
            lblEventDate1.Text = $"{dtEvent.Rows[i]["eventstart"].ToString()} ~ {dtEvent.Rows[i]["eventstart"].ToString()}";
            lblEventDate1.CssClass = "tableInfoContentFontSize-small ";
            tdEventDate.Controls.Add(lblEventDate1);//在此欄加入按鈕
            tdEventDate.ID = "tdEventDate" + dtEvent.Rows[i]["eventnid"];

            trEventDate.Cells.Add(tdEventDate);
            tableInfoContent.Rows.Add(trEventDate);

            //Content table 第三列 EventLimit
            TableRow trEventLimit = new TableRow();
            TableCell tdEventLimit = new TableCell();

            Label lblEventLimit = new Label();
            lblEventLimit.Text = "◆";
            lblEventLimit.CssClass = "tableInfoContentFontSize-Icon-large ";
            lblEventLimit.ID = "lblEventLimit" + dtEvent.Rows[i]["eventnid"];

            tdEventLimit.Controls.Add(lblEventLimit);//在此欄加入按鈕

            Label lblEventLimit1 = new Label();
            lblEventLimit1.Text = $"{ev.GetEvnetRegisterCount(dtEvent.Rows[i]["eventnid"].ToString(), dtEvent.Rows[i]["registermodel"].ToString())}/{(dtEvent.Rows[i]["limit"].ToString() == string.Empty ? lblLimit.Text : dtEvent.Rows[i]["limit"].ToString())}";
            lblEventLimit1.CssClass = "tableInfoContentFontSize-small ";

            lblEventLimit1.ID = "lblEventLimit1" + dtEvent.Rows[i]["eventnid"];
            
            tdEventLimit.Controls.Add(lblEventLimit1);//在此欄加入按鈕

            trEventLimit.Cells.Add(tdEventLimit);
            tableInfoContent.Rows.Add(trEventLimit);

            //Content table 第四列 EventCategory
            TableRow trEventCategory = new TableRow();
            TableCell tdEventCategory = new TableCell();
            Label lblEventCategory = new Label();
            lblEventCategory.Text = "◆";
            lblEventCategory.CssClass = "tableInfoContentFontSize-Icon-large " + GetColorCss(dtEvent.Rows[i]["categorycolor"].ToString());
            lblEventCategory.ID = "lblEventCategory" + dtEvent.Rows[i]["eventnid"];

            tdEventCategory.Controls.Add(lblEventCategory);//在此欄加入按鈕

            Label lblEventCategory1 = new Label();
            lblEventCategory1.Text = dtEvent.Rows[i]["categoryname"].ToString();
            lblEventCategory1.CssClass = "tableInfoContentFontSize-large " + GetColorCss(dtEvent.Rows[i]["categorycolor"].ToString());
            tdEventCategory.Controls.Add(lblEventCategory1);//在此欄加入按鈕
            lblEventCategory1.ID = "lblEventCategory1" + dtEvent.Rows[i]["eventnid"];

            trEventCategory.Cells.Add(tdEventCategory);
            tableInfoContent.Rows.Add(trEventCategory);

            //Content table 第五列 Button
            TableRow trButton = new TableRow();
            TableCell tdButton = new TableCell();
            tdButton.HorizontalAlign = HorizontalAlign.Right;
            Button btnEvent = new Button();
            btnEvent.ID = "btnEvent" + dtEvent.Rows[i]["eventnid"];
            btnEvent.CssClass = "Button";
            btnEvent.Click += new EventHandler(btn_Click); //添加相應事件
            btnEvent.CommandArgument = dtEvent.Rows[i]["eventnid"].ToString() + "," + dtEvent.Rows[i]["registermodel"].ToString();//自訂義參數

            Label lblEvent = new Label();
            lblEvent.CssClass = "tableInfoContentFontSize-medium";


            //報名狀態
            DateTime now = DateTime.Now;
            int startCompare = Convert.ToDateTime(dtEvent.Rows[i]["registerstart"].ToString()).CompareTo(now);
            int endCompare = Convert.ToDateTime(dtEvent.Rows[i]["registerend"].ToString()).CompareTo(now);

            if (startCompare > 0)
            {
                // registerstart > now
                lblEvent.Text = lblNYStart.Text;
                tdButton.Controls.Add(lblEvent);//在此欄加入按鈕

            }
            else if (endCompare < 0)
            {
                // registerend < now
                btnEvent.Text = lblView.Text;
                tdButton.Controls.Add(btnEvent);//在此欄加入按鈕
            }
            else
            {
                //registerstart < now < registerend
                btnEvent.Text = lblSignup.Text;
                tdButton.Controls.Add(btnEvent);//在此欄加入按鈕
            }





            trButton.Cells.Add(tdButton);
            tableInfoContent.Rows.Add(trButton);



            //把Content table 加到活動資訊 row
            tdInfo.Controls.Add(tableInfoContent);
            trInfo.Cells.Add(tdInfo);

            //把活動資訊 row 加入 table
            tableContent.Rows.Add(trInfo);


            cell.Controls.Add(tableContent);
            row.Cells.Add(cell);
        }

        return row;
    }

    // 取得活動分類選項
    protected void GeneratedCategoryItem()
    {
        this.ddlEventCategory.Items.Clear();

        ListItem li = new ListItem();
        li.Text = item_all.Text;
        li.Value = "";

        this.ddlEventCategory.Items.Add(li);

        SystemInfo si = new SystemInfo();
        DataTable dt = si.GetEventCategory("All");

        foreach (DataRow rs in dt.Rows)
        {
            ListItem li1 = new ListItem();
            li1.Text = rs["name"].ToString();
            li1.Value = rs["id"].ToString();

            this.ddlEventCategory.Items.Add(li1);
        }
    }

    /// <summary>
    /// 取得色碼css
    /// </summary>
    /// <param name="color"></param>
    /// <returns></returns>
    private string GetColorCss(string color)
    {
        switch (color)
        {
            case "#00A9E0":
                return "Color1";
            case "#71C5E8":
                return "Color2";
            case "#00629B":
                return "Color3";
            case "#78BE20":
                return "Color4";
            case "#B7DD79":
                return "Color5";
            case "#658D1B":
                return "Color6";
            case "#DA1884":
                return "Color7";
            case "#F395C7":
                return "Color8";
            case "#A50050":
                return "Color9";
            case "#00B2A9":
                return "Color10";
            case "#9CDBD9":
                return "Color11";
            case "#007367":
                return "Color12";
            case "#8031A7":
                return "Color13";
            case "#CAA2DD":
                return "Color14";
            case "#572C5F":
                return "Color15";
            case "#EEDC00":
                return "Color16";
            case "#F0EC74":
                return "Color17";
            case "#BBA600":
                return "Color18";
            case "#FF6A13":
                return "Color19";
            case "#FAAA8D":
                return "Color20";
            case "#A65523":
                return "Color21";
            default:
                return string.Empty;
        }
    }

    
}