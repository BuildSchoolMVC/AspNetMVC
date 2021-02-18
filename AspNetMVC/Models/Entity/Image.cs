using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspNetMVC.Models.Entity
{
    public class Image : BaseEntity
    {
        public Guid ImageId { get; set; }
        public string Url { get; set; }
        public string Name { get; set; }
        public string Size { get; set; }
        public string Type { get; set; }
    }
}