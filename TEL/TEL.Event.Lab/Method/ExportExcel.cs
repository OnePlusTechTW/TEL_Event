using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;
using System.IO;
using System.Data;
using TEL.Event.Lab.Data;
using System.Web;

namespace TEL.Event.Lab.Method
{
    public class ExportExcel
    {
        //Survey Model1 Export to Excel
        public void ExportSurveyModel1(string eventid)
        {
            var context = HttpContext.Current;
            context.Response.Clear();
            context.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            //設定檔名
            EventInfo ei = new EventInfo(eventid);
            string fileName = ei.EventName + @"_問卷資料.xlsx";

            //建立Excel
            XSSFWorkbook workbook = new XSSFWorkbook();
            XSSFSheet sheet = (XSSFSheet)workbook.CreateSheet("滿意度(講座)");

            //建立儲存格樣式
            ExcelStyle es = new ExcelStyle();
            XSSFCellStyle css1 = es.HeaderStyle(workbook);

            //新增標題列
            sheet.CreateRow(0);
            sheet.GetRow(0).CreateCell(0).SetCellValue("工號");
            sheet.GetRow(0).CreateCell(1).SetCellValue("部門");
            sheet.GetRow(0).CreateCell(2).SetCellValue("中文姓名");
            sheet.GetRow(0).CreateCell(3).SetCellValue("英文姓名");
            sheet.GetRow(0).CreateCell(4).SetCellValue("勤務地");
            sheet.GetRow(0).CreateCell(5).SetCellValue("您如何得知此講座課程");
            sheet.GetRow(0).CreateCell(6).SetCellValue("講師的教學方式");
            sheet.GetRow(0).CreateCell(7).SetCellValue("課程教材的內容");
            sheet.GetRow(0).CreateCell(8).SetCellValue("對於場地的規劃與安排");
            sheet.GetRow(0).CreateCell(9).SetCellValue("對於時間的規劃與安排");
            sheet.GetRow(0).CreateCell(10).SetCellValue("整體而言，對本次活動滿意程度");
            sheet.GetRow(0).CreateCell(11).SetCellValue("未來若再次邀請該講師授課，您的參與意願為");
            sheet.GetRow(0).CreateCell(12).SetCellValue("不願意的原因");
            sheet.GetRow(0).CreateCell(13).SetCellValue("參與此活動對您的幫助");
            sheet.GetRow(0).CreateCell(14).SetCellValue("建議與想法");
            sheet.GetRow(0).CreateCell(15).SetCellValue("是否有推薦公司舉辦的課程");
            sheet.GetRow(0).CreateCell(16).SetCellValue("填寫日期時間");

            //設定Header Style
            for (int i = 0; i < sheet.GetRow(0).Cells.Count; i++)
            {
                sheet.GetRow(0).GetCell(i).CellStyle = css1;
            }

            //設定欄位寬度
            sheet.SetColumnWidth(0, (int)((10 + 0.71) * 256));
            sheet.SetColumnWidth(1, (int)((15 + 0.71) * 256));
            sheet.SetColumnWidth(2, (int)((11 + 0.71) * 256));
            sheet.SetColumnWidth(3, (int)((20 + 0.71) * 256));
            sheet.SetColumnWidth(4, (int)((15 + 0.71) * 256));
            sheet.SetColumnWidth(5, (int)((26 + 0.71) * 256));
            sheet.SetColumnWidth(6, (int)((19 + 0.71) * 256));
            sheet.SetColumnWidth(7, (int)((19 + 0.71) * 256));
            sheet.SetColumnWidth(8, (int)((26 + 0.71) * 256));
            sheet.SetColumnWidth(9, (int)((26 + 0.71) * 256));
            sheet.SetColumnWidth(10, (int)((50 + 0.71) * 256));
            sheet.SetColumnWidth(11, (int)((30 + 0.71) * 256));
            sheet.SetColumnWidth(12, (int)((30 + 0.71) * 256));
            sheet.SetColumnWidth(13, (int)((26 + 0.71) * 256));
            sheet.SetColumnWidth(14, (int)((30 + 0.71) * 256));
            sheet.SetColumnWidth(15, (int)((30 + 0.71) * 256));
            sheet.SetColumnWidth(16, (int)((15 + 0.71) * 256));

            //填入資料
            SurveyData sv = new SurveyData();
            DataTable dt = sv.QueryExportModel1Data(eventid);
            int rowIndex = 1;

            foreach (DataRow dr in dt.Rows)
            {
                sheet.CreateRow(rowIndex);
                sheet.GetRow(rowIndex).CreateCell(0).SetCellValue(dr["empid"].ToString());
                sheet.GetRow(rowIndex).CreateCell(1).SetCellValue(dr["UnitName"].ToString());
                sheet.GetRow(rowIndex).CreateCell(2).SetCellValue(dr["empfullnamech"].ToString());
                sheet.GetRow(rowIndex).CreateCell(3).SetCellValue(dr["empfullnameen"].ToString());
                sheet.GetRow(rowIndex).CreateCell(4).SetCellValue(dr["Station"].ToString());
                sheet.GetRow(rowIndex).CreateCell(5).SetCellValue(dr["q1"].ToString());
                sheet.GetRow(rowIndex).CreateCell(6).SetCellValue(dr["q2"].ToString());
                sheet.GetRow(rowIndex).CreateCell(7).SetCellValue(dr["q3"].ToString());
                sheet.GetRow(rowIndex).CreateCell(8).SetCellValue(dr["q4"].ToString());
                sheet.GetRow(rowIndex).CreateCell(9).SetCellValue(dr["q5"].ToString());
                sheet.GetRow(rowIndex).CreateCell(10).SetCellValue(dr["q6"].ToString());
                sheet.GetRow(rowIndex).CreateCell(11).SetCellValue(dr["q7"].ToString());
                sheet.GetRow(rowIndex).CreateCell(12).SetCellValue(dr["q7reason"].ToString());
                sheet.GetRow(rowIndex).CreateCell(13).SetCellValue(dr["q8"].ToString());
                sheet.GetRow(rowIndex).CreateCell(14).SetCellValue(dr["q9"].ToString());
                sheet.GetRow(rowIndex).CreateCell(15).SetCellValue(dr["q10"].ToString());
                sheet.GetRow(rowIndex).CreateCell(16).SetCellValue(dr["fillindate"].ToString());

                //建立儲存格樣式
                ExcelStyle es2 = new ExcelStyle();
                XSSFCellStyle css2 = es.DataStyle(workbook);

                //設定Data Style
                for (int i = 0; i < sheet.GetRow(rowIndex).Cells.Count; i++)
                {
                    sheet.GetRow(rowIndex).GetCell(i).CellStyle = css2;
                }

                rowIndex++;
            }

            //輸出EXCEL檔案
            MemoryStream ms = new MemoryStream();
            workbook.Write(ms);
            context.Response.AddHeader("Content-Disposition", string.Format("attachment; filename={0}", fileName));
            context.Response.BinaryWrite(ms.ToArray());

            //釋放資源
            workbook = null;
            ms.Close();
            ms.Dispose();
            context.Response.Flush();
            context.Response.Close();
        }

