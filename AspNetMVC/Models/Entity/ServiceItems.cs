using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using AspNetMVC.Models.Entity;

namespace AspNetMVC.Models.Entity
{
    public class ServiceItems :BaseEnity
    {
        [Key]
        public int ServiceitemId { get; set; }
        [Display(Name = "服務項目")]
        public string Name { get; set; }
    }
}