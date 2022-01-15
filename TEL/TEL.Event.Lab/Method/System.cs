using System;
using System.Configuration;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TEL.Event.Lab.Data;

namespace TEL.Event.Lab.Method
{
    public class System
    {
        //判斷使用者為系統管理者、常態活動管理者、其他活動管理者、或是一般使用者
        // i=3 => 使用者是系統管理者
        // i=2 => 使用者是常態活動管理者
        // i=1 => 使用者是其他活動管理者
        // i=0 => 使用者是一般使用者
        public int IsManager(string empid)
        {
            int i = 0;
            SystemData userinfo = new SystemData();

            // 系統管理者
            if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings.Get("SiteManager")) && ConfigurationManager.AppSettings.Get("SiteManager").IndexOf(empid) >= 0)
            {
                i = 3;
            }
            // 常態活動管理者
            else if (userinfo.QueryEventManagerTable(empid) > 0)
            {
                i = 2;
            }
            // 其他活動管理者
            else if (userinfo.QueryOtherEventManagerTable(empid) > 0)
            {
                i = 1;
            }

            return i;
        }

        //判斷使用者是否須被導到Denied頁面
        public int IsDenied(string empid,string eventid)
        {
            int i = 0;
            SystemData userinfo = new SystemData();

            // 系統管理者
            if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings.Get("SiteManager")) && ConfigurationManager.AppSettings.Get("SiteManager").IndexOf(empid) >= 0)
            {
                i = 3;
            }
            // 常態活動管理者
            else if (userinfo.QueryEventManagerTable(empid) > 0)
            {
                i = 2;
            }
            // 其他活動管理者
            else if (userinfo.QueryOtherEventManager(empid,eventid) > 0)
            {
                i = 1;
            }

            return i;
        }
    }
}
