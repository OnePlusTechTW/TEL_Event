using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace TEL.Event.Lab.Method
{
    class CheckIDFormat
    {
        //驗證身分證號格式
        public bool CheckIdno(string idno)
        {
            var format = new Regex(@"^[A-Z]\d{9}$");

            if (!format.IsMatch(idno)) 
                return false;

            idno = idno.ToUpper();

            var a = new[] {10, 11, 12, 13, 14, 15, 16, 17, 34, 18, 19, 20, 21, 22, 35, 23, 24, 25, 26, 27, 28, 29, 32, 30, 31, 33};

            var b = new int[11];

            b[1] = a[(idno[0]) - 65] % 10;

            var c = b[0] = a[(idno[0]) - 65] / 10;

            for (var i = 1; i <= 9; i++)
            {
                b[i + 1] = idno[i] - 48;
                c += b[i] * (10 - i);
            }

            return ((c % 10) + b[10]) % 10 == 0;
        }

        //驗證居留證號格式
        public static bool CheckForeignIdno(string idno)
        {
            // 基礎檢查 「任意1個字母」+「A~D其中一個字母」+「8個數字」
            if (!Regex.IsMatch(idno, @"^[A-Za-z][A-Da-d]\d{8}$")) return false;
            idno = idno.ToUpper();

            // 縣市區域碼
            var cityCode = new[] { 10, 11, 12, 13, 14, 15, 16, 17, 34, 18, 19, 20, 21, 22, 35, 23, 24, 25, 26, 27, 28, 29, 32, 30, 31, 33 };
            // 計算時使用的容器，最後一個位置拿來放檢查碼，所以有11個位置(縣市區碼佔2個位置)
            var valueContainer = new int[11];
            valueContainer[0] = cityCode[idno[0] - 65] / 10; // 區域碼十位數
            valueContainer[1] = cityCode[idno[0] - 65] % 10; // 區域碼個位數
            valueContainer[2] = cityCode[idno[1] - 65] % 10; // 性別碼個位數

            // 證號執行特定數規則所產生的結果值的加總，這裡把初始值訂為區域碼的十位數數字(特定數為1，所以不用乘)
            var sumVal = valueContainer[0];

            // 迴圈執行特定數規則
            for (var i = 1; i <= 9; i++)
            {
                // 跳過性別碼，如果是一般身分證字號則不用跳過
                if (i > 1)
                    // 將當前證號於索引位置的數字放到容器的下一個索引的位置
                    valueContainer[i + 1] = idno[i] - 48;

                // 特定數為: 1987654321 ，因為首個數字1已經在sumVal初始值算過了，所以這裡從9開始
                sumVal += valueContainer[i] * (10 - i);
            }

            // 此為「檢查碼 = 10 - 總和值的個位數數字 ; 若個位數為0則取0為檢查碼」的反推
            return (sumVal + valueContainer[10]) % 10 == 0;
        }
    }
}
