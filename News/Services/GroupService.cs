using Microsoft.EntityFrameworkCore;
using News.Data;
using News.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace News.Services
{
    public class GroupService : IGroupService
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
            return await _dataContext.Groups.AsNoTracking().SingleOrDefaultAsync(x => x.Name.ToLower() == groupName.ToLower());
        }

        public async Task<bool> CreateGroupAsync(string groupName)
        {
            var existingGroup = await _dataContext.Groups.AsNoTracking().SingleOrDefaultAsync(x => x.Name == groupName.ToLower());
            if (existingGroup != null)
                return false;

            await _dataContext.Groups.AddAsync(new Group{ Name = groupName});
            var created = await _dataContext.SaveChangesAsync();
            return created > 0;
        }

        public async Task<Group> UpdateGroupAsync(string oldGroupName, string newGroupName)
        {
            _dataContext.Groups.FirstOrDefault(x => x.Name == oldGroupName).Name = newGroupName;
            _dataContext.SaveChanges();
            return await _dataContext.Groups.FirstOrDefaultAsync(x => x.Name == newGroupName);
        }

        public async Task<bool> DeleteGroupAsync(string groupName)
        {
            var group = await _dataContext.Groups.AsNoTracking().SingleOrDefaultAsync(x => x.Name == groupName.ToLower());

            if (group == null)
                return false;

            
            return await _dataContext.SaveChangesAsync() > 0;
        }

    }
}
