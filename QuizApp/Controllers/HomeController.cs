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

        public ActionResult SustainQuiz()
        {
            int id;
            bool res = Int32.TryParse(Session["QuizId"].ToString(),out id);
            return View();
        }

        public ActionResult Status()
        {
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
           /* List<QuizDto> quizes = new List<QuizDto>();
            List<Repository.DTO.QuizInstanceDto> quizInstances = new List<QuizInstanceDto>();
            quizInstances = repo.GetQuizInstancesForUser(user.Username);
            foreach(var item in quizInstances)
            {
                QuizDto quiz = new QuizDto();
                quiz = repo.getQuiz(item.Id);
                quizes.Add(quiz);
            }
            ViewBag.quizForuser = quizes;
            ViewBag.quzInstancesId = quizInstances;*/
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
