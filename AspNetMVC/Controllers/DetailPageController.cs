using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AspNetMVC.Controllers
{
    public class DetailPageController : Controller
    {
        // GET: DetailPage
        public ActionResult Index()
        {
            return View();
        }
    }
}