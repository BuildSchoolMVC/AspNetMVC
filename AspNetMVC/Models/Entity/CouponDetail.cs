using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspNetMVC.Models.Entity
{
    public class CouponDetail : BaseEnity
    {
        public int CouponID { get; set; }

        public int CustomerID { get; set; }

        public DateTime DateGet { get; set; }

        public int State { get; set; }

        public string CouponType { get; set; }

    }
}