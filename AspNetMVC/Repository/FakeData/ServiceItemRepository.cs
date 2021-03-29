using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AspNetMVC.ViewModels;

namespace AspNetMVC.Repository
{
    public class ServiceItemRepository
    {
        public List<ServiceItemsViewModel> CreateServicesList()
        {
            return new List<ServiceItemsViewModel>
            {
                new ServiceItemsViewModel
                {
                    Category ="hot", IsActive = "active",Services = new List<ServiceItemViewModel>
                    {
                       new ServiceItemViewModel{ImgUrl="air condtioner",Text="與PM 2.5說不，還你清新空氣。",Title="冷氣機清潔",Url="../Detail/Index/1"},
                       new ServiceItemViewModel{ImgUrl="kitchen",Text="廚房清潔教我們，讓你往後燒一桌好飯菜",Title="廚房清潔",Url="../Detail/Index/2"},
                       new ServiceItemViewModel{ImgUrl="wash machine",Text="外送洗衣，以袋計價，隔日取件。",Title="洗衣機清潔",Url="../Detail/Index/3"}
                    },
                },
                new ServiceItemsViewModel
                {
                    Category ="enterprise", IsActive = "",Services = new List<ServiceItemViewModel>
                    {
                       new ServiceItemViewModel{ImgUrl="office1",Text="舒適上班環境，工作效率DOUBLE。",Title="辦公室清潔",Url=""},
                       new ServiceItemViewModel{ImgUrl="office2",Text="舒適上班環境，工作效率DOUBLE。",Title="包月服務",Url=""},
                       new ServiceItemViewModel{ImgUrl="office3",Text="舒適上班環境，工作效率DOUBLE。",Title="整層清潔",Url=""}
                    },
                },
                new ServiceItemsViewModel
                {
                    Category ="other", IsActive = "",Services = new List<ServiceItemViewModel>
                    {
                       new ServiceItemViewModel{ImgUrl="bed",Text="藏在床墊背後汙垢由我們消滅。",Title="床墊清潔",Url="../Detail/Index/4"},
                       new ServiceItemViewModel{ImgUrl="water pip",Text="疏通阻塞的水管。",Title="水管清潔",Url="../Detail/Index/5"},
                       new ServiceItemViewModel{ImgUrl="wall",Text="來換掉斑駁的牆壁。",Title="壁癌處理",Url="../Detail/Index/6"}
                    },
                },
                new ServiceItemsViewModel
                {
                    Category ="comment", IsActive = "",Services = new List<ServiceItemViewModel>
                    {
                       new ServiceItemViewModel{ImgUrl="c1",Text="讚啦，幫了我大忙。",Title="陳先生",Url=""},
                       new ServiceItemViewModel{ImgUrl="c2",Text="不敢相信，這就是我要!!!",Title="林小姐",Url=""},
                       new ServiceItemViewModel{ImgUrl="c3",Text="太棒了，掃這麼乾淨，老婆這下沒話說了吧",Title="白先生",Url=""}
                    },
                },
            };
        }
    }
}