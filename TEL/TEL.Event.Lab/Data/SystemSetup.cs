using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace TEL.Event.Lab.Data
{
    public class SystemSetup
    {
        public DataTable GetEventCategory(string flag)
        {
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["tel_event"].ConnectionString;
            string sqlString = "";

            if (flag == "All")
                sqlString = @"select id,name from TEL_Event_Category with(NOLOCK) order by name";
            else
                sqlString = @"select id,name from TEL_Event_Category with(NOLOCK) where enabled='Y' order by name";

            DataTable result = null;

            using (SqlConnection connection = new SqlConnection(connStr))
            {
                connection.Open();

                SqlDataAdapter wrDad = new SqlDataAdapter();
                DataSet DS = new DataSet();

                wrDad.SelectCommand = new SqlCommand(sqlString, connection);

                wrDad.Fill(DS, "T");
                result = DS.Tables["T"];
            }

            return result;
        }
    }
}
