using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuizApp.Models
{
    public class UserChallengeEvent : IQuizEvent
    {
        public static readonly string TYPE = "UserChallenge";
        public string Type { get; set; }
        public User User { get; set; }

        public UserChallengeEvent(User user)
        {
            this.Type = TYPE;
            this.User = user;
        }

        public UserChallengeEvent()
        {
        }
    }
}