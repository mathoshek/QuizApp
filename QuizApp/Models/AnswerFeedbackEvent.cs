using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuizApp.Models
{
    public class AnswerFeedbackEvent: IQuizEvent
    {
        public static readonly string TYPE = "AnswerFeedback";
        public string Type { get; set; }
        public string QuestionId { get; set; }
        public bool Correct { get; set; }

        public AnswerFeedbackEvent(string questionId, bool correct)
        {
            this.Type = TYPE;
            this.QuestionId = questionId;
            this.Correct = correct;
        }

        public AnswerFeedbackEvent()
        {

        }
    }
}