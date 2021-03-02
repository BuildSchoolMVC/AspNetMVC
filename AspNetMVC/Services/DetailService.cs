using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Configuration;
using AspNetMVC.Models;
using AspNetMVC.Models.Entity;
using AspNetMVC.Repository;
using AspNetMVC.ViewModels;

namespace AspNetMVC.Services
{
    public class DetailService
    {
        private readonly UCleanerDBContext _context;
        private readonly BaseRepository _repository;

        public DetailService()
        {
            _context = new UCleanerDBContext();
            _repository = new BaseRepository(_context);
        }

        public DetailViewModel GetPackageProduct(int? id)
        {
            if (id != null)
            {
                var result = _repository.GetAll<PackageProduct>().FirstOrDefault(x => x.PackageProductId == id);

                if (result != null)
                {
                    var detailPageVM = new DetailViewModel
                    {
                        Id = result.PackageProductId,
                        Description = result.Description,
                        Hour = result.Hour,
                        Name = result.Name,
                        PhotoUrl = result.PhotoUrl,
                        Price = result.Price.ToString("###,###"),
                        ServiceItem = result.ServiceItem.Replace("+", "、"),
                        RoomName = RoomTypeSwitch(result.RoomType),
                        RoomName2 = RoomTypeSwitch(result.RoomType2),
                        RoomName3 = RoomTypeSwitch(result.RoomType3),
                        SquarefeetName = SquarefeetSwitch(result.Squarefeet),
                        SquarefeetName2 = SquarefeetSwitch(result.Squarefeet2),
                        SquarefeetName3 = SquarefeetSwitch(result.Squarefeet3)
                    };
                    return detailPageVM;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public OperationResult CreateComment(string account, int productId, int star, string comment)
        {
            var result = new OperationResult();
            Guid AccountId = _repository.GetAll<Account>().FirstOrDefault(x => x.AccountName == account).AccountId;

            try
            {
                if (AccountId != null)
                {
                    _repository.Create<Comment>(new Comment
                    {
                        CommentId = Guid.NewGuid(),
                        AccountId = AccountId,
                        PackageProductId = productId,
                        CreateTime = DateTime.UtcNow.AddHours(8),
                        CreateUser = account,
                        EditTime = DateTime.UtcNow.AddHours(8),
                        EditUser = account,
                        Star = star,
                        Content = comment
                    });
                    _context.SaveChanges();
                    result.IsSuccessful = true;
                    result.MessageInfo = "成功新增評論";
                }
                else
                {
                    result.IsSuccessful = false;
                    result.MessageInfo = "查無此人";
                }

            }
            catch (Exception ex)
            {
                result.IsSuccessful = false;
                result.Exception = ex;
            }
            return result;
        }

        public List<CommentViewModel> GetComment(int? productId)
        {
            if (productId != null)
            {
                var result = from comment in _context.Comments
                             where comment.PackageProductId == productId
                             join account in _context.Accounts on comment.AccountId equals account.AccountId
                             select new CommentViewModel
                             {
                                 CommentId = comment.CommentId,
                                 AccountName = account.AccountName,
                                 Content = comment.Content,
                                 CreateTime = comment.CreateTime,
                                 Star = comment.Star
                             };

                return result.OrderByDescending(x => x.CreateTime).ToList();
            }
            else
            {
                return null;
            }
        }
        public OperationResult DeleteComment(Guid? id, string accountName)
        {

            var user = _repository.GetAll<Account>().FirstOrDefault(x => x.AccountName == accountName);

            var queryResult = _repository.GetAll<Comment>().FirstOrDefault(x => x.CommentId == id && x.AccountId == user.AccountId);

            var operationResult = new OperationResult();

            if (id == null)
            {
                operationResult.IsSuccessful = false;
                operationResult.Status = OperationResultStatus.ErrorRequest;
                operationResult.MessageInfo = OperationResultStatus.ErrorRequest.ToString();
            }

            try
            {
                if (queryResult != null)
                {
                    _repository.Delete<Comment>(queryResult);
                    _context.SaveChanges();
                    operationResult.IsSuccessful = true;
                    operationResult.Status = OperationResultStatus.Success;
                    operationResult.MessageInfo = OperationResultStatus.Success.ToString();
                }
                else
                {
                    operationResult.IsSuccessful = false;
                    operationResult.Status = OperationResultStatus.NotFound;
                    operationResult.MessageInfo = OperationResultStatus.NotFound.ToString();
                }
            }
            catch (Exception ex)
            {
                operationResult.IsSuccessful = false;
                operationResult.Exception = ex;
                operationResult.Status = OperationResultStatus.ErrorRequest;
                operationResult.MessageInfo = OperationResultStatus.ErrorRequest.ToString();
            }
            return operationResult;
        }

        public OperationResult AddFavoriteAndDirectToCheckout(int id, string cookieValue)
        {
            var result = new OperationResult();
            try
            {
                var url = $"{WebConfigurationManager.AppSettings["WebsiteUrl"]}/ProductPage/CreateFavoriteData";
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                var cookies = new CookieContainer();
                var cookie = new Cookie();
                cookie.Name = "user";
                cookie.Value = $"user_accountname={cookieValue}";
                cookie.Domain = $"{WebConfigurationManager.AppSettings["WebsiteUrl"]}";
                cookies.Add(cookie);
                var username = Helpers.DecodeCookie(cookieValue);

                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.CookieContainer = cookies;

                NameValueCollection postParams = HttpUtility.ParseQueryString(string.Empty);

                postParams.Add("PackageProductId", id.ToString());

                byte[] byteArray = Encoding.UTF8.GetBytes(postParams.ToString());
                using (Stream reqStream = request.GetRequestStream())
                {
                    reqStream.Write(byteArray, 0, byteArray.Length);
                }

                var responseStr = "";

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    using (StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                    {
                        responseStr = sr.ReadToEnd();
                    }
                }

                var query = _repository.GetAll<UserFavorite>().OrderByDescending(x => x.CreateTime).FirstOrDefault(x => x.PackageProductId == id && x.AccountName == username);

                result.IsSuccessful = true;
                result.MessageInfo = query.FavoriteId.ToString();
            }
            catch (Exception ex)
            {
                result.IsSuccessful = false;
                result.Exception = ex;
                result.MessageInfo = ex.ToString();
            }
            return result;
        }
        private string RoomTypeSwitch(int? value)
        {
            return value == 0 ? "廚房" :
                   value == 1 ? "客廳" :
                   value == 2 ? "臥室" :
                   value == 3 ? "浴廁" :
                   value == 4 ? "陽台" : "-";
        }

        private string SquarefeetSwitch(int? value)
        {
            return value == 0 ? "5坪以下" :
                   value == 1 ? "6-10坪" :
                   value == 2 ? "11-15坪" :
                   value == 3 ? "16坪以上" : "-";
        }
    }
}