using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DrexTeacherAndSubject.Contracts
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> All();
        IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
        Task AddAsync(TEntity model);
        void Update(TEntity model);
        void Delete(TEntity model);
        Task SaveChangesAsync();
    }

}