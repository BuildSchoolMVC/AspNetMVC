using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AspNetMVC.Models.Entity;
using System.Linq;
using System.Web;

namespace AspNetMVC.Models.Entity
{
    public class SingleProduct:BaseEnity
    {
        [Key]
        [Display(Name = "單一服務ID")]
        public int ProductId { get; set; }
        [Display(Name = "單一服務名稱")]
        public string Name { get; set; }
        [Display(Name = "單一服務空間類型")]
        public string RoomType { get; set; }
        [Display(Name = "單一服務項目")]
        public string ServiceItem { get; set; }
        [Display(Name = "單一服務空間大小")]
        public string Squarefeet { get; set; }
        [Display(Name = "單一服務時數")]
        public float Hour { get; set; }

        public string Desciption { get; set; }

        public decimal UnitPrice { get; set; }

        public string Url { get; set; }
    }
}