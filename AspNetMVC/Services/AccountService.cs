using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AspNetMVC.Repository;
using AspNetMVC.ViewModels;
using AspNetMVC.Models;
using AspNetMVC.Models.Entity;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Text;
using System.Web.Security;
using System.Web.Configuration;

namespace AspNetMVC.Services
{
    public class AccountService
    {
        private readonly UCleanerDBContext _context;
        private readonly BaseRepository _repository;

        public enum AccountStatus
        {
            Exist, //已存在
            NonExist, //不存在
            Verified, //通過驗證
            UnVerified, //未驗證
            HasBeenVerified, //通過驗證
            Registered,
            UnRegistered,
            Error
        }

        public AccountService(){
            _context = new UCleanerDBContext();
            _repository = new BaseRepository(_context);
        }

        public void CreateAccount(RegisterViewModel account)
        {
            var result = new OperationResult();

            using (var transcation = _context.Database.BeginTransaction())
            {
                try
                {
                    var user = new Account
                    {
                        AccountId = Guid.NewGuid(),
                        AccountName = account.Name,
                        Address = account.Address,
                        Password = Helpers.ToMD5(account.Password),
                        Email = account.Email,
                        EmailVerification = false,
                        Gender = account.Gender, // 1 男 2 女 3 其他
                        Phone = account.Phone,
                        Authority = 3, //預設 3 : 一般會員
                        CreateTime = DateTime.UtcNow.AddHours(8),
                        CreateUser = account.Name,
                        EditTime = DateTime.UtcNow.AddHours(8),
                        EditUser = account.Name,
                        Remark = ""
                    };
                    _repository.Create<Account>(user);
                    _context.SaveChanges();

                    var member = new MemberMd
                    {
                        AccountId = user.AccountId,
                        CreateTime = user.CreateTime,
                        CreateUser = user.CreateUser,
                        EditTime = user.EditTime,
                        EditUser = user.EditUser,
                        Name = user.AccountName,
                        
                    };
                    _repository.Create<MemberMd>(member);
                    _context.SaveChanges();

                    result.IsSuccessful = true;
                    transcation.Commit();
                }
                catch (Exception ex)
                {
                    result.IsSuccessful = false;
                    result.Exception = ex;
                    transcation.Rollback();
                }
            }
                
        }

        /// <summary>
        /// 檢查此帳號是否存在
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool IsAccountExist(string name)
        {
            var user = _repository.GetAll<Account>().FirstOrDefault(x => x.AccountName == name);

            return user != null;
        }

        /// <summary>
        /// 檢查此信箱是否存在
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public bool IsEmailExist(string email)
        {
            var user = _repository.GetAll<Account>().FirstOrDefault(x => x.Email == email);

            return user != null;
        }

        /// <summary>
        /// 判斷帳密是否完全符合
        /// </summary>
        /// <param name="accountName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool IsLoginValid(string accountName, string password)
        {
            var p = Helpers.ToMD5(password);
            var user = _repository.GetAll<Account>().FirstOrDefault(x => x.AccountName == accountName && x.Password == p);
            return user != null;
        }

        /// <summary>
        /// 回傳此帳號是否通過信箱啟動
        /// </summary>
        /// <param name="accountName"></param>
        /// <returns></returns>
        public bool IsActivatedEmail(string accountName) {
            var user = _repository.GetAll<Account>().FirstOrDefault(x => x.AccountName == accountName);

            if (user == null) {
                return false;
            }
            else
            {
                return _repository.GetAll<Account>().FirstOrDefault(x => x.AccountName == accountName).EmailVerification;
            }
        }
        
        /// <summary>
        /// 透過Email啟動帳號
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string EmailActivation(Guid? id)
        {
            var result = new OperationResult();
            try
            {
                var user = _repository.GetAll<Account>().FirstOrDefault(x => x.AccountId == id);

                if(user != null)
                {
                    if(user.EmailVerification == false)
                    {
                        user.EmailVerification = true;
                        user.EditTime = DateTime.UtcNow.AddHours(8);
                        user.EditUser = user.AccountName;
                        _repository.Update<Account>(user);
                        _context.SaveChanges();

                        result.MessageInfo = AccountStatus.Verified.ToString();
                        result.IsSuccessful = true;
                    }
                    else
                    {
                        result.IsSuccessful = true;
                        result.MessageInfo = AccountStatus.HasBeenVerified.ToString();
                    }
                }
                else
                {
                    result.IsSuccessful = false;
                    result.MessageInfo = AccountStatus.NonExist.ToString();
                }
            }
            catch(Exception ex)
            {
                result.IsSuccessful = false;
                result.Exception = ex;
            }
            return result.MessageInfo;
        }
       
        /// <summary>
        /// 用於忘記密碼，以帳號、信箱查找
        /// </summary>
        /// <param name="account"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        public bool IsAccountMatch(string account, string email) => _repository.GetAll<Account>().FirstOrDefault(x => x.AccountName == account && x.Email == email) != null;

