using BookingApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Infrastructure.Abstracts
{
    public interface IWriteRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity, new()
    {
        Task<bool> AddAsync(TEntity model);

        Task<bool> AddRangeAsync(List<TEntity> datas);

        bool Remove(TEntity model);
        bool RemoveRange(List<TEntity> datas);

        Task<bool> RemoveAsync(string id);

        bool Update(TEntity model);

        Task<int> SaveAsync();
    }
}
