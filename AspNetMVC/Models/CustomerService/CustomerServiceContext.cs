using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace AspNetMVC.Models.CustomerService
{
    public partial class CustomerServiceContext :DbContext
    {
        public CustomerServiceContext() : base("CustomerService")
        {

        }

        public DbSet<CustomerService> CustomerServices { get; set; }
    }
}