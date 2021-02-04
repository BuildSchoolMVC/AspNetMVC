using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AspNetMVC.Models;

namespace AspNetMVC.Repository
{
    public class ImageCardRepository
    {
        public List<ImageCard> CreateImageCardList()
        {
            return new List<ImageCard>
            {
                new ImageCard{Name="居家鐘點清潔",Photo="/Assets/images/p1.jpg",Content="語音年輕人聯繫該聯繫該",StarCount=5,AnimationDelay=".3s"},
                new ImageCard{Name="收納整理",Photo="/Assets/images/p2.jpg",Content="高效晶片上傳寶貝有一論",StarCount=5,AnimationDelay=".6s"},
                new ImageCard{Name="冷氣清潔",Photo="/Assets/images/p3.jpg",Content="但是商機房價宣傳影",StarCount=5,AnimationDelay=".4s"},
                new ImageCard{Name="除塵蟎",Photo="/Assets/images/p4.jpg",Content="身份你的能力時尚微軟線路",StarCount=5,AnimationDelay=".8s"},
                new ImageCard{Name="洗衣機清潔",Photo="/Assets/images/p5.jpg",Content="儘量第一章方法警方在這伺服",StarCount=5,AnimationDelay=".5s"},
                new ImageCard{Name="洗衣服務",Photo="/Assets/images/p6.jpg",Content="室內認識語言優勢即可類別",StarCount=5,AnimationDelay="1s"},
            };
        }
    }
}