using News.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace News.Services
{
    public interface ISubjectService
    {
        Task<bool> CreateSubjectAsync(string subjectName);

        Task<bool> DeleteSubjectAsync(string subName);

        Task<List<Subject>> GetSubjectsByUserIdAsync(string uId);

        Task<Subject> GetAsync(string subName);

        Task<List<Subject>> GetSubjectsAsync();
    }
}