using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AspNetMVC.Models.Entity
{
    public class CustomerService : BaseEnity
    {
        [Key]
        [Display(Name = "客服資料ID")]
        public Guid CustomerServiceId { get;set;}

        [Display(Name = "名稱")]
        [StringLength(30)]
        public string Name { get; set; }

        [Display(Name = "信箱")]
        [StringLength(50)]
        public string Email { get; set; }

        [Display(Name = "電話")]
        [StringLength(20)]
        public string Phone { get; set; }

        [Display(Name = "分類")]
        public int Category { get; set; }
        [Display(Name = "內容")]
        [StringLength(100)]
        public string Content { get; set; }

        [Display(Name = "閱讀狀態")]
        public bool IsRead { get; set; }
    }
}