using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuizApp.Models
{
    public class DomainModel
    {
        public string Id { get; set; }
        public string DomainName { get; set; }
        public DomainModel()
        {

        }
        public DomainModel(string name)
        {
            this.DomainName = name;
        }
    }
}