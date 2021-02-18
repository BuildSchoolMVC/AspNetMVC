using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AspNetMVC.Models.Entity
{
    public class MemberMd : BaseEntity
    {
        [Key]
        public Guid AccountId { get; set; }
        public string Name { get; set; }
        public int CreditNumber { get; set; }
        public DateTime ExpiryDate { get; set; }
        public int SafeNum { get; set; }
    }
}