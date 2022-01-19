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
        public DataTable GetSurvey(string eventid, string surveymodel, string empid)
        {
            SurveyData sv = new SurveyData();
            return sv.QuerySurvey(eventid, surveymodel, empid);
        }

        //查詢問卷填寫人數
        public String GetSurveyFillinCount(string eventid, string registermodel, string surveymodel)
        {
            SurveyData sv = new SurveyData();
            EventData ev = new EventData();
            return sv.QuerySurveyFillinCount(eventid, surveymodel) + " / " + ev.QueryEvnetRegisterCount(eventid, registermodel);
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

            return sd.SaveEventDataMModel1(eventid, empid, q1, q1other, q2, q3, q4, q5, q6, q7, q7reason, q8, q9, q10);
        }

        //儲存模板2問卷資料(滿意度(活動))
        public String SaveSurveyDataMModel2(string eventid, string empid, string q1, string q1other, string q2, string q3, string q4, string q5, string q6, string q7, string q8)
        {
            SurveyData sd = new SurveyData();

            return sd.SaveEventDataMModel2(eventid, empid, q1, q1other, q2, q3, q4, q5, q6, q7, q8);
        }

        //儲存模板3問卷資料(滿意度(健檢))
        public String SaveSurveyDataMModel3(string eventid, string empid, string q1, string q2, string q2reason, string q3, string q3reason, string q4, string q4reason, string q5, string q5reason, string q6, string q6reason, string q7, string q7reason, string q8, string q9)
        {
            SurveyData sd = new SurveyData();

            return sd.SaveEventDataMModel3(eventid, empid, q1, q2, q2reason, q3, q3reason, q4, q4reason, q5, q5reason, q6, q6reason, q7, q7reason, q8, q9);
        }

        //儲存模板4問卷資料(滿意度(電腦替換))
        public String SaveSurveyDataMModel4(string eventid, string empid, string q1, string q2, string q3, string q4, string q5)
        {
            SurveyData sd = new SurveyData();

            return sd.SaveEventDataMModel4(eventid, empid, q1, q2, q3, q4, q5);
        }

        //Load問卷資料
        public DataTable GetSurveyData(string surveyid, string surveymodel)
        {
            SurveyData sv = new SurveyData();
            return sv.QuerySurveyData(surveyid, surveymodel);
        }
    }
}
