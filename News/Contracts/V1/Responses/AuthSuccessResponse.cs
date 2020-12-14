using News.Domain;
using System.Collections.Generic;

namespace News.Contracts.V1.Responses
{
    public class AuthSuccessResponse: AuthenticationResult
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public int Form { get; set; }
        public string Role { get; set; }
    }
}