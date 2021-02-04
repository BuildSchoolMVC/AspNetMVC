using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AspNetMVC.Models.CustomerService
{
    public class CustomerService
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int Category { get; set; }
        public string Content { get; set; }
        public DateTime? CreatedTime { get; set; }
        public bool IsRead { get; set; }
    }
}