using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using TEL.Event.Lab.Method;
using System.Globalization;

namespace TEL.Event.Lab.Data
{
    public class EventData
    {
        private string GetConnectionString()
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings["tel_event"].ConnectionString;
        }

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
						  CASE WHEN a.eventstart>getdate() THEN '尚未開始' WHEN dateadd(d,1,a.eventend) <getdate() THEN '已結束' ELSE '進行中' END AS status, 
                          REPLACE(CONVERT(VARCHAR, b.registerdate,120),'-','/') as registerdate 
                          FROM TEL_Event_Events a
                          INNER JOIN 
                          (SELECT id,eventid,empid,registerdate FROM TEL_Event_RegisterModel1
                           UNION
                           SELECT id,eventid,empid,registerdate FROM TEL_Event_RegisterModel2
                           UNION
                           SELECT id,eventid,empid,registerdate FROM TEL_Event_RegisterModel3
                           UNION
                           SELECT id,eventid,empid,registerdate FROM TEL_Event_RegisterModel4
                           UNION
                           SELECT id,eventid,empid,registerdate FROM TEL_Event_RegisterModel5
                           UNION
                           SELECT id,eventid,empid,registerdate FROM TEL_Event_RegisterModel6) b ON a.id=b.eventid
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

            sqlString = @"SELECT 
                            a.id AS eventnid,
                            a.name AS eventname,
                            b.name AS categoryname,
                            a.categoryid,
                            b.color AS categorycolor,
                            CONVERT(varchar,a.eventstart,111) as eventstart,
                            CONVERT(varchar,a.eventend,111) as eventend,
                            a.registerstart,
                            a.registerend,
                            a.limit,
                            a.member,
                            a.mailgroup,
                            a.mailgroupother,
                            a.description,
                            a.image1,
                            a.image1_name,
                            a.image2,
                            a.image2_name,
                            isnull(a.enabled, '') as enabled,
                            isnull(a.duplicated, '' ) as duplicated,
                            a.registermodel,
                            a.surveymodel,
                            a.surveystartdate
                          FROM TEL_Event_Events a
                          INNER JOIN TEL_Event_Category b ON a.categoryid=b.id
                           ";

            if (!string.IsNullOrEmpty(eventid))
            {
                sqlString += " WHERE a.id=@eventid ";
            }

            DataTable result = null;

            using (SqlConnection connection = new SqlConnection(connStr))
            {
                connection.Open();

                SqlDataAdapter wrDad = new SqlDataAdapter();
                DataSet DS = new DataSet();

                wrDad.SelectCommand = new SqlCommand(sqlString, connection);

                if (!string.IsNullOrEmpty(eventid))
                    wrDad.SelectCommand.Parameters.AddWithValue("@eventid", eventid);

                wrDad.Fill(DS, "T");
                result = DS.Tables["T"];
            }

