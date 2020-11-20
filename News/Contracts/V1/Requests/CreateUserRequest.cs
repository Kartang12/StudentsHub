using News.Domain;
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

        public string Group { get; set; }

        public virtual List<Subject> subjects { get; set; }
    }
}