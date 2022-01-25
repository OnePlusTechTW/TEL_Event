using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TEL.Event.Lab.Data;
using System.Data.SqlClient;

namespace TEL.Event.Lab.Method
{
    class SqlLog
    {
        //新增使用者新增、編輯、刪除活動報名或問卷的SQL LOG
        public String AddLog(string description, string relatedid, string sqlstr, string editor)
        {
            Log lg = new Log();

            return lg.InsertLog(description, relatedid, sqlstr, editor);
        }

        public String GetCommendText(SqlCommand cmd)
        {
            string sqlstr = cmd.CommandText;

            foreach (SqlParameter parms in cmd.Parameters)
            {
               sqlstr = sqlstr.Replace(parms.ParameterName, parms.Value.ToString());
            }

            return sqlstr;
        }
    }
}
