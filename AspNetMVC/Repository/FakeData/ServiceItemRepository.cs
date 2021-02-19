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
                       new ServiceItemViewModel{ImgUrl="air condtioner",Text="不再網通都有公斤多次進了等等，還沒記錄，網址打擊商標做出實。",Title="冷氣機清潔",Url="../Detail/Index/1"},
                       new ServiceItemViewModel{ImgUrl="kitchen",Text="不再網通都有公斤多次進了等等，還沒記錄，網址打擊商標做出實。",Title="廚房清潔",Url="../Detail/Index/2"},
                       new ServiceItemViewModel{ImgUrl="wash machine",Text="不再網通都有公斤多次進了等等，還沒記錄，網址打擊商標做出實。",Title="洗衣機清潔",Url="../Detail/Index/3"}
                    },
                },
                new ServiceItemsViewModel
                {
                    Category ="enterprise", IsActive = "",Services = new List<ServiceItemViewModel>
                    {
                       new ServiceItemViewModel{ImgUrl="office1",Text="不再網通都有公斤多次進了等等，還沒記錄，網址打擊商標做出實。",Title="辦公室清潔",Url=""},
                       new ServiceItemViewModel{ImgUrl="office2",Text="不再網通都有公斤多次進了等等，還沒記錄，網址打擊商標做出實。",Title="包月服務",Url=""},
                       new ServiceItemViewModel{ImgUrl="office3",Text="不再網通都有公斤多次進了等等，還沒記錄，網址打擊商標做出實。",Title="整層清潔",Url=""}
                    },
                },
                new ServiceItemsViewModel
                {
                    Category ="other", IsActive = "",Services = new List<ServiceItemViewModel>
                    {
                       new ServiceItemViewModel{ImgUrl="bed",Text="不再網通都有公斤多次進了等等，還沒記錄，網址打擊商標做出實。",Title="床墊清潔",Url="../Detail/Index/4"},
                       new ServiceItemViewModel{ImgUrl="water pip",Text="不再網通都有公斤多次進了等等，還沒記錄，網址打擊商標做出實。",Title="水管清潔",Url="../Detail/Index/5"},
                       new ServiceItemViewModel{ImgUrl="wall",Text="不再網通都有公斤多次進了等等，還沒記錄，網址打擊商標做出實。",Title="壁癌處理",Url="../Detail/Index/6"}
                    },
                },
                new ServiceItemsViewModel
                {
                    Category ="comment", IsActive = "",Services = new List<ServiceItemViewModel>
                    {
                       new ServiceItemViewModel{ImgUrl="c1",Text="不再網通都有公斤多次進了等等，還沒記錄，網址打擊商標做出實。",Title="陳先生",Url=""},
                       new ServiceItemViewModel{ImgUrl="c2",Text="不再網通都有公斤多次進了等等，還沒記錄，網址打擊商標做出實。",Title="林小姐",Url=""},
                       new ServiceItemViewModel{ImgUrl="c3",Text="不再網通都有公斤多次進了等等，還沒記錄，網址打擊商標做出實。",Title="白先生",Url=""}
                    },
                },
            };
        }
    }
}