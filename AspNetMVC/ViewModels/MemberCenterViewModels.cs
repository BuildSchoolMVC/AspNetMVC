using AspNetMVC.Models.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace AspNetMVC.ViewModels {
	public class InfoModel {
		public string AccountName { get; set; }
		public string Phone { get; set; }
		public string Email { get; set; }
		public string Address { get; set; }
	}
	public class IndexModel : InfoModel {
		public int PaidCount { get; set; }
		public int CouponCount { get; set; }
	}
	public class ChangePasswordModel {
		public string OldPassword { get; set; }
		public string NewPassword { get; set; }
	}
	public class OrderBriefModel {
		public string DateService { get; set; }
		public string Address { get; set; }
		public byte OrderState { get; set; }
		public decimal FinalPrice { get; set; }
		public Guid OrderId { get; set; }
	}
	public class OrderFullModel {
		public string DateService { get; set; }
		public string Address { get; set; }
		public byte OrderState { get; set; }
		public decimal FinalPrice { get; set; }
		public string FullName { get; set; }
		public string Phone { get; set; }
		public string Email { get; set; }
		public string CreateTime { get; set; }
		public string PaymentType { get; set; }
		public byte InvoiceType { get; set; }
		public byte? InvoiceDonateTo { get; set; }
		public string ProductName { get; set; }
		public decimal DiscountAmount { get; set; }
		public byte? Rate { get; set; }
		public PackageModel PackageModel { get; set; }
		public List<UserDefinedModel> UserDefinedList { get; set; }
	}
	public class PackageModel {
		public int[] RoomTypes { get; set; }
		public int[] SquareFeets { get; set; }
		public string ServiceItems { get; set; }
		public float Hour { get; set; }
		public decimal Price { get; set; }
	}
	public class UserDefinedModel {
		public int RoomType { get; set; }
		public int SquareFeet { get; set; }
		public string ServiceItems { get; set; }
		public float Hour { get; set; }
		public decimal Price { get; set; }
	}
	public class CommentModel {
		public Guid OrderId { get; set; }
		public byte? Rate { get; set; }
		public string Comment { get; set; }
	}
}