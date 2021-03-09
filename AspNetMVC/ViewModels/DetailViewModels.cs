using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspNetMVC.ViewModels
{
    public class DetailViewModel
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

    public class CommentViewModel
    {
        public Guid CommentId { get; set; }
        public string AccountName { get; set; }
        public int Star { get; set; }
        public string Content { get; set; }
        public DateTime CreateTime { get; set; }
    }

    public class CommentDataModel
    {
        public int PackageProductId { get; set; }
        public int StarCount { get; set; }
        public string Comment { get; set; }
    }
}