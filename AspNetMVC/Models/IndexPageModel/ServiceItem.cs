using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspNetMVC.Models
{
    public class ServiceItem
    {
        public string ImgUrl { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }

        public string Url { get; set; }
    }

    public class ServiceItems
    {
       public string Category { get; set; }

        public string IsActive { get; set; }
        public List<ServiceItem> Services { get; set; }

    }
}