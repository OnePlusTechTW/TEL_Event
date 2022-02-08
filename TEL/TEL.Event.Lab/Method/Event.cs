using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using TEL.Event.Lab.Data;

namespace TEL.Event.Lab.Method
{
    public class Event
    {
        //查詢我的活動
        public DataTable GetMyEvent(string name, string catid, string status, string empid)
        {
            EventData ev = new EventData();
            return ev.QueryMyEvent(name, catid, status, empid);
        }


        //Load健檢中心資料
        public DataTable GetHosipital(string eventid)
        {
            EventData ev = new EventData();
            return ev.QueryHosipitalData(eventid);
        }

        /// <summary>
        /// 
        /// 
        /// 
        /// 
        /// <summary>
        /// 
        /// 
        /// 
        /// 
        /// 
        /// 
        /// 
        /// 
        /// 
        /// 使用者可報名列表
        /// </summary>
        /// <param name="eventname"></param>
        /// <param name="eventcateid"></param>
        /// <returns></returns>
        public DataTable GetUserRegisterEventList(string eventname = "", string eventcateid = "")
        {
            EventData ev = new EventData();
            return ev.QueryUserRegisterEventList(eventname, eventcateid);
        }

        public string CreateEvent(Dictionary<string, string> eventsData, Dictionary<string, string> eventAdminData, string empid)
        {
            EventData ev = new EventData();
            return ev.InsertEvent(eventsData, eventAdminData, empid);
        }

        public DataTable GetEventInfo(string eventid)
        {
            EventData ev = new EventData();

            return ev.QueryEventInfo(eventid);
        }


        public DataTable GetEventInfo(string eventid = "", string eventname = "", string eventcateid = "", string eventSdate = "", string eventEdate = "", string status = "", string enabled = "")
        {
            EventData ev = new EventData();

            return ev.QueryEventInfo(eventid, eventname, eventcateid, eventSdate, eventEdate, status, enabled);
        }

        /// <summary>
        /// 查詢已報名活動資訊
        /// </summary>
        /// <param name="eventid"></param>
        /// <returns></returns>
        public DataTable GetRegisteredEventInfo(string eventid = "")
        {
            EventData ev = new EventData();

            return ev.QueryRegisteredEventInfo(eventid);
        }

        public string UpdateEvent(Dictionary<string, string> eventsData, Dictionary<string, string> eventAdminData, string empid)
        {
            EventData ev = new EventData();

            return ev.UpdateEvent(eventsData, eventAdminData, empid);
        }

        
        public DataTable GetEventAdmin(string eventid)
        {
            EventData ev = new EventData();

            return ev.QueryEventAdmin(eventid);
        }

        /// <summary>
        /// 新增報名表選項（欲參加的內容）
        /// </summary>
        /// <param name="content"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public string AddRegisterOption1(string eventid, string content, string limit,string modifiedby)
        {
            EventData ev = new EventData();

            return ev.InsertRegisterOption1(eventid, content, limit, modifiedby);
        }

        /// <summary>
        /// 刪除 報名表選項（欲參加內容）
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string DeleteRegisterOption1(string id)
        {
            EventData ev = new EventData();

            return ev.DeleteRegisterOption1(id);
        }

        /// <summary>
        /// 查詢報名表選項（欲參加內容）
        /// </summary>
        /// <param name="eventid"></param>
        /// <returns></returns>
        public DataTable GetRegisterOption1(string eventid)
        {
            EventData ev = new EventData();
            DataTable dt = new DataTable();
            dt = ev.QueryRegisterOption1(eventid);
            return dt;
        }

        /// <summary>
        /// 新增報名表選項（交通車）
        /// </summary>
        /// <param name="eventid"></param>
        /// <param name="transportation"></param>
        /// <param name="v"></param>
        /// <returns></returns>
        public string AddTransportation(string eventid, string transportation, string modifiedby)
        {
            EventData ev = new EventData();

            return ev.InsertRegisterOption2(eventid, transportation, modifiedby);
        }

        /// <summary>
        /// 刪除報名表選項（交通車）
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string DeleteGetTransportation(string id)
        {
            EventData ev = new EventData();

            return ev.DeleteRegisterOption2(id);
        }

        /// <summary>
        /// 查詢報名表選項（交通車）
        /// </summary>
        /// <param name="eventid"></param>
        /// <returns></returns>
        public DataTable GetTransportation(string eventid)
        {
            EventData ev = new EventData();
            DataTable dt = new DataTable();
            dt = ev.QueryRegisterOption2(eventid);
            return dt;
        }

