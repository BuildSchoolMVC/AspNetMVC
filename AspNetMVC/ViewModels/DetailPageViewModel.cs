using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspNetMVC.ViewModels
{
    public class DetailPageViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string RoomName { get; set; }
        public string RoomName2 { get; set; }
        public string RoomName3 { get; set; }
        public string ServiceItem { get; set; }
        public string SquarefeetName { get; set; }
        public string SquarefeetName2 { get; set; }
        public string SquarefeetName3 { get; set; }
        public float Hour { get; set; }
        public string Description { get; set; }
        public string Price { get; set; }
        public string PhotoUrl { get; set; }
    }
}