            return result;
        }

        //取得活動資訊
        public DataTable QueryEventInfo(string eventid = "", string eventname = "", string eventcateid = "", string eventSdate = "", string eventEdate = "", string status = "", string enabled = "", int isManager = 0, string empid = "")
        {
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["tel_event"].ConnectionString;
            string sqlString = "";

            sqlString = @"SELECT 
                            a.id AS eventnid,
                            a.name AS eventname,
                            b.name AS categoryname,
                            a.categoryid,
                            b.color AS categorycolor,
                            CONVERT(varchar,a.eventstart,111) as eventstart,
                            CONVERT(varchar,a.eventend,111) as eventend,
                            a.registerstart,
                            a.registerend,
                            a.limit,
                            a.member,
                            a.mailgroup,
                            a.mailgroupother,
                            a.description,
                            a.image1,
                            a.image1_name,
                            a.image2,
                            a.image2_name,
                            isnull(a.enabled, '') as enabled,
                            isnull(a.duplicated, '' ) as duplicated,
                            a.registermodel,
                            a.surveymodel,
                            a.surveystartdate
                          FROM TEL_Event_Events a
                          INNER JOIN TEL_Event_Category b ON a.categoryid=b.id 
                          LEFT JOIN TEL_Event_EventAdmin c ON a.id = c.eventid 
                          WHERE a.id = a.id ";

            if (isManager == 1)
            {
                sqlString += @" AND c.empid = @empid ";
            }

            if (isManager == 2)
            {
                sqlString += @" AND (a.[initby] = @empid OR c.empid = @empid) ";
            }

            if (!string.IsNullOrEmpty(eventid))
            {
                sqlString += @" AND a.id = @eventid ";
            }

            if (!string.IsNullOrEmpty(eventname))
            {
                sqlString += @" AND a.name like '%' + @eventname + '%'  ";
            }

            if (!string.IsNullOrEmpty(eventcateid))
            {
                sqlString += @" AND a.categoryid = @eventcateid ";
            }

            if (!string.IsNullOrEmpty(eventSdate))
            {
                sqlString += @" AND a.eventstart >= @eventSdate ";
            }

            if (!string.IsNullOrEmpty(eventEdate))
            {
                sqlString += @" AND a.eventstart <= @eventEdate ";
            }

            if (!string.IsNullOrEmpty(status))
            {
                switch (status)
                {

                    case "N"://尚未開始
                        sqlString += @" AND a.eventstart > GETDATE() ";
                        break;
                    case "O"://進行中
                        sqlString += @" AND a.eventstart <= GETDATE()
                                        AND GETDATE() <= a.eventend ";
                        break;

                    case "F"://已結束
                        sqlString += @" AND a.eventend < GETDATE() ";
                        break;
                    default:
                        break;
                }
            }

            if (!string.IsNullOrEmpty(enabled))
            {
                sqlString += @" AND a.enabled = @enabled ";
            }

            sqlString += @" ORDER BY  a.eventstart DESC, a.name";

            DataTable result = null;

            using (SqlConnection connection = new SqlConnection(connStr))
            {
                connection.Open();

                SqlDataAdapter wrDad = new SqlDataAdapter();
                DataSet DS = new DataSet();

                wrDad.SelectCommand = new SqlCommand(sqlString, connection);

                if (isManager == 1)
                {
                    wrDad.SelectCommand.Parameters.AddWithValue("@empid", empid);
                }

                if (isManager == 2)
                {
                    wrDad.SelectCommand.Parameters.AddWithValue("@empid", empid);
                }

                if (!string.IsNullOrEmpty(eventid))
                    wrDad.SelectCommand.Parameters.AddWithValue("@eventid", eventid);

                if (!string.IsNullOrEmpty(eventname))
                    wrDad.SelectCommand.Parameters.AddWithValue("@eventname", eventname);

                if (!string.IsNullOrEmpty(eventcateid))
                    wrDad.SelectCommand.Parameters.AddWithValue("@eventcateid", eventcateid);

                if (!string.IsNullOrEmpty(eventSdate))
                {
                    wrDad.SelectCommand.Parameters.AddWithValue("@eventSdate", eventSdate);
                }

                if (!string.IsNullOrEmpty(eventEdate))
                {
                    wrDad.SelectCommand.Parameters.AddWithValue("@eventEdate", eventEdate);
                }

                if (!string.IsNullOrEmpty(enabled))
                {
                    wrDad.SelectCommand.Parameters.AddWithValue("@enabled", enabled);
                }

                wrDad.Fill(DS, "T");
                result = DS.Tables["T"];
            }

            return result;
        }

        /// <summary>
        /// 刪除活動
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        internal string DeleteEvent(string id)
        {
            string connStr = GetConnectionString();
            string sqlStr = "";

            try
            {
                sqlStr = @"
                DELETE 
                    TEL_Event_Events 
                WHERE 
                    id = @id

                DELETE 
                    TEL_Event_EventAdmin 
                WHERE 
                    eventid = @id

                DELETE 
                    TEL_Event_RegisterOption1
                WHERE 
                    eventid = @id

                DELETE 
                    TEL_Event_RegisterOption2
                WHERE 
                    eventid = @id

                DELETE 
                    TEL_Event_RegisterOption3
                WHERE 
                    eventid = @id

                DELETE 
                    TEL_Event_RegisterOption4
                WHERE 
                    eventid = @id

                DELETE 
                    TEL_Event_RegisterOption5
                WHERE 
                    eventid = @id

                DELETE 
                    TEL_Event_RegisterOption6
                WHERE 
                    eventid = @id
            ";

                SqlConnection sqlConn = new SqlConnection(connStr);
                sqlConn.Open();
                SqlCommand sqlcommand = new SqlCommand(sqlStr, sqlConn);
                sqlcommand.Parameters.Clear();
                sqlcommand.Parameters.AddWithValue("@id", id);
                sqlcommand.ExecuteNonQuery();
                sqlConn.Close();
                sqlConn.Dispose();
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }

            return "";
        }



        /// <summary>
        /// 查詢已報名活動資訊
        /// </summary>
        /// <param name="eventid"></param>
        /// <returns></returns>
        public DataTable QueryRegisteredEventInfo(string eventid = "")
        {
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["tel_event"].ConnectionString;
            string sqlString = "";

            sqlString = @"SELECT 
                            a.id AS eventnid,
                            a.name AS eventname,
                            a.categoryid,
                            CONVERT(varchar,a.eventstart,111) as eventstart,
                            CONVERT(varchar,a.eventend,111) as eventend,
                            a.registerstart,
                            a.registerend,
                            a.limit,
                            a.member,
                            a.mailgroup,
                            a.mailgroupother,
                            a.description,
                            a.image1,
                            a.image1_name,
                            a.image2,
                            a.image2_name,
                            isnull(a.enabled, '') as enabled,
                            isnull(a.duplicated, '' ) as duplicated,
                            a.registermodel,
                            a.surveymodel,
                            a.surveystartdate,
                            b.id as registerid,
							b.empid
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
                           WHERE 
                               a.id = a.id ";

            if (!string.IsNullOrEmpty(eventid))
            {
                sqlString += @" AND a.id = @eventid ";
            }


            DataTable result = null;

            using (SqlConnection connection = new SqlConnection(connStr))
            {
                connection.Open();

                SqlDataAdapter wrDad = new SqlDataAdapter();
                DataSet DS = new DataSet();

                wrDad.SelectCommand = new SqlCommand(sqlString, connection);

                if (!string.IsNullOrEmpty(eventid))
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



        /// <summary>
        /// 取得活動報名 by empid
        /// </summary>
        /// <param name="eventid"></param>
        /// <param name="registermodel"></param>
        /// <param name="empid"></param>
        /// <returns></returns>
        public DataTable GetUserEvnetRegister(string eventid, string registermodel, string empid)
        {
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["tel_event"].ConnectionString;
            string sqlString = "";

            if (registermodel == "1")
            {
                sqlString = @"SELECT * FROM TEL_Event_RegisterModel1 ";
            }
            else if (registermodel == "2")
            {
                sqlString = @"SELECT * FROM TEL_Event_RegisterModel2 ";
            }
            else if (registermodel == "3")
            {
                sqlString = @"SELECT * FROM TEL_Event_RegisterModel3 ";
            }
            else if (registermodel == "4")
            {
                sqlString = @"SELECT * FROM TEL_Event_RegisterModel4 ";
            }
            else if (registermodel == "5")
            {
                sqlString = @"SELECT * FROM TEL_Event_RegisterModel5 ";
            }
            else if (registermodel == "6")
            {
                sqlString = @"SELECT * FROM TEL_Event_RegisterModel6 ";

            }
            sqlString += @"WHERE id = id ";

            if (!string.IsNullOrEmpty(eventid))
            {
                sqlString += @"
                            AND eventid = @eventid
                                ";
            }

            if (!string.IsNullOrEmpty(empid))
            {
                sqlString += @"
                            AND empid = @empid
                                ";
            }

            DataTable result = null;

            using (SqlConnection connection = new SqlConnection(connStr))
            {
                connection.Open();

                SqlDataAdapter wrDad = new SqlDataAdapter();
                DataSet DS = new DataSet();

                wrDad.SelectCommand = new SqlCommand(sqlString, connection);

                if (!string.IsNullOrEmpty(eventid))
                    wrDad.SelectCommand.Parameters.AddWithValue("@eventid", eventid);

                if (!string.IsNullOrEmpty(empid))
                    wrDad.SelectCommand.Parameters.AddWithValue("@empid", empid);

                wrDad.Fill(DS, "T");
                result = DS.Tables["T"];
            }

            return result;
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

        /// <summary>
        /// 使用者可報名列表
        /// </summary>
        /// <param name="dtUserMailGroupd">User 所存在的MailGroupd</param>
        /// <param name="empid"></param>
        /// <param name="eventname"></param>
        /// <param name="eventcateid"></param>
        /// <returns></returns>
        public DataTable QueryUserRegisterEventList(DataTable dtUserMailGroupd, string empid, string eventname = "", string eventcateid = "")
        {
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["tel_event"].ConnectionString;
            string sqlString = "";

            sqlString = @"
                    SELECT 
                        a.id AS eventnid,
                        a.name AS eventname,
                        b.name AS categoryname,
                        a.categoryid,
                        b.color AS categorycolor,
                        CONVERT(varchar,a.eventstart,111) as eventstart,
                        CONVERT(varchar,a.eventend,111) as eventend,
                        a.registerstart,
                        a.registerend,
                        a.limit,
                        a.member,
                        a.mailgroup,
                        a.mailgroupother,
                        a.description,
                        a.image1,
                        a.image1_name,
                        a.image2,
                        a.image2_name,
                        isnull(a.enabled, '') as enabled,
                        isnull(a.duplicated, '' ) as duplicated,
                        a.registermodel,
                        a.surveymodel,
                        a.surveystartdate
                    FROM 
                        TEL_Event_Events a
                    INNER JOIN 
                        TEL_Event_Category b ON a.categoryid=b.id
                        
                    WHERE 
                        a.id = a.id 
                    AND
                        a.eventend >= GETDATE()
                    AND
                        a.registerend >= GETDATE()
                    AND
                        a.enabled = 'Y'
                    AND 
						(	a.member = 'A' 
							OR
							a.id IN (SELECT eventid FROM [TEL_Event_Permission_Empid]
									WHERE [empid] = @empid
									UNION
									SELECT eventid FROM [TEL_Event_Permission_MailGroup] a
                                    INNER JOIN MailGroup b on a.mailgroupName=b.Name
								    WHERE b.[empid] = @empid )
						) 
                        ";

            if (!string.IsNullOrEmpty(eventname))
            {
                sqlString += @" AND a.name like '%' + @eventname + '%'  ";
            }

            if (!string.IsNullOrEmpty(eventcateid))
            {
                sqlString += @" AND a.categoryid = @eventcateid ";
            }

            sqlString += @" ORDER BY  a.registerstart";

            DataTable result = null;

            using (SqlConnection connection = new SqlConnection(connStr))
            {
                connection.Open();

                SqlDataAdapter wrDad = new SqlDataAdapter();
                DataSet DS = new DataSet();

                wrDad.SelectCommand = new SqlCommand(sqlString, connection);

                wrDad.SelectCommand.Parameters.AddWithValue("@empid", empid);

                if (!string.IsNullOrEmpty(eventname))
                    wrDad.SelectCommand.Parameters.AddWithValue("@eventname", eventname);

                if (!string.IsNullOrEmpty(eventcateid))
                    wrDad.SelectCommand.Parameters.AddWithValue("@eventcateid", eventcateid);

                wrDad.Fill(DS, "T");
                result = DS.Tables["T"];
            }

            return result;
        }

        internal string InsertEvent(Dictionary<string, string> eventsData, Dictionary<string, string> eventAdminData, string empid)
        {
            string connStr = GetConnectionString();
            string sqlStr = string.Empty;

            SqlConnection conn = new SqlConnection(connStr);
            SqlTransaction transaction;

            conn.Open();
            transaction = conn.BeginTransaction();
            try
            {
                sqlStr = @"
                INSERT INTO [TEL_Event_Events]
                       ([id]
                       ,[name]
                       ,[categoryid]
                       ,[eventstart]
                       ,[eventend]
                       ,[limit]
                       ,[registerstart]
                       ,[registerend]
                       ,[member]
                       ,[mailgroup]
                       ,[mailgroupother]
                       ,[description]
                       ,[image1]
                       ,[image1_name]
                       ,[image2]
                       ,[image2_name]
                       ,[enabled]
                       ,[duplicated]
                       ,[registermodel]
                       ,[surveymodel]
                       ,[initby]
                       ,[initdate]
                       ,[modifiedby]
                       ,[modifieddate])
                 VALUES
                       (@id
                       ,@name
                       ,@categoryid
                       ,@eventstart
                       ,@eventend
                       ,@limit
                       ,@registerstart
                       ,@registerend
                       ,@member
                       ,@mailgroup
                       ,@mailgroupother
                       ,@description
                       ,@image1
                       ,@image1_name
                       ,@image2
                       ,@image2_name
                       ,@enabled
                       ,@duplicated
                       ,@registermodel
                       ,@surveymodel
                       ,@initby
                       ,GETDATE()
                       ,@modifiedby
                       ,GETDATE())
                ";

                SqlCommand commandEvents = new SqlCommand(sqlStr, conn, transaction);
                commandEvents.Parameters.Clear();
                commandEvents.Parameters.AddWithValue("@id", eventsData["id"]);
                commandEvents.Parameters.AddWithValue("@name", eventsData["name"]);
                commandEvents.Parameters.AddWithValue("@categoryid", eventsData["categoryid"]);
                commandEvents.Parameters.AddWithValue("@eventstart", eventsData["eventstart"]);
                commandEvents.Parameters.AddWithValue("@eventend", eventsData["eventend"]);

                if (string.IsNullOrEmpty(eventsData["limit"]))
                    commandEvents.Parameters.AddWithValue("@limit", System.DBNull.Value);
                else
                    commandEvents.Parameters.AddWithValue("@limit", eventsData["limit"]);

                commandEvents.Parameters.AddWithValue("@registerstart", eventsData["registerstart"]);
                commandEvents.Parameters.AddWithValue("@registerend", eventsData["registerend"]);
                commandEvents.Parameters.AddWithValue("@member", eventsData["member"]);

                if (string.IsNullOrEmpty(eventsData["mailgroup"]))
                    commandEvents.Parameters.AddWithValue("@mailgroup", System.DBNull.Value);
                else
                    commandEvents.Parameters.AddWithValue("@mailgroup", eventsData["mailgroup"]);

                if (string.IsNullOrEmpty(eventsData["mailgroupother"]))
                    commandEvents.Parameters.AddWithValue("@mailgroupother", System.DBNull.Value);
                else
                    commandEvents.Parameters.AddWithValue("@mailgroupother", eventsData["mailgroupother"]);

                commandEvents.Parameters.AddWithValue("@description", eventsData["description"]);

                if (string.IsNullOrEmpty(eventsData["image1"]))
                    commandEvents.Parameters.AddWithValue("@image1", System.DBNull.Value);
                else
                    commandEvents.Parameters.AddWithValue("@image1", eventsData["image1"]);

                if (string.IsNullOrEmpty(eventsData["image1_name"]))
                    commandEvents.Parameters.AddWithValue("@image1_name", System.DBNull.Value);
                else
                    commandEvents.Parameters.AddWithValue("@image1_name", eventsData["image1_name"]);

                if (string.IsNullOrEmpty(eventsData["image2"]))
                    commandEvents.Parameters.AddWithValue("@image2", System.DBNull.Value);
                else
                    commandEvents.Parameters.AddWithValue("@image2", eventsData["image2"]);


                if (string.IsNullOrEmpty(eventsData["image2_name"]))
                    commandEvents.Parameters.AddWithValue("@image2_name", System.DBNull.Value);
                else
                    commandEvents.Parameters.AddWithValue("@image2_name", eventsData["image2_name"]);

                if (eventsData["enabled"] == "Y")
                    commandEvents.Parameters.AddWithValue("@enabled", eventsData["enabled"]);
                else
                    commandEvents.Parameters.AddWithValue("@enabled", System.DBNull.Value);

                if (eventsData["duplicated"] == "Y")
                    commandEvents.Parameters.AddWithValue("@duplicated", eventsData["duplicated"]);
                else
                    commandEvents.Parameters.AddWithValue("@duplicated", System.DBNull.Value);

                commandEvents.Parameters.AddWithValue("@registermodel", eventsData["registermodel"]);

                if (string.IsNullOrEmpty(eventsData["surveymodel"]))
                    commandEvents.Parameters.AddWithValue("@surveymodel", System.DBNull.Value);
                else
                    commandEvents.Parameters.AddWithValue("@surveymodel", eventsData["surveymodel"]);

                commandEvents.Parameters.AddWithValue("@modifiedby", empid);
                commandEvents.Parameters.AddWithValue("@initby", empid);


                commandEvents.ExecuteNonQuery();

                string eventID = eventAdminData["eventid"];

                //新增其它活動管理者
                if (!string.IsNullOrEmpty(eventAdminData["empid"]))
                {
                    string[] empids = eventAdminData["empid"].Split(',');
                    foreach (string empidstr in empids)
                    {
                        sqlStr = @"
                        INSERT INTO [TEL_Event_EventAdmin]
                               ([eventid]
                               ,[empid]
                               ,[modifiedby]
                               ,[modifieddate])
                         VALUES
                               (@eventid
                               ,@empid
                               ,@modifiedby
                               ,GETDATE())
                    ";

                        SqlCommand command = new SqlCommand(sqlStr, conn, transaction);

                        command.Parameters.Clear();
                        command.Parameters.AddWithValue("@eventid", eventID);
                        command.Parameters.AddWithValue("@empid", empidstr);
                        command.Parameters.AddWithValue("@modifiedby", empid);

                        command.ExecuteNonQuery();
                    }
                }

                //新增活動權限 by mailgroup
                if (!string.IsNullOrEmpty(eventsData["mailgroup"]))
                {
                    string[] mailgroups = eventsData["mailgroup"].Split(',');
                    foreach (string mailgroupstr in mailgroups)
                    {
                        sqlStr = @"
                        INSERT INTO [TEL_Event_Permission_MailGroup]
                               ([eventid]
                               ,[mailgroupName]
                               ,[modifiedby]
                               ,[modifieddate])
                         VALUES
                               (@eventid
                               ,@mailgroup
                               ,@modifiedby
                               ,GETDATE())
                    ";

                        SqlCommand commandPermissionMailGroup = new SqlCommand(sqlStr, conn, transaction);

                        commandPermissionMailGroup.Parameters.Clear();
                        commandPermissionMailGroup.Parameters.AddWithValue("@eventid", eventID);
                        commandPermissionMailGroup.Parameters.AddWithValue("@mailgroup", mailgroupstr);
                        commandPermissionMailGroup.Parameters.AddWithValue("@modifiedby", empid);

                        commandPermissionMailGroup.ExecuteNonQuery();
                    }
                }

                //新增活動權限 by mailgroupother
                if (!string.IsNullOrEmpty(eventsData["mailgroupother"]))
                {
                    string[] mailgroupothers = eventsData["mailgroupother"].Split(',');
                    foreach (string mailgroupotherstr in mailgroupothers)
                    {
                        sqlStr = @"
                        INSERT INTO [TEL_Event_Permission_Empid]
                               ([eventid]
                               ,[empid]
                               ,[modifiedby]
                               ,[modifieddate])
                         VALUES
                               (@eventid
                               ,@empid
                               ,@modifiedby
                               ,GETDATE())
                    ";

                        SqlCommand commandPermissionEmpid = new SqlCommand(sqlStr, conn, transaction);

                        commandPermissionEmpid.Parameters.Clear();
                        commandPermissionEmpid.Parameters.AddWithValue("@eventid", eventID);
                        commandPermissionEmpid.Parameters.AddWithValue("@empid", mailgroupotherstr);
                        commandPermissionEmpid.Parameters.AddWithValue("@modifiedby", empid);

                        commandPermissionEmpid.ExecuteNonQuery();
                    }
                }
                transaction.Commit();
            }
            catch (SqlException ex)
            {
                transaction.Rollback();
                return ex.ToString();
            }

            conn.Close();
            conn.Dispose();

            return "";
        }

        internal string UpdateEvent(Dictionary<string, string> eventsData, Dictionary<string, string> eventAdminData, string empid)
        {
            string connStr = GetConnectionString();
            string sqlStr = string.Empty;

            SqlConnection conn = new SqlConnection(connStr);
            SqlTransaction transaction;

            conn.Open();
            transaction = conn.BeginTransaction();
            try
            {
                sqlStr = @"
                        UPDATE [TEL_Event_Events]
                           SET 
                              [name] = @name
                              ,[categoryid] = @categoryid
                              ,[eventstart] = @eventstart
                              ,[eventend] = @eventend
                              ,[limit] = @limit
                              ,[registerstart] = @registerstart
                              ,[registerend] = @registerend
                              ,[member] = @member
                              ,[mailgroup] = @mailgroup
                              ,[mailgroupother] = @mailgroupother
                              ,[description] = @description
                        ";

                if (!string.IsNullOrEmpty(eventsData["image1"]))
                {
                    sqlStr += @"
                             ,[image1] = @image1
                             ,[image1_name] = @image1_name
                            ";
                }

                if (!string.IsNullOrEmpty(eventsData["image2"]))
                {
                    sqlStr += @"
                             ,[image2] = @image2
                             ,[image2_name] = @image2_name
                            ";
                }

                sqlStr += @",[enabled] = @enabled
                              ,[duplicated] = @duplicated
                              ,[registermodel] = @registermodel
                              ,[surveymodel] = @surveymodel
                              ,[modifiedby] = @modifiedby
                              ,[modifieddate] = GETDATE()
                         WHERE 
                              id = @id

                ";

                SqlCommand commandEvents = new SqlCommand(sqlStr, conn, transaction);
                commandEvents.Parameters.Clear();
                commandEvents.Parameters.AddWithValue("@id", eventsData["id"]);
                commandEvents.Parameters.AddWithValue("@name", eventsData["name"]);
                commandEvents.Parameters.AddWithValue("@categoryid", eventsData["categoryid"]);
                commandEvents.Parameters.AddWithValue("@eventstart", eventsData["eventstart"]);
                commandEvents.Parameters.AddWithValue("@eventend", eventsData["eventend"]);

                if (string.IsNullOrEmpty(eventsData["limit"]))
                    commandEvents.Parameters.AddWithValue("@limit", System.DBNull.Value);
                else
                    commandEvents.Parameters.AddWithValue("@limit", eventsData["limit"]);

                commandEvents.Parameters.AddWithValue("@registerstart", eventsData["registerstart"]);
                commandEvents.Parameters.AddWithValue("@registerend", eventsData["registerend"]);
                commandEvents.Parameters.AddWithValue("@member", eventsData["member"]);

                if (string.IsNullOrEmpty(eventsData["mailgroup"]))
                    commandEvents.Parameters.AddWithValue("@mailgroup", System.DBNull.Value);
                else
                    commandEvents.Parameters.AddWithValue("@mailgroup", eventsData["mailgroup"]);

                if (string.IsNullOrEmpty(eventsData["mailgroupother"]))
                    commandEvents.Parameters.AddWithValue("@mailgroupother", System.DBNull.Value);
                else
                    commandEvents.Parameters.AddWithValue("@mailgroupother", eventsData["mailgroupother"]);

                commandEvents.Parameters.AddWithValue("@description", eventsData["description"]);

                if (!string.IsNullOrEmpty(eventsData["image1"]))
                    commandEvents.Parameters.AddWithValue("@image1", eventsData["image1"]);

                if (!string.IsNullOrEmpty(eventsData["image1_name"]))
                    commandEvents.Parameters.AddWithValue("@image1_name", eventsData["image1_name"]);

                if (!string.IsNullOrEmpty(eventsData["image2"]))
                    commandEvents.Parameters.AddWithValue("@image2", eventsData["image2"]);


                if (!string.IsNullOrEmpty(eventsData["image2_name"]))
                    commandEvents.Parameters.AddWithValue("@image2_name", eventsData["image2_name"]);

                if (eventsData["enabled"] == "Y")
                    commandEvents.Parameters.AddWithValue("@enabled", eventsData["enabled"]);
                else
                    commandEvents.Parameters.AddWithValue("@enabled", System.DBNull.Value);

                if (eventsData["duplicated"] == "Y")
                    commandEvents.Parameters.AddWithValue("@duplicated", eventsData["duplicated"]);
                else
                    commandEvents.Parameters.AddWithValue("@duplicated", System.DBNull.Value);

                commandEvents.Parameters.AddWithValue("@registermodel", eventsData["registermodel"]);

                if (string.IsNullOrEmpty(eventsData["surveymodel"]))
                    commandEvents.Parameters.AddWithValue("@surveymodel", System.DBNull.Value);
                else
                    commandEvents.Parameters.AddWithValue("@surveymodel", eventsData["surveymodel"]);

                commandEvents.Parameters.AddWithValue("@modifiedby", empid);

                commandEvents.ExecuteNonQuery();

                //先刪除
                sqlStr = @"
                DELETE 
                    TEL_Event_EventAdmin 
                WHERE 
                    eventid = @id
                ";

                SqlCommand commandDelete = new SqlCommand(sqlStr, conn, transaction);
                commandDelete.Parameters.AddWithValue("@id", eventsData["id"]);
                commandDelete.ExecuteNonQuery();

                //再新增
                string eventID = eventAdminData["eventid"];

                if (!string.IsNullOrEmpty(eventAdminData["empid"]))
                {
                    string[] empids = eventAdminData["empid"].Split(',');
                    foreach (string empidstr in empids)
                    {
                        sqlStr = @"
                        INSERT INTO [TEL_Event_EventAdmin]
                               ([eventid]
                               ,[empid]
                               ,[modifiedby]
                               ,[modifieddate])
                         VALUES
                               (@eventid
                               ,@empid
                               ,@modifiedby
                               ,GETDATE())
                    ";

                        SqlCommand command = new SqlCommand(sqlStr, conn, transaction);

                        command.Parameters.Clear();
                        command.Parameters.AddWithValue("@eventid", eventID);
                        command.Parameters.AddWithValue("@empid", empidstr);
                        command.Parameters.AddWithValue("@modifiedby", empid);

                        command.ExecuteNonQuery();
                    }
                }

                //先刪除活動權限 by mailgroup
                sqlStr = @"
                DELETE 
                    TEL_Event_Permission_MailGroup 
                WHERE 
                    eventid = @id
                ";

                SqlCommand commandDeletePermissionMailGroup = new SqlCommand(sqlStr, conn, transaction);
                commandDeletePermissionMailGroup.Parameters.AddWithValue("@id", eventsData["id"]);
                commandDeletePermissionMailGroup.ExecuteNonQuery();

                if (!string.IsNullOrEmpty(eventsData["mailgroup"]))
                {
                    //新增活動權限 by mailgroup
                    string[] mailgroups = eventsData["mailgroup"].Split(',');
                    foreach (string mailgroupstr in mailgroups)
                    {
                        sqlStr = @"
                        INSERT INTO [TEL_Event_Permission_MailGroup]
                               ([eventid]
                               ,[mailgroupName]
                               ,[modifiedby]
                               ,[modifieddate])
                         VALUES
                               (@eventid
                               ,@mailgroup
                               ,@modifiedby
                               ,GETDATE())
                    ";

                        SqlCommand commandPermissionMailGroup = new SqlCommand(sqlStr, conn, transaction);

                        commandPermissionMailGroup.Parameters.Clear();
                        commandPermissionMailGroup.Parameters.AddWithValue("@eventid", eventID);
                        commandPermissionMailGroup.Parameters.AddWithValue("@mailgroup", mailgroupstr);
                        commandPermissionMailGroup.Parameters.AddWithValue("@modifiedby", empid);

                        commandPermissionMailGroup.ExecuteNonQuery();
                    }
                }

                //先刪除活動權限 by mailgroupother
                sqlStr = @"
                DELETE 
                    TEL_Event_Permission_Empid 
                WHERE 
                    eventid = @id
                ";

                SqlCommand commandDeletePermissionEmpid = new SqlCommand(sqlStr, conn, transaction);
                commandDeletePermissionEmpid.Parameters.AddWithValue("@id", eventsData["id"]);
                commandDeletePermissionEmpid.ExecuteNonQuery();

                if (!string.IsNullOrEmpty(eventsData["mailgroupother"]))
                {
                    //新增活動權限 by mailgroupother
                    string[] mailgroupothers = eventsData["mailgroupother"].Split(',');
                    foreach (string mailgroupotherstr in mailgroupothers)
                    {
                        sqlStr = @"
                        INSERT INTO [TEL_Event_Permission_Empid]
                               ([eventid]
                               ,[empid]
                               ,[modifiedby]
                               ,[modifieddate])
                         VALUES
                               (@eventid
                               ,@empid
                               ,@modifiedby
                               ,GETDATE())
                    ";

                        SqlCommand commandPermissionEmpid = new SqlCommand(sqlStr, conn, transaction);

                        commandPermissionEmpid.Parameters.Clear();
                        commandPermissionEmpid.Parameters.AddWithValue("@eventid", eventID);
                        commandPermissionEmpid.Parameters.AddWithValue("@empid", mailgroupotherstr);
                        commandPermissionEmpid.Parameters.AddWithValue("@modifiedby", empid);

                        commandPermissionEmpid.ExecuteNonQuery();
                    }
                }

                transaction.Commit();
            }
            catch (SqlException ex)
            {
                transaction.Rollback();
                return ex.ToString();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                return ex.ToString();
            }

            conn.Close();
            conn.Dispose();

            return "";
        }

        public DataTable QueryEventAdmin(string eventid)
        {
            string connStr = GetConnectionString();
            string sqlString = "";

            sqlString = @"
                        SELECT 
                            [eventid]
                            ,[empid]
                            ,[modifiedby]
                            ,[modifieddate]
                        FROM 
                            [TEL_Event_EventAdmin]
                        ";

            if (!string.IsNullOrEmpty(eventid))
            {
                sqlString += " WHERE eventid=@eventid ";
            }

            DataTable result = null;

            using (SqlConnection connection = new SqlConnection(connStr))
            {
                connection.Open();

                SqlDataAdapter wrDad = new SqlDataAdapter();
                DataSet DS = new DataSet();

                wrDad.SelectCommand = new SqlCommand(sqlString, connection);

                if (!string.IsNullOrEmpty(eventid))
                    wrDad.SelectCommand.Parameters.AddWithValue("@eventid", eventid);

                wrDad.Fill(DS, "T");
                result = DS.Tables["T"];
            }

            return result;
        }



        /// <summary>
        /// 新增報名表選項（欲參加的內容）
        /// </summary>
        /// <param name="content"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        internal string InsertRegisterOption1(string eventid, string content, string limit, string modifiedby)
        {
            string connStr = GetConnectionString();
            string sqlStr = "";

            try
            {
                sqlStr = @"
                        INSERT INTO 
                        TEL_Event_RegisterOption1
                            (
                                [id]
                                ,[eventid]
                                ,[description]
                                ,[limit]
                                ,[modifiedby]
                                ,[modifieddate]
                            )
                        VALUES 
                            (
                                newid(),
                                @eventid,
                                @description,
                                @limit,
                                @modifiedby,
                                GETDATE()
                            )";

                SqlConnection conn = new SqlConnection(connStr);
                conn.Open();
                SqlCommand command = new SqlCommand(sqlStr, conn);


                command.Parameters.Clear();
                command.Parameters.AddWithValue("@eventid", eventid);
                command.Parameters.AddWithValue("@description", content);
                command.Parameters.AddWithValue("@limit", limit);
                command.Parameters.AddWithValue("@modifiedby", modifiedby);

                command.ExecuteNonQuery();
                conn.Close();
                conn.Dispose();
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }

            return "";
        }

        /// <summary>
        /// 刪除 報名表選項（欲參加內容）
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        internal string DeleteRegisterOption1(string id)
        {
            string connStr = GetConnectionString();
            string sqlStr = "";

            try
            {
                sqlStr = @"
                DELETE 
                    TEL_Event_RegisterOption1 
                WHERE 
                    id = @id";

                SqlConnection sqlConn = new SqlConnection(connStr);
                sqlConn.Open();
                SqlCommand sqlcommand = new SqlCommand(sqlStr, sqlConn);
                sqlcommand.Parameters.Clear();
                sqlcommand.Parameters.AddWithValue("@id", id);
                sqlcommand.ExecuteNonQuery();
                sqlConn.Close();
                sqlConn.Dispose();
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }

            return "";

        }

        /// <summary>
        /// 查詢報名表選項（欲參加內容）
        /// </summary>
        /// <param name="eventid"></param>
        /// <returns></returns>
        internal DataTable QueryRegisterOption1(string eventid)
        {
            string connStr = GetConnectionString();
            string sqlStr = "";

            sqlStr = @"
                        SELECT 
                            [id]
                            ,[eventid]
                            ,[description]
                            ,[limit]
                            ,[modifiedby]
                            ,[modifieddate]
                        FROM 
                            [TEL_Event_RegisterOption1] ";

            if (!string.IsNullOrEmpty(eventid))
            {
                sqlStr += @" 
                        WHERE  [eventid] = @eventid
                            ";
            }

            sqlStr += @"
                        ORDER BY [description]";

            DataTable result = null;

            using (SqlConnection connection = new SqlConnection(connStr))
            {
                connection.Open();

                SqlDataAdapter wrDad = new SqlDataAdapter();
                DataSet DS = new DataSet();

                wrDad.SelectCommand = new SqlCommand(sqlStr, connection);

                if (!string.IsNullOrEmpty(eventid))
                {
                    wrDad.SelectCommand.Parameters.AddWithValue("@eventid", eventid);
                }

                wrDad.Fill(DS, "T");
                result = DS.Tables["T"];
            }

            return result;
        }

        /// <summary>
        /// 查詢報名表選項（欲參加內容是否存在）
        /// </summary>
        /// <param name="eventid"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        internal DataTable QueryRegisterOption1(string eventid, string name)
        {
            string connStr = GetConnectionString();
            string sqlStr = "";

            sqlStr = @"
                        SELECT 
                            [id]
                            ,[eventid]
                            ,[description]
                            ,[limit]
                            ,[modifiedby]
                            ,[modifieddate]
                        FROM 
                            [TEL_Event_RegisterOption1] 
                        WHERE [id]=[id] ";

            if (!string.IsNullOrEmpty(eventid))
            {
                sqlStr += @"AND  [eventid] = @eventid ";
            }

            if (!string.IsNullOrEmpty(name))
            {
                sqlStr += @"AND [description]=@description ";
            }

            sqlStr += @"ORDER BY [description]";

            DataTable result = null;

            using (SqlConnection connection = new SqlConnection(connStr))
            {
                connection.Open();

                SqlDataAdapter wrDad = new SqlDataAdapter();
                DataSet DS = new DataSet();

                wrDad.SelectCommand = new SqlCommand(sqlStr, connection);

                if (!string.IsNullOrEmpty(eventid))
                {
                    wrDad.SelectCommand.Parameters.AddWithValue("@eventid", eventid);
                }

                if (!string.IsNullOrEmpty(name))
                {
                    wrDad.SelectCommand.Parameters.AddWithValue("@description", name);
                }

                wrDad.Fill(DS, "T");
                result = DS.Tables["T"];
            }

            return result;
        }

        /// <summary>
        /// 查詢報名表選項（欲參加內容）限制人數
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        internal int QueryRegisterOption1Limit(string eventid, string description)
        {
            string connStr = GetConnectionString();
            string sqlStr = "";

            sqlStr = @"
                        SELECT 
                            [limit]
                        FROM 
                            [TEL_Event_RegisterOption1]
                        WHERE  
                            [eventid] = @eventid
                        AND
                            [description] = @description
                        ";


            DataTable result = null;

            using (SqlConnection connection = new SqlConnection(connStr))
            {
                connection.Open();

                SqlDataAdapter wrDad = new SqlDataAdapter();
                DataSet DS = new DataSet();

                wrDad.SelectCommand = new SqlCommand(sqlStr, connection);

                wrDad.SelectCommand.Parameters.AddWithValue("@eventid", eventid);
                wrDad.SelectCommand.Parameters.AddWithValue("@description", description);


                wrDad.Fill(DS, "T");
                result = DS.Tables["T"];
            }

            return Convert.ToInt16(result.Rows[0]["limit"]);
        }

        internal string InsertRegisterOption2(string eventid, string transportation, string modifiedby)
        {
            string connStr = GetConnectionString();
            string sqlStr = "";

            try
            {
                sqlStr = @"
                        INSERT INTO 
                        TEL_Event_RegisterOption2
                            (
                                [id]
                                ,[eventid]
                                ,[description]
                                ,[modifiedby]
                                ,[modifieddate]
                            )
                        VALUES 
                            (
                                newid(),
                                @eventid,
                                @description,
                                @modifiedby,
                                GETDATE()
                            )";

                SqlConnection conn = new SqlConnection(connStr);
                conn.Open();
                SqlCommand command = new SqlCommand(sqlStr, conn);


                command.Parameters.Clear();
                command.Parameters.AddWithValue("@eventid", eventid);
                command.Parameters.AddWithValue("@description", transportation);
                command.Parameters.AddWithValue("@modifiedby", modifiedby);

                command.ExecuteNonQuery();
                conn.Close();
                conn.Dispose();
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }

            return "";
        }

        /// <summary>
        /// 刪除報名表選項（交通車）
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        internal string DeleteRegisterOption2(string id)
        {
            string connStr = GetConnectionString();
            string sqlStr = "";

            try
            {
                sqlStr = @"
                DELETE 
                    TEL_Event_RegisterOption2 
                WHERE 
                    id = @id";

                SqlConnection sqlConn = new SqlConnection(connStr);
                sqlConn.Open();
                SqlCommand sqlcommand = new SqlCommand(sqlStr, sqlConn);
                sqlcommand.Parameters.Clear();
                sqlcommand.Parameters.AddWithValue("@id", id);
                sqlcommand.ExecuteNonQuery();
                sqlConn.Close();
                sqlConn.Dispose();
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }

            return "";

        }

        /// <summary>
        /// 查詢報名表選項（交通車）
        /// </summary>
        /// <param name="eventid"></param>
        /// <returns></returns>
        internal DataTable QueryRegisterOption2(string eventid)
        {
            string connStr = GetConnectionString();
            string sqlStr = "";

            sqlStr = @"
                        SELECT 
                            [id]
                            ,[eventid]
                            ,[description]
                            ,[modifiedby]
                            ,[modifieddate]
                        FROM 
                            [TEL_Event_RegisterOption2] ";

            if (!string.IsNullOrEmpty(eventid))
            {
                sqlStr += @" 
                        WHERE  [eventid] = @eventid
                            ";
            }

            sqlStr += @"
                        ORDER BY [description]";

            DataTable result = null;

            using (SqlConnection connection = new SqlConnection(connStr))
            {
                connection.Open();

                SqlDataAdapter wrDad = new SqlDataAdapter();
                DataSet DS = new DataSet();

                wrDad.SelectCommand = new SqlCommand(sqlStr, connection);

                if (!string.IsNullOrEmpty(eventid))
                {
                    wrDad.SelectCommand.Parameters.AddWithValue("@eventid", eventid);
                }

                wrDad.Fill(DS, "T");
                result = DS.Tables["T"];
            }

            return result;
        }

        /// <summary>
        /// 查詢報名表選項（交通車是否存在）
        /// </summary>
        /// <param name="eventid"></param>
        /// <param name="transportation"></param>
        /// <returns></returns>
        internal DataTable QueryRegisterOption2(string eventid, string transportation)
        {
            string connStr = GetConnectionString();
            string sqlStr = "";

            sqlStr = @"
                        SELECT 
                            [id]
                            ,[eventid]
                            ,[description]
                            ,[modifiedby]
                            ,[modifieddate]
                        FROM 
                            [TEL_Event_RegisterOption2] 
                        WHERE [id]=[id] ";

            if (!string.IsNullOrEmpty(eventid))
            {
                sqlStr += @"AND [eventid] = @eventid ";
            }

            if (!string.IsNullOrEmpty(transportation))
            {
                sqlStr += @"AND [description] = @description ";
            }

            sqlStr += @"ORDER BY [description]";

            DataTable result = null;

            using (SqlConnection connection = new SqlConnection(connStr))
            {
                connection.Open();

                SqlDataAdapter wrDad = new SqlDataAdapter();
                DataSet DS = new DataSet();

                wrDad.SelectCommand = new SqlCommand(sqlStr, connection);

                if (!string.IsNullOrEmpty(eventid))
                {
                    wrDad.SelectCommand.Parameters.AddWithValue("@eventid", eventid);
                }

                if (!string.IsNullOrEmpty(transportation))
                {
                    wrDad.SelectCommand.Parameters.AddWithValue("@description", transportation);
                }

                wrDad.Fill(DS, "T");
                result = DS.Tables["T"];
            }

            return result;
        }

        /// <summary>
        /// 新增報名表選項（餐點內容列表）
        /// </summary>
        /// <param name="eventid"></param>
        /// <param name="transportation"></param>
        /// <param name="v"></param>
        /// <returns></returns>
        internal string InsertRegisterOption3(string eventid, string meal, string modifiedby)
        {
            string connStr = GetConnectionString();
            string sqlStr = "";

            try
            {
                sqlStr = @"
                        INSERT INTO 
                        TEL_Event_RegisterOption3
                            (
                                [id]
                                ,[eventid]
                                ,[description]
                                ,[modifiedby]
                                ,[modifieddate]
                            )
                        VALUES 
                            (
                                newid(),
                                @eventid,
                                @description,
                                @modifiedby,
                                GETDATE()
                            )";

                SqlConnection conn = new SqlConnection(connStr);
                conn.Open();
                SqlCommand command = new SqlCommand(sqlStr, conn);


                command.Parameters.Clear();
                command.Parameters.AddWithValue("@eventid", eventid);
                command.Parameters.AddWithValue("@description", meal);
                command.Parameters.AddWithValue("@modifiedby", modifiedby);

                command.ExecuteNonQuery();
                conn.Close();
                conn.Dispose();
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }

            return "";
        }

        /// <summary>
        /// 刪除餐點內容列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        internal string DeleteRegisterOption3(string id)
        {
            string connStr = GetConnectionString();
            string sqlStr = "";

            try
            {
                sqlStr = @"
                DELETE 
                    TEL_Event_RegisterOption3
                WHERE 
                    id = @id";

                SqlConnection sqlConn = new SqlConnection(connStr);
                sqlConn.Open();
                SqlCommand sqlcommand = new SqlCommand(sqlStr, sqlConn);
                sqlcommand.Parameters.Clear();
                sqlcommand.Parameters.AddWithValue("@id", id);
                sqlcommand.ExecuteNonQuery();
                sqlConn.Close();
                sqlConn.Dispose();
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }

            return "";

        }

        /// <summary>
        /// 取得餐點內容列表
        /// </summary>
        /// <param name="eventid"></param>
        /// <returns></returns>
        internal DataTable QueryRegisterOption3(string eventid)
        {
            string connStr = GetConnectionString();
            string sqlStr = "";

            sqlStr = @"
                        SELECT 
                            [id]
                            ,[eventid]
                            ,[description]
                            ,[modifiedby]
                            ,[modifieddate]
                        FROM 
                            [TEL_Event_RegisterOption3] ";

            if (!string.IsNullOrEmpty(eventid))
            {
                sqlStr += @" 
                        WHERE  [eventid] = @eventid
                            ";
            }

            sqlStr += @"
                        ORDER BY [description]";

            DataTable result = null;

            using (SqlConnection connection = new SqlConnection(connStr))
            {
                connection.Open();

                SqlDataAdapter wrDad = new SqlDataAdapter();
                DataSet DS = new DataSet();

                wrDad.SelectCommand = new SqlCommand(sqlStr, connection);

                if (!string.IsNullOrEmpty(eventid))
                {
                    wrDad.SelectCommand.Parameters.AddWithValue("@eventid", eventid);
                }

                wrDad.Fill(DS, "T");
                result = DS.Tables["T"];
            }

            return result;
        }

        /// <summary>
        /// 取得餐點內容列表 (餐點內容是否已存在)
        /// </summary>
        /// <param name="eventid"></param>
        /// <param name="meal"></param>
        /// <returns></returns>
        internal DataTable QueryRegisterOption3(string eventid, string meal)
        {
            string connStr = GetConnectionString();
            string sqlStr = "";

            sqlStr = @"
                        SELECT 
                            [id]
                            ,[eventid]
                            ,[description]
                            ,[modifiedby]
                            ,[modifieddate]
                        FROM 
                            [TEL_Event_RegisterOption3] 
                        WHERE [id]=[id] ";

            if (!string.IsNullOrEmpty(eventid))
            {
                sqlStr += @"AND [eventid] = @eventid ";
            }

            if (!string.IsNullOrEmpty(meal))
            {
                sqlStr += @"AND [description] = @description ";
            }

            sqlStr += @"ORDER BY [description]";

            DataTable result = null;

            using (SqlConnection connection = new SqlConnection(connStr))
            {
                connection.Open();

                SqlDataAdapter wrDad = new SqlDataAdapter();
                DataSet DS = new DataSet();

                wrDad.SelectCommand = new SqlCommand(sqlStr, connection);

                if (!string.IsNullOrEmpty(eventid))
                {
                    wrDad.SelectCommand.Parameters.AddWithValue("@eventid", eventid);
                }

                if (!string.IsNullOrEmpty(meal))
                {
                    wrDad.SelectCommand.Parameters.AddWithValue("@description", meal);
                }

                wrDad.Fill(DS, "T");
                result = DS.Tables["T"];
            }

            return result;
        }

        /// <summary>
        /// 新增健檢方案
        /// </summary>
        /// <param name="eventid"></param>
        /// <param name="list"></param>
        /// <param name="modifiedby"></param>
        /// <returns></returns>
        internal string InsertRegisterOption4(string eventid, List<ImportModel> list, string modifiedby)
        {
            string connStr = GetConnectionString();
            string sqlStr = string.Empty;

            SqlConnection conn = new SqlConnection(connStr);
            SqlTransaction transaction;

            conn.Open();
            transaction = conn.BeginTransaction();
            try
            {
                //先刪除
                sqlStr = @"
                DELETE 
                    TEL_Event_RegisterOption4 
                WHERE 
                    eventid = @eventid
                ";

                SqlCommand commandDelete = new SqlCommand(sqlStr, conn, transaction);
                commandDelete.Parameters.AddWithValue("@eventid", eventid);

                commandDelete.ExecuteNonQuery();

                //再新增
                foreach (ImportModel item in list)
                {
                    sqlStr = @"
                        INSERT INTO 
                        TEL_Event_RegisterOption4
                            (
                                [id]
                                ,[eventid]
                                ,[hosipital]
                                ,[area]
                                ,[description]
                                ,[gender]
                                ,[secondoption1]
                                ,[secondoption2]
                                ,[secondoption3]
                                ,[avaliabledate]
                                ,[limit]
                                ,[modifiedby]
                                ,[modifieddate]
                            )
                        VALUES 
                            (
                                @id
                                ,@eventid
                                ,@hosipital
                                ,@area
                                ,@description
                                ,@gender
                                ,@secondoption1
                                ,@secondoption2
                                ,@secondoption3
                                ,@avaliabledate
                                ,@limit
                                ,@modifiedby
                                ,GETDATE()
                            )";

                    SqlCommand command = new SqlCommand(sqlStr, conn, transaction);

                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@id", Guid.NewGuid().ToString());
                    command.Parameters.AddWithValue("@eventid", eventid);
                    command.Parameters.AddWithValue("@hosipital", item.hosipital);
                    command.Parameters.AddWithValue("@area", item.area);
                    command.Parameters.AddWithValue("@description", item.description);
                    command.Parameters.AddWithValue("@gender", item.gender);
                    command.Parameters.AddWithValue("@secondoption1", item.secondoption1);
                    command.Parameters.AddWithValue("@secondoption2", item.secondoption2);
                    command.Parameters.AddWithValue("@secondoption3", item.secondoption3);
                    command.Parameters.AddWithValue("@avaliabledate", item.avaliabledate);
                    command.Parameters.AddWithValue("@limit", item.limit);
                    command.Parameters.AddWithValue("@modifiedby", modifiedby);

                    command.ExecuteNonQuery();
                }

                transaction.Commit();
            }
            catch (SqlException ex)
            {
                transaction.Rollback();
                return ex.ToString();

            }
            catch (Exception ex)
            {
                transaction.Rollback();
                return ex.ToString();

            }

            conn.Close();
            conn.Dispose();

            return "";
        }

        /// <summary>
        /// 新增電腦更換地點
        /// </summary>
        /// <param name="eventid"></param>
        /// <param name="list"></param>
        /// <param name="modifiedby"></param>
        /// <returns></returns>
        internal string InsertRegisterOption6(string eventid, List<ImportModel> list, string modifiedby)
        {
            string connStr = GetConnectionString();
            string sqlStr = string.Empty;

            SqlConnection conn = new SqlConnection(connStr);
            SqlTransaction transaction;

            conn.Open();
            transaction = conn.BeginTransaction();
            try
            {
                //先刪除
                sqlStr = @"
                DELETE 
                    TEL_Event_RegisterOption6 
                WHERE 
                    eventid = @eventid
                ";

                SqlCommand commandDelete = new SqlCommand(sqlStr, conn, transaction);
                commandDelete.Parameters.AddWithValue("@eventid", eventid);

                commandDelete.ExecuteNonQuery();

                //再新增
                foreach (ImportModel item in list)
                {
                    sqlStr = @"
                        INSERT INTO 
                        TEL_Event_RegisterOption6
                            (
                                [id]
                                ,[eventid]
                                ,[area]
                                ,[avaliabledate]
                                ,[limit]
                                ,[modifiedby]
                                ,[modifieddate]
                            )
                        VALUES 
                            (
                                @id
                                ,@eventid
                                ,@area
                                ,@avaliabledate
                                ,@limit
                                ,@modifiedby
                                ,GETDATE()
                            )";

                    SqlCommand command = new SqlCommand(sqlStr, conn, transaction);

                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@id", Guid.NewGuid().ToString());
                    command.Parameters.AddWithValue("@eventid", eventid);
                    command.Parameters.AddWithValue("@area", item.area);
                    command.Parameters.AddWithValue("@avaliabledate", item.avaliabledate);
                    command.Parameters.AddWithValue("@limit", item.limit);
                    command.Parameters.AddWithValue("@modifiedby", modifiedby);

                    command.ExecuteNonQuery();
                }

                transaction.Commit();
            }
            catch (SqlException ex)
            {
                transaction.Rollback();
                return ex.ToString();

            }

            conn.Close();
            conn.Dispose();

            return "";
        }

        /// <summary>
        /// 新增健檢包寄送地點
        /// </summary>
        /// <param name="eventid"></param>
        /// <param name="sendArea"></param>
        /// <param name="modifiedby"></param>
        /// <returns></returns>
        internal string InsertRegisterOption5(string eventid, string sendArea, string modifiedby)
        {
            string connStr = GetConnectionString();
            string sqlStr = "";

            try
            {
                sqlStr = @"
                        INSERT INTO 
                        TEL_Event_RegisterOption5
                            (
                                [id]
                                ,[eventid]
                                ,[description]
                                ,[modifiedby]
                                ,[modifieddate]
                            )
                        VALUES 
                            (
                                newid(),
                                @eventid,
                                @description,
                                @modifiedby,
                                GETDATE()
                            )";

                SqlConnection conn = new SqlConnection(connStr);
                conn.Open();
                SqlCommand command = new SqlCommand(sqlStr, conn);


                command.Parameters.Clear();
                command.Parameters.AddWithValue("@eventid", eventid);
                command.Parameters.AddWithValue("@description", sendArea);
                command.Parameters.AddWithValue("@modifiedby", modifiedby);

                command.ExecuteNonQuery();
                conn.Close();
                conn.Dispose();
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }

            return "";
        }

        /// <summary>
        /// 刪除健檢包寄送地點
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        internal string DeleteRegisterOption5(string id)
        {
            string connStr = GetConnectionString();
            string sqlStr = "";

            try
            {
                sqlStr = @"
                DELETE 
                    TEL_Event_RegisterOption5
                WHERE 
                    id = @id";

                SqlConnection sqlConn = new SqlConnection(connStr);
                sqlConn.Open();
                SqlCommand sqlcommand = new SqlCommand(sqlStr, sqlConn);
                sqlcommand.Parameters.Clear();
                sqlcommand.Parameters.AddWithValue("@id", id);
                sqlcommand.ExecuteNonQuery();
                sqlConn.Close();
                sqlConn.Dispose();
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }

            return "";

        }



        /// <summary>
        /// 查詢健檢包寄送地點
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        internal DataTable QueryRegisterOption5(string eventid)
        {
            string connStr = GetConnectionString();
            string sqlStr = "";

            sqlStr = @"
                        SELECT 
                            [id]
                            ,[eventid]
                            ,[description]
                            ,[modifiedby]
                            ,[modifieddate]
                        FROM 
                            [TEL_Event_RegisterOption5] ";

            if (!string.IsNullOrEmpty(eventid))
            {
                sqlStr += @" 
                        WHERE  [eventid] = @eventid
                            ";
            }

            sqlStr += @"
                        ORDER BY [description]";

            DataTable result = null;

            using (SqlConnection connection = new SqlConnection(connStr))
            {
                connection.Open();

                SqlDataAdapter wrDad = new SqlDataAdapter();
                DataSet DS = new DataSet();

                wrDad.SelectCommand = new SqlCommand(sqlStr, connection);

                if (!string.IsNullOrEmpty(eventid))
                {
                    wrDad.SelectCommand.Parameters.AddWithValue("@eventid", eventid);
                }

                wrDad.Fill(DS, "T");
                result = DS.Tables["T"];
            }

            return result;
        }

        /// <summary>
        /// 查詢健檢包寄送地點 (健檢包寄送地點是否已存在)
        /// </summary>
        /// <param name="name"></param>
        /// <param name="sendArea"></param>
        /// <returns></returns>
        internal DataTable QueryRegisterOption5(string eventid, string sendArea)
        {
            string connStr = GetConnectionString();
            string sqlStr = "";

            sqlStr = @"
                        SELECT 
                            [id]
                            ,[eventid]
                            ,[description]
                            ,[modifiedby]
                            ,[modifieddate]
                        FROM 
                            [TEL_Event_RegisterOption5] 
                        WHERE [id]=[id] ";

            if (!string.IsNullOrEmpty(eventid))
            {
                sqlStr += @"AND [eventid] = @eventid ";
            }

            if (!string.IsNullOrEmpty(sendArea))
            {
                sqlStr += @"AND [description] = @description ";
            }

            sqlStr += @"ORDER BY [description] ";

            DataTable result = null;

            using (SqlConnection connection = new SqlConnection(connStr))
            {
                connection.Open();

                SqlDataAdapter wrDad = new SqlDataAdapter();
                DataSet DS = new DataSet();

                wrDad.SelectCommand = new SqlCommand(sqlStr, connection);

                if (!string.IsNullOrEmpty(eventid))
                {
                    wrDad.SelectCommand.Parameters.AddWithValue("@eventid", eventid);
                }

                if (!string.IsNullOrEmpty(sendArea))
                {
                    wrDad.SelectCommand.Parameters.AddWithValue("@description", sendArea);
                }

                wrDad.Fill(DS, "T");
                result = DS.Tables["T"];
            }

            return result;
        }

        /// <summary>
        /// 查詢健檢方案
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        internal DataTable QueryRegisterOption4(string eventid)
        {
            string connStr = GetConnectionString();
            string sqlStr = "";

            sqlStr = @"
                        SELECT 
                            [id]
                            ,[eventid]
                            ,[hosipital]
                            ,[area]
                            ,[description]
                            ,[gender]
                            ,[secondoption1]
                            ,[secondoption2]
                            ,[secondoption3]
                            ,CONVERT(varchar, [avaliabledate], 111) as avaliabledate
                            ,[limit]
                            ,[modifiedby]
                            ,[modifieddate]
                        FROM 
                            [TEL_Event_RegisterOption4] ";

            if (!string.IsNullOrEmpty(eventid))
            {
                sqlStr += @" 
                        WHERE  [eventid] = @eventid
                            ";
            }
            sqlStr += @"
                        ORDER BY [hosipital], area, description, avaliabledate, gender";

            DataTable result = null;

            using (SqlConnection connection = new SqlConnection(connStr))
            {
                connection.Open();

                SqlDataAdapter wrDad = new SqlDataAdapter();
                DataSet DS = new DataSet();

                wrDad.SelectCommand = new SqlCommand(sqlStr, connection);

                if (!string.IsNullOrEmpty(eventid))
                {
                    wrDad.SelectCommand.Parameters.AddWithValue("@eventid", eventid);
                }

                wrDad.Fill(DS, "T");
                result = DS.Tables["T"];
            }

            return result;
        }

        /// <summary>
        /// 查詢電腦更換地點
        /// </summary>
        /// <param name="eventid"></param>
        /// <returns></returns>
        internal DataTable QueryRegisterOption6(string eventid)
        {
            string connStr = GetConnectionString();
            string sqlStr = "";

            sqlStr = @"
                        SELECT 
                            [id]
                            ,[eventid]
                            ,[area]
                            ,Format([avaliabledate],N'yyyy/MM/dd HH:mm') as avaliabledate
                            ,[limit]
                            ,[modifiedby]
                            ,[modifieddate]
                        FROM 
                            [TEL_Event_RegisterOption6] ";

            if (!string.IsNullOrEmpty(eventid))
            {
                sqlStr += @" 
                        WHERE  [eventid] = @eventid
                            ";
            }
            sqlStr += @"
                        ORDER BY area, avaliabledate";

            DataTable result = null;

            using (SqlConnection connection = new SqlConnection(connStr))
            {
                connection.Open();

                SqlDataAdapter wrDad = new SqlDataAdapter();
                DataSet DS = new DataSet();

                wrDad.SelectCommand = new SqlCommand(sqlStr, connection);

                if (!string.IsNullOrEmpty(eventid))
                {
                    wrDad.SelectCommand.Parameters.AddWithValue("@eventid", eventid);
                }

                wrDad.Fill(DS, "T");
                result = DS.Tables["T"];
            }

            return result;
        }

        /// <summary>
        /// 更新問券發送時間
        /// </summary>
        /// <param name="eventid"></param>
        internal void UpdateEventSurveyStartDate(string eventid)
        {
            string connStr = GetConnectionString();
            string sqlStr = "";

            try
            {
                sqlStr = @"
                        UPDATE
                            TEL_Event_Events
                        SET
                            [surveystartdate] = GETDATE()
                        WHERE
                            id = @id";

                SqlConnection conn = new SqlConnection(connStr);
                conn.Open();
                SqlCommand command = new SqlCommand(sqlStr, conn);


                command.Parameters.Clear();

                command.Parameters.AddWithValue("@id", eventid);

                command.ExecuteNonQuery();
                conn.Close();
                conn.Dispose();
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// 取得活動權限 by MailGroup
        /// </summary>
        /// <param name="mailgroup"></param>
        /// <returns></returns>
        internal DataTable QueryEventPermissionMailGroup(string mailgroup = "")
        {
            string connStr = GetConnectionString();
            string sqlStr = "";

            sqlStr = @"
                        SELECT 
                            [eventid]
                            ,[mailgroupName]
                            ,[modifiedby]
                            ,[modifieddate]
                        FROM 
                            [TEL_Event_Permission_MailGroup] ";

            if (!string.IsNullOrEmpty(mailgroup))
            {
                sqlStr += @" 
                        WHERE  [mailgroupName] = @mailgroup
                            ";
            }

            DataTable result = null;

            using (SqlConnection connection = new SqlConnection(connStr))
            {
                connection.Open();

                SqlDataAdapter wrDad = new SqlDataAdapter();
                DataSet DS = new DataSet();

                wrDad.SelectCommand = new SqlCommand(sqlStr, connection);

                if (!string.IsNullOrEmpty(mailgroup))
                {
                    wrDad.SelectCommand.Parameters.AddWithValue("@mailgroup", mailgroup);
                }

                wrDad.Fill(DS, "T");
                result = DS.Tables["T"];
            }

            return result;
        }

        #region RegisterModel1

        //取得活動選項報名人數
        public int QueryEvnetRegisterOption1RegisterCount(string eventid, string optionid, string registerid)
        {
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["tel_event"].ConnectionString;
            string sqlString = "";

            sqlString = @"
                            SELECT 
                                COUNT(selectedoption) AS count 
                            FROM 
                                TEL_Event_RegisterModel1 
                            WHERE 
                                eventid = @eventid
                            AND
                                selectedoption = @optionid";

            if (!string.IsNullOrEmpty(registerid))
            {
                sqlString += @"
                            AND
                                id != @registerid ";
            }



            DataTable result = null;

            using (SqlConnection connection = new SqlConnection(connStr))
            {
                connection.Open();

                SqlDataAdapter wrDad = new SqlDataAdapter();
                DataSet DS = new DataSet();

                wrDad.SelectCommand = new SqlCommand(sqlString, connection);
                wrDad.SelectCommand.Parameters.AddWithValue("@eventid", eventid);
                wrDad.SelectCommand.Parameters.AddWithValue("@optionid", optionid);

                if (!string.IsNullOrEmpty(registerid))
                    wrDad.SelectCommand.Parameters.AddWithValue("@registerid", registerid);



                wrDad.Fill(DS, "T");
                result = DS.Tables["T"];
            }

            return Convert.ToInt16(result.Rows[0]["count"].ToString());
        }

        /// <summary>
        /// 新增 模板1報名資料
        /// </summary>
        /// <param name="eventsData"></param>
        /// <param name="modifiedby"></param>
        internal string InsertRegisterModel1(Dictionary<string, string> eventsData, string modifiedby)
        {
            string connStr = GetConnectionString();
            string sqlStr = "";

            try
            {
                sqlStr = @"
                    INSERT INTO [TEL_Event_RegisterModel1]
                           ([id]
                           ,[eventid]
                           ,[empid]
                           ,[registerdate]
                           ,[selectedoption]
                           ,[feedback]
                           ,[modifiedby]
                           ,[modifieddate])
                     VALUES
                           (@id
                           ,@eventid
                           ,@empid
                           ,@registerdate
                           ,@selectedoption
                           ,@feedback
                           ,@modifiedby
                           ,GETDATE()
                            ) ";

                SqlConnection conn = new SqlConnection(connStr);
                conn.Open();
                SqlCommand command = new SqlCommand(sqlStr, conn);


                command.Parameters.Clear();
                command.Parameters.AddWithValue("@id", eventsData["id"]);
                command.Parameters.AddWithValue("@eventid", eventsData["eventid"]);
                command.Parameters.AddWithValue("@empid", eventsData["empid"]);
                command.Parameters.AddWithValue("@registerdate", eventsData["registerdate"]);
                command.Parameters.AddWithValue("@selectedoption", eventsData["selectedoption"]);
                command.Parameters.AddWithValue("@feedback", eventsData["feedback"]);
                command.Parameters.AddWithValue("@modifiedby", modifiedby);

                command.ExecuteNonQuery();
                conn.Close();
                conn.Dispose();

                //Add log
                SqlLog sl = new SqlLog();
                sl.AddLog("新增模板1報名資料", eventsData["id"], sl.GetCommendText(command), modifiedby);

            }
            catch (Exception ex)
            {
                return $"ErrorMsg：{ex.Message}{Environment.NewLine}ErrorStackTrace：{ex.StackTrace}";
            }

            return "";
        }

        /// <summary>
        /// 更新 模板1報名資料
        /// </summary>
        /// <param name="eventsData"></param>
        /// <param name="modifiedby"></param>
        /// <returns></returns>
        internal string UpdateRegisterModel1(Dictionary<string, string> eventsData, string modifiedby)
        {
            string connStr = GetConnectionString();
            string sqlStr = "";

            try
            {
                sqlStr = @"
                        UPDATE [TEL_Event_RegisterModel1]
                        SET 
                            [selectedoption] = @selectedoption
                            ,[feedback] = @feedback
                            ,[modifiedby] = @modifiedby
                            ,[modifieddate] = GETDATE()
                        WHERE id = @id ";

                SqlConnection conn = new SqlConnection(connStr);
                conn.Open();
                SqlCommand command = new SqlCommand(sqlStr, conn);


                command.Parameters.Clear();
                command.Parameters.AddWithValue("@id", eventsData["id"]);
                command.Parameters.AddWithValue("@selectedoption", eventsData["selectedoption"]);
                command.Parameters.AddWithValue("@feedback", eventsData["feedback"]);
                command.Parameters.AddWithValue("@modifiedby", modifiedby);

                command.ExecuteNonQuery();
                conn.Close();
                conn.Dispose();

                //Add log
                SqlLog sl = new SqlLog();
                sl.AddLog("新增模板1報名資料", eventsData["id"], sl.GetCommendText(command), modifiedby);
            }
            catch (Exception ex)
            {
                return $"ErrorMsg：{ex.Message}{Environment.NewLine}ErrorStackTrace：{ex.StackTrace}";
            }

            return "";
        }

        /// <summary>
        /// 刪除 模板1報名資料
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        internal string DeleteRegisterModel1(string id, string modifiedby)
        {
            string connStr = GetConnectionString();
            string sqlStr = "";

            try
            {
                sqlStr = @"
                        DELETE FROM [TEL_Event_RegisterModel1]
                        WHERE id = @id ";

                SqlConnection conn = new SqlConnection(connStr);
                conn.Open();
                SqlCommand command = new SqlCommand(sqlStr, conn);


                command.Parameters.Clear();
                command.Parameters.AddWithValue("@id", id);

                command.ExecuteNonQuery();
                conn.Close();
                conn.Dispose();


                //Add log
                SqlLog sl = new SqlLog();
                sl.AddLog("刪除模板1報名資料", id, sl.GetCommendText(command), modifiedby);
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }

            return "";
        }

        /// <summary>
        /// 查詢 模板1報名資料
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataTable QueryRegisterModel1(string id)
        {
            string connStr = GetConnectionString();
            string sqlString = "";

            sqlString = @"
                            SELECT [id]
                                ,[eventid]
                                ,[empid]
                                ,[registerdate]
                                ,[selectedoption]
                                ,[feedback]
                                ,[modifiedby]
                                ,[modifieddate]
                            FROM 
                                [TEL_Event_RegisterModel1]
                            WHERE 
                                id = id
                            ";

            if (!string.IsNullOrEmpty(id))
            {
                sqlString += @"
                            AND id = @id
                                ";
            }

            DataTable result = null;

            using (SqlConnection connection = new SqlConnection(connStr))
            {
                connection.Open();

                SqlDataAdapter wrDad = new SqlDataAdapter();
                DataSet DS = new DataSet();

                wrDad.SelectCommand = new SqlCommand(sqlString, connection);

                if (!string.IsNullOrEmpty(id))
                    wrDad.SelectCommand.Parameters.AddWithValue("@id", id);


                wrDad.Fill(DS, "T");
                result = DS.Tables["T"];
            }

            return result;
        }
        #endregion

        #region RegisterModel2

        /// <summary>
        /// 取得 欲參加的內容 在RegisterModel2 已報名的人數
        /// </summary>
        /// <param name="eventid"></param>
        /// <param name="optionid"></param>
        /// <param name="registerid"></param>
        /// <returns></returns>
        public int QueryOption1RegisterCountByRegisterModel2(string eventid, string optionid, string registerid)
        {
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["tel_event"].ConnectionString;
            string sqlString = "";

            sqlString = @"
                            SELECT 
                                COUNT(selectedoption) AS count 
                            FROM 
                                TEL_Event_RegisterModel2
                            WHERE 
                                eventid = @eventid
                            AND
                                selectedoption = @optionid";

            if (!string.IsNullOrEmpty(registerid))
            {
                sqlString += @"
                            AND
                                id != @registerid ";
            }



            DataTable result = null;

            using (SqlConnection connection = new SqlConnection(connStr))
            {
                connection.Open();

                SqlDataAdapter wrDad = new SqlDataAdapter();
                DataSet DS = new DataSet();

                wrDad.SelectCommand = new SqlCommand(sqlString, connection);
                wrDad.SelectCommand.Parameters.AddWithValue("@eventid", eventid);
                wrDad.SelectCommand.Parameters.AddWithValue("@optionid", optionid);

                if (!string.IsNullOrEmpty(registerid))
                    wrDad.SelectCommand.Parameters.AddWithValue("@registerid", registerid);



                wrDad.Fill(DS, "T");
                result = DS.Tables["T"];
            }

            return Convert.ToInt16(result.Rows[0]["count"].ToString());
        }

        /// <summary>
        /// 取得 欲參加的內容 在 RegisterModel2family 已報名的人數
        /// </summary>
        /// <param name="eventid"></param>
        /// <param name="optionid"></param>
        /// <param name="registerid"></param>
        /// <returns></returns>
        public int QueryOption1RegisterCountByRegisterModel2family(string eventid, string optionid, string registerid)
        {
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["tel_event"].ConnectionString;
            string sqlString = "";

            sqlString = @"
                            SELECT 
                                COUNT(selectedoption) AS count 
                            FROM 
                                TEL_Event_RegisterModel2 a
                            LEFT JOIN 
                                TEL_Event_RegisterModel2family b on a.id = b.registerid
                            WHERE 
                                a.eventid = @eventid
                            AND
                                a.selectedoption = @optionid";

            if (!string.IsNullOrEmpty(registerid))
            {
                sqlString += @"
                            AND
                                a.id != @registerid ";
            }



            DataTable result = null;

            using (SqlConnection connection = new SqlConnection(connStr))
            {
                connection.Open();

                SqlDataAdapter wrDad = new SqlDataAdapter();
                DataSet DS = new DataSet();

                wrDad.SelectCommand = new SqlCommand(sqlString, connection);
                wrDad.SelectCommand.Parameters.AddWithValue("@eventid", eventid);
                wrDad.SelectCommand.Parameters.AddWithValue("@optionid", optionid);

                if (!string.IsNullOrEmpty(registerid))
                    wrDad.SelectCommand.Parameters.AddWithValue("@registerid", registerid);



                wrDad.Fill(DS, "T");
                result = DS.Tables["T"];
            }

            return Convert.ToInt16(result.Rows[0]["count"].ToString());
        }

        /// <summary>
        /// 新增 模板2報名資料
        /// </summary>
        /// <param name="eventsData"></param>
        /// <param name="dataTable"></param>
        /// <param name="modifiedby"></param>
        /// <returns></returns>
        internal string InsertRegisterModel2(Dictionary<string, string> eventsData, DataTable dataTable, string modifiedby)
        {
            string connStr = GetConnectionString();
            string sqlStr = "";

            SqlConnection conn = new SqlConnection(connStr);
            SqlTransaction transaction;

            conn.Open();
            transaction = conn.BeginTransaction();

            try
            {
                sqlStr = @"
                    INSERT INTO [TEL_Event_RegisterModel2]
                           ([id]
                            ,[eventid]
                            ,[empid]
                            ,[registerdate]
                            ,[selectedoption]
                            ,[mobile]
                            ,[traffic]
                            ,[meal]
                            ,[feedback]
                            ,[modifiedby]
                            ,[modifieddate])
                     VALUES
                           (@id
                           ,@eventid
                           ,@empid
                           ,@registerdate
                           ,@selectedoption
                           ,@mobile
                           ,@traffic
                           ,@meal
                           ,@feedback
                           ,@modifiedby
                           ,GETDATE()
                            ) ";


                SqlCommand command = new SqlCommand(sqlStr, conn, transaction);


                command.Parameters.Clear();
                command.Parameters.AddWithValue("@id", eventsData["id"]);
                command.Parameters.AddWithValue("@eventid", eventsData["eventid"]);
                command.Parameters.AddWithValue("@empid", eventsData["empid"]);
                command.Parameters.AddWithValue("@registerdate", eventsData["registerdate"]);
                command.Parameters.AddWithValue("@selectedoption", eventsData["selectedoption"]);
                command.Parameters.AddWithValue("@feedback", eventsData["feedback"]);
                command.Parameters.AddWithValue("@mobile", eventsData["mobile"]);
                command.Parameters.AddWithValue("@traffic", eventsData["traffic"]);
                command.Parameters.AddWithValue("@meal", eventsData["meal"]);
                command.Parameters.AddWithValue("@modifiedby", modifiedby);

                command.ExecuteNonQuery();

                foreach (DataRow dr in dataTable.Rows)
                {
                    sqlStr = @"
                        INSERT INTO [TEL_Event_RegisterModel2family]
                               ([id]
                                ,[registerid]
                                ,[name]
                                ,[idno]
                                ,[birthday]
                                ,[gender]
                                ,[meal]
                                ,[modifiedby]
                                ,[modifieddate])
                         VALUES
                               (@id
                               ,@registerid
                               ,@name
                               ,@idno
                               ,@birthday
                               ,@gender
                               ,@meal
                               ,@modifiedby
                               ,GETDATE())
                    ";

                    SqlCommand commandRegisterModel2family = new SqlCommand(sqlStr, conn, transaction);

                    commandRegisterModel2family.Parameters.Clear();
                    commandRegisterModel2family.Parameters.AddWithValue("@id", Guid.NewGuid().ToString());
                    commandRegisterModel2family.Parameters.AddWithValue("@registerid", eventsData["id"]);
                    commandRegisterModel2family.Parameters.AddWithValue("@name", dr["name"].ToString());
                    commandRegisterModel2family.Parameters.AddWithValue("@idno", dr["idno"].ToString());
                    commandRegisterModel2family.Parameters.AddWithValue("@birthday", dr["birthday"].ToString());
                    commandRegisterModel2family.Parameters.AddWithValue("@gender", dr["gender"].ToString());
                    commandRegisterModel2family.Parameters.AddWithValue("@meal", dr["meal"].ToString());
                    commandRegisterModel2family.Parameters.AddWithValue("@modifiedby", modifiedby);

                    commandRegisterModel2family.ExecuteNonQuery();
                }

                transaction.Commit();

                //Add log
                SqlLog sl = new SqlLog();
                sl.AddLog("新增模板2報名資料", eventsData["id"], sl.GetCommendText(command), modifiedby);
            }
            catch (SqlException ex)
            {
                transaction.Rollback();
                return $"ErrorMsg：{ex.Message}{Environment.NewLine}ErrorStackTrace：{ex.StackTrace}";
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                return $"ErrorMsg：{ex.Message}{Environment.NewLine}ErrorStackTrace：{ex.StackTrace}";
            }

            conn.Close();
            conn.Dispose();

            return "";
        }

        /// <summary>
        /// 更新 模板2報名資料
        /// </summary>
        /// <param name="eventsData"></param>
        /// <param name="dataTable"></param>
        /// <param name="modifiedby"></param>
        /// <returns></returns>
        internal string UpdateRegisterModel2(Dictionary<string, string> eventsData, DataTable dataTable, string modifiedby)
        {
            string connStr = GetConnectionString();
            string sqlStr = "";

            SqlConnection conn = new SqlConnection(connStr);
            SqlTransaction transaction;

            conn.Open();
            transaction = conn.BeginTransaction();

            try
            {
                sqlStr = @"
                        UPDATE 
                            [TEL_Event_RegisterModel2]
                        SET 
                            [registerdate] = @registerdate
                            ,[selectedoption] = @selectedoption
                            ,[mobile] = @mobile
                            ,[traffic] = @traffic
                            ,[meal] = @meal
                            ,[feedback] = @feedback
                            ,[modifiedby] = @modifiedby
                            ,[modifieddate] = GETDATE()
                        WHERE 
                            id = @id";


                SqlCommand command = new SqlCommand(sqlStr, conn, transaction);


                command.Parameters.Clear();
                command.Parameters.AddWithValue("@id", eventsData["id"]);
                command.Parameters.AddWithValue("@registerdate", eventsData["registerdate"]);
                command.Parameters.AddWithValue("@selectedoption", eventsData["selectedoption"]);
                command.Parameters.AddWithValue("@feedback", eventsData["feedback"]);
                command.Parameters.AddWithValue("@mobile", eventsData["mobile"]);
                command.Parameters.AddWithValue("@traffic", eventsData["traffic"]);
                command.Parameters.AddWithValue("@meal", eventsData["meal"]);
                command.Parameters.AddWithValue("@modifiedby", modifiedby);

                command.ExecuteNonQuery();

                sqlStr = @"
                        DELETE FROM [TEL_Event_RegisterModel2family]
                        WHERE [registerid] = @id
                    ";

                SqlCommand commandDelete = new SqlCommand(sqlStr, conn, transaction);
                commandDelete.Parameters.Clear();
                commandDelete.Parameters.AddWithValue("@id", eventsData["id"]);
                commandDelete.ExecuteNonQuery();

                foreach (DataRow dr in dataTable.Rows)
                {
                    sqlStr = @"
                        INSERT INTO [TEL_Event_RegisterModel2family]
                               ([id]
                                ,[registerid]
                                ,[name]
                                ,[idno]
                                ,[birthday]
                                ,[gender]
                                ,[meal]
                                ,[modifiedby]
                                ,[modifieddate])
                         VALUES
                               (@id
                               ,@registerid
                               ,@name
                               ,@idno
                               ,@birthday
                               ,@gender
                               ,@meal
                               ,@modifiedby
                               ,GETDATE())
                    ";

                    SqlCommand commandRegisterModel2family = new SqlCommand(sqlStr, conn, transaction);

                    commandRegisterModel2family.Parameters.Clear();
                    commandRegisterModel2family.Parameters.AddWithValue("@id", Guid.NewGuid().ToString());
                    commandRegisterModel2family.Parameters.AddWithValue("@registerid", eventsData["id"]);
                    commandRegisterModel2family.Parameters.AddWithValue("@name", dr["name"].ToString());
                    commandRegisterModel2family.Parameters.AddWithValue("@idno", dr["idno"].ToString());
                    commandRegisterModel2family.Parameters.AddWithValue("@birthday", dr["birthday"].ToString());
                    commandRegisterModel2family.Parameters.AddWithValue("@gender", dr["gender"].ToString());
                    commandRegisterModel2family.Parameters.AddWithValue("@meal", dr["meal"].ToString());
                    commandRegisterModel2family.Parameters.AddWithValue("@modifiedby", modifiedby);

                    commandRegisterModel2family.ExecuteNonQuery();
                }

                transaction.Commit();

                //Add log
                SqlLog sl = new SqlLog();
                sl.AddLog("更新模板2報名資料", eventsData["id"], sl.GetCommendText(command), modifiedby);
            }
            catch (SqlException ex)
            {
                transaction.Rollback();
                return $"ErrorMsg：{ex.Message}{Environment.NewLine}ErrorStackTrace：{ex.StackTrace}";
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                return $"ErrorMsg：{ex.Message}{Environment.NewLine}ErrorStackTrace：{ex.StackTrace}";
            }

            conn.Close();
            conn.Dispose();

            return "";
        }

        /// <summary>
        /// 刪除 模板2報名資料
        /// </summary>
        /// <param name="eventsData"></param>
        /// <param name="dataTable"></param>
        /// <param name="modifiedby"></param>
        /// <returns></returns>
        internal string DeleteRegisterModel2(string id, string modifiedby)
        {
            string connStr = GetConnectionString();
            string sqlStr = "";

            SqlConnection conn = new SqlConnection(connStr);
            SqlTransaction transaction;

            conn.Open();
            transaction = conn.BeginTransaction();

            try
            {
                sqlStr = @"
                        DELETE FROM [TEL_Event_RegisterModel2]
                        WHERE 
                            id = @id";


                SqlCommand command = new SqlCommand(sqlStr, conn, transaction);
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@id", id);

                command.ExecuteNonQuery();

                sqlStr = @"
                        DELETE FROM [TEL_Event_RegisterModel2family]
                        WHERE [registerid] = @id
                    ";

                SqlCommand commandDelete = new SqlCommand(sqlStr, conn, transaction);
                commandDelete.Parameters.Clear();
                commandDelete.Parameters.AddWithValue("@id", id);
                commandDelete.ExecuteNonQuery();

                transaction.Commit();

                //Add log
                SqlLog sl = new SqlLog();
                sl.AddLog("刪除模板2報名資料", id, sl.GetCommendText(command), modifiedby);
            }
            catch (SqlException ex)
            {
                transaction.Rollback();
                return $"ErrorMsg：{ex.Message}{Environment.NewLine}ErrorStackTrace：{ex.StackTrace}";
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                return $"ErrorMsg：{ex.Message}{Environment.NewLine}ErrorStackTrace：{ex.StackTrace}";
            }

            conn.Close();
            conn.Dispose();

            return "";
        }

        /// <summary>
        /// 查詢 模板2報名資料
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataTable QueryRegisterModel2(string id)
        {
            string connStr = GetConnectionString();
            string sqlString = "";

            sqlString = @"
                        SELECT 
                            [id]
                            ,[eventid]
                            ,[empid]
                            ,[registerdate]
                            ,[selectedoption]
                            ,[mobile]
                            ,[traffic]
                            ,[meal]
                            ,[feedback]
                            ,[modifiedby]
                            ,[modifieddate]
                        FROM [TEL_Event_RegisterModel2]
                        WHERE
                            id = id
                            ";

            if (!string.IsNullOrEmpty(id))
            {
                sqlString += @"
                            AND id = @id
                                ";
            }

            DataTable result = null;

            using (SqlConnection connection = new SqlConnection(connStr))
            {
                connection.Open();

                SqlDataAdapter wrDad = new SqlDataAdapter();
                DataSet DS = new DataSet();

                wrDad.SelectCommand = new SqlCommand(sqlString, connection);

                if (!string.IsNullOrEmpty(id))
                    wrDad.SelectCommand.Parameters.AddWithValue("@id", id);

                wrDad.Fill(DS, "T");
                result = DS.Tables["T"];
            }

            return result;
        }

        //<summary>
        /// 查詢 模板2報名資料family
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataTable QueryRegisterModel2family(string id)
        {
            string connStr = GetConnectionString();
            string sqlString = "";

            sqlString = @"
                        SELECT 
                            [id]
                            ,[registerid]
                            ,[name]
                            ,[idno]
                            ,CONVERT(varchar,[birthday],111) as birthday
                            ,[gender]
                            ,[meal]
                            ,[modifiedby]
                            ,[modifieddate]
                        FROM [TEL_Event_RegisterModel2family]
                        WHERE
                            id = id
                            ";

            if (!string.IsNullOrEmpty(id))
            {
                sqlString += @"
                            AND registerid = @id
                                ";
            }

            DataTable result = null;

            using (SqlConnection connection = new SqlConnection(connStr))
            {
                connection.Open();

                SqlDataAdapter wrDad = new SqlDataAdapter();
                DataSet DS = new DataSet();

                wrDad.SelectCommand = new SqlCommand(sqlString, connection);

                if (!string.IsNullOrEmpty(id))
                    wrDad.SelectCommand.Parameters.AddWithValue("@id", id);

                wrDad.Fill(DS, "T");
                result = DS.Tables["T"];
            }

            return result;
        }
        #endregion

        #region RegisterModel3
        public DataTable QueryHosipitalOption(string eventid)
        {
            string connStr = GetConnectionString();
            string sqlString = "";

            sqlString = @"
                        SELECT 
                            DISTINCT [hosipital]
                        FROM 
                        [TEL_Event_RegisterOption4]
                                            WHERE
                                                id = id
                            ";

            if (!string.IsNullOrEmpty(eventid))
            {
                sqlString += @"
                            AND eventid = @id
                                ";
            }

            DataTable result = null;

            using (SqlConnection connection = new SqlConnection(connStr))
            {
                connection.Open();

                SqlDataAdapter wrDad = new SqlDataAdapter();
                DataSet DS = new DataSet();

                wrDad.SelectCommand = new SqlCommand(sqlString, connection);

                if (!string.IsNullOrEmpty(eventid))
                    wrDad.SelectCommand.Parameters.AddWithValue("@id", eventid);

                wrDad.Fill(DS, "T");
                result = DS.Tables["T"];
            }

            return result;
        }

        internal DataTable QueryAreaOption(string eventid, string hosipital)
        {
            string connStr = GetConnectionString();
            string sqlString = "";

            sqlString = @"
                        SELECT 
                            DISTINCT [area]
                        FROM 
                        [TEL_Event_RegisterOption4]
                                            WHERE
                                                id = id
                            ";

            if (!string.IsNullOrEmpty(eventid))
            {
                sqlString += @"
                            AND eventid = @id
                                ";
            }

            if (!string.IsNullOrEmpty(hosipital))
            {
                sqlString += @"
                            AND hosipital = @hosipital
                                ";
            }

            DataTable result = null;

            using (SqlConnection connection = new SqlConnection(connStr))
            {
                connection.Open();

                SqlDataAdapter wrDad = new SqlDataAdapter();
                DataSet DS = new DataSet();

                wrDad.SelectCommand = new SqlCommand(sqlString, connection);

                if (!string.IsNullOrEmpty(eventid))
                    wrDad.SelectCommand.Parameters.AddWithValue("@id", eventid);

                if (!string.IsNullOrEmpty(hosipital))
                    wrDad.SelectCommand.Parameters.AddWithValue("@hosipital", hosipital);

                wrDad.Fill(DS, "T");
                result = DS.Tables["T"];
            }

            return result;
        }

        internal DataTable QuerySolutionOption(string eventid, string hosipital, string area)
        {
            string connStr = GetConnectionString();
            string sqlString = "";

            sqlString = @"
                        SELECT 
                            DISTINCT [description]
                        FROM 
                            [TEL_Event_RegisterOption4]
                        WHERE
                            id = id
                            ";

            if (!string.IsNullOrEmpty(eventid))
            {
                sqlString += @"
                            AND eventid = @id
                                ";
            }

            if (!string.IsNullOrEmpty(hosipital))
            {
                sqlString += @"
                            AND hosipital = @hosipital
                                ";
            }

            if (!string.IsNullOrEmpty(area))
            {
                sqlString += @"
                            AND area = @area
                                ";
            }

            DataTable result = null;

            using (SqlConnection connection = new SqlConnection(connStr))
            {
                connection.Open();

                SqlDataAdapter wrDad = new SqlDataAdapter();
                DataSet DS = new DataSet();

                wrDad.SelectCommand = new SqlCommand(sqlString, connection);

                if (!string.IsNullOrEmpty(eventid))
                    wrDad.SelectCommand.Parameters.AddWithValue("@id", eventid);

                if (!string.IsNullOrEmpty(hosipital))
                    wrDad.SelectCommand.Parameters.AddWithValue("@hosipital", hosipital);

                if (!string.IsNullOrEmpty(area))
                    wrDad.SelectCommand.Parameters.AddWithValue("@area", area);

                wrDad.Fill(DS, "T");
                result = DS.Tables["T"];
            }

            return result;
        }
        internal DataTable QueryGenderOption(string eventid, string hosipital, string area, string solution)
        {
            string connStr = GetConnectionString();
            string sqlString = "";

            sqlString = @"
                        SELECT 
                            DISTINCT [gender]
                        FROM 
                            [TEL_Event_RegisterOption4]
                        WHERE
                            id = id
                            ";

            if (!string.IsNullOrEmpty(eventid))
            {
                sqlString += @"
                            AND eventid = @id
                                ";
            }

            if (!string.IsNullOrEmpty(hosipital))
            {
                sqlString += @"
                            AND hosipital = @hosipital
                                ";
            }

            if (!string.IsNullOrEmpty(area))
            {
                sqlString += @"
                            AND area = @area
                                ";
            }

            if (!string.IsNullOrEmpty(area))
            {
                sqlString += @"
                            AND [description] = @solution
                                ";
            }

            DataTable result = null;

            using (SqlConnection connection = new SqlConnection(connStr))
            {
                connection.Open();

                SqlDataAdapter wrDad = new SqlDataAdapter();
                DataSet DS = new DataSet();

                wrDad.SelectCommand = new SqlCommand(sqlString, connection);

                if (!string.IsNullOrEmpty(eventid))
                    wrDad.SelectCommand.Parameters.AddWithValue("@id", eventid);

                if (!string.IsNullOrEmpty(hosipital))
                    wrDad.SelectCommand.Parameters.AddWithValue("@hosipital", hosipital);

                if (!string.IsNullOrEmpty(area))
                    wrDad.SelectCommand.Parameters.AddWithValue("@area", area);

                if (!string.IsNullOrEmpty(solution))
                    wrDad.SelectCommand.Parameters.AddWithValue("@solution", solution);

                wrDad.Fill(DS, "T");
                result = DS.Tables["T"];
            }

            return result;
        }

        internal DataTable QueryExpectdateOption(string eventid, string hosipital, string area, string solution, string gender)
        {
            string connStr = GetConnectionString();
            string sqlString = "";

            sqlString = @"
                        SELECT 
                            DISTINCT CONVERT(VARCHAR, [avaliabledate],111) AS [avaliabledate]
                        FROM 
                            [TEL_Event_RegisterOption4]
                        WHERE
                            id = id
                            ";

            if (!string.IsNullOrEmpty(eventid))
            {
                sqlString += @"
                            AND eventid = @id
                                ";
            }

            if (!string.IsNullOrEmpty(hosipital))
            {
                sqlString += @"
                            AND hosipital = @hosipital
                                ";
            }

            if (!string.IsNullOrEmpty(area))
            {
                sqlString += @"
                            AND area = @area
                                ";
            }

            if (!string.IsNullOrEmpty(area))
            {
                sqlString += @"
                            AND [description] = @solution
                                ";
            }

            if (!string.IsNullOrEmpty(gender))
            {
                sqlString += @"
                            AND [gender] = @gender
                                ";
            }

            DataTable result = null;

            using (SqlConnection connection = new SqlConnection(connStr))
            {
                connection.Open();

                SqlDataAdapter wrDad = new SqlDataAdapter();
                DataSet DS = new DataSet();

                wrDad.SelectCommand = new SqlCommand(sqlString, connection);

                if (!string.IsNullOrEmpty(eventid))
                    wrDad.SelectCommand.Parameters.AddWithValue("@id", eventid);

                if (!string.IsNullOrEmpty(hosipital))
                    wrDad.SelectCommand.Parameters.AddWithValue("@hosipital", hosipital);

                if (!string.IsNullOrEmpty(area))
                    wrDad.SelectCommand.Parameters.AddWithValue("@area", area);

                if (!string.IsNullOrEmpty(solution))
                    wrDad.SelectCommand.Parameters.AddWithValue("@solution", solution);

                if (!string.IsNullOrEmpty(gender))
                    wrDad.SelectCommand.Parameters.AddWithValue("@gender", gender);

                wrDad.Fill(DS, "T");
                result = DS.Tables["T"];
            }

            return result;
        }

        internal DataTable QuerySecondoption1Option(string eventid, string hosipital, string area, string solution, string gender)
        {
            string connStr = GetConnectionString();
            string sqlString = "";

            sqlString = @"
                        SELECT 
                            DISTINCT secondoption1
                        FROM 
                            [TEL_Event_RegisterOption4]
                        WHERE
                            id = id
                            ";

            if (!string.IsNullOrEmpty(eventid))
            {
                sqlString += @"
                            AND eventid = @id
                                ";
            }

            if (!string.IsNullOrEmpty(hosipital))
            {
                sqlString += @"
                            AND hosipital = @hosipital
                                ";
            }

            if (!string.IsNullOrEmpty(area))
            {
                sqlString += @"
                            AND area = @area
                                ";
            }

            if (!string.IsNullOrEmpty(area))
            {
                sqlString += @"
                            AND [description] = @solution
                                ";
            }

            if (!string.IsNullOrEmpty(gender))
            {
                sqlString += @"
                            AND [gender] = @gender
                                ";
            }

            DataTable result = null;

            using (SqlConnection connection = new SqlConnection(connStr))
            {
                connection.Open();

                SqlDataAdapter wrDad = new SqlDataAdapter();
                DataSet DS = new DataSet();

                wrDad.SelectCommand = new SqlCommand(sqlString, connection);

                if (!string.IsNullOrEmpty(eventid))
                    wrDad.SelectCommand.Parameters.AddWithValue("@id", eventid);

                if (!string.IsNullOrEmpty(hosipital))
                    wrDad.SelectCommand.Parameters.AddWithValue("@hosipital", hosipital);

                if (!string.IsNullOrEmpty(area))
                    wrDad.SelectCommand.Parameters.AddWithValue("@area", area);

                if (!string.IsNullOrEmpty(solution))
                    wrDad.SelectCommand.Parameters.AddWithValue("@solution", solution);

                if (!string.IsNullOrEmpty(gender))
                    wrDad.SelectCommand.Parameters.AddWithValue("@gender", gender);

                wrDad.Fill(DS, "T");
                result = DS.Tables["T"];
            }

            return result;
        }

        internal DataTable QuerySecondoption2Option(string eventid, string hosipital, string area, string solution, string gender)
        {
            string connStr = GetConnectionString();
            string sqlString = "";

            sqlString = @"
                        SELECT 
                            DISTINCT secondoption2
                        FROM 
                            [TEL_Event_RegisterOption4]
                        WHERE
                            id = id
                            ";

            if (!string.IsNullOrEmpty(eventid))
            {
                sqlString += @"
                            AND eventid = @id
                                ";
            }

            if (!string.IsNullOrEmpty(hosipital))
            {
                sqlString += @"
                            AND hosipital = @hosipital
                                ";
            }

            if (!string.IsNullOrEmpty(area))
            {
                sqlString += @"
                            AND area = @area
                                ";
            }

            if (!string.IsNullOrEmpty(area))
            {
                sqlString += @"
                            AND [description] = @solution
                                ";
            }

            if (!string.IsNullOrEmpty(gender))
            {
                sqlString += @"
                            AND [gender] = @gender
                                ";
            }

            DataTable result = null;

            using (SqlConnection connection = new SqlConnection(connStr))
            {
                connection.Open();

                SqlDataAdapter wrDad = new SqlDataAdapter();
                DataSet DS = new DataSet();

                wrDad.SelectCommand = new SqlCommand(sqlString, connection);

                if (!string.IsNullOrEmpty(eventid))
                    wrDad.SelectCommand.Parameters.AddWithValue("@id", eventid);

                if (!string.IsNullOrEmpty(hosipital))
                    wrDad.SelectCommand.Parameters.AddWithValue("@hosipital", hosipital);

                if (!string.IsNullOrEmpty(area))
                    wrDad.SelectCommand.Parameters.AddWithValue("@area", area);

                if (!string.IsNullOrEmpty(solution))
                    wrDad.SelectCommand.Parameters.AddWithValue("@solution", solution);

                if (!string.IsNullOrEmpty(gender))
                    wrDad.SelectCommand.Parameters.AddWithValue("@gender", gender);

                wrDad.Fill(DS, "T");
                result = DS.Tables["T"];
            }

            return result;
        }

        internal DataTable QuerySecondoption3Option(string eventid, string hosipital, string area, string solution, string gender)
        {
            string connStr = GetConnectionString();
            string sqlString = "";

            sqlString = @"
                        SELECT 
                            DISTINCT secondoption3
                        FROM 
                            [TEL_Event_RegisterOption4]
                        WHERE
                            id = id
                            ";

            if (!string.IsNullOrEmpty(eventid))
            {
                sqlString += @"
                            AND eventid = @id
                                ";
            }

            if (!string.IsNullOrEmpty(hosipital))
            {
                sqlString += @"
                            AND hosipital = @hosipital
                                ";
            }

            if (!string.IsNullOrEmpty(area))
            {
                sqlString += @"
                            AND area = @area
                                ";
            }

            if (!string.IsNullOrEmpty(area))
            {
                sqlString += @"
                            AND [description] = @solution
                                ";
            }

            if (!string.IsNullOrEmpty(gender))
            {
                sqlString += @"
                            AND [gender] = @gender
                                ";
            }

            DataTable result = null;

            using (SqlConnection connection = new SqlConnection(connStr))
            {
                connection.Open();

                SqlDataAdapter wrDad = new SqlDataAdapter();
                DataSet DS = new DataSet();

                wrDad.SelectCommand = new SqlCommand(sqlString, connection);

                if (!string.IsNullOrEmpty(eventid))
                    wrDad.SelectCommand.Parameters.AddWithValue("@id", eventid);

                if (!string.IsNullOrEmpty(hosipital))
                    wrDad.SelectCommand.Parameters.AddWithValue("@hosipital", hosipital);

                if (!string.IsNullOrEmpty(area))
                    wrDad.SelectCommand.Parameters.AddWithValue("@area", area);

                if (!string.IsNullOrEmpty(solution))
                    wrDad.SelectCommand.Parameters.AddWithValue("@solution", solution);

                if (!string.IsNullOrEmpty(gender))
                    wrDad.SelectCommand.Parameters.AddWithValue("@gender", gender);

                wrDad.Fill(DS, "T");
                result = DS.Tables["T"];
            }

            return result;
        }

        internal DataTable QueryHealthAddressOption(string eventid)
        {
            string connStr = GetConnectionString();
            string sqlString = "";

            sqlString = @"
                        SELECT 
                            [description]
                        FROM 
                            [TEL_Event_RegisterOption5]
                        WHERE
                            id = id
                            ";

            if (!string.IsNullOrEmpty(eventid))
            {
                sqlString += @"
                            AND eventid = @id
                                ";
            }

            DataTable result = null;

            using (SqlConnection connection = new SqlConnection(connStr))
            {
                connection.Open();

                SqlDataAdapter wrDad = new SqlDataAdapter();
                DataSet DS = new DataSet();

                wrDad.SelectCommand = new SqlCommand(sqlString, connection);

                if (!string.IsNullOrEmpty(eventid))
                    wrDad.SelectCommand.Parameters.AddWithValue("@id", eventid);

                wrDad.Fill(DS, "T");
                result = DS.Tables["T"];
            }

            return result;
        }

        /// <summary>
        /// 取得健檢方案人數上限
        /// </summary>
        /// <param name="eventid"></param>
        /// <param name="hosipital"></param>
        /// <param name="area"></param>
        /// <param name="solution"></param>
        /// <param name="gender"></param>
        /// <param name="expectdate"></param>
        /// <returns></returns>
        internal int QueryRegisterOption4Limit(string eventid, string hosipital, string area, string solution, string gender, string expectdate)
        {
            string connStr = GetConnectionString();
            string sqlStr = "";

            sqlStr = @"
                        SELECT 
                            [limit]
                        FROM 
                            [TEL_Event_RegisterOption4]
                        WHERE  
                            [eventid] = @eventid
                        AND
                            hosipital = @hosipital
                        AND
                            area = @area
                        AND
                            description = @solution
                        AND
                            [gender] = @gender
                        AND
                            CONVERT(VARCHAR, avaliabledate,111) = @expectdate ";



            DataTable result = null;

            using (SqlConnection connection = new SqlConnection(connStr))
            {
                connection.Open();

                SqlDataAdapter wrDad = new SqlDataAdapter();
                DataSet DS = new DataSet();

                wrDad.SelectCommand = new SqlCommand(sqlStr, connection);

                wrDad.SelectCommand.Parameters.AddWithValue("@eventid", eventid);
                wrDad.SelectCommand.Parameters.AddWithValue("@hosipital", hosipital);
                wrDad.SelectCommand.Parameters.AddWithValue("@area", area);
                wrDad.SelectCommand.Parameters.AddWithValue("@solution", solution);
                wrDad.SelectCommand.Parameters.AddWithValue("@gender", gender);
                wrDad.SelectCommand.Parameters.AddWithValue("@expectdate", Convert.ToDateTime(expectdate));


                wrDad.Fill(DS, "T");
                result = DS.Tables["T"];
            }


            return result.Rows.Count == 0 ? 0 : Convert.ToInt16(result.Rows[0]["limit"]);
        }

        /// <summary>
        /// 取得健檢方案已報名人數
        /// </summary>
        /// <param name="eventid"></param>
        /// <param name="hosipital"></param>
        /// <param name="area"></param>
        /// <param name="solution"></param>
        /// <param name="gender"></param>
        /// <param name="expectdate"></param>
        /// <returns></returns>
        internal int QueryRegisterOption3Count(string eventid, string hosipital, string area, string solution, string gender, string expectdate, string registerid)
        {
            string connStr = GetConnectionString();
            string sqlStr = "";

            sqlStr = @"
                        SELECT 
                            COUNT([id]) AS RegisterCount
                        FROM 
                            [TEL_Event_RegisterModel3]
                        WHERE  
                            [eventid] = @eventid
                        AND
                            hosipital = @hosipital
                        AND
                            area = @area
                        AND
                            solution = @solution
                        AND
                            [gender] = @gender
                        AND
                            CONVERT(VARCHAR, expectdate,111) = @expectdate ";

            if (!string.IsNullOrEmpty(registerid))
            {
                sqlStr += @"
                        AND
                            [id] != @registerid
                        ";
            }

            DataTable result = null;

            using (SqlConnection connection = new SqlConnection(connStr))
            {
                connection.Open();

                SqlDataAdapter wrDad = new SqlDataAdapter();
                DataSet DS = new DataSet();

                wrDad.SelectCommand = new SqlCommand(sqlStr, connection);

                wrDad.SelectCommand.Parameters.AddWithValue("@eventid", eventid);
                wrDad.SelectCommand.Parameters.AddWithValue("@hosipital", hosipital);
                wrDad.SelectCommand.Parameters.AddWithValue("@area", area);
                wrDad.SelectCommand.Parameters.AddWithValue("@solution", solution);
                wrDad.SelectCommand.Parameters.AddWithValue("@gender", gender);
                wrDad.SelectCommand.Parameters.AddWithValue("@expectdate", Convert.ToDateTime(expectdate));

                if (!string.IsNullOrEmpty(registerid))
                    wrDad.SelectCommand.Parameters.AddWithValue("@registerid", registerid);

                wrDad.Fill(DS, "T");
                result = DS.Tables["T"];
            }


            return result.Rows.Count == 0 ? 0 : Convert.ToInt16(result.Rows[0]["RegisterCount"]);
        }

        /// <summary>
        /// 新增 模板3報名資料
        /// </summary>
        /// <param name="eventsData"></param>
        /// <param name="dataTable"></param>
        /// <param name="modifiedby"></param>
        /// <returns></returns>
        internal string InsertRegisterModel3(Dictionary<string, string> data, string modifiedby)
        {
            string connStr = GetConnectionString();
            string sqlStr = "";

            try
            {
                sqlStr = @"
                    INSERT INTO [TEL_Event_RegisterModel3]
                           (
                            [id]
                            ,[eventid]
                            ,[empid]
                            ,[registerdate]
                            ,[examineeidentity]
                            ,[examineename]
                            ,[examineeidno]
                            ,[examineebirthday]
                            ,[examineemobile]
                            ,[hosipital]
                            ,[area]
                            ,[solution]
                            ,[gender]
                            ,[expectdate]
                            ,[seconddate]
                            ,[secondsolution1]
                            ,[secondsolution2]
                            ,[secondsolution3]
                            ,[optional]
                            ,[address]
                            ,[meal]
                            ,[feedback]
                            ,[modifiedby]
                            ,[modifieddate])
                     VALUES
                           (
                            @id
                            ,@eventid
                            ,@empid
                            ,@registerdate
                            ,@examineeidentity
                            ,@examineename
                            ,@examineeidno
                            ,@examineebirthday
                            ,@examineemobile
                            ,@hosipital
                            ,@area
                            ,@solution
                            ,@gender
                            ,@expectdate
                            ,@seconddate
                            ,@secondsolution1
                            ,@secondsolution2
                            ,@secondsolution3
                            ,@optional
                            ,@address
                            ,@meal
                            ,@feedback
                            ,@modifiedby
                            ,GETDATE()
                            ) ";

                SqlConnection conn = new SqlConnection(connStr);
                conn.Open();
                SqlCommand command = new SqlCommand(sqlStr, conn);


                command.Parameters.Clear();


                command.Parameters.AddWithValue("@id", data["id"]);
                command.Parameters.AddWithValue("@eventid", data["eventid"]);
                command.Parameters.AddWithValue("@empid", data["empid"]);
                command.Parameters.AddWithValue("@registerdate", data["registerdate"]);
                command.Parameters.AddWithValue("@examineeidentity", data["examineeidentity"]);
                command.Parameters.AddWithValue("@examineename", data["examineename"]);
                command.Parameters.AddWithValue("@examineeidno", data["examineeidno"]);
                command.Parameters.AddWithValue("@examineebirthday", data["examineebirthday"]);
                command.Parameters.AddWithValue("@examineemobile", data["examineemobile"]);
                command.Parameters.AddWithValue("@hosipital", data["hosipital"]);
                command.Parameters.AddWithValue("@area", data["area"]);
                command.Parameters.AddWithValue("@solution", data["solution"]);
                command.Parameters.AddWithValue("@gender", data["gender"]);
                command.Parameters.AddWithValue("@expectdate", data["expectdate"]);
                command.Parameters.AddWithValue("@seconddate", data["seconddate"]);
                command.Parameters.AddWithValue("@secondsolution1", data["secondsolution1"]);
                command.Parameters.AddWithValue("@secondsolution2", data["secondsolution2"]);
                command.Parameters.AddWithValue("@secondsolution3", data["secondsolution3"]);
                command.Parameters.AddWithValue("@optional", data["optional"]);
                command.Parameters.AddWithValue("@address", data["address"]);
                command.Parameters.AddWithValue("@meal", data["meal"]);
                command.Parameters.AddWithValue("@feedback", data["feedback"]);
                command.Parameters.AddWithValue("@modifiedby", modifiedby);


                command.ExecuteNonQuery();
                conn.Close();
                conn.Dispose();

                //Add log
                SqlLog sl = new SqlLog();
                sl.AddLog("新增模板3報名資料", data["id"], sl.GetCommendText(command), modifiedby);
            }
            catch (Exception ex)
            {
                return $"ErrorMsg：{ex.Message}{Environment.NewLine}ErrorStackTrace：{ex.StackTrace}";
            }

            return "";
        }


        /// <summary>
        /// 更新 模板3報名資料
        /// </summary>
        /// <param name="data"></param>
        /// <param name="modifiedby"></param>
        /// <returns></returns>
        internal string UpdateRegisterModel3(Dictionary<string, string> data, string modifiedby)
        {
            string connStr = GetConnectionString();
            string sqlStr = "";

            try
            {
                sqlStr = @"
                        UPDATE 
                            [TEL_Event_RegisterModel3]
                        SET 
                            [examineeidentity] = @examineeidentity
                            ,[examineename] = @examineename
                            ,[examineeidno] = @examineeidno
                            ,[examineebirthday] = @examineebirthday
                            ,[examineemobile] = @examineemobile
                            ,[hosipital] = @hosipital
                            ,[area] = @area
                            ,[solution] = @solution
                            ,[gender] = @gender
                            ,[expectdate] = @expectdate
                            ,[seconddate] = @seconddate
                            ,[secondsolution1] = @secondsolution1
                            ,[secondsolution2] = @secondsolution2
                            ,[secondsolution3] = @secondsolution3
                            ,[optional] = @optional
                            ,[address] = @address
                            ,[meal] = @meal
                            ,[feedback] = @feedback
                            ,[modifiedby] = @modifiedby
                            ,[modifieddate] = GETDATE()
                        WHERE 
                            id = @id
                        ";

                SqlConnection conn = new SqlConnection(connStr);
                conn.Open();
                SqlCommand command = new SqlCommand(sqlStr, conn);

                command.Parameters.Clear();

                command.Parameters.AddWithValue("@id", data["id"]);
                command.Parameters.AddWithValue("@examineeidentity", data["examineeidentity"]);
                command.Parameters.AddWithValue("@examineename", data["examineename"]);
                command.Parameters.AddWithValue("@examineeidno", data["examineeidno"]);
                command.Parameters.AddWithValue("@examineebirthday", data["examineebirthday"]);
                command.Parameters.AddWithValue("@examineemobile", data["examineemobile"]);
                command.Parameters.AddWithValue("@hosipital", data["hosipital"]);
                command.Parameters.AddWithValue("@area", data["area"]);
                command.Parameters.AddWithValue("@solution", data["solution"]);
                command.Parameters.AddWithValue("@gender", data["gender"]);
                command.Parameters.AddWithValue("@expectdate", data["expectdate"]);
                command.Parameters.AddWithValue("@seconddate", data["seconddate"]);
                command.Parameters.AddWithValue("@secondsolution1", data["secondsolution1"]);
                command.Parameters.AddWithValue("@secondsolution2", data["secondsolution2"]);
                command.Parameters.AddWithValue("@secondsolution3", data["secondsolution3"]);
                command.Parameters.AddWithValue("@optional", data["optional"]);
                command.Parameters.AddWithValue("@address", data["address"]);
                command.Parameters.AddWithValue("@meal", data["meal"]);
                command.Parameters.AddWithValue("@feedback", data["feedback"]);
                command.Parameters.AddWithValue("@modifiedby", modifiedby);

                command.ExecuteNonQuery();
                conn.Close();
                conn.Dispose();

                //Add log
                SqlLog sl = new SqlLog();
                sl.AddLog("更新模板3報名資料", data["id"], sl.GetCommendText(command), modifiedby);
            }
            catch (Exception ex)
            {
                return $"ErrorMsg：{ex.Message}{Environment.NewLine}ErrorStackTrace：{ex.StackTrace}";
            }

            return "";
        }

        /// <summary>
        /// 刪除 模板3報名資料
        /// </summary>
        /// <param name="data"></param>
        /// <param name="modifiedby"></param>
        /// <returns></returns>
        internal string DeleteRegisterModel3(string id, string modifiedby)
        {
            string connStr = GetConnectionString();
            string sqlStr = "";

            try
            {
                sqlStr = @"
                        DELETE FROM [dbo].[TEL_Event_RegisterModel3]
                        WHERE 
                            id = @id
                        ";

                SqlConnection conn = new SqlConnection(connStr);
                conn.Open();
                SqlCommand command = new SqlCommand(sqlStr, conn);


                command.Parameters.Clear();
                command.Parameters.AddWithValue("@id", id);


                command.ExecuteNonQuery();
                conn.Close();
                conn.Dispose();

                //Add log
                SqlLog sl = new SqlLog();
                sl.AddLog("刪除模板3報名資料", id, sl.GetCommendText(command), modifiedby);
            }
            catch (Exception ex)
            {
                return $"ErrorMsg：{ex.Message}{Environment.NewLine}ErrorStackTrace：{ex.StackTrace}";
            }

            return "";
        }

        /// <summary>
        /// 查詢 模板3報名資料
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        internal DataTable QueryRegisterModel3(string id)
        {
            string connStr = GetConnectionString();
            string sqlString = "";

            sqlString = @"
                        SELECT 
                            [id]
                            ,[eventid]
                            ,[empid]
                            ,[registerdate]
                            ,[examineeidentity]
                            ,[examineename]
                            ,[examineeidno]
                            ,CONVERT(VARCHAR, [examineebirthday],111) as [examineebirthday]
                            ,[examineemobile]
                            ,[hosipital]
                            ,[area]
                            ,[solution]
                            ,[gender]
                            ,[expectdate]
                            ,[seconddate]
                            ,[secondsolution1]
                            ,[secondsolution2]
                            ,[secondsolution3]
                            ,[optional]
                            ,[address]
                            ,[meal]
                            ,[feedback]
                            ,[modifiedby]
                            ,[modifieddate]
                        FROM 
                            [dbo].[TEL_Event_RegisterModel3]
                        WHERE
                            id = id
                            ";

            if (!string.IsNullOrEmpty(id))
            {
                sqlString += @"
                            AND id = @id
                                ";
            }

            DataTable result = null;

            using (SqlConnection connection = new SqlConnection(connStr))
            {
                connection.Open();

                SqlDataAdapter wrDad = new SqlDataAdapter();
                DataSet DS = new DataSet();

                wrDad.SelectCommand = new SqlCommand(sqlString, connection);

                if (!string.IsNullOrEmpty(id))
                    wrDad.SelectCommand.Parameters.AddWithValue("@id", id);

                wrDad.Fill(DS, "T");
                result = DS.Tables["T"];
            }

            return result;
        }
        #endregion

        #region RegisterModel4
        /// <summary>
        /// 取得健檢方案已報名人數
        /// </summary>
        /// <param name="eventid"></param>
        /// <param name="hosipital"></param>
        /// <param name="area"></param>
        /// <param name="solution"></param>
        /// <param name="gender"></param>
        /// <param name="expectdate"></param>
        /// <param name="registerid"></param>
        /// <returns></returns>
        internal int QueryRegisterOption4Count(string eventid, string hosipital, string area, string solution, string gender, string expectdate, string registerid)
        {
            string connStr = GetConnectionString();
            string sqlStr = "";

            sqlStr = @"
                        SELECT 
                            COUNT([id]) AS RegisterCount
                        FROM 
                            [TEL_Event_RegisterModel4]
                        WHERE  
                            [eventid] = @eventid
                        AND
                            hosipital = @hosipital
                        AND
                            area = @area
                        AND
                            solution = @solution
                        AND
                            [gender] = @gender
                        AND
                            CONVERT(VARCHAR, expectdate,111) = @expectdate ";

            if (!string.IsNullOrEmpty(registerid))
            {
                sqlStr += @"
                        AND
                            [id] != @registerid";
            }


            DataTable result = null;

            using (SqlConnection connection = new SqlConnection(connStr))
            {
                connection.Open();

                SqlDataAdapter wrDad = new SqlDataAdapter();
                DataSet DS = new DataSet();

                wrDad.SelectCommand = new SqlCommand(sqlStr, connection);

                wrDad.SelectCommand.Parameters.AddWithValue("@eventid", eventid);
                wrDad.SelectCommand.Parameters.AddWithValue("@hosipital", hosipital);
                wrDad.SelectCommand.Parameters.AddWithValue("@area", area);
                wrDad.SelectCommand.Parameters.AddWithValue("@solution", solution);
                wrDad.SelectCommand.Parameters.AddWithValue("@gender", gender);
                wrDad.SelectCommand.Parameters.AddWithValue("@expectdate", Convert.ToDateTime(expectdate));

                if (!string.IsNullOrEmpty(registerid))
                    wrDad.SelectCommand.Parameters.AddWithValue("@registerid", registerid);


                wrDad.Fill(DS, "T");
                result = DS.Tables["T"];
            }


            return result.Rows.Count == 0 ? 0 : Convert.ToInt16(result.Rows[0]["RegisterCount"]);
        }

        /// <summary>
        /// 新增 模板4報名資料
        /// </summary>
        /// <param name="eventsData"></param>
        /// <param name="dataTable"></param>
        /// <param name="modifiedby"></param>
        /// <returns></returns>
        internal string InsertRegisterModel4(Dictionary<string, string> data, string modifiedby)
        {
            string connStr = GetConnectionString();
            string sqlStr = "";

            try
            {
                sqlStr = @"
                    INSERT INTO [TEL_Event_RegisterModel4]
                           (
                            [id]
                            ,[eventid]
                            ,[empid]
                            ,[registerdate]
                            ,[examineeidentity]
                            ,[examineename]
                            ,[examineename2]
                            ,[examineeidno]
                            ,[examineebirthday]
                            ,[examineemobile]
                            ,[hosipital]
                            ,[area]
                            ,[solution]
                            ,[gender]
                            ,[expectdate]
                            ,[seconddate]
                            ,[secondsolution1]
                            ,[secondsolution2]
                            ,[secondsolution3]
                            ,[optional]
                            ,[address]
                            ,[meal]
                            ,[needhotel]
                            ,[checkininfo]
                            ,[feedback]
                            ,[modifiedby]
                            ,[modifieddate])
                     VALUES
                           (
                            @id
                            ,@eventid
                            ,@empid
                            ,@registerdate
                            ,@examineeidentity
                            ,@examineename
                            ,@examineename2
                            ,@examineeidno
                            ,@examineebirthday
                            ,@examineemobile
                            ,@hosipital
                            ,@area
                            ,@solution
                            ,@gender
                            ,@expectdate
                            ,@seconddate
                            ,@secondsolution1
                            ,@secondsolution2
                            ,@secondsolution3
                            ,@optional
                            ,@address
                            ,@meal
                            ,@needhotel
                            ,@checkininfo
                            ,@feedback
                            ,@modifiedby
                            ,GETDATE()
                            ) ";

                SqlConnection conn = new SqlConnection(connStr);
                conn.Open();
                SqlCommand command = new SqlCommand(sqlStr, conn);


                command.Parameters.Clear();


                command.Parameters.AddWithValue("@id", data["id"]);
                command.Parameters.AddWithValue("@eventid", data["eventid"]);
                command.Parameters.AddWithValue("@empid", data["empid"]);
                command.Parameters.AddWithValue("@registerdate", data["registerdate"]);
                command.Parameters.AddWithValue("@examineeidentity", data["examineeidentity"]);
                command.Parameters.AddWithValue("@examineename", data["examineename"]);
                command.Parameters.AddWithValue("@examineename2", data["examineename2"]);
                command.Parameters.AddWithValue("@examineeidno", data["examineeidno"]);
                command.Parameters.AddWithValue("@examineebirthday", data["examineebirthday"]);
                command.Parameters.AddWithValue("@examineemobile", data["examineemobile"]);
                command.Parameters.AddWithValue("@hosipital", data["hosipital"]);
                command.Parameters.AddWithValue("@area", data["area"]);
                command.Parameters.AddWithValue("@solution", data["solution"]);
                command.Parameters.AddWithValue("@gender", data["gender"]);
                command.Parameters.AddWithValue("@expectdate", data["expectdate"]);
                command.Parameters.AddWithValue("@seconddate", data["seconddate"]);
                command.Parameters.AddWithValue("@secondsolution1", data["secondsolution1"]);
                command.Parameters.AddWithValue("@secondsolution2", data["secondsolution2"]);
                command.Parameters.AddWithValue("@secondsolution3", data["secondsolution3"]);
                command.Parameters.AddWithValue("@optional", data["optional"]);
                command.Parameters.AddWithValue("@address", data["address"]);
                command.Parameters.AddWithValue("@meal", data["meal"]);
                command.Parameters.AddWithValue("@needhotel", data["needhotel"]);
                command.Parameters.AddWithValue("@checkininfo", data["checkininfo"]);
                command.Parameters.AddWithValue("@feedback", data["feedback"]);
                command.Parameters.AddWithValue("@modifiedby", modifiedby);


                command.ExecuteNonQuery();
                conn.Close();
                conn.Dispose();

                //Add log
                SqlLog sl = new SqlLog();
                sl.AddLog("新增模板4報名資料", data["id"], sl.GetCommendText(command), modifiedby);
            }
            catch (Exception ex)
            {
                return $"ErrorMsg：{ex.Message}{Environment.NewLine}ErrorStackTrace：{ex.StackTrace}";
            }

            return "";
        }


        /// <summary>
        /// 更新 模板4報名資料
        /// </summary>
        /// <param name="data"></param>
        /// <param name="modifiedby"></param>
        /// <returns></returns>
        internal string UpdateRegisterModel4(Dictionary<string, string> data, string modifiedby)
        {
            string connStr = GetConnectionString();
            string sqlStr = "";

            try
            {
                sqlStr = @"
                        UPDATE 
                            [TEL_Event_RegisterModel4]
                        SET 
                            [examineeidentity] = @examineeidentity
                            ,[examineename] = @examineename
                            ,[examineename2] = @examineename2
                            ,[examineeidno] = @examineeidno
                            ,[examineebirthday] = @examineebirthday
                            ,[examineemobile] = @examineemobile
                            ,[hosipital] = @hosipital
                            ,[area] = @area
                            ,[solution] = @solution
                            ,[gender] = @gender
                            ,[expectdate] = @expectdate
                            ,[seconddate] = @seconddate
                            ,[secondsolution1] = @secondsolution1
                            ,[secondsolution2] = @secondsolution2
                            ,[secondsolution3] = @secondsolution3
                            ,[optional] = @optional
                            ,[address] = @address
                            ,[meal] = @meal
                            ,[needhotel] = @needhotel
                            ,[checkininfo] = @checkininfo
                            ,[feedback] = @feedback
                            ,[modifiedby] = @modifiedby
                            ,[modifieddate] = GETDATE()
                        WHERE 
                            id = @id
                        ";

                SqlConnection conn = new SqlConnection(connStr);
                conn.Open();
                SqlCommand command = new SqlCommand(sqlStr, conn);

                command.Parameters.Clear();

                command.Parameters.AddWithValue("@id", data["id"]);
                command.Parameters.AddWithValue("@examineeidentity", data["examineeidentity"]);
                command.Parameters.AddWithValue("@examineename", data["examineename"]);
                command.Parameters.AddWithValue("@examineename2", data["examineename2"]);
                command.Parameters.AddWithValue("@examineeidno", data["examineeidno"]);
                command.Parameters.AddWithValue("@examineebirthday", data["examineebirthday"]);
                command.Parameters.AddWithValue("@examineemobile", data["examineemobile"]);
                command.Parameters.AddWithValue("@hosipital", data["hosipital"]);
                command.Parameters.AddWithValue("@area", data["area"]);
                command.Parameters.AddWithValue("@solution", data["solution"]);
                command.Parameters.AddWithValue("@gender", data["gender"]);
                command.Parameters.AddWithValue("@expectdate", data["expectdate"]);
                command.Parameters.AddWithValue("@seconddate", data["seconddate"]);
                command.Parameters.AddWithValue("@secondsolution1", data["secondsolution1"]);
                command.Parameters.AddWithValue("@secondsolution2", data["secondsolution2"]);
                command.Parameters.AddWithValue("@secondsolution3", data["secondsolution3"]);
                command.Parameters.AddWithValue("@optional", data["optional"]);
                command.Parameters.AddWithValue("@address", data["address"]);
                command.Parameters.AddWithValue("@meal", data["meal"]);
                command.Parameters.AddWithValue("@needhotel", data["needhotel"]);
                command.Parameters.AddWithValue("@checkininfo", data["checkininfo"]);
                command.Parameters.AddWithValue("@feedback", data["feedback"]);
                command.Parameters.AddWithValue("@modifiedby", modifiedby);

                command.ExecuteNonQuery();
                conn.Close();
                conn.Dispose();

                //Add log
                SqlLog sl = new SqlLog();
                sl.AddLog("更新模板4報名資料", data["id"], sl.GetCommendText(command), modifiedby);
            }
            catch (Exception ex)
            {
                return $"ErrorMsg：{ex.Message}{Environment.NewLine}ErrorStackTrace：{ex.StackTrace}";
            }

            return "";
        }

        /// <summary>
        /// 刪除 模板4報名資料
        /// </summary>
        /// <param name="data"></param>
        /// <param name="modifiedby"></param>
        /// <returns></returns>
        internal string DeleteRegisterModel4(string id, string modifiedby)
        {
            string connStr = GetConnectionString();
            string sqlStr = "";

            try
            {
                sqlStr = @"
                        DELETE FROM [dbo].[TEL_Event_RegisterModel4]
                        WHERE 
                            id = @id
                        ";

                SqlConnection conn = new SqlConnection(connStr);
                conn.Open();
                SqlCommand command = new SqlCommand(sqlStr, conn);


                command.Parameters.Clear();
                command.Parameters.AddWithValue("@id", id);


                command.ExecuteNonQuery();
                conn.Close();
                conn.Dispose();

                //Add log
                SqlLog sl = new SqlLog();
                sl.AddLog("刪除模板4報名資料", id, sl.GetCommendText(command), modifiedby);
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }

            return "";
        }

        /// <summary>
        /// 查詢 模板4報名資料
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        internal DataTable QueryRegisterModel4(string id)
        {
            string connStr = GetConnectionString();
            string sqlString = "";

            sqlString = @"
                        SELECT 
                            [id]
                            ,[eventid]
                            ,[empid]
                            ,[registerdate]
                            ,[examineeidentity]
                            ,[examineename]
                            ,[examineename2]
                            ,[examineeidno]
                            ,CONVERT(VARCHAR, [examineebirthday],111) as [examineebirthday]
                            ,[examineemobile]
                            ,[hosipital]
                            ,[area]
                            ,[solution]
                            ,[gender]
                            ,[expectdate]
                            ,[seconddate]
                            ,[secondsolution1]
                            ,[secondsolution2]
                            ,[secondsolution3]
                            ,[optional]
                            ,[address]
                            ,[meal]
                            ,[needhotel]
                            ,[checkininfo]
                            ,[feedback]
                            ,[modifiedby]
                            ,[modifieddate]
                        FROM 
                            [dbo].[TEL_Event_RegisterModel4]
                        WHERE
                            id = id
                            ";

            if (!string.IsNullOrEmpty(id))
            {
                sqlString += @"
                            AND id = @id
                                ";
            }

            DataTable result = null;

            using (SqlConnection connection = new SqlConnection(connStr))
            {
                connection.Open();

                SqlDataAdapter wrDad = new SqlDataAdapter();
                DataSet DS = new DataSet();

                wrDad.SelectCommand = new SqlCommand(sqlString, connection);

                if (!string.IsNullOrEmpty(id))
                    wrDad.SelectCommand.Parameters.AddWithValue("@id", id);

                wrDad.Fill(DS, "T");
                result = DS.Tables["T"];
            }

            return result;
        }
        #endregion

        #region RegisterModel5
        /// <summary>
        /// 新增 模板5報名資料
        /// </summary>
        /// <param name="eventsData"></param>
        /// <param name="dataTable"></param>
        /// <param name="modifiedby"></param>
        /// <returns></returns>
        internal string InsertRegisterModel5(Dictionary<string, string> data, string modifiedby)
        {
            string connStr = GetConnectionString();
            string sqlStr = "";

            try
            {
                sqlStr = @"
                    INSERT INTO [TEL_Event_RegisterModel5]
                           (
                            [id]
                            ,[eventid]
                            ,[empid]
                            ,[registerdate]
                            ,[attachment1]
                            ,[attachment1_name]
                            ,[description1]
                            ,[attachment2]
                            ,[attachment2_name]
                            ,[description2]
                            ,[attachment3]
                            ,[attachment3_name]
                            ,[description3]
                            ,[feedback]
                            ,[modifiedby]
                            ,[modifieddate])
                     VALUES
                           (
                            @id
                            ,@eventid
                            ,@empid
                            ,@registerdate
                            ,@attachment1
                            ,@attachment1_name
                            ,@description1
                            ,@attachment2
                            ,@attachment2_name
                            ,@description2
                            ,@attachment3
                            ,@attachment3_name
                            ,@description3
                            ,@feedback
                            ,@modifiedby
                            ,GETDATE()
                            ) ";

                SqlConnection conn = new SqlConnection(connStr);
                conn.Open();
                SqlCommand command = new SqlCommand(sqlStr, conn);


                command.Parameters.Clear();

                command.Parameters.AddWithValue("@id", data["id"]);
                command.Parameters.AddWithValue("@eventid", data["eventid"]);
                command.Parameters.AddWithValue("@empid", data["empid"]);
                command.Parameters.AddWithValue("@registerdate", data["registerdate"]);
                command.Parameters.AddWithValue("@attachment1", data["attachment1"]);
                command.Parameters.AddWithValue("@attachment1_name", data["attachment1_name"]);
                command.Parameters.AddWithValue("@description1", data["description1"]);
                command.Parameters.AddWithValue("@attachment2", data["attachment2"]);
                command.Parameters.AddWithValue("@attachment2_name", data["attachment2_name"]);
                command.Parameters.AddWithValue("@description2", data["description2"]);
                command.Parameters.AddWithValue("@attachment3", data["attachment3"]);
                command.Parameters.AddWithValue("@attachment3_name", data["attachment3_name"]);
                command.Parameters.AddWithValue("@description3", data["description3"]);
                command.Parameters.AddWithValue("@feedback", data["feedback"]);
                command.Parameters.AddWithValue("@modifiedby", modifiedby);

                command.ExecuteNonQuery();
                conn.Close();
                conn.Dispose();

                //Add log
                SqlLog sl = new SqlLog();
                sl.AddLog("新增模板5報名資料", data["id"], sl.GetCommendText(command), modifiedby);
            }
            catch (Exception ex)
            {
                return $"ErrorMsg：{ex.Message}{Environment.NewLine}ErrorStackTrace：{ex.StackTrace}";
            }

            return "";
        }


        /// <summary>
        /// 更新 模板5報名資料
        /// </summary>
        /// <param name="data"></param>
        /// <param name="modifiedby"></param>
        /// <returns></returns>
        internal string UpdateRegisterModel5(Dictionary<string, string> data, string modifiedby)
        {
            string connStr = GetConnectionString();
            string sqlStr = "";

            try
            {
                sqlStr = @"
                        UPDATE 
                            [TEL_Event_RegisterModel5]
                        SET 
                            [modifiedby] = @modifiedby
                            ,[modifieddate] = GETDATE()
                            ,[feedback] = @feedback ";

                if (!string.IsNullOrEmpty(data["attachment1"]))
                {
                    sqlStr += @"
                            ,[attachment1] = @attachment1
                            ,[attachment1_name] = @attachment1_name
                            ,[description1] = @description1 ";
                }

                if (!string.IsNullOrEmpty(data["attachment2"]))
                {
                    sqlStr += @"
                            ,[attachment2] = @attachment2
                            ,[attachment2_name] = @attachment2_name
                            ,[description2] = @description2 ";
                }

                if (!string.IsNullOrEmpty(data["attachment3"]))
                {
                    sqlStr += @"
                            ,[attachment3] = @attachment3
                            ,[attachment3_name] = @attachment3_name
                            ,[description3] = @description3";
                }


                sqlStr += @"     
                        WHERE 
                            id = @id
                        ";

                SqlConnection conn = new SqlConnection(connStr);
                conn.Open();
                SqlCommand command = new SqlCommand(sqlStr, conn);

                command.Parameters.Clear();

                command.Parameters.AddWithValue("@id", data["id"]);

                if (!string.IsNullOrEmpty(data["attachment1"]))
                {
                    command.Parameters.AddWithValue("@attachment1", data["attachment1"]);
                    command.Parameters.AddWithValue("@attachment1_name", data["attachment1_name"]);
                    command.Parameters.AddWithValue("@description1", data["description1"]);
                }

                if (!string.IsNullOrEmpty(data["attachment2"]))
                {
                    command.Parameters.AddWithValue("@attachment2", data["attachment2"]);
                    command.Parameters.AddWithValue("@attachment2_name", data["attachment2_name"]);
                    command.Parameters.AddWithValue("@description2", data["description2"]);
                }

                if (!string.IsNullOrEmpty(data["attachment3"]))
                {
                    command.Parameters.AddWithValue("@attachment3", data["attachment3"]);
                    command.Parameters.AddWithValue("@attachment3_name", data["attachment3_name"]);
                    command.Parameters.AddWithValue("@description3", data["description3"]);
                }


                command.Parameters.AddWithValue("@feedback", data["feedback"]);
                command.Parameters.AddWithValue("@modifiedby", modifiedby);

                command.ExecuteNonQuery();
                conn.Close();
                conn.Dispose();

                //Add log
                SqlLog sl = new SqlLog();
                sl.AddLog("更新模板5報名資料", data["id"], sl.GetCommendText(command), modifiedby);
            }
            catch (Exception ex)
            {
                return $"ErrorMsg：{ex.Message}{Environment.NewLine}ErrorStackTrace：{ex.StackTrace}";
            }

            return "";
        }

        /// <summary>
        /// 刪除 模板5報名資料
        /// </summary>
        /// <param name="data"></param>
        /// <param name="modifiedby"></param>
        /// <returns></returns>
        internal string DeleteRegisterModel5(string id, string modifiedby)
        {
            string connStr = GetConnectionString();
            string sqlStr = "";

            try
            {
                sqlStr = @"
                        DELETE FROM [dbo].[TEL_Event_RegisterModel5]
                        WHERE 
                            id = @id
                        ";

                SqlConnection conn = new SqlConnection(connStr);
                conn.Open();
                SqlCommand command = new SqlCommand(sqlStr, conn);


                command.Parameters.Clear();
                command.Parameters.AddWithValue("@id", id);


                command.ExecuteNonQuery();
                conn.Close();
                conn.Dispose();

                //Add log
                SqlLog sl = new SqlLog();
                sl.AddLog("刪除模板5報名資料", id, sl.GetCommendText(command), modifiedby);
            }
            catch (Exception ex)
            {
                return $"ErrorMsg：{ex.Message}{Environment.NewLine}ErrorStackTrace：{ex.StackTrace}";
            }

            return "";
        }

        /// <summary>
        /// 查詢 模板5報名資料
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        internal DataTable QueryRegisterModel5(string id)
        {
            string connStr = GetConnectionString();
            string sqlString = "";

            sqlString = @"
                        SELECT 
                            [id]
                            ,[eventid]
                            ,[empid]
                            ,[registerdate]
                            ,[attachment1]
                            ,[attachment1_name]
                            ,[description1]
                            ,[attachment2]
                            ,[attachment2_name]
                            ,[description2]
                            ,[attachment3]
                            ,[attachment3_name]
                            ,[description3]
                            ,[feedback]
                            ,[modifiedby]
                            ,[modifieddate]
                        FROM 
                            [dbo].[TEL_Event_RegisterModel5]
                        WHERE
                            id = id
                            ";

            if (!string.IsNullOrEmpty(id))
            {
                sqlString += @"
                            AND id = @id
                                ";
            }

            DataTable result = null;

            using (SqlConnection connection = new SqlConnection(connStr))
            {
                connection.Open();

                SqlDataAdapter wrDad = new SqlDataAdapter();
                DataSet DS = new DataSet();

                wrDad.SelectCommand = new SqlCommand(sqlString, connection);

                if (!string.IsNullOrEmpty(id))
                    wrDad.SelectCommand.Parameters.AddWithValue("@id", id);

                wrDad.Fill(DS, "T");
                result = DS.Tables["T"];
            }

            return result;
        }
        #endregion

        #region RegisterModel6
        /// <summary>
        /// 查詢地點選項
        /// </summary>
        /// <param name="eventid"></param>
        /// <returns></returns>
        internal DataTable QueryAreaOption6(string eventid)
        {
            string connStr = GetConnectionString();
            string sqlStr = "";

            sqlStr = @"
                        SELECT 
                            DISTINCT  [area]
                        FROM 
                            [TEL_Event_RegisterOption6] ";

            if (!string.IsNullOrEmpty(eventid))
            {
                sqlStr += @" 
                        WHERE  [eventid] = @eventid
                            ";
            }

            DataTable result = null;

            using (SqlConnection connection = new SqlConnection(connStr))
            {
                connection.Open();

                SqlDataAdapter wrDad = new SqlDataAdapter();
                DataSet DS = new DataSet();

                wrDad.SelectCommand = new SqlCommand(sqlStr, connection);

                if (!string.IsNullOrEmpty(eventid))
                {
                    wrDad.SelectCommand.Parameters.AddWithValue("@eventid", eventid);
                }

                wrDad.Fill(DS, "T");
                result = DS.Tables["T"];
            }

            return result;
        }

        /// <summary>
        /// 查詢日期選項
        /// </summary>
        /// <param name="eventid"></param>
        /// <returns></returns>
        internal DataTable QueryAvaliableDatOption(string eventid, string area)
        {
            string connStr = GetConnectionString();
            string sqlStr = "";

            sqlStr = @"
                        SELECT 
                            DISTINCT CONVERT(VARCHAR, [avaliabledate],111) AS [avaliabledate]
                        FROM 
                            [TEL_Event_RegisterOption6] 
                        WHERE  id = id";

            if (!string.IsNullOrEmpty(eventid))
            {
                sqlStr += @" 
                       AND [eventid] = @eventid
                            ";
            }

            if (!string.IsNullOrEmpty(area))
            {
                sqlStr += @" 
                       AND [area] = @area
                            ";
            }

            DataTable result = null;

            using (SqlConnection connection = new SqlConnection(connStr))
            {
                connection.Open();

                SqlDataAdapter wrDad = new SqlDataAdapter();
                DataSet DS = new DataSet();

                wrDad.SelectCommand = new SqlCommand(sqlStr, connection);

                if (!string.IsNullOrEmpty(eventid))
                {
                    wrDad.SelectCommand.Parameters.AddWithValue("@eventid", eventid);
                }

                if (!string.IsNullOrEmpty(area))
                {
                    wrDad.SelectCommand.Parameters.AddWithValue("@area", area);

                }

                wrDad.Fill(DS, "T");
                result = DS.Tables["T"];
            }

            return result;
        }

        /// <summary>
        /// 取得Option6 地點時間方案人數上限
        /// </summary>
        /// <param name="eventid"></param>
        /// <param name="area"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        internal int QueryRegisterOption6Limit(string eventid, string area, string date)
        {
            string connStr = GetConnectionString();
            string sqlStr = "";

            sqlStr = @"
                        SELECT 
                            [limit]
                        FROM 
                            [TEL_Event_RegisterOption6]
                        WHERE  
                            [eventid] = @eventid
                        AND
                            area = @area
                        AND
                            CONVERT(VARCHAR, avaliabledate,111) = @date ";

            DataTable result = null;

            using (SqlConnection connection = new SqlConnection(connStr))
            {
                connection.Open();

                SqlDataAdapter wrDad = new SqlDataAdapter();
                DataSet DS = new DataSet();

                wrDad.SelectCommand = new SqlCommand(sqlStr, connection);
                wrDad.SelectCommand.Parameters.AddWithValue("@eventid", eventid);
                wrDad.SelectCommand.Parameters.AddWithValue("@area", area);
                wrDad.SelectCommand.Parameters.AddWithValue("@date", Convert.ToDateTime(date));

                wrDad.Fill(DS, "T");
                result = DS.Tables["T"];
            }

            return result.Rows.Count == 0 ? 0 : Convert.ToInt16(result.Rows[0]["limit"]);
        }

        /// <summary>
        /// 取得地點時間方案報名人數
        /// </summary>
        /// <param name="eventid"></param>
        /// <param name="area"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        internal int GetRegisterOption6Count(string eventid, string area, string date, string registerid)
        {
            string connStr = GetConnectionString();
            string sqlStr = "";

            sqlStr = @"
                        SELECT 
                            COUNT([id]) AS RegisterCount
                        FROM 
                            [TEL_Event_RegisterModel6]
                        WHERE  
                            [eventid] = @eventid
                        AND
                            changearea = @area
                        AND
                            CONVERT(VARCHAR, changedate,111) = @date ";

            if (!string.IsNullOrEmpty(registerid))
            {
                sqlStr += @"
                        AND
                            id != @registerid ";
            }

            DataTable result = null;

            using (SqlConnection connection = new SqlConnection(connStr))
            {
                connection.Open();

                SqlDataAdapter wrDad = new SqlDataAdapter();
                DataSet DS = new DataSet();

                wrDad.SelectCommand = new SqlCommand(sqlStr, connection);
                wrDad.SelectCommand.Parameters.AddWithValue("@eventid", eventid);
                wrDad.SelectCommand.Parameters.AddWithValue("@area", area);
                wrDad.SelectCommand.Parameters.AddWithValue("@date", Convert.ToDateTime(date));

                if (!string.IsNullOrEmpty(registerid))
                    wrDad.SelectCommand.Parameters.AddWithValue("@registerid", registerid);


                wrDad.Fill(DS, "T");
                result = DS.Tables["T"];
            }

            return result.Rows.Count == 0 ? 0 : Convert.ToInt16(result.Rows[0]["RegisterCount"]);
        }

        /// <summary>
        /// 新增 模板5報名資料
        /// </summary>
        /// <param name="eventsData"></param>
        /// <param name="dataTable"></param>
        /// <param name="modifiedby"></param>
        /// <returns></returns>
        internal string InsertRegisterModel6(Dictionary<string, string> data, string modifiedby)
        {
            string connStr = GetConnectionString();
            string sqlStr = "";

            try
            {
                sqlStr = @"
                    INSERT INTO [TEL_Event_RegisterModel6]
                           (
                            [id]
                            ,[eventid]
                            ,[empid]
                            ,[registerdate]
                            ,[changearea]
                            ,[changedate]
                            ,[feedback]
                            ,[modifiedby]
                            ,[modifieddate])
                     VALUES
                           (
                            @id
                            ,@eventid
                            ,@empid
                            ,@registerdate
                            ,@changearea
                            ,@changedate
                            ,@feedback
                            ,@modifiedby
                            ,GETDATE()
                            ) ";

                SqlConnection conn = new SqlConnection(connStr);
                conn.Open();
                SqlCommand command = new SqlCommand(sqlStr, conn);


                command.Parameters.Clear();

                command.Parameters.AddWithValue("@id", data["id"]);
                command.Parameters.AddWithValue("@eventid", data["eventid"]);
                command.Parameters.AddWithValue("@empid", data["empid"]);
                command.Parameters.AddWithValue("@registerdate", data["registerdate"]);
                command.Parameters.AddWithValue("@changearea", data["changearea"]);
                command.Parameters.AddWithValue("@changedate", data["changedate"]);
                command.Parameters.AddWithValue("@feedback", data["feedback"]);
                command.Parameters.AddWithValue("@modifiedby", modifiedby);

                command.ExecuteNonQuery();
                conn.Close();
                conn.Dispose();

                //Add log
                SqlLog sl = new SqlLog();
                sl.AddLog("新增模板1報名資料", data["id"], sl.GetCommendText(command), modifiedby);
            }
            catch (Exception ex)
            {
                return $"ErrorMsg：{ex.Message}{Environment.NewLine}ErrorStackTrace：{ex.StackTrace}";
            }

            return "";
        }


        /// <summary>
        /// 更新 模板5報名資料
        /// </summary>
        /// <param name="data"></param>
        /// <param name="modifiedby"></param>
        /// <returns></returns>
        internal string UpdateRegisterModel6(Dictionary<string, string> data, string modifiedby)
        {
            string connStr = GetConnectionString();
            string sqlStr = "";

            try
            {
                sqlStr = @"
                        UPDATE 
                            [TEL_Event_RegisterModel6]
                        SET 
                            [changearea] = @changearea
                            ,[changedate] = @changedate
                            ,[feedback] = @feedback
                            ,[modifiedby] = @modifiedby
                            ,[modifieddate] = GETDATE()
                        WHERE 
                            id = @id
                        ";

                SqlConnection conn = new SqlConnection(connStr);
                conn.Open();
                SqlCommand command = new SqlCommand(sqlStr, conn);

                command.Parameters.Clear();

                command.Parameters.AddWithValue("@id", data["id"]);
                command.Parameters.AddWithValue("@changearea", data["changearea"]);
                command.Parameters.AddWithValue("@changedate", data["changedate"]);
                command.Parameters.AddWithValue("@feedback", data["feedback"]);
                command.Parameters.AddWithValue("@modifiedby", modifiedby);

                command.ExecuteNonQuery();
                conn.Close();
                conn.Dispose();

                //Add log
                SqlLog sl = new SqlLog();
                sl.AddLog("更新模板6報名資料", data["id"], sl.GetCommendText(command), modifiedby);
            }
            catch (Exception ex)
            {
                return $"ErrorMsg：{ex.Message}{Environment.NewLine}ErrorStackTrace：{ex.StackTrace}";
            }

            return "";
        }

        /// <summary>
        /// 刪除 模板5報名資料
        /// </summary>
        /// <param name="data"></param>
        /// <param name="modifiedby"></param>
        /// <returns></returns>
        internal string DeleteRegisterModel6(string id, string modifiedby)
        {
            string connStr = GetConnectionString();
            string sqlStr = "";

            try
            {
                sqlStr = @"
                        DELETE FROM [dbo].[TEL_Event_RegisterModel6]
                        WHERE 
                            id = @id
                        ";

                SqlConnection conn = new SqlConnection(connStr);
                conn.Open();
                SqlCommand command = new SqlCommand(sqlStr, conn);


                command.Parameters.Clear();
                command.Parameters.AddWithValue("@id", id);


                command.ExecuteNonQuery();
                conn.Close();
                conn.Dispose();

                //Add log
                SqlLog sl = new SqlLog();
                sl.AddLog("刪除模板6報名資料", id, sl.GetCommendText(command), modifiedby);
            }
            catch (Exception ex)
            {
                return $"ErrorMsg：{ex.Message}{Environment.NewLine}ErrorStackTrace：{ex.StackTrace}";
            }

            return "";
        }

        /// <summary>
        /// 查詢 模板5報名資料
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        internal DataTable QueryRegisterModel6(string id)
        {
            string connStr = GetConnectionString();
            string sqlString = "";

            sqlString = @"
                        SELECT 
                            [id]
                            ,[eventid]
                            ,[empid]
                            ,[registerdate]
                            ,[changearea]
                            ,CONVERT(VARCHAR, [changedate],111) AS [changedate]
                            ,[feedback]
                            ,[modifiedby]
                            ,[modifieddate]
                        FROM 
                            [dbo].[TEL_Event_RegisterModel6]
                        WHERE
                            id = id
                            ";

            if (!string.IsNullOrEmpty(id))
            {
                sqlString += @"
                            AND id = @id
                                ";
            }

            DataTable result = null;

            using (SqlConnection connection = new SqlConnection(connStr))
            {
                connection.Open();

                SqlDataAdapter wrDad = new SqlDataAdapter();
                DataSet DS = new DataSet();

                wrDad.SelectCommand = new SqlCommand(sqlString, connection);

                if (!string.IsNullOrEmpty(id))
                    wrDad.SelectCommand.Parameters.AddWithValue("@id", id);

                wrDad.Fill(DS, "T");
                result = DS.Tables["T"];
            }

            return result;
        }
        #endregion
    }
}
