using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AspNetMVC.Models.Entity
{
    public class Account : BaseEntity
    {
        [Key]
        public Guid AccountId { get; set; }

        [StringLength(200)]
        [Required]
        public string AccountName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public int? Gender { get; set; }

        [StringLength(50)]
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public string EmailStatus { get; set; }

        [StringLength(30)]
        public string Phone { get; set; }
        
        [StringLength(100)]
        public string Address { get; set; }

        public int Authority { get; set; }

        public bool IsThirdParty { get; set; }

        public bool IsIntegrated { get; set; }

        public string SocialPlatform { get; set; }

        public string Remark { get; set; }

    }
}