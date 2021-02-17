using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AspNetMVC.Repository;
using AspNetMVC.ViewModels;
using AspNetMVC.Models;
using AspNetMVC.Models.Entity;

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

            try
            {
                _repository.Create(new Account
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
                });
                _context.SaveChanges();
                result.IsSuccessful = true;
            }
            catch(Exception ex)
            {
                result.IsSuccessful = false;
                result.Exception = ex;
            }
        }

        /// <summary>
        /// 檢查此帳號是否存在
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool AccountIsExist(string name)
        {
            var user = _repository.GetAll<Account>().FirstOrDefault(x => x.AccountName == name);

            return user != null;
        }

        /// <summary>
        /// 檢查此信箱是否存在
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public bool EmailIsExist(string email)
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
        public string EmailActivation(Guid id)
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
                    result.IsSuccessful = true;
                }
                else
                {
                    result.IsSuccessful = false;
                    result.MessageInfo = "查無此人";
                }
            }
            catch (Exception ex)
            {
                result.IsSuccessful = false;
                result.Exception = ex;
                result.MessageInfo = "發生錯誤";
            }

            return result;
        }

        public Guid GetAccountId(string accountName)
        {
            return _repository.GetAll<Account>().FirstOrDefault(x => x.AccountName == accountName).AccountId;
        }

        public Account GetUser(Guid id) 
        {
            return _repository.GetAll<Account>().FirstOrDefault(x => x.AccountId == id);
        }
    }
    
}