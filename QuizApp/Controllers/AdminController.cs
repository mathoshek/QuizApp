using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;
using Repository.DTO;
using System.Collections;
using Newtonsoft.Json;


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
            ViewBag.Subdomains = repository.getQuizQuestionSubdomains();

            Dictionary<int, Dictionary<int, string>> data = new Dictionary<int, Dictionary<int, string>>();
            foreach (var domain in ViewBag.Domains)
            {
                Dictionary<int, string> dict = new Dictionary<int, string>();
                foreach (var subdomain in ViewBag.Subdomains)
                {
                    if (subdomain.DomainId == domain.Id)
                    {
                        dict[subdomain.Id] = subdomain.Name;
                    }
                }
                data[domain.Id] = dict;
            }

            ViewBag.SubdomainData = JsonConvert.SerializeObject(data);

            if (questionId.HasValue == false)
            {
                ViewBag.Mode = "create";
                return View(new Models.QuestionModel(
                    repository.getQuizQuestionSubdomains().FirstOrDefault().DomainId,
                    "",
                    true,
                    "",
                    false,
                    "",
                    false,
                    "",
                    false,
                    repository.getQuizQuestionSubdomains().FirstOrDefault().Id));
            }

            ViewBag.Mode = "update";
            ViewBag.QuestionId = questionId.Value.ToString();
            QuizQuestionDto qqdto = repository.getQuizQuestion(questionId.Value);

            return View(new Models.QuestionModel(
                qqdto.DomainId, qqdto.QuestionText, qqdto.IsSingleChoice, qqdto.Answer1Text, qqdto.Answer1Correct,
                qqdto.Answer2Text, qqdto.Answer2Correct, qqdto.Answer3Text, qqdto.Answer3Correct, qqdto.SubdomainId));
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

        public ActionResult ManageDomains()
        {
            ViewBag.Domains = repository.getQuestionDomains();
            ViewBag.Subdomains = repository.getQuizQuestionSubdomains();
            return View();
        }

        public ActionResult AllQuestions()
        {
            ViewBag.Questions = repository.getQuizQuestions();
            ViewBag.Domains = repository.getQuestionDomains().ToDictionary(x => x.Id);
            ViewBag.Subdomains = repository.getQuizQuestionSubdomains().ToDictionary(x => x.Id);
            return View();
        }

        public ActionResult CreateQuiz()
        {
            ViewBag.Domains = repository.getQuestionDomains();
            return View();
        }

        [HttpPost]
        public ActionResult AddDomain(Models.AddDomainModel domain)
        {
            repository.addQuestionDomain(domain.DomainName);
            return RedirectToAction("ManageDomains");
        }

        [HttpPost]
        public ActionResult Question(int? questionId, Models.QuestionModel question)
        {
            if (questionId.HasValue == false)
            {
                repository.addQuizQuestion(question.QuestionText, question.FirstAnswerText, question.FirstAnswerCorrect, question.SecondAnswerText, question.SecondAnswerCorrect, question.ThirdAnswerText, question.ThirdAnswerCorrect, question.DomainId, question.QuestionType == 0, question.SubdomainId);
            }
            else
            {
                repository.UpdateQuizQuestion(questionId.Value, question.DomainId, question.QuestionText, question.QuestionType == 0,
                    question.FirstAnswerText, question.FirstAnswerCorrect,
                    question.SecondAnswerText, question.SecondAnswerCorrect,
                    question.ThirdAnswerText, question.ThirdAnswerCorrect,
                    question.SubdomainId);
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
            qi.IsFinished = false;

            qi = repository.AddQuizInstanceToUser(model.Username, qi);

            QuizDto qdto = repository.getQuiz(model.QuizId);

            List<int> questionIdsRandom = new List<int>();
            Random rnd = new Random();
            int length = repository.getQuizQuestions().Where(x => x.DomainId == qdto.DomainId).Count();
            for (int i = 0; i < length; i++)
            {
                questionIdsRandom.Add(i);
            }
            List<int> randomQuestion = new List<int>();

            if (length <= 10)
            {
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
            }
            else
            {
                for (int j = 0; j < 10; j++)
                {
                    int question = rnd.Next(0, questionIdsRandom.Count);
                    randomQuestion.Add(questionIdsRandom.ElementAt(question));
                    int elem = questionIdsRandom.ElementAt(question);
                    questionIdsRandom.Remove(elem);
                }
                 foreach (var id in randomQuestion)
                 {
                    foreach (var x in repository.getQuizQuestions().Where(x => x.DomainId == qdto.DomainId))
                    {
                        if (x.Id == id)
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
                    }
                }
            }

            return AssignQuiz();
        }

        [HttpPost]
        public ActionResult AddSubdomain(Models.AddSubdomainModel model)
        {
            repository.addQuizQuestionSubdomain(model.SubdomainName, model.DomainId);
            return RedirectToAction("ManageDomains");
        }
    }
}