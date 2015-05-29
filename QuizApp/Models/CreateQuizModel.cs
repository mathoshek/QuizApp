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
        public int Time { get; set; }

        [Required]
        public float PassingScore { get; set; }

        [Required]
        public int QuestionsNumber { get; set; }

        [Required]
        public  int DomainId { get; set; }
    }
}