        /// <summary>
        /// 新增報名表選項（餐點內容列表）
        /// </summary>
        /// <param name="eventid"></param>
        /// <param name="transportation"></param>
        /// <param name="v"></param>
        /// <returns></returns>
        public string AddMeal(string eventid, string meal, string modifiedby)
        {
            EventData ev = new EventData();

            return ev.InsertRegisterOption3(eventid, meal, modifiedby);
        }

        /// <summary>
        /// 刪除餐點內容列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string DeleteMeal(string id)
        {
            EventData ev = new EventData();

            return ev.DeleteRegisterOption3(id);
        }

        /// <summary>
        /// 取得餐點內容列表
        /// </summary>
        /// <param name="eventid"></param>
        /// <returns></returns>
        public DataTable GetMeal(string eventid)
        {
            EventData ev = new EventData();
            DataTable dt = new DataTable();
            dt = ev.QueryRegisterOption3(eventid);
            return dt;
        }

        /// <summary>
        /// 新增健檢方案
        /// </summary>
        /// <param name="eventid"></param>
        /// <param name="list"></param>
        /// <param name="modifiedby"></param>
        /// <returns></returns>
        public string AddUserHealthSolutions(string eventid, List<ImportModel> list, string modifiedby)
        {
            EventData ev = new EventData();

            return ev.InsertRegisterOption4(eventid, list, modifiedby);
        }

        /// <summary>
        /// 新增電腦更換地點
        /// </summary>
        /// <param name="eventid"></param>
        /// <param name="list"></param>
        /// <param name="modifiedby"></param>
        /// <returns></returns>
        public string AddComputerChange(string eventid, List<ImportModel> list, string modifiedby)
        {
            EventData ev = new EventData();

            return ev.InsertRegisterOption6(eventid, list, modifiedby);
        }

        /// <summary>
        /// 新增健檢包寄送地點
        /// </summary>
        /// <param name="eventid"></param>
        /// <param name="sendArea"></param>
        /// <param name="modifiedby"></param>
        /// <returns></returns>
        public string AddSendArea(string eventid, string sendArea, string modifiedby)
        {
            EventData ev = new EventData();

            return ev.InsertRegisterOption5(eventid, sendArea, modifiedby);
        }

        /// <summary>
        /// 刪除健檢包寄送地點
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string DeleteSendArea(string id)
        {
            EventData ev = new EventData();

            return ev.DeleteRegisterOption5(id);
        }

        /// <summary>
        /// 查詢健檢包寄送地點
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public DataTable GetSendArea(string eventid)
        {
            EventData ev = new EventData();
            DataTable dt = new DataTable();
            dt = ev.QueryRegisterOption5(eventid);
            return dt;
        }

        /// <summary>
        /// 查詢健檢方案
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public DataTable GetHealthSolutions(string eventid)
        {
            EventData ev = new EventData();
            DataTable dt = new DataTable();
            dt = ev.QueryRegisterOption4(eventid);
            return dt;
        }

        /// <summary>
        /// 取得電腦更換地點列表
        /// </summary>
        /// <param name="eventid"></param>
        /// <returns></returns>
        public DataTable GetComputerChange(string eventid)
        {
            EventData ev = new EventData();
            DataTable dt = new DataTable();
            dt = ev.QueryRegisterOption6(eventid);
            return dt;
        }

        /// <summary>
        /// 刪除活動
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string DeleteEvent(string id)
        {
            EventData ev = new EventData();

            return ev.DeleteEvent(id);
        }

        /// <summary>
        /// 更新問券發送時間
        /// </summary>
        /// <param name="eventid"></param>
        public void UpdateEventSurveyStartDate(string eventid)
        {
            EventData ev = new EventData();
            ev.UpdateEventSurveyStartDate(eventid);
        }

        //取得活動報名人數
        public String GetEvnetRegisterCount(string eventid, string registermodel)
        {
            EventData ev = new EventData();
            return ev.QueryEvnetRegisterCount(eventid, registermodel);
        }
    }

    public class ImportModel
    {
        public string hosipital { get; set; }

        public string area { get; set; }
        public string description { get; set; }
        public string gender { get; set; }
        public string secondoption1 { get; set; }
        public string secondoption2 { get; set; }
        public string secondoption3 { get; set; }
        public string avaliabledate { get; set; }
        public string limit { get; set; }

    }
}
