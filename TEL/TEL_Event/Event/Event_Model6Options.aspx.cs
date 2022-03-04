using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TEL.Event.Lab.Method;

public partial class Event_Event_Model6Options : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            SetDefaultGridView();
        }
    }

    protected void btnImportChangeArea_Click(object sender, EventArgs e)
    {
        tbImportMsg.Text = string.Empty;
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Guid.NewGuid().ToString(), "ShowDialogFileUpload();", true);
    }

    protected void btnImport_Click(object sender, EventArgs e)
    {
        try
        {
            if (FileUpload1.HasFile)
            {
                List<ImportModel> list = new List<ImportModel>();
                list = ExcelToList();

                if (list.Count > 0)
                {
                    //寫入DB
                    Event ev = new Event();
                    string eventid = string.Empty;
                    eventid = Request.QueryString["id"];
                    string result = ev.AddComputerChange(eventid, list, Page.Session["EmpID"].ToString());

                    StringBuilder sb = new StringBuilder();
                    if (string.IsNullOrEmpty(result))
                    {
                        //成功
                        tbImportMsg.Text = lblImportSuccess.Text;
                        SetDefaultGridView();
                    }
                    else
                    {
                        //失敗
                        sb.Append(lblImportFailed.Text);
                        sb.AppendLine();
                        sb.AppendLine();
                        sb.Append(lblImportFailedMsg.Text);
                        sb.AppendLine();
                        sb.Append(result);


                        tbImportMsg.Text = sb.ToString();
                    }
                }
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Guid.NewGuid().ToString(), "ShowDialogFileUpload();", true);
            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void btnfinish_Click(object sender, EventArgs e)
    {
        Response.Redirect("Event.aspx");
    }

    protected void gridRegisterOption6_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gridRegisterOption6.PageIndex = e.NewPageIndex;
        SetDefaultGridView();
    }

    protected void gridRegisterOption6_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //調整GridView資料行底色
            if (e.Row.RowIndex > -1)
            {
                //奇數行
                if (e.Row.RowIndex % 2 == 0)
                    e.Row.BackColor = Color.FromArgb(225, 225, 225);
                else
                    e.Row.BackColor = Color.FromArgb(245, 245, 245);
            }
        }
    }

    /// <summary>
    /// Excel匯入成Datable
    /// </summary>
    /// <param name="file">匯入路徑(包含檔名與副檔名)</param>
    /// <returns></returns>
    public List<ImportModel> ExcelToList()
    {
        DataTable dt = new DataTable();
        IWorkbook workbook;
        string fileExt = Path.GetExtension(FileUpload1.FileName).ToLower();
        Stream uploadFileStream = FileUpload1.PostedFile.InputStream;

        //XSSFWorkbook 適用XLSX格式，HSSFWorkbook 適用XLS格式
        if (fileExt == ".xlsx")
        {
            workbook = new XSSFWorkbook(uploadFileStream);
        }
        else if (fileExt == ".xls")
        {
            workbook = new HSSFWorkbook(uploadFileStream);
        }
        else
        {
            workbook = null;
        }
        if (workbook == null) { return null; }

        ISheet sheet = workbook.GetSheetAt(0);

        //表頭  
        IRow header = sheet.GetRow(sheet.FirstRowNum);
        List<int> columns = new List<int>();
        for (int i = 0; i < header.LastCellNum; i++)
        {
            object obj = GetValueType(header.GetCell(i));
            if (obj == null || obj.ToString() == string.Empty)
            {
                dt.Columns.Add(new DataColumn("Columns" + i.ToString()));
            }
            else
                dt.Columns.Add(new DataColumn(obj.ToString()));
            columns.Add(i);
        }

        //資料 
        List<int> duplicateList = new List<int>();

        for (int i = sheet.FirstRowNum + 1; i <= sheet.LastRowNum; i++)
        {
            ImportModel group = new ImportModel();
            DataRow dr = dt.NewRow();
            bool hasValue = false;
            foreach (int j in columns)
            {
                dr[j] = GetValueType(sheet.GetRow(i).GetCell(j));
                if (dr[j] != null && dr[j].ToString() != string.Empty)
                {
                    hasValue = true;
                }
            }
            if (hasValue)
            {
                DataRow[] rows = dt.Select($@"地點='{dr["地點"].ToString()}' AND 日期時間='{dr["日期時間"].ToString()}' ");

                if (rows.Count() > 0)
                    duplicateList.Add(i + 1);

                dt.Rows.Add(dr);
            }
        }

        List<ImportModel> list = new List<ImportModel>();

        if (duplicateList.Count > 0)
        {
            StringBuilder sb = new StringBuilder();

            string rowsList = string.Empty;
            foreach (int row in duplicateList)
            {
                rowsList += row.ToString() + ",";
            }

            //失敗
            sb.Append(lblImportFailed.Text);
            sb.AppendLine();
            sb.AppendLine();
            sb.Append(string.Format(lblDuplicate.Text, rowsList.Substring(0, rowsList.Length - 1)));
            sb.AppendLine();

            tbImportMsg.Text = sb.ToString();

            return list;
        }

        list = (from DataRow dr in dt.Rows
                select new ImportModel()
                {
                    area = dr["地點"].ToString(),
                    avaliabledate = Convert.ToDateTime(dr["日期時間"].ToString()).ToString("yyyy/MM/dd 00:00:00"),
                    limit = dr["人數上限"].ToString()
                }).ToList();


        return list;
    }

    /// <summary>
    /// 獲取單元格型別
    /// </summary>
    /// <param name="cell"></param>
    /// <returns></returns>
    private static object GetValueType(ICell cell)
    {
        if (cell == null)
            return null;
        switch (cell.CellType)
        {
            case CellType.Blank: //BLANK:  
                return null;
            case CellType.Boolean: //BOOLEAN:  
                return cell.BooleanCellValue;
            case CellType.Numeric: //NUMERIC:
                if (HSSFDateUtil.IsCellDateFormatted(cell))//日期類型
                    return cell.DateCellValue;
                else//其他數字類型
                    return cell.NumericCellValue;
            case CellType.String: //STRING:  
                return cell.StringCellValue;
            case CellType.Error: //ERROR:  
                return cell.ErrorCellValue;
            case CellType.Formula: //FORMULA:  
            default:
                return "=" + cell.CellFormula;
        }
    }
    private void SetDefaultGridView()
    {
        DataTable dt = new DataTable();
        Event ev = new Event();
        string eventid = string.Empty;
        eventid = Request.QueryString["id"];
        dt = ev.GetComputerChange(eventid);
        this.gridRegisterOption6.DataSource = dt;
        this.gridRegisterOption6.DataBind();
    }
}