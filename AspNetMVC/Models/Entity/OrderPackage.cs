using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AspNetMVC.Models.Entity {
	public class OrderPackage : BaseEntity {
		[Key]
		public Guid OrderId { get; set; }
		[Required]
		public string RoomTypes { get; set; }
		[Required]
		public string SquareFeets { get; set; }
		[Required]
		public string ServiceItems { get; set; }
		[Required]
		public float Hour { get; set; }
		[Required]
		public decimal Price { get; set; }
	}
}