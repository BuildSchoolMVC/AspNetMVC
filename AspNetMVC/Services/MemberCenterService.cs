using AspNetMVC.Models;
using AspNetMVC.Models.Entity;
using AspNetMVC.Models.WeDefinedException;
using AspNetMVC.Repository;
using AspNetMVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace AspNetMVC.Services
{
	public class MemberCenterService
	{
		private readonly UCleanerDBContext _context;
		private readonly BaseRepository _repository;

		private static readonly int countPerPage = 3;

		public MemberCenterService()
		{
			_context = new UCleanerDBContext();
			_repository = new BaseRepository(_context);
		}

		public IndexModel GetIndex(string accountName)
		{
			Account account = GetAccount(accountName);
			IndexModel indexData = new IndexModel
			{
				PaidCount = GetOrderCount(OrderState.Paid, account),
				CouponCount = _repository.GetAll<CouponDetail>()
												.Where(x => x.AccountName == account.AccountName && x.State == (int)UseState.Unused)
												.Count(),
				AccountName = account.AccountName,
				Phone = account.Phone,
				Email = account.Email,
				Address = account.Address,
			};
			return indexData;
		}
		public OperationResult ChangeInfo(InfoModel form, string accountName)
		{
			var result = new OperationResult();
			try
			{
				Account account = GetAccount(accountName);
				InfoValidation(form);

				account.Phone = form.Phone;
				account.Email = form.Email;
				account.Address = form.Address;

				_context.SaveChanges();
				result.IsSuccessful = true;
			}
			catch (Exception ex)
			{
				result.IsSuccessful = false;
				result.Exception = ex;
			}
			return result;
		}
		public void IsThirdParty(string accountName)
		{
			Account account = _repository.GetAll<Account>().First(x => x.AccountName == accountName);
			if (account.IsThirdParty)
			{
				throw new PermissionException("您使用第三方登入，無權進行此操作");
			}
		}
		public OperationResult ChangePassword(ChangePasswordModel form, string accountName)
		{
			var result = new OperationResult();
			try
			{
				Account account = GetAccount(accountName);
				PasswordValidation(form, account);

				account.Password = Helpers.ToMD5(form.NewPassword);

				_context.SaveChanges();
				result.IsSuccessful = true;
			}
			catch (Exception ex)
			{
				result.IsSuccessful = false;
				result.Exception = ex;
			}
			return result;
		}
		private void InfoValidation(InfoModel form)
		{
			Regex phoneRgx = new Regex(@"^09\d{8}$");
			Regex emailRgx = new Regex(@"^[\w\.\-]+\@[\w\.\-]+$");
			Regex addressRgx = new Regex(@"^.+$");

			if (!phoneRgx.IsMatch(form.Phone ?? ""))
			{
				throw new Exception("請輸入正確手機");
			}
			if (!emailRgx.IsMatch(form.Email ?? ""))
			{
				throw new Exception("請輸入正確信箱");
			}
			if (!addressRgx.IsMatch(form.Address ?? ""))
			{
				throw new Exception("請輸入正確地址");
			}
		}
		private void PasswordValidation(ChangePasswordModel form, Account account)
		{
			//後端驗證 舊密碼符合 新密碼符合規則
			if (account.Password != Helpers.ToMD5(form.OldPassword))
			{
				throw new Exception("舊密碼錯誤");
			}
			Regex aUpperCase = new Regex(@"^[a-zA-Z\d]*[A-Z][a-zA-Z\d]*$");
			Regex aLowerCase = new Regex(@"^[a-zA-Z\d]*[a-z][a-zA-Z\d]*$");
			Regex lengthLimit = new Regex(@"^[a-zA-Z\d]{6,15}$");

			if (!aUpperCase.IsMatch(form.NewPassword))
			{
				throw new Exception("需至少一位大寫");
			}
			else if (!aLowerCase.IsMatch(form.NewPassword))
			{
				throw new Exception("需至少一位小寫");
			}
			else if (!lengthLimit.IsMatch(form.NewPassword))
			{
				throw new Exception("長度需在6~15位之間");
			}
		}
		public IEnumerable<OrderBriefModel> GetOrderBrief(OrderState? state, int page, Account account)
		{
			IEnumerable<Order> userOrders;
			if (state == null)
			{
				userOrders = _repository.GetAll<Order>()
				.Where(x => x.AccountName == account.AccountName)
				.OrderByDescending(x => x.DateService)
				.Skip((page - 1) * countPerPage)
				.Take(countPerPage);
			}
			else
			{
				userOrders = _repository.GetAll<Order>()
				.Where(x => x.AccountName == account.AccountName && x.OrderState == (byte)state)
				.OrderByDescending(x => x.DateService)
				.Skip((page - 1) * countPerPage)
				.Take(countPerPage);
			}
			return (from o in userOrders
							join od in _repository.GetAll<OrderDetail>()
							on o.OrderId equals od.OrderId
							select new
							{
								o.OrderId,
								o.DateService,
								o.Address,
								o.OrderState,
								od.FinalPrice
							}).ToList()
							.OrderByDescending(x => x.DateService)
							.Select(x => new OrderBriefModel
							{
								OrderId = x.OrderId,
								DateService = x.DateService.ToString("yyyy-MM-dd,HH:mm"),
								Address = x.Address,
								OrderState = x.OrderState,
								FinalPrice = x.FinalPrice
							});
		}
		public OrderFullModel GetOrderFull(Guid orderId)
		{
			try
			{
				Order o = _repository.GetAll<Order>().First(x => x.OrderId == orderId);
				OrderDetail od = _repository.GetAll<OrderDetail>().First(x => x.OrderId == orderId);

				OrderFullModel orderFull = new OrderFullModel
				{
					DateService = o.DateService.ToString("yyyy-MM-dd,HH:mm"),
					Address = o.Address,
					OrderState = o.OrderState,
					FinalPrice = od.FinalPrice,
					FullName = o.FullName,
					Phone = o.Phone,
					Email = o.Email,
					CreateTime = o.CreateTime.ToString("yyyy-MM-dd,HH:mm"),
					InvoiceType = o.InvoiceType,
					InvoiceDonateTo = o.InvoiceDonateTo,
					ProductName = od.ProductName,
					Rate = o.Rate,
				};

				var couponDetail = _repository.GetAll<CouponDetail>()
					.FirstOrDefault(x => x.CouponDetailId == o.CouponDetailId);
				if (couponDetail == null)
				{
					orderFull.DiscountAmount = 0;
				}
				else
				{
					orderFull.DiscountAmount = _repository.GetAll<Coupon>()
						.First(x => x.CouponId == couponDetail.CouponId)
						.DiscountAmount;
				}

				switch (o.PaymentType)
				{
					case "Credit_CreditCard":
						orderFull.PaymentType = "信用卡";
						break;
					case "":
						orderFull.PaymentType = "未付款";
						break;
					default:
						orderFull.PaymentType = "其他";
						break;
				}

				OrderPackage package = _repository.GetAll<OrderPackage>().FirstOrDefault(x => x.OrderId == orderId);

				if (package != null)
				{
					orderFull.PackageModel = new PackageModel
					{
						RoomTypes = package.RoomTypes.Split(',').Select(x => int.Parse(x)).ToArray(),
						SquareFeets = package.SquareFeets.Split(',').Select(x => int.Parse(x)).ToArray(),
						ServiceItems = package.ServiceItems.Replace("+", " + "),
						Hour = package.Hour,
						Price = package.Price,
					};
				}
				else
				{
					var userDefined = _repository.GetAll<OrderUserDefined>().Where(x => x.OrderId == orderId);

					orderFull.UserDefinedList = new List<UserDefinedModel>();
					foreach (var item in userDefined)
					{
						orderFull.UserDefinedList.Add(new UserDefinedModel
						{
							RoomType = item.RoomType,
							SquareFeet = item.SquareFeet,
							ServiceItems = item.ServiceItems.Replace(",", " + "),
							Hour = item.Hour,
							Price = item.Price
						});
					}
				}
				return orderFull;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public int GetOrderCount(Account account)
		{
			return _repository.GetAll<Order>()
				.Where(x => x.AccountName == account.AccountName)
				.Count();
		}
		public int GetOrderCount(OrderState state, Account account)
		{
			return _repository.GetAll<Order>()
				.Where(x => x.AccountName == account.AccountName && x.OrderState == (byte)state)
				.Count();
		}
		public OperationResult SubmitComment(CommentModel form, Account account)
		{
			var result = new OperationResult();
			try
			{
				Order order = _repository.GetAll<Order>().First(x => x.OrderId == form.OrderId && x.AccountName == account.AccountName);

				order.Rate = form.Rate == 0 ? null : form.Rate;
				order.Comment = form.Comment == null ? string.Empty : form.Comment;

				_context.SaveChanges();
				result.IsSuccessful = true;
			}
			catch (Exception ex)
			{
				result.IsSuccessful = false;
				result.Exception = ex;
			}
			return result;
		}
		public Account GetAccount(string accountName)
		{
			try
			{
				return _repository.GetAll<Account>().First(x => x.AccountName == accountName);
			}
			catch (Exception)
			{
				throw new Exception("此帳號不存在");
			}
		}
	}
}