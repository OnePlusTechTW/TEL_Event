using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace TEL.Event.Lab.Data
{
    class Log
    {
        //Insert 使用者新增、編輯、刪除的SQL LOG
        public String InsertLog(string description, string relatedid, string sqlstr, string editor)
        {
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["tel_event"].ConnectionString;
            string sqlString = "";

            try
            {
                sqlString = @"INSERT INTO Log (Description,RelatedID,SQLStr,Editer,EditDate) 
                              VALUES (@description,@relatedid,@sqlstr,@editor,GETDATE())";

                SqlConnection sqlConn = new SqlConnection(connStr);
                sqlConn.Open();
                SqlCommand sqlcommand = new SqlCommand(sqlString, sqlConn);
                sqlcommand.Parameters.Clear();

                // create your parameters
                sqlcommand.Parameters.Add("@description", System.Data.SqlDbType.NVarChar);
                sqlcommand.Parameters.Add("@relatedid", System.Data.SqlDbType.UniqueIdentifier);
                sqlcommand.Parameters.Add("@sqlstr", System.Data.SqlDbType.NVarChar);
                sqlcommand.Parameters.Add("@editor", System.Data.SqlDbType.NVarChar);

                // set values to parameters from textboxes
                sqlcommand.Parameters["@description"].Value = description;
                sqlcommand.Parameters["@relatedid"].Value = System.Guid.Parse(relatedid);
                sqlcommand.Parameters["@sqlstr"].Value = sqlstr;
                sqlcommand.Parameters["@editor"].Value = editor;

                int RowsAffected = sqlcommand.ExecuteNonQuery();
                sqlConn.Close();
                sqlConn.Dispose();
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }

            return "";
        }

    }
}
