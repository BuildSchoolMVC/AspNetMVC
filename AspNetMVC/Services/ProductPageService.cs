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

        public void CreateUserDefinedDataInFavorite(IEnumerable<UserDefinedAll> model, string name, Guid TempGuid)
        {

            var result = new OperationResult();
            using (var transcation = _context.Database.BeginTransaction())
            {

                try
                {
                    foreach (var i in model)
                    {
                        var product = new UserDefinedProduct
                        {
                            UserDefinedProductId = Guid.NewGuid(),
                            UserDefinedId = TempGuid,
                            AccountName = name,
                            ServiceItems = i.ServiceItem,
                            RoomType = i.RoomType,
                            Squarefeet = i.Squarefeet,
                            Name = i.Title,
                            Hour = countHour(i.RoomType, i.Squarefeet),
                            Price = Convert.ToDecimal(countHour(i.RoomType, i.Squarefeet)) * 500,
                            CreateTime = DateTime.UtcNow.AddHours(8),
                            CreateUser = name,
                            EditTime = DateTime.UtcNow.AddHours(8),
                            EditUser = name,
                        };
                    _repository.Create<UserDefinedProduct>(product);
                    }
                    _context.SaveChanges();
                    var userfavorite= new UserFavorite
                    {
                        FavoriteId = Guid.NewGuid(),
                        AccountName = name,
                        UserDefinedId = TempGuid,
                        PackageProductId = null,
                        IsPakage = false,
                        IsDelete = false,
                        CreateTime = DateTime.UtcNow.AddHours(8),
                        CreateUser = name,
                        EditTime = DateTime.UtcNow.AddHours(8),
                        EditUser = name,
                    };


                    _repository.Create<UserFavorite>(userfavorite);
                    

                    result.IsSuccessful = true;
                    transcation.Commit();
                }
                catch (Exception ex)
                {
                    result.IsSuccessful = false;
                    result.Exception = ex;
                    transcation.Rollback();
                }
            }
            
        }
        public float countHour(int RoomType, int Squarefeet)
        {
            float hour;
            float basehour = 1;
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
        public OperationResult CreatePackageProductDataInFavorite(int packageproductId, string name)
        {

            var result = new OperationResult();

            try
            {

                _repository.Create(new UserFavorite
                {
                    FavoriteId = Guid.NewGuid(),
                    AccountName = name,
                    UserDefinedId = null,
                    PackageProductId = packageproductId,
                    IsPakage = true,
                    IsDelete = false,
                    CreateTime = DateTime.UtcNow.AddHours(8),
                    CreateUser = name,
                    EditTime = DateTime.UtcNow.AddHours(8),
                    EditUser = name,
                });
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

    }
}