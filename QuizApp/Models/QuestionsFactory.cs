using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace QuizApp.Models
{
    public class QuestionsFactory
    {
        private static Random random = new Random(DateTime.Now.Millisecond);
        private static readonly int SecondsToAnswer = 10;
        private static readonly int MODULO = 1000;

        public static List<Question> GenerateQuestions(int n)
        {
            List<Question> questions = new List<Question>();
            for (int i = 1; i <= n; i++)
            {
                int a = random.Next(MODULO);
                Thread.Sleep(1);
                int b = random.Next(MODULO) + 1;
                Thread.Sleep(1);
                int operation = random.Next(0, 3);
                double result = 0.0;
                char op = '+';
                switch (operation)
                {
                    case 0:
                        result = a + b;
                        op = '+';
                        break;
                    case 1:
                        result = a - b;
                        op = '-';
                        break;
                    case 2:
                        result = a * b;
                        op = '*';
                        break;
                    case 3:
                        result = (double)a / (double)b;
                        op = '/';
                        break;
                }
                string text = String.Format("What is the result of the following expresion?\n {0} {1} {2}", a, op, b);
                int correctIndex = random.Next(0, 3);
                List<string> answers = new List<string>(4);
                for (int j = 0; j < 4; j++)
                {
                    if (j == correctIndex)
                    {
                        answers.Add(result.ToString());
                    }
                    else
                    {
                        answers.Add(((double)random.Next(MODULO)).ToString());
                    }
                }
                Question question = new Question(text, answers, SecondsToAnswer, correctIndex);
                questions.Add(question);
            }
            return questions;
        }
    }
}