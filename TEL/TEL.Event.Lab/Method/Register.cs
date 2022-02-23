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

        
    }
}
