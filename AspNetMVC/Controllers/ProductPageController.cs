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

        public ProductPageController()
        {
            _productPageService = new ProductPageService();
            _accountService = new AccountService();
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

        //[HttpPost]
        //public ActionResult CreatePackage( UserDefinedAllViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        foreach (var i in model.UserDefinedAlls)
        //        {
        //            var result = _productPageService.CreateUserDefinedPackageData(i);
        //        }
        //    }
        //    else
        //    {

        //    }
        //    return View();
        //}


    }
}