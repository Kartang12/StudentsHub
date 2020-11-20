using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace News.Domain
{
    public class User : IdentityUser
    {
        public virtual Group group { get; set; }
        public virtual List<Subject> subjects { get; set; }
    }
}
