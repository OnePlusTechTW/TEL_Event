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
                sheet.GetRow(rowIndex).CreateCell(17).SetCellValue(dr["q7reason"].ToString());
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
            sheet.GetRow(0).CreateCell(5).SetCellValue("對於替換的過程");
            sheet.GetRow(0).CreateCell(6).SetCellValue("對於時間的規劃");
            sheet.GetRow(0).CreateCell(7).SetCellValue("整體而言，對本次活動滿意程度");
            sheet.GetRow(0).CreateCell(8).SetCellValue("建議與想法");
            sheet.GetRow(0).CreateCell(9).SetCellValue("填寫日期時間");

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
            sheet.SetColumnWidth(7, (int)((40 + 0.71) * 256));
            sheet.SetColumnWidth(8, (int)((30 + 0.71) * 256));
            sheet.SetColumnWidth(9, (int)((20 + 0.71) * 256));

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
                sheet.GetRow(rowIndex).CreateCell(5).SetCellValue(dr["q2"].ToString());
                sheet.GetRow(rowIndex).CreateCell(6).SetCellValue(dr["q3"].ToString());
                sheet.GetRow(rowIndex).CreateCell(7).SetCellValue(dr["q4"].ToString());
                sheet.GetRow(rowIndex).CreateCell(8).SetCellValue(dr["q5"].ToString());
                sheet.GetRow(rowIndex).CreateCell(9).SetCellValue(dr["fillindate"].ToString());

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

        public void ExportRegisterModel1(string eventid, string empName)
        {
            var context = HttpContext.Current;
            context.Response.Clear();
            context.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            //設定檔名
            EventInfo ei = new EventInfo(eventid);
            string fileName = ei.EventName + @"_報名資料.xlsx";

            //建立Excel
            XSSFWorkbook workbook = new XSSFWorkbook();
            XSSFSheet sheet = (XSSFSheet)workbook.CreateSheet("活動報名 (簡)");

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

            sheet.GetRow(0).CreateCell(5).SetCellValue("參加的內容");
            sheet.GetRow(0).CreateCell(6).SetCellValue("意見/問題回饋");
            sheet.GetRow(0).CreateCell(7).SetCellValue("報名日期時間");
            sheet.GetRow(0).CreateCell(8).SetCellValue("最後修改人員");
            sheet.GetRow(0).CreateCell(9).SetCellValue("最後修改日期時間");

            

            //設定Header Style
            for (int i = 0; i < sheet.GetRow(0).Cells.Count; i++)
            {
                sheet.GetRow(0).GetCell(i).CellStyle = css1;
            }

            //設定欄位寬度
            sheet.SetColumnWidth(0, (int)((10 + 0.71) * 256));
            sheet.SetColumnWidth(1, (int)((15 + 0.71) * 256));
            sheet.SetColumnWidth(2, (int)((20 + 0.71) * 256));
            sheet.SetColumnWidth(3, (int)((20 + 0.71) * 256));
            sheet.SetColumnWidth(4, (int)((15 + 0.71) * 256));

            sheet.SetColumnWidth(5, (int)((25 + 0.71) * 256));
            sheet.SetColumnWidth(6, (int)((35 + 0.71) * 256));
            sheet.SetColumnWidth(7, (int)((25 + 0.71) * 256));//報名日期時間
            sheet.SetColumnWidth(8, (int)((15 + 0.71) * 256));//最後修改人員
            sheet.SetColumnWidth(9, (int)((25 + 0.71) * 256));//最後修改日期時間



            //填入資料
            Register re = new Register();
            DataTable dt = re.GetExportRegisterModel1Data(eventid, empName);
            
            int rowIndex = 1;

            foreach (DataRow dr in dt.Rows)
            {
                UserInfo userInfo = new UserInfo(dr["empid"].ToString());

                sheet.CreateRow(rowIndex);
                sheet.GetRow(rowIndex).CreateCell(0).SetCellValue(dr["empid"].ToString());
                sheet.GetRow(rowIndex).CreateCell(1).SetCellValue($"{userInfo.UnitCode}-{userInfo.UnitName}");
                sheet.GetRow(rowIndex).CreateCell(2).SetCellValue($"{userInfo.LastNameCH}{userInfo.FirstNameCH}");
                sheet.GetRow(rowIndex).CreateCell(3).SetCellValue($"{userInfo.FirstNameEN} {userInfo.LastNameEN}");
                sheet.GetRow(rowIndex).CreateCell(4).SetCellValue(userInfo.Station);
                sheet.GetRow(rowIndex).CreateCell(5).SetCellValue(dr["selectedoption"].ToString());
                sheet.GetRow(rowIndex).CreateCell(6).SetCellValue(dr["feedback"].ToString());
                sheet.GetRow(rowIndex).CreateCell(7).SetCellValue(dr["registerdate"].ToString());
                sheet.GetRow(rowIndex).CreateCell(8).SetCellValue(dr["modifiedby"].ToString());
                sheet.GetRow(rowIndex).CreateCell(9).SetCellValue(dr["modifieddate"].ToString());
                

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

        public void ExportRegisterModel2(string eventid, string empName)
        {
            var context = HttpContext.Current;
            context.Response.Clear();
            context.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            //設定檔名
            EventInfo ei = new EventInfo(eventid);
            string fileName = ei.EventName + @"_報名資料.xlsx";

            //建立Excel
            XSSFWorkbook workbook = new XSSFWorkbook();
            #region 活動報名(個資)

            XSSFSheet sheet = (XSSFSheet)workbook.CreateSheet("活動報名(個資)");

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
            sheet.GetRow(0).CreateCell(5).SetCellValue("身分證字號");
            sheet.GetRow(0).CreateCell(6).SetCellValue("出生年月日");
            sheet.GetRow(0).CreateCell(7).SetCellValue("性別");
            sheet.GetRow(0).CreateCell(8).SetCellValue("E-mail");


            sheet.GetRow(0).CreateCell(9).SetCellValue("手機");
            sheet.GetRow(0).CreateCell(10).SetCellValue("參加的內容");
            sheet.GetRow(0).CreateCell(11).SetCellValue("交通車");
            sheet.GetRow(0).CreateCell(12).SetCellValue("餐點內容");


            sheet.GetRow(0).CreateCell(13).SetCellValue("意見/問題回饋");
            sheet.GetRow(0).CreateCell(14).SetCellValue("報名日期時間");
            sheet.GetRow(0).CreateCell(15).SetCellValue("最後修改人員");
            sheet.GetRow(0).CreateCell(16).SetCellValue("最後修改日期時間");



            //設定Header Style
            for (int i = 0; i < sheet.GetRow(0).Cells.Count; i++)
            {
                sheet.GetRow(0).GetCell(i).CellStyle = css1;
            }

            //設定欄位寬度
            sheet.SetColumnWidth(0, (int)((10 + 0.71) * 256));//工號
            sheet.SetColumnWidth(1, (int)((15 + 0.71) * 256));//部門
            sheet.SetColumnWidth(2, (int)((20 + 0.71) * 256));//中文姓名
            sheet.SetColumnWidth(3, (int)((20 + 0.71) * 256));//英文姓名
            sheet.SetColumnWidth(4, (int)((15 + 0.71) * 256));//勤務地
            sheet.SetColumnWidth(5, (int)((20 + 0.71) * 256));//身分證字號
            sheet.SetColumnWidth(6, (int)((20 + 0.71) * 256));//出生年月日
            sheet.SetColumnWidth(7, (int)((10 + 0.71) * 256));//性別
            sheet.SetColumnWidth(8, (int)((20 + 0.71) * 256));//mail

            sheet.SetColumnWidth(9, (int)((20 + 0.71) * 256));//手機
            sheet.SetColumnWidth(10, (int)((25 + 0.71) * 256));//參加的內容
            sheet.SetColumnWidth(11, (int)((15 + 0.71) * 256));//交通車
            sheet.SetColumnWidth(12, (int)((15 + 0.71) * 256));//餐點內容


            sheet.SetColumnWidth(13, (int)((35 + 0.71) * 256));//意見/問題回饋
            sheet.SetColumnWidth(14, (int)((25 + 0.71) * 256));//報名日期時間
            sheet.SetColumnWidth(15, (int)((15 + 0.71) * 256));//最後修改人員
            sheet.SetColumnWidth(16, (int)((25 + 0.71) * 256));//最後修改日期時間



            //填入資料
            Register re = new Register();
            DataTable dt = re.GetExportRegisterModel2Data(eventid, empName);

            int rowIndex = 1;

            foreach (DataRow dr in dt.Rows)
            {
                UserInfo userInfo = new UserInfo(dr["empid"].ToString());

                sheet.CreateRow(rowIndex);
                sheet.GetRow(rowIndex).CreateCell(0).SetCellValue(dr["empid"].ToString());
                sheet.GetRow(rowIndex).CreateCell(1).SetCellValue($"{userInfo.UnitCode}-{userInfo.UnitName}");
                sheet.GetRow(rowIndex).CreateCell(2).SetCellValue($"{userInfo.LastNameCH}{userInfo.FirstNameCH}");
                sheet.GetRow(rowIndex).CreateCell(3).SetCellValue($"{userInfo.FirstNameEN} {userInfo.LastNameEN}");
                sheet.GetRow(rowIndex).CreateCell(4).SetCellValue(userInfo.Station);
                sheet.GetRow(rowIndex).CreateCell(5).SetCellValue(userInfo.NationalID);
                sheet.GetRow(rowIndex).CreateCell(6).SetCellValue(userInfo.Birthday);
                sheet.GetRow(rowIndex).CreateCell(7).SetCellValue(userInfo.Gender);
                sheet.GetRow(rowIndex).CreateCell(8).SetCellValue(userInfo.EMail);

                sheet.GetRow(rowIndex).CreateCell(9).SetCellValue(dr["mobile"].ToString());
                sheet.GetRow(rowIndex).CreateCell(10).SetCellValue(dr["selectedoption"].ToString());
                sheet.GetRow(rowIndex).CreateCell(11).SetCellValue(dr["traffic"].ToString());
                sheet.GetRow(rowIndex).CreateCell(12).SetCellValue(dr["meal"].ToString());


                sheet.GetRow(rowIndex).CreateCell(13).SetCellValue(dr["feedback"].ToString());
                sheet.GetRow(rowIndex).CreateCell(14).SetCellValue(dr["registerdate"].ToString());
                sheet.GetRow(rowIndex).CreateCell(15).SetCellValue(dr["modifiedby"].ToString());
                sheet.GetRow(rowIndex).CreateCell(16).SetCellValue(dr["modifieddate"].ToString());


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

            #endregion

            #region 活動報名(家屬)

            XSSFSheet sheetFamily = (XSSFSheet)workbook.CreateSheet("活動報名(家屬)");

            //建立儲存格樣式
            ExcelStyle esFamily = new ExcelStyle();
            XSSFCellStyle cssesFamily = es.HeaderStyle(workbook);

            //新增標題列
            sheetFamily.CreateRow(0);
            sheetFamily.GetRow(0).CreateCell(0).SetCellValue("工號");
            sheetFamily.GetRow(0).CreateCell(1).SetCellValue("部門");
            sheetFamily.GetRow(0).CreateCell(2).SetCellValue("中文姓名");
            sheetFamily.GetRow(0).CreateCell(3).SetCellValue("英文姓名");
            sheetFamily.GetRow(0).CreateCell(4).SetCellValue("勤務地");
            sheetFamily.GetRow(0).CreateCell(5).SetCellValue("身分證字號");
            sheetFamily.GetRow(0).CreateCell(6).SetCellValue("出生年月日");
            sheetFamily.GetRow(0).CreateCell(7).SetCellValue("性別");
            sheetFamily.GetRow(0).CreateCell(8).SetCellValue("E-mail");
            sheetFamily.GetRow(0).CreateCell(9).SetCellValue("報名日期時間");


            sheetFamily.GetRow(0).CreateCell(10).SetCellValue("家屬姓名");
            sheetFamily.GetRow(0).CreateCell(11).SetCellValue("家屬身份證字號");
            sheetFamily.GetRow(0).CreateCell(12).SetCellValue("家屬出生年月日");
            sheetFamily.GetRow(0).CreateCell(13).SetCellValue("家屬性別");
            sheetFamily.GetRow(0).CreateCell(14).SetCellValue("餐點內容");


            sheetFamily.GetRow(0).CreateCell(15).SetCellValue("最後修改人員");
            sheetFamily.GetRow(0).CreateCell(16).SetCellValue("最後修改日期時間");



            //設定Header Style
            for (int i = 0; i < sheetFamily.GetRow(0).Cells.Count; i++)
            {
                sheetFamily.GetRow(0).GetCell(i).CellStyle = css1;
            }

            //設定欄位寬度
            sheetFamily.SetColumnWidth(0, (int)((10 + 0.71) * 256));//工號
            sheetFamily.SetColumnWidth(1, (int)((15 + 0.71) * 256));//部門
            sheetFamily.SetColumnWidth(2, (int)((20 + 0.71) * 256));//中文姓名
            sheetFamily.SetColumnWidth(3, (int)((20 + 0.71) * 256));//英文姓名
            sheetFamily.SetColumnWidth(4, (int)((15 + 0.71) * 256));//勤務地
            sheetFamily.SetColumnWidth(5, (int)((20 + 0.71) * 256));//身分證字號
            sheetFamily.SetColumnWidth(6, (int)((20 + 0.71) * 256));//出生年月日
            sheetFamily.SetColumnWidth(7, (int)((10 + 0.71) * 256));//性別
            sheetFamily.SetColumnWidth(8, (int)((20 + 0.71) * 256));//mail
            sheetFamily.SetColumnWidth(9, (int)((25 + 0.71) * 256));//報名日期時間


            sheetFamily.SetColumnWidth(10, (int)((20 + 0.71) * 256));//家屬姓名
            sheetFamily.SetColumnWidth(11, (int)((20 + 0.71) * 256));//家屬身份證字號
            sheetFamily.SetColumnWidth(12, (int)((25 + 0.71) * 256));//家屬出生年月日
            sheetFamily.SetColumnWidth(13, (int)((10 + 0.71) * 256));//家屬性別
            sheetFamily.SetColumnWidth(14, (int)((15 + 0.71) * 256));//餐點內容


            sheetFamily.SetColumnWidth(15, (int)((15 + 0.71) * 256));//最後修改人員
            sheetFamily.SetColumnWidth(16, (int)((25 + 0.71) * 256));//最後修改日期時間



            //填入資料
            DataTable dtsheetFamily = re.GetExportRegisterModel2FamilyData(eventid, empName);

            int rowIndexsheetFamily = 1;

            foreach (DataRow dr in dtsheetFamily.Rows)
            {
                UserInfo userInfo = new UserInfo(dr["empid"].ToString());

                sheetFamily.CreateRow(rowIndexsheetFamily);
                sheetFamily.GetRow(rowIndexsheetFamily).CreateCell(0).SetCellValue(dr["empid"].ToString());
                sheetFamily.GetRow(rowIndexsheetFamily).CreateCell(1).SetCellValue($"{userInfo.UnitCode}-{userInfo.UnitName}");
                sheetFamily.GetRow(rowIndexsheetFamily).CreateCell(2).SetCellValue($"{userInfo.LastNameCH}{userInfo.FirstNameCH}");
                sheetFamily.GetRow(rowIndexsheetFamily).CreateCell(3).SetCellValue($"{userInfo.FirstNameEN} {userInfo.LastNameEN}");
                sheetFamily.GetRow(rowIndexsheetFamily).CreateCell(4).SetCellValue(userInfo.Station);
                sheetFamily.GetRow(rowIndexsheetFamily).CreateCell(5).SetCellValue(userInfo.NationalID);
                sheetFamily.GetRow(rowIndexsheetFamily).CreateCell(6).SetCellValue(userInfo.Birthday);
                sheetFamily.GetRow(rowIndexsheetFamily).CreateCell(7).SetCellValue(userInfo.Gender);
                sheetFamily.GetRow(rowIndexsheetFamily).CreateCell(8).SetCellValue(userInfo.EMail);

                sheetFamily.GetRow(rowIndexsheetFamily).CreateCell(9).SetCellValue(dr["registerdate"].ToString());

                sheetFamily.GetRow(rowIndexsheetFamily).CreateCell(10).SetCellValue(dr["name"].ToString());
                sheetFamily.GetRow(rowIndexsheetFamily).CreateCell(11).SetCellValue(dr["idno"].ToString());
                sheetFamily.GetRow(rowIndexsheetFamily).CreateCell(12).SetCellValue(dr["birthdayfamily"].ToString());
                sheetFamily.GetRow(rowIndexsheetFamily).CreateCell(13).SetCellValue(dr["gender"].ToString());
                sheetFamily.GetRow(rowIndexsheetFamily).CreateCell(14).SetCellValue(dr["meal"].ToString());


                sheetFamily.GetRow(rowIndexsheetFamily).CreateCell(15).SetCellValue(dr["modifiedby"].ToString());
                sheetFamily.GetRow(rowIndexsheetFamily).CreateCell(16).SetCellValue(dr["modifieddate"].ToString());


                //建立儲存格樣式
                ExcelStyle es2 = new ExcelStyle();
                XSSFCellStyle css2 = es.DataStyle(workbook);

                //設定Data Style
                for (int i = 0; i < sheetFamily.GetRow(rowIndexsheetFamily).Cells.Count; i++)
                {
                    sheetFamily.GetRow(rowIndexsheetFamily).GetCell(i).CellStyle = css2;
                }

                rowIndexsheetFamily++;
            }

            #endregion

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

        public void ExportRegisterModel3(string eventid, string empName)
        {
            var context = HttpContext.Current;
            context.Response.Clear();
            context.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            //設定檔名
            EventInfo ei = new EventInfo(eventid);
            string fileName = ei.EventName + @"_報名資料.xlsx";

            //建立Excel
            XSSFWorkbook workbook = new XSSFWorkbook();
            XSSFSheet sheet = (XSSFSheet)workbook.CreateSheet("健康檢查報名(Local))");

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
            sheet.GetRow(0).CreateCell(5).SetCellValue("健檢組別");


            sheet.GetRow(0).CreateCell(6).SetCellValue("受診者身分別");
            sheet.GetRow(0).CreateCell(7).SetCellValue("受診者中文姓名");
            sheet.GetRow(0).CreateCell(8).SetCellValue("受診者身分證字號");
            sheet.GetRow(0).CreateCell(9).SetCellValue("受診者出生年月日");
            sheet.GetRow(0).CreateCell(10).SetCellValue("受診者手機");
            sheet.GetRow(0).CreateCell(11).SetCellValue("健檢醫院");
            sheet.GetRow(0).CreateCell(12).SetCellValue("地區");
            sheet.GetRow(0).CreateCell(13).SetCellValue("費用&方案");
            sheet.GetRow(0).CreateCell(14).SetCellValue("受診者性別");
            sheet.GetRow(0).CreateCell(15).SetCellValue("健檢次方案1");
            sheet.GetRow(0).CreateCell(16).SetCellValue("健檢次方案2");
            sheet.GetRow(0).CreateCell(17).SetCellValue("健檢次方案3");
            sheet.GetRow(0).CreateCell(18).SetCellValue("期望受檢日");
            sheet.GetRow(0).CreateCell(19).SetCellValue("備用受檢日");
            sheet.GetRow(0).CreateCell(20).SetCellValue("自費加選項目");
            sheet.GetRow(0).CreateCell(21).SetCellValue("健檢包寄送地點");
            sheet.GetRow(0).CreateCell(22).SetCellValue("餐點樣式");


            sheet.GetRow(0).CreateCell(23).SetCellValue("意見/問題回饋");
            sheet.GetRow(0).CreateCell(24).SetCellValue("報名日期時間");
            sheet.GetRow(0).CreateCell(25).SetCellValue("最後修改人員");
            sheet.GetRow(0).CreateCell(26).SetCellValue("最後修改日期時間");



            //設定Header Style
            for (int i = 0; i < sheet.GetRow(0).Cells.Count; i++)
            {
                sheet.GetRow(0).GetCell(i).CellStyle = css1;
            }

            //設定欄位寬度
            sheet.SetColumnWidth(0, (int)((10 + 0.71) * 256));
            sheet.SetColumnWidth(1, (int)((15 + 0.71) * 256));
            sheet.SetColumnWidth(2, (int)((20 + 0.71) * 256));
            sheet.SetColumnWidth(3, (int)((20 + 0.71) * 256));
            sheet.SetColumnWidth(4, (int)((15 + 0.71) * 256));
            sheet.SetColumnWidth(5, (int)((15 + 0.71) * 256));


            sheet.SetColumnWidth(6, (int)((15 + 0.71) * 256));
            sheet.SetColumnWidth(7, (int)((20 + 0.71) * 256));
            sheet.SetColumnWidth(8, (int)((20 + 0.71) * 256));
            sheet.SetColumnWidth(9, (int)((20 + 0.71) * 256));
            sheet.SetColumnWidth(10, (int)((15 + 0.71) * 256));
            sheet.SetColumnWidth(11, (int)((15 + 0.71) * 256));
            sheet.SetColumnWidth(12, (int)((15 + 0.71) * 256));
            sheet.SetColumnWidth(13, (int)((30 + 0.71) * 256));
            sheet.SetColumnWidth(14, (int)((15 + 0.71) * 256));
            sheet.SetColumnWidth(15, (int)((15 + 0.71) * 256));
            sheet.SetColumnWidth(16, (int)((15 + 0.71) * 256));
            sheet.SetColumnWidth(17, (int)((15 + 0.71) * 256));
            sheet.SetColumnWidth(18, (int)((15 + 0.71) * 256));
            sheet.SetColumnWidth(19, (int)((15 + 0.71) * 256));
            sheet.SetColumnWidth(20, (int)((15 + 0.71) * 256));
            sheet.SetColumnWidth(21, (int)((20 + 0.71) * 256));
            sheet.SetColumnWidth(22, (int)((20 + 0.71) * 256));



            sheet.SetColumnWidth(23, (int)((35 + 0.71) * 256));
            sheet.SetColumnWidth(24, (int)((25 + 0.71) * 256));//報名日期時間
            sheet.SetColumnWidth(25, (int)((15 + 0.71) * 256));//最後修改人員
            sheet.SetColumnWidth(26, (int)((25 + 0.71) * 256));//最後修改日期時間



            //填入資料
            Register re = new Register();
            DataTable dt = re.GetExportRegisterModel3Data(eventid, empName);
            int rowIndex = 1;

            foreach (DataRow dr in dt.Rows)
            {
                UserInfo userInfo = new UserInfo(dr["empid"].ToString());

                sheet.CreateRow(rowIndex);
                sheet.GetRow(rowIndex).CreateCell(0).SetCellValue(dr["empid"].ToString());
                sheet.GetRow(rowIndex).CreateCell(1).SetCellValue($"{userInfo.UnitCode}-{userInfo.UnitName}");
                sheet.GetRow(rowIndex).CreateCell(2).SetCellValue($"{userInfo.LastNameCH}{userInfo.FirstNameCH}");
                sheet.GetRow(rowIndex).CreateCell(3).SetCellValue($"{userInfo.FirstNameEN} {userInfo.LastNameEN}");
                sheet.GetRow(rowIndex).CreateCell(4).SetCellValue(userInfo.Station);
                sheet.GetRow(rowIndex).CreateCell(5).SetCellValue(userInfo.HealthGroup);

                sheet.GetRow(rowIndex).CreateCell(6).SetCellValue(dr["examineeidentity"].ToString());
                sheet.GetRow(rowIndex).CreateCell(7).SetCellValue(dr["examineename"].ToString());
                sheet.GetRow(rowIndex).CreateCell(8).SetCellValue(dr["examineeidno"].ToString());
                sheet.GetRow(rowIndex).CreateCell(9).SetCellValue(dr["examineebirthday"].ToString());
                sheet.GetRow(rowIndex).CreateCell(10).SetCellValue(dr["examineemobile"].ToString());
                sheet.GetRow(rowIndex).CreateCell(11).SetCellValue(dr["hosipital"].ToString());
                sheet.GetRow(rowIndex).CreateCell(12).SetCellValue(dr["area"].ToString());
                sheet.GetRow(rowIndex).CreateCell(13).SetCellValue(dr["solution"].ToString());
                sheet.GetRow(rowIndex).CreateCell(14).SetCellValue(dr["gender"].ToString());
                sheet.GetRow(rowIndex).CreateCell(15).SetCellValue(dr["expectdate"].ToString());
                sheet.GetRow(rowIndex).CreateCell(16).SetCellValue(dr["seconddate"].ToString());
                sheet.GetRow(rowIndex).CreateCell(17).SetCellValue(dr["secondsolution1"].ToString());
                sheet.GetRow(rowIndex).CreateCell(18).SetCellValue(dr["secondsolution2"].ToString());
                sheet.GetRow(rowIndex).CreateCell(19).SetCellValue(dr["secondsolution3"].ToString());
                sheet.GetRow(rowIndex).CreateCell(20).SetCellValue(dr["optional"].ToString());
                sheet.GetRow(rowIndex).CreateCell(21).SetCellValue(dr["address"].ToString());
                sheet.GetRow(rowIndex).CreateCell(22).SetCellValue(dr["meal"].ToString());

                sheet.GetRow(rowIndex).CreateCell(23).SetCellValue(dr["feedback"].ToString());
                sheet.GetRow(rowIndex).CreateCell(24).SetCellValue(dr["registerdate"].ToString());
                sheet.GetRow(rowIndex).CreateCell(25).SetCellValue(dr["modifiedby"].ToString());
                sheet.GetRow(rowIndex).CreateCell(26).SetCellValue(dr["modifieddate"].ToString());


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

        public void ExportRegisterModel4(string eventid, string empName)
        {
            var context = HttpContext.Current;
            context.Response.Clear();
            context.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            //設定檔名
            EventInfo ei = new EventInfo(eventid);
            string fileName = ei.EventName + @"_報名資料.xlsx";

            //建立Excel
            XSSFWorkbook workbook = new XSSFWorkbook();
            XSSFSheet sheet = (XSSFSheet)workbook.CreateSheet("健康檢查報名 (駐在員)");

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
            sheet.GetRow(0).CreateCell(5).SetCellValue("健檢組別");


            sheet.GetRow(0).CreateCell(6).SetCellValue("受診者身分別");
            sheet.GetRow(0).CreateCell(7).SetCellValue("受診者姓名");
            sheet.GetRow(0).CreateCell(8).SetCellValue("受診者拼音");
            sheet.GetRow(0).CreateCell(9).SetCellValue("受診者居留證字號");
            sheet.GetRow(0).CreateCell(10).SetCellValue("受診者出生年月日");
            sheet.GetRow(0).CreateCell(11).SetCellValue("受診者手機");
            sheet.GetRow(0).CreateCell(12).SetCellValue("健檢醫院");
            sheet.GetRow(0).CreateCell(13).SetCellValue("地區");
            sheet.GetRow(0).CreateCell(14).SetCellValue("費用&方案");
            sheet.GetRow(0).CreateCell(15).SetCellValue("受診者性別");
            sheet.GetRow(0).CreateCell(16).SetCellValue("健檢次方案1");
            sheet.GetRow(0).CreateCell(17).SetCellValue("健檢次方案2");
            sheet.GetRow(0).CreateCell(18).SetCellValue("健檢次方案3");
            sheet.GetRow(0).CreateCell(19).SetCellValue("期望受檢日");
            sheet.GetRow(0).CreateCell(20).SetCellValue("備用受檢日");
            sheet.GetRow(0).CreateCell(21).SetCellValue("自費加選項目");
            sheet.GetRow(0).CreateCell(22).SetCellValue("健檢包寄送地點");
            sheet.GetRow(0).CreateCell(23).SetCellValue("餐點樣式");
            sheet.GetRow(0).CreateCell(24).SetCellValue("是否預約飯店");
            sheet.GetRow(0).CreateCell(25).SetCellValue("住宿人名單");


            sheet.GetRow(0).CreateCell(26).SetCellValue("意見/問題回饋");
            sheet.GetRow(0).CreateCell(27).SetCellValue("報名日期時間");
            sheet.GetRow(0).CreateCell(28).SetCellValue("最後修改人員");
            sheet.GetRow(0).CreateCell(29).SetCellValue("最後修改日期時間");



            //設定Header Style
            for (int i = 0; i < sheet.GetRow(0).Cells.Count; i++)
            {
                sheet.GetRow(0).GetCell(i).CellStyle = css1;
            }

            //設定欄位寬度
            sheet.SetColumnWidth(0, (int)((10 + 0.71) * 256));
            sheet.SetColumnWidth(1, (int)((15 + 0.71) * 256));
            sheet.SetColumnWidth(2, (int)((20 + 0.71) * 256));
            sheet.SetColumnWidth(3, (int)((20 + 0.71) * 256));
            sheet.SetColumnWidth(4, (int)((15 + 0.71) * 256));
            sheet.SetColumnWidth(5, (int)((15 + 0.71) * 256));


            sheet.SetColumnWidth(6, (int)((15 + 0.71) * 256));
            sheet.SetColumnWidth(7, (int)((20 + 0.71) * 256));
            sheet.SetColumnWidth(8, (int)((20 + 0.71) * 256));
            sheet.SetColumnWidth(9, (int)((20 + 0.71) * 256));
            sheet.SetColumnWidth(10, (int)((20 + 0.71) * 256));
            sheet.SetColumnWidth(11, (int)((15 + 0.71) * 256));
            sheet.SetColumnWidth(12, (int)((15 + 0.71) * 256));
            sheet.SetColumnWidth(13, (int)((15 + 0.71) * 256));
            sheet.SetColumnWidth(14, (int)((30 + 0.71) * 256));
            sheet.SetColumnWidth(15, (int)((15 + 0.71) * 256));
            sheet.SetColumnWidth(16, (int)((15 + 0.71) * 256));
            sheet.SetColumnWidth(17, (int)((15 + 0.71) * 256));
            sheet.SetColumnWidth(18, (int)((15 + 0.71) * 256));
            sheet.SetColumnWidth(19, (int)((15 + 0.71) * 256));
            sheet.SetColumnWidth(20, (int)((15 + 0.71) * 256));
            sheet.SetColumnWidth(21, (int)((15 + 0.71) * 256));
            sheet.SetColumnWidth(22, (int)((20 + 0.71) * 256));
            sheet.SetColumnWidth(23, (int)((20 + 0.71) * 256));
            sheet.SetColumnWidth(24, (int)((20 + 0.71) * 256));
            sheet.SetColumnWidth(25, (int)((20 + 0.71) * 256));


            sheet.SetColumnWidth(26, (int)((35 + 0.71) * 256));
            sheet.SetColumnWidth(27, (int)((25 + 0.71) * 256));//報名日期時間
            sheet.SetColumnWidth(28, (int)((15 + 0.71) * 256));//最後修改人員
            sheet.SetColumnWidth(29, (int)((25 + 0.71) * 256));//最後修改日期時間



            //填入資料
            Register re = new Register();
            DataTable dt = re.GetExportRegisterModel4Data(eventid, empName);
            int rowIndex = 1;

            foreach (DataRow dr in dt.Rows)
            {
                UserInfo userInfo = new UserInfo(dr["empid"].ToString());

                sheet.CreateRow(rowIndex);
                sheet.GetRow(rowIndex).CreateCell(0).SetCellValue(dr["empid"].ToString());
                sheet.GetRow(rowIndex).CreateCell(1).SetCellValue($"{userInfo.UnitCode}-{userInfo.UnitName}");
                sheet.GetRow(rowIndex).CreateCell(2).SetCellValue($"{userInfo.LastNameCH}{userInfo.FirstNameCH}");
                sheet.GetRow(rowIndex).CreateCell(3).SetCellValue($"{userInfo.FirstNameEN} {userInfo.LastNameEN}");
                sheet.GetRow(rowIndex).CreateCell(4).SetCellValue(userInfo.Station);
                sheet.GetRow(rowIndex).CreateCell(5).SetCellValue(userInfo.HealthGroup);

                sheet.GetRow(rowIndex).CreateCell(6).SetCellValue(dr["examineeidentity"].ToString());
                sheet.GetRow(rowIndex).CreateCell(7).SetCellValue(dr["examineename"].ToString());
                sheet.GetRow(rowIndex).CreateCell(8).SetCellValue(dr["examineename2"].ToString());
                sheet.GetRow(rowIndex).CreateCell(9).SetCellValue(dr["examineeidno"].ToString());
                sheet.GetRow(rowIndex).CreateCell(10).SetCellValue(dr["examineebirthday"].ToString());
                sheet.GetRow(rowIndex).CreateCell(11).SetCellValue(dr["examineemobile"].ToString());
                sheet.GetRow(rowIndex).CreateCell(12).SetCellValue(dr["hosipital"].ToString());
                sheet.GetRow(rowIndex).CreateCell(13).SetCellValue(dr["area"].ToString());
                sheet.GetRow(rowIndex).CreateCell(14).SetCellValue(dr["solution"].ToString());
                sheet.GetRow(rowIndex).CreateCell(15).SetCellValue(dr["gender"].ToString());
                sheet.GetRow(rowIndex).CreateCell(16).SetCellValue(dr["expectdate"].ToString());
                sheet.GetRow(rowIndex).CreateCell(17).SetCellValue(dr["seconddate"].ToString());
                sheet.GetRow(rowIndex).CreateCell(18).SetCellValue(dr["secondsolution1"].ToString());
                sheet.GetRow(rowIndex).CreateCell(19).SetCellValue(dr["secondsolution2"].ToString());
                sheet.GetRow(rowIndex).CreateCell(20).SetCellValue(dr["secondsolution3"].ToString());
                sheet.GetRow(rowIndex).CreateCell(21).SetCellValue(dr["optional"].ToString());
                sheet.GetRow(rowIndex).CreateCell(22).SetCellValue(dr["address"].ToString());
                sheet.GetRow(rowIndex).CreateCell(23).SetCellValue(dr["meal"].ToString());
                sheet.GetRow(rowIndex).CreateCell(24).SetCellValue(dr["needhotel"].ToString());
                sheet.GetRow(rowIndex).CreateCell(25).SetCellValue(dr["checkininfo"].ToString());

                sheet.GetRow(rowIndex).CreateCell(26).SetCellValue(dr["feedback"].ToString());
                sheet.GetRow(rowIndex).CreateCell(27).SetCellValue(dr["registerdate"].ToString());
                sheet.GetRow(rowIndex).CreateCell(28).SetCellValue(dr["modifiedby"].ToString());
                sheet.GetRow(rowIndex).CreateCell(29).SetCellValue(dr["modifieddate"].ToString());


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

        public void ExportRegisterModel5(string eventid, string empName, string page)
        {
            var context = HttpContext.Current;
            context.Response.Clear();
            context.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            //設定檔名
            EventInfo ei = new EventInfo(eventid);
            string fileName = ei.EventName + @"_報名資料.xlsx";

            //建立Excel
            XSSFWorkbook workbook = new XSSFWorkbook();
            XSSFSheet sheet = (XSSFSheet)workbook.CreateSheet("上傳附件類");

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


            sheet.GetRow(0).CreateCell(5).SetCellValue("上傳附件1");
            sheet.GetRow(0).CreateCell(6).SetCellValue("上傳附件1之說明");
            sheet.GetRow(0).CreateCell(7).SetCellValue("上傳附件2");
            sheet.GetRow(0).CreateCell(8).SetCellValue("上傳附件2之說明");
            sheet.GetRow(0).CreateCell(9).SetCellValue("上傳附件3");
            sheet.GetRow(0).CreateCell(10).SetCellValue("上傳附件3之說明");


            sheet.GetRow(0).CreateCell(11).SetCellValue("意見/問題回饋");
            sheet.GetRow(0).CreateCell(12).SetCellValue("報名日期時間");
            sheet.GetRow(0).CreateCell(13).SetCellValue("最後修改人員");
            sheet.GetRow(0).CreateCell(14).SetCellValue("最後修改日期時間");



            //設定Header Style
            for (int i = 0; i < sheet.GetRow(0).Cells.Count; i++)
            {
                sheet.GetRow(0).GetCell(i).CellStyle = css1;
            }

            //設定欄位寬度
            sheet.SetColumnWidth(0, (int)((10 + 0.71) * 256));
            sheet.SetColumnWidth(1, (int)((15 + 0.71) * 256));
            sheet.SetColumnWidth(2, (int)((20 + 0.71) * 256));
            sheet.SetColumnWidth(3, (int)((20 + 0.71) * 256));
            sheet.SetColumnWidth(4, (int)((15 + 0.71) * 256));


            sheet.SetColumnWidth(5, (int)((30 + 0.71) * 256));
            sheet.SetColumnWidth(6, (int)((20 + 0.71) * 256));
            sheet.SetColumnWidth(7, (int)((30 + 0.71) * 256));
            sheet.SetColumnWidth(8, (int)((20 + 0.71) * 256));
            sheet.SetColumnWidth(9, (int)((30 + 0.71) * 256));
            sheet.SetColumnWidth(10, (int)((20 + 0.71) * 256));


            sheet.SetColumnWidth(11, (int)((35 + 0.71) * 256));
            sheet.SetColumnWidth(12, (int)((25 + 0.71) * 256));//報名日期時間
            sheet.SetColumnWidth(13, (int)((15 + 0.71) * 256));//最後修改人員
            sheet.SetColumnWidth(14, (int)((25 + 0.71) * 256));//最後修改日期時間



            //填入資料
            Register re = new Register();
            DataTable dt = re.GetExportRegisterModel5Data(eventid, empName);
            int rowIndex = 1;

            foreach (DataRow dr in dt.Rows)
            {
                UserInfo userInfo = new UserInfo(dr["empid"].ToString());

                sheet.CreateRow(rowIndex);
                sheet.GetRow(rowIndex).CreateCell(0).SetCellValue(dr["empid"].ToString());
                sheet.GetRow(rowIndex).CreateCell(1).SetCellValue($"{userInfo.UnitCode}-{userInfo.UnitName}");
                sheet.GetRow(rowIndex).CreateCell(2).SetCellValue($"{userInfo.LastNameCH}{userInfo.FirstNameCH}");
                sheet.GetRow(rowIndex).CreateCell(3).SetCellValue($"{userInfo.FirstNameEN} {userInfo.LastNameEN}");
                sheet.GetRow(rowIndex).CreateCell(4).SetCellValue(userInfo.Station);



                XSSFHyperlink linkAttachment1 = new XSSFHyperlink(HyperlinkType.Url);
                linkAttachment1.Address = HttpContext.Current.Request.Url.AbsoluteUri.Replace($"/Event/{page}.aspx", $"/Event/EventThumbnail/{dr["attachment1"].ToString()}");
                sheet.GetRow(rowIndex).CreateCell(5).Hyperlink = linkAttachment1;
                sheet.GetRow(rowIndex).CreateCell(5).SetCellValue(dr["attachment1_name"].ToString());
                sheet.GetRow(rowIndex).CreateCell(6).SetCellValue(dr["description1"].ToString());

                XSSFHyperlink linkAttachment2 = new XSSFHyperlink(HyperlinkType.Url);
                linkAttachment2.Address = HttpContext.Current.Request.Url.AbsoluteUri.Replace($"/Event/{page}.aspx", $"/Event/EventThumbnail/{dr["attachment2"].ToString()}");
                sheet.GetRow(rowIndex).CreateCell(7).Hyperlink = linkAttachment2;
                sheet.GetRow(rowIndex).CreateCell(7).SetCellValue(dr["attachment2_name"].ToString());
                sheet.GetRow(rowIndex).CreateCell(8).SetCellValue(dr["description2"].ToString());

                XSSFHyperlink linkAttachment3 = new XSSFHyperlink(HyperlinkType.Url);
                linkAttachment3.Address = HttpContext.Current.Request.Url.AbsoluteUri.Replace($"/Event/{page}.aspx", $"/Event/EventThumbnail/{dr["attachment3"].ToString()}");
                sheet.GetRow(rowIndex).CreateCell(9).Hyperlink = linkAttachment3;
                sheet.GetRow(rowIndex).CreateCell(9).SetCellValue(dr["attachment3_name"].ToString());
                sheet.GetRow(rowIndex).CreateCell(10).SetCellValue(dr["description3"].ToString());


                sheet.GetRow(rowIndex).CreateCell(11).SetCellValue(dr["feedback"].ToString());
                sheet.GetRow(rowIndex).CreateCell(12).SetCellValue(dr["registerdate"].ToString());
                sheet.GetRow(rowIndex).CreateCell(13).SetCellValue(dr["modifiedby"].ToString());
                sheet.GetRow(rowIndex).CreateCell(14).SetCellValue(dr["modifieddate"].ToString());


                //建立儲存格樣式
                ExcelStyle es2 = new ExcelStyle();

                XSSFCellStyle css2 = es.DataStyle(workbook);

                //設定Data Style
                for (int i = 0; i < sheet.GetRow(rowIndex).Cells.Count; i++)
                {
                    if (i != 5 && i != 7 && i != 9)
                        sheet.GetRow(rowIndex).GetCell(i).CellStyle = css2;
                    else
                    {
                        ExcelStyle es11 = new ExcelStyle();
                        XSSFCellStyle linkStyle = es11.DataStyle(workbook);
                        XSSFFont linkFont = (XSSFFont)workbook.CreateFont();
                        linkFont.Underline = FontUnderlineType.Single;
                        linkFont.Color = IndexedColors.Blue.Index;
                        linkStyle.SetFont(linkFont);

                        sheet.GetRow(rowIndex).GetCell(i).CellStyle = linkStyle;
                    }
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

        public void ExportRegisterModel6(string eventid, string empName)
        {
            var context = HttpContext.Current;
            context.Response.Clear();
            context.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            //設定檔名
            EventInfo ei = new EventInfo(eventid);
            string fileName = ei.EventName + @"_報名資料.xlsx";

            //建立Excel
            XSSFWorkbook workbook = new XSSFWorkbook();
            XSSFSheet sheet = (XSSFSheet)workbook.CreateSheet(" IS電腦替換");

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

            sheet.GetRow(0).CreateCell(5).SetCellValue("地點");
            sheet.GetRow(0).CreateCell(6).SetCellValue("日期時間");


            sheet.GetRow(0).CreateCell(7).SetCellValue("意見/問題回饋");
            sheet.GetRow(0).CreateCell(8).SetCellValue("報名日期時間");
            sheet.GetRow(0).CreateCell(9).SetCellValue("最後修改人員");
            sheet.GetRow(0).CreateCell(10).SetCellValue("最後修改日期時間");



            //設定Header Style
            for (int i = 0; i < sheet.GetRow(0).Cells.Count; i++)
            {
                sheet.GetRow(0).GetCell(i).CellStyle = css1;
            }

            //設定欄位寬度
            sheet.SetColumnWidth(0, (int)((10 + 0.71) * 256));
            sheet.SetColumnWidth(1, (int)((15 + 0.71) * 256));
            sheet.SetColumnWidth(2, (int)((20 + 0.71) * 256));
            sheet.SetColumnWidth(3, (int)((20 + 0.71) * 256));
            sheet.SetColumnWidth(4, (int)((15 + 0.71) * 256));


            sheet.SetColumnWidth(5, (int)((20 + 0.71) * 256));
            sheet.SetColumnWidth(6, (int)((25 + 0.71) * 256));


            sheet.SetColumnWidth(7, (int)((35 + 0.71) * 256));
            sheet.SetColumnWidth(8, (int)((25 + 0.71) * 256));//報名日期時間
            sheet.SetColumnWidth(9, (int)((15 + 0.71) * 256));//最後修改人員
            sheet.SetColumnWidth(10, (int)((25 + 0.71) * 256));//最後修改日期時間



            //填入資料
            Register re = new Register();
            DataTable dt = re.GetExportRegisterModel6Data(eventid, empName);
            int rowIndex = 1;

            foreach (DataRow dr in dt.Rows)
            {
                UserInfo userInfo = new UserInfo(dr["empid"].ToString());

                sheet.CreateRow(rowIndex);
                sheet.GetRow(rowIndex).CreateCell(0).SetCellValue(dr["empid"].ToString());
                sheet.GetRow(rowIndex).CreateCell(1).SetCellValue($"{userInfo.UnitCode}-{userInfo.UnitName}");
                sheet.GetRow(rowIndex).CreateCell(2).SetCellValue($"{userInfo.LastNameCH}{userInfo.FirstNameCH}");
                sheet.GetRow(rowIndex).CreateCell(3).SetCellValue($"{userInfo.FirstNameEN} {userInfo.LastNameEN}");
                sheet.GetRow(rowIndex).CreateCell(4).SetCellValue(userInfo.Station);


                sheet.GetRow(rowIndex).CreateCell(5).SetCellValue(dr["changearea"].ToString());
                sheet.GetRow(rowIndex).CreateCell(6).SetCellValue(dr["changedate"].ToString());


                sheet.GetRow(rowIndex).CreateCell(7).SetCellValue(dr["feedback"].ToString());
                sheet.GetRow(rowIndex).CreateCell(8).SetCellValue(dr["registerdate"].ToString());
                sheet.GetRow(rowIndex).CreateCell(9).SetCellValue(dr["modifiedby"].ToString());
                sheet.GetRow(rowIndex).CreateCell(10).SetCellValue(dr["modifieddate"].ToString());


                //建立儲存格樣式
                ExcelStyle es2 = new ExcelStyle();

                XSSFCellStyle css2 = es2.DataStyle(workbook);

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
