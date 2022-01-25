using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace TEL.Event.Lab.Data
{
    public class SystemSetupData
    {
        #region 活動分類
        /// <summary>
        /// 新增活動分類
        /// </summary>
        /// <param name="name"></param>
        /// <param name="color"></param>
        /// <param name="enabled"></param>
        /// <param name="empid"></param>
        /// <returns></returns>
        public string InsertEventCategory(string name, string color, string enabled, string empid)
        {
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["tel_event"].ConnectionString;
            string sqlStr = "";

            try
            {
                sqlStr = @"
                        INSERT INTO 
                        TEL_Event_Category
                            (
                                id,
                                name,
                                color,
                                enabled,
                                modifiedby,
                                modifieddate
                            )
                        VALUES 
                            (
                                newid(),
                                @name,
                                @color,
                                @enabled,
                                @modifiedby,
                                GETDATE()
                            )";

                SqlConnection conn = new SqlConnection(connStr);
                conn.Open();
                SqlCommand command = new SqlCommand(sqlStr, conn);


                command.Parameters.Clear();
                command.Parameters.AddWithValue("@name", name);

                if (!string.IsNullOrEmpty(color))
                    command.Parameters.AddWithValue("@color", color);
                else
                    command.Parameters.AddWithValue("@color", System.DBNull.Value);

                command.Parameters.AddWithValue("@enabled", enabled);
                command.Parameters.AddWithValue("@modifiedby", empid);


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
        /// 更新活動分類
        /// </summary>
        /// <param name="id"></param>
        /// <param name="color"></param>
        /// <param name="enabled"></param>
        /// <param name="empid"></param>
        /// <returns></returns>
        internal string UpdateEventCategory(string id, string color, string enabled, string empid)
        {
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["tel_event"].ConnectionString;
            string sqlStr = "";

            try
            {
                sqlStr = @"
                        UPDATE
                            TEL_Event_Category
                        SET
                            color = @color,
                            enabled = @enabled,
                            modifiedby = @modifiedby,
                            modifieddate = GETDATE()
                        WHERE
                            id = @id";

                SqlConnection conn = new SqlConnection(connStr);
                conn.Open();
                SqlCommand command = new SqlCommand(sqlStr, conn);


                command.Parameters.Clear();

                command.Parameters.AddWithValue("@id", id);

                if (!string.IsNullOrEmpty(color))
                    command.Parameters.AddWithValue("@color", color);
                else
                    command.Parameters.AddWithValue("@color", System.DBNull.Value);

                command.Parameters.AddWithValue("@enabled", enabled);
                command.Parameters.AddWithValue("@modifiedby", empid);


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
        /// 刪除活動分類
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        internal string DeleteEventCategory(string id)
        {
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["tel_event"].ConnectionString;
            string sqlStr = "";

            try
            {
                sqlStr = @"
                DELETE 
                    TEL_Event_Category 
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
        /// 查詢活動分類
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        internal DataTable QueryEventCategory(string name)
        {
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["tel_event"].ConnectionString;
            string sqlStr = "";

            sqlStr = @"
                        SELECT 
                            [id]
	                        ,[name]
	                        ,[color]
	                        ,[enabled]
	                        ,[modifiedby]
	                        ,[modifieddate]
                        FROM 
                            [TEL_Event_Category] ";

            if (!string.IsNullOrEmpty(name))
            {
                sqlStr += @" 
                        WHERE  [name] = @name
                            ";
            }

            sqlStr += @"
                        ORDER BY [name]";

            DataTable result = null;

            using (SqlConnection connection = new SqlConnection(connStr))
            {
                connection.Open();

                SqlDataAdapter wrDad = new SqlDataAdapter();
                DataSet DS = new DataSet();

                wrDad.SelectCommand = new SqlCommand(sqlStr, connection);

                if (!string.IsNullOrEmpty(name))
                {
                    wrDad.SelectCommand.Parameters.AddWithValue("@name", name);
                }

                wrDad.Fill(DS, "T");
                result = DS.Tables["T"];
            }

            return result;
        }
        #endregion

        #region 常態活動管理者
        /// <summary>
        /// 新增常態活動管理者
        /// </summary>
        /// <param name="empid"></param>
        /// <param name="modifiedby"></param>
        /// <returns></returns>
        internal string InsertEventAdmin(string empid, string modifiedby)
        {
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["tel_event"].ConnectionString;
            string sqlStr = "";

            try
            {
                sqlStr = @"
                        INSERT INTO 
                        TEL_Event_Admin
                            (
                                empid,
                                modifiedby,
                                modifieddate
                            )
                        VALUES 
                            (
                                @empid,
                                @modifiedby,
                                GETDATE()
                            )";

                SqlConnection conn = new SqlConnection(connStr);
                conn.Open();
                SqlCommand command = new SqlCommand(sqlStr, conn);


                command.Parameters.Clear();
                command.Parameters.AddWithValue("@empid", empid);
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
        /// 刪除常態活動管理者
        /// </summary>
        /// <param name="empID"></param>
        /// <returns></returns>
        internal string DeleteEventAdmin(string empID)
        {
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["tel_event"].ConnectionString;
            string sqlStr = "";

            try
            {
                sqlStr = @"
                DELETE 
                    TEL_Event_Admin 
                WHERE 
                    empid = @empid";

                SqlConnection sqlConn = new SqlConnection(connStr);
                sqlConn.Open();
                SqlCommand sqlcommand = new SqlCommand(sqlStr, sqlConn);
                sqlcommand.Parameters.Clear();
                sqlcommand.Parameters.AddWithValue("@empid", empID);
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
        /// 查詢常態活動管理者
        /// </summary>
        /// <param name="empid"></param>
        /// <returns></returns>
        internal DataTable QueryEventManager(string empid)
        {
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["tel_event"].ConnectionString;
            string sqlStr = "";

            sqlStr = @"
                        SELECT 
                            ea.[empid]
                            , u.FirstNameEN+' '+u.LastNameEN as name
                            ,[modifiedby]
                            ,[modifieddate]
                        FROM 
                            [TEL_Event_Admin] ea 
                        INNER JOIN Users u ON u.EmpID = ea.empid ";

            if (!string.IsNullOrEmpty(empid))
            {
                sqlStr += @" 
                        WHERE  ea.[empid] = @empid 
                            ";
            }

            sqlStr += @" 
                        ORDER BY ea.[empid]";

            DataTable result = null;

            using (SqlConnection connection = new SqlConnection(connStr))
            {
                connection.Open();

                SqlDataAdapter wrDad = new SqlDataAdapter();
                DataSet DS = new DataSet();

                wrDad.SelectCommand = new SqlCommand(sqlStr, connection);

                if (!string.IsNullOrEmpty(empid))
                {
                    wrDad.SelectCommand.Parameters.AddWithValue("@empid", empid);
                }

                wrDad.Fill(DS, "T");
                result = DS.Tables["T"];
            }

            return result;
        }
        #endregion

        #region 郵件群組
        /// <summary>
        /// 新增郵件群組
        /// </summary>
        /// <param name="name"></param>
        /// <param name="enabled"></param>
        /// <param name="modifiedby"></param>
        /// <returns></returns>
        internal string InsertMailGroup(string name, string enabled, string modifiedby)
        {
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["tel_event"].ConnectionString;
            string sqlStr = "";

            try
            {
                sqlStr = @"
                        INSERT INTO 
                        TEL_Event_Mailgroup
                            (
                                id,
                                name,
                                enabled,
                                modifiedby,
                                modifieddate
                            )
                        VALUES 
                            (
                                newid(),
                                @name,
                                @enabled,
                                @modifiedby,
                                GETDATE()
                            )";

                SqlConnection conn = new SqlConnection(connStr);
                conn.Open();
                SqlCommand command = new SqlCommand(sqlStr, conn);


                command.Parameters.Clear();
                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@enabled", enabled);
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
        /// 更新郵件群組
        /// </summary>
        /// <param name="id"></param>
        /// <param name="enabled"></param>
        /// <param name="modifiedby"></param>
        /// <returns></returns>
        internal string UpdateMailGroup(string id, string enabled, string modifiedby)
        {
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["tel_event"].ConnectionString;
            string sqlStr = "";

            try
            {
                sqlStr = @"
                        UPDATE
                            TEL_Event_Mailgroup
                        SET
                            enabled = @enabled,
                            modifiedby = @modifiedby,
                            modifieddate = GETDATE()
                        WHERE
                            id = @id";

                SqlConnection conn = new SqlConnection(connStr);
                conn.Open();
                SqlCommand command = new SqlCommand(sqlStr, conn);


                command.Parameters.Clear();

                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@enabled", enabled);
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
        /// 刪除郵件群組
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        internal string DeleteMailGroup(string id)
        {
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["tel_event"].ConnectionString;
            string sqlStr = "";

            try
            {
                sqlStr = @"
                DELETE 
                    TEL_Event_Mailgroup 
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
        /// 查詢郵件群組
        /// </summary>
        /// <returns></returns>
        internal DataTable QueryMailGroup(string name)
        {
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["tel_event"].ConnectionString;
            string sqlStr = "";

            sqlStr = @"
                        SELECT 
                            [id]
	                        ,[name]
	                        ,[enabled]
	                        ,[modifiedby]
	                        ,[modifieddate]
                        FROM 
                            TEL_Event_Mailgroup ";

            if (!string.IsNullOrEmpty(name))
            {
                sqlStr += @" 
                        WHERE  [name] = @name
                            ";
            }

            sqlStr += @"
                        ORDER BY [name]";

            DataTable result = null;

            using (SqlConnection connection = new SqlConnection(connStr))
            {
                connection.Open();

                SqlDataAdapter wrDad = new SqlDataAdapter();
                DataSet DS = new DataSet();

                wrDad.SelectCommand = new SqlCommand(sqlStr, connection);
                if (!string.IsNullOrEmpty(name))
                {
                    wrDad.SelectCommand.Parameters.AddWithValue("@name", name);
                }

                wrDad.Fill(DS, "T");
                result = DS.Tables["T"];
            }

            return result;
        }
        #endregion

        #region 員工健檢

        #endregion
    }
}
