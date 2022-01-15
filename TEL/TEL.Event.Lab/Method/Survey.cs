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
    }
}
