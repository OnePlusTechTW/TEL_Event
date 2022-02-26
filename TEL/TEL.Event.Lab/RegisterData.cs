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
        
        /// <summary>
        /// 取得 RegisterModel1 excel 匯出資料
        /// </summary>
        /// <param name="eventid"></param>
        /// <param name="empName"></param>
        /// <returns></returns>
        internal DataTable QueryExportRegisterModel1Data(string eventid, string empName)
        {
            string connStr = GetConnectionString();
            string sqlString = string.Empty;

            sqlString = @"
                        SELECT
                            [id] as registerid
                            ,[eventid]
                            ,a.[empid]
                            ,selectedoption
                            ,feedback
                            ,CONVERT(VARCHAR, a.[registerdate],20) AS [registerdate]
                            ,[modifiedby]
                            ,CONVERT(VARCHAR, a.[modifieddate],20) AS [modifieddate]
                        FROM [TEL_Event_RegisterModel1] a
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

        /// <summary>
        /// 取得 RegisterModel2 excel 匯出資料
        /// </summary>
        /// <param name="eventid"></param>
        /// <param name="empName"></param>
        /// <returns></returns>
        internal DataTable QueryExportRegisterModel2Data(string eventid, string empName)
        {
            string connStr = GetConnectionString();
            string sqlString = string.Empty;

            sqlString = @"
                        SELECT
                            [id] as registerid
                            ,[eventid]
                            ,a.[empid]
                            ,[mobile]
                            ,selectedoption
                            ,[traffic]
                            ,[meal]
                            ,feedback
                            ,CONVERT(VARCHAR, a.[registerdate],20) AS [registerdate]
                            ,[modifiedby]
                            ,CONVERT(VARCHAR, a.[modifieddate],20) AS [modifieddate]
                        FROM [TEL_Event_RegisterModel2] a
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

        /// <summary>
        /// 取得 RegisterModel2family excel 匯出資料
        /// </summary>
        /// <param name="eventid"></param>
        /// <param name="empName"></param>
        /// <returns></returns>
        internal DataTable QueryExportRegisterModel2FamilyData(string eventid, string empName)
        {
            string connStr = GetConnectionString();
            string sqlString = string.Empty;

            sqlString = @"
                        SELECT
                            a.[id] as registerid
                            ,a.[eventid]
                            ,a.[empid]
                            ,CONVERT(VARCHAR, a.[registerdate],20) AS [registerdate]
                            ,b.[name]
                            ,b.[idno]
                            ,CONVERT(VARCHAR, b.[birthday],111) AS birthdayfamily
                            ,b.[gender]
                            ,b.[meal]
                            ,a.[modifiedby]
                            ,CONVERT(VARCHAR, a.[modifieddate],20) AS [modifieddate]
                        FROM 
                            TEL_Event_RegisterModel2 a
                        INNER JOIN TEL_Event_RegisterModel2family b ON a.[id] = b.[registerid]
                        INNER JOIN TEL_Event_UserFullName c ON a.empid=c.EmpID 
                        WHERE
                            a.eventid=@eventid ";

            if (!string.IsNullOrEmpty(empName))
            {
                sqlString += "" +
                    "   AND (c.empid LIKE @empid OR c.FullnameEN LIKE @empid OR c.FullnameCH LIKE @empid) ";
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

        /// <summary>
        /// 取得 RegisterModel3family excel 匯出資料
        /// </summary>
        /// <param name="eventid"></param>
        /// <param name="empName"></param>
        /// <returns></returns>
        internal DataTable QueryExportRegisterModel3Data(string eventid, string empName)
        {
            string connStr = GetConnectionString();
            string sqlString = string.Empty;

            sqlString = @"
                        SELECT
                            a.[id] as registerid
                            ,a.[eventid]
                            ,a.[empid]
                            ,CONVERT(VARCHAR, a.[registerdate],20) AS [registerdate]
                            ,[examineeidentity]
                            ,[examineename]
                            ,[examineeidno]
                            ,CONVERT(VARCHAR, a.[examineebirthday],111) AS [examineebirthday]
                            ,[examineemobile]
                            ,[hosipital]
                            ,[area]
                            ,[solution]
                            ,a.[gender]
                            ,CONVERT(VARCHAR, a.[expectdate],111) AS [expectdate]
                            ,CONVERT(VARCHAR, a.[seconddate],111) AS [seconddate]
                            ,[secondsolution1]
                            ,[secondsolution2]
                            ,[secondsolution3]
                            ,[optional]
                            ,a.[address]
                            ,[meal]
                            ,[feedback]
                            ,a.[modifiedby]
                            ,CONVERT(VARCHAR, a.[modifieddate],20) AS [modifieddate]
                        FROM 
                            TEL_Event_RegisterModel3 a
                        INNER JOIN TEL_Event_UserFullName c ON a.empid=c.EmpID 
                        WHERE
                            a.eventid=@eventid ";

            if (!string.IsNullOrEmpty(empName))
            {
                sqlString += "" +
                    "   AND (c.empid LIKE @empid OR c.FullnameEN LIKE @empid OR c.FullnameCH LIKE @empid) ";
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

        /// <summary>
        /// 取得 RegisterModel4family excel 匯出資料
        /// </summary>
        /// <param name="eventid"></param>
        /// <param name="empName"></param>
        /// <returns></returns>
        internal DataTable QueryExportRegisterModel4Data(string eventid, string empName)
        {
            string connStr = GetConnectionString();
            string sqlString = string.Empty;

            sqlString = @"
                        SELECT
                            a.[id] as registerid
                            ,a.[eventid]
                            ,a.[empid]
                            ,CONVERT(VARCHAR, a.[registerdate],20) AS [registerdate]
                            ,[examineeidentity]
                            ,[examineename]
                            ,[examineename2]
                            ,[examineeidno]
                            ,CONVERT(VARCHAR, a.[examineebirthday],111) AS [examineebirthday]
                            ,[examineemobile]
                            ,[hosipital]
                            ,[area]
                            ,[solution]
                            ,a.[gender]
                            ,CONVERT(VARCHAR, a.[expectdate],111) AS [expectdate]
                            ,CONVERT(VARCHAR, a.[seconddate],111) AS [seconddate]
                            ,[secondsolution1]
                            ,[secondsolution2]
                            ,[secondsolution3]
                            ,[optional]
                            ,a.[address]
                            ,[meal]
                            ,[needhotel]
                            ,[checkininfo]
                            ,[feedback]
                            ,a.[modifiedby]
                            ,CONVERT(VARCHAR, a.[modifieddate],20) AS [modifieddate]
                        FROM 
                            TEL_Event_RegisterModel4 a
                        INNER JOIN TEL_Event_UserFullName c ON a.empid=c.EmpID 
                        WHERE
                            a.eventid=@eventid ";

            if (!string.IsNullOrEmpty(empName))
            {
                sqlString += "" +
                    "   AND (c.empid LIKE @empid OR c.FullnameEN LIKE @empid OR c.FullnameCH LIKE @empid) ";
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

        /// <summary>
        /// 取得 RegisterModel5family excel 匯出資料
        /// </summary>
        /// <param name="eventid"></param>
        /// <param name="empName"></param>
        /// <returns></returns>
        internal DataTable QueryExportRegisterModel5Data(string eventid, string empName)
        {
            string connStr = GetConnectionString();
            string sqlString = string.Empty;

            sqlString = @"
                        SELECT
                            a.[id] as registerid
                            ,a.[eventid]
                            ,a.[empid]
                            ,CONVERT(VARCHAR, a.[registerdate],20) AS [registerdate]
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
                            ,a.[modifiedby]
                            ,CONVERT(VARCHAR, a.[modifieddate],20) AS [modifieddate]
                        FROM 
                            TEL_Event_RegisterModel5 a
                        INNER JOIN TEL_Event_UserFullName c ON a.empid=c.EmpID 
                        WHERE
                            a.eventid=@eventid ";

            if (!string.IsNullOrEmpty(empName))
            {
                sqlString += "" +
                    "   AND (c.empid LIKE @empid OR c.FullnameEN LIKE @empid OR c.FullnameCH LIKE @empid) ";
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

        /// <summary>
        /// 取得 RegisterModel6family excel 匯出資料
        /// </summary>
        /// <param name="eventid"></param>
        /// <param name="empName"></param>
        /// <returns></returns>
        internal DataTable QueryExportRegisterModel6Data(string eventid, string empName)
        {
            string connStr = GetConnectionString();
            string sqlString = string.Empty;

            sqlString = @"
                        SELECT
                            a.[id] as registerid
                            ,a.[eventid]
                            ,a.[empid]
                            ,CONVERT(VARCHAR, a.[registerdate],20) AS [registerdate]
                            ,[changearea]
                            ,CONVERT(VARCHAR, a.[changedate],20) AS [changedate]
                            ,[feedback]
                            ,a.[modifiedby]
                            ,CONVERT(VARCHAR, a.[modifieddate],20) AS [modifieddate]
                        FROM 
                            TEL_Event_RegisterModel6 a
                        INNER JOIN TEL_Event_UserFullName c ON a.empid=c.EmpID 
                        WHERE
                            a.eventid=@eventid ";

            if (!string.IsNullOrEmpty(empName))
            {
                sqlString += "" +
                    "   AND (c.empid LIKE @empid OR c.FullnameEN LIKE @empid OR c.FullnameCH LIKE @empid) ";
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
