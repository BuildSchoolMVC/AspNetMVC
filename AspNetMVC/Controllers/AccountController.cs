using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AspNetMVC.Models;
using AspNetMVC.ViewModel;
using AspNetMVC.Service;
using System.Text;
using System.Web.Security;
using System.Configuration;
using System.Collections.Generic;

namespace AspNetMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly AccountService _accountService;
        public AccountController()
        {
            _accountService = new AccountService();
        }

        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login([Bind(Include = "AccountName,Password,RememberMe,ValidationMessage")]LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            else
            {
                var isVerify = new GoogleReCaptcha().GetCaptchaResponse(model.ValidationMessage);

                if (isVerify)
                {
                    if (_accountService.IsActivatedEmail(model.AccountName)) { 
                    
                        if (_accountService.IsLoginValid(model.AccountName, model.Password))
                        {
                            HttpCookie cookie_user = new HttpCookie("user");
                            var cookieText = Encoding.UTF8.GetBytes(model.AccountName);
                            var encryptedValue = Convert.ToBase64String(MachineKey.Protect(cookieText, "protectedCookie"));
                            cookie_user.Values["user_id"] = encryptedValue;

                            if (model.RememberMe == true) cookie_user.Expires = DateTime.Now.AddDays(7);

                            Response.Cookies.Add(cookie_user);

                            return Json(new { response = "success" });
                        }
                        else
                        {
                            return Json(new { response = "fail" });
                        }
                    }
                    else
                    {
                        return Json(new { response = "emailActivationFail" });
                    }
                }
                else
                {
                    return Json(new { response = "valdationFail" });
                }
            }
        }

        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Register([Bind(Include = "Email,Password,Name,Gender,Address,Phone,ValidationMessage")]RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var isVerify = new GoogleReCaptcha().GetCaptchaResponse(model.ValidationMessage);
                if (isVerify)
                {
                    _accountService.CreateAccount(model);

                    Dictionary<string, string> kvp = new Dictionary<string, string>
                {
                        { "accountname",model.Name},
                        { "name",model.Name},
                        { "password",model.Password},
                        { "datetime",DateTime.Now.ToString().Split(' ')[0]},
                        { "accountid",_accountService.GetAccountId(model.Name)},
                };

                    Email objEmail = new Email
                    {
                        Server_UserName = ConfigurationManager.AppSettings["GmailServer_UserName"],
                        Server_Password = ConfigurationManager.AppSettings["GmailServer_Password"],
                        Server_SmtpClient = ConfigurationManager.AppSettings["GmailServer_SmtpClient"],
                        Server_SmtpClientPort = ConfigurationManager.AppSettings["GmailServer_SmtpClientPort"],
                        RecipientAddress = model.Email,
                        SenderName = "系統管理者",
                        SenderAddress = ConfigurationManager.AppSettings["GmailServer_UserName"] + "@gmail.com",
                        Subject = "會員帳號啟動 - 此信件由系統自動發送，請勿直接回覆 from [Gmail]"
                    };

                    objEmail.Body = objEmail.ReplaceString(objEmail.GetEmailString(Email.Template.EmailActivation), kvp); // 取得回覆HTML模板，並且指定字串插入模板裡，最後指派給此信的內容

                    objEmail.SendEmailFromGmail();

                    return Json(new { response = "success" });
                }
                else
                {
                    return Json(new { response = "valdationFail" });
                }
            }

            // 如果執行到這裡，發生某項失敗，則重新顯示表單
            return View(model);
        }
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        public ActionResult RegisterIsExist(string name)
        {
            if (ModelState.IsValid)
            {
                if(_accountService.AccountIsExist(name)){
                    return Json(new { response = "exist" });
                }
                else
                {
                    return Json(new { response = "nonexist" });
                }
            }
            return Json(new { response = "error" });
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult RegisterEmailIsExist(string email)
        {
            if (ModelState.IsValid)
            {
                if (_accountService.EmailIsExist(email))
                {
                    return Json(new { response = AccountService.AccountStatus.Exist.ToString() });
                }
                else
                {
                    return Json(new { response = AccountService.AccountStatus.NonExist.ToString() });
                }
            }
            return Json(new { response = AccountService.AccountStatus.Error.ToString() });
        }

        [AllowAnonymous]
        public ActionResult RegisterEmailActivation(Guid id)
        {
            if (ModelState.IsValid)
            {
                 ViewBag.Result = _accountService.EmailActivation(id);

                return View();
            }
            else
            {
                return View("Error");
            }
        }

        public RedirectToRouteResult Logout()
        {
            HttpCookie cookie_user = new HttpCookie("user")
            {
                Expires = DateTime.Now.AddDays(-1)
            };
            Response.Cookies.Add(cookie_user);

            //HttpCookie cookie_decode = new HttpCookie("decode_user");

            //if (Request.Cookies["user"] != null)
            //{
            //    var convertedResult = DecodeCookie(Request.Cookies["user"]["user_id"]);
            //    cookie_decode.Value = convertedResult;
            //    Response.Cookies.Add(cookie_decode);
            //}

            return RedirectToAction("Index", "Home");
        }
    }
}