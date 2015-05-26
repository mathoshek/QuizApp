using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.DTO
{
    class QuizDto
    {
        public int Id { get; set; }
        public string QuizTitle { get; set; }
        public int Time { get; set; }
        public double PassingScore { get; set; }
        public int NumQuestions { get; set; }
    }
}
