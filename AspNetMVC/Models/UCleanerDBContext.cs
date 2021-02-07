﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using AspNetMVC.Models.Entity;

namespace AspNetMVC.Models
{
    public partial class UCleanerDBContext :DbContext
    {
        public UCleanerDBContext() : base("name=UCleanerDbContext")
        {
        }

        public virtual DbSet<CustomerService> CustomerServices { get; set; } //在此註冊資料表

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}