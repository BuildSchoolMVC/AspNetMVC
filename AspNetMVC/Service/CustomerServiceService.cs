using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AspNetMVC.Repository;
using AspNetMVC.ViewModel;
using AspNetMVC.Models.CustomerService;

namespace AspNetMVC.Service
{
    public class CustomerServiceService
    {
        private readonly CustomerServiceRepository _repository;

        public CustomerServiceService()
        {
            _repository = new CustomerServiceRepository();
        }

        public List<CustomerService> ShowData()
        {
            return _repository.GetAll().ToList();
        }

        public void AddData(CustomerViewModel c)
        {
            _repository.CreateData(c);
        }

        public CustomerService ShowCustomerInfo(int? id)
        {
            return _repository.ReadContent(id);
        }
    }
}