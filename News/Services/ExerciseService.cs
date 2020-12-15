using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using News.Data;
using News.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace News.Services
{
    public class ExerciseService : IExerciseService
    {
        private readonly DataContext _dataContext;
        private readonly UserManager<MyUser> _userManager;
        public ExerciseService(DataContext dataContext, UserManager<MyUser> userManager)
        {
            _dataContext = dataContext;
            _userManager = userManager;
        }

        public async Task<List<Exercise>> GetAll()
        {
            return await _dataContext.Exersises.Include(x=> x.subject).AsNoTracking().ToListAsync();
        }

        public async Task<Exercise> GetExerciseByIdAsync(string id)
        {
            return await _dataContext.Exersises.Include(x => x.subject).AsNoTracking().SingleOrDefaultAsync(x => x.exId == id);
        }

        public async Task<List<Exercise>> GetExercisesBySubjectAsync(string subjectId)
        {
            return await _dataContext.Exersises.AsNoTracking().Where(x => x.subject.Id.ToString() == subjectId).ToListAsync();
        }
        
        public async Task<bool> DeleteExcesiseAsync(string id)
        {
            List<StudentExercise> sEx = await _dataContext.StudentExercises.Where(x => x.exId == id).ToListAsync();
            _dataContext.StudentExercises.RemoveRange(sEx);
            Exercise ex = _dataContext.Exersises.SingleOrDefault(x => x.exId == id);
            _dataContext.Exersises.Remove(ex);
            var removed = await _dataContext.SaveChangesAsync();
            return removed > 0;
        }

        public async Task<bool> CreateExcersiseAsync(string title, string content, string correctAnswer, string subjectId)
        {
            var id = Guid.NewGuid().ToString();
            await _dataContext.Exersises.AddAsync(new Exercise
            {
                exId = id,
                title = title,
                content = content,
                correctAnswer = correctAnswer,
            });
            await _dataContext.SaveChangesAsync();

            var temp = _dataContext.Exersises.First(x => x.exId == id);
            temp.subject = _dataContext.Subjects.SingleOrDefault(x => x.Id.ToString() == subjectId);
            var created = await _dataContext.SaveChangesAsync();
            return created > 0;
        }

        public async Task<bool> SaveExcersiseAsync(string userId, string taskId, string answer)
        {
            var ex = await _dataContext.Exersises.FirstOrDefaultAsync(x => x.exId == taskId);
            await _dataContext.StudentExercises.AddAsync(new StudentExercise
            {
                userId = userId,
                exId = taskId,
                answer = answer,
                mark = answer == ex.correctAnswer ? true : false
            });

            return await _dataContext.SaveChangesAsync() > 0;
        }
        
        public async Task<StudentExercise[]> GetMarksAsync(string userId)
        {
            var a = _dataContext.StudentExercises.AsNoTracking().Where(x => x.userId == userId);
            var stEx = await a.ToArrayAsync();

            return await Task.FromResult(stEx);
        }

        public async Task<StudentExercise> CheckTask(string exId, string uId)
        {
            var a = await _dataContext.StudentExercises.AsNoTracking().FirstAsync(x => x.userId == uId && x.exId == exId);

            return a;
        }

        public async Task<bool> UpdateExcersiseAsync(string excesiseId, string title, string content, string correctAnswer)
        {
            var ex = await _dataContext.Exersises.SingleOrDefaultAsync(x => x.exId == excesiseId);

            if (!string.IsNullOrEmpty(title))
                ex.title = title;
            if (!string.IsNullOrEmpty(content))
                ex.content = content;
            if (!string.IsNullOrEmpty(correctAnswer))
                ex.correctAnswer = correctAnswer;

            var saved = _dataContext.SaveChanges() > 0;
            return saved;
        }
    }
}
