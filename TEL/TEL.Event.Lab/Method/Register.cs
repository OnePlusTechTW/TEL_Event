using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEL.Event.Lab.Method
{
    public class Register
    {
        /// <summary>
        /// 取得報名表單資料
        /// </summary>
        /// <param name="eventid"></param>
        /// <param name="eventRegisterModel"></param>
        /// <param name="v"></param>
        /// <returns></returns>
        public DataTable GetRegister(string eventid, string eventRegisterModel, string empName)
        {
            RegisterData re = new RegisterData();
            DataTable dt = new DataTable();
            dt = re.QueryRegister(eventid, eventRegisterModel, empName);
            return dt;
        }

        /// <summary>
        /// 取得 RegisterModel1 excel 匯出資料
        /// </summary>
        /// <param name="eventid"></param>
        /// <param name="empName"></param>
        /// <returns></returns>
        public DataTable GetExportRegisterModel1Data(string eventid, string empName)
        {
            RegisterData re = new RegisterData();
            DataTable dt = new DataTable();
            dt = re.QueryExportRegisterModel1Data(eventid, empName);
            return dt;
        }

        /// <summary>
        /// 取得 RegisterModel2 excel 匯出資料
        /// </summary>
        /// <param name="eventid"></param>
        /// <param name="empName"></param>
        /// <returns></returns>
        internal DataTable GetExportRegisterModel2Data(string eventid, string empName)
        {
            RegisterData re = new RegisterData();
            DataTable dt = new DataTable();
            dt = re.QueryExportRegisterModel2Data(eventid, empName);
            return dt;
        }

        /// <summary>
        /// 取得 RegisterModel2family excel 匯出資料
        /// </summary>
        /// <param name="eventid"></param>
        /// <param name="empName"></param>
        /// <returns></returns>
        internal DataTable GetExportRegisterModel2FamilyData(string eventid, string empName)
        {
            RegisterData re = new RegisterData();
            DataTable dt = new DataTable();
            dt = re.QueryExportRegisterModel2FamilyData(eventid, empName);
            return dt;
        }

        /// <summary>
        /// 取得 RegisterModel3family excel 匯出資料
        /// </summary>
        /// <param name="eventid"></param>
        /// <param name="empName"></param>
        /// <returns></returns>
        internal DataTable GetExportRegisterModel3Data(string eventid, string empName)
        {
            RegisterData re = new RegisterData();
            DataTable dt = new DataTable();
            dt = re.QueryExportRegisterModel3Data(eventid, empName);
            return dt;
        }

        /// <summary>
        /// 取得 RegisterModel4family excel 匯出資料
        /// </summary>
        /// <param name="eventid"></param>
        /// <param name="empName"></param>
        /// <returns></returns>
        internal DataTable GetExportRegisterModel4Data(string eventid, string empName)
        {
            RegisterData re = new RegisterData();
            DataTable dt = new DataTable();
            dt = re.QueryExportRegisterModel4Data(eventid, empName);
            return dt;
        }

        /// <summary>
        /// 取得 RegisterModel5family excel 匯出資料
        /// </summary>
        /// <param name="eventid"></param>
        /// <param name="empName"></param>
        /// <returns></returns>
        internal DataTable GetExportRegisterModel5Data(string eventid, string empName)
        {
            RegisterData re = new RegisterData();
            DataTable dt = new DataTable();
            dt = re.QueryExportRegisterModel5Data(eventid, empName);
            return dt;
        }

        /// <summary>
        /// 取得 RegisterModel6family excel 匯出資料
        /// </summary>
        /// <param name="eventid"></param>
        /// <param name="empName"></param>
        /// <returns></returns>
        internal DataTable GetExportRegisterModel6Data(string eventid, string empName)
        {
            RegisterData re = new RegisterData();
            DataTable dt = new DataTable();
            dt = re.QueryExportRegisterModel6Data(eventid, empName);
            return dt;
        }
    }
}
