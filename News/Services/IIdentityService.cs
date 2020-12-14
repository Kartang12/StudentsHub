
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
        Task<AuthSuccessResponse> RegisterAsync(string email, string name, string password, string role, string formId, List<string> subjects);

        Task<AuthSuccessResponse> LoginAsync(string email, string password);

        Task<MyUser> GetUsersByName(string name);

        Task<MyUser> GetUserByEmail(string name);

        Task<IdentityResult> DeleteUser(string id);
    }
}