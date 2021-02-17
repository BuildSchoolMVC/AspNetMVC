using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AspNetMVC.Services;

namespace AspNetMVC.Controllers
{
    public class DetailPageController : Controller
    {
        private readonly DetailPageService _detailPageService;
        public DetailPageController()
        {
            _detailPageService = new DetailPageService();
        }

        // GET: DetailPage
        public ActionResult Index(int? id)
        {
            var result = _detailPageService.GetSingleProduct(id);

            if (result != null) 
            {
                    ViewBag.Comments = _detailPageService.GetComment(id);
                return View(result);
            }
            else 
            {
                return View("Error");
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult AddComment(int PackageProductId,int StarCount,string Comment) {
            _detailPageService.CreateComment(Helpers.DecodeCookie(Request.Cookies["user"]["user_accountname"]), PackageProductId, StarCount, Comment);

            return Json(new { response = "成功新增評論!"});
        }

        public ActionResult GetLatestComment(int packageProductId)
        {
            var result = _detailPageService.GetComment(packageProductId).OrderByDescending(x=>x.CreateTime).First();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}