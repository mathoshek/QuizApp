using Repository;
using Repository.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuizApp.Controllers
{
    public class HomeController : Controller
    {
        private QuizAppRepo repo = new QuizAppRepo();

        public ActionResult TakeQuiz(int quizInstanceId, int index)
        {
            QuizInstanceDto qidto = repo.GetQuizInstance(quizInstanceId);
            QuizDto qdto = repo.getQuiz(qidto.QuizId);
            List<QuestionInstanceDto> lqi = repo.getQuestionInstancesForQuizInstance(quizInstanceId);

            if (qidto.IsStarted == false)
            {
                repo.StartQuizInstance(quizInstanceId);
            }
            else
            {
                if (qidto.IsFinished == false)
                {
                    if (DateTime.Now.Subtract(qidto.StartTime.Value).TotalMinutes >= qdto.Time)
                    {
                        repo.FinishQuizInstance(quizInstanceId);
                        return RedirectToAction("Result", new { quizInstanceId = quizInstanceId });
                    }
                }
                else
                {
                    return RedirectToAction("Result", new { quizInstanceId = quizInstanceId });
                }
            }

            ViewBag.QuizInstanceId = quizInstanceId;
            ViewBag.CurrentIndex = index;
            ViewBag.MaxIndex = lqi.Count;
            ViewBag.Question = repo.getQuizQuestion(lqi[index].QuestionId);
            ViewBag.TimeLeft = qdto.Time * 60 - (int)DateTime.Now.Subtract(qidto.StartTime.Value).TotalSeconds;
            ViewBag.Domains = repo.getQuestionDomains().ToDictionary(x => x.Id);
            ViewBag.Subdomains = repo.getQuizQuestionSubdomains().ToDictionary(x => x.Id);
            if (lqi[index].AnswerSaved)
            {
                Models.SubmitAnswerModel model = new Models.SubmitAnswerModel();
                if (repo.getQuizQuestion(lqi[index].QuestionId).IsSingleChoice)
                {
                    if (lqi[index].Choice1 == true)
                        model.SingleChoiceAnswer = 0;
                    if (lqi[index].Choice2 == true)
                        model.SingleChoiceAnswer = 1;
                    if (lqi[index].Choice3 == true)
                        model.SingleChoiceAnswer = 2;
                }
                else
                {
                    model.MultipleChoiceAnswer1 = lqi[index].Choice1;
                    model.MultipleChoiceAnswer2 = lqi[index].Choice2;
                    model.MultipleChoiceAnswer3 = lqi[index].Choice3;
                }
                return View(model);
            }
            else
                return View();
        }

        public ActionResult Result(int quizInstanceId)
        {
            return View();
        }

        public ActionResult Status(int quizInstanceId)
        {
            ViewBag.Domains = repo.getQuestionDomains().ToDictionary(x => x.Id);
            ViewBag.Subdomains = repo.getQuizQuestionSubdomains().ToDictionary(x => x.Id);
            ViewBag.Questions = repo.getQuizQuestions().ToDictionary(x => x.Id);
            ViewBag.QuestionInstances = repo.getQuestionInstancesForQuizInstance(quizInstanceId);
            ViewBag.QuizInstanceId = quizInstanceId;

            return View();
        }

        public ActionResult FinishQuiz(int quizInstanceId)
        {
            return RedirectToAction("Result", new { quizInstanceId = quizInstanceId });
        }

        [HttpPost]
        public ActionResult SubmitAnswer(int quizInstanceId, int index, Models.SubmitAnswerModel model)
        {
            List<QuestionInstanceDto> lqi = repo.getQuestionInstancesForQuizInstance(quizInstanceId);
            QuestionInstanceDto qidto = lqi[index];
            QuizQuestionDto qqdto = repo.getQuizQuestion(qidto.QuestionId);

            if (qqdto.IsSingleChoice)
            {
                switch (model.SingleChoiceAnswer)
                {
                    case 0:
                        repo.SaveQuestionInstanceAnswer(qidto.Id, true, false, false);
                        break;
                    case 1:
                        repo.SaveQuestionInstanceAnswer(qidto.Id, false, true, false);
                        break;
                    case 2:
                        repo.SaveQuestionInstanceAnswer(qidto.Id, false, false, true);
                        break;
                }
            }
            else
            {
                repo.SaveQuestionInstanceAnswer(qidto.Id, model.MultipleChoiceAnswer1, model.MultipleChoiceAnswer2, model.MultipleChoiceAnswer3);
            }

            return RedirectToAction("TakeQuiz", new { quizInstanceId = quizInstanceId, index = index });
        }

        public ActionResult Index()
        {
            var user = Session["LoggedUser"] as UserDto;
            if (user != null)
            {
                return RedirectToAction("UserLoggedIn");
            }
            return View();
        }

        public ActionResult UserLoggedIn()
        {
            UserDto user = (UserDto)Session["LoggedUser"];

            Dictionary<string, string> quizesForUser = new Dictionary<string, string>();

            foreach (var quizInstance in repo.GetQuizInstancesForUser(user.Username))
            {
                QuizDto qdto = repo.getQuiz(quizInstance.QuizId);
                quizesForUser[quizInstance.Id.ToString()] = qdto.QuizTitle;
            }
            ViewBag.quizesForUser = quizesForUser;
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Models.UserModel u)
        {
            if (ModelState.IsValid)
            {
                UserDto user = repo.getUser(u.Username);
                if (user != null && user.Password == u.Password)
                {
                    Session["LoggedUser"] = user;
                    return RedirectToAction("Index");
                }
                ViewBag.Message = "User does not exist or wrong password...";
                return View(u);
            }
            ViewBag.Message = "Login Failed...";
            return View(u);
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(Models.UserModel u)
        {
            if (ModelState.IsValid)
            {
                UserDto user = repo.getUser(u.Username);
                if (user == null)
                {
                    repo.addUser(u.Username, u.Password, "student");
                    return RedirectToAction("Login");
                }
                ViewBag.Message = "User exists...";
                return View(u);
            }
            ViewBag.Message = "Register failed...";
            return View(u);
        }

        public ActionResult Logout()
        {
            Session["LoggedUser"] = null;
            return RedirectToAction("Index");
        }
    }
}
