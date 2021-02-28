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

                Email.SendMail("密碼重置 - 此信件由系統自動發送，請勿直接回覆 from [Gmail]", customerService.Email, Email.Template.ForgotPassword, kvp);

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
            var user = Helpers.DecodeCookie(Request.Cookies["user"]["user_accountname"]);
            var customer = _customerServiceService.ShowSingleData(id, user);
            return View(customer);
        }
    }
}
