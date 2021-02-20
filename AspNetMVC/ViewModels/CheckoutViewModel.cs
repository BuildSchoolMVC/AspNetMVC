using AspNetMVC.Models.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AspNetMVC.ViewModels {
	public class DataViewModel {
		[Required]
		public bool IsPackage { get; set; }
		public PackageProduct Package { get; set; }
		public List<UserDefinedProduct> UserDefinedList { get; set; }
		public IEnumerable<RoomType> RoomTypeList { get; set; }
		public IEnumerable<SquareFeet> SquareFeetList { get; set; }
	}
}