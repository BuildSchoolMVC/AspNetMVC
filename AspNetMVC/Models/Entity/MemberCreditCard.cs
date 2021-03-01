using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AspNetMVC.Models.Entity
{
    public class MemberCreditCard : BaseEntity
    {
        
        [MinLength(16)]
        [MaxLength(16)]
        [Required]
        [Key]
        public string CreditNumber { get; set; }
        public string AccountName { get; set; }
        [Required]
        public int ExpiryDate { get; set; }
    }
}