using Microsoft.EntityFrameworkCore;
using News.Data;
using News.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace News.Services
{
    public class FormService : IFormService
    {
        private readonly DataContext _dataContext;

        public FormService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<List<Form>> GetAllFormsAsync()
        {
            return await _dataContext.Forms.AsNoTracking().ToListAsync();
        }

        public async Task<Form> GetFormAsync(string id)
        {
            return await _dataContext.Forms.AsNoTracking().SingleOrDefaultAsync(x => x.Id.ToString() == id);
        }

        public async Task<bool> CreateFormAsync(int formNumber)
        {
            var existingGroup = await _dataContext.Forms.AsNoTracking().SingleOrDefaultAsync(x => x.Number == formNumber);
            if (existingGroup != null)
                return false;

            await _dataContext.Forms.AddAsync(new Form { Number = formNumber});
            return await _dataContext.SaveChangesAsync() > 0;
        }

        public async Task<Form> UpdateFormAsync(string id, int newNumber)
        {
            _dataContext.Forms.FirstOrDefault(x => x.Id.ToString() == id).Number = newNumber;
            _dataContext.SaveChanges();
            return await _dataContext.Forms.FirstOrDefaultAsync(x => x.Id.ToString() == id);
        }

        public async Task<bool> DeleteFormAsync(string id)
        {
            var form = await _dataContext.Forms.SingleOrDefaultAsync(x => x.Id.ToString() == id);
            if (form == null)
                return false;
            _dataContext.Forms.Remove(form);

            return await _dataContext.SaveChangesAsync() > 0;
        }

    }
}