        //Survey Model2 Export to Excel
        public void ExportSurveyModel2(string eventid)
        {
            var context = HttpContext.Current;
            context.Response.Clear();
            context.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            //設定檔名
            EventInfo ei = new EventInfo(eventid);
            string fileName = ei.EventName + @"_問卷資料.xlsx";

            //建立Excel
            XSSFWorkbook workbook = new XSSFWorkbook();
            XSSFSheet sheet = (XSSFSheet)workbook.CreateSheet("滿意度(活動)");

            //建立儲存格樣式
            ExcelStyle es = new ExcelStyle();
            XSSFCellStyle css1 = es.HeaderStyle(workbook);

            //新增標題列
            sheet.CreateRow(0);
            sheet.GetRow(0).CreateCell(0).SetCellValue("工號");
            sheet.GetRow(0).CreateCell(1).SetCellValue("部門");
            sheet.GetRow(0).CreateCell(2).SetCellValue("中文姓名");
            sheet.GetRow(0).CreateCell(3).SetCellValue("英文姓名");
            sheet.GetRow(0).CreateCell(4).SetCellValue("勤務地");
            sheet.GetRow(0).CreateCell(5).SetCellValue("您如何得知此活動");
            sheet.GetRow(0).CreateCell(6).SetCellValue("對於活動的流程之安排");
            sheet.GetRow(0).CreateCell(7).SetCellValue("對於活動的內容之安排");
            sheet.GetRow(0).CreateCell(8).SetCellValue("對於場地的規劃與安排");
            sheet.GetRow(0).CreateCell(9).SetCellValue("對於時間的規劃與安排");
            sheet.GetRow(0).CreateCell(10).SetCellValue("整體而言，對本次活動滿意程度");
            sheet.GetRow(0).CreateCell(11).SetCellValue("建議與想法");
            sheet.GetRow(0).CreateCell(12).SetCellValue("是否有推薦公司舉辦的課程");
            sheet.GetRow(0).CreateCell(13).SetCellValue("填寫日期時間");

            //設定Header Style
            for (int i = 0; i<sheet.GetRow(0).Cells.Count;i++)
            {
                sheet.GetRow(0).GetCell(i).CellStyle = css1;
            }

            //設定欄位寬度
            sheet.SetColumnWidth(0, (int)((10 + 0.71) * 256));
            sheet.SetColumnWidth(1, (int)((15 + 0.71) * 256));
            sheet.SetColumnWidth(2, (int)((11 + 0.71) * 256));
            sheet.SetColumnWidth(3, (int)((20 + 0.71) * 256));
            sheet.SetColumnWidth(4, (int)((15 + 0.71) * 256));
            sheet.SetColumnWidth(5, (int)((22 + 0.71) * 256));
            sheet.SetColumnWidth(6, (int)((26 + 0.71) * 256));
            sheet.SetColumnWidth(7, (int)((26 + 0.71) * 256));
            sheet.SetColumnWidth(8, (int)((26 + 0.71) * 256));
            sheet.SetColumnWidth(9, (int)((26 + 0.71) * 256));
            sheet.SetColumnWidth(10, (int)((35 + 0.71) * 256));
            sheet.SetColumnWidth(11, (int)((30 + 0.71) * 256));
            sheet.SetColumnWidth(12, (int)((30 + 0.71) * 256));
            sheet.SetColumnWidth(13, (int)((15 + 0.71) * 256));

            //填入資料
            SurveyData sv = new SurveyData();
            DataTable dt = sv.QueryExportModel2Data(eventid);
            int rowIndex = 1;

            foreach (DataRow dr in dt.Rows)
            {
                sheet.CreateRow(rowIndex);
                sheet.GetRow(rowIndex).CreateCell(0).SetCellValue(dr["empid"].ToString());
                sheet.GetRow(rowIndex).CreateCell(1).SetCellValue(dr["UnitName"].ToString());
                sheet.GetRow(rowIndex).CreateCell(2).SetCellValue(dr["empfullnamech"].ToString());
                sheet.GetRow(rowIndex).CreateCell(3).SetCellValue(dr["empfullnameen"].ToString());
                sheet.GetRow(rowIndex).CreateCell(4).SetCellValue(dr["Station"].ToString());
                sheet.GetRow(rowIndex).CreateCell(5).SetCellValue(dr["q1"].ToString());
                sheet.GetRow(rowIndex).CreateCell(6).SetCellValue(dr["q2"].ToString());
                sheet.GetRow(rowIndex).CreateCell(7).SetCellValue(dr["q3"].ToString());
                sheet.GetRow(rowIndex).CreateCell(8).SetCellValue(dr["q4"].ToString());
                sheet.GetRow(rowIndex).CreateCell(9).SetCellValue(dr["q5"].ToString());
                sheet.GetRow(rowIndex).CreateCell(10).SetCellValue(dr["q6"].ToString());
                sheet.GetRow(rowIndex).CreateCell(11).SetCellValue(dr["q7"].ToString());
                sheet.GetRow(rowIndex).CreateCell(12).SetCellValue(dr["q8"].ToString());
                sheet.GetRow(rowIndex).CreateCell(13).SetCellValue(dr["fillindate"].ToString());

                //建立儲存格樣式
                ExcelStyle es2 = new ExcelStyle();
                XSSFCellStyle css2 = es.DataStyle(workbook);

                //設定Data Style
                for (int i = 0; i < sheet.GetRow(rowIndex).Cells.Count; i++)
                {
                    sheet.GetRow(rowIndex).GetCell(i).CellStyle = css2;
                }
                
                rowIndex++;
            }

            //輸出EXCEL檔案
            MemoryStream ms = new MemoryStream();
            workbook.Write(ms);
            context.Response.AddHeader("Content-Disposition", string.Format("attachment; filename={0}", fileName));
            context.Response.BinaryWrite(ms.ToArray());

            //釋放資源
            workbook = null;
            ms.Close();
            ms.Dispose();
            context.Response.Flush();
            context.Response.Close();
        }

