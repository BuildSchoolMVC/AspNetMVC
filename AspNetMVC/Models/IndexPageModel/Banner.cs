using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspNetMVC.Models
{
    public class Banner
    {
        public int Id { get; set; }
        public List<string> Slogan { get; set; }
        public string Title { get; set; }
    }
}