using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using TEL.Event.Lab.Data;

namespace TEL.Event.Lab.Method
{
    public class Event
    {
        //查詢我的活動
        public DataTable GetMyEvent(string name, string catid, string status, string empid)
        {
            EventData ev = new EventData();
            return ev.QueryMyEvent(name, catid, status, empid);
        }


        //Load健檢中心資料
        public DataTable GetHosipital(string eventid)
        {
            EventData ev = new EventData();
            return ev.QueryHosipitalData(eventid);
        }
    }
}
