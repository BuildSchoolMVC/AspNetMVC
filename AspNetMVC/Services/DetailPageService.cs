using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AspNetMVC.Models;
using AspNetMVC.Models.Entity;
using AspNetMVC.Repository;
using AspNetMVC.ViewModels;

namespace AspNetMVC.Services
{
    public class DetailPageService
    {
        private readonly UCleanerDBContext _context;
        private readonly BaseRepository _repository;

        public DetailPageService()
        {
            _context = new UCleanerDBContext();
            _repository = new BaseRepository(_context);
        }

        public DetailPageViewModel GetSingleProduct(int id) 
        {

            var result = _repository.GetAll<PackageProduct>().FirstOrDefault(x=>x.PackageProductId == id);

            if (result != null) {
                var detailPageVM = new DetailPageViewModel
                {
                    Id = result.PackageProductId,
                    Description = result.Description,
                    Hour = result.Hour,
                    Name = result.Name,
                    PhotoUrl = result.PhotoUrl,
                    Price = result.Price.ToString("###,###"),
                    ServiceItem = result.ServiceItem.Replace("+","、"),
                    RoomName = RoomTypeSwitch(result.RoomType),
                    RoomName2 = RoomTypeSwitch(result.RoomType2),
                    RoomName3 = RoomTypeSwitch(result.RoomType3),
                    SquarefeetName = SquarefeetSwitch(result.Squarefeet),
                    SquarefeetName2 = SquarefeetSwitch(result.Squarefeet2),
                    SquarefeetName3 = SquarefeetSwitch(result.Squarefeet3)
                };
                return detailPageVM;
            }
            else
            {
                return new DetailPageViewModel 
                {
                    Name = ""
                };
            }
        }

        public void CreateComment(string account,int productId,int star,string comment) 
        {
            var result = new OperationResult(); 
            Guid AccountId = _repository.GetAll<Account>().FirstOrDefault(x => x.AccountName == account).AccountId;

            try
            {
                _repository.Create<Comment>(new Comment
                {
                    CommentId = Guid.NewGuid(),
                    AccountId = AccountId,
                    PackageProductId = productId,
                    CreateTime = DateTime.UtcNow.AddHours(8),
                    CreateUser = "jacko1114",
                    EditTime = DateTime.UtcNow.AddHours(8),
                    EditUser = "jacko1114",
                    Star = star,
                    Content = comment
                });
                _context.SaveChanges();
                result.IsSuccessful = true;
            }
            catch (Exception ex) {
                result.IsSuccessful = false;
                result.Exception = ex;
            }
        }

        private string RoomTypeSwitch(int? value) {
            return value == 0 ? "廚房" :
                   value == 1 ? "客廳" :
                   value == 2 ? "客廳" :
                   value == 3 ? "客廳" :
                   value == 4 ? "陽台" : "-";
            }

        private string SquarefeetSwitch(int? value)
        {
            return value == 0 ? "5坪以下" :
                   value == 1 ? "6-10坪" :
                   value == 2 ? "11-15坪" :
                   value == 3 ? "16坪以上" : "-";
        }
    }
}