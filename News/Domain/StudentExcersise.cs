using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace News.Domain
{
    public class StudentExcersise
    {
        public Excersise task { get; set; }
        public User user { get; set; }
        public string answer { get; set; }
        public int mark { get; set; }
    }
}
