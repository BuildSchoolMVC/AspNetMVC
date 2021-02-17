using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspNetMVC.ViewModel
{
    public class MemberCenterViewModels
    {
        public int name { get; set; }
        public int phone { get; set; }
        public string mail { get; set; }
        public string address { get; set; }
        public string password { get; set; }
    }

    public class MemberCenterOrder
    { 
        public Array reservationOrder { get; set; }
        public Array finishOrder { get; set; }
    }

    public class MemberCenterCredit
    { 
        public int creditNumber { get; set; }
        public DateTime expiryDate { get; set; }
        public int safeNum { get; set; }
    }
    public class Favorites
    { 
        public Array favorites { get; set; }
    }
}