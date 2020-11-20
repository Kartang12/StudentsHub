using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace News.Domain
{
    public class Excersise
    {
        public Guid Id { get; set; }
        public string title { get; set; }
        public string content { get; set; }
        public string correctAnswer { get; set; }
        public Subject subject { get; set; }
    }
}
