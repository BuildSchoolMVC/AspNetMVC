using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using AspNetMVC.Models;

namespace AspNetMVC.Repository
{
    public class BaseRepository
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
        }
        public virtual void Update<T>(T entity) where T : class
        {
            _context.Entry(entity).State = EntityState.Modified;
        }
        public virtual void Delete<T>(T entity) where T : class
        {
            _context.Entry(entity).State = EntityState.Deleted;
        }

        public IQueryable<TSource> GetAll<TSource>() where TSource : class
        {
            return _context.Set<TSource>();
        }   
    }
}