using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEL.Event.Lab
{
    public class RegisterData
    {
        private string GetConnectionString()
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings["tel_event"].ConnectionString;
        }

        /// <summary>
        /// 取得報名表單資料
        /// </summary>
        /// <param name="eventid"></param>
        /// <param name="eventRegisterModel"></param>
        /// <param name="empName"></param>
        /// <returns></returns>
        internal DataTable QueryRegister(string eventid, string eventRegisterModel, string empName)
        {
            string connStr = GetConnectionString();
            string sqlString = string.Empty;

            sqlString = @"
                        SELECT
                            [id] as registerid
                            ,[eventid]
                            ,a.[empid]
                            ,b.FullnameCH as empnamech
                            ,b.FullnameEN as empnameen
                            ,CONVERT(VARCHAR, a.[registerdate],20) AS [registerdate]
                            ,[modifiedby]
                            ,CONVERT(VARCHAR, a.[modifieddate],20) AS [modifieddate] ";

            if (eventRegisterModel == "1")
            {
                sqlString += @" 
                        FROM [TEL_Event_RegisterModel1] a ";
            }
            else if (eventRegisterModel == "2")
            {
                sqlString += @" 
                        FROM [TEL_Event_RegisterModel2] a ";
            }
            else if (eventRegisterModel == "3")
            {
                sqlString += @" 
                        FROM [TEL_Event_RegisterModel3] a ";
            }
            if (eventRegisterModel == "4")
            {
                sqlString += @" 
                        FROM [TEL_Event_RegisterModel4] a ";
            }
            else if (eventRegisterModel == "5")
            {
                sqlString += @" 
                        FROM [TEL_Event_RegisterModel5] a ";
            }
            else if (eventRegisterModel == "6")
            {
                sqlString += @" 
                        FROM [TEL_Event_RegisterModel6] a ";
            }

            sqlString += @"
                        INNER JOIN TEL_Event_UserFullName b ON a.empid=b.EmpID 
                        WHERE
                            a.eventid=@eventid ";

            if (!string.IsNullOrEmpty(empName))
            {
                sqlString += "" +
                    "   AND (b.empid LIKE @empid OR b.FullnameEN LIKE @empid OR b.FullnameCH LIKE @empid) ";
            }

            sqlString += "" +
                "       ORDER BY a.[registerdate] ASC";

            DataTable result = null;

            using (SqlConnection connection = new SqlConnection(connStr))
            {
                connection.Open();

                SqlDataAdapter wrDad = new SqlDataAdapter();
                DataSet DS = new DataSet();

                wrDad.SelectCommand = new SqlCommand(sqlString, connection);
                wrDad.SelectCommand.Parameters.AddWithValue("@eventid", eventid);

                if (!string.IsNullOrEmpty(empName))
                {
                    wrDad.SelectCommand.Parameters.AddWithValue("@empid", '%' + empName + '%');
                }

                wrDad.Fill(DS, "T");
                result = DS.Tables["T"];
            }

            return result;
        }

        

        
    }
}
