﻿using AspNetMVC.Models;
using AspNetMVC.Models.Entity;
using AspNetMVC.Repository;
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
		public UserFavorite GetFavorite(Guid favoriteId, string accountName) {
			UserFavorite favorite = _repository
				.GetAll<UserFavorite>()
				.First(f => f.FavoriteId == favoriteId && f.AccountName == accountName);
			return favorite;
		}
		public List<UserDefinedProduct> GetUserDefinedList(UserFavorite favorite) {
			List<UserDefinedProduct> data = _repository
				.GetAll<UserDefinedProduct>()
				.Where(x => x.UserDefinedId == favorite.UserDefinedId).ToList();
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
			DateTime now = DateTime.Now;
			_repository.Create<Coupon>(new Coupon {
				CouponId = couponId,
				CouponName = "母親節優惠券",
				DiscountAmount = 150,
				DateStart = new DateTime(2021, 5, 1),
				DateEnd = new DateTime(2021, 5, 31),
				CreateTime = now,
				EditTime = now,
				CreateUser = "blender222",
				EditUser = "blender222",
			});
			_context.SaveChanges();
		}
		public void CreateCouponDetail(string accountName, int couponId) {
			DateTime now = DateTime.Now;
			_repository.Create<CouponDetail>(new CouponDetail {
				CouponDetailId = Guid.NewGuid(),
				CouponId = couponId,
				AccountName = accountName,
				State = (int)UseState.Unused,
				CreateTime = now,
				EditTime = now,
				CreateUser = accountName,
				EditUser = accountName,
			});
			_context.SaveChanges();
		}
		public List<CouponJson> GetCouponList(string accountName) {
			var allCouponDetail = _repository
				.GetAll<CouponDetail>()
				.Where(x => x.AccountName == accountName);
			var allCoupon = _repository.GetAll<Coupon>();
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
							//State = cd.State
						};
			List<CouponJson> list = new List<CouponJson>();
			foreach (var item in cList) {
				list.Add(new CouponJson {
					CouponDetailId = item.CouponDetailId,
					CouponName = item.CouponName,
					DiscountAmount = item.DiscountAmount,
					DateEnd = item.DateEnd.ToString("yyyy.MM.dd"),
					//State = item.State
				});
			}
			return list;
		}
		public void GetTotalAmount(string accountName) {

		}
		public OperationResult AddOrder(Controllers.UserForm userForm, string accountName) {
			//檢查帳號存在
			_repository.GetAll<Account>().First(x => x.AccountName == accountName);

			var result = new OperationResult();
			using (var transcation = _context.Database.BeginTransaction()) {
				try {
					Order order = new Order {
						OrderId = Guid.NewGuid(),
						AccountName = accountName,
						FullName = userForm.FullName,
						Email = userForm.Email,
						Phone = userForm.Phone,
						DateService = DateTime.Parse(userForm.DateService),
						Address = $"{userForm.County}{userForm.District}{userForm.Address}",
						Remark = userForm.Remark,
						OrderState = (byte)OrderState.PendingPayment,
						Rate = null,
						Comment = string.Empty,
						CouponId = Guid.Parse(userForm.CouponId),
						PaymentMethod = (byte)PayMethod.ECPay,
						InvoiceType = byte.Parse(userForm.InvoiceType),
						InvoiceDonateTo = byte.Parse(userForm.InvoiceDonateTo),
					};
					_repository.Create<Order>(order);
					_context.SaveChanges();
					OrderDetail od = new OrderDetail {
						OrderDetailId = Guid.NewGuid(),
						OrderId = order.OrderId,
						//UserDefinedId = ,
						//PackageProductId = ,
						//ProductPrice = ,
						//ProductName = ,
						//IsPackage = ,
					};
					_repository.Create<OrderDetail>(od);
					_context.SaveChanges();
					
					result.IsSuccessful = true;
					transcation.Commit();

				} catch (Exception ex) {
					result.IsSuccessful = false;
					result.Exception = ex;
					transcation.Rollback();
				}
			}
			return result;
		}
	}
	public class CouponJson {
		public Guid CouponDetailId { get; set; }
		public string CouponName { get; set; }
		public decimal DiscountAmount { get; set; }
		public string DateEnd { get; set; }
		public int State { get; set; }
	}
}