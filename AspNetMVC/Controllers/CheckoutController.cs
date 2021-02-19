using AspNetMVC.Models;
using AspNetMVC.Models.Entity;
using AspNetMVC.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AspNetMVC.Controllers {
	public class CheckoutController : Controller {
		private readonly CheckoutService _checkoutService;
		private readonly UserFavoriteService _userFavoriteService;

		public CheckoutController() {
			_checkoutService = new CheckoutService();
			_userFavoriteService = new UserFavoriteService();
		}

		public ActionResult Index() {
			string accountName = Helpers.DecodeCookie(Request.Cookies["user"]["user_accountname"]);
			Guid favoriteId = Guid.Parse("059ec4ea-21dc-46e1-b730-10fb157b10b4");
			UserFavorite userFavorite = _checkoutService.GetFavorite(favoriteId, accountName);
			if (userFavorite.IsPackage) {
				PackageProduct data = _checkoutService.GetPackage(userFavorite);
				return View(
					new {
						IsPackage = userFavorite.IsPackage,
						Data = data
					}
				);
			} else {
				List<UserDefinedProduct> data = _checkoutService.GetUserDefinedList(userFavorite);
				return View(
					new {
						IsPackage = userFavorite.IsPackage,
						Data = data
					}
				);
			}
			//return View(userFavorite);
		}
		[HttpPost]
		public ActionResult GetOrder(FormCollection submit) {
			//var aa = order.;

			return Json(new { title = "預約成功", content = "服務人員將在預約時間前1小時內與您聯繫" });
		}
		//public ActionResult AddFavorite() {
		//	string accountName = Helpers.DecodeCookie(Request.Cookies["user"]["user_accountname"]);
		//	Guid? UserDefinedId = null;
		//	int? PackageProductId = 1;
		//	accountName = "leemike0429";
		//	_userFavoriteService.CreateFavorite(accountName, UserDefinedId, PackageProductId);
		//	return Content("success");
		//}
		[HttpGet]
		public ActionResult GetDistricts() {
			return Json(CountyModels.County);
		}
	}
}