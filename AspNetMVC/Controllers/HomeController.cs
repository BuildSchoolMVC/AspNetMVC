using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AspNetMVC.Models;
using AspNetMVC.Models.CustomerService;

namespace AspNetMVC.Controllers
{
    public class HomeController : Controller
    {
        private CustomerServiceContext context = new CustomerServiceContext();
        public ActionResult Index()
        {
            List<ImageCard> imageCards = new List<ImageCard>
            {
                new ImageCard{Name="居家鐘點清潔",Photo="/Assets/images/p1.jpg",Content="語音年輕人聯繫該聯繫該",StarCount=5,AnimationDelay=".3s"},
                new ImageCard{Name="收納整理",Photo="/Assets/images/p2.jpg",Content="高效晶片上傳寶貝有一論",StarCount=5,AnimationDelay=".6s"},
                new ImageCard{Name="冷氣清潔",Photo="/Assets/images/p3.jpg",Content="但是商機房價宣傳影",StarCount=5,AnimationDelay=".4s"},
                new ImageCard{Name="除塵蟎",Photo="/Assets/images/p4.jpg",Content="身份你的能力時尚微軟線路",StarCount=5,AnimationDelay=".8s"},
                new ImageCard{Name="洗衣機清潔",Photo="/Assets/images/p5.jpg",Content="儘量第一章方法警方在這伺服",StarCount=5,AnimationDelay=".5s"},
                new ImageCard{Name="洗衣服務",Photo="/Assets/images/p6.jpg",Content="室內認識語言優勢即可類別",StarCount=5,AnimationDelay="1s"},
            };

            return View(imageCards);
        }

        public ActionResult AllServiceView()
        {
            List<AllServiceCard> allServiceCards = new List<AllServiceCard>
            {
                new AllServiceCard{Title="鐘點清潔",Url="javascript:;",Icon="man-in-glasses",Content="專業清潔每小時500 - 600元不等創造舒適的窩"},
                new AllServiceCard{Title="冷氣機清洗",Url="javascript:;",Icon="energy-air",Content="與PM 2.5說不，還你清新空氣"},
                new AllServiceCard{Title="洗衣機清洗",Url="javascript:;",Icon="washing-machine",Content="與藏身許久煩人的汙垢宣戰"},
                new AllServiceCard{Title="收納整理",Url="javascript:;",Icon="briefcase-2",Content="收納整理，迎接好心情"},
                new AllServiceCard{Title="裝潢清潔",Url="javascript:;",Icon="bucket1",Content="裝潢後的清潔交給我們，木屑粉塵一網打盡"},
                new AllServiceCard{Title="除塵蟎",Url="javascript:;",Icon="bug",Content="除去塵蟎，拒當過敏兒"},
                new AllServiceCard{Title="清毒除蟲",Url="javascript:;",Icon="bug",Content="專業消毒噴霧機，擊退蟲害SO EASY"},
                new AllServiceCard{Title="辦公室定期",Url="javascript:;",Icon="architecture-alt",Content="舒適上班環境，工作效率DOUBLE"},
                new AllServiceCard{Title="地板保養",Url="javascript:;",Icon="triangle",Content="石板打蠟與木質地板保養，換得家裡大家開心"},
                new AllServiceCard{Title="洗衣服務",Url="javascript:;",Icon="jacket",Content="外送洗衣，以袋計價，隔日取件"},
            };
            return View(allServiceCards);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind] CustomerService customerService)
        {

            if (ModelState.IsValid)
            {
                context.CustomerServices.Add(customerService);
                context.SaveChanges();
            }
            return View();
        }
    }
}