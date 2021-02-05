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
        [Display(Name = "名稱")]
        public string Name { get; set; }
        [Display(Name = "信箱")]
        public string Email { get; set; }
        [Display(Name = "電話")]
        public string Phone { get; set; }
        [Display(Name = "分類")]
        public int Category { get; set; }
        [Display(Name = "內容")]
        public string Content { get; set; }
        [Display(Name = "創建時間")]
        public DateTime? CreatedTime { get; set; }
        [Display(Name = "閱讀狀態")]
        public bool IsRead { get; set; }
    }
}