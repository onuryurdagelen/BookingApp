using BookingApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Infrastructure.Abstracts
{
    public interface IRepository<TEntity> where TEntity :BaseEntity, new()
    {
        DbSet<TEntity> Table { get; }
    }
}
