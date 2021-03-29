using AspNetMVC.Models;
using AspNetMVC.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AspNetMVC.Models.Entity;

namespace AspNetMVC.Services {
	public class UserFavoriteService {
		private readonly UCleanerDBContext _context;
		private readonly BaseRepository _repository;

		public UserFavoriteService() {
			_context = new UCleanerDBContext();
			_repository = new BaseRepository(_context);
		}
		public void CreateFavorite(string accountName, Guid? userDefinedId = null, int? packageProductId = null) {
			DateTime now = DateTime.Now;
			var result = new OperationResult();
			try {
				_repository.Create<UserFavorite>(new UserFavorite {
					FavoriteId = Guid.NewGuid(),
					AccountName = accountName,
					UserDefinedId = userDefinedId,
					PackageProductId = packageProductId,
					IsPackage = packageProductId != null,
					IsDelete = false,
					CreateTime = now,
					EditTime = now,
					CreateUser = accountName,
					EditUser = accountName,
				});
				_context.SaveChanges();
				result.IsSuccessful = true;
			} catch (Exception ex) {
				result.IsSuccessful = false;
				result.Exception = ex;
			}
		}
	}
}