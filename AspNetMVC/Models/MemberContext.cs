using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using AspNetMVC.Models.Entity;
using AspNetMVC.Models;

namespace AspNetMVC.Models
{
    public class MemberContext : DbContext
    {
        public virtual DbSet<MemberMd> Member { get; set; }
    }
}