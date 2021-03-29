using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AspNetMVC.Models.Entity {
	public class OrderUserDefined : BaseEntity {
		[Key]
		public int Id { get; set; }
		[Required]
		public Guid OrderId { get; set; }
		[Required]
		public int RoomType { get; set; }
		[Required]
		public int SquareFeet { get; set; }
		[Required]
		public string ServiceItems { get; set; }
		[Required]
		public float Hour { get; set; }
		[Required]
		public decimal Price { get; set; }
	}
}