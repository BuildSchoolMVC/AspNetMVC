using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AspNetMVC.Models;
using AspNetMVC.Models.Entity;
using AspNetMVC.Services;
using AspNetMVC.ViewModel;
using Newtonsoft.Json;

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