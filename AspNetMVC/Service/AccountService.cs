using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security;
using System.Security.Cryptography;
using AspNetMVC.Repository;
using AspNetMVC.ViewModel;
using AspNetMVC.Models;
using AspNetMVC.Models.Entity;
using System.Text;

namespace AspNetMVC.Service
{
    public class AccountService
    {
        private readonly UCleanerDBContext _context;
        private readonly BaseRepository<Account> _accountRepository;

        public AccountService(){
            _context = new UCleanerDBContext();
            _accountRepository = new BaseRepository<Account>(_context);
        }

        public void CreateAccount(RegisterViewModel account)
        {
            _accountRepository.Create(new Account
            {
                AccountId = Guid.NewGuid(),
                AccountName = account.Name,
                Address = account.Address,
                Password = ToMD5(account.Password),
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
        }

        /// <summary>
        /// 檢查此帳號是否存在
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool AccountIsExist(string name)
        {
            var user = _accountRepository.GetAll().ToList().FirstOrDefault(x => x.AccountName == name);

            return user != null;
        }
        /// <summary>
        /// 檢查此信箱是否存在
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public bool EmailIsExist(string email)
        {
            var user = _accountRepository.GetAll().ToList().FirstOrDefault(x => x.Email == email);

            return user != null;
        }

        /// <summary>
        /// 判斷帳密是否完全符合
        /// </summary>
        /// <param name="accountName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool LoginIsValid(string accountName, string password)
        {
            var user = _accountRepository.GetAll().ToList().FirstOrDefault(x => x.AccountName == accountName && x.Password == ToMD5(password));
            return user != null;
        }

        public string ToMD5(string strings)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] bytes = Encoding.Default.GetBytes(strings);//將要加密的字串轉換為位元組陣列
            byte[] encryptdata = md5.ComputeHash(bytes);//將字串加密後也轉換為字元陣列
            return Convert.ToBase64String(encryptdata);//將加密後的位元組陣列轉換為加密字串
        }
    }
    
}