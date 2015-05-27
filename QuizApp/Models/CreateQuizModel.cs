using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace QuizApp.Models
{
    public class CreateQuizModel
    {
        [Required]
        public string QuizTitle { get; set; }

        [Required]
        public string Time { get; set; }

        [Required]
        public string PassingScore { get; set; }

        [Required]
        public string QuestionsNumber { get; set; }
    }
}