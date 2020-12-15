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
            return await _dataContext.Subjects.AsNoTracking().Include(x=>x.form).ToListAsync();
        }
        
        public async Task<Subject> GetAsync(string id)
        {
            return await _dataContext.Subjects.AsNoTracking().Include(x=>x.form).SingleOrDefaultAsync(x => x.Id.ToString() == id);
        }

        public Task<List<Subject>> GetSubjectsByUserIdAsync(string uId)
        {
            var user = _dataContext.Users.Include(x => x.subjects).FirstOrDefault(x => x.Id == uId);
            return Task.FromResult(user.subjects);
        }

        public async Task<bool> CreateSubjectAsync(string subjectName, string formId)
        {
            var a = _dataContext.Subjects.Where(x => x.Name == subjectName && x.form.Id.ToString() == formId);
            if (a.Count() > 0)
                return false;
            var form = _dataContext.Forms.First(x => x.Id.ToString() == formId);
            await _dataContext.Subjects.AddAsync(new Subject{Name = subjectName, form = form});
            var created = await _dataContext.SaveChangesAsync();
            return created > 0;
        }

        public async Task<bool> UpdateSubjectAsync(string id, string subjectName, string formId)
        {
            var a = _dataContext.Subjects.Where(x => x.Id.ToString() == id);
            if (a.Count() < 0)
                return false;

            var subject = _dataContext.Subjects.First(x => x.Id.ToString() == id);
            if (subject == null)
                return false;

            if (subjectName != null)
                subject.Name = subjectName;

            if(formId != null)
                subject.form = _dataContext.Forms.First(x => x.Id.ToString() == formId);

            var created = await _dataContext.SaveChangesAsync();
            return created > 0;
        }

        public async Task<bool> DeleteSubjectAsync(string id)
        {
            Subject sub = _dataContext.Subjects.SingleOrDefault(x => x.Id.ToString() == id);
            _dataContext.Subjects.Remove(sub);
            return await _dataContext.SaveChangesAsync() > 0;
        }

        public async Task<List<Subject>> GetSubjectsForStudent(string uId)
        {
            var form = _dataContext.Users.Include(x=>x.form).FirstOrDefault(x => x.Id == uId).form;

            List<Subject> subjects = await _dataContext.Subjects.Where(x => x.form.Number == form.Number).ToListAsync();
            return subjects;
        }
    }
}
