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
        public MemberCenterViewModels GetMember(string accountname) {
            var source = _repository.GetAll<Account>().FirstOrDefault(x => x.AccountName == accountname);
            var result = new MemberCenterViewModels()
            {
                Name = source.AccountName,
                Phone = source.Phone,
                Mail = source.Email,
                Address = source.Address
            };
            return result;
        }
    }
}