using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;


namespace QuizApp.Controllers
{
    public class AdminController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Question()
        {         
            return View();
        }
        
        public ActionResult CreateDomain()
        {
            return View();
        }

        public ActionResult ViewDomains()
        {
            return View();
        }

        public ActionResult FirstPage()
        {
            return View();
        }

        public ActionResult CreateQuiz()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateDomain(Models.DomainModel domain)
        {
            if (domain.DomainName != null)
            {

            }
            else
            {

            }
            return View(domain);
        }

        [HttpPost]
        public ActionResult Question(Models.QuestionModel question)
        {
            if (question.QuestionText != null)
            {
               
            }
            else
            {

            }
            return View(question);
        }
    
        [HttpPost]
        public ActionResult CreateQuiz(Models.CreateQuizModel quiz)
        {
            return View(quiz);
        }
    }
}