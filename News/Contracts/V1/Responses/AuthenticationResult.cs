using System.Collections.Generic;

namespace News.Contracts.V1.Responses
{
    public class AuthenticationResult
    {   
        public bool Success { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}