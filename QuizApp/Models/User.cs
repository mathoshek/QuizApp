using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuizApp.Models
{
    public class User
    {
        public string Id { get; set; }
        public string Username { get; set; }
        
        public User()
        {
            this.Username = Guid.NewGuid().ToString();
            this.Id = "usr_" + Guid.NewGuid().ToString();
        }

        public User(string id, string username)
        {
            this.Id = id;
            this.Username = username;
        }
    }
}