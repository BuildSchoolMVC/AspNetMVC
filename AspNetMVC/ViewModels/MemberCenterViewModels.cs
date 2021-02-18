using System;
using System.Collections.Generic;
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
        public string Password { get; set; }
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
        public Array Favorites { get; set; }
    }
}