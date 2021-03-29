using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AspNetMVC.Models.Entity
{
    public class Comment : BaseEntity
    {
        [Key]
        public Guid CommentId { get; set; }
       
        public Guid AccountId { get; set; }

        public int PackageProductId { get; set; }

        public int Star { get; set; }

        public string Content { get; set; }
    }
}