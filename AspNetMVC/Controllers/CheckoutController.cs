using AspNetMVC.Models;
using AspNetMVC.Models.Entity;
using AspNetMVC.Services;
using AspNetMVC.ViewModels;
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
		public ActionResult AddOrder(UserForm post) {
			DateTime now = DateTime.Now;
			string accountName;
			string productName;
			string url = System.IO.File.ReadAllText(Server.MapPath(@"~/App_data/Config/url.txt")); ;
			string merchantTradeNo;
			Guid favoriteId;
			decimal finalAmount;
			Guid? couponDetailId;

			if (post.CouponDetailId == null) {
				couponDetailId = null;
			} else {
				couponDetailId = Guid.Parse(post.CouponDetailId);
			}
			try {
				accountName = Helpers.DecodeCookie(Request.Cookies["user"]["user_accountname"]);
				favoriteId = Guid.Parse(post.FavoriteId);
				_checkoutService.CheckAccountExist(accountName);
				_checkoutService.CheckFavoriteId(accountName, favoriteId);
				finalAmount = _checkoutService.GetTotalPrice(favoriteId);

				if (couponDetailId != null) {
					finalAmount -= _checkoutService.GetDiscountAmount(couponDetailId);
				}

				merchantTradeNo = _checkoutService.GetNextMerchantTradeNo();
				OrderData orderData = new OrderData {
					AccountName = accountName,
					FavoriteId = favoriteId,
					CouponDetailId = couponDetailId,
					FinalPrice = finalAmount,
					MerchantTradeNo = merchantTradeNo,
					Now = now,
				};
				var result = _checkoutService.CreateOrder(post, orderData, out productName);

				if (result.IsSuccessful) {
					_checkoutService.SaveMerchantTradeNo(merchantTradeNo);
				} else {
					throw new Exception("訂單建立失敗");
				}
			} catch (Exception ex) {
				return Json(ex.Message);
			}

			ECPayForm ecpayForm = new ECPayForm();
			ecpayForm.ChoosePayment = "ALL";
			ecpayForm.EncryptType = "1";
			ecpayForm.ItemName = "uCleaner打掃服務";
			ecpayForm.MerchantID = "2000132";
			ecpayForm.MerchantTradeDate = now.ToString("yyyy/MM/dd HH:mm:ss");
			ecpayForm.MerchantTradeNo = merchantTradeNo;
			ecpayForm.OrderResultURL = "https://localhost:44308" + "/Checkout/SuccessView";
			ecpayForm.PaymentType = "aio";
			ecpayForm.ReturnURL = url + "/Checkout/ECPayReturn";
			ecpayForm.TotalAmount = Math.Round(finalAmount).ToString();
			ecpayForm.TradeDesc = HttpUtility.UrlEncode(productName);

			string HashKey = "5294y06JbISpM5x9";
			string HashIV = "v77hoKGq4kWxNNIS";
			Dictionary<string, string> paramList = new Dictionary<string, string> {
				{ "ChoosePayment", ecpayForm.ChoosePayment },
				//{ "ClientBackURL", ecpayForm.ClientBackURL },
				{ "EncryptType", ecpayForm.EncryptType },
				{ "ItemName", ecpayForm.ItemName },
				{ "MerchantID", ecpayForm.MerchantID },
				{ "MerchantTradeDate", ecpayForm.MerchantTradeDate },
				{ "MerchantTradeNo", ecpayForm.MerchantTradeNo },
				{ "OrderResultURL", ecpayForm.OrderResultURL },
				{ "PaymentType", ecpayForm.PaymentType },
				{ "ReturnURL", ecpayForm.ReturnURL },
				{ "TotalAmount", ecpayForm.TotalAmount },
				{ "TradeDesc", ecpayForm.TradeDesc },
			};
			string Parameters = string.Join("&", paramList.Select(x => $"{x.Key}={x.Value}").OrderBy(x => x));

			//string Parameters = string.Format("ChoosePayment={0}&ClientBackURL={1}&EncryptType={2}&ItemName={3}&MerchantID={4}&MerchantTradeDate={5}&MerchantTradeNo={6}&PaymentType={7}&ReturnURL={8}&TotalAmount={9}&TradeDesc={10}",
			//	ecpayForm.ChoosePayment,
			//	ecpayForm.ClientBackURL,
			//	ecpayForm.EncryptType,
			//	ecpayForm.ItemName,
			//	ecpayForm.MerchantID,
			//	ecpayForm.MerchantTradeDate,
			//	ecpayForm.MerchantTradeNo,
			//	//ecpayForm.OrderResultURL,
			//	ecpayForm.PaymentType,
			//	ecpayForm.ReturnURL,
			//	ecpayForm.TotalAmount,
			//	ecpayForm.TradeDesc
			//);

			ecpayForm.CheckMacValue = GetCheckMacValue(HashKey, Parameters, HashIV);

			return Json(ecpayForm);
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
			string MerchantTradeNo = Request.Form["MerchantTradeNo"];
			string RtnCode = Request.Form["RtnCode"];
			string TradeNo = Request.Form["TradeNo"];
			string PaymentType = Request.Form["PaymentType"];
			string SimulatePaid = Request.Form["SimulatePaid"];
			string CheckMacValue = Request.Form["CheckMacValue"];
			//string MerchantID = Request.Form["MerchantID"];
			//string StoreID = Request.Form["StoreID"];
			//string RtnMsg = Request.Form["RtnMsg"];
			//string TradeAmt = Request.Form["TradeAmt"];
			//string PaymentDate = Request.Form["PaymentDate"];
			//string PaymentTypeChargeFee = Request.Form["PaymentTypeChargeFee"];
			//string TradeDate = Request.Form["TradeDate"];

			bool isSuccess = RtnCode == "1" && SimulatePaid == "0";
			_checkoutService.UpdateOrder(MerchantTradeNo, TradeNo, PaymentType, isSuccess);

			foreach (var key in Request.Form.AllKeys) {
				Debug.WriteLine($"foreach: {key}, {Request.Form[key]}");
			}
			return "1|OK";
		}
		public ActionResult SuccessView() {
			string MerchantTradeNo = Request.Form["MerchantTradeNo"] ?? "";
			string TradeNo = Request.Form["TradeNo"] ?? "";
			//string RtnCode = Request.Form["RtnCode"];
			//string PaymentType = Request.Form["PaymentType"];
			//string SimulatePaid = Request.Form["SimulatePaid"];
			//string CheckMacValue = Request.Form["CheckMacValue"];
			//string MerchantID = Request.Form["MerchantID"];
			//string StoreID = Request.Form["StoreID"];
			//string RtnMsg = Request.Form["RtnMsg"];
			//string TradeAmt = Request.Form["TradeAmt"];
			//string PaymentDate = Request.Form["PaymentDate"];
			//string PaymentTypeChargeFee = Request.Form["PaymentTypeChargeFee"];
			//string TradeDate = Request.Form["TradeDate"];

			foreach (var key in Request.Form.AllKeys) {
				Debug.WriteLine($"success: {key}, {Request.Form[key]}");
			}

			Order order = _checkoutService.GetOrder(MerchantTradeNo, TradeNo);
			OrderDetail od = _checkoutService.GetOrderDetail(order);
			UserFavorite userF = _checkoutService.GetFavorite(od);

			SuccessViewModel viewModel = new SuccessViewModel {
				FavoriteId = userF.FavoriteId,
				IsPackage = userF.IsPackage,
				Package = null,
				UserDefinedList = null,
				RoomTypeList = _checkoutService.GetRoomTypeList(),
				SquareFeetList = _checkoutService.GetSquareFeetList(),
				DateService = order.DateService,
				Address = order.Address,
				DiscountAmount = _checkoutService.GetCouponAmount(order.CouponDetailId),
				FinalPrice = od.FinalPrice,
			};
			if (userF.IsPackage) {
				viewModel.Package = _checkoutService.GetPackage(userF);
			} else {
				viewModel.UserDefinedList = _checkoutService.GetUserDefinedList(userF);
			}

			return View(viewModel);
		}
		public ActionResult AddCoupon(int id = 1) {
			_checkoutService.CreateCoupon(id);
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
	public class ECPayForm {
		public string CheckMacValue { get; set; }
		public string ChoosePayment { get; set; }
		public string ClientBackURL { get; set; }
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
	public class OrderData {
		public string AccountName;
		public Guid FavoriteId;
		public Guid? CouponDetailId;
		public decimal FinalPrice;
		public string MerchantTradeNo;
		public DateTime Now;
	}
}
//TODO 服務條款
