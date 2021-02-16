using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AspNetMVC.ViewModels;

namespace AspNetMVC.Repository
{
    public class NewsCardRepository
    {
        public List<NewsCardViewModel> CreateNewsCardList()
        {
            return new List<NewsCardViewModel>
            {
                new NewsCardViewModel{Alt="新年將近，五個改善你廚房的方法",Content="新年將近，五個改善你廚房的方法",Delay=".1s",ImgUrl="n1",Tag="生活",Url="https://www.independent.co.uk/life-style/kitchen-organise-sustainable-new-year-resolution-a9273931.html"},
                new NewsCardViewModel{Alt="民調顯示，數百國人對不清潔房屋對健康的危害意識相當薄弱",Content="民調顯示，數百國人對不清潔房屋對健康的危害意識相當薄弱",Delay=".2s",ImgUrl="n2",Tag="居家",Url="https://www.independent.co.uk/property/house-and-home/home-cleaning-illness-flu-cold-household-virus-poll-a9200231.html"},
                new NewsCardViewModel{Alt="上次清潔烤箱是什麼時候了呢",Content="上次清潔烤箱是什麼時候了呢",Delay=".3s",ImgUrl="n3",Tag="生活",Url="https://www.independent.co.uk/life-style/kitchen-organise-sustainable-new-year-resolution-a9273931.html"},
                new NewsCardViewModel{Alt="10大有效的清潔用品",Content="10大有效的清潔用品",Delay=".4s",ImgUrl="n4",Tag="清潔用品",Url="https://www.independent.co.uk/life-style/kitchen-organise-sustainable-new-year-resolution-a9273931.html"},
            };
        }
    }
}