namespace AspNetMVC.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Accounts
    {
        [Key]
        public Guid AccountId { get; set; }

        [Required]
        [StringLength(30)]
        public string AccountName { get; set; }

        [Required]
        public string Password { get; set; }

        public int? Gender { get; set; }

        [Required]
        [StringLength(50)]
        public string Email { get; set; }

        public bool EmailVerification { get; set; }

        [StringLength(30)]
        public string Phone { get; set; }

        [StringLength(100)]
        public string Address { get; set; }

        public int Authority { get; set; }

        public string Remark { get; set; }

        public DateTime CreateTime { get; set; }

        public string CreateUser { get; set; }

        public DateTime EditTime { get; set; }

        public string EditUser { get; set; }
    }
}
