using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuizApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
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
                if (u.Username.Equals("abc") && u.Password.Equals("pass"))
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
