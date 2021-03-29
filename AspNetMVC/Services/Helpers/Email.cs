using AspNetMVC.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace AspNetMVC.Services
{
    public class Email
    {
        public string Server_UserName { get; set; }
        public string Server_Password { get; set; }
        public string Server_SmtpClient { get; set; }
        public string Server_SmtpClientPort { get; set; }
        public string RecipientAddress { get; set; }
        public string SenderAddress { get; set; }
        public string SenderName { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }

        public enum Template
        {
            EmailActivation,
            SystemReply,
            ForgotPassword,
            SuccessResetPassword
        }

        public Email(string subject,string body,string recipientAddress)
        {
            Server_UserName = ConfigurationManager.AppSettings["GmailServer_UserName"];
            Server_Password = ConfigurationManager.AppSettings["GmailServer_Password"];
            Server_SmtpClient = ConfigurationManager.AppSettings["GmailServer_SmtpClient"];
            Server_SmtpClientPort = ConfigurationManager.AppSettings["GmailServer_SmtpClientPort"];
            SenderAddress = ConfigurationManager.AppSettings["GmailServer_UserName"] + "@gmail.com";
            SenderName = "系統管理者";
            Body = body;
            Subject = subject;
            RecipientAddress = recipientAddress;
        }

        public void SendEmailFromGmail()
        {
            var result = new OperationResult();
            try
            {
                MailMessage mailMessage = new MailMessage(); // 建立出信件
                NetworkCredential netWorkCredential = new NetworkCredential(Server_UserName, Server_Password);

                mailMessage.From = new MailAddress(SenderAddress,"uCleaner - 打掃服務"); //建立出寄件者地址
                mailMessage.To.Add(RecipientAddress); //收件者地址
                mailMessage.Subject = Subject; //信件主旨
                mailMessage.Body = Body; //信件內容
                mailMessage.IsBodyHtml = true; // 是否用HTML格式

                SmtpClient smtpClient = new SmtpClient(Server_SmtpClient)
                {
                    UseDefaultCredentials = false,  //設定Smtp伺服器不用傳送憑證
                    EnableSsl = true,
                    Credentials = netWorkCredential,
                    Port = int.Parse(Server_SmtpClientPort)
                };

                smtpClient.Send(mailMessage);
                result.IsSuccessful = true;
            }catch(Exception ex)
            {
                result.IsSuccessful = false;
                result.Exception = ex;
            }
        }

        public static string GetTemplateEmailString(Template template)
        {
            string returnString = "";
            string path = AppDomain.CurrentDomain.BaseDirectory + $@"\Assets\template\{template}Template.html"; // 取得該專案當前路徑 + 樣板檔名

            if (File.Exists(path))
            {
                StreamReader streamReader = new StreamReader(path, Encoding.GetEncoding("Big5")); 

                returnString = streamReader.ReadToEnd(); //讀取整個檔案

                streamReader.Dispose();
            }
            return returnString;
        }

        public static string ReplaceString(string strings,Dictionary<string,string> dic)
        {
            string result = strings;

            foreach(var item in dic)
            {
                result = Regex.Replace(result, $"#{item.Key}#", item.Value);
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="title">信件主旨</param>
        /// <param name="email">寄送信箱</param>
        /// <param name="template">使用到的模板</param>
        /// <param name="kvp">模板內文替代文字</param>
        public static void SendMail(string title, string email, Email.Template template, Dictionary<string, string> kvp)
        {
            var body = ReplaceString(GetTemplateEmailString(template), kvp);

            Email objEmail = new Email(title, body, email);

            objEmail.SendEmailFromGmail();
        }
    }
}