        /// <summary>
        /// 重置密碼
        /// </summary>
        /// <param name="id"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        public OperationResult UpdatePassword(Guid id,string newPassword) 
        {
            var result = new OperationResult();

            try
            {
                var user = _repository.GetAll<Account>().FirstOrDefault(x => x.AccountId == id);
                if (user != null)
                {
                    user.Password = Helpers.ToMD5(newPassword);
                    user.EditTime = DateTime.UtcNow.AddHours(8);
                    user.EditUser = user.AccountName;
                    _repository.Update<Account>(user);
                    _context.SaveChanges();
                    result.Status = OperationResultStatus.Success;
                }
                else
                {
                    result.Status = OperationResultStatus.Fail;
                }
            }
            catch (Exception ex)
            {
                result.Exception = ex;
                result.Status = OperationResultStatus.ErrorRequest;
            }

            return result;
        }

        public async Task<OperationResult> RegisterByGoogle(string token)
        {
            using (HttpClient client = new HttpClient())
            {

                var or = new OperationResult();
                try
                {
                    var url = $"https://oauth2.googleapis.com/tokeninfo?id_token={token}";
                    client.Timeout = TimeSpan.FromSeconds(30);

                    HttpResponseMessage response = await client.GetAsync(url); //發送Get 請求
                    response.EnsureSuccessStatusCode();

                    var responsebody = await response.Content.ReadAsStringAsync();

                    var googleApiTokenInfo = JsonConvert.DeserializeObject<GoogleApiTokenInfo>(responsebody);


                    if (IsAccountExist(googleApiTokenInfo.Sub) || IsEmailExist(googleApiTokenInfo.Email))
                    {
                        or.IsSuccessful = false;
                        or.MessageInfo = "此帳號已重複申請";
                    }
                    else
                    {
                        RegisterViewModel account = new RegisterViewModel {
                            Address = "",
                            Email = googleApiTokenInfo.Email,
                            Gender = 3,
                            Name = googleApiTokenInfo.Sub,
                            Phone = "",
                            ConfirmPassword = googleApiTokenInfo.Sub,
                            Password = googleApiTokenInfo.Sub,
                            ValidationMessage = ""
                        };
                        CreateAccount(account);
                        Dictionary<string, string> kvp = new Dictionary<string, string>
                    {
                        { "accountname",googleApiTokenInfo.Sub},
                        { "name",googleApiTokenInfo.Name},
                        { "password",googleApiTokenInfo.Sub},
                        { "datetime",DateTime.UtcNow.AddHours(8).ToString().Split(' ')[0]},
                        { "accountid",GetAccountId(googleApiTokenInfo.Sub).ToString()},
                    };

                        SendMail("會員驗證信", googleApiTokenInfo.Email, kvp);

                        or.IsSuccessful = true;
                        or.MessageInfo = account.Name;
                    }
                }
                catch (Exception ex)
                {
                    or.IsSuccessful = false;
                    or.Exception = ex;
                    or.MessageInfo = "發生錯誤";
                }
                return or;
            }
        }

        public async Task<OperationResult> LoginByGoogle(string token)
        {
            using (HttpClient client = new HttpClient())
            {
                var or = new OperationResult();
                try
                {
                    var url = $"https://oauth2.googleapis.com/tokeninfo?id_token={token}";
                    client.Timeout = TimeSpan.FromSeconds(30);

                    HttpResponseMessage response = await client.GetAsync(url); //發送Get 請求
                    response.EnsureSuccessStatusCode();

                    var responsebody = await response.Content.ReadAsStringAsync();

                    var googleApiTokenInfo = JsonConvert.DeserializeObject<GoogleApiTokenInfo>(responsebody);


                    if (IsAccountExist(googleApiTokenInfo.Sub) && IsEmailExist(googleApiTokenInfo.Email))
                    {
                        or.IsSuccessful = true;
                        or.MessageInfo = $"驗證成功 {googleApiTokenInfo.Sub}";
                    }
                    else
                    {
                        or.IsSuccessful = false;
                        or.MessageInfo = "驗證失敗";
                    }
                }
                catch (Exception ex)
                {
                    or.IsSuccessful = false;
                    or.Exception = ex;
                    or.MessageInfo = "發生錯誤";
                }
                return or;
            }
        }


        public OperationResult RegisterByFacebook(FacebookInfo fbInfo)
        {
            var or = new OperationResult();

            if (IsAccountExist(fbInfo.FacebookId) || IsEmailExist(fbInfo.Email))
            {
                or.IsSuccessful = false;
                or.MessageInfo = "此帳號已重複申請";
            }
            else
            {
                RegisterViewModel account = new RegisterViewModel
                {
                    Address = "",
                    Email = fbInfo.Email,
                    Gender = 3,
                    Name = fbInfo.FacebookId,
                    Phone = "",
                    ConfirmPassword = fbInfo.FacebookId,
                    Password = fbInfo.FacebookId,
                    ValidationMessage = ""
                };

                CreateAccount(account);

                Dictionary<string, string> kvp = new Dictionary<string, string>
                    {
                        { "accountname",fbInfo.FacebookId},
                        { "name",fbInfo.Name},
                        { "password",fbInfo.FacebookId},
                        { "datetime",DateTime.UtcNow.AddHours(8).ToString().Split(' ')[0]},
                        { "accountid",GetAccountId(fbInfo.FacebookId).ToString()},
                    };

                SendMail("會員驗證信", fbInfo.Email, kvp);

                or.IsSuccessful = true;
                or.MessageInfo = account.Name;
            }
            return or;
        }

