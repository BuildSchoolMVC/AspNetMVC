using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AspNetMVC.Models;

namespace AspNetMVC.Repository
{
    public class BannerRepository
    {
        public List<Banner> CreateBannerList()
        {
            return new List<Banner>
            {
                new Banner{Id=1,Slogan=new List<string>{"新的一年即將到來","忙碌的你勢必忘了打掃","沒關係，有我們在" }, Title="年前打掃優惠"},
                new Banner{Id=2,Slogan=new List<string>{ "每月用少少的錢打掃辦公環境", "換來公司倍速的成長" }, Title="辦公室清潔"},
                new Banner{Id=4,Slogan=new List<string>{ "即將開放，敬請期待", }, Title="辦公室清潔"},
                new Banner{Id=3,Slogan=new List<string>{"我們提供多樣的專業服務","讓你迎接舒服的空間" }, Title="多樣化服務"},
                new Banner{Id=5,Slogan=new List<string>{ "專業除螨，過敏兒遠離你" }, Title="除螨服務"},
                new Banner{Id=6,Slogan=new List<string>{ "收拾環境，整理心情" }, Title="收納整理"},
                new Banner{Id=7,Slogan=new List<string>{"呼出好心情" }, Title="冷氣機清洗"},
            };
        }
    }
}