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
        //[HttpGet]
        //public ActionResult CreateUserDefinedData()
        //{
            
        //    return View();
        //}

        [HttpPost]
        public JsonResult CreateUserDefinedData(UserDefinedAllViewModel model)
        {
            var UserName = Helpers.DecodeCookie(Request.Cookies["user"]["user_accountname"]);
            var TempGuid = Guid.NewGuid();

            if (ModelState.IsValid)
            {
                _productPageService.CreateUserDefinedDataInFavorite(model.UserDefinedAlls, UserName, TempGuid);
                
                return Json(new { response = "success" }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { response = "error" }, JsonRequestBehavior.AllowGet);

        }


        [HttpPost]
        public JsonResult CreateFavoriteData([Bind(Include = "PackageProductId")] ProductPageViewModel model)
        {
            
            var UserName = Helpers.DecodeCookie(Request.Cookies["user"]["user_accountname"]);

            if (ModelState.IsValid)
            {
                _productPageService.CreatePackageProductDataInFavorite(model.PackageProductId, UserName);
                return Json(new { response = "success" }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { response = "error" }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SearchForFavorite()
        {
            var UserName = Helpers.DecodeCookie(Request.Cookies["user"]["user_accountname"]);
            
            var Data =_productPageService.GetFavoriteUserFavoriteData(UserName);
                
            return Json(Data, JsonRequestBehavior.AllowGet);
        }


    }
}