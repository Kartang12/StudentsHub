using News.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace News.Services
{
    public interface IExerciseService
    {

        Task<List<Exercise>> GetAll();
        Task<Exercise> GetExerciseByIdAsync(string id);
        Task<List<Exercise>> GetExercisesBySubjectAsync(string subjectId);
        Task<bool> CreateExcersiseAsync(string title, string content, string correctAnswer, string subjectId);
        Task<bool> DeleteExcesiseAsync(string id);
        Task<bool> UpdateExcersiseAsync(string excesiseId, string title, string content, string correctAnswer);

        Task<bool> SaveExcersiseAsync(string userId, string taskId, string answer);

        Task<StudentExercise[]> GetMarksAsync(string userId);
    }
}
