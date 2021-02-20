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
        [Display(Name = "帳號ID")]
        public Guid AccountId { get; set; }

        [Display(Name = "帳號名稱")]
        [StringLength(200)]
        [Required]
        public string AccountName { get; set; }

        [Display(Name = "帳號密碼")]
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "性別")]
        public int? Gender { get; set; }

        [Display(Name = "信箱")]
        [StringLength(50)]
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "信箱驗證狀態")]
        public bool EmailVerification { get; set; }
        
        [Display(Name = "電話")]
        [StringLength(30)]
        public string Phone { get; set; }
        
        [Display(Name = "住址")]
        [StringLength(100)]
        public string Address { get; set; }

        [Display(Name = "操作權限")]
        public int Authority { get; set; }

        [Display(Name = "備註")]
        public string Remark { get; set; }

    }
}