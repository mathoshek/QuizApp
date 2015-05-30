using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuizApp.Models
{
    public class QuestionModel
    {
        public int DomainId { get; set; }
        public string QuestionText { get; set; }
        public bool IsSingleChoice { get; set; }
        public string FirstAnswerText { get; set; }
        public bool FirstAnswerCorrect { get; set; }
        public string SecondAnswerText { get; set; }
        public bool SecondAnswerCorrect { get; set; }
        public string ThirdAnswerText { get; set; }
        public bool ThirdAnswerCorrect { get; set; }

        public QuestionModel()
        {
        }

        public QuestionModel(int domainId, 
            string questionText, 
            bool isSingleChoice,
            string firstAnswerText,
            bool firstAnswerCorrect,
            string secondAnswerText,
            bool secondAnswerCorrect,
            string thirdAnswerText, 
            bool thirdAnswerCorrect)
        {
            this.DomainId = domainId;
            this.QuestionText = questionText;
            this.IsSingleChoice = isSingleChoice;
            this.FirstAnswerText = firstAnswerText;
            this.FirstAnswerCorrect = firstAnswerCorrect;
            this.SecondAnswerText = secondAnswerText;
            this.SecondAnswerCorrect = secondAnswerCorrect;
            this.ThirdAnswerText = thirdAnswerText;
            this.ThirdAnswerCorrect = thirdAnswerCorrect;
        }
    }

}