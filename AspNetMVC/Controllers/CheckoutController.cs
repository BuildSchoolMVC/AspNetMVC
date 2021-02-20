using AspNetMVC.Models;
using AspNetMVC.Models.Entity;
using AspNetMVC.Services;
using AspNetMVC.ViewModels;
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
		[HttpGet]
		public ActionResult Index(string id) {
			string accountName = Helpers.DecodeCookie(Request.Cookies["user"]["user_accountname"]);
			Guid favoriteId = Guid.Parse("1a648031-ac16-45db-bbf2-2b6c168f000a");
			//Guid favoriteId = Guid.Parse("2ca80158-498c-43ff-81bb-d2870776bdb3");
			//假資料
			UserFavorite userFavorite = new UserFavorite {
				FavoriteId = favoriteId,
				AccountName = "blender222",
				UserDefinedId = null,
				PackageProductId = 3,
				IsPackage = true,
				IsDelete = false,
			};
			DataViewModel dataViewModel = new DataViewModel {
				IsPackage = userFavorite.IsPackage,
				Package = null,
				UserDefinedList = null,
				RoomTypeList = _checkoutService.GetRoomTypeList(),
				SquareFeetList = _checkoutService.GetSquareFeetList()
			};
			//================================================
			//Guid favoriteId;
			//UserFavorite userFavorite;
			//try {
			//	favoriteId = Guid.Parse(id);
			//	userFavorite = _checkoutService.GetFavorite(favoriteId, accountName);
			//} catch (Exception) {
			//	return View("Error");
			//}
			//DataViewModel dataViewModel = new DataViewModel {
			//	IsPackage = userFavorite.IsPackage,
			//	Package = null,
			//	UserDefinedList = null,
			//	RoomTypeList = _checkoutService.GetRoomTypeList(),
			//	SquareFeetList = _checkoutService.GetSquareFeetList()
			//};
			//if (userFavorite.IsPackage) {
			//	dataViewModel.Package = _checkoutService.GetPackage(userFavorite);
			//} else {
			//	dataViewModel.UserDefinedList = _checkoutService.GetUserDefinedList(userFavorite);
			//}
			return View(dataViewModel);
		}
		[HttpPost]
		public ActionResult GetOrder(FormCollection submit) {

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
			return Json(CountyModels.County, JsonRequestBehavior.AllowGet);
		}
	}
}