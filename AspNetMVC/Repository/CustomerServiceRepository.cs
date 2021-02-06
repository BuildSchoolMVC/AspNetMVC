using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using AspNetMVC.Models.CustomerService;
using AspNetMVC.ViewModel;

namespace AspNetMVC.Repository
{
    public class CustomerServiceRepository
    {
        private readonly CustomerServiceContext _context;
        public CustomerServiceRepository()
        {
            _context = new CustomerServiceContext();
        }

        public IEnumerable<CustomerService> GetAll()
        {
            return _context.CustomerServices;
        }

        public void CreateData(CustomerViewModel c)
        {
            _context.CustomerServices.Add(new CustomerService
            {
                Name = c.Name,
                Phone = c.Phone,
                Email = c.Email,
                Category = c.Category,
                Content = c.Content,
                IsRead = false,
                CreatedTime = DateTime.UtcNow.AddHours(8)//轉換我們時區之時間
            });
            _context.SaveChanges();
        }
        public CustomerService ReadContent(int? id)
        {
            var customer = _context.CustomerServices.Find(id);
            customer.IsRead = true;
            _context.Entry(customer).State = EntityState.Modified;
            _context.SaveChanges();
            return customer;
        } 
    }
}