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
                var result=_productPageService.CreatePackageProductDataInFavorite(model.PackageProductId, UserName);
                if (result.Status == 0)
                {
                return Json(new { response = "success" }, JsonRequestBehavior.AllowGet);
                }
                else if(result.Status==1)
                {
                    return Json(new { response = "exist" }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { response = "error" }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SearchForFavorite()
        {
            var UserName = Helpers.DecodeCookie(Request.Cookies["user"]["user_accountname"]);
            
            var Data =_productPageService.GetFavoriteUserFavoriteData(UserName);
                
            return Json(Data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeleteFavorite(string favoriteid)
        {

            if (ModelState.IsValid)
            {
                _productPageService.DeleteFavoriteData(Guid.Parse(favoriteid));

                return Json(new { response = "success" }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { response = "error" }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ModifyUserDefinedByUserDefindId(Guid userDefinedId, UserDefinedAll userDefinedall)
        {

            if (ModelState.IsValid)
            {
                _productPageService.modifyUserDefined(userDefinedId,userDefinedall);

                return Json(new { response = "success" }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { response = "error" }, JsonRequestBehavior.AllowGet);
        }

    }
}