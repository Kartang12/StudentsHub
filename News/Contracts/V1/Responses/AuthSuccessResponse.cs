using News.Domain;
using System.Collections.Generic;

namespace News.Contracts.V1.Responses
{
    public class AuthSuccessResponse: AuthenticationResult
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Group { get; set; }
        public List<string> Roles { get; set; }
        public List<Subject> subjects { get; set; }
    }
}