using AspNetMVC.Repository;
using AspNetMVC.Services;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AspNetMVC.Models;
using AspNetMVC.Models.Entity;
using AspNetMVC.ViewModel;
using Newtonsoft.Json;
using AspNetMVC.ViewModels;

namespace AspNetMVC.Controllers
{
    public class MemberCenterController : Controller

    {
        
        private MemberMd db = new MemberMd();
        private readonly MemberCenterService _MemberCenterService;
        private readonly AccountService _AccountService;
        public MemberCenterController()
        {
            _MemberCenterService = new MemberCenterService();
            _AccountService = new AccountService();
        }
        public Guid MemberHelper()
        {
            var UserName = Helpers.DecodeCookie(Request.Cookies["user"]["user_accountname"]);
            var AccountId = _AccountService.GetAccountId(UserName);
            return AccountId;
        }
        
        public ActionResult Index()
        {
            
            if (MemberHelper() == null)
            {
                return RedirectToAction("Login","Account");
            }

            List<CouponViewModel> coupons = new List<CouponViewModel>()
            {
               new CouponViewModel{CouponName="Anderson",DiscountAmount=100m, DateStart=new DateTime(2021,01,01),DateEnd=new DateTime(2021,12,31)},
               new CouponViewModel{CouponName="peter",DiscountAmount=100m, DateStart=new DateTime(2021,01,01),DateEnd=new DateTime(2022,12,31)},
               new CouponViewModel{CouponName="kevin",DiscountAmount=100m, DateStart=new DateTime(2021,01,01),DateEnd=new DateTime(2023,12,31)},
            };

            var unusedcoupons = coupons.Where(x => x.Status == 0).ToList();

            var usedcoupons = coupons.Where(x => x.Status == 1).ToList(); ;

            var expiredcoupons = coupons.Where(x => x.Status == 2).ToList();


            
            ViewBag.unusedcoupondata = unusedcoupons;

           
            ViewBag.usedcoupondata = usedcoupons;

          
            ViewBag.expiredcoupondata = expiredcoupons;
            
            MemberCenterViewModels memberVm = _MemberCenterService.GetMember(MemberHelper());
            return View(memberVm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "Name,Phone,Mail,Address")] MemberCenterViewModels memberVm)
        {
            MemberMd memberMd = _MemberCenterService.SaveModel(MemberHelper(),memberVm);

            
            return View(memberVm);
        }
        [HttpPost]
        public ActionResult Password(string Password, string NewPassword,string ConfirmPassword)
        {
            var password = new MemberCenterPassword();
            password.Password = Password;
            password.NewPassword = NewPassword;
            password.ConfirmPassword = ConfirmPassword;
             var result = _MemberCenterService.EditPassword(MemberHelper(),password);
             if (result.IsSuccessful)
             {
                 return Json(new { response = "success" }, JsonRequestBehavior.AllowGet);
             }
             else
             {
                 return Json(new { response = "error" }, JsonRequestBehavior.AllowGet);
             }
        }
    }
}