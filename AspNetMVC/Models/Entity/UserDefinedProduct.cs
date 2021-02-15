using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AspNetMVC.Models.Entity
{
    public class UserDefinedProduct :BaseEnity
    {
        [Key]
        [Display(Name = "客製化組合GUID")]
        public int UserDefinedGUID { get; set; }
        [Display(Name = "會員Id")]
        public int MemberId { get; set; }
        [Display(Name = "總服務名稱")]
        public string Name { get; set; }
        [Display(Name = "總服務空間類型")]
        public string RoomType { get; set; }
        [Display(Name = "總服務項目")]
        public string ServiceItem { get; set; }
        [Display(Name = "總服務空間大小")]
        public string Squarefeet { get; set; }
        [Display(Name = "總服務時數")]
        public float Hour { get; set; }

        public decimal Price { get; set; }

        public string Url { get; set; }
    }
}