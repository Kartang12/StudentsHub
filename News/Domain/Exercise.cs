using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace News.Domain
{
    public class Exercise
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public string exId { get; set; }
        public string title { get; set; }
        public string content { get; set; }
        public string correctAnswer { get; set; }

        [JsonIgnore]
        public Subject subject { get; set; }
    }
}
