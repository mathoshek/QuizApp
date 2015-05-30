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

        public ActionResult Question(int? questionId)
        {
            ViewBag.Domains = repository.getQuestionDomains();

            if (questionId.HasValue == false)
            {
                ViewBag.Mode = "create";
                return View(new Models.QuestionModel(
                    repository.getQuestionDomains().FirstOrDefault().Id,
                    "",
                    true,
                    "",
                    false,
                    "",
                    false,
                    "",
                    false));
            }

            ViewBag.Mode = "update";
            ViewBag.QuestionId = questionId.Value.ToString();
            QuizQuestionDto qqdto = repository.getQuizQuestion(questionId.Value);

            return View(new Models.QuestionModel(
                qqdto.DomainId, qqdto.QuestionText, qqdto.IsSingleChoice, qqdto.Answer1Text, qqdto.Answer1Correct,
                qqdto.Answer2Text, qqdto.Answer2Correct, qqdto.Answer3Text, qqdto.Answer3Correct));
        }

        public ActionResult DeleteQuestion(int questionId)
        {
            repository.DeleteQuizQuestion(questionId);
            return RedirectToAction("AllQuestions");
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
        public ActionResult Question(int? questionId, Models.QuestionModel question)
        {
            if (questionId.HasValue == false)
            {
                repository.addQuizQuestion(question.QuestionText, question.FirstAnswerText, question.FirstAnswerCorrect, question.SecondAnswerText, question.SecondAnswerCorrect, question.ThirdAnswerText, question.ThirdAnswerCorrect, question.DomainId, question.IsSingleChoice);
            }
            else
            {
                repository.UpdateQuizQuestion(questionId.Value, question.DomainId, question.QuestionText, question.IsSingleChoice,
                    question.FirstAnswerText, question.FirstAnswerCorrect,
                    question.SecondAnswerText, question.SecondAnswerCorrect,
                    question.ThirdAnswerText, question.ThirdAnswerCorrect);
            }
            return RedirectToAction("AllQuestions");
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
            if (repository.UserHasQuiz(model.Username, model.QuizId))
            {
                ViewBag.Message = "User already has this quiz assigned";
                return AssignQuiz();
            }

            QuizInstanceDto qi = new QuizInstanceDto();
            qi.IsStarted = false;
            qi.QuizId = model.QuizId;

            qi = repository.AddQuizInstanceToUser(model.Username, qi);

            QuizDto qdto = repository.getQuiz(model.QuizId);

            // TODO: adauga random intrebari;
            foreach (var x in repository.getQuizQuestions().Where(x => x.DomainId == qdto.DomainId))
            {
                QuestionInstanceDto qidto = new QuestionInstanceDto();
                qidto.QuestionId = x.Id;
                qidto.QuizInstanceId = qi.Id;
                qidto.AnswerSaved = false;
                qidto.Choice1 = false;
                qidto.Choice2 = false;
                qidto.Choice3 = false;
                repository.addQuestionInstance(qidto);
            }

            return AssignQuiz();
        }
    }
}