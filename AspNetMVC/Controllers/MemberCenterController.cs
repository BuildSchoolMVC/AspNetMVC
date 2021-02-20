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

namespace AspNetMVC.Controllers
{
    public class MemberCenterController : Controller

    {
        
        private MemberMd db = new MemberMd();
        // GET: MemberCenter
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
            
            MemberCenterViewModels memberVm = _MemberCenterService.GetMember(AccountId);
            return View(memberVm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "Name,Phone,Mail,Address")] Guid AccountId, MemberCenterViewModels memberVm)
        {
            MemberMd memberMd = _MemberCenterService.SaveModel(AccountId,memberVm);
            if (ModelState.IsValid)
            {
                
                return RedirectToAction("Index");
            }
            return View(memberMd);
        }
        
    }
}