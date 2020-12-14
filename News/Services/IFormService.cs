using News.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace News.Services
{
    public interface IFormService
    {
        Task<List<Form>> GetAllFormsAsync();
        Task<Form> GetFormAsync(string id);
        Task<bool> CreateFormAsync(int number);
        Task<Form> UpdateFormAsync(string id, int newNumber);
        Task<bool> DeleteFormAsync(string id);
    }
}
