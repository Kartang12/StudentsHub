using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace News.Domain
{
    public class User : IdentityUser
    {
        public Group group { get; set; }
        public List<Subject> subjects { get; set; }
    }
}
