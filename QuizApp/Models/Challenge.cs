using QuizApp.QuizHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuizApp.Models
{
    public class Challenge
    {
        public string Id { get; set; }
        public QuizWebSocketHandler Initiator { get; set; }
        public QuizWebSocketHandler Challenged { get; set; }
        public List<Question> Questions { get; set; }

        private int _currentQuestionIndex;
        private DateTime _lastQuestionTime;
        private Dictionary<string, Dictionary<string, bool>> _answeredQuestions = new Dictionary<string, Dictionary<string, bool>>();

        public Challenge(QuizWebSocketHandler initiator, QuizWebSocketHandler challenged)
        {
            this.Id = Guid.NewGuid().ToString();
            this.Initiator = initiator;
            this.Challenged = challenged;
            this.Questions = QuestionsFactory.GenerateQuestions(10);
            _currentQuestionIndex = -1;
            _answeredQuestions[Initiator.User.Id] = new Dictionary<string, bool>();
            _answeredQuestions[Challenged.User.Id] = new Dictionary<string, bool>();
        }

        public void Start()
        {
            ShowNextQuestion();
        }

        private void ShowNextQuestion()
        {
            _currentQuestionIndex++;
            if (!IsFinished())
            {
                var question = Questions[_currentQuestionIndex];
                var questionSentEvent = new QuestionSendEvent(question);
                var message = Newtonsoft.Json.JsonConvert.SerializeObject(questionSentEvent);
                Initiator.Send(message);
                Challenged.Send(message);
                _lastQuestionTime = DateTime.Now;
            }
        }

        public bool AnswerQuestion(string userId, string questionId, int answerIndex)
        {
            QuizWebSocketHandler user;
            if (Initiator.User.Id == userId)
            {
                user = Initiator;
            }
            else if (Challenged.User.Id == userId)
            {
                user = Challenged;
            }
            else
            {
                return false;
            }
            Question question = this.Questions.FirstOrDefault(a => a.Id == questionId);
            if (question != null)
            {
                if ((DateTime.Now - _lastQuestionTime).TotalSeconds <= question.SecondsToAnswer)
                {
                    bool correct = false;
                    if (!_answeredQuestions[user.User.Id].ContainsKey(question.Id))
                    {
                        correct = _answeredQuestions[user.User.Id][question.Id] = question.Answer(answerIndex);
                    }
                    if (_answeredQuestions[Initiator.User.Id].ContainsKey(question.Id) && _answeredQuestions[Challenged.User.Id].ContainsKey(question.Id))
                    {
                        ShowNextQuestion();
                    }
                    return correct;
                }
                else
                {
                    ShowNextQuestion();
                }                
            }
            return false;
        }

        public bool IsFinished()
        {
            return _currentQuestionIndex == Questions.Count - 1;
        }

        public ChallengeResultEvent GetResults()
        {
            int initiatorScore = _answeredQuestions[Initiator.User.Id].Count(a => a.Value);
            int challengedScore = _answeredQuestions[Challenged.User.Id].Count(a => a.Value);
            ChallengeResultEvent result = new ChallengeResultEvent(Initiator.User, Challenged.User, initiatorScore, challengedScore);
            return result;
        }
    }
}