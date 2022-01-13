using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace TEL.Event.Lab.Data
{
    internal class UserData
    {
        internal DataTable QueryUserInfoTable(string empID)
        {
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["tel_event"].ConnectionString;
            string sqlString = $@"select* from Users with(NOLOCK) where EmpID =@empid";

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

            return result;
        }

        internal int QueryEventManagerTable(string empID)
        {
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["tel_event"].ConnectionString;
            string sqlString = $@"select* from TEL_Event_Admin with(NOLOCK) where EmpID =@empid";

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

        internal int QueryOtherEventManagerTable(string empID)
        {
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["tel_event"].ConnectionString;
            string sqlString = $@"select* from TEL_Event_EventAdmin with(NOLOCK) where EmpID =@empid";

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
    }
}
