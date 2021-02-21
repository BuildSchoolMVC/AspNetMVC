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
    }
}