        public OperationResult LoginByFacebook(FacebookInfo fbInfo)
        {
            var or = new OperationResult();

            if (!IsEmailExist(fbInfo.Email))
            {
                if (IsAccountExist(fbInfo.FacebookId))
                {
                    or.IsSuccessful = true;
                    or.MessageInfo = $"驗證成功 {fbInfo.FacebookId}";
                }
                else
                {
                    or.IsSuccessful = false;
                    or.MessageInfo = "此帳號還未註冊";
                }

            }
            else
            {
                or.IsSuccessful = false;
                or.MessageInfo = "此帳號信箱已註冊過，改以其他社群帳號或使用本站會員系統註冊";
            }

            return or;
        }

        public async Task<OperationResult> RegisterByLine(string code)
        {
            using (HttpClient client = new HttpClient())
            {

                var or = new OperationResult();
                try
                {
                    var url = $"https://api.line.me/v2/oauth/accessToken";
                    client.Timeout = TimeSpan.FromSeconds(30);
                    var values = new Dictionary<string,string>{
                           { "grant_type", "authorization_code" },
                           { "client_id", $"{WebConfigurationManager.AppSettings["Line_client_id"]}" },
                           { "client_secret",$"{WebConfigurationManager.AppSettings["Line_client_secret"]}"},
                           { "code",code},
                           { "redirect_uri","https://localhost:44308/Account/RegisterByLineLogin"}
                        };
                    var content = new FormUrlEncodedContent(values);
                    var response = await client.PostAsync(url, content);
                    var responseString = await response.Content.ReadAsStringAsync();
                    var googleApiTokenInfo = JsonConvert.DeserializeObject<GoogleApiTokenInfo>(responseString);


                    if (IsAccountExist(googleApiTokenInfo.Sub) || IsEmailExist(googleApiTokenInfo.Email))
                    {
                        or.IsSuccessful = false;
                        or.MessageInfo = "此帳號已重複申請";
                    }
                    else
                    {
                        RegisterViewModel account = new RegisterViewModel
                        {
                            Address = "",
                            Email = googleApiTokenInfo.Email,
                            Gender = 3,
                            Name = googleApiTokenInfo.Sub,
                            Phone = "",
                            ConfirmPassword = googleApiTokenInfo.Sub,
                            Password = googleApiTokenInfo.Sub,
                            ValidationMessage = ""
                        };
                        CreateAccount(account);
                        Dictionary<string, string> kvp = new Dictionary<string, string>
                    {
                        { "accountname",googleApiTokenInfo.Sub},
                        { "name",googleApiTokenInfo.Name},
                        { "password",googleApiTokenInfo.Sub},
                        { "datetime",DateTime.UtcNow.AddHours(8).ToString().Split(' ')[0]},
                        { "accountid",GetAccountId(googleApiTokenInfo.Sub).ToString()},
                    };

                        SendMail("會員驗證信", googleApiTokenInfo.Email, kvp);

                        or.IsSuccessful = true;
                        or.MessageInfo = account.Name;
                    }
                }
                catch (Exception ex)
                {
                    or.IsSuccessful = false;
                    or.Exception = ex;
                    or.MessageInfo = "發生錯誤";
                }
                return or;
            }
        }

        public Guid GetAccountId(string accountName)
        {
            return _repository.GetAll<Account>().FirstOrDefault(x => x.AccountName == accountName).AccountId;
        }

        public Account GetUser(Guid id) 
        {
            return _repository.GetAll<Account>().FirstOrDefault(x => x.AccountId == id);
        }

        public HttpCookie SetCookie(string accountName,bool rememberMe)
        {
            HttpCookie cookie_user = new HttpCookie("user");
            var cookieText = Encoding.UTF8.GetBytes(accountName);
            var encryptedValue = Convert.ToBase64String(MachineKey.Protect(cookieText, "protectedCookie"));
            cookie_user.Values["user_accountname"] = encryptedValue;

            if (rememberMe == true) cookie_user.Expires = DateTime.Now.AddDays(7);

            return cookie_user;
        }

        public void SendMail(string title,string email, Dictionary<string, string> kvp)
        {
            Email objEmail = new Email
            {
                RecipientAddress = email,
                Subject = $"{title} - 此信件由系統自動發送，請勿直接回覆 from [Gmail]"
            };

            objEmail.Body = objEmail.ReplaceString(objEmail.GetEmailString(Email.Template.EmailActivation), kvp);

            objEmail.SendEmailFromGmail();
        }
    }
    
}