using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace AspNetMVC.ViewModel
{
    public class PackageProductGridViewModel
    {
        [Display(Name = "套裝服務名稱")]
        public string Name { get; set; }
        public string Url { get; set; }
    }
}