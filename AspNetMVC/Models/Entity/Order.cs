using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AspNetMVC.Models.Entity {
	public class Order : BaseEnity {
		[Key]
		public Guid OrderId { get; set; }

		[Required]
		public Guid AccountId { get; set; }

		[Required]
		public DateTime DateService { get; set; }

		[Required]
		public string Address { get; set; }

		[Required]
		public byte OrderState { get; set; }
		//待付款 0
		//已付款 1
		//已取消 2

		public byte Rate { get; set; }

		public string Comment { get; set; }

		public Guid CouponID { get; set; }

		[Required]
		public byte PaymentMethod { get; set; }
		//信用卡 0
		//ATM 1

		[Required]
		public byte InvoiceType { get; set; }
		//個人電子發票 0
		//捐贈 1
		//載具發票 2

		public byte InvoiceDonateTo { get; set; }
		//中華民國唐氏症基金會 0
		//陽光社會福利基金會 1
		//台灣兒童暨家庭扶助基金會 2

	}
	//public enum OrderState {
	//  //
	//	PendingPayment,
	//	//
	//	AlreadyPaid,
	//	//
	//	Cancelled,
	//}
	//public enum PaymentMethod {
	//  //
	//	CreditCard,
	//	//
	//	ATM,
	//}
	//public enum InvoiceType {
	//	//
	//	Personal,
	//	//
	//	Carrier,
	//	//
	//	Donate,
	//}
	//public enum InvoiceDonateTo {
	//	
	//	DownSyndrome,
	//	
	//	SunshineSocial,
	//	
	//	ChildrenFamilies,
	//}
}