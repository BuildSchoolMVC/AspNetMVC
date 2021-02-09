using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AspNetMVC.ViewModel
{
    public class ProductPageListViewModel
    {
        public int PackageProductId { get; set; }
        [Display(Name = "服務名稱")]
        public string Name { get; set; }
        [Display(Name = "清潔空間類型")]
        public string RoomType { get; set; }
        [Display(Name = "清潔項目")]
        public string ServiceItem { get; set; }
        [Display(Name = "清潔空間大小")]
        public string Squarefeet { get; set; }
        [Display(Name = "清潔時數")]
        public decimal PackagePrice { get; set; }
        public string Url { get; set; }
    }
}