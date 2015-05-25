using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuizApp.Models
{
    public interface IQuizEvent
    {
        string Type { get; set; }
    }
}