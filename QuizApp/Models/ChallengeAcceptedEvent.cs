using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuizApp.Models
{
    public class ChallengeAcceptedEvent : IQuizEvent
    {
        public static readonly string TYPE = "ChallengeAccepted";
        public string Type { get; set; }
        public User User { get; set; }

        public ChallengeAcceptedEvent(User user)
        {
            this.Type = TYPE;
            this.User = user;
        }

        public ChallengeAcceptedEvent()
        {
        }
    }
}