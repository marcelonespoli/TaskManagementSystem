using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using TMS.Domain.Core.Models;

namespace TMS.Domain.Interfaces
{
    public interface IRepository<TEntity> : IDisposable where TEntity : Entity<TEntity>
    {
        IEnumerable<TEntity> GetAll();
        TEntity GetById(Guid id);
        IEnumerable<TEntity> Search(Expression<Func<TEntity, bool>> predicate);
        void Add(TEntity obj);
        void Update(TEntity obj);
        void Remove(Guid id);        
        int SaveChanges();
    }
}
