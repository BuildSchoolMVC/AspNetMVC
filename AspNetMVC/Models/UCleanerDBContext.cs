using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using AspNetMVC.Models.Entity;
using AspNetMVC.Models;

namespace AspNetMVC.Models
{
    public partial class UCleanerDBContext :DbContext
    {
        public UCleanerDBContext() : base("name=UCleanerDbContext")
        {
        }

        public virtual DbSet<CustomerService> CustomerServices { get; set; } //在此註冊資料表
        public virtual DbSet<PackageProduct> PackageProducts { get; set; }
        public virtual DbSet<RoomType> RoomTypes { get; set; }
        public virtual DbSet<ServiceItem> ServiceItems { get; set; }
        public virtual DbSet<SingleProduct> SingleProducts { get; set; }
        public virtual DbSet<SquareFeet> SquareFeets { get; set; }
        public virtual DbSet<UserDefinedProduct> UserDefinedProducts{ get; set; }
        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}