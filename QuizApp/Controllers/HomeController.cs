 /// <summary>
 /// Nume si prenume: Hurjui Danut/Sandru Adrian/Chmilevski Paul Albert
 /// Laborator: Vineri 16-18
 /// Tema laborator: Proiect ASP.NET MVC 
 /// Data predare proiect: 29.05.2015
 /// Declaratie: Declar pe propria raspundere ca acest proiect nu a fost copiat
 ///din alte surse
 /// Bibliografie, surse de inspiratie: MSDN, http://codeprojects, carti
 /// </summary> 

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

        [HttpPost]
        public ActionResult SustainQuiz(Models.StartQuizModel model)
        {
            Session["QuizInstanceId"] = model.QuizInstanceId;
            int id;
            List<Repository.DTO.QuizQuestionDto> questionsForQuiz = new List<Repository.DTO.QuizQuestionDto>();
            bool res = Int32.TryParse(model.QuizInstanceId, out id);
            List<QuestionInstanceDto> list = repo.getQuestionInstancesForQuizInstance(id);
            Dictionary<string, string> questionForQuiz = new Dictionary<string, string>();

            foreach(var item in list)
            {
                Repository.DTO.QuizQuestionDto ques = new Repository.DTO.QuizQuestionDto();
                ques = repo.getQuizQuestion(id);
                questionForQuiz[model.QuizInstanceId.ToString()] = ques.Id.ToString();
                questionsForQuiz.Add(ques);
            }

            ViewBag.questionForQuiz = questionForQuiz;
            ViewBag.questionsForQuiz = questionsForQuiz;
            return View();
        }

        public ActionResult Status()
        {
            ViewBag.QuestionText = repo.getQuestionInstancesForQuizInstance(1);

            return View();
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
            UserDto user =  (UserDto)Session["LoggedUser"];

            Dictionary<string, string> quizesForUser = new Dictionary<string, string>();

            foreach(var quizInstance in repo.GetQuizInstancesForUser(user.Username))
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
