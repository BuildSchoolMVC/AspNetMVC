using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using AspNetMVC.Models.Entity;

namespace AspNetMVC.Models.Entity
{
    public class RoomType :BaseEnity
    {
        [Key]
        public int RoomTypeId { get; set; }
        [Display(Name = "空間類型")]
        public string Name { get; set; }
        
    }
}