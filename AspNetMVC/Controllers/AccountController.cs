using System;
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

        public ActionResult Login()
        {
            if(Request.Cookies["user"] != null)
            {
                return View("../Home/Index");
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult Login([Bind(Include = "AccountName,Password,RememberMe,ValidationMessage")] LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { response = "fail", isSuccess = false });
            }
            else
            {
                var isVerify = new GoogleReCaptcha().GetCaptchaResponse(model.ValidationMessage);

                if (isVerify)
                {
                    if (_accountService.IsActivatedEmail(model.AccountName)) {
                        if (_accountService.IsLoginValid(model.AccountName, model.Password))
                        {
                            var cookie = _accountService.SetCookie(model.AccountName,model.RememberMe);

                            Response.Cookies.Add(cookie);

                            return Json(new { response = "success", isSuccess = true });
                        }
                        else
                        {
                            return Json(new { response = "",isSuccess = false });
                        }
                    }
                    else
                    {
                        return Json(new { response = "emailActivationFail", isSuccess = false });
                    }
                }
                else
                {
                    return Json(new { response = "valdationFail", isSuccess = false });
                }
            }
        }

        public ActionResult Register()
        {
            if (Request.Cookies["user"] != null)
            {
                return View("../Home/Index");
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult Register([Bind(Include = "Email,Password,Name,Gender,Address,Phone,ValidationMessage")] RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var isVerify = new GoogleReCaptcha().GetCaptchaResponse(model.ValidationMessage);
                if (isVerify)
                {
                    model.IsIntegrated = false;
                    model.IsThirdParty = false;
                    model.SocialPatform = "None";
                    _accountService.CreateAccount(model);

                    Dictionary<string, string> kvp = new Dictionary<string, string>
                    {
                        { "accountname",model.Name},
                        { "name",model.Name},
                        { "password",model.Password},
                        { "datetime",DateTime.UtcNow.AddHours(8).ToString().Split(' ')[0]},
                        { "accountid",_accountService.GetAccountId(model.Name).ToString()},
                        { "isSocialActivation","false"}
                    };

                    _accountService.SendMail("會員驗證信",model.Email, kvp);

                    return Json(new { response = "success",isSuccess= true });
                }
                else
                {
                    return Json(new { response = "valdationFail", isSuccess = false });
                }
            }

            // 如果執行到這裡，發生某項失敗，則重新顯示表單
            return View(model);
        }

        [HttpPost]
        public ActionResult RegisterIsExist(string name)
        {
            if (ModelState.IsValid)
            {
                if (_accountService.IsAccountExist(name))
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
                if (_accountService.IsEmailExist(email))
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
                    ViewBag.Result = _accountService.EmailActivation(id).MessageInfo;
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
                    Dictionary<string, string> kvp = new Dictionary<string, string>
                    {
                        { "id", _accountService.GetAccountId(model.AccountName).ToString()},
                        { "accountname", model.AccountName},
                        { "datetimestring",DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss")},
                        { "datetime",DateTime.Now.ToString()}
                    };

                    _accountService.SendMail("密碼重置", model.Email,kvp);

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
            var result = await _accountService.GetGoogleInfo(token);

            if (result.IsSuccessful)
            {
                var googleTokenInfo = JsonConvert.DeserializeObject<GoogleApiTokenInfo>(result.MessageInfo);

                if (_accountService.IsSocialAccountRegister(googleTokenInfo.Email, "Google"))
                {

                    // 登入並導回首頁登入
                    //return View();
                }
                else
                {
                    ViewBag.SocialInfo = new RegisterViewModel
                    {
                        Email = googleTokenInfo.Email,
                        SocialPatform = "Google",
                        ImgUrl = googleTokenInfo.Picture,
                    };
                }
            }
            else
            {

            }

            return View();
        }

        public async Task<ActionResult> LoginByGoogleLogin(string token)
        {
            var result = await _accountService.LoginByGoogle(token);

            if (result.IsSuccessful)
            {
                var cookie = _accountService.SetCookie(result.MessageInfo.Split(' ')[1], false);
                Response.Cookies.Add(cookie);
            }

            return Json(new { response = result.MessageInfo, status = result.IsSuccessful });
        }

        [HttpPost]
        public ActionResult RegisterByFacebookLogin([Bind(Include = "FacebookId,Email,Name")]FacebookInfo model)
        {
            var result = _accountService.RegisterByFacebook(model);

            return Json(new { response = result.MessageInfo, status = result.IsSuccessful });
        }

        [HttpPost]
        public ActionResult LoginByFacebookLogin([Bind(Include = "FacebookId,Email,Name")] FacebookInfo model)
        {
            var result = _accountService.LoginByFacebook(model);

            if (result.IsSuccessful)
            {
                var cookie = _accountService.SetCookie(result.MessageInfo.Split(' ')[1], false);
                Response.Cookies.Add(cookie);
                return Json(new { response = "註冊成功", status = result.IsSuccessful });
            }
            else
            {
                return Json(new { response = result.MessageInfo, status = result.IsSuccessful });
            }
        }

        public ActionResult RegisterByLineLogin(string code)
        {
            var result = _accountService.RegisterByLine(code);

            if (result.IsSuccessful)
            {
                return new RedirectResult("/Account/Login");
            }
            else
            {
                string content = $"<h1 style='text-align:center;font-size:80px;margin-top:100px;'>{result.MessageInfo}或改以其他社群帳號或本站會員系統註冊。</h1><a href='https://localhost:44308/' style='text-align:center;display:block;'>將於5秒後回到首頁，或是按此回去</a></script>" + "<script>setTimeout(function(){ window.location.href = 'https://localhost:44308/'},5000);</script>";
               
                return Content(content);
            }
        }

        public ActionResult LoginByLineLogin(string code)
        {
            var result = _accountService.LoginByLine(code);

            if (result.IsSuccessful)
            {
                
                var cookie = _accountService.SetCookie(result.MessageInfo.Split(' ')[1], false);
                Response.Cookies.Add(cookie);
                return new RedirectResult("/Home/Index");
            }
            else
            {
                string content = @"<h1 style='text-align:center;font-size:80px;margin-top:100px;'>此帳號還未註冊</h1><a href='https://localhost:44308/' style='text-align:center;display:block;'>將於5秒後回到首頁，或是按此回去</a></script><script>setTimeout(function(){ window.location.href = 'https://localhost:44308/'},5000);</script>";
                return Content(content);
            }
        }

    }
}