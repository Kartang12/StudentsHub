using Microsoft.EntityFrameworkCore;
using News.Data;
using News.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace News.Services
{
    public class ExcersiseService : IExcersiseService
    {
        private readonly DataContext _dataContext;

        public ExcersiseService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<List<Excersise>> GetAll()
        {
            return await _dataContext.Excersises.Include(x=> x.subject).AsNoTracking().ToListAsync();
        }

        public async Task<Excersise> GetExcerseByIdAsync(string id)
        {
            return await _dataContext.Excersises.Include(x => x.subject).AsNoTracking().SingleOrDefaultAsync(x => x.Id.ToString() == id);
        }

        public async Task<List<Excersise>> GetExcersesBySubjectAsync(string subjectName)
        {
            return await _dataContext.Excersises.AsNoTracking().Where(x => x.subject.Name == subjectName).Include(x => x.subject).ToListAsync();
        }
        
        public async Task<bool> DeleteExcesiseAsync(string id)
        {
            Excersise ex = _dataContext.Excersises.SingleOrDefault(x => x.Id.ToString() == id);
            _dataContext.Excersises.Remove(ex);
            var removed = await _dataContext.SaveChangesAsync();
            return removed > 0;
        }

        public async Task<bool> CreateExcersiseAsync(string title, string content, string correctAnswer, string subjectName)
        {
            var id = Guid.NewGuid();
            await _dataContext.Excersises.AddAsync(new Excersise
            {
                Id = id,
                title = title,
                content = content,
                correctAnswer = correctAnswer,
            });
            await _dataContext.SaveChangesAsync();

            var temp = _dataContext.Excersises.First(x => x.Id == id);
            temp.subject = _dataContext.Subjects.SingleOrDefault(x => x.Name == subjectName);
            var created = await _dataContext.SaveChangesAsync();
            return created > 0;
        }

        public async Task<Excersise> UpdateExcersiseAsync(string excesiseId, string title, string content, string correctAnswer)
        {
            var ex = await _dataContext.Excersises.SingleOrDefaultAsync(x => x.Id.ToString() == excesiseId);

            if (!string.IsNullOrEmpty(title))
                ex.title = title;
            if (!string.IsNullOrEmpty(content))
                ex.content = content;
            if (!string.IsNullOrEmpty(correctAnswer))
                ex.correctAnswer = correctAnswer;

            _dataContext.SaveChanges();
            return ex;
        }
    }
}
