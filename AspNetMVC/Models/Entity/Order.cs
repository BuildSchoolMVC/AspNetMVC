using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AspNetMVC.Models.Entity {
	public class Order : BaseEntity {
		[Key]
		public int OrderId { get; set; }

        [Required]
        public Guid AccountId { get; set; }

        public int FavoriteId { get; set; }

        [Required]
        public DateTime DateCreated { get; set; }

        [Required]
        public DateTime DateService { get; set; }

        [Required]
        public DateTime TimeService { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public OrderState OrderState { get; set; }

        public byte Rate { get; set; }

        public string Comment { get; set; }

        public int CouponID { get; set; }

        [Required]
        public PaymentMethod PaymentMethod { get; set; }

        [Required]
        public InvoiceType InvoiceType { get; set; }

        public InvoiceDonateTo InvoiceDonateTo { get; set; }

    }
    public enum OrderState
    {
        //待付款
        PendingPayment,
        //已付款
        AlreadyPaid,
        //已取消
        Cancelled,
    }
    public enum PaymentMethod
    {
        CreditCard,
        ATM,
    }
    public enum InvoiceType
    {
        //個人電子發票
        Personal,
        //載具發票
        Carrier,
        //捐贈
        Donate,
    }
    public enum InvoiceDonateTo
    {
        //中華民國唐氏症基金會
        DownSyndrome,
        //陽光社會福利基金會
        SunshineSocial,
        //台灣兒童暨家庭扶助基金會
        ChildrenFamilies,
    }
}