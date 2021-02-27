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
using System.Net;
using System.Collections.Specialized;
using System.IO;

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
        public OperationResult CreateAccount(RegisterViewModel account)
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
                        EmailStatus = JsonConvert.SerializeObject(new { EmailVerification = account.EmailVerification , IsProvidedByThirdParty = account.IsProvidedByThirdParty, IsProvidedByUser =  account.IsProvidedByUser}),
                        Gender = account.Gender, // 1 男 2 女 3 其他
                        Phone = account.Phone,
                        Authority = 3, //預設 3 : 一般會員
                        CreateTime = DateTime.UtcNow.AddHours(8),
                        CreateUser = account.Name,
                        EditTime = DateTime.UtcNow.AddHours(8),
                        EditUser = account.Name,
                        IsThirdParty = account.IsThirdParty,
                        IsIntegrated = account.IsIntegrated,
                        SocialPlatform = account.SocialPatform,
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

            return result;
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
                return JsonConvert.DeserializeObject<EmailStatus>(user.EmailStatus).EmailVerification;
            }
        }
        
        /// <summary>
        /// 透過Email啟動帳號
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public OperationResult EmailActivation(Guid? id)
        {
            var result = new OperationResult();
            try
            {
                var user = _repository.GetAll<Account>().FirstOrDefault(x => x.AccountId == id);

                if(user != null)
                {
                    if(JsonConvert.DeserializeObject<EmailStatus>(user.EmailStatus).EmailVerification == false)
                    {
                        var emailStatus = JsonConvert.DeserializeObject<EmailStatus>(user.EmailStatus);
                        emailStatus.EmailVerification = true;

                        user.EmailStatus = JsonConvert.SerializeObject(emailStatus);
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
            return result;
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

        public OperationResult RegisterByThirdParty(SocialRegisterViewModel model)
        {
            var op = new OperationResult();
            if (model.IsIntegrated)
            {
                if (IsLoginValid(model.AccountName, model.Password))
                {
                    var user = GetUser(model.AccountName, Helpers.ToMD5(model.Password));

                    user.IsIntegrated = true;
                    user.IsThirdParty = true;
                    user.SocialPlatform = model.SocialPlatform;
                    user.EditTime = DateTime.UtcNow.AddHours(8);
                    user.AccountName = model.AccountName;

                    _repository.Update<Account>(user);
                    _context.SaveChanges();

                    op.IsSuccessful = true;
                    op.MessageInfo = "註冊成功";
                }
                else
                {
                    op.IsSuccessful = false;
                    op.MessageInfo = "帳密有誤";
                }
            }
            else
            {
                var account = new RegisterViewModel
                {
                    Email = model.Email,
                    Address = null,
                    Gender = 3,
                    EmailVerification = true,
                    Name = model.AccountName,
                    ConfirmPassword = "",
                    Password = "",
                    SocialPatform = model.SocialPlatform,
                    Phone = null,
                    IsIntegrated = false,
                    IsThirdParty = true,
                    IsProvidedByThirdParty = model.IsIsProvidedByThirdParty
                    
                };

                if (model.IsIsProvidedByThirdParty) account.IsProvidedByUser = model.IsIsProvidedByThirdParty;
                else account.IsProvidedByUser = !string.IsNullOrEmpty(model.Email);

                var result = CreateAccount(account);

                if (result.IsSuccessful)
                {
                    op.IsSuccessful = result.IsSuccessful;
                    op.MessageInfo = "註冊成功";
                }
                else
                {
                    op.IsSuccessful = result.IsSuccessful;
                    op.MessageInfo = "註冊失敗";
                }
            }

            return op;
        }

        public async Task<OperationResult> GetGoogleInfo(string token)
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
                   
                    or.IsSuccessful = true;
                    or.MessageInfo = responsebody;
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

        public async Task<OperationResult> GetFacebookInfo(string code)
        {
            using (HttpClient client = new HttpClient())
            {
                var or = new OperationResult();
                try
                {
                    var getAccessTokenUrl = "https://graph.facebook.com/v9.0/oauth/access_token?";
                    getAccessTokenUrl += $"client_id={WebConfigurationManager.AppSettings["Facebook_id"]}";
                    getAccessTokenUrl += $"&redirect_uri=https://localhost:44308/Account/RegisterByFacebookLogin";
                    getAccessTokenUrl += $"&client_secret={WebConfigurationManager.AppSettings["Facebook_secret"]}";
                    getAccessTokenUrl += $"&code={code}";

                    HttpResponseMessage accessTokenResponse = await client.GetAsync(getAccessTokenUrl);
                    accessTokenResponse.EnsureSuccessStatusCode();

                    var responseBody = await accessTokenResponse.Content.ReadAsStringAsync();

                    var result = JsonConvert.DeserializeObject<FacebookApiTokenInfo>(responseBody);

                    var getUserInfoUrl = "https://graph.facebook.com/me?access_token=";
                    getUserInfoUrl += result.Access_token;

                    HttpResponseMessage userInforesponse = await client.GetAsync(getUserInfoUrl);
                    userInforesponse.EnsureSuccessStatusCode();

                    var userInfoResponseBody = await userInforesponse.Content.ReadAsStringAsync();

                    or.IsSuccessful = true;
                    or.MessageInfo = userInfoResponseBody;
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

        public OperationResult GetLineInfo(string code)
        {
            var or = new OperationResult();

            try
            {
                var url = "https://api.line.me/oauth2/v2.1/token";
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                NameValueCollection postParams = HttpUtility.ParseQueryString(string.Empty);

                var values = new Dictionary<string, string>{
                        { "grant_type", "authorization_code" },
                        { "client_id", $"{WebConfigurationManager.AppSettings["Line_client_id"]}" },
                        { "client_secret",$"{WebConfigurationManager.AppSettings["Line_client_secret"]}"},
                        { "code",code},
                        { "redirect_uri","https://localhost:44308/Account/LineLogin"}
                    };
                foreach (var kvp in values)
                {
                    postParams.Add(kvp.Key, kvp.Value);
                }

                byte[] byteArray = Encoding.UTF8.GetBytes(postParams.ToString());
                using (Stream reqStream = request.GetRequestStream())
                {
                    reqStream.Write(byteArray, 0, byteArray.Length);
                }

                string responseStr = "";

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    using (StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                    {
                        responseStr = sr.ReadToEnd();
                    }
                }

                LineLoginToken tokenObj = JsonConvert.DeserializeObject<LineLoginToken>(responseStr);
                string id_token = tokenObj.Id_token;

                var jst = new System.IdentityModel.Tokens.Jwt.JwtSecurityToken(id_token);
                LineUserProfile user = new LineUserProfile();
                user.UserId = jst.Payload.Sub;
                user.DisplayName = jst.Payload["name"].ToString();
                user.PictureUrl = jst.Payload["picture"].ToString();
                if (jst.Payload.ContainsKey("email") && !string.IsNullOrEmpty(Convert.ToString(jst.Payload["email"])))
                {
                    user.Email = jst.Payload["email"].ToString();
                }

                or.IsSuccessful = true;
                or.MessageInfo = JsonConvert.SerializeObject(user);
            }
            catch (Exception ex)
            {
                or.IsSuccessful = false;
                or.Exception = ex;
                or.MessageInfo = "發生錯誤";
            }
            return or;
        }

        public OperationResult GetMicrosoftInfo(string code)
        {
            var or = new OperationResult();

            try
            {
                var url = $"https://login.microsoftonline.com/common/oauth2/v2.0/token";
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                NameValueCollection postParams = HttpUtility.ParseQueryString(string.Empty);

                var values = new Dictionary<string, string>{
                        { "grant_type", "authorization_code" },
                        { "client_id", $"{WebConfigurationManager.AppSettings["Microsoft_client_id"]}" },
                        { "client_secret",$"{WebConfigurationManager.AppSettings["Microsoft_client_secret"]}"},
                        { "code",code},
                        { "scope","&scope=openid%20https%3A%2F%2Fgraph.microsoft.com%2Fmail.read"},
                        { "redirect_uri",@HttpUtility.UrlEncode("https://localhost:44308/Account/MicrosoftLogin")}
                    };
                foreach (var kvp in values)
                {
                    postParams.Add(kvp.Key, kvp.Value);
                }

                byte[] byteArray = Encoding.UTF8.GetBytes(postParams.ToString());
                using (Stream reqStream = request.GetRequestStream())
                {
                    reqStream.Write(byteArray, 0, byteArray.Length);
                }

                string responseStr = "";

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    using (StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                    {
                        responseStr = sr.ReadToEnd();
                    }
                }

                LineLoginToken tokenObj = JsonConvert.DeserializeObject<LineLoginToken>(responseStr);
                string id_token = tokenObj.Id_token;

                var jst = new System.IdentityModel.Tokens.Jwt.JwtSecurityToken(id_token);
                LineUserProfile user = new LineUserProfile();
                user.UserId = jst.Payload.Sub;
                user.DisplayName = jst.Payload["name"].ToString();
                user.PictureUrl = jst.Payload["picture"].ToString();
                if (jst.Payload.ContainsKey("email") && !string.IsNullOrEmpty(Convert.ToString(jst.Payload["email"])))
                {
                    user.Email = jst.Payload["email"].ToString();
                }

                or.IsSuccessful = true;
                or.MessageInfo = JsonConvert.SerializeObject(user);
            }
            catch (Exception ex)
            {
                or.IsSuccessful = false;
                or.Exception = ex;
                or.MessageInfo = ex.ToString();
            }
            return or;
        }

        public bool IsSocialAccountRegister(string email,string socailPlatform) => _repository.GetAll<Account>().FirstOrDefault(x => x.Email == email && x.SocialPlatform == socailPlatform) != null;

        public Guid GetAccountId(string accountName)
        {
            return _repository.GetAll<Account>().FirstOrDefault(x => x.AccountName == accountName).AccountId;
        }

        public Account GetUser(Guid id) 
        {
            return _repository.GetAll<Account>().FirstOrDefault(x => x.AccountId == id);
        }

        public Account GetUser(string email)
        {
            return _repository.GetAll<Account>().FirstOrDefault(x => x.Email == email);
        }

        public Account GetUser(string accountName,string password)
        {
           return _repository.GetAll<Account>().FirstOrDefault(x => x.AccountName == accountName && x.Password == password);
        }
    }
    
}