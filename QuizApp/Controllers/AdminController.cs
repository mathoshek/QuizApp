using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;
using Repository.DTO;
using System.Collections;


namespace QuizApp.Controllers
{
    public class AdminController : Controller
    {
        Repository.QuizAppRepo repository = new Repository.QuizAppRepo();
        public ActionResult Index()
        {
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

        public ActionResult AllQuestions()
        {
            ViewBag.Questions = repository.getQuizQuestions();
            return View();
        }

        public ActionResult CreateQuiz()
        {
            ViewBag.Domains = repository.getQuestionDomains();
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
            if (ModelState.IsValid)
            {
                if (quiz != null)
                {
                    repository.addQuiz(quiz.QuizTitle, quiz.Time, quiz.PassingScore, quiz.QuestionsNumber, quiz.DomainId);
                }
            }
            return View("Index");
        }

        public ActionResult AssignQuiz()
        {
            ViewBag.Users = repository.getUsers();
            ViewBag.Quizes = repository.getQuizes();
            IDictionary<string, List<string>> quizInstances = new Dictionary<string, List<string>>();
            foreach (var user in repository.getUsers())
            {
                List<string> list = new List<string>();
                foreach (var quizInstanceDto in repository.GetQuizInstancesForUser(user.Username))
                {
                    list.Add(repository.getQuiz(quizInstanceDto.QuizId).QuizTitle);
                }
                quizInstances[user.Username] = list;
            }
            ViewBag.QuizInstances = quizInstances;

            return View();
        }

        [HttpPost]
        public ActionResult AssignQuiz(Models.AssignQuizModel model)
        {
            ViewBag.Users = repository.getUsers();
            ViewBag.Quizes = repository.getQuizes();
            IDictionary<string, List<string>> quizInstances = new Dictionary<string, List<string>>();
            foreach (var user in repository.getUsers())
            {
                List<string> list = new List<string>();
                foreach (var quizInstanceDto in repository.GetQuizInstancesForUser(user.Username))
                {
                    list.Add(repository.getQuiz(quizInstanceDto.QuizId).QuizTitle);
                }
                quizInstances[user.Username] = list;
            }
            ViewBag.QuizInstances = quizInstances;

            if (repository.UserHasQuiz(model.Username, model.QuizId))
            {
                ViewBag.Message = "User already has this quiz assigned";
                return View();
            }

            // TODO: adauga random intrebari;

            QuizInstanceDto qi = new QuizInstanceDto();
            qi.IsStarted = false;
            qi.QuizId = model.QuizId;

            repository.AddQuizInstanceToUser(model.Username, qi);

            return View();
        }
    }
}