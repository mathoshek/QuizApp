using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace QuizApp.Models
{
    public class AssignQuizModel
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public int QuizId { get; set; }
    }
}
