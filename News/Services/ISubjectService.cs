using News.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace News.Services
{
    public interface ISubjectService
    {
        Task<bool> CreateSubjectAsync(string subjectName, string formId);

        Task<bool> DeleteSubjectAsync(string id);

        Task<List<Subject>> GetSubjectsByUserIdAsync(string uId);
        
        Task<List<Subject>> GetSubjectsForStudent(string uId);

        Task<Subject> GetAsync(string id);
        
        Task<bool> UpdateSubjectAsync(string id, string name, string formId);

        Task<List<Subject>> GetSubjectsAsync();
    }
}