using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AspNetMVC.Repository;
using AspNetMVC.ViewModel;

namespace AspNetMVC.Service
{
    public class IndexPageService
    {
        public List<ImageCard> ImageCardData { get; set; }

        public List<NewsCard> NewsCardData { get; set; }

        public List<Banner> BannerData { get; set; }

        public List<ServiceItems> ServicesData { get; set; }

        public IndexPageService()
        {
            ImageCardData = new ImageCardRepository().CreateImageCardList();

            NewsCardData = new NewsCardRepository().CreateNewsCardList();

            BannerData = new BannerRepository().CreateBannerList();

            ServicesData = new ServiceItemRepository().CreateServicesList();
        }
    }
}