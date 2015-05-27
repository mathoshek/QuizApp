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

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Question()
        {         
            return View();
        }
        
        [HttpPost]
        public ActionResult Question(Models.QuestionModel question)
        {
            if (question.QuestionText != null)
            {
               
            }
            else
            {

            }
            return View(question);
        }
    }
}