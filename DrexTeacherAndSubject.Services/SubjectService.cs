using DrexTeacherAndSubject.Contracts;
using DrexTeacherAndSubject.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DrexTeacherAndSubject.Services
{
    public class SubjectService : ISubjectService
    {
        private readonly IRepository<Subject> _repository;

        public SubjectService(IRepository<Subject> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Subject>> GetAllAsync()
        {
            return await _repository.All().ToListAsync();
        }

        public async Task<Subject?> GetByIdAsync(Guid id) 
        {
            return await _repository.Find(s => s.Id == id).FirstOrDefaultAsync(); 
        }

        public async Task AddAsync(Subject subject)
        {
            await _repository.AddAsync(subject);
            await _repository.SaveChangesAsync();
        }

        public async Task UpdateAsync(Subject subject)
        {
            _repository.Update(subject);
            await _repository.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id) 
        {
            var subject = await GetByIdAsync(id);
            if (subject != null)
            {
                _repository.Delete(subject);
                await _repository.SaveChangesAsync();
            }
        }
    }
}
