using AspNetMVC.Models.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace AspNetMVC.ViewModel
{
    public class MemberCenterViewModels
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Mail { get; set; }
        public string Address { get; set; }
        public MemberCenterOrder order { get; set; }
        public MemberCenterCredit credit { get; set; }
        public Favorites favorites { get; set; }
        public Account account { get; set; }
        public MemberCenterPassword password { get; set; }
    }
    public class MemberCenterPassword
    {
        public string Password { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }

    public class MemberCenterOrder
    { 
        public Array ReservationOrder { get; set; }
        public Array FinishOrder { get; set; }
    }

    public class MemberCenterCredit
    { 
        public int CreditNumber { get; set; }
        public DateTime ExpiryDate { get; set; }
        public int SafeNum { get; set; }
    }
    public class Favorites
    { 
        public Array _Favorites { get; set; }
    }
}