using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AspNetMVC.Models.Entity {
	public class CouponDetail : BaseEntity {
		[Key]
		public Guid CouponDetailId { get; set; }
		public int CouponId { get; set; }
		public string AccountName { get; set; }
		public int State { get; set; }
	}
	public enum UseState {
		Unused,//0 未使用
		Used,//1 已使用
		Expired,//2 已過期
	}
}