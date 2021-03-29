using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AspNetMVC.Repository;
using AspNetMVC.ViewModels;

namespace AspNetMVC.Services
{
    public class IndexService
    {
        public List<ImageCardViewModel> ImageCardData { get; set; }

        public List<NewsCardViewModel> NewsCardData { get; set; }

        public List<BannerViewModel> BannerData { get; set; }

        public List<ServiceItemsViewModel> ServicesData { get; set; }

        public IndexService()
        {
            ImageCardData = new ImageCardRepository().CreateImageCardList();

            NewsCardData = new NewsCardRepository().CreateNewsCardList();

            BannerData = new BannerRepository().CreateBannerList();

            ServicesData = new ServiceItemRepository().CreateServicesList();
        }
    }
}