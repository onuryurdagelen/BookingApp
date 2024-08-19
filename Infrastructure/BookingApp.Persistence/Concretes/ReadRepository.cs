using BookingApp.Domain.Entities;
using BookingApp.Infrastructure.Abstracts;
using BookingApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Persistence.Concretes
{
    public class ReadRepository<TEntity> : IReadRepository<TEntity> where TEntity : BaseEntity, new()
    {
        protected readonly AppDbContext _context;
        public ReadRepository(AppDbContext context)
        {
            _context = context;
        }

        public DbSet<TEntity> Table => _context.Set<TEntity>();

        /* Table return edildiğinde IQueryable<TEntity> döndürür.*/
        public IQueryable<TEntity> GetAll(bool isTracking = true)
         => isTracking ? Table.AsQueryable() :
            Table.AsQueryable().AsNoTracking();

        public async Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> method, bool isTracking = true)
        => isTracking ? await Table.FirstOrDefaultAsync(method) :
            await Table.AsNoTracking().FirstOrDefaultAsync();

        public IQueryable<TEntity> GetWhere(Expression<Func<TEntity, bool>> method, bool isTracking = true)
        => isTracking ? Table.Where(method) :
            Table.Where(method).AsNoTracking();

        public async Task<TEntity> GetByIdAsync(string id)
        => await Table.FindAsync(Guid.Parse(id));

        public IQueryable<TEntity> Include(params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = Table;

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return query;
        }

       
    }
}
