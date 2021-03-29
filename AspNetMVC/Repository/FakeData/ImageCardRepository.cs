using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AspNetMVC.ViewModels;

namespace AspNetMVC.Repository
{
    public class ImageCardRepository
    {
        public List<ImageCardViewModel> CreateImageCardList()
        {
            return new List<ImageCardViewModel>
            {
                new ImageCardViewModel{Name="居家鐘點清潔",Photo="/Assets/images/p1.jpg",Content="居家清潔交給我",StarCount=4.2m,AnimationDelay=".3s"},
                new ImageCardViewModel{Name="收納整理",Photo="/Assets/images/p2.jpg",Content="收納整理我最行",StarCount=4.3m,AnimationDelay=".6s"},
                new ImageCardViewModel{Name="冷氣清潔",Photo="/Assets/images/p3.jpg",Content="冷氣藏垢清潔溜溜",StarCount=5,AnimationDelay=".4s"},
                new ImageCardViewModel{Name="除塵蟎",Photo="/Assets/images/p4.jpg",Content="塵蟎一網打盡",StarCount=4.9m,AnimationDelay=".8s"},
                new ImageCardViewModel{Name="洗衣機清潔",Photo="/Assets/images/p5.jpg",Content="陳年汙垢一掃而空",StarCount=5,AnimationDelay=".5s"},
                new ImageCardViewModel{Name="洗衣服務",Photo="/Assets/images/p6.jpg",Content="跟買新衣一樣",StarCount=3.9m,AnimationDelay="1s"},
            };
        }
    }
}