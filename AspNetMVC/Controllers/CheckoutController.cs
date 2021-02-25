using AspNetMVC.Models;
using AspNetMVC.Models.Entity;
using AspNetMVC.Services;
using AspNetMVC.ViewModels;
using ECPay.Payment.Integration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
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
		//public ActionResult ToECPay() {
		//	List<string> enErrors = new List<string>();
		//	try {
		//		using (AllInOne oPayment = new AllInOne()) {
		//			/* 服務參數 */
		//			oPayment.ServiceMethod = HttpMethod.HttpPOST;//介接服務時，呼叫 API 的方法
		//			oPayment.ServiceURL = "https://payment-stage.ecpay.com.tw/Cashier/AioCheckOut/V5";//要呼叫介接服務的網址
		//			oPayment.HashKey = "5294y06JbISpM5x9";//ECPay提供的Hash Key
		//			oPayment.HashIV = "v77hoKGq4kWxNNIS";//ECPay提供的Hash IV
		//			oPayment.MerchantID = "2000132";//ECPay提供的特店編號

		//			/* 基本參數 */
		//			oPayment.Send.ReturnURL = "http://example.com";//付款完成通知回傳的網址
		//			oPayment.Send.ClientBackURL = "http://www.ecpay.com.tw/";//瀏覽器端返回的廠商網址
		//			oPayment.Send.OrderResultURL = "";//瀏覽器端回傳付款結果網址
		//			oPayment.Send.MerchantTradeNo = "ECPay" + new Random().Next(0, 99999).ToString();//廠商的交易編號
		//			oPayment.Send.MerchantTradeDate = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");//廠商的交易時間
		//			oPayment.Send.TotalAmount = Decimal.Parse("1000");//交易總金額
		//			oPayment.Send.TradeDesc = "交易描述";//交易描述
		//			oPayment.Send.ChoosePayment = PaymentMethod.ALL;//使用的付款方式
		//			oPayment.Send.Remark = "";//備註欄位
		//			oPayment.Send.ChooseSubPayment = PaymentMethodItem.None;//使用的付款子項目
		//			oPayment.Send.NeedExtraPaidInfo = ExtraPaymentInfo.No;//是否需要額外的付款資訊
		//			oPayment.Send.DeviceSource = DeviceType.PC;//來源裝置
		//			oPayment.Send.IgnorePayment = ""; //不顯示的付款方式
		//			oPayment.Send.PlatformID = "";//特約合作平台商代號
		//			oPayment.Send.HoldTradeAMT = HoldTradeType.Yes;
		//			oPayment.Send.CustomField1 = "";
		//			oPayment.Send.CustomField2 = "";
		//			oPayment.Send.CustomField3 = "";
		//			oPayment.Send.CustomField4 = "";
		//			oPayment.Send.EncryptType = 1;

		//			//訂單的商品資料
		//			oPayment.Send.Items.Add(new Item() {
		//				Name = "蘋果",//商品名稱
		//				Price = Decimal.Parse("1000"),//商品單價
		//				Currency = "新台幣",//幣別單位
		//				Quantity = Int32.Parse("1"),//購買數量
		//				URL = "http://google.com",//商品的說明網址

		//			});

		//			/*************************非即時性付款:ATM、CVS 額外功能參數**************/

		//			#region ATM 額外功能參數

		//			//oPayment.SendExtend.ExpireDate = 3;//允許繳費的有效天數
		//			//oPayment.SendExtend.PaymentInfoURL = "";//伺服器端回傳付款相關資訊
		//			//oPayment.SendExtend.ClientRedirectURL = "";//Client 端回傳付款相關資訊

		//			#endregion


		//			#region CVS 額外功能參數

		//			//oPayment.SendExtend.StoreExpireDate = 3; //超商繳費截止時間 CVS:以分鐘為單位 BARCODE:以天為單位
		//			//oPayment.SendExtend.Desc_1 = "test1";//交易描述 1
		//			//oPayment.SendExtend.Desc_2 = "test2";//交易描述 2
		//			//oPayment.SendExtend.Desc_3 = "test3";//交易描述 3
		//			//oPayment.SendExtend.Desc_4 = "";//交易描述 4
		//			//oPayment.SendExtend.PaymentInfoURL = "";//伺服器端回傳付款相關資訊
		//			//oPayment.SendExtend.ClientRedirectURL = "";///Client 端回傳付款相關資訊

		//			#endregion

		//			/***************************信用卡額外功能參數***************************/

		//			#region Credit 功能參數

		//			//oPayment.SendExtend.BindingCard = BindingCardType.No; //記憶卡號
		//			//oPayment.SendExtend.MerchantMemberID = ""; //記憶卡號識別碼
		//			//oPayment.SendExtend.Language = "ENG"; //語系設定

		//			#endregion Credit 功能參數

		//			#region 一次付清

		//			//oPayment.SendExtend.Redeem = false;   //是否使用紅利折抵
		//			//oPayment.SendExtend.UnionPay = true; //是否為銀聯卡交易

		//			#endregion

		//			#region 分期付款

		//			//oPayment.SendExtend.CreditInstallment = 3;//刷卡分期期數

		//			#endregion 分期付款

		//			#region 定期定額

		//			//oPayment.SendExtend.PeriodAmount = 1000;//每次授權金額
		//			//oPayment.SendExtend.PeriodType = PeriodType.Day;//週期種類
		//			//oPayment.SendExtend.Frequency = 1;//執行頻率
		//			//oPayment.SendExtend.ExecTimes = 2;//執行次數
		//			//oPayment.SendExtend.PeriodReturnURL = "";//伺服器端回傳定期定額的執行結果網址。

		//			#endregion


		//			/* 產生訂單 */
		//			enErrors.AddRange(oPayment.CheckOut());
		//		}
		//	} catch (Exception ex) {
		//		// 例外錯誤處理。
		//		enErrors.Add(ex.Message);
		//	} finally {
		//		// 顯示錯誤訊息。
		//		if (enErrors.Count() > 0) {
		//			// string szErrorMessage = String.Join("\\r\\n", enErrors);
		//		}
		//	}
		//	return null;
		//}
		public ActionResult FromECPay() {
			string urlString = "https://payment-stage.ecpay.com.tw/Cashier/AioCheckOut/V5";

			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlString);
			request.ContentType = "application/x-www-form-urlencoded";
			request.Method = "POST";

			string MerchantID = "2000132";
			string MerchantTradeNo = "ecPayTest1234";
			string MerchantTradeDate = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
			string PaymentType = "aio";
			string TotalAmount = "1200";
			string TradeDesc = HttpUtility.UrlEncode("交易描述");
			string ItemName = "豆花 60 元 x 1";
			string ReturnURL = "https://78ca04b1a9f6.ap.ngrok.io";
			string ChoosePayment = "ALL";
			string CheckMacValue = "CFA9BDE377361FBDD8F160274930E815D1A8A2E3E80CE7D404C45FC9A0A1E407";
			string EncryptType = "1";
			
			var postData = $"MerchantID={MerchantID}&MerchantTradeNo={MerchantTradeNo}&MerchantTradeDate={MerchantTradeDate}&PaymentType={PaymentType}&TotalAmount={TotalAmount}&TradeDesc={TradeDesc}&ItemName={ItemName}&ReturnURL={ReturnURL}&ChoosePayment={ChoosePayment}&CheckMacValue={CheckMacValue}&EncryptType={EncryptType}";

			byte[] byteArray = Encoding.UTF8.GetBytes(postData);
			request.ContentLength = byteArray.Length;

			Stream dataStream = request.GetRequestStream();
			dataStream.Write(byteArray, 0, byteArray.Length);
			dataStream.Close();

			WebResponse response = request.GetResponse();



			Debug.WriteLine(((HttpWebResponse)response).StatusDescription);
			StreamReader streamReader = new StreamReader(response.GetResponseStream());
			Debug.WriteLine(streamReader.ReadToEnd());



			//HttpWebResponse response = (HttpWebResponse)request.GetResponse();
			//Stream myRequestStream = request.GetRequestStream();
			//StreamWriter myStreamWriter = new StreamWriter(myRequestStream, Encoding.GetEncoding("gb2312"));
			//myStreamWriter.Write(postDataStr);
			//myStreamWriter.Close();

			//response.Cookies = cookie.GetCookies(response.ResponseUri);
			//Stream myResponseStream = response.GetResponseStream();
			//StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
			//string retString = myStreamReader.ReadToEnd();
			//myStreamReader.Close();
			//myResponseStream.Close();

			return null;
		}
		public ActionResult TestNgrok() {

			Debug.WriteLine("ngrok777");
			return null;
		}
		public async void TestAsync() {
			//string urlString = "https://payment-stage.ecpay.com.tw/Cashier/AioCheckOut/V5";
			//HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlString);
			//request.Method = "POST";
			//request.ContentType = "application/x-www-form-urlencoded";
			//HttpWebResponse response = (HttpWebResponse)request.GetResponse();
			//string rpStr = response.ToString();

			HttpClient client = new HttpClient();
			Dictionary<string, string> data = new Dictionary<string, string> {
				{ "0", "111" },
				{ "1", "222" }
			};
			var content = new FormUrlEncodedContent(data);
			var response = await client.PostAsync("", content);
			var responseString = await response.Content.ReadAsStringAsync();

			//Debug.WriteLine(responseString);
		}
		public ActionResult TestResponse() {

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
}