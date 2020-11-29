using News.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace News.Services
{
    public interface IExcersiseService
    {

        Task<List<Excersise>> GetAll();
        Task<Excersise> GetExcerseByIdAsync(string id);
        Task<List<Excersise>> GetExcersesBySubjectAsync(string subjectName);
        Task<bool> CreateExcersiseAsync(string title, string content, string correctAnswer, string subjectName);
        //Task<bool> CreateExcersiseAsync(string title, string content, string correctAnswer, string subjectName);
        Task<bool> DeleteExcesiseAsync(string id);
        Task<Excersise> UpdateExcersiseAsync(string excesiseId, string title, string content, string correctAnswer);
    }
}
