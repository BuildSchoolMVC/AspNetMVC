using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AspNetMVC.Models.Entity
{
    public class OrderDetail : BaseEnity
    {
        [Key]
        [Required]
        public int OrderId { get; set; }
        [Required]
        public int RoomTypeId { get; set; }
        [Required]
        public int ServiceItems { get; set; }
        [Required]
        public int SquareFeet { get; set; }
        [Required]
        public double Hours { get; set; }
        [Required]
        public int PackageID { get; set; }
        [Required]
        public int ProductID { get; set; }
        [Required]
        public decimal ProductPrice { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        public bool IsPakage { get; set; }
    }
}