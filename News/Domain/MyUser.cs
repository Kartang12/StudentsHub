using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace News.Domain
{
    public class MyUser : IdentityUser
    {
        public Form form { get; set; }
        public virtual List<Subject> subjects { get; set; }
    }
}
