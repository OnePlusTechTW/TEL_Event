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
    //使用者細部資訊object
    public class UserInfo
    {
        public string UserID = "";
        public string EmpID = "";
        public string FirstNameEN = "";
        public string LastNameEN = "";
        public string FirstNameCH = "";
        public string LastNameCH = "";
        public string FullNameEN = "";
        public string FullNameCH = "";
        public string Gender = "";
        public string NationalID = "";
        public string PassportID = "";
        public string Birthday = "";
        public string Address = "";
        public string UnitCode = "";
        public string UnitName = "";
        public string LeaderID = "";
        public string TypeID = "";
        public string TypeName = "";
        public string EMail = "";
        public string MobileNumber = "";
        public string Level = "";
        public string Band = "";
        public string Title = "";
        public string Station = "";
        public string Mark = "";
        public string Language = "";
        public string AccountType = "";
        public string ArrivalDate = "";
        public string HealthGroup = "";


        public UserInfo(string empID)
        {
            UserData userinfo = new UserData();
            DataTable WMTB = userinfo.QueryUserInfoTable(empID);

            if (WMTB.Rows.Count > 0)
            {
                UserID = WMTB.Rows[0]["UserID"].ToString();
                EmpID = WMTB.Rows[0]["EmpID"].ToString();
                FirstNameEN = WMTB.Rows[0]["FirstNameEN"].ToString();
                LastNameEN = WMTB.Rows[0]["LastNameEN"].ToString();
                FirstNameCH = WMTB.Rows[0]["FirstNameCH"].ToString();
                LastNameCH = WMTB.Rows[0]["LastNameCH"].ToString();
                FullNameEN= WMTB.Rows[0]["FirstNameEN"].ToString() + " " + WMTB.Rows[0]["LastNameEN"].ToString();
                FullNameCH = WMTB.Rows[0]["LastNameCH"].ToString() + WMTB.Rows[0]["FirstNameCH"].ToString();
                Gender = WMTB.Rows[0]["Gender"].ToString();
                NationalID = WMTB.Rows[0]["NationalID"].ToString();
                PassportID = WMTB.Rows[0]["PassportID"].ToString();
                Address = WMTB.Rows[0]["Address"].ToString();
                UnitCode = WMTB.Rows[0]["UnitCode"].ToString();
                UnitName = WMTB.Rows[0]["UnitName"].ToString();
                LeaderID = WMTB.Rows[0]["LeaderID"].ToString();
                TypeID = WMTB.Rows[0]["TypeID"].ToString();
                TypeName = WMTB.Rows[0]["TypeName"].ToString();
                EMail = WMTB.Rows[0]["EMail"].ToString();
                MobileNumber = WMTB.Rows[0]["MobileNumber"].ToString();
                Level = WMTB.Rows[0]["Level"].ToString();
                Band = WMTB.Rows[0]["Band"].ToString();
                Title = WMTB.Rows[0]["Title"].ToString();
                Station = WMTB.Rows[0]["Station"].ToString();
                Mark = WMTB.Rows[0]["Mark"].ToString();
                Language = WMTB.Rows[0]["Language"].ToString();
                AccountType = WMTB.Rows[0]["Language"].ToString();
                HealthGroup = WMTB.Rows[0]["groupname"].ToString();

                //生日
                if (!Convert.IsDBNull(WMTB.Rows[0]["Birthday"]))
                {
                    DateTime tempBirthday;
                    if (DateTime.TryParse(WMTB.Rows[0]["Birthday"].ToString(), out tempBirthday))
                    {
                        Birthday = tempBirthday.ToString("yyyy/MM/dd");
                    }
                }
                //到職日
                if (!Convert.IsDBNull(WMTB.Rows[0]["ArrivalDate"]))
                {
                    DateTime tempArrivalDate;
                    if (DateTime.TryParse(WMTB.Rows[0]["ArrivalDate"].ToString(), out tempArrivalDate))
                    {
                        ArrivalDate = tempArrivalDate.ToString("yyyy/MM/dd");
                    }
                }
            }
        }
    }
}
