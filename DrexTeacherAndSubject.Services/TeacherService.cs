using DrexTeacherAndSubject.Contracts;
using DrexTeacherAndSubject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrexTeacherAndSubject.Services
{
    public class TeacherService : ITeacherService
    {
        private readonly IRepository<Teacher> _repository;

        public TeacherService(IRepository<Teacher> repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository)); // Null check for repository
        }

        public async Task<IEnumerable<Teacher>> GetAllAsync()
        {
            return await _repository.All().ToListAsync(); // Ensure you have using Microsoft.EntityFrameworkCore
        }

        public async Task<Teacher?> GetByIdAsync(int id)
        {
            return await _repository.Find(a => a.TeacherId == id).FirstOrDefaultAsync(); // Nullable return type
        }

        public async Task AddAsync(Teacher teacher)
        {
            if (teacher == null) throw new ArgumentNullException(nameof(teacher)); // Null check for teacher
            await _repository.AddAsync(teacher);
            await _repository.SaveChangesAsync();
        }

        public async Task UpdateAsync(Teacher teacher)
        {
            if (teacher == null) throw new ArgumentNullException(nameof(teacher)); // Null check for teacher
            _repository.Update(teacher);
            await _repository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var teacher = await GetByIdAsync(id);
            if (teacher != null) // Check if teacher exists before deletion
            {
                _repository.Delete(teacher);
                await _repository.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException($"Teacher with ID {id} not found."); // Optional: handle not found case
            }
        }
    }
}
