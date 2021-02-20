using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AspNetMVC.Models.Entity
{
    public class UserDefinedProduct :BaseEntity
    {
        [Key]
        public Guid UserDefinedProductId { get; set; }
        public Guid UserDefinedId { get; set; }
        public string AccountName { get; set; }
        public string Name { get; set; }
        public int RoomType { get; set; }
        public string ServiceItems { get; set; }
        public int Squarefeet { get; set; }
        public float Hour { get; set; }
        public decimal Price { get; set; }
    }
}