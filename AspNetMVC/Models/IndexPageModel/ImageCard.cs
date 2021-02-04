using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspNetMVC.Models
{
    public class ImageCard
    {
        public string Name { get; set; }
        public string Photo { get; set; }
        public string Content { get; set; }
        public decimal StarCount { get; set; }
        public string AnimationDelay { get; set; }
    }
}