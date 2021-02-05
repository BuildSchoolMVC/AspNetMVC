using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AspNetMVC.Repository;
using AspNetMVC.ViewModel;

namespace AspNetMVC.Service
{
    public class CustomerServiceService
    {
        private readonly CustomerServiceRepository _repository;

        public CustomerServiceService()
        {
            _repository = new CustomerServiceRepository();
        }

        public List<CustomerViewModel> ShowData()
        {
            return _repository.GetAll().Select(x => new CustomerViewModel()
            {
                Name = x.Name,
                Email = x.Email,
                Category = x.Category,
                Content = x.Content,
                Phone = x.Phone
            }).ToList();
        }

        public void AddData(CustomerViewModel c)
        {
            _repository.CreateData(c);
        }
    }
}