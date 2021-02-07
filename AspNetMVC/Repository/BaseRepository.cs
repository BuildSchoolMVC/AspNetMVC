using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using AspNetMVC.Models;

namespace AspNetMVC.Repository
{
    public class BaseRepository<TSource> where TSource : class
    {
        private readonly DbContext _context;

        protected DbContext Context
        {
            get
            {
                return _context;
            }
        }

        public BaseRepository(UCleanerDBContext context)
        {
            _context = context ?? throw new ArgumentNullException();
        }
        public virtual void Create<T>(T entity) where T : class
        {
            _context.Entry(entity).State = EntityState.Added;
            _context.SaveChanges();
        }
        public virtual void Update<T>(T entity) where T : class
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }
        public virtual void Delete<T>(T entity) where T : class
        {
            _context.Entry(entity).State = EntityState.Deleted;
            _context.SaveChanges();
        }

        public IQueryable<TSource> GetAll()
        {
            return _context.Set<TSource>();
        }
    }
}