using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace News.Contracts.V1.Requests
{
    public class CreateUserRequest
    {
        [EmailAddress]
        public string Email { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }

        public string Role { get; set; }

        public string FormId { get; set; }

        public virtual List<string> SubjectIds { get; set; }
    }
}