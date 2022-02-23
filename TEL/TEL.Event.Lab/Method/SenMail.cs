using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace TEL.Event.Lab.Method
{
    public class SenMail
    {
        private static string mailAddress = "FiestaSystem@tel.com";
        private static string account = "FiestaSystem";
        private static string password = "";

        public string message;
        public string title;
        public string mailTo;

        public static bool Send(string subject, string body, string mailObj)
        {
            //string body = string.Empty;
            //var assembly = Assembly.GetExecutingAssembly();
            //var resourceName = "SendMailByExcel.Resources.MailLayout.html";
            //using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            //using (StreamReader reader = new StreamReader(stream))
            //{
            //    body = reader.ReadToEnd();
            //}

            string title2 = subject;

            try
            {

                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.tel.co.jp");

                mail.From = new MailAddress(mailAddress, "Fiesta System");
                mail.To.Add(mailObj);
                mail.Subject = title2;
                mail.IsBodyHtml = true;

                mail.Body = body;

                SmtpServer.Port = 25;
                SmtpServer.Credentials = new System.Net.NetworkCredential(account, password);
                SmtpServer.EnableSsl = false;
                SmtpServer.Timeout = 30000;
                SmtpServer.Send(mail);
                return true;
            }
            catch (Exception ex)
            {
                string errMsg = ex.Message;
                return false;
            }
        }

        public static bool SendMail(string strSender, DataTable dtRecipient, string strSubject, string strBody, string strDisplay)
        {
            MailMessage objMessage = new MailMessage();
            objMessage.IsBodyHtml = true;
            objMessage.BodyEncoding = System.Text.Encoding.UTF8;
            objMessage.SubjectEncoding = System.Text.Encoding.UTF8;
            objMessage.From = new MailAddress(strSender, strDisplay);
            objMessage.Subject = strSubject;
            objMessage.Body = strBody;

            foreach (DataRow dr in dtRecipient.Rows)
            {
                MailGroup mg = new MailGroup();
                DataTable dtUserMailGroup = mg.GetUserMailGroup(dr["empid"].ToString());
                string address = dtUserMailGroup.Rows[0]["Address"].ToString();
                objMessage.To.Add(new MailAddress(address));
            }

            int tryMax = 3;       // 嘗試次數
            int sleepTime = 5000; // 間隔時間(毫秒)
            int tryCnt = 0;
            while (tryCnt < tryMax)
            {
                try
                {
                    SmtpClient strServer = new SmtpClient("smtp.tel.co.jp");
                    strServer.Send(objMessage);
                    return true;
                }
                catch (Exception ex)
                {
                    // 間隔時間嘗試重送一次
                    System.Threading.Thread.Sleep(sleepTime);
                    tryCnt += 1;
                }
            }

            return false;
        }

    }
}
