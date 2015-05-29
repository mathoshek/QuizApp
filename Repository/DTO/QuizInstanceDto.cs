using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.DTO
{
    public class QuizInstanceDto
    {
        public int Id { get; set; }

        public int QuizId { get; set; }

        public bool IsStarted { get; set; }

        public Nullable<DateTime> StartTime { get; set; }
    }
}
