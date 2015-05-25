using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuizApp.Models
{
    public class Question
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public List<string> Answers { get; set; }
        public int SecondsToAnswer { get; set; }
        private int CorrectAnswerIndex;

        public Question()
        {

        }

        public Question(string text, List<string> answers, int secondsToAnswer, int correctAnswerIndex)
        {
            this.Id = Guid.NewGuid().ToString();
            this.Text = text;
            this.Answers = new List<string>(answers);
            this.SecondsToAnswer = secondsToAnswer;
            this.CorrectAnswerIndex = correctAnswerIndex;
        }

        public bool Answer(int index)
        {
            return index == CorrectAnswerIndex;
        }
    }
}