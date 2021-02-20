using AspNetMVC.Repository;
using AspNetMVC.ViewModel;
using AspNetMVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AspNetMVC.Repository;

namespace AspNetMVC.Services
{
    public class CouponService
    {
        public List<CouponViewModel> CouponData { get; set; }

        public CouponService()
        {
            CouponData = new CouponRepository().CreateCouponList();
        }
    }

}