using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AspNetMVC.Repository;
using AspNetMVC.ViewModel;
using AspNetMVC.Models;
using AspNetMVC.Models.Entity;

namespace AspNetMVC.Service
{
    public class CustomerServiceService
    {
        private readonly UCleanerDBContext _context;
        private readonly BaseRepository<CustomerService> _repository;

        /// <summary>
        /// 實體化處理連線的物件及資料庫(包含CRUD等方法)
        /// </summary>
        public CustomerServiceService()
        {
            _context = new UCleanerDBContext();
            _repository = new BaseRepository<CustomerService>(_context);
        }

        /// <summary>
        /// 新增客服資料到資料表
        /// </summary>
        /// <param name="c">從前端表單所有收集的資料</param>
        public void CreateData(CustomerViewModel c)
        {
            _repository.Create(new CustomerService
            {
                CustomerServiceId = Guid.NewGuid(),
                Name = c.Name,
                Phone = c.Phone,
                Email = c.Email,
                Category = c.Category,
                Content = c.Content,
                IsRead = false,
                CreateUser = c.Name, //建立後不可能有改變
                CreateTime = DateTime.UtcNow.AddHours(8),//轉換我們時區之時間
                EditUser = c.Name,//第一次建立即為編輯者，後續可改變
                EditTime = DateTime.UtcNow.AddHours(8),
            });
        }
        /// <summary>
        /// 顯示該資料表所有資料
        /// </summary>
        /// <returns></returns>
        public List<CustomerService> ShowData() => _repository.GetAll().ToList();

        /// <summary>
        ///  查看單筆資料
        /// </summary>
        /// <param name="id"></param>
        /// <param name="user">編輯者</param>
        /// <returns></returns>
        public CustomerService ReadContent(Guid? id,string user)
        {
            var customer = _repository.GetAll().FirstOrDefault(x => x.CustomerServiceId == id);
            customer.IsRead = true;
            customer.EditTime = DateTime.UtcNow.AddHours(8);
            customer.EditUser = user;
            _repository.Update(customer);
            return customer;
        }
    }
}