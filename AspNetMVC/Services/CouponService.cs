using AspNetMVC.Repository;
using AspNetMVC.ViewModel;
using AspNetMVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspNetMVC.Services
{
    public class CouponService
    {
        public List<CouponViewModel> CouponData { get; set; }

        public CouponService()
        {
            CouponData = new CouponRepository().CreateCouponList();
        }

        //public List<CouponViewModel> GetAll()
        //{
        //    return new List<CouponViewModel>
        //    {
        //        coupons = coupons.Where(x => x.Status == 0).ToList();
        //        ViewBag.coupondata = coupons;

        //        coupons = coupons.Where(x => x.Status == 1).ToList();
        //        ViewBag.coupondata = coupons;

        //        coupons = coupons.Where(x => x.Status == 2).ToList();
        //        ViewBag.coupondata = coupons;
        //};
        
    }

}