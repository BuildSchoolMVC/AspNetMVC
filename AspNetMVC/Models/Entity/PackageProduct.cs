using AspNetMVC.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AspNetMVC.Models.Entity
{
    public class PackageProduct : BaseEntity, IProduct
    {
        [Key]
        [Display(Name = "套裝服務ID")]
        public int PackageProductId { get; set; }
        public string Name { get; set; }
        public int RoomType { get; set; }
        public int RoomType2 { get; set; }
        public int? RoomType3 { get; set; }
        public string ServiceItem { get; set; }
        public int Squarefeet { get; set; }
        public int Squarefeet2 { get; set; }
        public int? Squarefeet3 { get; set; }
        public float Hour { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string PhotoUrl { get; set; }
    }
}