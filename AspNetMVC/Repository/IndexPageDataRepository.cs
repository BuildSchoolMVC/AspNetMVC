using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspNetMVC.Repository
{
    public class IndexPageDataRepository
    {
        public ImageCardRepository imageCardData { get; set; }

        public NewsCardRepository newsCardData { get; set; }

        public IndexPageDataRepository()
        {

        }
    }
}