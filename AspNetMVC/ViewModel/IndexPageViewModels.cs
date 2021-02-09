using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspNetMVC.ViewModel
{
    public class AllServiceCard
    {
        public string Title { get; set; }
        public string Url { get; set; }
        public string Icon { get; set; }
        public string Content { get; set; }
    }
    public class Banner
    {
        public int Id { get; set; }
        public List<string> Slogan { get; set; }
        public string Title { get; set; }
    }
    public class ImageCard
    {
        public string Name { get; set; }
        public string Photo { get; set; }
        public string Content { get; set; }
        public decimal StarCount { get; set; }
        public string AnimationDelay { get; set; }
    }
    public class NewsCard
    {
        public string Alt { get; set; }
        public string Url { get; set; }
        public string ImgUrl { get; set; }
        public string Content { get; set; }
        public string Delay { get; set; }
        public string Tag { get; set; }
    }

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