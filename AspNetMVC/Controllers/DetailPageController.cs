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
        public ActionResult Index(int id)
        {
            var result = _detailPageService.GetSingleProduct(id);

            if (result.Name != "") 
            { 
                return View(result);
            }
            else 
            {
                return View("Error");
            }

        }

        //public ActionResult Create() {
        //    _detailPageService.CreateComment("jacko1114", 1, 4,"服務非常周到!!!");
        //    _detailPageService.CreateComment("jacko1114", 2, 5, "不管相信這是我的家!!!");
        //    _detailPageService.CreateComment("jacko1114", 4, 5, "準備預約下一次!!!");
        //    _detailPageService.CreateComment("jacko1114", 6, 3, "還行。");
        //    _detailPageService.CreateComment("jacko1114", 8, 2, "普通!!!");
        //    _detailPageService.CreateComment("jacko1114", 5, 4, "讚!!!");
        //    return null;
        //}
    }
}