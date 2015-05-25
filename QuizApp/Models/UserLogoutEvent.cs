using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuizApp.Models
{
    public class UserLogoutEvent : IQuizEvent
    {
        public static readonly string TYPE = "UserLogout";
        public string Type { get; set; }
        public User User { get; set; }

        public UserLogoutEvent(User user)
        {
            this.Type = TYPE;
            this.User = user;
        }

        public UserLogoutEvent()
        {
        }
    }
}