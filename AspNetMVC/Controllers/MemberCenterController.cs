using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AspNetMVC.Controllers
{
    public class MemberCenterController : Controller
    {
        // GET: MemberCenter
        public ActionResult Index()
        {
            return View();
        }
    }
}