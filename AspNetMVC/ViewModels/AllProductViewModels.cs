﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AspNetMVC.ViewModels
{


    public class ProductPageViewModel
    {
        public int PackageProductId { get; set; }
        public string Title { get; set; }
        public int RoomType { get; set; }
        public int RoomType2 { get; set; }
        public int? RoomType3 { get; set; }

        public string ServiceItem { get; set; }
        public int Squarefeet { get; set; }
        public int Squarefeet2 { get; set; }
        public int? Squarefeet3 { get; set; }
        public float Hour { get; set; }
        public decimal Price { get; set; }
        public string PhotoUrl { get; set; }
    }

    public class SingleProductViewModel
    {

        public string Name { get; set; }
        public string ServiceItem { get; set; }
        public string Squarefeet { get; set; }
        public float Hour { get; set; }
        public decimal Price { get; set; }

        public string PhotoUrl { get; set; }
    }

    public class UserDefinedAllViewModel
    {
        public Guid UserDefinedId { get; set; }
        public string Name { get; set; }
        public string RoomType { get; set; }
        public string ServiceItem { get; set; }
        public string Squarefeet { get; set; }
        public float Hour { get; set; }
        public decimal Price { get; set; }


    }

}