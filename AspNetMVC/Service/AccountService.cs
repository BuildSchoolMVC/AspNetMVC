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
                Gender = account.Gender,
                Phone = account.Phone,
                Authority = 3,
                CreateTime = DateTime.UtcNow.AddHours(8),
                CreateUser = account.Name,
                EditTime = DateTime.UtcNow.AddHours(8),
                EditUser = account.Name,
                Remark = ""
            });
        }
        public bool AccountIsExist(string name)
        {
            var user = _accountRepository.GetAll().ToList().FirstOrDefault(x => x.AccountName == name);
            
            if(user != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool EmailIsExist(string email)
        {
            var user = _accountRepository.GetAll().ToList().FirstOrDefault(x => x.Email == email);

            if (user != null)
            {
                return true;
            }
            else
            {
                return false;
            }
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