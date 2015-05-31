using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuizApp.Models
{
    public class AddSubdomainModel
    {
        public int Id { get; set; }
        public string SubdomainName { get; set; }
        public int DomainId { get; set; }
    }
}