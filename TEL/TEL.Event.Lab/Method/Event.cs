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
        /// 使用者可報名列表
        /// </summary>
        /// <param name="eventname"></param>
        /// <param name="eventcateid"></param>
        /// <returns></returns>
        public DataTable GetUserRegisterEventList(string empid, string eventname = "", string eventcateid = "")
        {
            EventData ev = new EventData();
            MailGroup mg = new MailGroup();
            DataTable dtUserMailGroup = new DataTable();
            dtUserMailGroup = mg.GetUserMailGroup(empid);
            return ev.QueryUserRegisterEventList(dtUserMailGroup, empid, eventname, eventcateid);
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


        public DataTable GetEventInfo(string eventid = "", string eventname = "", string eventcateid = "", string eventSdate = "", string eventEdate = "", string status = "", string enabled = "", int isManager = 0, string empid = "")
        {
            EventData ev = new EventData();

            return ev.QueryEventInfo(eventid, eventname, eventcateid, eventSdate, eventEdate, status, enabled, isManager, empid);
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
        /// 查詢報名表選項（欲參加內容）限制人數
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int GetRegisterOption1Limit(string eventid, string description)
        {
            EventData ev = new EventData();
            return ev.QueryRegisterOption1Limit(eventid, description);
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

        /// <summary>
        /// 取得活動報名人數 by empid
        /// </summary>
        /// <param name="eventid"></param>
        /// <param name="registermodel"></param>
        /// <param name="empid"></param>
        /// <returns></returns>
        public DataTable GetUserEvnetRegister(string eventid, string registermodel, string empid)
        {
            EventData ev = new EventData();
            return ev.GetUserEvnetRegister(eventid, registermodel, empid);
        }

        /// <summary>
        /// 取得活動權限 by MailGroup
        /// </summary>
        /// <param name="mailgroup"></param>
        /// <returns></returns>
        public DataTable GetEventPermissionMailGroup(string mailgroup = "")
        {
            EventData ev = new EventData();
            return ev.QueryEventPermissionMailGroup(mailgroup);
        }

        #region RegisterModel1

        //取得活動選項報名人數
        public int GetEvnetRegisterOption1RegisterCount(string eventid, string optionid)
        {
            EventData ev = new EventData();
            return ev.QueryEvnetRegisterOption1RegisterCount(eventid, optionid);
        }

        /// <summary>
        /// 新增 模板1報名資料
        /// </summary>
        /// <param name="eventsData"></param>
        /// <param name="modifiedby"></param>
        public string AddRegisterModel1(Dictionary<string, string> eventsData, string modifiedby)
        {
            EventData ev = new EventData();

            return ev.InsertRegisterModel1(eventsData, modifiedby);
        }

        // <summary>
        /// 更新 模板1報名資料
        /// </summary>
        /// <param name="eventsData"></param>
        /// <param name="modifiedby"></param>
        /// <returns></returns>
        public string UpdateRegisterModel1(Dictionary<string, string> eventsData, string modifiedby)
        {
            EventData ev = new EventData();

            return ev.UpdateRegisterModel1(eventsData, modifiedby);
        }

        /// <summary>
        /// 刪除 模板1報名資料
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string DeleteRegisterModel1(string id)
        {
            EventData ev = new EventData();

            return ev.DeleteRegisterModel1(id);
        }

        /// <summary>
        /// 取得 模板1報名資料
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataTable GetRegisterModel1(string id)
        {
            EventData ev = new EventData();
            DataTable dt = new DataTable();
            dt = ev.QueryRegisterModel1(id);
            return dt;
        }
        #endregion

        #region RegisterModel2
        /// <summary>
        /// 新增 模板2報名資料
        /// </summary>
        /// <param name="eventsData"></param>
        /// <param name="dataTable"></param>
        /// <param name="modifiedby"></param>
        /// <returns></returns>
        public string AddRegisterModel2(Dictionary<string, string> eventsData, DataTable dataTable, string modifiedby)
        {
            EventData ev = new EventData();

            return ev.InsertRegisterModel2(eventsData, dataTable, modifiedby);
        }

        /// <summary>
        /// 更新 模板2報名資料
        /// </summary>
        /// <param name="eventsData"></param>
        /// <param name="dataTable"></param>
        /// <param name="modifiedby"></param>
        /// <returns></returns>
        public string UpdateRegisterModel2(Dictionary<string, string> eventsData, DataTable dataTable, string modifiedby)
        {
            EventData ev = new EventData();

            return ev.UpdateRegisterModel2(eventsData, dataTable, modifiedby);
        }

        /// <summary>
        /// 刪除 模板2報名資料
        /// </summary>
        /// <param name="eventsData"></param>
        /// <param name="dataTable"></param>
        /// <param name="modifiedby"></param>
        /// <returns></returns>
        public string DeleteRegisterModel2(string id)
        {
            EventData ev = new EventData();
            return ev.DeleteRegisterModel2(id);
        }

        /// <summary>
        /// 查詢 模板2報名資料
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataTable GetRegisterModel2(string id)
        {
            EventData ev = new EventData();
            DataTable dt = new DataTable();
            dt = ev.QueryRegisterModel2(id);
            return dt;
        }

        //<summary>
        /// 查詢 模板2報名資料family
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataTable GetRegisterModel2family(string id)
        {
            EventData ev = new EventData();
            DataTable dt = new DataTable();
            dt = ev.QueryRegisterModel2family(id);
            return dt;
        }
        #endregion

        #region RegisterModel3
        public DataTable GetHosipitalOption(string eventid)
        {
            EventData ev = new EventData();
            DataTable dt = new DataTable();
            dt = ev.QueryHosipitalOption(eventid);
            return dt;
        }

        public DataTable GetAreaOption(string eventid, string hosipital)
        {
            EventData ev = new EventData();
            DataTable dt = new DataTable();
            dt = ev.QueryAreaOption(eventid, hosipital);
            return dt;
        }

        public DataTable GetSolutionOption(string eventid, string hosipital, string area)
        {
            EventData ev = new EventData();
            DataTable dt = new DataTable();
            dt = ev.QuerySolutionOption(eventid, hosipital, area);
            return dt;
        }
        public DataTable GetGenderOption(string eventid, string hosipital, string area, string solution)
        {
            EventData ev = new EventData();
            DataTable dt = new DataTable();
            dt = ev.QueryGenderOption(eventid, hosipital, area, solution);
            return dt;
        }

        public DataTable GetExpectdateOption(string eventid, string hosipital, string area, string solution, string gender)
        {
            EventData ev = new EventData();
            DataTable dt = new DataTable();
            dt = ev.QueryExpectdateOption(eventid, hosipital, area, solution, gender);
            return dt;
        }

        public DataTable GetSecondoption1Option(string eventid, string hosipital, string area, string solution, string gender)
        {
            EventData ev = new EventData();
            DataTable dt = new DataTable();
            dt = ev.QuerySecondoption1Option(eventid, hosipital, area, solution, gender);
            return dt;
        }

        public DataTable GetSecondoption2Option(string eventid, string hosipital, string area, string solution, string gender)
        {
            EventData ev = new EventData();
            DataTable dt = new DataTable();
            dt = ev.QuerySecondoption2Option(eventid, hosipital, area, solution, gender);
            return dt;
        }

        public DataTable GetSecondoption3Option(string eventid, string hosipital, string area, string solution, string gender)
        {
            EventData ev = new EventData();
            DataTable dt = new DataTable();
            dt = ev.QuerySecondoption3Option(eventid, hosipital, area, solution, gender);
            return dt;
        }

        public DataTable GetHealthAddressOption(string eventid)
        {
            EventData ev = new EventData();
            DataTable dt = new DataTable();
            dt = ev.QueryHealthAddressOption(eventid);
            return dt;
        }

        public int GetRegisterOption4Limit(string eventid, string hosipital, string area, string solution, string gender, string expectdate)
        {
            EventData ev = new EventData();
            return ev.QueryRegisterOption4Limit(eventid, hosipital, area, solution, gender, expectdate);
        }

        public string AddRegisterModel3(Dictionary<string, string> data, string modifiedby)
        {
            EventData ev = new EventData();

            return ev.InsertRegisterModel3(data, modifiedby);
        }

        /// <summary>
        /// 更新 模板3報名資料
        /// </summary>
        /// <param name="data"></param>
        /// <param name="modifiedby"></param>
        /// <returns></returns>
        public string UpdateRegisterModel3(Dictionary<string, string> data, string modifiedby)
        {
            EventData ev = new EventData();

            return ev.UpdateRegisterModel3(data, modifiedby);
        }

        /// <summary>
        /// 刪除 模板3報名資料
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string DeleteRegisterModel3(string id)
        {
            EventData ev = new EventData();

            return ev.DeleteRegisterModel3(id);
        }

        /// <summary>
        /// 查詢 模板3報名資料
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataTable GetRegisterModel3(string id)
        {
            EventData ev = new EventData();
            DataTable dt = new DataTable();
            dt = ev.QueryRegisterModel3(id);
            return dt;
        }
        #endregion

        #region RegisterModel4
        /// <summary>
        /// 新增 模板4報名資料
        /// </summary>
        /// <param name="eventsData"></param>
        /// <param name="dataTable"></param>
        /// <param name="modifiedby"></param>
        /// <returns></returns>
        public string AddRegisterModel4(Dictionary<string, string> data, string modifiedby)
        {
            EventData ev = new EventData();

            return ev.InsertRegisterModel4(data, modifiedby);
        }

        /// <summary>
        /// 更新 模板4報名資料
        /// </summary>
        /// <param name="data"></param>
        /// <param name="modifiedby"></param>
        /// <returns></returns>
        public string UpdateRegisterModel4(Dictionary<string, string> data, string modifiedby)
        {
            EventData ev = new EventData();

            return ev.UpdateRegisterModel4(data, modifiedby);
        }

        /// <summary>
        /// 刪除 模板4報名資料
        /// </summary>
        /// <param name="data"></param>
        /// <param name="modifiedby"></param>
        /// <returns></returns>
        public string DeleteRegisterModel4(string id)
        {
            EventData ev = new EventData();

            return ev.DeleteRegisterModel4(id);
        }

        /// <summary>
        /// 查詢 模板4報名資料
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataTable GetRegisterModel4(string id)
        {
            EventData ev = new EventData();
            DataTable dt = new DataTable();
            dt = ev.QueryRegisterModel4(id);
            return dt;
        }
        #endregion

        #region RegisterModel5
        /// <summary>
        /// 新增 模板5報名資料
        /// </summary>
        /// <param name="data"></param>
        /// <param name="modifiedby"></param>
        /// <returns></returns>
        public string AddRegisterModel5(Dictionary<string, string> data, string modifiedby)
        {
            EventData ev = new EventData();

            return ev.InsertRegisterModel5(data, modifiedby);
        }

        /// <summary>
        /// 更新 模板5報名資料
        /// </summary>
        /// <param name="data"></param>
        /// <param name="modifiedby"></param>
        /// <returns></returns>
        public string UpdateRegisterModel5(Dictionary<string, string> data, string modifiedby)
        {
            EventData ev = new EventData();

            return ev.UpdateRegisterModel5(data, modifiedby);
        }

        /// <summary>
        /// 刪除 模板5報名資料
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string DeleteRegisterModel5(string id)
        {
            EventData ev = new EventData();

            return ev.DeleteRegisterModel5(id);
        }

        /// <summary>
        /// 查詢 模板5報名資料
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataTable GetRegisterModel5(string id)
        {
            EventData ev = new EventData();
            DataTable dt = new DataTable();
            dt = ev.QueryRegisterModel5(id);
            return dt;
        }
        #endregion

        #region RegisterModel6

        /// <summary>
        /// 查詢地點選項
        /// </summary>
        /// <param name="eventid"></param>
        /// <returns></returns>
        public DataTable GetAreaOption6(string eventid)
        {
            EventData ev = new EventData();
            DataTable dt = new DataTable();
            dt = ev.QueryAreaOption6(eventid);
            return dt;
        }

        /// <summary>
        /// 查詢日期選項
        /// </summary>
        /// <param name="eventid"></param>
        /// <returns></returns>
        public DataTable GetAvaliableDatOption(string eventid, string area)
        {
            EventData ev = new EventData();
            DataTable dt = new DataTable();
            dt = ev.QueryAvaliableDatOption(eventid, area);
            return dt;
        }

        /// <summary>
        /// 新增 模板6報名資料
        /// </summary>
        /// <param name="data"></param>
        /// <param name="modifiedby"></param>
        /// <returns></returns>
        public string AddRegisterModel6(Dictionary<string, string> data, string modifiedby)
        {
            EventData ev = new EventData();

            return ev.InsertRegisterModel6(data, modifiedby);
        }

        /// <summary>
        /// 更新 模板6報名資料
        /// </summary>
        /// <param name="data"></param>
        /// <param name="modifiedby"></param>
        /// <returns></returns>
        public string UpdateRegisterModel6(Dictionary<string, string> data, string modifiedby)
        {
            EventData ev = new EventData();

            return ev.UpdateRegisterModel6(data, modifiedby);
        }

        /// <summary>
        /// 刪除 模板6報名資料
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string DeleteRegisterModel6(string id)
        {
            EventData ev = new EventData();

            return ev.DeleteRegisterModel6(id);
        }

        /// <summary>
        /// 查詢 模板6報名資料
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataTable GetRegisterModel6(string id)
        {
            EventData ev = new EventData();
            DataTable dt = new DataTable();
            dt = ev.QueryRegisterModel6(id);
            return dt;
        }
        #endregion

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
