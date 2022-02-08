using System;
using System.Collections.Generic;
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
    }
}
