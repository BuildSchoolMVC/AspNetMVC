using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using AspNetMVC.Services;
using AspNetMVC.ViewModels;

namespace AspNetMVC.Controllers
{
    public class DetailController : Controller
    {
        private readonly DetailService _detailService;
        public DetailController()
        {
            _detailService = new DetailService();
        }

        // GET: Detail
        public ActionResult Index(int? id)
        {
            var result = _detailService.GetPackageProduct(id);

            if(result != null)
            {
                ViewBag.Comments = _detailService.GetComment(id);
                return View(result);
            }
            else
            {
                return View("Error");
            }


        }

        [HttpPost]
        [AllowAnonymous]
        public JsonResult AddComment(int PackageProductId,int StarCount,string Comment)
        {
            var result = _detailService.CreateComment(Helpers.DecodeCookie(Request.Cookies["user"]["user_accountname"]),PackageProductId,StarCount,Comment);

            return Json(new { response = result.MessageInfo, status = result.IsSuccessful });
        }

        public JsonResult GetLatestComment(int packageProductId)
        {
            var result = _detailService.GetComment(packageProductId).OrderByDescending(x => x.CreateTime).First();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeleteComment(Guid? id)
        {
            var accountName = Helpers.DecodeCookie(Request.Cookies["user"]["user_accountname"]);
            var result = _detailService.DeleteComment(id, accountName);

            return Json(new { status = result.IsSuccessful, response = result.MessageInfo });
        }

        public RedirectResult DirectToCheckout(int id)
        {
            var usernameCookie = Request.Cookies["user"]["user_accountname"];
            var result = _detailService.AddFavoriteAndDirectToCheckout(id, usernameCookie);
            return new RedirectResult($"/Checkout?id={result.MessageInfo}");
        }
    }
}