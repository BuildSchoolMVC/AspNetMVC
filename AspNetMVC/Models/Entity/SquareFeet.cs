using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using AspNetMVC.Models.Entity;

namespace AspNetMVC.Models.Entity
{
    public class SquareFeet:BaseEnity
    {
        [Key]
        public int SquareFeetId { get; set; }
        [Display(Name = "空間大小")]
        public string SquareFeetValue { get; set; }
    }
}