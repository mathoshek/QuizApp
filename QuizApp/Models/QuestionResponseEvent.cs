using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuizApp.Models
{
    public class QuestionResponseEvent : IQuizEvent
    {
        public static readonly string TYPE = "QuestionResponse";
        public string Type { get; set; }
        public Question Question { get; set; }
        public int AnswerIndex { get; set; }

        public QuestionResponseEvent(Question question, int answerIndex)
        {
            this.Type = TYPE;
            this.Question = question;
            this.AnswerIndex = answerIndex;
        }

        public QuestionResponseEvent()
        {

        }
    }
}