using System;
using System.Collections.Generic;

namespace News.Domain
{
    public class Group
    {
        public string Id { get; set; }

        public List<User> users { get; set; } = new List<User>();
    }
}
