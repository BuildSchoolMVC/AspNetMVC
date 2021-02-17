﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AspNetMVC.Models.Entity
{
    public class UserDefinedProduct :BaseEntity
    {
        [Key]
        public Guid UserDefinedId { get; set; }
        public int MemberId { get; set; }
        public string Name { get; set; }
        public string RoomType { get; set; }
        public string ServiceItem { get; set; }
        public string Squarefeet { get; set; }
        public float Hour { get; set; }
        public decimal Price { get; set; }//待刪
        public string PhotoUrl { get; set; }//待刪
    }
}