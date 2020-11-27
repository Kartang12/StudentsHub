using News.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace News.Services
{
    public interface IGroupService
    {
        Task<List<Group>> GetAllGroupsAsync();
        Task<Group> GetGroupAsync(string groupName);
        Task<bool> CreateGroupAsync(string groupName);
        Task<Group> UpdateGroupAsync(string oldGroupName, string newGroupName);
        Task<bool> DeleteGroupAsync(string groupName);
    }
}
