using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Web;

namespace TEL.Event.Lab.Data
{
    public class EventData
    {
        //取得我的活動資料
        public DataTable QueryMyEvent(string name, string catid, string status, string empid)
        {
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["tel_event"].ConnectionString;
            string sqlString = "";
            DataTable result = null;

            try
            {
                sqlString = @"SELECT a.name AS eventname,c.name AS categoryname,
                          REPLACE(CONVERT(VARCHAR, a.registerstart,120),'-','/') AS registerstart,REPLACE(CONVERT(VARCHAR, a.registerend,120),'-','/') as registerend,
                          CONVERT(VARCHAR, a.eventstart,111) as eventstart,CONVERT(VARCHAR, a.eventend,111) AS eventend,a.registermodel,
                          a.surveystartdate,a.surveymodel,b.id AS registerid, d.id AS surveyid, 
						  CAST(a.id AS varchar(40))+'_'+a.registermodel+'_'+CAST(b.id AS varchar(40)) AS registerinfo,
                          CAST(a.id AS varchar(40))+'_'+a.surveymodel+'_'+ISNULL(CAST(d.id AS varchar(40)),'') AS surveyinfo,
						  CASE WHEN a.eventstart>getdate() THEN '尚未開始' WHEN a.eventend<getdate() THEN '已結束' ELSE '進行中' END AS status
                          FROM TEL_Event_Events a
                          INNER JOIN 
                          (SELECT id,eventid,empid FROM TEL_Event_RegisterModel1
                           UNION
                           SELECT id,eventid,empid FROM TEL_Event_RegisterModel2
                           UNION
                           SELECT id,eventid,empid FROM TEL_Event_RegisterModel3
                           UNION
                           SELECT id,eventid,empid FROM TEL_Event_RegisterModel4
                           UNION
                           SELECT id,eventid,empid FROM TEL_Event_RegisterModel5
                           UNION
                           SELECT id,eventid,empid FROM TEL_Event_RegisterModel6) b ON a.id=b.eventid
                          INNER JOIN TEL_Event_Category c ON a.categoryid=c.id
                          LEFT JOIN 
                          (SELECT id,eventid,empid FROM TEL_Event_SurveyModel1
                           UNION 
                           SELECT id,eventid,empid FROM TEL_Event_SurveyModel2
                           UNION 
                           SELECT id,eventid,empid FROM TEL_Event_SurveyModel3
                           UNION 
                           SELECT id,eventid,empid FROM TEL_Event_SurveyModel4) d ON a.id=d.eventid and b.empid=d.empid 
                          WHERE b.empid=@empid ";

                if (!string.IsNullOrEmpty(name))
                {
                    sqlString += "AND a.name LIKE @name ";
                }

                if (!string.IsNullOrEmpty(catid))
                {
                    sqlString += "AND c.id=@catid ";
                }

                if (!string.IsNullOrEmpty(status))
                {
                    if (status == "N")
                        sqlString += "AND a.eventstart>GETDATE() ";
                    else if (status == "O")
                        sqlString += "AND (a.eventstart<=GETDATE() AND a.eventend>=GETDATE()) ";
                    else if (status == "F")
                        sqlString += "AND a.eventend<GETDATE() ";
                }

                sqlString += "ORDER BY a.eventstart DESC";

                using (SqlConnection connection = new SqlConnection(connStr))
                {
                    connection.Open();

                    SqlDataAdapter wrDad = new SqlDataAdapter();
                    DataSet DS = new DataSet();

                    wrDad.SelectCommand = new SqlCommand(sqlString, connection);
                    wrDad.SelectCommand.Parameters.AddWithValue("@empid", empid);

                    if (!string.IsNullOrEmpty(name))
                    {
                        wrDad.SelectCommand.Parameters.AddWithValue("@name", '%' + name + '%');
                    }

                    if (!string.IsNullOrEmpty(catid))
                    {
                        wrDad.SelectCommand.Parameters.AddWithValue("@catid", catid);
                    }


                    wrDad.Fill(DS, "T");
                    result = DS.Tables["T"];
                }
            }
            catch (Exception ex)
            {

                var context = HttpContext.Current;
                context.Response.Write(ex.ToString());
            }

            return result;
        }

        //取得活動資訊
        public DataTable QueryEventInfo(string eventid)
        {
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["tel_event"].ConnectionString;
            string sqlString = "";

            sqlString = @"SELECT a.name AS eventname,b.name AS categoryname,b.color AS categorycolor,a.eventstart,a.eventend,
                          a.registerstart,a.registerend,a.limit,a.description,a.registermodel,a.surveymodel
                          FROM TEL_Event_Events a
                          INNER JOIN TEL_Event_Category b ON a.categoryid=b.id
                          WHERE a.id=@eventid ";

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

        //取得活動報名人數
        public String QueryEvnetRegisterCount(string eventid, string registermodel)
        {
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["tel_event"].ConnectionString;
            string sqlString = "";

            if (registermodel == "1")
            {
                sqlString = @"SELECT COUNT(empid) AS count FROM TEL_Event_RegisterModel1 ";
            }
            else if (registermodel == "2")
            {
                sqlString = @"SELECT COUNT(empid) AS count FROM TEL_Event_RegisterModel2 ";
            }
            else if (registermodel == "3")
            {
                sqlString = @"SELECT COUNT(empid) AS count FROM TEL_Event_RegisterModel3 ";
            }
            else if (registermodel == "4")
            {
                sqlString = @"SELECT COUNT(empid) AS count FROM TEL_Event_RegisterModel4 ";
            }
            else if (registermodel == "5")
            {
                sqlString = @"SELECT COUNT(empid) AS count FROM TEL_Event_RegisterModel5 ";
            }
            else if (registermodel == "6")
            {
                sqlString = @"SELECT COUNT(empid) AS count FROM TEL_Event_RegisterModel6 ";

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


        //取得健檢中心資料
        public DataTable QueryHosipitalData(string eventid)
        {
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["tel_event"].ConnectionString;
            string sqlString = "";

            sqlString = @"SELECT DISTINCT(hosipital) 
                          FROM TEL_Event_RegisterOption4 
                          WHERE eventid=@eventid
                          ORDER BY hosipital";

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
    }
}
