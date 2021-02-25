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
                return Json(new { response = "fail", status = false });
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

                            return Json(new { response = "登入成功", status = 1 });
                        }
                        else
                        {
                            return Json(new { response = "無此人", status = 0 });
                        }
                    }
                    else
                    {
                        return Json(new { response = "信箱尚未驗證成功", status = 0 });
                    }
                }
                else
                {
                    return Json(new { response = "驗證失敗", status = 0 });
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

                    return Json(new { response = "操作成立", status = 1 });
                }
                else
                {
                    return Json(new { response = "驗證失敗", status = 0 });
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
                    return Json(new { response = "已存在", status = 1 });
                    }
                else
                {
                    return Json(new { response = "不存在", status = 1 });
                }
            }
            return Json(new { response = "發生錯誤", status = 0 });
        }

        [HttpPost]
        public ActionResult RegisterEmailIsExist(string email)
        {
            if (ModelState.IsValid)
            {
                if (_accountService.IsEmailExist(email))
                {
                    return Json(new { response = "已存在", status = 1 });
                }
                else
                {
                    return Json(new { response = "不存在", status = 1 });
                }
            }
            return Json(new { response = "發生錯誤", status = 0 });
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

                    return Json(new { response = "操作完成", status = 1 }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { response = "發生錯誤", status = 0 }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { response = "發生錯誤", status = 0 }, JsonRequestBehavior.AllowGet);
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

                    return Json(new { response = result.Status, status = 1 });
                }
                else
                {
                   return Json(new { response = OperationResultStatus.Fail, status = 0 });
                }
            }
            else
            {
                return Json(new { response = OperationResultStatus.ErrorRequest , status = 0 });
            }
        }

        [HttpPost]
        public async Task<ActionResult> GoogleLogin(string token,int type)
        {
            var result = await _accountService.GetGoogleInfo(token);

            if (result.IsSuccessful)
            {
                var googleTokenInfo = JsonConvert.DeserializeObject<GoogleApiTokenInfo>(result.MessageInfo);

                if (_accountService.IsSocialAccountRegister(googleTokenInfo.Email, "Google")) //檢查此帳戶是否存在並註冊
                {
                    var cookie = _accountService.SetCookie(_accountService.GetUser(googleTokenInfo.Email).AccountName, false);
                    Response.Cookies.Add(cookie);
                    return Json(new { response = "第三方登入", status = 1 });
                }
                else
                {
                    if(type == 0) //若不是，去判斷是進行註冊還是登入
                    {
                        var SocialInfo = new SocialInfo
                        {
                            Email = googleTokenInfo.Email,
                            SocialPlatform = "Google",
                            ImgUrl = googleTokenInfo.Picture,
                        };

                        return Json(new { response = JsonConvert.SerializeObject(SocialInfo), status = 1 });
                    }
                    else
                    {
                        return Json(new { response = "尚未註冊", status = 0 });
                    }
                    
                }
            }
            else
            {
                return Json(new { response = "發生錯誤", status = 0 });
            }
        }

        [HttpPost]
        public ActionResult FacebookLogin(string email,string socialPlatform,string imgUrl,int type)
        {
            if (_accountService.IsSocialAccountRegister(email, socialPlatform))
            {
                var cookie = _accountService.SetCookie(_accountService.GetUser(email).AccountName, false);
                Response.Cookies.Add(cookie);
                return Json(new { response = "第三方登入", status = 1 }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                if(type == 0)
                {
                    var info = new SocialInfo
                    {
                        SocialPlatform = socialPlatform,
                        Email = email,
                        ImgUrl = imgUrl
                    };
                    return Json(new { response = JsonConvert.SerializeObject(info), status = 1 }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { response = "尚未註冊", status = 0 }, JsonRequestBehavior.AllowGet);
                }
            }
        }

        public ActionResult LineLogin(string code,int state)
        {
            var result = _accountService.GetLineInfo(code);

            if(result.IsSuccessful)
            {
                var user = JsonConvert.DeserializeObject<LineUserProfile>(result.MessageInfo);

                if (_accountService.IsSocialAccountRegister(user.Email, "Line"))
                {
                    var cookie = _accountService.SetCookie(_accountService.GetUser(user.Email).AccountName, false);
                    Response.Cookies.Add(cookie);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    if (state == 0)
                    {
                        return RedirectToAction("SocialRegister", "Account", new { email = user.Email,photo = user.PictureUrl,social="Line"});
                    }
                    else
                    {
                        ViewBag.Error = true;
                        return View("Login");
                    }

                }
            }
            else
            {
                return Json(new { response = "發生錯誤", status = 0 }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult SocialRegister(string email,string photo,string social)
        {
            ViewBag.Email = email;
            ViewBag.Photo = photo;
            ViewBag.Social = social;
            return View();
        }

        [HttpPost]
        public ActionResult SocialRegister(SocialRegisterViewModel model)
        {
            var result = _accountService.RegisterByThirdParty(model);
            return Json(new { response = result.MessageInfo, status = result.IsSuccessful});
        }
    }
}