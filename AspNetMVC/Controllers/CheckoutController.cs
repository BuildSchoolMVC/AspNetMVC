using AspNetMVC.Models;
using AspNetMVC.Models.Entity;
using AspNetMVC.Services;
using AspNetMVC.ViewModels;
using ECPay.Payment.Integration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
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
			string accountName;
			Guid favoriteId;
			UserFavorite userFavorite;
			//id = "1a648031-ac16-45db-bbf2-2b6c168f000a";
			//id = "2ca80158-498c-43ff-81bb-d2870776bdb3";
			//假資料
			//UserFavorite userFavorite = new UserFavorite {
			//	FavoriteId = favoriteId,
			//	AccountName = "blender222",
			//	UserDefinedId = null,
			//	PackageProductId = 3,
			//	IsPackage = true,
			//	IsDelete = false,
			//};
			//DataViewModel dataViewModel = new DataViewModel {
			//	IsPackage = userFavorite.IsPackage,
			//	Package = null,
			//	UserDefinedList = null,
			//	RoomTypeList = _checkoutService.GetRoomTypeList(),
			//	SquareFeetList = _checkoutService.GetSquareFeetList()
			//};
			//================================================

			try {
				accountName = Helpers.DecodeCookie(Request.Cookies["user"]["user_accountname"]);
				favoriteId = Guid.Parse(id);
				userFavorite = _checkoutService.GetFavorite(favoriteId, accountName);
			} catch (Exception) {
				return View("Error");
			}
			DataViewModel dataViewModel = new DataViewModel {
				FavoriteId = favoriteId,
				IsPackage = userFavorite.IsPackage,
				Package = null,
				UserDefinedList = null,
				RoomTypeList = _checkoutService.GetRoomTypeList(),
				SquareFeetList = _checkoutService.GetSquareFeetList()
			};
			if (userFavorite.IsPackage) {
				dataViewModel.Package = _checkoutService.GetPackage(userFavorite);
			} else {
				dataViewModel.UserDefinedList = _checkoutService.GetUserDefinedList(userFavorite);
			}
			return View(dataViewModel);
		}
		[HttpPost]
		public ActionResult AddOrder(AllForm post) {
			DateTime now = DateTime.Now;
			string accountName;
			string productName;
			string merchantTradeNo = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 20);
			Guid favoriteId;
			decimal totalAmount;
			Guid? couponDetailId;

			if (post.UserForm.CouponDetailId == null) {
				couponDetailId = null;
			} else {
				couponDetailId = Guid.Parse(post.UserForm.CouponDetailId);
			}
			try {
				accountName = Helpers.DecodeCookie(Request.Cookies["user"]["user_accountname"]);
				favoriteId = Guid.Parse(post.UserForm.FavoriteId);
				_checkoutService.CheckAccountExist(accountName);
				_checkoutService.CheckFavoriteId(accountName, favoriteId);
				totalAmount = _checkoutService.GetTotalAmount(favoriteId);

				if (couponDetailId != null) {
					totalAmount -= _checkoutService.GetDiscountAmount(couponDetailId);
				}

				var result = _checkoutService.CreateOrder(post.UserForm, accountName, favoriteId, couponDetailId, totalAmount, merchantTradeNo, ref now, out productName);
				if (result.IsSuccessful) {
					//TODO 儲存最後一個訂單編號

				} else {
					throw new Exception("訂單建立失敗");
				}
			} catch (Exception ex) {
				return Json(ex.Message);
			}

			post.ECPayForm.ChoosePayment = "ALL";
			post.ECPayForm.EncryptType = "1";
			post.ECPayForm.ItemName = "uCleaner打掃服務";
			post.ECPayForm.MerchantID = "2000132";
			post.ECPayForm.MerchantTradeDate = now.ToString("yyyy/MM/dd HH:mm:ss");
			post.ECPayForm.MerchantTradeNo = merchantTradeNo;
			post.ECPayForm.OrderResultURL = "https://402f14af8bb4.ngrok.io" + "/Checkout/SuccessView";
			//TODO OrderResultURL
			post.ECPayForm.PaymentType = "aio";
			//TODO url文件化
			post.ECPayForm.ReturnURL = "https://fb6edfcb4a90.ngrok.io" + "/Checkout/ECPayReturn";
			post.ECPayForm.TotalAmount = Math.Round(totalAmount).ToString();
			post.ECPayForm.TradeDesc = HttpUtility.UrlEncode(productName);

			string HashKey = "5294y06JbISpM5x9";
			string HashIV = "v77hoKGq4kWxNNIS";
			string Parameters = string.Format("ChoosePayment={0}&EncryptType={1}&ItemName={2}&MerchantID={3}&MerchantTradeDate={4}&MerchantTradeNo={5}&OrderResultURL={6}&PaymentType={7}&ReturnURL={8}&TotalAmount={9}&TradeDesc={10}",
				post.ECPayForm.ChoosePayment,
				post.ECPayForm.EncryptType,
				post.ECPayForm.ItemName,
				post.ECPayForm.MerchantID,
				post.ECPayForm.MerchantTradeDate,
				post.ECPayForm.MerchantTradeNo,
				post.ECPayForm.OrderResultURL,
				post.ECPayForm.PaymentType,
				post.ECPayForm.ReturnURL,
				post.ECPayForm.TotalAmount,
				post.ECPayForm.TradeDesc
			);

			post.ECPayForm.CheckMacValue = GetCheckMacValue(HashKey, Parameters, HashIV);
			Debug.WriteLine($"MerchantTradeNo1: {post.ECPayForm.MerchantTradeNo}");

			return Json(post.ECPayForm);
		}
		private string GetCheckMacValue(string HashKey, string parameters, string HashIV) {
			string CheckMacValue = $"HashKey={HashKey}&{parameters}&HashIV={HashIV}";
			CheckMacValue = HttpUtility.UrlEncode(CheckMacValue).ToLower();

			//SHA256加密
			using (SHA256 sha256Hash = SHA256.Create()) {
				byte[] source = Encoding.UTF8.GetBytes(CheckMacValue);//將字串轉為Byte[]
				byte[] crypto = sha256Hash.ComputeHash(source);//加密
				StringBuilder builder = new StringBuilder();
				for (int i = 0; i < crypto.Length; i++) {
					builder.Append(crypto[i].ToString("X2"));
				}
				return builder.ToString();
			}
		}
		public string ECPayReturn() {
			string MerchantID = Request.Form["MerchantID"];
			string MerchantTradeNo = Request.Form["MerchantTradeNo"];
			string StoreID = Request.Form["StoreID"];
			string RtnCode = Request.Form["RtnCode"];
			string RtnMsg = Request.Form["RtnMsg"];
			string TradeNo = Request.Form["TradeNo"];
			string TradeAmt = Request.Form["TradeAmt"];
			string PaymentDate = Request.Form["PaymentDate"];
			string PaymentType = Request.Form["PaymentType"];
			string PaymentTypeChargeFee = Request.Form["PaymentTypeChargeFee"];
			string TradeDate = Request.Form["TradeDate"];
			string SimulatePaid = Request.Form["SimulatePaid"];
			string CheckMacValue = Request.Form["CheckMacValue"];
			
			bool isSuccess = RtnCode == "1" && SimulatePaid == "0";
			_checkoutService.UpdateOrder(MerchantTradeNo, TradeNo, PaymentType, isSuccess);

			Debug.WriteLine($"MerchantID: {MerchantID}");
			Debug.WriteLine($"MerchantTradeNo: {MerchantTradeNo}");
			Debug.WriteLine($"StoreID: {StoreID}");
			Debug.WriteLine($"RtnCode: {RtnCode}");
			Debug.WriteLine($"RtnMsg: {RtnMsg}");
			Debug.WriteLine($"TradeNo: {TradeNo}");
			Debug.WriteLine($"TradeAmt: {TradeAmt}");
			Debug.WriteLine($"PaymentDate: {PaymentDate}");
			Debug.WriteLine($"PaymentType: {PaymentType}");
			Debug.WriteLine($"PaymentTypeChargeFee: {PaymentTypeChargeFee}");
			Debug.WriteLine($"TradeDate: {TradeDate}");
			Debug.WriteLine($"SimulatePaid: {SimulatePaid}");
			Debug.WriteLine($"CheckMacValue: {CheckMacValue}");

			return "1|OK";
		}
		public ActionResult SuccessView() {
			return View();
		}
		public ActionResult AddCoupon() {
			_checkoutService.CreateCoupon(3);
			return null;
		}
		public ActionResult AddCouponDetail(int couponId) {
			string accountName = Helpers.DecodeCookie(Request.Cookies["user"]["user_accountname"]);
			_checkoutService.CreateCouponDetail(accountName, couponId);
			return null;
		}
		[HttpGet]
		public ActionResult GetCouponList() {
			try {
				string accountName = Helpers.DecodeCookie(Request.Cookies["user"]["user_accountname"]);
				var couponList = _checkoutService.GetCouponList(accountName);
				return Json(couponList, JsonRequestBehavior.AllowGet);
			} catch (Exception) {
				return View("Error");
			}
		}
		[HttpGet]
		public ActionResult GetDistricts() {
			return Json(CountyModels.County, JsonRequestBehavior.AllowGet);
		}
	}
	public class AllForm {
		public UserForm UserForm { get; set; }
		public ToECPayForm ECPayForm { get; set; }
	}
	public class UserForm {
		public string FavoriteId { get; set; }
		public string DateService { get; set; }
		public string FullName { get; set; }
		public string Phone { get; set; }
		public string Email { get; set; }
		public string County { get; set; }
		public string District { get; set; }
		public string Address { get; set; }
		public string Remark { get; set; }
		public string InvoiceType { get; set; }
		public string InvoiceDonateTo { get; set; }
		public string CouponDetailId { get; set; }
	}
	public class ToECPayForm {
		public string CheckMacValue { get; set; }
		public string ChoosePayment { get; set; }
		public string EncryptType { get; set; }
		public string ItemName { get; set; }
		public string MerchantID { get; set; }
		public string MerchantTradeDate { get; set; }
		public string MerchantTradeNo { get; set; }
		public string OrderResultURL { get; set; }
		public string PaymentType { get; set; }
		public string ReturnURL { get; set; }
		public string TotalAmount { get; set; }
		public string TradeDesc { get; set; }
	}
}
//TODO 分離AllForm
//TODO Request.Form大改
//TODO 付款成功頁面
//TODO 訂單編號A00000001
