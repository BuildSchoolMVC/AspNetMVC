using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using AspNetMVC.Models;
using AspNetMVC.Repository;
using AspNetMVC.ViewModels;
using AspNetMVC.Services;
using System.Configuration;

namespace AspNetMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IndexService _datas;

        private readonly CustomerServiceService _customerServiceService;

        public HomeController()
        {
            _datas = new IndexService();
            _customerServiceService = new CustomerServiceService();
        }
        public ActionResult Index()
        {
            return View(_datas);
        }

        public ActionResult AllServiceView()
        {
            var allServiceCards = new AllServiceCardRepository().CreateAllServiceCardList();
            return View(allServiceCards);
        }

        /// <summary>
        /// 負責收集前端送來的資料
        /// </summary>
        /// <param name="customerService"></param>
        /// <returns></returns>
        [HttpPost] 
        public ActionResult CustomerServiceCreate([Bind(Include = "Name,Email,Phone,Category,Content")] CustomerViewModel customerService)
        {
            if (ModelState.IsValid)
            {
                _customerServiceService.CreateData(customerService);

                string category = customerService.Category == 1 ? "儲值問題" : customerService.Category == 2 ? "諮詢問題" : "客訴問題";

                Dictionary<string, string> kvp = new Dictionary<string, string>
                {
                    { "name", customerService.Name },
                    { "phone", customerService.Phone},
                    { "datetime", DateTime.Now.ToString()},
                    { "category", category},
                    { "content", customerService.Content}
                };

                Email objEmail = new Email
                {
                    Server_UserName = ConfigurationManager.AppSettings["GmailServer_UserName"],
                    Server_Password = ConfigurationManager.AppSettings["GmailServer_Password"],
                    Server_SmtpClient = ConfigurationManager.AppSettings["GmailServer_SmtpClient"],
                    Server_SmtpClientPort = ConfigurationManager.AppSettings["GmailServer_SmtpClientPort"],
                    RecipientAddress = customerService.Email,
                    SenderName = "系統管理者",
                    SenderAddress = ConfigurationManager.AppSettings["GmailServer_UserName"] + "@gmail.com",
                    Subject = $"[{category}] - 此信件由系統自動發送，請勿直接回覆 from [Gmail]"
                };

                objEmail.Body = objEmail.ReplaceString(objEmail.GetEmailString(Email.Template.SystemReply), kvp); // 取得回覆HTML模板，並且指定字串插入模板裡，最後指派給此信的內容

                objEmail.SendEmailFromGmail();

                 return Json(new { response = "success" });
            }
            return Json(new{response="error" });
        }

        public ActionResult ShowList()
        {
            var customerData = _customerServiceService.ShowAllData();
            return View(customerData);
        }

        public ActionResult ShowDetail(Guid? id)
        {
            var customer = _customerServiceService.ShowSingleData(id,"");
            return View(customer);
        }
    }
}