        //Survey Model3 Export to Excel
        public void ExportSurveyModel3(string eventid)
        {
            var context = HttpContext.Current;
            context.Response.Clear();
            context.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            //設定檔名
            EventInfo ei = new EventInfo(eventid);
            string fileName = ei.EventName + @"_問卷資料.xlsx";

            //建立Excel
            XSSFWorkbook workbook = new XSSFWorkbook();
            XSSFSheet sheet = (XSSFSheet)workbook.CreateSheet("滿意度(健檢)");

            //建立儲存格樣式
            ExcelStyle es = new ExcelStyle();
            XSSFCellStyle css1 = es.HeaderStyle(workbook);

            //新增標題列
            sheet.CreateRow(0);
            sheet.GetRow(0).CreateCell(0).SetCellValue("工號");
            sheet.GetRow(0).CreateCell(1).SetCellValue("部門");
            sheet.GetRow(0).CreateCell(2).SetCellValue("中文姓名");
            sheet.GetRow(0).CreateCell(3).SetCellValue("英文姓名");
            sheet.GetRow(0).CreateCell(4).SetCellValue("勤務地");
            sheet.GetRow(0).CreateCell(5).SetCellValue("本次選擇的健檢中心");
            sheet.GetRow(0).CreateCell(6).SetCellValue("是否有強迫推銷自費項目的行為");
            sheet.GetRow(0).CreateCell(7).SetCellValue("填答是，請說明原因");
            sheet.GetRow(0).CreateCell(8).SetCellValue("醫護人員態度與解說");
            sheet.GetRow(0).CreateCell(9).SetCellValue("填答不滿意，請說明原因");
            sheet.GetRow(0).CreateCell(10).SetCellValue("健檢流程順暢度");
            sheet.GetRow(0).CreateCell(11).SetCellValue("填答不滿意，請說明原因");
            sheet.GetRow(0).CreateCell(12).SetCellValue("整體環境滿意度");
            sheet.GetRow(0).CreateCell(13).SetCellValue("填答不滿意，請說明原因");
            sheet.GetRow(0).CreateCell(14).SetCellValue("餐點滿意度");
            sheet.GetRow(0).CreateCell(15).SetCellValue("填答不滿意，請說明原因");
            sheet.GetRow(0).CreateCell(16).SetCellValue("健檢中心整體滿意度");
            sheet.GetRow(0).CreateCell(17).SetCellValue("填答不滿意，請說明原因");
            sheet.GetRow(0).CreateCell(18).SetCellValue("關於公司提供的年度健檢安排，您是否有其他的建議");
            sheet.GetRow(0).CreateCell(19).SetCellValue("請問是否需要安排社內駐廠醫師提供健檢報告解說");
            sheet.GetRow(0).CreateCell(20).SetCellValue("填寫日期時間");

            //設定Header Style
            for (int i = 0; i < sheet.GetRow(0).Cells.Count; i++)
            {
                sheet.GetRow(0).GetCell(i).CellStyle = css1;
            }

            //設定欄位寬度
            sheet.SetColumnWidth(0, (int)((10 + 0.71) * 256));
            sheet.SetColumnWidth(1, (int)((15 + 0.71) * 256));
            sheet.SetColumnWidth(2, (int)((11 + 0.71) * 256));
            sheet.SetColumnWidth(3, (int)((20 + 0.71) * 256));
            sheet.SetColumnWidth(4, (int)((15 + 0.71) * 256));
            sheet.SetColumnWidth(5, (int)((25 + 0.71) * 256));
            sheet.SetColumnWidth(6, (int)((18 + 0.71) * 256));
            sheet.SetColumnWidth(7, (int)((22 + 0.71) * 256));
            sheet.SetColumnWidth(8, (int)((22 + 0.71) * 256));
            sheet.SetColumnWidth(9, (int)((27 + 0.71) * 256));
            sheet.SetColumnWidth(10, (int)((18 + 0.71) * 256));
            sheet.SetColumnWidth(11, (int)((27 + 0.71) * 256));
            sheet.SetColumnWidth(12, (int)((18 + 0.71) * 256));
            sheet.SetColumnWidth(13, (int)((27 + 0.71) * 256));
            sheet.SetColumnWidth(14, (int)((15 + 0.71) * 256));
            sheet.SetColumnWidth(15, (int)((27 + 0.71) * 256));
            sheet.SetColumnWidth(16, (int)((22 + 0.71) * 256));
            sheet.SetColumnWidth(17, (int)((27 + 0.71) * 256));
            sheet.SetColumnWidth(18, (int)((29 + 0.71) * 256));
            sheet.SetColumnWidth(19, (int)((27 + 0.71) * 256));
            sheet.SetColumnWidth(20, (int)((15 + 0.71) * 256));

            //填入資料
            SurveyData sv = new SurveyData();
            DataTable dt = sv.QueryExportModel3Data(eventid);
            int rowIndex = 1;

            foreach (DataRow dr in dt.Rows)
            {
                sheet.CreateRow(rowIndex);
                sheet.GetRow(rowIndex).CreateCell(0).SetCellValue(dr["empid"].ToString());
                sheet.GetRow(rowIndex).CreateCell(1).SetCellValue(dr["UnitName"].ToString());
                sheet.GetRow(rowIndex).CreateCell(2).SetCellValue(dr["empfullnamech"].ToString());
                sheet.GetRow(rowIndex).CreateCell(3).SetCellValue(dr["empfullnameen"].ToString());
                sheet.GetRow(rowIndex).CreateCell(4).SetCellValue(dr["Station"].ToString());
                sheet.GetRow(rowIndex).CreateCell(5).SetCellValue(dr["q1"].ToString());
                sheet.GetRow(rowIndex).CreateCell(6).SetCellValue(dr["q2"].ToString());
                sheet.GetRow(rowIndex).CreateCell(7).SetCellValue(dr["q2reason"].ToString());
                sheet.GetRow(rowIndex).CreateCell(8).SetCellValue(dr["q3"].ToString());
                sheet.GetRow(rowIndex).CreateCell(9).SetCellValue(dr["q3reason"].ToString());
                sheet.GetRow(rowIndex).CreateCell(10).SetCellValue(dr["q4"].ToString());
                sheet.GetRow(rowIndex).CreateCell(11).SetCellValue(dr["q4reason"].ToString());
                sheet.GetRow(rowIndex).CreateCell(12).SetCellValue(dr["q5"].ToString());
                sheet.GetRow(rowIndex).CreateCell(13).SetCellValue(dr["q5reason"].ToString());
                sheet.GetRow(rowIndex).CreateCell(14).SetCellValue(dr["q6"].ToString());
                sheet.GetRow(rowIndex).CreateCell(15).SetCellValue(dr["q6reason"].ToString());
                sheet.GetRow(rowIndex).CreateCell(16).SetCellValue(dr["q7"].ToString());
                sheet.GetRow(rowIndex).CreateCell(17).SetCellValue(dr["q7reason "].ToString());
                sheet.GetRow(rowIndex).CreateCell(18).SetCellValue(dr["q8"].ToString());
                sheet.GetRow(rowIndex).CreateCell(19).SetCellValue(dr["q9"].ToString());
                sheet.GetRow(rowIndex).CreateCell(20).SetCellValue(dr["fillindate"].ToString());

                //建立儲存格樣式
                ExcelStyle es2 = new ExcelStyle();
                XSSFCellStyle css2 = es.DataStyle(workbook);

                //設定Data Style
                for (int i = 0; i < sheet.GetRow(rowIndex).Cells.Count; i++)
                {
                    sheet.GetRow(rowIndex).GetCell(i).CellStyle = css2;
                }

                rowIndex++;
            }

            //輸出EXCEL檔案
            MemoryStream ms = new MemoryStream();
            workbook.Write(ms);
            context.Response.AddHeader("Content-Disposition", string.Format("attachment; filename={0}", fileName));
            context.Response.BinaryWrite(ms.ToArray());

            //釋放資源
            workbook = null;
            ms.Close();
            ms.Dispose();
            context.Response.Flush();
            context.Response.Close();
        }

