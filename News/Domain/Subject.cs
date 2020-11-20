using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace News.Domain
{
    public class Subject
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}