using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AspNetMVC.ViewModel;
using AspNetMVC.Repository;
using AspNetMVC.ViewModels;
using AspNetMVC.Models;
using AspNetMVC.Models.Entity;

namespace AspNetMVC.Services
{
    public class MemberCenterService
    {
        private readonly UCleanerDBContext _context;
        private readonly BaseRepository _repository;
        public List<Account> _accounts { get; set; }

        public MemberCenterService() {
            _context = new UCleanerDBContext();
            _repository = new BaseRepository(_context);
        }
        public MemberCenterViewModels GetMember(Guid accountId) {
            var source = _repository.GetAll<MemberMd>().FirstOrDefault(x => x.AccountId == accountId);
            var result = new MemberCenterViewModels()
            {
                Name = source.Name,
                Phone = source.Phone,
                Mail = source.Mail,
                Address = source.Address
            };
            return result;
        }
        public Account GetAll(Guid accountId)
        {
            var source = _repository.GetAll<Account>().FirstOrDefault(x => x.AccountId == accountId);
            return source;
        }
        public MemberMd SaveModel(Guid accountId,MemberCenterViewModels memberVm) 
        {
            var source = _repository.GetAll<MemberMd>().FirstOrDefault(x => x.AccountId == accountId);
            source.Name = memberVm.Name;
            source.Phone = memberVm.Phone;
            source.Mail = memberVm.Mail;
            source.Address = memberVm.Address;
            _repository.Update<MemberMd>(source);
            _context.SaveChanges();
            return source;
        }
        public OperationResult EditPassword(Guid accountId,MemberCenterPassword password)
        {
            var result = new OperationResult();
            var source = _repository.GetAll<Account>().FirstOrDefault(x => x.AccountId == accountId);
            var orginPassword = password.Password;
            var Md5OrginPassword = Helpers.ToMD5(orginPassword);
            var newPassword = password.NewPassword;
            var Md5NewPassword = Helpers.ToMD5(newPassword);
            var confirmPassword = password.ConfirmPassword;
            
            if (Md5OrginPassword == source.Password)
            {
                source.Password = Md5NewPassword;
                _repository.Update<Account>(source);
                _context.SaveChanges();
                result.IsSuccessful = true;
            }
            else
            {
                result.IsSuccessful = false;
            }
            return result;
        }
    }
}