using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace News.Domain
{
    public class Excersise
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }
        public string title { get; set; }
        public string content { get; set; }
        public string correctAnswer { get; set; }   
        public Subject subject { get; set; }
    }
}
