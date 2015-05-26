using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.DTO
{
    class QuizQuestionDto
    {
        public int Id { get; set; }
        public string QuestionText { get; set; }
        public string Answer1Text { get; set; }
        public bool Answer1Correct { get; set; }
        public string Answer2Text { get; set; }
        public bool Answer2Correct { get; set; }
        public string Answer3Text { get; set; }
        public bool Answer3Correct { get; set; }
    }
}
