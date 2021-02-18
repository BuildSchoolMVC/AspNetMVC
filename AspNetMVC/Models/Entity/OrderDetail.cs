using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AspNetMVC.Models.Entity {
	public class OrderDetail : BaseEntity {
		[Key]
		[Required]
		public Guid OrderDetailId { get; set; }
		[Required]
		public Guid OrderId { get; set; }

		public Guid? UserDefinedId { get; set; }

		public int? PackageProductId { get; set; }
		[Required]
		public decimal ProductPrice { get; set; }
		[Required]
		public string ProductName { get; set; }
		[Required]
		public bool IsPakage { get; set; }
	}
}