using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AspNetMVC.ViewModel;

namespace AspNetMVC.Repository
{
    public class AllServiceCardRepository
    {

        public List<AllServiceCard> CreateAllServiceCardList()
        {
            return new List<AllServiceCard>
            {
                new AllServiceCard{Title="冷氣機清洗",Url="/ProductPage",Icon="energy-air",Content="與PM 2.5說不，還你清新空氣"},
                new AllServiceCard{Title="洗衣機清洗",Url="/ProductPage",Icon="washing-machine",Content="與藏身許久煩人的汙垢宣戰"},
                new AllServiceCard{Title="收納整理",Url="/ProductPage",Icon="briefcase-2",Content="收納整理，迎接好心情"},
                new AllServiceCard{Title="裝潢清潔",Url="/ProductPage",Icon="bucket1",Content="裝潢後的清潔交給我們，木屑粉塵一網打盡"},
                new AllServiceCard{Title="除塵蟎",Url="/ProductPage",Icon="bug",Content="除去塵蟎，拒當過敏兒"},
                new AllServiceCard{Title="清毒除蟲",Url="/ProductPage",Icon="bug",Content="專業消毒噴霧機，擊退蟲害SO EASY"},
                new AllServiceCard{Title="辦公室定期",Url="/ProductPage",Icon="architecture-alt",Content="舒適上班環境，工作效率DOUBLE"},
                new AllServiceCard{Title="地板保養",Url="/ProductPage",Icon="triangle",Content="石板打蠟與木質地板保養，換得家裡大家開心"},
                new AllServiceCard{Title="洗衣服務",Url="/ProductPage",Icon="jacket",Content="外送洗衣，以袋計價，隔日取件"},
                new AllServiceCard{Title="鐘點清潔",Url="/ProductPage",Icon="man-in-glasses",Content="專業清潔每小時500 - 600元不等創造舒適的窩"},
            };
        }
    }
}