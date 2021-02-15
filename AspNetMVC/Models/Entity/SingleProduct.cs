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
        public string ProductId { get; set; }
        public string Name { get; set; }
        public int RoomType { get; set; }
        public string ServiceItem { get; set; }
        public int Squarefeet { get; set; }
        public float Hour { get; set; }
        public decimal Price { get; set; }
        public string PhotoUrl { get; set; }
    }
}