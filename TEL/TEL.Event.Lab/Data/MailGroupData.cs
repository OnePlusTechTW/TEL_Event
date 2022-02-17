using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEL.Event.Lab.Data
{
    public class MailGroupData
    {
        private string GetConnectionString()
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings["tel_event"].ConnectionString;
        }

        /// <summary>
        /// 查詢User郵件群組 By MailGroup
        /// </summary>
        /// <returns></returns>
        internal DataTable QueryUserMailGroupByMailGroup(string name)
        {
            string connStr = GetConnectionString();
            string sqlStr = "";

            sqlStr = @"
                        SELECT
	                        [UID]
	                        ,[Name]
	                        ,[Address]
	                        ,[EmpID]
                        FROM 
	                        [MailGroup] 
                        WHERE  
                            [Name] = @name";


            DataTable result = null;

            using (SqlConnection connection = new SqlConnection(connStr))
            {
                connection.Open();

                SqlDataAdapter wrDad = new SqlDataAdapter();
                DataSet DS = new DataSet();

                wrDad.SelectCommand = new SqlCommand(sqlStr, connection);

                wrDad.SelectCommand.Parameters.AddWithValue("@name", name);

                wrDad.Fill(DS, "T");
                result = DS.Tables["T"];
            }

            return result;
        }

        /// <summary>
        /// 查詢User郵件群組 By MailGroup
        /// </summary>
        /// <returns></returns>
        internal DataTable QueryUserMailGroupByEmpid(string empid)
        {
            string connStr = GetConnectionString();
            string sqlStr = "";

            sqlStr = @"
                        SELECT
	                        [UID]
	                        ,[Name]
	                        ,[Address]
	                        ,[EmpID]
                        FROM 
	                        [MailGroup] 
                        WHERE  
                            [EmpID] = @empid";


            DataTable result = null;

            using (SqlConnection connection = new SqlConnection(connStr))
            {
                connection.Open();

                SqlDataAdapter wrDad = new SqlDataAdapter();
                DataSet DS = new DataSet();

                wrDad.SelectCommand = new SqlCommand(sqlStr, connection);

                wrDad.SelectCommand.Parameters.AddWithValue("@empid", empid);

                wrDad.Fill(DS, "T");
                result = DS.Tables["T"];
            }

            return result;
        }
    }
}
