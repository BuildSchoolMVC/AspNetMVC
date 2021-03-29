using AspNetMVC.Models;
using AspNetMVC.Repository;
using AspNetMVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AspNetMVC.Models.Entity;
using Newtonsoft.Json;
using AspNetMVC.Controllers;

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
            var result = _repository.GetAll<PackageProduct>().Where(x=>x.IsValid==true
            ).Select(x => new ProductPageViewModel()
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
                            IsDelete = false,
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
                var include = _repository.GetAll<UserFavorite>().Any(x => x.PackageProductId == packageproductId && x.AccountName == name && x.IsDelete == false);

                if (include)
                {
                    result.Status = 1;
                }
                else
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
                    result.Status = 0;
                }
            }
            catch (Exception ex)
            {
                result.Status = 2;
                result.Exception = ex;
            }
            return result;
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

                        PackageProduct p = _repository.GetAll<PackageProduct>().FirstOrDefault(x => x.PackageProductId == item.PackageProductId && x.IsValid == true);

                        if(p!=null)
                        {
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
                    }
                    else
                    {
                        var query = from udp in _repository.GetAll<UserDefinedProduct>()
                                    where udp.UserDefinedId == item.UserDefinedId && udp.IsDelete==false
                                    join rt in _repository.GetAll<RoomType>() on udp.RoomType equals rt.Value
                                    select new UserFavoriteData
                                    {
                                        Hour = udp.Hour,
                                        PhotoUrl = rt.PhotoUrl,
                                        Price = udp.Price,
                                        RoomType = udp.RoomType.ToString(),
                                        ServiceItem = udp.ServiceItems,
                                        Squarefeet = udp.Squarefeet.ToString(),
                                        Title = udp.Name
                                    };

                        UserFavoriteViewModel ufVM = new UserFavoriteViewModel
                        {
                            Data = query.ToList(),
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
                   value == 3 ? "16坪以上" : "";
        }

        public string RoomTypeSwitch(int? value)
        {
            return value == 0 ? "廚房" :
                   value == 1 ? "客廳" :
                   value == 2 ? "臥室" :
                   value == 3 ? "浴廁" :
                   value == 4 ? "陽台" : "";
        }
        public IQueryable<UserDefinedAll> SearchAllUserDefined(Guid favoriteid)
        {
            var item = _repository.GetAll<UserFavorite>().FirstOrDefault(x => x.FavoriteId == favoriteid);
            var result = _repository.GetAll<UserDefinedProduct>().Where(x => x.UserDefinedId == item.UserDefinedId && x.IsDelete != true).OrderBy(x=>x.CreateTime).Select(x => new UserDefinedAll
            {
                UserDefinedProductId = x.UserDefinedProductId.ToString(),
                UserDefinedId = x.UserDefinedId.ToString(),
                Title=x.Name,
                RoomType = x.RoomType,
                ServiceItem = x.ServiceItems,
                Squarefeet = x.Squarefeet,
            });

            return result;
        }
        public OperationResult DeleteFavoriteData(Guid favoriteId)
        {
            var result = new OperationResult();
            var item = _repository.GetAll<UserFavorite>().FirstOrDefault(x => x.FavoriteId == favoriteId);
            try
            {
                item.IsDelete = true;
                item.EditTime = DateTime.UtcNow.AddHours(8);
                item.EditUser = item.AccountName;
                _repository.Update<UserFavorite>(item);
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
        public OperationResult modifyUserDefined(IEnumerable<UserDefinedAll> userDefinedall, string name)
        {

            var result = new OperationResult();

            try
            {
                foreach (var item in userDefinedall)
                {
                    if (item.UserDefinedProductId == null)
                    {
                        var product = new UserDefinedProduct
                        {
                            UserDefinedProductId = Guid.NewGuid(),
                            UserDefinedId = Guid.Parse(item.UserDefinedId),
                            IsDelete = false,
                            AccountName = name,
                            ServiceItems = item.ServiceItem,
                            RoomType = item.RoomType,
                            Squarefeet = item.Squarefeet,
                            Name = item.Title,
                            Hour = countHour(item.RoomType, item.Squarefeet),
                            Price = Convert.ToDecimal(countHour(item.RoomType, item.Squarefeet)) * 500,
                            CreateTime = DateTime.UtcNow.AddHours(8),
                            CreateUser = name,
                            EditTime = DateTime.UtcNow.AddHours(8),
                            EditUser = name,
                        };
                        _repository.Create<UserDefinedProduct>(product);
                    }
                    else
                    {
                        var singleitem = _repository.GetAll<UserDefinedProduct>().FirstOrDefault(x => x.UserDefinedProductId.ToString() == item.UserDefinedProductId);
                        singleitem.RoomType = item.RoomType;
                        singleitem.Squarefeet = item.Squarefeet;
                        singleitem.ServiceItems = item.ServiceItem;
                        singleitem.IsDelete = item.IsDelete;
                        singleitem.Hour = countHour(item.RoomType, item.Squarefeet);
                        singleitem.Price = Convert.ToDecimal(countHour(item.RoomType, item.Squarefeet)) * 500;
                        singleitem.EditTime = DateTime.UtcNow.AddHours(8);
                        singleitem.EditUser = singleitem.AccountName;
                        _repository.Update<UserDefinedProduct>(singleitem);
                        _context.SaveChanges();
                        var itemlength = _repository.GetAll<UserDefinedProduct>().Count(x => x.UserDefinedId.ToString() == item.UserDefinedId && x.IsDelete == false);
                        if (itemlength == 0)
                        {
                            var removeitem = _repository.GetAll<UserFavorite>().FirstOrDefault(x => x.UserDefinedId.ToString() == item.UserDefinedId);
                            removeitem.IsDelete = true;
                            removeitem.EditTime = DateTime.UtcNow.AddHours(8);
                            removeitem.EditUser = name;
                            _repository.Update<UserFavorite>(removeitem);
                        }
                    }
                }

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

