using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using TEL.Event.Lab.Data;

namespace TEL.Event.Lab.Method
{
    public class SystemSetup
    {
        #region 活動分類
        /// <summary>
        /// 新增活動類別
        /// </summary>
        /// <param name="name"></param>
        /// <param name="color"></param>
        /// <param name="enabled"></param>
        /// <param name="empid"></param>
        /// <returns></returns>
        public string AddEventCategory(string name, string color, string enabled, string empid)
        {
            SystemSetupData systemSetupData = new SystemSetupData();

            string result = systemSetupData.InsertEventCategory(name, color, enabled, empid);

            return result;
        }

        /// <summary>
        /// 儲存活動類別
        /// </summary>
        /// <param name="id"></param>
        /// <param name="color"></param>
        /// <param name="enabled"></param>
        /// <param name="empid"></param>
        /// <returns></returns>
        public string SaveEventCategory(string id, string color, string enabled, string empid)
        {
            SystemSetupData systemSetupData = new SystemSetupData();

            string result = systemSetupData.UpdateEventCategory(id, color, enabled, empid);

            return result;
        }

        /// <summary>
        /// 刪除活動類別
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string DeleteEventCategory(string id)
        {
            SystemSetupData systemSetupData = new SystemSetupData();

            string result = systemSetupData.DeleteEventCategory(id);

            return result;
        }

        /// <summary>
        /// 耶得活動類別
        /// </summary>
        /// <returns></returns>
        public DataTable GetEventCategory(string name)
        {
            SystemSetupData systemSetupData = new SystemSetupData();
            return systemSetupData.QueryEventCategory(name);
        }
        #endregion

        #region 常態活動管理者
        /// <summary>
        /// 新增常態活動管理者
        /// </summary>
        /// <param name="empid"></param>
        /// <param name="modifiedby"></param>
        /// <returns></returns>
        public string AddEventAdmin(string empid, string modifiedby)
        {
            SystemSetupData systemSetupData = new SystemSetupData();

            string result = systemSetupData.InsertEventAdmin(empid, modifiedby);

            return result;
        }

        /// <summary>
        /// 刪除常態活動管理者
        /// </summary>
        /// <param name="empID"></param>
        /// <returns></returns>
        public string DeleteEventAdmin(string empID)
        {
            SystemSetupData systemSetupData = new SystemSetupData();

            string result = systemSetupData.DeleteEventAdmin(empID);

            return result;
        }

        /// <summary>
        /// 取得常態活動管理者
        /// </summary>
        /// <param name="empid"></param>
        /// <returns></returns>
        public DataTable GetEventManager(string empid)
        {
            SystemSetupData systemSetupData = new SystemSetupData();
            return systemSetupData.QueryEventManager(empid);
        }
        #endregion


        #region 郵件群組
        /// <summary>
        /// 新增郵件群組
        /// </summary>
        /// <param name="name"></param>
        /// <param name="enabled"></param>
        /// <param name="modifiedby"></param>
        /// <returns></returns>
        public string AddMailGroup(string name, string enabled, string modifiedby)
        {
            SystemSetupData systemSetupData = new SystemSetupData();

            string result = systemSetupData.InsertMailGroup(name, enabled, modifiedby);

            return result;
        }

        /// <summary>
        /// 儲存郵件群組
        /// </summary>
        /// <param name="id"></param>
        /// <param name="enabled"></param>
        /// <param name="modifiedby"></param>
        /// <returns></returns>
        public string SaveMailGroup(string id, string enabled, string modifiedby)
        {
            SystemSetupData systemSetupData = new SystemSetupData();

            string result = systemSetupData.UpdateMailGroup(id, enabled, modifiedby);

            return result;
        }

        /// <summary>
        /// 刪除郵件群組
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string DeleteMailGroup(string id)
        {
            SystemSetupData systemSetupData = new SystemSetupData();

            string result = systemSetupData.DeleteMailGroup(id);

            return result;
        }

        /// <summary>
        /// 取得郵件群組
        /// </summary>
        /// <returns></returns>
        public DataTable GetMailGroup(string name)
        {
            SystemSetupData systemSetupData = new SystemSetupData();
            return systemSetupData.QueryMailGroup(name);
        }
        #endregion

        #region 員工健檢

        #endregion

    }
}
