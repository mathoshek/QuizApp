using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.DTO
{
    public class QuestionInstanceDto
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public bool AnswerSaved { get; set; }
        public bool Choice1 { get; set; }
        public bool Choice2 { get; set; }
        public bool Choice3 { get; set; }

        public int QuizInstanceId { get; set; }
    }
}
