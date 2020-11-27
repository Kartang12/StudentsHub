using Microsoft.EntityFrameworkCore;
using News.Data;
using News.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace News.Services
{
    public class SubjectService : ISubjectService
    {
        private readonly DataContext _dataContext;

        public SubjectService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<List<Subject>> GetSubjectsAsync()
        {
            return await _dataContext.Subjects.AsNoTracking().ToListAsync();
        }
        
        public async Task<Subject> GetAsync(string subName)
        {
            return await _dataContext.Subjects.AsNoTracking().SingleOrDefaultAsync(x => x.Name == subName);
        }

        public Task<List<Subject>> GetSubjectsByUserIdAsync(string uId)
        {
            return Task.FromResult( _dataContext.Users.Include(x => x.subjects).FirstOrDefault(x => x.Id == uId).subjects);
        }

        public async Task<bool> CreateSubjectAsync(string subjectName)
        {
            var a = _dataContext.Subjects.Where(x => x.Name == subjectName);
            if (a.Count() > 0)
                return false;
            await _dataContext.Subjects.AddAsync(new Subject{Name = subjectName});
            var created = await _dataContext.SaveChangesAsync();
            return created > 0;
        }

        public async Task<bool> DeleteSubjectAsync(string subName)
        {
            Subject sub = _dataContext.Subjects.SingleOrDefault(x => x.Name == subName);
            _dataContext.Subjects.Remove(sub);
            var removed = await _dataContext.SaveChangesAsync();
            return removed > 0;
        }
    }
}
