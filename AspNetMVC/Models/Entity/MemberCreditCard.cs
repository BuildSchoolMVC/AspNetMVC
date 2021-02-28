using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AspNetMVC.Models.Entity
{
    public class MemberCreditCard : BaseEntity
    {
        [Key]
        public string AccountName { get; set; }
        [MinLength(16)]
        [MaxLength(16)]
        [Required]
        public string CreditNumber { get; set; }
        [Required]
        public int ExpiryDate { get; set; }
    }
}