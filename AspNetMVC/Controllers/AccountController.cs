﻿using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AspNetMVC.Models;
using AspNetMVC.ViewModels;
using AspNetMVC.Services;
using System.Text;
using System.Web.Security;
using System.Configuration;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;

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
        public ActionResult Login()
        {
            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        public ActionResult Login([Bind(Include = "AccountName,Password,RememberMe,ValidationMessage")] LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { response = "fail" });
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
                            cookie_user.Values["user_accountname"] = encryptedValue;

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
        public ActionResult Register()
        {
            return View();
        }

        // POST: /Account/Register
        [HttpPost]
        public ActionResult Register([Bind(Include = "Email,Password,Name,Gender,Address,Phone,ValidationMessage")] RegisterViewModel model)
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
                        { "accountid",_accountService.GetAccountId(model.Name).ToString()},
                    };

                    Email objEmail = new Email
                    {
                        RecipientAddress = model.Email,
                        Subject = "會員帳號啟動 - 此信件由系統自動發送，請勿直接回覆 from [Gmail]"
                    };

                    objEmail.Body = objEmail.ReplaceString(objEmail.GetEmailString(Email.Template.EmailActivation), kvp);

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
        public ActionResult RegisterIsExist(string name)
        {
            if (ModelState.IsValid)
            {
                if (_accountService.AccountIsExist(name))
                {
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
        public ActionResult RegisterEmailIsExist(string email)
        {
            if (ModelState.IsValid)
            {
                if (_accountService.EmailIsExist(email))
                {
                    return Json(new { response = "Exist" });
                }
                else
                {
                    return Json(new { response = "NonExist" });
                }
            }
            return Json(new { response = "Error" });
        }

        public ActionResult RegisterEmailActivation(Guid? id)
        {
            if(id != null)
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

            return RedirectToAction("Index", "Home");
        }

        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public JsonResult ForgotPassword([Bind(Include = "Email,AccountName")] ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid) {
                if (_accountService.IsAccountMatch(model.AccountName, model.Email))
                {

                    Email objEmail = new Email
                    {
                        RecipientAddress = model.Email,
                        Subject = "密碼重置 - 此信件由系統自動發送，請勿直接回覆 from [Gmail]"
                    };
                    Dictionary<string, string> kvp = new Dictionary<string, string>
                    {
                        { "id", _accountService.GetAccountId(model.AccountName).ToString()},
                        { "accountname", model.AccountName},
                        { "datetimestring",DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss")},
                        { "datetime",DateTime.Now.ToString()}
                    };

                    objEmail.Body = objEmail.ReplaceString(objEmail.GetEmailString(Email.Template.ForgotPassword), kvp);

                    objEmail.SendEmailFromGmail();

                    return Json(new { response = "success" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { response = "error" }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { response = "error" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ResetPassword() 
        {
            string id = Request["id"];
            string applicationTime = Request["t"];
            string systemTime = DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss");

            var applicationTimeExpiredHour = int.Parse(applicationTime.Split('_')[3]) + 2 >= 24? int.Parse(applicationTime.Split('_')[3]) - 22 : int.Parse(applicationTime.Split('_')[3]) + 2;

            var systemTimeHour = int.Parse(systemTime.Split('_')[3]);

            ViewBag.STH = systemTimeHour;
            ViewBag.ATEH = applicationTimeExpiredHour;
            ViewBag.IsExpired = systemTimeHour >= applicationTimeExpiredHour;
            ViewBag.Id = id;

            return View();
        }

        [HttpPost]
        public ActionResult ResetPassword([Bind(Include = "AccountId,NewPassword")]NewPasswordViewModel model) 
        {
            if (ModelState.IsValid)
            {
                var result = _accountService.UpdatePassword(model.AccountId, model.NewPassword);

                if (result.Status == 0) 
                {
                    Email objEmail = new Email
                    {
                        RecipientAddress = _accountService.GetUser(model.AccountId).Email,
                        Subject = "你的【uCleaner - 打掃服務】會員密碼已重置"
                    };
                    Dictionary<string, string> kvp = new Dictionary<string, string>
                    {
                        { "accountname", _accountService.GetUser(model.AccountId).AccountName},
                    };

                    objEmail.Body = objEmail.ReplaceString(objEmail.GetEmailString(Email.Template.SuccessResetPassword), kvp);

                    objEmail.SendEmailFromGmail();

                    return Json(new { response = result.Status });
                }
                else
                {
                   return Json(new { response = OperationResultStatus.Fail });
                }
            }
            else
            {
                return Json(new { response = OperationResultStatus.ErrorRequest });
            }
        }

        [HttpPost]
        public async Task<ActionResult> RegisterByGoogleLogin(string token)
        {

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var url = $"https://oauth2.googleapis.com/tokeninfo?id_token={token}";
                    client.Timeout = TimeSpan.FromSeconds(30);
                    HttpResponseMessage response = await client.GetAsync(url); //發送Get 請求
                    response.EnsureSuccessStatusCode();
                    var responsebody = await response.Content.ReadAsStringAsync();

                    //var googleApiTokenInfo = JsonConvert.DeserializeObject<GoogleApiTokenInfo>(responsebody);
                }
                catch (Exception ex) {
                    return null;
                }
            }
            return Json(new { response="success 好棒棒!"});
        }
    }
}