using News.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace News.Contracts.V1.Requests
{
    public class CreateEcerciseRequest
    {
        public string Title { get; set; }
        public string Content{ get; set; }
        public string CorrectAnswer { get; set; }
        public string SubjectId { get; set; }
    }
}
