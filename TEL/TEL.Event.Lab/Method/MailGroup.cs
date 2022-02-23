using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TEL.Event.Lab.Data;

namespace TEL.Event.Lab.Method
{
    public class MailGroup
    {
        /// <summary>
        /// 郵件群組是否有效
        /// </summary>
        /// <returns></returns>
        public bool IsMailGroupExist(string name)
        {
            MailGroupData mailGroupData = new MailGroupData();
            DataTable dt = mailGroupData.QueryUserMailGroupByMailGroup(name);

            if (dt.Rows.Count > 0)
            {
                return true;
            }
            return false;
        }

        public DataTable GetUserMailGroup(string empid)
        {
            MailGroupData mailGroupData = new MailGroupData();
            DataTable dt = mailGroupData.QueryUserMailGroupByEmpid(empid);

            return dt;
        }
    }
}
