using AspNetMVC.Models;
using AspNetMVC.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AspNetMVC.Controllers {
	public class CheckoutController : Controller {
		// GET: Checkout
		public ActionResult Index() {
			return View();
		}
		public ActionResult GetDistricts() {
			List<CountyModels> county = new List<CountyModels>();
			county.Add(new CountyModels("台北市", new List<string>() {
				"中正區", "大同區", "中山區", "松山區", "大安區", "萬華區",
				"信義區", "士林區", "北投區", "內湖區", "南港區", "文山區",
			}));
			county.Add(new CountyModels("新北市", new List<string>() {
				"萬里區", "金山區", "板橋區", "汐止區", "深坑區", "石碇區", "瑞芳區", "平溪區",
				"雙溪區", "貢寮區", "新店區", "坪林區", "烏來區", "永和區", "中和區", "土城區",
				"三峽區", "樹林區", "鶯歌區", "三重區", "新莊區", "泰山區", "林口區", "蘆洲區",
				"五股區", "八里區", "淡水區", "三芝區", "石門區",
			}));
			return Json(county);
		}
		[HttpPost]
		public ActionResult GetOrder(FormCollection submit) {
			//var aa = order.;

			return Json(new { title = "預約成功", content = "服務人員將在預約時間前1小時內與您聯繫" });
		}
		public ActionResult TestLike() {
			//using (var context = new ) {
			var obj = new UserFavorite { CreateTime = DateTime.Now };
			//}
			return Json("");
		}
	}
}