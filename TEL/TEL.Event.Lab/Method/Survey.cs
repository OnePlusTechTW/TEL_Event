using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using TEL.Event.Lab.Data;

namespace TEL.Event.Lab.Method
{
    public class Survey
    {
        //查詢活動問卷填寫資料
        public DataTable GetSurvey(string eventid, string surveymodel,string empid)
        {
            SurveyData sv = new SurveyData();
            return sv.QuerySurvey(eventid, surveymodel,empid);
        }

        //查詢問卷填寫人數
        public String GetSurveyFillinCount(string eventid, string registermodel, string surveymodel)
        {
            SurveyData sv = new SurveyData();
            EventData ev = new EventData();
            return sv.QuerySurveyFillinCount(eventid, surveymodel) + " / " +ev.QueryEvnetRegisterCount(eventid, registermodel);
        }

        //刪除活動問卷填寫資料
        public void DeleteSurveyData(string surveyid, string surveymodel)
        {
            SurveyData sv = new SurveyData();
            sv.DeleteSurveyData(surveyid, surveymodel);
        }


        //儲存模板1問卷資料(滿意度(講座))
        public String SaveSurveyDataMModel1(string eventid, string empid, string q1, string q1other, string q2, string q3, string q4, string q5, string q6, string q7, string q7reason, string q8, string q9, string q10)
        {
            SurveyData sd = new SurveyData();

            return sd.SaveEventDataMModel1(eventid,  empid,  q1,  q1other,  q2,  q3,  q4,  q5,  q6, q7, q7reason, q8, q9, q10);
        }

        public DataTable GetSurveyData(string surveyid, string surveymodel)
        {
            SurveyData sv = new SurveyData();
            return sv.QuerySurveyData(surveyid, surveymodel);
        }
    }
}
