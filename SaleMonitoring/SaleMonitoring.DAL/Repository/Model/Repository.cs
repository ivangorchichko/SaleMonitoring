using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using SaleMonitoring.DAL.DataBaseContext;
using SaleMonitoring.DAL.Repository.Contract;
using SaleMonitoring.DomainModel.Contract;
using SaleMonitoring.DomainModel.DataModel;


namespace SaleMonitoring.DAL.Repository.Model
{
    public class Repository : IRepository
    {
        public Repository()
        {
            _context = new PurchaseContext();
        }

        private readonly PurchaseContext _context;



        public IEnumerable<TEntity> Get<TEntity>()
            where TEntity : class
        {
            return _context.Set<TEntity>();
        }

        public IEnumerable<TEntity> Get<TEntity>(int pageSize, int page, Expression<Func<TEntity, bool>> predicate = null)
            where TEntity : class, IGenericProperty
        {
            if (predicate != null)
            {
                return _context.Set<TEntity>().Where(predicate).OrderByDescending(x => x.Date).Skip((page - 1) * pageSize)
                    .Take(pageSize).ToList();
            }
            else
            {
                return _context.Set<TEntity>().OrderByDescending(x => x.Date).Skip((page - 1) * pageSize)
                    .Take(pageSize).ToList();
            }
        }

        public void Add<TEntity>(TEntity entity) where TEntity : class
        {
            _context.Set<TEntity>().Add(entity);
            _context.SaveChanges();
        }

        public void Remove<TEntity>(TEntity entity) where TEntity : class
        {
            _context.Set<TEntity>().Remove(entity);
            _context.SaveChanges();
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}


