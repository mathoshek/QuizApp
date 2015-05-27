using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuizApp.Models
{
    public class QuestionModel
    {
        public string Id { get; set; }
        public string QuestionText { get; set; }
        public string FirstAnswer { get; set; }
        public string SecondAnswer { get; set; }
        public string ThirdAnswer { get; set; }
        public int DomainId { get; set; }
        public bool CorrectFirst { get; set; }
        public bool CorrectSecond { get; set; }
        public bool CorrectThird { get; set; }

        public QuestionModel()
        {

        }

        public QuestionModel(string text, string FirstAnswers, string secondsAnswer, string ThirdAnswers, bool correct, bool corrects, bool correctt, int DomainId)
        {
            this.QuestionText = text;
            this.FirstAnswer = FirstAnswer;
            this.SecondAnswer = secondsAnswer;
            this.ThirdAnswer = ThirdAnswer;
            this.CorrectFirst = correct;
            this.CorrectSecond = corrects;
            this.CorrectThird = correctt;
            this.DomainId = DomainId;
        }
    }
}