using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using TEL.Event.Lab.Method;

namespace TEL.Event.Lab.Data
{
    public class SurveyData
    {
        //取得活動問卷填寫資料
        public DataTable QuerySurvey(string eventid, string surveymodel,string empid)
        {
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["tel_event"].ConnectionString;
            string sqlString = "";

            sqlString = @"SELECT a.id AS surveyid, b.EmpID AS empid, b.FullnameCH AS empnamech, b.FullnameEN AS empnameen,
                          CONVERT(VARCHAR, a.fillindate,111) AS fillindate,";

            if (surveymodel=="1")
            {
                sqlString += @"CAST(a.id AS varchar(40))+'_1' AS surveyinfo " +
                    "          FROM TEL_Event_SurveyModel1 a ";
            }
            else if (surveymodel == "2")
            {
                sqlString += @"CAST(a.id AS varchar(40))+'_2' AS surveyinfo " +
                    "          FROM TEL_Event_SurveyModel2 a ";
            }
            else if (surveymodel == "3")
            {
                sqlString += @"CAST(a.id AS varchar(40))+'_3' AS surveyinfo " +
                    "          FROM TEL_Event_SurveyModel3 a ";
            }
            else if (surveymodel == "4")
            {
                sqlString += @"CAST(a.id AS varchar(40))+'_4' AS surveyinfo " +
                    "          FROM TEL_Event_SurveyModel4 a ";
            }

            sqlString += @"INNER JOIN TEL_Event_UserFullName b ON a.empid=b.EmpID
                           WHERE a.eventid=@eventid ";

            if (!string.IsNullOrEmpty(empid))
            {
                sqlString += "AND (b.empid LIKE @empid OR b.FullnameEN LIKE @empid OR b.FullnameCH LIKE @empid) ";
            }

            sqlString += "ORDER BY a.fillindate DESC";

            DataTable result = null;

            using (SqlConnection connection = new SqlConnection(connStr))
            {
                connection.Open();

                SqlDataAdapter wrDad = new SqlDataAdapter();
                DataSet DS = new DataSet();

                wrDad.SelectCommand = new SqlCommand(sqlString, connection);
                wrDad.SelectCommand.Parameters.AddWithValue("@eventid", eventid);

                if (!string.IsNullOrEmpty(empid))
                {
                    wrDad.SelectCommand.Parameters.AddWithValue("@empid", '%' + empid + '%');
                }

                wrDad.Fill(DS, "T");
                result = DS.Tables["T"];
            }

            return result;
        }

        //取得問卷填寫人數
        public String QuerySurveyFillinCount(string eventid, string surveymodel)
        {
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["tel_event"].ConnectionString;
            string sqlString = "";

            if (surveymodel == "1")
            {
                sqlString = @"SELECT COUNT(empid) AS count FROM TEL_Event_SurveyModel1 ";
            }
            else if (surveymodel == "2")
            {
                sqlString = @"SELECT COUNT(empid) AS count FROM TEL_Event_SurveyModel2 ";
            }
            else if (surveymodel == "3")
            {
                sqlString = @"SELECT COUNT(empid) AS count FROM TEL_Event_SurveyModel3 ";
            }
            else if (surveymodel == "4")
            {
                sqlString = @"SELECT COUNT(empid) AS count FROM TEL_Event_SurveyModel4 ";
            }

            sqlString += @"WHERE eventid=@eventid ";

            DataTable result = null;

            using (SqlConnection connection = new SqlConnection(connStr))
            {
                connection.Open();

                SqlDataAdapter wrDad = new SqlDataAdapter();
                DataSet DS = new DataSet();

                wrDad.SelectCommand = new SqlCommand(sqlString, connection);
                wrDad.SelectCommand.Parameters.AddWithValue("@eventid", eventid);

                wrDad.Fill(DS, "T");
                result = DS.Tables["T"];
            }

            return result.Rows[0]["count"].ToString();
        }

        //刪除問卷填寫資料
        public void  DeleteSurveyData(string surveyid, string surveymodel)
        {
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["tel_event"].ConnectionString;
            string sqlString = "";
            
            if (surveymodel == "1")
            {
                sqlString = @"DELETE TEL_Event_SurveyModel1 WHERE id=@surveyid";
            }
            else if (surveymodel == "2")
            {
                sqlString = @"DELETE TEL_Event_SurveyModel2 WHERE id=@surveyid";
            }
            else if (surveymodel == "3")
            {
                sqlString = @"DELETE TEL_Event_SurveyModel2 WHERE id=@surveyid";
            }
            else if (surveymodel == "4")
            {
                sqlString = @"DELETE TEL_Event_SurveyModel4 WHERE id=@surveyid";
            }

            SqlConnection sqlConn = new SqlConnection(connStr);
            sqlConn.Open();
            SqlCommand sqlcommand = new SqlCommand(sqlString, sqlConn);
            sqlcommand.Parameters.Clear();
            sqlcommand.Parameters.AddWithValue("@surveyid", surveyid);
            sqlcommand.ExecuteNonQuery();
            sqlConn.Close();
            sqlConn.Dispose();
        }
    }
}
