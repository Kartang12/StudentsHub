using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace News.Domain
{
    public class Form
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public int Number { get; set; }

        [JsonIgnore]
        public List<MyUser> Students { get; set; }
    }
}
