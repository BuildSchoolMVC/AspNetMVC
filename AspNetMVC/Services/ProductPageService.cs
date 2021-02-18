using AspNetMVC.Models;
using AspNetMVC.Repository;
using AspNetMVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AspNetMVC.Models.Entity;

namespace AspNetMVC.Services
{
    public class ProductPageService
    {
        private readonly UCleanerDBContext _context;
        private readonly BaseRepository _repository;

        public ProductPageService()
        {
            _context = new UCleanerDBContext();
            _repository = new BaseRepository(_context);
        }
        public List<ProductPageViewModel> GetData()
        {
            var result = _repository.GetAll<PackageProduct>().Select(x => new ProductPageViewModel()
            {
                PackageProductId = x.PackageProductId,
                Title = x.Name,
                Price = x.Price,
                Hour = x.Hour,
                RoomType = x.RoomType,
                RoomType2 = x.RoomType2,
                RoomType3 = x.RoomType3,
                ServiceItem = x.ServiceItem,
                Squarefeet = x.Squarefeet,
                Squarefeet2 = x.Squarefeet2,
                Squarefeet3 = x.Squarefeet3,
                PhotoUrl = x.PhotoUrl

            }).ToList();
            return result;
        }

        public OperationResult CreateUserDefinedPackageData(UserDefinedAll model, Guid account, string name)
        {

            var result = new OperationResult();
            try
            {
                var TempGuid = Guid.NewGuid();
                _repository.Create(new UserDefinedProduct
                {
                    UserDefinedId = TempGuid,
                    MemberId = account,
                    ServiceItems = model.ServiceItem,
                    RoomType = model.RoomType,
                    Squarefeet = model.Squarefeet,
                    Name = model.Title,
                    Hour = countHour(model.RoomType, model.Squarefeet),
                    Price = Convert.ToDecimal(countHour(model.RoomType, model.Squarefeet)) * 500,
                    CreateTime = DateTime.UtcNow.AddHours(8),
                    CreateUser = name,
                    EditTime = DateTime.UtcNow.AddHours(8),
                    EditUser = name,

                }
                    );
                _context.SaveChanges();
                result.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                result.IsSuccessful = false;
                result.Exception = ex;
            }
            return result;
        }
        public float countHour(int RoomType, int Squarefeet)
        {
            float hour;
            float basehour = 0;
            float unit = 0.5F;
            if (RoomType <= 2)
            {
                hour = basehour;
            }
            else
            {
                hour = basehour / 2;
            }
            hour += Squarefeet * unit;
            return hour;
        }

    }
}