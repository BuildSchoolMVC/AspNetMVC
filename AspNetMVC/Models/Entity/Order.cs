using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AspNetMVC.Models.Entity {
	public class Order : BaseEntity {
		[Key]
		public Guid OrderId { get; set; }

        [Required]
        public string AccountName { get; set; }

		[Required]
		public string FullName { get; set; }

		[Required]
		public string Email { get; set; }

		[Required]
		public string Phone { get; set; }

		[Required]
		public DateTime DateService { get; set; }

		[Required]
		public string Address { get; set; }

		public string Remark { get; set; }

		[Required]
		public byte OrderState { get; set; }

        public byte? Rate { get; set; }

        public string Comment { get; set; }

		public Guid? CouponId { get; set; }

		[Required]
		public byte PaymentMethod { get; set; }

		[Required]
		public byte InvoiceType { get; set; }

		public byte? InvoiceDonateTo { get; set; }

	}
	public enum OrderState {
		//待付款 0
		PendingPayment,
		//已付款 1
		AlreadyPaid,
		//已取消 2
		Cancelled,
	}
	public enum PayMethod {
		//綠界支付 0
		ECPay,
		////信用卡 0
		//CreditCard,
		////ATM 1
		//ATM,
	}
	public enum InvoiceType {
		//個人電子發票 0
		Personal,
		//捐贈 1
		Donate,
		//載具發票 2
		Carrier,
	}
	public enum InvoiceDonateTo {
		//中華民國唐氏症基金會 0
		DownSyndrome,
		//陽光社會福利基金會 1
		SunshineSocial,
		//台灣兒童暨家庭扶助基金會 2
		ChildrenFamilies,
	}
}