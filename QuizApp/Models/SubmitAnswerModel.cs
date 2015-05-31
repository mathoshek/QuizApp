using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuizApp.Models
{
    public class SubmitAnswerModel
    {
        public int SingleChoiceAnswer { get; set; }
        public bool MultipleChoiceAnswer1 { get; set; }
        public bool MultipleChoiceAnswer2 { get; set; }
        public bool MultipleChoiceAnswer3 { get; set; }

        public SubmitAnswerModel(int sinlgeChoiceAnswer, bool choice1, bool choice2, bool choice3)
        {
            this.SingleChoiceAnswer = sinlgeChoiceAnswer;
            this.MultipleChoiceAnswer1 = choice1;
            this.MultipleChoiceAnswer2 = choice2;
            this.MultipleChoiceAnswer3 = choice3;
        }
        public SubmitAnswerModel()
        {

        }
    }
}