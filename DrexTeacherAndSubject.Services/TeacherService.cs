using DrexTeacherAndSubject.Contracts;
using DrexTeacherAndSubject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
            // Include related Subjects when fetching Teachers
            return await _repository.All()
                .Include(t => t.Subjects)
                .ToListAsync();
        }

        public async Task<Teacher?> GetByIdAsync(Guid id)
        {
            // Include Subjects for a specific Teacher
            return await _repository.Find(t => t.Id == id)
                .Include(t => t.Subjects)
                .FirstOrDefaultAsync();
        }

        public async Task AddAsync(Teacher entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            await _repository.AddAsync(entity);
            await _repository.SaveChangesAsync();
        }

        public async Task UpdateAsync(Teacher entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            _repository.Update(entity);
            await _repository.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await GetByIdAsync(id);
            if (entity == null)
            {
                return;  
            }

            _repository.Delete(entity);
            await _repository.SaveChangesAsync();
        }
    }
}