        //Survey Model4 Export to Excel
        public void ExportSurveyModel4(string eventid)
        {
            var context = HttpContext.Current;
            context.Response.Clear();
            context.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            //設定檔名
            EventInfo ei = new EventInfo(eventid);
            string fileName = ei.EventName + @"_問卷資料.xlsx";

            //建立Excel
            XSSFWorkbook workbook = new XSSFWorkbook();
            XSSFSheet sheet = (XSSFSheet)workbook.CreateSheet("滿意度(電腦替換)");

            //建立儲存格樣式
            ExcelStyle es = new ExcelStyle();
            XSSFCellStyle css1 = es.HeaderStyle(workbook);

            //新增標題列
            sheet.CreateRow(0);
            sheet.GetRow(0).CreateCell(0).SetCellValue("工號");
            sheet.GetRow(0).CreateCell(1).SetCellValue("部門");
            sheet.GetRow(0).CreateCell(2).SetCellValue("中文姓名");
            sheet.GetRow(0).CreateCell(3).SetCellValue("英文姓名");
            sheet.GetRow(0).CreateCell(4).SetCellValue("勤務地");
            sheet.GetRow(0).CreateCell(5).SetCellValue("對於問卷的填寫");
            sheet.GetRow(0).CreateCell(6).SetCellValue("對於替換的過程");
            sheet.GetRow(0).CreateCell(7).SetCellValue("對於時間的規劃");
            sheet.GetRow(0).CreateCell(8).SetCellValue("整體而言，對本次活動滿意程度");
            sheet.GetRow(0).CreateCell(9).SetCellValue("建議與想法");
            sheet.GetRow(0).CreateCell(10).SetCellValue("填寫日期時間");

            //設定Header Style
            for (int i = 0; i < sheet.GetRow(0).Cells.Count; i++)
            {
                sheet.GetRow(0).GetCell(i).CellStyle = css1;
            }

            //設定欄位寬度
            sheet.SetColumnWidth(0, (int)((10 + 0.71) * 256));
            sheet.SetColumnWidth(1, (int)((15 + 0.71) * 256));
            sheet.SetColumnWidth(2, (int)((11 + 0.71) * 256));
            sheet.SetColumnWidth(3, (int)((20 + 0.71) * 256));
            sheet.SetColumnWidth(4, (int)((15 + 0.71) * 256));
            sheet.SetColumnWidth(5, (int)((20 + 0.71) * 256));
            sheet.SetColumnWidth(6, (int)((20 + 0.71) * 256));
            sheet.SetColumnWidth(7, (int)((20 + 0.71) * 256));
            sheet.SetColumnWidth(8, (int)((35 + 0.71) * 256));
            sheet.SetColumnWidth(9, (int)((30 + 0.71) * 256));
            sheet.SetColumnWidth(10, (int)((15 + 0.71) * 256));

            //填入資料
            SurveyData sv = new SurveyData();
            DataTable dt = sv.QueryExportModel4Data(eventid);
            int rowIndex = 1;

            foreach (DataRow dr in dt.Rows)
            {
                sheet.CreateRow(rowIndex);
                sheet.GetRow(rowIndex).CreateCell(0).SetCellValue(dr["empid"].ToString());
                sheet.GetRow(rowIndex).CreateCell(1).SetCellValue(dr["UnitName"].ToString());
                sheet.GetRow(rowIndex).CreateCell(2).SetCellValue(dr["empfullnamech"].ToString());
                sheet.GetRow(rowIndex).CreateCell(3).SetCellValue(dr["empfullnameen"].ToString());
                sheet.GetRow(rowIndex).CreateCell(4).SetCellValue(dr["Station"].ToString());
                sheet.GetRow(rowIndex).CreateCell(5).SetCellValue(dr["q1"].ToString());
                sheet.GetRow(rowIndex).CreateCell(6).SetCellValue(dr["q2"].ToString());
                sheet.GetRow(rowIndex).CreateCell(7).SetCellValue(dr["q3"].ToString());
                sheet.GetRow(rowIndex).CreateCell(8).SetCellValue(dr["q4"].ToString());
                sheet.GetRow(rowIndex).CreateCell(9).SetCellValue(dr["q5"].ToString());
                sheet.GetRow(rowIndex).CreateCell(10).SetCellValue(dr["fillindate"].ToString());

                //建立儲存格樣式
                ExcelStyle es2 = new ExcelStyle();
                XSSFCellStyle css2 = es.DataStyle(workbook);

                //設定Data Style
                for (int i = 0; i < sheet.GetRow(rowIndex).Cells.Count; i++)
                {
                    sheet.GetRow(rowIndex).GetCell(i).CellStyle = css2;
                }

                rowIndex++;
            }

            //輸出EXCEL檔案
            MemoryStream ms = new MemoryStream();
            workbook.Write(ms);
            context.Response.AddHeader("Content-Disposition", string.Format("attachment; filename={0}", fileName));
            context.Response.BinaryWrite(ms.ToArray());

            //釋放資源
            workbook = null;
            ms.Close();
            ms.Dispose();
            context.Response.Flush();
            context.Response.Close();
        }
    }
}
