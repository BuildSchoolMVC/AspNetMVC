using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AspNetMVC.Models;
using AspNetMVC.Models.CustomerService;
using AspNetMVC.Repository;
using System.Data;
using System.Data.Entity;

namespace AspNetMVC.Controllers
{
    public class HomeController : Controller
    {
        private IndexPageDataRepository datas = new IndexPageDataRepository();

        private readonly CustomerServiceContext _context = new CustomerServiceContext();
        public ActionResult Index()
        {
            return View(datas);
        }

        public ActionResult AllServiceView()
        {
            var allServiceCards = new AllServiceCardRepository().CreateAllServiceCardList();
            return View(allServiceCards);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CustomerServiceCreate([Bind(Include = "Name,Email,Phone,Category")] CustomerService customerService)
        {

            if (ModelState.IsValid)
            {
                //_context.Entry(customerService).State = EntityState.Added;
                //customerService.CreatedTime = new DateTime();
                customerService.IsRead = false;

                _context.CustomerServices.Add(customerService);
                _context.SaveChanges();
                return Json(new { response = "success", customerService = customerService });
            }
            return Json(new{response="error" });
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
