using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace TEL.Event.Lab.Data
{
    public class EventData
    {
        public DataTable GetMyEvent(string name, string catid, string status, string empid)
        {
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["tel_event"].ConnectionString;
            string sqlString = "";

            sqlString = @"select a.name as eventname,c.name as categoryname,
                          replace(convert(varchar, a.registerstart,120),'-','/') as registerstart,replace(convert(varchar, a.registerend,120),'-','/') as registerend,
                          convert(varchar, a.eventstart,111) as eventstart,convert(varchar, a.eventend,111) as eventend,a.registermodel,
                          a.surveystartdate,a.surveymodel,b.id as registerid, d.id as surveyid, 
						  cast(a.id as varchar(40))+'_'+a.registermodel+'_'+cast(b.id as varchar(40)) as registerinfo,
                          cast(a.id as varchar(40))+'_'+a.surveymodel+'_'+isnull(cast(d.id as varchar(40)),'') as surveyinfo,
						  case when a.eventstart>getdate() then '尚未開始' when a.eventend<getdate() then '已結束' else '進行中' end as status
                          from [dbo].[TEL_Event_Events] a
                          inner join 
                          (select id,eventid,empid from [dbo].[TEL_Event_RegisterModel1]
                           union
                           select id,eventid,empid from [dbo].[TEL_Event_RegisterModel2]
                           union
                           select id,eventid,empid from [dbo].[TEL_Event_RegisterModel3]
                           union
                           select id,eventid,empid from [dbo].[TEL_Event_RegisterModel4]
                           union
                           select id,eventid,empid from [dbo].[TEL_Event_RegisterModel5]
                           union
                           select id,eventid,empid from [dbo].[TEL_Event_RegisterModel6]) b on a.id=b.eventid
                          inner join [dbo].[TEL_Event_Category] c on a.categoryid=c.id
                          left join 
                          (select id,eventid,empid from [dbo].[TEL_Event_SurveyModel1]
                           union 
                           select id,eventid,empid from [dbo].[TEL_Event_SurveyModel2]
                           union 
                           select id,eventid,empid from [dbo].[TEL_Event_SurveyModel3]
                           union 
                           select id,eventid,empid from [dbo].[TEL_Event_SurveyModel4])d on a.id=d.eventid 
                          where b.empid=@empid ";

            if (!string.IsNullOrEmpty(name))
            {
                sqlString += "and a.name like @name ";
            }

            if (!string.IsNullOrEmpty(catid))
            {
                sqlString += "and c.id=@catid ";
            }

            if (!string.IsNullOrEmpty(status))
            {
                if (status == "N")
                    sqlString += "and a.eventstart>getdate() ";
                else if (status == "O")
                    sqlString += "and (a.eventstart<=getdate() and a.eventend>=getdate()) ";
                else if (status == "F")
                    sqlString += "and a.eventend<getdate() ";
            }

            sqlString += "order by a.eventstart desc";

            DataTable result = null;

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

            return result;
        }
    }
}
