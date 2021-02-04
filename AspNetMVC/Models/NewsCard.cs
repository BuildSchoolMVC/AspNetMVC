using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspNetMVC.Models
{
    public class NewsCard
    {
        public string Alt { get; set; }
        public string Url { get; set; }
        public string ImgUrl { get; set; }
        public string Content { get; set; }
        public string Delay { get; set; }
        public string Tag { get; set; }
    }
}