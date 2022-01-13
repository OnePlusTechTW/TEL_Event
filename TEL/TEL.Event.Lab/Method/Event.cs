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
        public DataTable GetEventCategory(string flag)
        {
            SystemData ss = new SystemData();
            return ss.QueryEventCategory(flag);
        }

        public DataTable GetMyEvent(string name, string catid, string status, string empid)
        {
            EventData ev = new EventData();
            return ev.QueryMyEvent(name, catid, status, empid);
        }
    }
}
