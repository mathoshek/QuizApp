using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuizApp.Controllers
{
    public class AdminController : Controller
    {
        private QuizAppRepo repo = new QuizAppRepo();

        public ActionResult Index()
        {
            ViewBag.Users = repo.getUsers();
            return View();
        }

    }
}
