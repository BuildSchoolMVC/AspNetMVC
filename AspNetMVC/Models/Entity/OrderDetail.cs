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
		[Required]
		public Guid FavoriteId { get; set; }
		[Required]
		public decimal ProductPrice { get; set; } //改FinalPrice
		[Required]
		public string ProductName { get; set; }
	}
}