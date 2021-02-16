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
        private readonly DetailPageService _accountService;
        public DetailPageController()
        {
            _accountService = new DetailPageService();
        }

        // GET: DetailPage
        public ActionResult Index(int id)
        {
            var result = _accountService.GetSingleProduct(id);

            if (result.Name != "") 
            { 
                return View(result);
            }
            else 
            {
                return View("Error");
            }

        }
    }
}