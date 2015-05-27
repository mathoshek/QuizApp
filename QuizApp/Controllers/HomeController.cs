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

        public ActionResult Index()
        {
            return RedirectToAction("Login");
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
                    Session["LoggedUserId"] = 1;
                    Session["LoggedUser"] = u.Username;
                    return RedirectToAction("AfterLogin");
                }
            }
            return View(u);
        }

        public ActionResult AfterLogin()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(Models.UserModel u)
        {
            var u1 = u;
            u1.Username = u.Username;
            return RedirectToAction("Login");

        }

    }
}
