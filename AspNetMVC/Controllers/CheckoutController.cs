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
		public ActionResult ToECPay(AllForm post) {
			//檢查
			string accountName;
			Guid favoriteId;
			try {
				accountName = Helpers.DecodeCookie(Request.Cookies["user"]["user_accountname"]);
				//從資料庫依據accountName取得價格
				//favoriteId = Guid.Parse(id);
				//= _checkoutService.GetTotalAmount(favoriteId, accountName);
			} catch (Exception) {
				return View("Error");
			}
			//建立訂單
			


			post.ChoosePayment = "ALL";
			post.EncryptType = "1";
			post.ItemName = HttpUtility.UrlEncode("");
			post.MerchantID = "2000132";
			post.MerchantTradeDate = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
			post.MerchantTradeNo = Guid.NewGuid().ToString().Substring(0, 20);
			post.PaymentType = "aio";
			post.ReturnURL = "https://872599f54008.ngrok.io" + "/Checkout/TestResponse";
			post.TotalAmount = "1000";
			post.TradeDesc = HttpUtility.UrlEncode("打掃服務");

			string HashKey = "5294y06JbISpM5x9";
			string HashIV = "v77hoKGq4kWxNNIS";
			string Parameters = $"ChoosePayment={post.ChoosePayment}&EncryptType={post.EncryptType}&ItemName={post.ItemName}&MerchantID={post.MerchantID}&MerchantTradeDate={post.MerchantTradeDate}&MerchantTradeNo={post.MerchantTradeNo}&PaymentType={post.PaymentType}&ReturnURL={post.ReturnURL}&TotalAmount={post.TotalAmount}&TradeDesc={post.TradeDesc}";
			post.CheckMacValue = GetCheckMacValue(HashKey, Parameters, HashIV);

			return Json(post);
		}
		private string NoUse() {
			string urlString = "https://payment-stage.ecpay.com.tw/Cashier/AioCheckOut/V5";

			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlString);
			request.ContentType = "application/x-www-form-urlencoded";
			request.Method = "POST";

			string ChoosePayment = "ALL";
			string EncryptType = "1";
			string ItemName = HttpUtility.UrlEncode("uCleaner");
			string MerchantID = "2000132";
			string MerchantTradeDate = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
			string MerchantTradeNo = "durkzodkfjafdaf00009";
			string PaymentType = "aio";
			string ReturnURL = "https://872599f54008.ngrok.io" + "/Checkout/TestResponse";
			string TotalAmount = "1200";
			string TradeDesc = HttpUtility.UrlEncode("desc");

			string HashKey = "5294y06JbISpM5x9";
			string HashIV = "v77hoKGq4kWxNNIS";
			string Parameters = $"ChoosePayment={ChoosePayment}&EncryptType={EncryptType}&ItemName={ItemName}&MerchantID={MerchantID}&MerchantTradeDate={MerchantTradeDate}&MerchantTradeNo={MerchantTradeNo}&PaymentType={PaymentType}&ReturnURL={ReturnURL}&TotalAmount={TotalAmount}&TradeDesc={TradeDesc}";
			string CheckMacValue = GetCheckMacValue(HashKey, Parameters, HashIV);

			//var postData = $"CheckMacValue={CheckMacValue}&ChoosePayment={ChoosePayment}&EncryptType={EncryptType}&ItemName={ItemName}&MerchantID={MerchantID}&MerchantTradeDate={MerchantTradeDate}&MerchantTradeNo={MerchantTradeNo}&PaymentType={PaymentType}&ReturnURL={ReturnURL}&TotalAmount={TotalAmount}&TradeDesc={TradeDesc}";

			//byte[] byteArray = Encoding.UTF8.GetBytes(postData);
			//request.ContentLength = byteArray.Length;

			//Stream dataStream = request.GetRequestStream();
			//dataStream.Write(byteArray, 0, byteArray.Length);
			//dataStream.Close();

			//WebResponse response = request.GetResponse();


			//Debug.WriteLine(((HttpWebResponse)response).StatusDescription);
			//StreamReader streamReader = new StreamReader(response.GetResponseStream());
			//Debug.WriteLine(response.ContentType);
			//Debug.WriteLine(response.Headers);
			//Debug.WriteLine(response.ResponseUri);

			//return Content(streamReader.ReadToEnd());
			return null;
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
					builder.Append(crypto[i].ToString("x2"));
				}
				string result = builder.ToString().ToUpper();
				return result;
			}
		}
		public ActionResult TestNgrok() {
			
			return null;
		}
		public ActionResult TestResponse() {
			Debug.WriteLine("ngrok777");
			return null;
		}
		[HttpPost]
		public ActionResult AddOrder(FormCollection submit) {

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
		public ActionResult AddCoupon() {
			_checkoutService.CreateCoupon(3);
			return null;
		}
		public ActionResult AddCouponDetail() {
			string accountName = Helpers.DecodeCookie(Request.Cookies["user"]["user_accountname"]);
			_checkoutService.CreateCouponDetail(accountName, 2);
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
		public UserForm userForm { get; set; }
		public string CheckMacValue { get; set; }
		public string ChoosePayment { get; set; }
		public string EncryptType { get; set; }
		public string ItemName { get; set; }
		public string MerchantID { get; set; }
		public string MerchantTradeDate { get; set; }
		public string MerchantTradeNo { get; set; }
		public string PaymentType { get; set; }
		public string ReturnURL { get; set; }
		public string TotalAmount { get; set; }
		public string TradeDesc { get; set; }
	}
	public class UserForm {
		public string FullName { get; set; }
		public string Address { get; set; }
		public string ServiceDate { get; set; }

	}
}