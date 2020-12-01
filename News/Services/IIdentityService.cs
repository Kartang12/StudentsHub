
using Microsoft.AspNetCore.Identity;
using News.Contracts.V1.Responses;
using News.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace News.Services
{
    public interface IIdentityService
    {
        Task<AuthSuccessResponse> RegisterAsync(string email, string name, string password, string role, string group, List<string> subjects);

        Task<AuthSuccessResponse> LoginAsync(string email, string password);

        Task<User> GetUsersByName(string name);

        Task<User> GetUserByEmail(string name);

        Task<IdentityResult> DeleteUser(string id);
    }
}