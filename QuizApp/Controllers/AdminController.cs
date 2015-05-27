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
        Repository.QuizAppRepo repository = new Repository.QuizAppRepo();
        public ActionResult Index()
        {
            ViewBag.Questions = repository.getQuizQuestions();
            return View();
        }

        public ActionResult Question()
        {
            ViewBag.Domains = repository.getQuestionDomains();
            return View();
        }
        
        public ActionResult CreateDomain()
        {
            return View();
        }

        public ActionResult ViewDomains()
        {
            ViewBag.Domains = repository.getQuestionDomains();
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
            repository.addQuestionDomain(domain.DomainName);
            return RedirectToAction("ViewDomains");
        }

        [HttpPost]
        public ActionResult Question(Models.QuestionModel question)
        {
            bool isSingle = false;
            if (question.isSingle == 1)
            {
                isSingle = true;
            }
            repository.addQuizQuestion(question.QuestionText, question.FirstAnswer, question.CorrectFirst, question.SecondAnswer, question.CorrectSecond, question.ThirdAnswer, question.CorrectThird, question.DomainId,isSingle );
            return RedirectToAction("Index");
        }
    
        [HttpPost]
        public ActionResult CreateQuiz(Models.CreateQuizModel quiz)
        {
            return View(quiz);
        }
    }
}