using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AspNetMVC.Models.Entity {
	public class ECPayParam {
		[Key]
		public int Key { get; set; }
		public string MerchantTradeNo { get; set; }
	}
}