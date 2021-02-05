using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace AspNetMVC.Models.CustomerService
{
    public partial class CustomerServiceContext :DbContext
    {
        public CustomerServiceContext() : base("name=CustomerServiceConn")
        {
        }

        public virtual DbSet<CustomerService> CustomerServices { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}