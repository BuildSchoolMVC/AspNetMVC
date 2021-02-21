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
        private readonly AccountService _account;

        public ProductPageService()
        {
            _context = new UCleanerDBContext();
            _repository = new BaseRepository(_context);
            _account = new AccountService();
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
                    var userfavorite = new UserFavorite
                    {
                        FavoriteId = Guid.NewGuid(),
                        AccountName = name,
                        UserDefinedId = TempGuid,
                        PackageProductId = null,
                        IsPackage = false,
                        IsDelete = false,
                        CreateTime = DateTime.UtcNow.AddHours(8),
                        CreateUser = name,
                        EditTime = DateTime.UtcNow.AddHours(8),
                        EditUser = name,
                    };


                    _repository.Create<UserFavorite>(userfavorite);
                    _context.SaveChanges();

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
                    IsPackage = true,
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
        public Account GetUser(Guid id)
        {
            return _repository.GetAll<Account>().FirstOrDefault(x => x.AccountId == id);
        }

        public List<UserFavoriteViewModel> GetFavoriteUserFavoriteData(string username)
        {
            List<UserFavoriteViewModel> packageslist = new List<UserFavoriteViewModel>();

            var userFavorites = _repository.GetAll<UserFavorite>().Where(x => x.AccountName == username).OrderBy(x => x.CreateTime).ToList();

            foreach (var item in userFavorites)
            {
                if (item.IsDelete == false)
                {
                    if (item.IsPackage)
                    {

                        PackageProduct p = _repository.GetAll<PackageProduct>().FirstOrDefault(x => x.PackageProductId == item.PackageProductId);

                        UserFavoriteData ufdata = new UserFavoriteData
                        {
                            Hour = p.Hour,
                            ServiceItem = p.ServiceItem,
                            Price = p.Price,
                            Title = p.Name,
                            RoomType = $"{RoomTypeSwitch(p.RoomType)},{RoomTypeSwitch(p.RoomType2)},{RoomTypeSwitch(p.RoomType3)}",
                            Squarefeet = $"{SquarefeetSwitch(p.Squarefeet)},{SquarefeetSwitch(p.Squarefeet2)},{SquarefeetSwitch(p.Squarefeet3)}",
                            PhotoUrl = p.PhotoUrl,
                            PackageProductId = p.PackageProductId
                        };

                        UserFavoriteViewModel ufVM = new UserFavoriteViewModel
                        {
                            Data = new List<UserFavoriteData> { ufdata },
                            FavoriteId = item.FavoriteId,
                            IsPackage = item.IsPackage
                        };
                        packageslist.Add(ufVM);
                    }
                    else
                    {
                        List<UserFavoriteData> Ud = _repository.GetAll<UserDefinedProduct>().Where(x => x.UserDefinedId == item.UserDefinedId).Select(x => new UserFavoriteData
                        {
                            Hour = x.Hour,
                            Price = x.Price,
                            RoomType = x.RoomType.ToString(),
                            ServiceItem = x.ServiceItems,
                            Squarefeet = x.Squarefeet.ToString(),
                            Title = x.Name,
                            PhotoUrl = ""
                        }).ToList();

                        UserFavoriteViewModel ufVM = new UserFavoriteViewModel
                        {
                            Data = Ud,
                            FavoriteId = item.FavoriteId,
                            IsPackage = item.IsPackage
                        };
                        packageslist.Add(ufVM);
                    }
                }
            }
            return packageslist;

        }
        public string SquarefeetSwitch(int? value)
        {
            return value == 0 ? "5坪以下" :
                   value == 1 ? "6-10坪" :
                   value == 2 ? "11-15坪" :
                   value == 3 ? "16坪以上" : "-";
        }

        public string RoomTypeSwitch(int? value)
        {
            return value == 0 ? "廚房" :
                   value == 1 ? "客廳" :
                   value == 2 ? "臥室" :
                   value == 3 ? "浴廁" :
                   value == 4 ? "陽台" : "-";
        }

        public OperationResult DeleteFavoriteData(Guid favoriteId)
        {
            var result = new OperationResult();
            var item = _repository.GetAll<UserFavorite>().FirstOrDefault(x => x.FavoriteId == favoriteId);
            try
            {

                _repository.Update<UserFavorite>(new UserFavorite
                {
                    FavoriteId = item.FavoriteId,
                    AccountName = item.AccountName,
                    UserDefinedId = null,
                    PackageProductId = item.PackageProductId,
                    IsPackage = true,
                    IsDelete = true,
                    CreateTime = DateTime.UtcNow.AddHours(8),
                    CreateUser = item.AccountName,
                    EditTime = DateTime.UtcNow.AddHours(8),
                    EditUser = item.AccountName,
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

