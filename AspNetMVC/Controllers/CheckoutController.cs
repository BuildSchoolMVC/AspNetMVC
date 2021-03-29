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
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace AspNetMVC.Controllers
{
	public class CheckoutController : Controller
	{
		private readonly CheckoutService _checkoutService;
		private static readonly HttpClient client = new HttpClient();

		public CheckoutController()
		{
			_checkoutService = new CheckoutService();
		}
		[HttpGet]
		public ActionResult Index(string id)
		{
			string accountName;
			Guid favoriteId;
			UserFavorite userFavorite;

			try
			{
				accountName = Helpers.DecodeCookie(Request.Cookies["user"]["user_accountname"]);
				favoriteId = Guid.Parse(id);
				userFavorite = _checkoutService.GetFavorite(favoriteId, accountName);
			}
			catch (Exception)
			{
				return View("Error");
			}
			DataViewModel dataViewModel = new DataViewModel
			{
				FavoriteId = favoriteId,
				IsPackage = userFavorite.IsPackage,
				Package = null,
				UserDefinedList = null,
				RoomTypeList = _checkoutService.GetRoomTypeList(),
				SquareFeetList = _checkoutService.GetSquareFeetList()
			};
			if (userFavorite.IsPackage)
			{
				dataViewModel.Package = _checkoutService.GetPackage(userFavorite);
			}
			else
			{
				dataViewModel.UserDefinedList = _checkoutService.GetUserDefinedList(userFavorite);
			}
			return View(dataViewModel);
		}
		[HttpPost]
		public ActionResult AddOrder(UserForm post)
		{
			DateTime now = DateTime.UtcNow.AddHours(8);
			string accountName;
			string productName;
			string url = WebConfigurationManager.AppSettings["WebsiteUrl"];
			string merchantTradeNo;
			Guid favoriteId;
			decimal finalAmount;
			Guid? couponDetailId;

			if (post.CouponDetailId == null)
			{
				couponDetailId = null;
			}
			else
			{
				couponDetailId = Guid.Parse(post.CouponDetailId);
			}
			try
			{
				accountName = Helpers.DecodeCookie(Request.Cookies["user"]["user_accountname"]);
				favoriteId = Guid.Parse(post.FavoriteId);
				_checkoutService.CheckAccountExist(accountName);
				_checkoutService.CheckFavoriteId(accountName, favoriteId);
				finalAmount = _checkoutService.GetTotalPrice(favoriteId);

				if (couponDetailId != null)
				{
					finalAmount -= _checkoutService.GetDiscountAmount(couponDetailId);
				}

				merchantTradeNo = _checkoutService.GetNextMerchantTradeNo();
				OrderData orderData = new OrderData
				{
					AccountName = accountName,
					FavoriteId = favoriteId,
					CouponDetailId = couponDetailId,
					FinalPrice = finalAmount,
					MerchantTradeNo = merchantTradeNo,
					Now = now,
				};
				var result = _checkoutService.CreateOrder(post, orderData, out productName);

				if (result.IsSuccessful)
				{
					_checkoutService.SaveMerchantTradeNo(merchantTradeNo);
				}
				else
				{
					throw new Exception("訂單建立失敗");
				}
			}
			catch (Exception ex)
			{
				return Json(ex.Message);
			}

			ECPayForm ecpayForm = new ECPayForm
			{
				ChoosePayment = "ALL",
				EncryptType = "1",
				ItemName = "uCleaner打掃服務",
				MerchantID = "2000132",
				MerchantTradeDate = now.ToString("yyyy/MM/dd HH:mm:ss"),
				MerchantTradeNo = merchantTradeNo,
				OrderResultURL = url + "/Checkout/Success",
				PaymentType = "aio",
				ReturnURL = url + "/Checkout/ECPayReturn",

				TotalAmount = Math.Round(finalAmount).ToString(),
				TradeDesc = HttpUtility.UrlEncode(productName),
			};

			string HashKey = "5294y06JbISpM5x9";
			string HashIV = "v77hoKGq4kWxNNIS";
			Dictionary<string, string> paramList = new Dictionary<string, string> {
				{ "ChoosePayment", ecpayForm.ChoosePayment },
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
			ecpayForm.CheckMacValue = GetCheckMacValue(HashKey, Parameters, HashIV);

			return Json(ecpayForm);
		}
		public async Task<ActionResult> Repay(string orderId)
		{
			Guid orderGuid = Guid.Parse(orderId);
			Order order = _checkoutService.GetOrder(orderGuid);
			OrderDetail od = _checkoutService.GetOrderDetail(order);

			//雙重檢查，檢查資料庫訂單狀態、綠界的付款狀態
			if (!_checkoutService.CheckIsUnpaid(orderGuid))
			{
				string errorMsg = JsonConvert.SerializeObject(new
				{
					IsSuccessful = false,
					Message = "此筆訂單已付款"
				});
				return Content(errorMsg, "application/json");
			}
			if (DateTime.UtcNow.AddHours(8).Date >= order.DateService.Date)
			{
				string errorMsg = JsonConvert.SerializeObject(new
				{
					IsSuccessful = false,
					Message = "訂單已過付款期限，需在服務日前一天以前付款"
				});
				return Content(errorMsg, "application/json");
			}

			ECPayForm form = GetECPayForm(orderGuid);

			//向綠界查詢訂單，確認此訂單未付款
			var dictionary = new Dictionary<string, string> {
				{ "MerchantID", form.MerchantID },
				{ "MerchantTradeNo", form.MerchantTradeNo },
				{ "TimeStamp", DateTimeOffset.Now.ToUnixTimeSeconds().ToString() },
			};
			string Parameters = string.Join("&", dictionary.Select(x => $"{x.Key}={x.Value}").OrderBy(x => x));

			string HashKey = "5294y06JbISpM5x9";
			string HashIV = "v77hoKGq4kWxNNIS";
			string checkMacValue = GetCheckMacValue(HashKey, Parameters, HashIV);

			dictionary.Add("CheckMacValue", checkMacValue);
			var content = new FormUrlEncodedContent(dictionary);

			var response = await client.PostAsync("https://payment-stage.ecpay.com.tw/Cashier/QueryTradeInfo/V5", content);
			string responseStr = await response.Content.ReadAsStringAsync();

			var paramArray = responseStr.Split('&');
			var responseDictionary = new Dictionary<string, string>();
			foreach (var param in paramArray)
			{
				var kv = param.Split('=');
				responseDictionary.Add(kv[0], kv[1]);
			}

			if (responseDictionary["TradeStatus"] == "1")
				return Content($"此筆訂單已付款");

			//確認完畢，給新的MerchantTradeNo
			form.MerchantTradeDate = DateTime.UtcNow.AddHours(8).ToString("yyyy/MM/dd HH:mm:ss");
			form.MerchantTradeNo = _checkoutService.GetNextMerchantTradeNo();

			var newDictionary = new Dictionary<string, string> {
				{ "ChoosePayment", form.ChoosePayment },
				{ "EncryptType", form.EncryptType },
				{ "ItemName", form.ItemName },
				{ "MerchantID", form.MerchantID },
				{ "MerchantTradeDate", form.MerchantTradeDate },
				{ "MerchantTradeNo", form.MerchantTradeNo },
				{ "OrderResultURL", form.OrderResultURL },
				{ "PaymentType", form.PaymentType },
				{ "ReturnURL", form.ReturnURL },
				{ "TotalAmount", form.TotalAmount },
				{ "TradeDesc", form.TradeDesc },
			};

			string newParameters = string.Join("&", newDictionary.Select(x => $"{x.Key}={x.Value}").OrderBy(x => x));
			form.CheckMacValue = GetCheckMacValue(HashKey, newParameters, HashIV);
			//1.更新資料庫原訂單
			var result = _checkoutService.RepayUpdateOrder(order, od, newDictionary);

			//儲存上一次使用的MerchantTradeNo
			if (result.IsSuccessful)
			{
				_checkoutService.SaveMerchantTradeNo(newDictionary["MerchantTradeNo"]);
			}
			else
			{
				throw new Exception("訂單建立失敗");
			}

			string paramsJson = JsonConvert.SerializeObject(form);
			//2.送回參數在綠界重新建立訂單
			return Content(paramsJson, "application/json");
		}
		private ECPayForm GetECPayForm(Guid orderId)
		{
			Order order = _checkoutService.GetOrder(orderId);
			OrderDetail od = _checkoutService.GetOrderDetail(order);

			string merchantTradeNo = order.MerchantTradeNo;
			string url = WebConfigurationManager.AppSettings["WebsiteUrl"];
			decimal finalAmount = od.FinalPrice;
			string productName = od.ProductName;

			ECPayForm ecpayForm = new ECPayForm
			{
				ChoosePayment = "ALL",
				EncryptType = "1",
				ItemName = "uCleaner打掃服務",
				MerchantID = "2000132",
				MerchantTradeDate = DateTime.UtcNow.AddHours(8).ToString("yyyy/MM/dd HH:mm:ss"),
				MerchantTradeNo = merchantTradeNo,
				OrderResultURL = url + "/Checkout/Success",
				PaymentType = "aio",
				ReturnURL = url + "/Checkout/ECPayReturn",

				TotalAmount = Math.Round(finalAmount).ToString(),
				TradeDesc = HttpUtility.UrlEncode(productName),
			};

			string HashKey = "5294y06JbISpM5x9";
			string HashIV = "v77hoKGq4kWxNNIS";
			Dictionary<string, string> paramList = new Dictionary<string, string> {
				{ "ChoosePayment", ecpayForm.ChoosePayment },
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
			ecpayForm.CheckMacValue = GetCheckMacValue(HashKey, Parameters, HashIV);

			return ecpayForm;
		}
		private string GetCheckMacValue(string HashKey, string parameters, string HashIV)
		{
			string CheckMacValue = $"HashKey={HashKey}&{parameters}&HashIV={HashIV}";
			CheckMacValue = HttpUtility.UrlEncode(CheckMacValue).ToLower();

			//SHA256加密
			using (SHA256 sha256Hash = SHA256.Create())
			{
				byte[] source = Encoding.UTF8.GetBytes(CheckMacValue);//將字串轉為Byte[]
				byte[] crypto = sha256Hash.ComputeHash(source);//加密
				StringBuilder builder = new StringBuilder();
				for (int i = 0; i < crypto.Length; i++)
				{
					builder.Append(crypto[i].ToString("X2"));
				}
				return builder.ToString();
			}
		}
		public string ECPayReturn()
		{
			string MerchantTradeNo = Request.Form["MerchantTradeNo"];
			string RtnCode = Request.Form["RtnCode"];
			string TradeNo = Request.Form["TradeNo"];
			string PaymentType = Request.Form["PaymentType"];
			string SimulatePaid = Request.Form["SimulatePaid"];

			bool isSuccess = RtnCode == "1" && SimulatePaid == "0";
			_checkoutService.UpdateOrder(MerchantTradeNo, TradeNo, PaymentType, isSuccess);

			foreach (var key in Request.Form.AllKeys)
			{
				Debug.WriteLine($"foreach: {key}, {Request.Form[key]}");
			}
			return "1|OK";
		}
		public ActionResult Success()
		{
			string MerchantTradeNo = Request.Form["MerchantTradeNo"] ?? "";
			string TradeNo = Request.Form["TradeNo"] ?? "";
			string RtnCode = Request.Form["RtnCode"];

			foreach (var key in Request.Form.AllKeys)
			{
				Debug.WriteLine($"success: {key}, {Request.Form[key]}");
			}
			if (RtnCode != "1")
			{
				ViewData["Title"] = "付款失敗";
				ViewData["Content"] = "付款失敗，請前往會員中心，並重新付款";
				return View("Message");
			}
			return RedirectToAction("SuccessView", new { MerchantTradeNo = MerchantTradeNo });
		}
		public ActionResult SuccessView(string MerchantTradeNo)
		{
			Order order = _checkoutService.GetOrder(MerchantTradeNo);
			OrderDetail od = _checkoutService.GetOrderDetail(order);
			UserFavorite userF = _checkoutService.GetFavorite(od);

			SuccessViewModel viewModel = new SuccessViewModel
			{
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
			if (userF.IsPackage)
			{
				viewModel.Package = _checkoutService.GetPackage(userF);
			}
			else
			{
				viewModel.UserDefinedList = _checkoutService.GetUserDefinedList(userF);
			}

			return View(viewModel);
		}
		public ActionResult AddCoupon(int id = 1)
		{
			_checkoutService.CreateCoupon(id);
			return null;
		}
		public ActionResult AddCouponDetail(int id = 1)
		{
			string accountName = Helpers.DecodeCookie(Request.Cookies["user"]["user_accountname"]);
			_checkoutService.CreateCouponDetail(accountName,(CouponNum)id);
			return null;
		}
		[HttpGet]
		public ActionResult GetCouponList()
		{
			try
			{
				string accountName = Helpers.DecodeCookie(Request.Cookies["user"]["user_accountname"]);
				var couponList = _checkoutService.GetCouponList(accountName);
				return Json(couponList, JsonRequestBehavior.AllowGet);
			}
			catch (Exception)
			{
				return View("Error");
			}
		}
		[HttpGet]
		public ActionResult GetDistricts()
		{
			return Json(CountyModels.County, JsonRequestBehavior.AllowGet);
		}
	}
}
