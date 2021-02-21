using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspNetMVC.Models.Entity {
	public class CouponDetail : BaseEntity {
		[Key]
		public Guid CouponDetailId { get; set; }
		public int CouponId { get; set; }
		public string AccountName { get; set; }
		public int State { get; set; }
		//0 未使用
		//1 已使用
		//2 已過期
	}
	public enum UseState {
		Unused,
		Used,
		Expired,
	}
}