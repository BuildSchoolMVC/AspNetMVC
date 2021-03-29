using AspNetMVC.Models.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AspNetMVC.ViewModels {
	public class DataViewModel {
		public Guid FavoriteId { get; set; }
		public bool IsPackage { get; set; }
		public PackageProduct Package { get; set; }
		public List<UserDefinedProduct> UserDefinedList { get; set; }
		public IEnumerable<RoomType> RoomTypeList { get; set; }
		public IEnumerable<SquareFeet> SquareFeetList { get; set; }
	}
	public class SuccessViewModel : DataViewModel {
		public DateTime DateService { get; set; }
		public string Address { get; set; }
		public decimal DiscountAmount { get; set; }
		public decimal FinalPrice { get; set; }
		public int MyProperty { get; set; }
	}
	public class UserForm {
		public string FavoriteId { get; set; }
		public string DateService { get; set; }
		public string FullName { get; set; }
		public string Phone { get; set; }
		public string Email { get; set; }
		public string County { get; set; }
		public string District { get; set; }
		public string Address { get; set; }
		public string Remark { get; set; }
		public string InvoiceType { get; set; }
		public string InvoiceDonateTo { get; set; }
		public string CouponDetailId { get; set; }
	}
	public class ECPayForm {
		public string CheckMacValue { get; set; }
		public string ChoosePayment { get; set; }
		public string ClientBackURL { get; set; }
		public string EncryptType { get; set; }
		public string ItemName { get; set; }
		public string MerchantID { get; set; }
		public string MerchantTradeDate { get; set; }
		public string MerchantTradeNo { get; set; }
		public string OrderResultURL { get; set; }
		public string PaymentType { get; set; }
		public string ReturnURL { get; set; }
		public string TotalAmount { get; set; }
		public string TradeDesc { get; set; }
	}
	public class OrderData {
		public string AccountName;
		public Guid FavoriteId;
		public Guid? CouponDetailId;
		public decimal FinalPrice;
		public string MerchantTradeNo;
		public DateTime Now;
	}
	public class CouponJson {
		public Guid CouponDetailId { get; set; }
		public string CouponName { get; set; }
		public decimal DiscountAmount { get; set; }
		public string DateEnd { get; set; }
		public int State { get; set; }
	}
}