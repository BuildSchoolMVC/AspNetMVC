using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspNetMVC.Models.ProductPage
{
    public class SingleProduct
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string RoomType { get; set; }
        public string ServiceItem { get; set; }
        public string Squarefeet { get; set; }
        public float Hour { get; set; }
        public decimal UnitPrice { get; set; }
        public string Url { get; set; }
        public string Descruption { get; set; }
    }
}