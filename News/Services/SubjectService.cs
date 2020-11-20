using Microsoft.EntityFrameworkCore;
using News.Data;
using News.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace News.Services
{
    public class SubjectService
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

        public async Task<bool> CreateSubjectAsync(string subjectName)
        {
            await _dataContext.Subjects.AddAsync(new Subject{Name = subjectName});
            var created = await _dataContext.SaveChangesAsync();
            return created > 0;
        }

        public async Task<bool> DeleteSubjectAsync(string id)
        {
            Subject sub = _dataContext.Subjects.SingleOrDefault(x => x.Id.ToString() == id);
            _dataContext.Subjects.Remove(sub);
            var removed = await _dataContext.SaveChangesAsync();
            return removed > 0;
        }
    }
}
