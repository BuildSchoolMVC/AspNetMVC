using AspNetMVC.Controllers;
using AspNetMVC.Models;
using AspNetMVC.Models.Entity;
using AspNetMVC.Repository;
using AspNetMVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspNetMVC.Services {
	public class CheckoutService {
		private readonly UCleanerDBContext _context;
		private readonly BaseRepository _repository;

		public CheckoutService() {
			_context = new UCleanerDBContext();
			_repository = new BaseRepository(_context);
		}
		public Order GetOrder(Guid orderId) {
			Order order = _repository.GetAll<Order>()
				.First(x => x.OrderId == orderId);
			return order;
		}
		public Order GetOrder(string merchantTradeNo) {
			Order order = _repository.GetAll<Order>()
				.First(x => x.MerchantTradeNo == merchantTradeNo);
			return order;
		}
		public OrderDetail GetOrderDetail(Order order) {
			OrderDetail od = _repository.GetAll<OrderDetail>()
				.First(x => x.OrderId == order.OrderId);
			return od;
		}
		public UserFavorite GetFavorite(OrderDetail od) {
			UserFavorite favorite = _repository.GetAll<UserFavorite>()
				.First(x => x.FavoriteId == od.FavoriteId);
			return favorite;
		}
		public UserFavorite GetFavorite(Guid favoriteId, string accountName) {
			UserFavorite favorite = _repository
				.GetAll<UserFavorite>()
				.First(f => f.FavoriteId == favoriteId && f.AccountName == accountName);
			return favorite;
		}
		public List<UserDefinedProduct> GetUserDefinedList(UserFavorite favorite) {
			List<UserDefinedProduct> data = _repository
				.GetAll<UserDefinedProduct>()
				.Where(x => x.UserDefinedId == favorite.UserDefinedId && x.IsDelete == false).ToList();
			return data;
		}
		public PackageProduct GetPackage(UserFavorite favorite) {
			PackageProduct data = _repository
				.GetAll<PackageProduct>()
				.First(x => x.PackageProductId == favorite.PackageProductId);
			return data;
		}
		public IEnumerable<RoomType> GetRoomTypeList() {
			return _repository.GetAll<RoomType>();
		}
		public IEnumerable<SquareFeet> GetSquareFeetList() {
			return _repository.GetAll<SquareFeet>();
		}
		public void CreateCoupon(int couponId) {
			DateTime now = DateTime.UtcNow.AddHours(8);
			_repository.Create<Coupon>(new Coupon {
				CouponId = couponId,
				CouponName = "新朋友享好禮",
				DiscountAmount = 100,
				DateStart = now,
				DateEnd = new DateTime(now.Year + 1, 1, 1),
				CreateTime = now,
				EditTime = now,
				CreateUser = "uCleaner",
				EditUser = "uCleaner",
			});
			_context.SaveChanges();
		}
		public decimal GetCouponAmount(Guid? couponDetailId) {
			if (couponDetailId == null) {
				return 0;
			}
			var couponDetail = _repository.GetAll<CouponDetail>()
				.First(x => x.CouponDetailId == couponDetailId);
			var coupon = _repository.GetAll<Coupon>()
				.First(x => x.CouponId == couponDetail.CouponId);
			return coupon.DiscountAmount;
		}
		public List<CouponJson> GetCouponList(string accountName) {
			var allCouponDetail = _repository
				.GetAll<CouponDetail>()
				.Where(x => x.AccountName == accountName);
			var cList = from cd in allCouponDetail
									where cd.State == (int)UseState.Unused
									join c in _repository.GetAll<Coupon>()
									on cd.CouponId equals c.CouponId
									orderby c.DateEnd
									select new {
										CouponDetailId = cd.CouponDetailId,
										CouponName = c.CouponName,
										DiscountAmount = c.DiscountAmount,
										DateEnd = c.DateEnd,
									};
			List<CouponJson> list = new List<CouponJson>();
			foreach (var item in cList) {
				list.Add(new CouponJson {
					CouponDetailId = item.CouponDetailId,
					CouponName = item.CouponName,
					DiscountAmount = item.DiscountAmount,
					DateEnd = item.DateEnd.ToString("yyyy.MM.dd"),
				});
			}
			return list;
		}
		public void SaveMerchantTradeNo(string merchantTradeNo) {
			_repository.GetAll<ECPayParam>().First().MerchantTradeNo = merchantTradeNo;
			_context.SaveChanges();
		}
		public string GetNextMerchantTradeNo() {
			//uCleanerA00000000000
			string logoA = "uCleanerA";
			ECPayParam ecpay = _repository.GetAll<ECPayParam>().First();
			char splitChar = ecpay.MerchantTradeNo[8];
			string[] logo_no = ecpay.MerchantTradeNo.Split(splitChar);

			while (true) {
				//上一次單號+1
				decimal decLastNo = decimal.Parse(ecpay.MerchantTradeNo.Substring(logoA.Length)) + 1;
				//破上限歸0
				if (decLastNo > 99999999999) {
					decLastNo = 0;
					splitChar++;
				}
				logo_no[1] = decLastNo.ToString().PadLeft(11, '0');

				string tempNo = $"{logo_no[0]}{splitChar}{logo_no[1]}";
				//檢查是否存在重複
				Order o = _repository.GetAll<Order>()
							.FirstOrDefault(x => x.MerchantTradeNo == tempNo);
				if (o == null) {
					return tempNo;
				} else if (tempNo == $"{logo_no[0]}A00000000000") {
					throw new Exception("單號全部重複");
				} else {
					ecpay.MerchantTradeNo = tempNo;
				}
			}
		}
		public decimal GetDiscountAmount(Guid? couponDetailId) {
			int couponId = _repository.GetAll<CouponDetail>()
				.First(x => x.CouponDetailId == couponDetailId)
				.CouponId;
			return _repository.GetAll<Coupon>()
				.First(x => x.CouponId == couponId)
				.DiscountAmount;
		}
		public OperationResult CreateOrder(UserForm userForm, OrderData data, out string productName) {
			var result = new OperationResult();
			//尋找是否有可用Coupon
			CouponDetail couponDetail = CheckCoupon(data.AccountName, data.CouponDetailId);
			if (couponDetail == null) {
				data.CouponDetailId = null;
			} else {
				data.CouponDetailId = couponDetail.CouponDetailId;
			}
			byte? invoiceDonateTo;
			if (userForm.InvoiceDonateTo == null) {
				invoiceDonateTo = null;
			} else {
				invoiceDonateTo = byte.Parse(userForm.InvoiceDonateTo);
			}
			UserFavorite favorite = _repository.GetAll<UserFavorite>()
				.First(x => x.FavoriteId == data.FavoriteId);
			PackageProduct package = null;
			List<UserDefinedProduct> userDefinedList = null;

			if (favorite.IsPackage) {
				package = GetPackage(favorite);
				productName = package.Name;
			} else {
				userDefinedList = GetUserDefinedList(favorite);
				productName = userDefinedList[0].Name;
			}
			using (var transaction = _context.Database.BeginTransaction()) {
				try {
					Order order = new Order {
						OrderId = Guid.NewGuid(),
						AccountName = data.AccountName,
						FullName = userForm.FullName,
						Email = userForm.Email,
						Phone = userForm.Phone,
						DateService = DateTime.Parse(userForm.DateService),
						Address = $"{userForm.County}{userForm.District}{userForm.Address}",
						Remark = userForm.Remark == null ? string.Empty : userForm.Remark,
						OrderState = (byte)OrderState.Unpaid,
						Rate = null,
						Comment = string.Empty,
						CouponDetailId = data.CouponDetailId,
						PaymentType = string.Empty,
						InvoiceType = byte.Parse(userForm.InvoiceType),
						InvoiceDonateTo = invoiceDonateTo,
						MerchantTradeNo = data.MerchantTradeNo,
						TradeNo = string.Empty,
						CreateTime = data.Now,
						EditTime = data.Now,
						CreateUser = data.AccountName,
						EditUser = data.AccountName,
					};
					_repository.Create<Order>(order);
					_context.SaveChanges();

					OrderDetail od = new OrderDetail {
						OrderDetailId = Guid.NewGuid(),
						OrderId = order.OrderId,
						FavoriteId = data.FavoriteId,
						FinalPrice = data.FinalPrice,
						ProductName = productName,
						CreateTime = data.Now,
						EditTime = data.Now,
						CreateUser = data.AccountName,
						EditUser = data.AccountName,
					};
					_repository.Create<OrderDetail>(od);
					_context.SaveChanges();

					if (favorite.IsPackage) {
						List<int?> roomTypes = new List<int?> { package.RoomType, package.RoomType2 };
						if (package.RoomType3 >= 0) roomTypes.Add(package.RoomType3);
						List<int?> squareFeets = new List<int?> { package.Squarefeet, package.Squarefeet2 };
						if (package.Squarefeet3 >= 0) squareFeets.Add(package.Squarefeet3);

						OrderPackage orderPackage = new OrderPackage {
							OrderId = order.OrderId,
							RoomTypes = string.Join(",", roomTypes),
							SquareFeets = string.Join(",", squareFeets),
							ServiceItems = package.ServiceItem,
							Hour = package.Hour,
							Price = package.Price,
							CreateTime = data.Now,
							EditTime = data.Now,
							CreateUser = data.AccountName,
							EditUser = data.AccountName,
						};
						_repository.Create<OrderPackage>(orderPackage);
					} else {
						foreach (var item in userDefinedList) {
							OrderUserDefined orderUserDefined = new OrderUserDefined {
								OrderId = order.OrderId,
								RoomType = item.RoomType,
								SquareFeet = item.Squarefeet,
								ServiceItems = item.ServiceItems,
								Hour = item.Hour,
								Price = item.Price,
								CreateTime = data.Now,
								EditTime = data.Now,
								CreateUser = data.AccountName,
								EditUser = data.AccountName,
							};
							_repository.Create<OrderUserDefined>(orderUserDefined);
						}
					}
					_context.SaveChanges();

					if (couponDetail != null) {
						couponDetail.State = (int)UseState.Used;
						_context.SaveChanges();
					}

					result.IsSuccessful = true;
					transaction.Commit();

				} catch (Exception ex) {
					result.IsSuccessful = false;
					result.Exception = ex;
					transaction.Rollback();
				}
			}
			return result;
		}
		public OperationResult RepayUpdateOrder(Order order, OrderDetail od, Dictionary<string, string> newDictionary) {
			var result = new OperationResult();
			using (var transaction = _context.Database.BeginTransaction()) {
				try {
					order.MerchantTradeNo = newDictionary["MerchantTradeNo"];
					order.EditTime = DateTime.UtcNow.AddHours(8);
					_context.SaveChanges();

					od.EditTime = DateTime.UtcNow.AddHours(8);
					_context.SaveChanges();

					result.IsSuccessful = true;
					transaction.Commit();

				} catch (Exception ex) {
					result.IsSuccessful = false;
					result.Exception = ex;
					transaction.Rollback();
				}
			}
			return result;
		}
		public bool CheckIsUnpaid(Guid orderId) {
			Order order = GetOrder(orderId);
			return order.OrderState == (byte)OrderState.Unpaid;
		}
		public decimal GetTotalPrice(Guid favoriteId) {
			UserFavorite f = _repository.GetAll<UserFavorite>().First(x => x.FavoriteId == favoriteId);
			if (f.IsPackage) {
				return _repository
					.GetAll<PackageProduct>()
					.First(x => x.PackageProductId == f.PackageProductId)
					.Price;
			} else {
				return _repository
					.GetAll<UserDefinedProduct>()
					.Where(x => x.UserDefinedId == f.UserDefinedId)
					.Sum(x => x.Price);
			}
		}
		public void UpdateOrder(string merchantTradeNo, string tradeNo, string paymentType, bool isOK) {
			Order o = _repository.GetAll<Order>().First(x => x.MerchantTradeNo == merchantTradeNo);
			if (isOK) {
				o.OrderState = (byte)OrderState.Paid;
				o.PaymentType = paymentType;
			}
			o.TradeNo = tradeNo;

			_context.SaveChanges();
		}
		public void CheckAccountExist(string accountName) {
			//帳號不存在拋例外
			try {
				_repository.GetAll<Account>().First(x => x.AccountName == accountName);
			} catch (Exception) {
				throw new Exception("帳號不存在");
			}
		}
		public void CheckFavoriteId(string accountName, Guid favoriteId) {
			//收藏Id與帳號不符拋例外
			try {
				_repository.GetAll<UserFavorite>()
					.First(x => x.AccountName == accountName && x.FavoriteId == favoriteId);
			} catch (Exception) {
				throw new Exception("收藏不存在");
			}
		}
		public CouponDetail CheckCoupon(string accountName, Guid? couponDetailId) {
			return _repository.GetAll<CouponDetail>()
				.FirstOrDefault(x => x.AccountName == accountName && x.CouponDetailId == couponDetailId && x.State == (int)UseState.Unused);
		}
		public void CreateCouponDetail(string accountName, CouponNum couponId) {
			DateTime now = DateTime.UtcNow.AddHours(8);
			_repository.Create<CouponDetail>(new CouponDetail {
				CouponDetailId = Guid.NewGuid(),
				CouponId = (int)couponId,
				AccountName = accountName,
				State = (int)UseState.Unused,
				CreateTime = now,
				EditTime = now,
				CreateUser = accountName,
				EditUser = accountName,
			});
			_context.SaveChanges();
		}
	}
}