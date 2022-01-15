using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using TEL.Event.Lab.Data;

namespace TEL.Event.Lab.Method
{
    //活動細部資訊object
    public class EventInfo
    {
        public string EventName = "";
        public string EventCategory = "";
        public string EventCategoryColor = "";
        public string EventStart = "";
        public string EventEnd = "";
        public string EventRegisterStart = "";
        public string EventRegisterEnd = "";
        public string EventLimit = "";
        public string EventDescription = "";
        public string EventRegisterModel = "";
        public string EventSurveyModel = "";

        public EventInfo(string eventid)
        {
            EventData eventinfo = new EventData();
            DataTable WMTB = eventinfo.QueryEventInfo(eventid);

            if (WMTB.Rows.Count > 0)
            {
                EventName = WMTB.Rows[0]["eventname"].ToString();
                EventCategory = WMTB.Rows[0]["categoryname"].ToString();
                EventCategoryColor = WMTB.Rows[0]["categorycolor"].ToString();
                EventStart = WMTB.Rows[0]["eventstart"].ToString();
                EventEnd = WMTB.Rows[0]["eventend"].ToString();
                EventRegisterStart = WMTB.Rows[0]["registerstart"].ToString();
                EventRegisterEnd = WMTB.Rows[0]["registerend"].ToString();
                EventLimit = WMTB.Rows[0]["limit"].ToString();
                EventDescription = WMTB.Rows[0]["description"].ToString();
                EventRegisterModel = WMTB.Rows[0]["registermodel"].ToString();
                EventSurveyModel = WMTB.Rows[0]["surveymodel"].ToString();
            }
        }
    }
}
