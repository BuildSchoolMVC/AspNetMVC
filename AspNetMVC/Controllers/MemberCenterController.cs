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

namespace AspNetMVC.Controllers
{
    public class MemberCenterController : Controller

    {
        
        private UCleanerDBContext db = new UCleanerDBContext();
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
        
        public ActionResult Index(Guid AccountId)
        {

            if (AccountId == null)
            {
                return RedirectToAction("Login","AccountController");
            }
            var result = _MemberCenterService.GetMember(AccountId);
            return View(result);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Name,Phone,Mail,Address")] MemberMd memberMd)
        {
            if (ModelState.IsValid)
            {
                db.Entry(memberMd).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(memberMd);
        }
    }
}