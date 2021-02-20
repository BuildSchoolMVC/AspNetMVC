﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AspNetMVC.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "帳號")]
        public string AccountName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "密碼")]
        public string Password { get; set; }

        [Display(Name = "記住我?")]
        public bool RememberMe { get; set; }

        public string ValidationMessage { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "電子郵件")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "密碼")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "確認密碼")]
        public string ConfirmPassword { get; set; }

        [Display(Name="名稱")]
        public string Name { get; set; }

        [Display(Name="性別")]
        public int Gender { get; set; }

        [Display(Name = "住址")]
        public string Address { get; set; }

        [Display(Name = "電話")]
        public string Phone { get; set; }
        public string ValidationMessage { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "信箱")]
        public string Email { get; set; }

        [Display(Name = "帳號名稱")]
        public string AccountName { get; set; }
    }

    public class ResetPasswordViewModel 
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "新密碼")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "確認密碼")]
        public string ConfirmPassword { get; set; }
    }

    public class NewPasswordViewModel 
    { 
        public Guid AccountId { get; set; }
        public string NewPassword { get; set; }
    }

    public class GoogleApiTokenInfo
    {
        public string Email { get; set; }
        public string Locale { get; set; }
        public string Name { get; set; }
        public string Sub { get; set; }
    }

    public class FacebookInfo
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string FacebookId { get; set; }
    }
}
