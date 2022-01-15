using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace TEL.Event.Lab.Data
{
    public class SystemData
    {
        //取得活動分類資料
        //若在查詢條件，須傳入參數"All"，才會取得所有的活動分類
        //若在活動新增或編輯頁面的輸入欄位，則不傳入參數，才會取得啟用的活動分類
        public DataTable QueryEventCategory(string flag)
        {
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["tel_event"].ConnectionString;
            string sqlString = "";

            if (flag == "All")
                sqlString = @"SELECT id,name FROM TEL_Event_Category ORDER BY name";
            else
                sqlString = @"SELECT id,name FROM TEL_Event_Category WHERE enabled='Y' ORDER BY name";

            DataTable result = null;

            using (SqlConnection connection = new SqlConnection(connStr))
            {
                connection.Open();

                SqlDataAdapter wrDad = new SqlDataAdapter();
                DataSet DS = new DataSet();

                wrDad.SelectCommand = new SqlCommand(sqlString, connection);

                wrDad.Fill(DS, "T");
                result = DS.Tables["T"];
            }

            return result;
        }

        //取得使用者是否為常態活動管理者
        internal int QueryEventManagerTable(string empID)
        {
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["tel_event"].ConnectionString;
            string sqlString = $@"SELECT * FROM TEL_Event_Admin WITH(NOLOCK) WHERE EmpID =@empid";

            DataTable result = null;

            using (SqlConnection connection = new SqlConnection(connStr))
            {
                connection.Open();

                SqlDataAdapter wrDad = new SqlDataAdapter();
                DataSet DS = new DataSet();

                wrDad.SelectCommand = new SqlCommand(sqlString, connection);
                wrDad.SelectCommand.Parameters.AddWithValue("@empid", empID);
                wrDad.Fill(DS, "T");
                result = DS.Tables["T"];
            }

            return result.Rows.Count;
        }

        //取得使用者是否為其他活動管理者(Master Page連結用)
        internal int QueryOtherEventManagerTable(string empID)
        {
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["tel_event"].ConnectionString;
            string sqlString = $@"SELECT * FROM TEL_Event_EventAdmin WITH(NOLOCK) WHERE EmpID =@empid";

            DataTable result = null;

            using (SqlConnection connection = new SqlConnection(connStr))
            {
                connection.Open();

                SqlDataAdapter wrDad = new SqlDataAdapter();
                DataSet DS = new DataSet();

                wrDad.SelectCommand = new SqlCommand(sqlString, connection);
                wrDad.SelectCommand.Parameters.AddWithValue("@empid", empID);
                wrDad.Fill(DS, "T");
                result = DS.Tables["T"];
            }

            return result.Rows.Count;
        }

        //取得使用者是否為其他活動管理者(頁面權限判斷用)
        internal int QueryOtherEventManager(string empID,string eventid)
        {
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["tel_event"].ConnectionString;
            string sqlString = $@"SELECT * FROM TEL_Event_EventAdmin WITH(NOLOCK) WHERE eventid=@eventid AND empid=@empid ";

            DataTable result = null;

            using (SqlConnection connection = new SqlConnection(connStr))
            {
                connection.Open();

                SqlDataAdapter wrDad = new SqlDataAdapter();
                DataSet DS = new DataSet();

                wrDad.SelectCommand = new SqlCommand(sqlString, connection);
                wrDad.SelectCommand.Parameters.AddWithValue("@eventid", eventid);
                wrDad.SelectCommand.Parameters.AddWithValue("@empid", empID);
                wrDad.Fill(DS, "T");
                result = DS.Tables["T"];
            }

            return result.Rows.Count;
        }
    }
}
