using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuizApp.Models
{
    public class ChallengeRejectedEvent : IQuizEvent
    {
        public static readonly string TYPE = "ChallengeRejected";
        public string Type { get; set; }
        public User User { get; set; }

        public ChallengeRejectedEvent(User user)
        {
            this.Type = TYPE;
            this.User = user;
        }

        public ChallengeRejectedEvent()
        {
        }
    }
}