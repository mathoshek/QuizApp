<<<<<<< HEAD
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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



    }
}
=======
﻿using Repository;
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
>>>>>>> 1f0f2b0ef5bbc5e1998704969955fc06b8956232
