using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using TEL.Event.Lab.Method;

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
        public DataTable QueryEventInfo(string eventid = "", string eventname = "", string eventcateid = "", string eventSdate = "", string eventEdate="", string status = "", string enabled = "")
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
                         WHERE 
                               a.id = a.id ";

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

            sqlString += @" ORDER BY  a.eventstart";

            DataTable result = null;

            using (SqlConnection connection = new SqlConnection(connStr))
            {
                connection.Open();

                SqlDataAdapter wrDad = new SqlDataAdapter();
                DataSet DS = new DataSet();

                wrDad.SelectCommand = new SqlCommand(sqlString, connection);

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
                            a.surveystartdate
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
        /// <param name="eventname"></param>
        /// <param name="eventcateid"></param>
        /// <returns></returns>
        public DataTable QueryUserRegisterEventList(string eventname = "", string eventcateid = "")
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
                        a.surveystartdate,
                        c.id as RegisterModelID
                    FROM 
                        TEL_Event_Events a
                    INNER JOIN 
                        TEL_Event_Category b ON a.categoryid=b.id
                    LEFT JOIN 
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
                        SELECT id,eventid,empid FROM TEL_Event_RegisterModel6) c ON a.id = c.eventid
                    WHERE 
                        a.id = a.id 
                    AND
                        a.eventend >= GETDATE()
                    AND
                        a.registerend >= GETDATE()
                    AND
                        a.enabled = 'Y'
                        ";

            if (!string.IsNullOrEmpty(eventname))
            {
                sqlString += @" AND a.name like '%' + @eventname + '%'  ";
            }

            if (!string.IsNullOrEmpty(eventcateid))
            {
                sqlString += @" AND a.categoryid = @eventcateid ";
            }

            sqlString += @" ORDER BY  a.eventstart";

            DataTable result = null;

            using (SqlConnection connection = new SqlConnection(connStr))
            {
                connection.Open();

                SqlDataAdapter wrDad = new SqlDataAdapter();
                DataSet DS = new DataSet();

                wrDad.SelectCommand = new SqlCommand(sqlString, connection);

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

                commandEvents.ExecuteNonQuery();

                //再新增
                string eventID = eventAdminData["eventid"];
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
                            ,CONVERT(varchar, [avaliabledate], 111) as avaliabledate
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
    }
}
