﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspNetMVC.Models.Entity
{
    public class Coupon : BaseEnity
    {
        public int CouponID { get; set; }

        public string CouponName { get; set; }

        public decimal DiscountAmount { get; set; }

        public DateTime DateStart { get; set; }

        public DateTime DateEnd { get; set; }

    }
}