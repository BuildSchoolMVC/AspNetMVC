using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AspNetMVC.Services;
using AspNetMVC.ViewModels;
using AspNetMVC.Models.Entity;
using Newtonsoft.Json; 

namespace AspNetMVC.Controllers
{
    public class ProductPageController : Controller
    {
        private readonly ProductPageService _productPageService;
        private readonly AccountService _accountService;
        private readonly Helpers _helpers;

        public ProductPageController()
        {
            _productPageService = new ProductPageService();
            _accountService = new AccountService();
            _helpers = new Helpers();
        }
        // GET: ProductPage
        public ActionResult Index()
        {
            var result = _productPageService.GetData();
            return View(result);
        }
        [HttpGet]
        public ActionResult CreatePackage()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreatePackage(UserDefinedAllViewModel model)
        {
            var UserName = Helpers.DecodeCookie(Request.Cookies["user"]["user_accountname"]);
            var tempName= _accountService.GetAccountId(UserName);


            if (ModelState.IsValid)
            {
                foreach (var i in model.UserDefinedAlls)
                {
                    _productPageService.CreateUserDefinedPackageData(i,tempName, UserName);
                }
                
            }

            return View();
        }


    }
}