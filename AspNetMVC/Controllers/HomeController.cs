using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using AspNetMVC.Models;
using AspNetMVC.Repository;
using AspNetMVC.ViewModel;
using AspNetMVC.Service;

namespace AspNetMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IndexPageService _datas;

        private readonly CustomerServiceService _customerServiceService;

        public HomeController()
        {
            _datas = new IndexPageService();
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
                 return Json(new { response = "success" });
            }
            return Json(new{response="error" });
        }

        public ActionResult ShowList()
        {
            var customerData = _customerServiceService.ShowData();
            return View(customerData);
        }

        public ActionResult ShowDetail(Guid? id)
        {
            var customer = _customerServiceService.ReadContent(id,"");
            return View(customer);
        }
    }
}
