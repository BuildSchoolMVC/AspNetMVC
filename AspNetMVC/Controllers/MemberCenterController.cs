using AspNetMVC.Models.Entity;
using AspNetMVC.Models.WeDefinedException;
using AspNetMVC.Services;
using AspNetMVC.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AspNetMVC.Controllers
{
	public class MemberCenterController : Controller
	{
		private readonly MemberCenterService _memberCenterService;

		public MemberCenterController()
		{
			_memberCenterService = new MemberCenterService();
		}

		[HttpGet]
		public ActionResult Index()
		{
			try
			{
				string accountName = Helpers.DecodeCookie(Request.Cookies["user"]["user_accountname"]);
				IndexModel data = _memberCenterService.GetIndex(accountName);
				return View(data);
			}
			catch (Exception)
			{
				return RedirectToAction("Login", "Account");
			}
		}
		[HttpPost]
		public ActionResult ChangeInfo(InfoModel form)
		{
			string accountName;
			try
			{
				accountName = Helpers.DecodeCookie(Request.Cookies["user"]["user_accountname"]);
			}
			catch (Exception)
			{
				return RedirectToAction("Login", "Account");
			}
			var result = _memberCenterService.ChangeInfo(form, accountName);

			var response = new
			{
				result.IsSuccessful,
				Message = result.Exception == null ? "" : result.Exception.Message
			};
			return Json(response);
		}
		[HttpPost]
		public ActionResult ChangePassword(ChangePasswordModel form)
		{
			string accountName;
			try
			{
				accountName = Helpers.DecodeCookie(Request.Cookies["user"]["user_accountname"]);
				_memberCenterService.IsThirdParty(accountName);
			}
			catch (PermissionException ex)
			{
				var res = new { IsSuccesful = false, ex.Message };
				return Json(res);
			}
			catch (Exception)
			{
				return RedirectToAction("Login", "Account");
			}
			var result = _memberCenterService.ChangePassword(form, accountName);

			var response = new
			{
				result.IsSuccessful,
				Message = result.Exception == null ? "" : result.Exception.Message
			};
			return Json(response);
		}
		[HttpGet]
		public ActionResult GetOrderBrief(string sort, int page = 1)
		{
			try
			{
				string accountName = Helpers.DecodeCookie(Request.Cookies["user"]["user_accountname"]);
				Account account = _memberCenterService.GetAccount(accountName);

				IEnumerable<OrderBriefModel> orderBriefs = null;
				switch (sort)
				{
					case "all":
						orderBriefs = _memberCenterService.GetOrderBrief(null, page, account);
						break;
					case "paid":
						orderBriefs = _memberCenterService.GetOrderBrief(OrderState.Paid, page, account);
						break;
					case "unpaid":
						orderBriefs = _memberCenterService.GetOrderBrief(OrderState.Unpaid, page, account);
						break;
					case "finished":
						orderBriefs = _memberCenterService.GetOrderBrief(OrderState.Finished, page, account);
						break;
					default:
						throw new Exception("狀態錯誤");
				}

				int orderCount = _memberCenterService.GetOrderCount(account);
				int paidCount = _memberCenterService.GetOrderCount(OrderState.Paid, account);
				int unpaidCount = _memberCenterService.GetOrderCount(OrderState.Unpaid, account);
				int finishedCount = _memberCenterService.GetOrderCount(OrderState.Finished, account);

				string response = JsonConvert.SerializeObject(new
				{
					orderCount,
					paidCount,
					unpaidCount,
					finishedCount,
					orderBriefs
				});
				return Content(response, "application/json");
			}
			catch (Exception)
			{
				throw;
			}
		}
		[HttpGet]
		public ActionResult GetOrderFull(string orderId)
		{
			Guid orderGuid = Guid.Parse(orderId);
			try
			{
				string accountName = Helpers.DecodeCookie(Request.Cookies["user"]["user_accountname"]);
				Account account = _memberCenterService.GetAccount(accountName);

				OrderFullModel orderFull = _memberCenterService.GetOrderFull(orderGuid);

				string response = JsonConvert.SerializeObject(orderFull);
				return Content(response, "application/json");
			}
			catch (Exception)
			{
				throw;
			}
		}
		[HttpPost]
		public ActionResult SubmitComment(CommentModel form)
		{
			try
			{
				string accountName = Helpers.DecodeCookie(Request.Cookies["user"]["user_accountname"]);
				Account account = _memberCenterService.GetAccount(accountName);

				var result = _memberCenterService.SubmitComment(form, account);

				var response = new
				{
					result.IsSuccessful,
					Message = result.Exception == null ? "" : result.Exception.Message
				};
				return Json(response);
			}
			catch (Exception)
			{
				return View("Error");
			}
		}
		[HttpGet]
		public ActionResult GetCouponList()
		{
			try
			{
				var checkoutService = new CheckoutService();
				string accountName = Helpers.DecodeCookie(Request.Cookies["user"]["user_accountname"]);

				var list = checkoutService.GetCouponList(accountName);
				var response = list.Select(x => new { x.CouponName, x.DateEnd, x.DiscountAmount });

				return Json(response, JsonRequestBehavior.AllowGet);
			}
			catch (Exception ex)
			{
				return new HttpStatusCodeResult(500, ex.Message);
			}
		}
	}
}