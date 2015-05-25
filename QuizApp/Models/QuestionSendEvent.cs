using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuizApp.Models
{
    public class QuestionSendEvent : IQuizEvent
    {
        public static readonly string TYPE = "QuestionSend";
        public string Type { get; set; }
        public Question Question { get; set; }
        

        public QuestionSendEvent(Question question)
        {
            this.Type = TYPE;
            this.Question = question;
        }

        public QuestionSendEvent()
        {

        }
    }
}