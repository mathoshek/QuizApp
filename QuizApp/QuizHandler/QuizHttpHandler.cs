using Microsoft.Web.WebSockets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.WebSockets;
using QuizApp.Models;
using Newtonsoft.Json;

namespace QuizApp.QuizHandler
{
    public class QuizHttpHandler : IHttpHandler
    {
        public bool IsReusable
        {
            get { return false; }
        }

        public void ProcessRequest(HttpContext context)
        {
            if (context.IsWebSocketRequest)
            {
                context.AcceptWebSocketRequest(new QuizWebSocketHandler(context.Request.QueryString["username"]));
            }
        }
    }

    public class QuizWebSocketHandler : WebSocketHandler
    {
        private static WebSocketCollection _quizClients = new WebSocketCollection();
        public static List<QuizWebSocketHandler> _clients = new List<QuizWebSocketHandler>();
        private static Dictionary<string, Challenge> _challenges = new Dictionary<string, Challenge>();
        private static Dictionary<string, Challenge> _userChallenge = new Dictionary<string, Challenge>();

        public User User { get; set; }

        public QuizWebSocketHandler()
            : this(Guid.NewGuid().ToString())
        {
        }

        public QuizWebSocketHandler(string username)
        {
            this.User = new User("usr_" + Guid.NewGuid().ToString(), username);
        }

        public override void OnOpen()
        {
            var loginEvent = new UserLoginEvent(this.User);
            var message = JsonConvert.SerializeObject(loginEvent);
            _quizClients.Broadcast(message);
            foreach (var item in _clients)
            {
                loginEvent = new UserLoginEvent(item.User);
                message = JsonConvert.SerializeObject(loginEvent);
                this.Send(message);
            }
            _quizClients.Add(this);
            _clients.Add(this);
        }

        public override void OnClose()
        {
            _quizClients.Remove(this);
            _clients.Remove(this);
            var logoutEvent = new UserLogoutEvent(this.User);
            var message = JsonConvert.SerializeObject(logoutEvent);
            _quizClients.Broadcast(message);
        }

        public override void OnMessage(string message)
        {
            dynamic data = JsonConvert.DeserializeObject(message);
            ParseEvent(data);
        }

        private void ParseEvent(dynamic data)
        {
            if (data.Type == UserChallengeEvent.TYPE)
            {
                var challengedUser = _clients.FirstOrDefault(a => a.User.Id == data.User.Id.ToString());
                if (challengedUser != null)
                {
                    var userChallenge = new UserChallengeEvent(this.User);
                    var message = JsonConvert.SerializeObject(userChallenge);
                    challengedUser.Send(message);
                }
            }
            else if (data.Type == ChallengeAcceptedEvent.TYPE)
            {
                var challengedUser = this;
                var initiatorUser = _clients.FirstOrDefault(a => a.User.Id == data.User.Id.ToString());
                if (initiatorUser != null)
                {
                    var challengeAcceptedEvent = new ChallengeAcceptedEvent(challengedUser.User);
                    var message = JsonConvert.SerializeObject(challengeAcceptedEvent);
                    initiatorUser.Send(message);
                    Thread.Sleep(2);
                    StartChallenge(initiatorUser, challengedUser);
                }
            }
            else if (data.Type == ChallengeRejectedEvent.TYPE)
            {
                var challengedUser = this;
                var initiatorUser = _clients.FirstOrDefault(a => a.User.Id == data.User.Id.ToString());
                if (initiatorUser != null)
                {
                    var challengeRejectedEvent = new ChallengeRejectedEvent(challengedUser.User);
                    var message = JsonConvert.SerializeObject(challengeRejectedEvent);
                    initiatorUser.Send(message);
                }
            }
            else if (data.Type == QuestionResponseEvent.TYPE)
            {
                if (_userChallenge.ContainsKey(this.User.Id))
                {
                    var challenge = _userChallenge[this.User.Id];
                    bool correct = challenge.AnswerQuestion(this.User.Id, data.Question.Id.ToString(), Convert.ToInt32(data.AnswerIndex.ToString()));
                    AnswerFeedbackEvent answerFeedback = new AnswerFeedbackEvent(data.Question.Id.ToString(), correct);
                    var message = JsonConvert.SerializeObject(answerFeedback);
                    this.Send(message);
                    Thread.Sleep(2);
                    if (challenge.IsFinished())
                    {
                        var results = challenge.GetResults();
                        message = JsonConvert.SerializeObject(results);
                        challenge.Initiator.Send(message);
                        challenge.Challenged.Send(message);
                    }
                }                
            }
        }

        private void StartChallenge(QuizWebSocketHandler initiatorUser, QuizWebSocketHandler challengedUser)
        {
            Challenge challenge = new Challenge(initiatorUser, challengedUser);
            RegisterChallenge(challenge);
            challenge.Start();
        }

        private void RegisterChallenge(Challenge challenge)
        {
            _challenges[challenge.Id] = challenge;
            _userChallenge[challenge.Initiator.User.Id] = challenge;
            _userChallenge[challenge.Challenged.User.Id] = challenge;
        }

        private void UnRegisterChallenge(Challenge challenge)
        {
            _challenges.Remove(challenge.Id);
            _userChallenge.Remove(challenge.Initiator.User.Id);
            _userChallenge.Remove(challenge.Challenged.User.Id);
        }
    }
}