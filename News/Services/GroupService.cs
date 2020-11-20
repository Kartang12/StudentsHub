using Microsoft.EntityFrameworkCore;
using News.Data;
using News.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace News.Services
{
    public class GroupService
    {
        private readonly DataContext _dataContext;

        public GroupService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<List<Group>> GetAllGroupsAsync()
        {
            return await _dataContext.Groups.AsNoTracking().ToListAsync();
        }

        public async Task<Group> GetGroupAsync(string groupName)
        {
            return await _dataContext.Groups.AsNoTracking().SingleOrDefaultAsync(x => x.Id == groupName.ToLower());
        }

        public async Task<bool> CreateGroupAsync(string groupName)
        {
            var existingGroup = await _dataContext.Groups.AsNoTracking().SingleOrDefaultAsync(x => x.Id == groupName.ToLower());
            if (existingGroup != null)
                return false;

            await _dataContext.Groups.AddAsync(new Group{ Id= groupName});
            var created = await _dataContext.SaveChangesAsync();
            return created > 0;
        }

        public async Task<Group> UpdateGroupAsync(string oldGroupName, string newGroupName)
        {
            _dataContext.Groups.FirstOrDefault(x => x.Id == oldGroupName).Id = newGroupName;
            _dataContext.SaveChanges();
            return await _dataContext.Groups.FirstOrDefaultAsync(x => x.Id == newGroupName);
        }

        public async Task<bool> DeleteGroupAsync(string groupName)
        {
            var group = await _dataContext.Groups.AsNoTracking().SingleOrDefaultAsync(x => x.Id == groupName.ToLower());

            if (group == null)
                return false;

            
            return await _dataContext.SaveChangesAsync() > 0;
        }

    }
}
