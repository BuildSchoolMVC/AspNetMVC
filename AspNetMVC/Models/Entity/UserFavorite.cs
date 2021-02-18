using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AspNetMVC.Models.Entity {
	public class UserFavorite : BaseEntity {
		[Key]
		public Guid FavoriteId { get; set; }
		[Required]
		public Guid AccountId { get; set; }

		public Guid? UserDefinedId { get; set; }

		public int? PackageProductId { get; set; }

		[Required]
		public bool IsPakage { get; set; }
		[Required]
		public bool IsDelete { get; set; }
	}
}