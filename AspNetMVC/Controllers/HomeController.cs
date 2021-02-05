using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using AspNetMVC.Models;
using AspNetMVC.Models.CustomerService;
using AspNetMVC.Repository;
using AspNetMVC.ViewModel;
using AspNetMVC.Service;

namespace AspNetMVC.Controllers
{
    public class HomeController : Controller
    {
        private IndexPageService _datas = new IndexPageService();

        private readonly CustomerServiceService _customerServiceService;

        public HomeController()
        {
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

        [HttpPost]
        public ActionResult CustomerServiceCreate([Bind(Include = "Name,Email,Phone,Category,Content")] CustomerViewModel customerService)
        {
            if (ModelState.IsValid)
            {
                _customerServiceService.AddData(customerService);
                 return Json(new { response = "success" });
            }
            return Json(new{response="error" });
        }

        public ActionResult ShowList()
        {
            var customerData = _customerServiceService.ShowData();
            return View(customerData);
        }

        public ActionResult ShowDetail(int? id)
        {
            var customer = _customerServiceService.ShowCustomerInfo(id);
            return View(customer);
        }
    }
}
