using BookingApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Infrastructure.Abstracts
{
    public interface IReadRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity, new()
    {
        IQueryable<TEntity> Include(params Expression<Func<TEntity, object>>[] includes);
        IQueryable<TEntity> GetAll(bool isTracking = true);
        IQueryable<TEntity> GetWhere(Expression<Func<TEntity, bool>> method, bool isTracking = true);
        Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> method, bool isTracking = true);
        Task<TEntity> GetByIdAsync(string id);
    }
}
