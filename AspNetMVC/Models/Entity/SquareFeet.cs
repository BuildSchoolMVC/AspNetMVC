using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using AspNetMVC.Models.Entity;

namespace AspNetMVC.Models.Entity
{
    public class SquareFeet:BaseEntity
    {
        [Key]
        public int SquareFeetId { get; set; }
        public string Name { get; set; }
        public int Value { get; set; }

    }
}