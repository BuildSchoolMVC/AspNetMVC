﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;



namespace AspNetMVC.ViewModels
{
    public class CouponViewModel
    {
        public int CouponId { get; set; }
        public string CouponName { get; set; }

        public decimal DiscountAmount { get; set; }

        public DateTime DateStart { get; set; }

        public DateTime DateEnd { get; set; }
        /// <summary>
        /// 優惠卷狀態
        /// </summary>
        //0 未使用
        //1 已使用
        //2 已過期
        public int Status { get; set; }

    }
}