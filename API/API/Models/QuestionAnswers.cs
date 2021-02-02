using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class QuestionAnswers
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public Type Type { get; set; }
        public Area Area { get; set; }

        [Required]
        public string Question { get; set; }
        public string Answers { get; set; }
        public string Answer { get; set; } = null;
    }
}
