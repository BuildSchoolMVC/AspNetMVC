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
}