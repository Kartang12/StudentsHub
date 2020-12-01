using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace News.Contracts.V1.Requests
{
    public class ExersiseUpdateRequest
    {
        public string title { get; set; }
        public string content { get; set; }
        public string correctAnswer { get; set; }
    }
}
