using News.Domain;
using System.Collections.Generic;

namespace News.Contracts.V1.Responses
{
    public class AuthSuccessResponse
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public List<Subject> subjects { get; set; }
    }
}