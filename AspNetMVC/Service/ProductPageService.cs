using AspNetMVC.Models;
using AspNetMVC.Repository;
using AspNetMVC.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AspNetMVC.Models.Entity;

namespace AspNetMVC.Service
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
        public List<ProductPageViewModel> CreateData()
        {
            var result= _repository.GetAll<PackageProduct>().Select(x => new ProductPageViewModel()
            {
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

    }
}