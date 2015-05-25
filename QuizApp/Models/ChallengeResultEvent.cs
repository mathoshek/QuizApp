using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuizApp.Models
{
    public class ChallengeResultEvent : IQuizEvent
    {
        public static readonly string TYPE = "ChallengeResult";
        public string Type { get; set; }
        public User Initiator { get; set; }
        public User Challenged { get; set; }
        public int InitiatorScore { get; set; }
        public int ChallengedScore { get; set; }

        public ChallengeResultEvent(User initiator, User challenged, int initiatorScore, int challengedScore)
        {
            this.Type = TYPE;
            this.Initiator = initiator;
            this.Challenged = challenged;
            this.InitiatorScore = initiatorScore;
            this.ChallengedScore = challengedScore;
        }

        public ChallengeResultEvent()
        {

        }
    }
}