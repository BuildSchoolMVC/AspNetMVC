using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AspNetMVC.Models;
using AspNetMVC.Models.CustomerService;
using AspNetMVC.Repository;

namespace AspNetMVC.Controllers
{
    public class HomeController : Controller
    {
        private CustomerServiceContext context = new CustomerServiceContext();
        public ActionResult Index()
        {
            var imageCards = new ImageCardRepository().CreateImageCardList();
            return View(imageCards);
        }

        public ActionResult AllServiceView()
        {
            var allServiceCards = new AllServiceCardRepository().CreateAllServiceCardList();
            return View(allServiceCards);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind] CustomerService customerService)
        {

            if (ModelState.IsValid)
            {
                context.CustomerServices.Add(customerService);
                context.SaveChanges();
            }
            return View();
        }
    }
}