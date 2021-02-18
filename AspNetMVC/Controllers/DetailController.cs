using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AspNetMVC.Services;

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

            if (result != null) 
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
        public ActionResult AddComment(int PackageProductId,int StarCount,string Comment) {
            _detailService.CreateComment(Helpers.DecodeCookie(Request.Cookies["user"]["user_accountname"]), PackageProductId, StarCount, Comment);

            return Json(new { response = "成功新增評論!"});
        }

        public ActionResult GetLatestComment(int packageProductId)
        {
            var result = _detailService.GetComment(packageProductId).OrderByDescending(x=>x.CreateTime).First();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeleteComment(Guid? id)
        {
            var accountName = Helpers.DecodeCookie(Request.Cookies["user"]["user_accountname"]);
            var result = _detailService.DeleteComment(id, accountName);

            return Json(new { response = result.Status });
        }

    }
}