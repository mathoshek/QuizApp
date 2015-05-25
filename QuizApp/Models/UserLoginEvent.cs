using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuizApp.Models
{
    public class UserLoginEvent : IQuizEvent
    {
        public static readonly string TYPE = "UserLogin";
        public string Type { get; set; }
        public User User { get; set; }

        public UserLoginEvent(User user)
        {
            this.Type = TYPE;
            this.User = user;
        }

        public UserLoginEvent()
        {
        }
    }
}