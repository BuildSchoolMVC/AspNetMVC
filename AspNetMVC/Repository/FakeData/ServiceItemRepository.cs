using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AspNetMVC.Models;

namespace AspNetMVC.Repository
{
    public class ServiceItemRepository
    {
        public List<ServiceItems> CreateServicesList()
        {
            return new List<ServiceItems>
            {
                new ServiceItems
                {
                    Category ="hot", IsActive = "active",Services = new List<ServiceItem>
                    {
                       new ServiceItem{ImgUrl="air condtioner",Text="不再網通都有公斤多次進了等等，還沒記錄，網址打擊商標做出實。",Title="冷氣機清潔",Url="../DetailPage/Index"},
                       new ServiceItem{ImgUrl="kitchen",Text="不再網通都有公斤多次進了等等，還沒記錄，網址打擊商標做出實。",Title="廚房清潔",Url="../DetailPage/Index"},
                       new ServiceItem{ImgUrl="wash machine",Text="不再網通都有公斤多次進了等等，還沒記錄，網址打擊商標做出實。",Title="洗衣機清潔",Url="../DetailPage/Index"}
                    },
                },
                new ServiceItems
                {
                    Category ="enterprise", IsActive = "",Services = new List<ServiceItem>
                    {
                       new ServiceItem{ImgUrl="office1",Text="不再網通都有公斤多次進了等等，還沒記錄，網址打擊商標做出實。",Title="辦公室清潔",Url="../DetailPage/Index"},
                       new ServiceItem{ImgUrl="office2",Text="不再網通都有公斤多次進了等等，還沒記錄，網址打擊商標做出實。",Title="包月服務",Url="../DetailPage/Index"},
                       new ServiceItem{ImgUrl="office3",Text="不再網通都有公斤多次進了等等，還沒記錄，網址打擊商標做出實。",Title="整層清潔",Url="../DetailPage/Index"}
                    },
                },
                new ServiceItems
                {
                    Category ="other", IsActive = "",Services = new List<ServiceItem>
                    {
                       new ServiceItem{ImgUrl="bed",Text="不再網通都有公斤多次進了等等，還沒記錄，網址打擊商標做出實。",Title="床墊清潔",Url="../DetailPage/Index"},
                       new ServiceItem{ImgUrl="water pip",Text="不再網通都有公斤多次進了等等，還沒記錄，網址打擊商標做出實。",Title="水管清潔",Url="../DetailPage/Index"},
                       new ServiceItem{ImgUrl="wall",Text="不再網通都有公斤多次進了等等，還沒記錄，網址打擊商標做出實。",Title="壁癌處理",Url="../DetailPage/Index"}
                    },
                },
                new ServiceItems
                {
                    Category ="comment", IsActive = "",Services = new List<ServiceItem>
                    {
                       new ServiceItem{ImgUrl="c1",Text="不再網通都有公斤多次進了等等，還沒記錄，網址打擊商標做出實。",Title="陳先生",Url="../DetailPage/Index"},
                       new ServiceItem{ImgUrl="c2",Text="不再網通都有公斤多次進了等等，還沒記錄，網址打擊商標做出實。",Title="林小姐",Url="../DetailPage/Index"},
                       new ServiceItem{ImgUrl="c3",Text="不再網通都有公斤多次進了等等，還沒記錄，網址打擊商標做出實。",Title="白先生",Url="../DetailPage/Index"}
                    },
                },
            };
        }
    }
}