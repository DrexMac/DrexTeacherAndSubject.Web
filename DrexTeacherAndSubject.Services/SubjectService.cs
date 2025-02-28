using DrexTeacherAndSubject.Contracts;
using DrexTeacherAndSubject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrexTeacherAndSubject.Services
{
    public class SubjectService : ISubjectService
    {
        private readonly IRepository<Subject> _repository;

        public SubjectService(IRepository<Subject> repository)
        {
            if (repository == null)
            {
                return;
            }
            _repository = repository;
        }

        public async Task<IEnumerable<Subject>> GetAllAsync()
        {
            return await _repository.All()
                .Where(s => s.IsDeleted == false)
                .ToListAsync();
        }

        public async Task<Subject?> GetByIdAsync(Guid id)
        {
            return await _repository.Find(s => s.Id == id && s.IsDeleted == false)
                .FirstOrDefaultAsync();
        }

        public async Task AddAsync(Subject entity)
        {
            if (entity != null)
            {
                entity.IsDeleted = false;
                await _repository.AddAsync(entity);
                await _repository.SaveChangesAsync();
            }
        }

        public async Task UpdateAsync(Subject entity)
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
                    entity.IsDeleted = true; // Set IsDeleted to true for soft delete
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
                    entity.IsDeleted = false; // Set IsDeleted to false for undelete
                    _repository.Update(entity);
                    await _repository.SaveChangesAsync();
                }
            }
        }
    }
}