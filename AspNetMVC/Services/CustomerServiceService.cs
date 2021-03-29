using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AspNetMVC.Repository;
using AspNetMVC.ViewModels;
using AspNetMVC.Models;
using AspNetMVC.Models.Entity;

namespace AspNetMVC.Services
{
    public class CustomerServiceService
    {
        private readonly UCleanerDBContext _context;
        private readonly BaseRepository _repository;

        /// <summary>
        /// 實體化處理連線的物件及資料庫(包含CRUD等方法)
        /// </summary>
        public CustomerServiceService()
        {
            _context = new UCleanerDBContext();
            _repository = new BaseRepository(_context);
        }

        /// <summary>
        /// 新增客服資料到資料表
        /// </summary>
        /// <param name="c">從前端表單所有收集的資料</param>
        public void CreateData(CustomerViewModel cVM)
        {
            var result = new OperationResult();

            try
            {
                _repository.Create(new CustomerService
                {
                    CustomerServiceId = Guid.NewGuid(),
                    Name = cVM.Name,
                    Phone = cVM.Phone,
                    Email = cVM.Email,
                    Category = cVM.Category,
                    Content = cVM.Content,
                    IsRead = false,
                    CreateUser = cVM.Name, //建立後不可改變
                    CreateTime = DateTime.UtcNow.AddHours(8),//轉換為我們時區之時間
                    EditUser = cVM.Name,//第一次建立時即為編輯者，後續可改變
                    EditTime = DateTime.UtcNow.AddHours(8),
                });
                _context.SaveChanges();
                result.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                result.IsSuccessful = false;
                result.Exception = ex;
            }
        }
        /// <summary>
        /// 顯示該資料表所有資料
        /// </summary>
        /// <returns></returns>
        public List<CustomerService> ShowAllData() => _repository.GetAll<CustomerService>().ToList();

        /// <summary>
        ///  查看單筆資料
        /// </summary>
        /// <param name="id"></param>
        /// <param name="user">編輯者</param>
        /// <returns></returns>
        public CustomerService ShowSingleData(Guid? id,string user)
        {
            var customer = _repository.GetAll<CustomerService>().FirstOrDefault(x => x.CustomerServiceId == id);
            customer.IsRead = true;
            customer.EditTime = DateTime.UtcNow.AddHours(8);
            customer.EditUser = user;
            _repository.Update(customer);
            _context.SaveChanges();
            return customer;
        }
    }
}