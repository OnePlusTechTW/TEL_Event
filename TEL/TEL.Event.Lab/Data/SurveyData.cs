using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using TEL.Event.Lab.Method;
using System.Web;

namespace TEL.Event.Lab.Data
{
    public class SurveyData
    {
        //取得活動問卷資料
        public DataTable QuerySurvey(string eventid, string surveymodel, string empid)
        {
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["tel_event"].ConnectionString;
            string sqlString = "";

            sqlString = @"SELECT a.id AS surveyid, b.EmpID AS empid, b.FullnameCH AS empnamech, b.FullnameEN AS empnameen,
                          CONVERT(VARCHAR, a.fillindate,111) AS fillindate,";

            if (surveymodel == "1")
            {
                sqlString += @"CAST(a.id AS varchar(40))+'_1'AS surveyinfo " +
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

            sqlString += "ORDER BY a.fillindate ASC";

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

        //取得 Model1 Export to Excel資料
        public DataTable QueryExportModel1Data(string eventid)
        {
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["tel_event"].ConnectionString;
            string sqlString = "";

            sqlString = @"SELECT a.empid, b.UnitName,b.LastNameCH+b.FirstNameCH AS empfullnamech,b.FirstNameEN+' '+b.LastNameEN AS empfullnameen,b.Station,
                          a.q1,a.q2,a.q3,a.q4,a.q5,a.q6,a.q7,
                          a.q7reason,a.q8,a.q9,a.q10,CONVERT(VARCHAR, a.fillindate,111) AS fillindate 
                          FROM TEL_Event_SurveyModel1 a
                          INNER JOIN Users b ON a.empid=b.empid 
                          WHERE a.eventid=@eventid 
                          ORDER BY a.fillindate DESC";

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

            return result;
        }

        //取得 Model2 Export to Excel資料
        public DataTable QueryExportModel2Data(string eventid)
        {
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["tel_event"].ConnectionString;
            string sqlString = "";

            sqlString = @"SELECT a.empid, b.UnitName,b.LastNameCH+b.FirstNameCH AS empfullnamech,b.FirstNameEN+' '+b.LastNameEN AS empfullnameen,b.Station,
                          a.q1,a.q2,a.q3,a.q4,a.q5,a.q6,a.q7,
                          a.q8,CONVERT(VARCHAR, a.fillindate,111) AS fillindate 
                          FROM TEL_Event_SurveyModel2 a
                          INNER JOIN Users b ON a.empid=b.empid 
                          WHERE a.eventid=@eventid 
                          ORDER BY a.fillindate DESC";

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

            return result;
        }

        //取得 Model3 Export to Excel資料
        public DataTable QueryExportModel3Data(string eventid)
        {
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["tel_event"].ConnectionString;
            string sqlString = "";

            sqlString = @"SELECT a.empid, b.UnitName,b.LastNameCH+b.FirstNameCH AS empfullnamech,b.FirstNameEN+' '+b.LastNameEN AS empfullnameen,b.Station,
                          a.q1,a.q2,a.q2reason,a.q3,a.q3reason,a.q4,a.q4reason,a.q5,a.q5reason,a.q6,a.q6reason,a.q7,a.q7reason,a.q8,a.q9,
                          CONVERT(VARCHAR, a.fillindate,111) AS fillindate 
                          FROM TEL_Event_SurveyModel3 a
                          INNER JOIN Users b ON a.empid=b.empid 
                          WHERE a.eventid=@eventid 
                          ORDER BY a.fillindate DESC";

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

            return result;
        }

        //取得 Model4 Export to Excel資料
        public DataTable QueryExportModel4Data(string eventid)
        {
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["tel_event"].ConnectionString;
            string sqlString = "";

            sqlString = @"SELECT a.empid, b.UnitName,b.LastNameCH+b.FirstNameCH AS empfullnamech,b.FirstNameEN+' '+b.LastNameEN AS empfullnameen,b.Station,
                          a.q1,a.q2,a.q3,a.q4,a.q5,
                          CONVERT(VARCHAR, a.fillindate,111) AS fillindate 
                          FROM TEL_Event_SurveyModel4 a
                          INNER JOIN Users b ON a.empid=b.empid
                          WHERE a.eventid=@eventid 
                          ORDER BY a.fillindate DESC";

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

            return result;
        }

        //刪除問卷填寫資料
        public String DeleteSurveyData(string eventid, string surveyid, string surveymodel, string empid)
        {
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["tel_event"].ConnectionString;
            string sqlString = "";

            try
            {           
                // 刪除問卷資料 
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

                //Add log
                SqlLog sl = new SqlLog();
                sl.AddLog("刪除使用者問卷填寫資料", eventid, sl.GetCommendText(sqlcommand), empid);
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }

            return "";
        }

        //儲存模板1問卷資料(滿意度(講座))
        public String SaveEventDataMModel1(string eventid, string empid, string q1, string q1other, string q2, string q3, string q4, string q5, string q6, string q7, string q7reason, string q8, string q9, string q10)
        {
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["tel_event"].ConnectionString;
            string sqlString = "";

            try
            {
                sqlString = @"INSERT INTO TEL_Event_SurveyModel1 (id,eventid,empid,fillindate,q1,q1other,q2,q3,q4,q5,q6,q7,q7reason,q8,q9,q10,modifiedby,modifieddate)
                              VALUES (newid(),@P01,@P02,@P03,@P04,@P05,@P06,@P07,@P08,@P09,@P10,@P11,@P12,@P13,@P14,@P15,@P16,@P17)";

                SqlConnection sqlConn = new SqlConnection(connStr);
                sqlConn.Open();
                SqlCommand sqlcommand = new SqlCommand(sqlString, sqlConn);
                sqlcommand.Parameters.Clear();
                sqlcommand.Parameters.AddWithValue("@P01", eventid);
                sqlcommand.Parameters.AddWithValue("@P02", empid);
                sqlcommand.Parameters.AddWithValue("@P03", System.DateTime.Now);
                sqlcommand.Parameters.AddWithValue("@P04", q1);
                if (!string.IsNullOrEmpty(q1other))
                    sqlcommand.Parameters.AddWithValue("@P05", q1other);
                else
                    sqlcommand.Parameters.AddWithValue("@P05", System.DBNull.Value);
                sqlcommand.Parameters.AddWithValue("@P06", q2);
                sqlcommand.Parameters.AddWithValue("@P07", q3);
                sqlcommand.Parameters.AddWithValue("@P08", q4);
                sqlcommand.Parameters.AddWithValue("@P09", q5);
                sqlcommand.Parameters.AddWithValue("@P10", q6);
                sqlcommand.Parameters.AddWithValue("@P11", q7);
                if (!string.IsNullOrEmpty(q7reason))
                    sqlcommand.Parameters.AddWithValue("@P12", q7reason);
                else
                    sqlcommand.Parameters.AddWithValue("@P12", System.DBNull.Value);
                sqlcommand.Parameters.AddWithValue("@P13", q8);
                if (!string.IsNullOrEmpty(q9))
                    sqlcommand.Parameters.AddWithValue("@P14", q9);
                else
                    sqlcommand.Parameters.AddWithValue("@P14", System.DBNull.Value);
                if (!string.IsNullOrEmpty(q10))
                    sqlcommand.Parameters.AddWithValue("@P15", q10);
                else
                    sqlcommand.Parameters.AddWithValue("@P15", System.DBNull.Value);
                sqlcommand.Parameters.AddWithValue("@P16", empid);
                sqlcommand.Parameters.AddWithValue("@P17", System.DateTime.Now);

                sqlcommand.ExecuteNonQuery();
                sqlConn.Close();
                sqlConn.Dispose();

                //Add log
                SqlLog sl = new SqlLog();
                sl.AddLog("新增使用者問卷填寫資料", eventid, sl.GetCommendText(sqlcommand), empid);
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }

            return "";
        }

        //儲存模板2問卷資料(滿意度(活動))
        public String SaveEventDataMModel2(string eventid, string empid, string q1, string q1other, string q2, string q3, string q4, string q5, string q6, string q7, string q8)
        {
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["tel_event"].ConnectionString;
            string sqlString = "";

            try
            {
                sqlString = @"INSERT INTO TEL_Event_SurveyModel2 (id,eventid,empid,fillindate,q1,q1other,q2,q3,q4,q5,q6,q7,q8,modifiedby,modifieddate)
                              VALUES (newid(),@P01,@P02,@P03,@P04,@P05,@P06,@P07,@P08,@P09,@P10,@P11,@P12,@P13,@P14)";

                SqlConnection sqlConn = new SqlConnection(connStr);
                sqlConn.Open();
                SqlCommand sqlcommand = new SqlCommand(sqlString, sqlConn);
                sqlcommand.Parameters.Clear();
                sqlcommand.Parameters.AddWithValue("@P01", eventid);
                sqlcommand.Parameters.AddWithValue("@P02", empid);
                sqlcommand.Parameters.AddWithValue("@P03", System.DateTime.Now);
                sqlcommand.Parameters.AddWithValue("@P04", q1);
                if (!string.IsNullOrEmpty(q1other))
                    sqlcommand.Parameters.AddWithValue("@P05", q1other);
                else
                    sqlcommand.Parameters.AddWithValue("@P05", System.DBNull.Value);
                sqlcommand.Parameters.AddWithValue("@P06", q2);
                sqlcommand.Parameters.AddWithValue("@P07", q3);
                sqlcommand.Parameters.AddWithValue("@P08", q4);
                sqlcommand.Parameters.AddWithValue("@P09", q5);
                sqlcommand.Parameters.AddWithValue("@P10", q6);
                if (!string.IsNullOrEmpty(q7))
                    sqlcommand.Parameters.AddWithValue("@P11", q7);
                else
                    sqlcommand.Parameters.AddWithValue("@P11", System.DBNull.Value);
                if (!string.IsNullOrEmpty(q8))
                    sqlcommand.Parameters.AddWithValue("@P12", q8);
                else
                    sqlcommand.Parameters.AddWithValue("@P12", System.DBNull.Value);
                sqlcommand.Parameters.AddWithValue("@P13", empid);
                sqlcommand.Parameters.AddWithValue("@P14", System.DateTime.Now);

                sqlcommand.ExecuteNonQuery();
                sqlConn.Close();
                sqlConn.Dispose();

                //Add log
                SqlLog sl = new SqlLog();
                sl.AddLog("新增使用者問卷填寫資料", eventid, sl.GetCommendText(sqlcommand), empid);
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }

            return "";
        }

        //儲存模板2問卷資料(滿意度(健檢))
        public String SaveEventDataMModel3(string eventid, string empid, string q1, string q2, string q2reason, string q3, string q3reason, string q4, string q4reason, string q5, string q5reason, string q6, string q6reason, string q7, string q7reason, string q8, string q9)
        {
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["tel_event"].ConnectionString;
            string sqlString = "";

            try
            {
                sqlString = @"INSERT INTO TEL_Event_SurveyModel3 (id,eventid,empid,fillindate,q1,q2,q2reason,q3,q3reason,q4,q4reason,q5,q5reason,q6,q6reason,q7,q7reason,q8,q9,modifiedby,modifieddate)
                              VALUES (newid(),@P01,@P02,@P03,@P04,@P05,@P06,@P07,@P08,@P09,@P10,@P11,@P12,@P13,@P14,@P15,@P16,@P17,@P18,@P19,@P20)";

                SqlConnection sqlConn = new SqlConnection(connStr);
                sqlConn.Open();
                SqlCommand sqlcommand = new SqlCommand(sqlString, sqlConn);
                sqlcommand.Parameters.Clear();
                sqlcommand.Parameters.AddWithValue("@P01", eventid);
                sqlcommand.Parameters.AddWithValue("@P02", empid);
                sqlcommand.Parameters.AddWithValue("@P03", System.DateTime.Now);
                sqlcommand.Parameters.AddWithValue("@P04", q1);
                sqlcommand.Parameters.AddWithValue("@P05", q2);
                if (!string.IsNullOrEmpty(q2reason))
                    sqlcommand.Parameters.AddWithValue("@P06", q2reason);
                else
                    sqlcommand.Parameters.AddWithValue("@P06", System.DBNull.Value);

                sqlcommand.Parameters.AddWithValue("@P07", q3);
                if (!string.IsNullOrEmpty(q3reason))
                    sqlcommand.Parameters.AddWithValue("@P08", q3reason);
                else
                    sqlcommand.Parameters.AddWithValue("@P08", System.DBNull.Value);

                sqlcommand.Parameters.AddWithValue("@P09", q4);
                if (!string.IsNullOrEmpty(q4reason))
                    sqlcommand.Parameters.AddWithValue("@P10", q4reason);
                else
                    sqlcommand.Parameters.AddWithValue("@P10", System.DBNull.Value);

                sqlcommand.Parameters.AddWithValue("@P11", q5);
                if (!string.IsNullOrEmpty(q5reason))
                    sqlcommand.Parameters.AddWithValue("@P12", q5reason);
                else
                    sqlcommand.Parameters.AddWithValue("@P12", System.DBNull.Value);

                sqlcommand.Parameters.AddWithValue("@P13", q6);
                if (!string.IsNullOrEmpty(q6reason))
                    sqlcommand.Parameters.AddWithValue("@P14", q6reason);
                else
                    sqlcommand.Parameters.AddWithValue("@P14", System.DBNull.Value);

                sqlcommand.Parameters.AddWithValue("@P15", q7);
                if (!string.IsNullOrEmpty(q7reason))
                    sqlcommand.Parameters.AddWithValue("@P16", q7reason);
                else
                    sqlcommand.Parameters.AddWithValue("@P16", System.DBNull.Value);

                if (!string.IsNullOrEmpty(q8))
                    sqlcommand.Parameters.AddWithValue("@P17", q8);
                else
                    sqlcommand.Parameters.AddWithValue("@P17", System.DBNull.Value);

                sqlcommand.Parameters.AddWithValue("@P18", q9);
                sqlcommand.Parameters.AddWithValue("@P19", empid);
                sqlcommand.Parameters.AddWithValue("@P20", System.DateTime.Now);

                sqlcommand.ExecuteNonQuery();
                sqlConn.Close();
                sqlConn.Dispose();

                //Add log
                SqlLog sl = new SqlLog();
                sl.AddLog("新增使用者問卷填寫資料", eventid, sl.GetCommendText(sqlcommand), empid);
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }

            return "";
        }

        //儲存模板4問卷資料(滿意度(電腦替換))
        public String SaveEventDataMModel4(string eventid, string empid, string q1, string q2, string q3, string q4, string q5)
        {
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["tel_event"].ConnectionString;
            string sqlString = "";

            try
            {
                sqlString = @"INSERT INTO TEL_Event_SurveyModel4 (id,eventid,empid,fillindate,q1,q2,q3,q4,q5,modifiedby,modifieddate)
                              VALUES (newid(),@P01,@P02,@P03,@P04,@P05,@P06,@P07,@P08,@P09,@P10)";

                SqlConnection sqlConn = new SqlConnection(connStr);
                sqlConn.Open();
                SqlCommand sqlcommand = new SqlCommand(sqlString, sqlConn);
                sqlcommand.Parameters.Clear();
                sqlcommand.Parameters.AddWithValue("@P01", eventid);
                sqlcommand.Parameters.AddWithValue("@P02", empid);
                sqlcommand.Parameters.AddWithValue("@P03", System.DateTime.Now);
                sqlcommand.Parameters.AddWithValue("@P04", q1);
                sqlcommand.Parameters.AddWithValue("@P05", q2);
                sqlcommand.Parameters.AddWithValue("@P06", q3);
                sqlcommand.Parameters.AddWithValue("@P07", q4);
                if (!string.IsNullOrEmpty(q5))
                    sqlcommand.Parameters.AddWithValue("@P08", q5);
                else
                    sqlcommand.Parameters.AddWithValue("@P08", System.DBNull.Value);
                sqlcommand.Parameters.AddWithValue("@P09", empid);
                sqlcommand.Parameters.AddWithValue("@P10", System.DateTime.Now);

                sqlcommand.ExecuteNonQuery();
                sqlConn.Close();
                sqlConn.Dispose();

                //Add log
                SqlLog sl = new SqlLog();
                sl.AddLog("新增使用者問卷填寫資料", eventid, sl.GetCommendText(sqlcommand), empid);
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }

            return "";
        }

        //取得使用者問卷填寫資料
        public DataTable QuerySurveyData(string surveyid, string surveymodel)
        {
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["tel_event"].ConnectionString;
            string sqlString = "";

            if (surveymodel == "1")
            {
                sqlString += @"SELECT * FROM TEL_Event_SurveyModel1 ";
            }
            else if (surveymodel == "2")
            {
                sqlString += @"SELECT * FROM TEL_Event_SurveyModel2 ";
            }
            else if (surveymodel == "3")
            {
                sqlString += @"SELECT * FROM TEL_Event_SurveyModel3 ";
            }
            else if (surveymodel == "4")
            {
                sqlString += @"SELECT * FROM TEL_Event_SurveyModel4 ";
            }

            sqlString += @"WHERE id=@surveyid ";

            DataTable result = null;

            using (SqlConnection connection = new SqlConnection(connStr))
            {
                connection.Open();

                SqlDataAdapter wrDad = new SqlDataAdapter();
                DataSet DS = new DataSet();

                wrDad.SelectCommand = new SqlCommand(sqlString, connection);
                wrDad.SelectCommand.Parameters.AddWithValue("@surveyid", surveyid);

                wrDad.Fill(DS, "T");
                result = DS.Tables["T"];
            }

            return result;
        }
    }
}
