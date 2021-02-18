using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspNetMVC.Models.Entity
{
    public class MemberMd : BaseEnity
    {
        
        public string Name { get; set; }
        public int CreditNumber { get; set; }
        public DateTime ExpiryDate { get; set; }
        public int SafeNum { get; set; }
    }
}