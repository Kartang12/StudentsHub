using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace News.Domain
{
    public class StudentExcersise
    {
        public string taskId { get; set; }
        public string userId { get; set; }
        public string answer { get; set; }
        public int mark { get; set; }
    }
}
