using AspNetMVC.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AspNetMVC.Models.Entity
{
    public class PackageProduct : BaseEnity
    {
        [Key]
        [Display(Name = "套裝服務ID")]
        public int PackageProductId { get; set; }
        [Display(Name = "套裝服務名稱")]
        public string Name { get; set; }
        [Display(Name = "套裝服務空間類型")]
        public string RoomType { get; set; }
        [Display(Name = "套裝服務所有項目")]
        public string ServiceItem { get; set; }
        [Display(Name = "套裝服務總坪數大小")]
        public string Squarefeet { get; set; }
        [Display(Name = "套裝服務總時數")]
        public float Hour { get; set; }
        [Display(Name = "套裝服務描述")]
        public string Description { get; set; }
        [Display(Name = "套裝服務價錢")]
        public decimal PackagePrice { get; set; }
        public string Url { get; set; }
    }
}