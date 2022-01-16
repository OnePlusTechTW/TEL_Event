using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.HSSF.Util;

namespace TEL.Event.Lab.Method
{
    class ExcelStyle
    {
        public XSSFCellStyle HeaderStyle(IWorkbook wb)
        {
            XSSFCellStyle style = (XSSFCellStyle)wb.CreateCellStyle();

            style.Alignment = HorizontalAlignment.Center;
            style.VerticalAlignment = VerticalAlignment.Center;
            style.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Grey25Percent.Index;
            style.FillPattern = FillPattern.SolidForeground;
            style.WrapText = true;
            IFont font = wb.CreateFont();
            font.FontHeightInPoints = 12;
            font.IsBold = true;
            font.FontName = "Microsoft JhengHei, Georgia";
            style.SetFont(font);

            return style;
        }

        public XSSFCellStyle DataStyle(IWorkbook wb)
        {
            XSSFCellStyle style = (XSSFCellStyle)wb.CreateCellStyle();

            style.WrapText = true;
            IFont font = wb.CreateFont();
            font.FontHeightInPoints = 12;
            font.FontName = "Microsoft JhengHei, Georgia";
            style.SetFont(font);

            return style;
        }
    }
}
