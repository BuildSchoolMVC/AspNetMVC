using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspNetMVC.Models.Entity
{
    public class MemberMd : BaseEnity
    {
        public string name { get; set; }
        public int creditNumber { get; set; }
        public DateTime expiryDate { get; set; }
        public int safeNum { get; set; }
    }
}