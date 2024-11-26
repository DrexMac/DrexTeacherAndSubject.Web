using DrexTeacherAndSubject.Contracts;
using DrexTeacherAndSubject.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DrexTeacherAndSubject.MySql
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly DefaultDbContext DbContext;
        private DbSet<TEntity> DbSet;
        public Repository(DefaultDbContext applicationDbContext)
        {
            DbContext = applicationDbContext;
            DbSet = DbContext.Set<TEntity>();
        }
        public async Task AddAsync(TEntity model)
        {
            await DbSet.AddAsync(model);
        }
        public async Task AddAsync(TEntity[] models)
        {
            await DbSet.AddRangeAsync(models);
        }
        public IQueryable<TEntity> All()
        {
            return DbSet.AsNoTracking();
        }
        public IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.Where(predicate);
        }
        public void Update(TEntity model)
        {
            DbSet.Update(model);
        }
        public void Update(TEntity[] models)
        {
            DbSet.UpdateRange(models);
        }
        public void Delete(TEntity model)
        {
            DbSet.Remove(model);
        }
        public void Delete(TEntity[] models)
        {
            DbSet.RemoveRange(models);
        }
        public async Task SaveChangesAsync()
        {
            await DbContext.SaveChangesAsync();
        }

    }
}
