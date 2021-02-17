using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AspNetMVC.Services;
using AspNetMVC.ViewModels;

namespace AspNetMVC.Controllers
{
    public class ProductPageController : Controller
    {
        private readonly ProductPageService _productPageService;
        public ProductPageController()
        {
            _productPageService = new ProductPageService();
        }
        // GET: ProductPage
        public ActionResult Index()
        {
            var result = _productPageService.CreateData();
            return View(result);
        }

        [HttpPost]
        public ActionResult CreatePackage([Bind(Include= "UserDefinedId,MemberId,Name,RoomType,ServiceItem,Squarefeet,Hour")] UserDefinedAllViewModel userDefined)
        {
        
            return View();
        }


    }
}