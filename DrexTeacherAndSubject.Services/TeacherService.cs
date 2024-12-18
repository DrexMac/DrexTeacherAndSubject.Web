using DrexTeacherAndSubject.Contracts;
using DrexTeacherAndSubject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrexTeacherAndSubject.Services
{
    public class TeacherService : ITeacherService
    {
        private readonly IRepository<Teacher> _repository;

        public TeacherService(IRepository<Teacher> repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<IEnumerable<Teacher>> GetAllAsync()
        {
            return await _repository.All()
                .Where(t => t.IsDeleted != true)
                .Include(t => t.Subjects)
                .ToListAsync();
        }

        public async Task<Teacher?> GetByIdAsync(Guid id)
        {
            return await _repository.Find(t => t.Id == id && t.IsDeleted != true)
                .Include(t => t.Subjects)
                .FirstOrDefaultAsync();
        }

        public async Task AddAsync(Teacher entity)
        {
            if (entity != null)
            {
                entity.IsDeleted = false;
                await _repository.AddAsync(entity);
                await _repository.SaveChangesAsync();
            }
        }

        public async Task UpdateAsync(Teacher entity)
        {
            if (entity != null)
            {
                _repository.Update(entity);
                await _repository.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                _repository.Delete(entity);
                await _repository.SaveChangesAsync();
            }
        }

        public async Task SoftDeleteAsync(Guid? id)
        {
            if (id.HasValue)
            {
                var entity = await _repository.Find(t => t.Id == id.Value && t.IsDeleted != true)
                    .FirstOrDefaultAsync();

                if (entity != null)
                {
                    entity.IsDeleted = true;
                    _repository.Update(entity);
                    await _repository.SaveChangesAsync();
                }
            }
        }

        public async Task UndeleteAsync(Guid? id)
        {
            if (id.HasValue)
            {
                var entity = await _repository.Find(t => t.Id == id.Value && t.IsDeleted == true)
                    .FirstOrDefaultAsync();

                if (entity != null)
                {
                    entity.IsDeleted = false;
                    _repository.Update(entity);
                    await _repository.SaveChangesAsync();
                }
            }
        }
    }
}
