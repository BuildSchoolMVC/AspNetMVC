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
        public virtual DbSet<PackageProduct> PackageProduct { get; set; }
        public virtual DbSet<RoomType> RoomType { get; set; }
        public virtual DbSet<ServiceItems> ServiceItems { get; set; }
        public virtual DbSet<SingleProduct> SingleProduct { get; set; }
        public virtual DbSet<SquareFeet> SquareFeet { get; set; }
        public virtual DbSet<UserDefinedProduct> UserDefinedProduct { get; set; }
        public virtual DbSet<Account> Accounts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}