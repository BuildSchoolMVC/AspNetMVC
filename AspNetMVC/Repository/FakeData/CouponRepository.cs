using AspNetMVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspNetMVC.Repository
{
    public class CouponRepository
    {
        public List<CouponViewModel> CreateCouponList()
        {
            return new List<CouponViewModel>
            {
               new CouponViewModel{CouponName="Anderson",DiscountAmount=100m, DateStart=new DateTime(2021,01,01),DateEnd=new DateTime(2021,12,31)},
               new CouponViewModel{CouponName="Anderson",DiscountAmount=100m, DateStart=new DateTime(2021,01,01),DateEnd=new DateTime(2022,12,31)},
               new CouponViewModel{CouponName="Anderson",DiscountAmount=100m, DateStart=new DateTime(2021,01,01),DateEnd=new DateTime(2023,12,31)},
            };
        }


    }
}