using AspNetMVC.Repository;
using AspNetMVC.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
 

namespace AspNetMVC.Controllers
{
    public class MemberCenterController : Controller
    {
        //get: membercenter
        public ActionResult Index()
        {
            ViewBag.coupondata = new CouponService().CouponData;
            return View();
        }

        //public ActionResult CouponService()
        //{
        //    ViewBag.CouponData = new CouponService().CouponData;
        //    return View();
        //}
    }
}