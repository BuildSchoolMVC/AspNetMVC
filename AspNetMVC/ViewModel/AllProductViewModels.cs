using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AspNetMVC.ViewModel
{
    public class PackageProductGridViewModel
    {
        public string Name { get; set; }
        public string PhotoUrl { get; set; }
    }

    public class ProductPageListViewModel
    {

        public string Name { get; set; }
        public string RoomType { get; set; }
        public string RoomType2 { get; set; }
        public string RoomType3 { get; set; }

        public string ServiceItem { get; set; }
        public string Squarefeet { get; set; }
        public string Squarefeet2 { get; set; }
        public string Squarefeet3 { get; set; }
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

        public string Name { get; set; }
        public string RoomType { get; set; }
        public string ServiceItem { get; set; }
        public string Squarefeet { get; set; }
        public float Hour { get; set; }
        public decimal Price { get; set; }
        public string PhotoUrl { get; set; }


    }

}