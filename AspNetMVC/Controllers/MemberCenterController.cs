using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AspNetMVC.Services;

namespace AspNetMVC.Controllers
{
    public class MemberCenterController : Controller
    {
        
        // GET: MemberCenter
        private readonly MemberCenterService _MemberCenterService;
        public MemberCenterController()
        {
            _MemberCenterService = new MemberCenterService();
        }
        
        public ActionResult Index(Guid accountId)
        {
            
            if (Response.Cookies["cookie_user"] == null)
            {
                return RedirectToAction("Login", "AccountController");
            }
            var result = _MemberCenterService.GetMember(accountId);
            return View(result);
        }
    }
}