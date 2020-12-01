using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PraktikaProject.Models;

namespace PraktikaProject.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            Task task = new Task();
            using (TasksDBEntities db = new TasksDBEntities())
            {
                task.TaskCollection = db.Tasks.ToList<Task>();
            }
            return View(task);
        }
        [HttpGet]
        public ActionResult Login()
        {
            if (Session["loggedUser"] != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        [HttpPost]
        public ActionResult Login(User userModel)
        {
            using (WorkTimeDBEntities GRE = new WorkTimeDBEntities())
            {
                var UserDetails = GRE.Users.Where(x => x.username == userModel.username && x.password == userModel.password).FirstOrDefault();
                if (UserDetails == null)
                {
                    userModel.LoginErrorMessage = "Invalid credentials !!";
                    return View(userModel);
                }
                else
                {
                    Session["loggedUser"] = UserDetails.username;
                    Session["isCurrentUserAdmin"] = UserDetails.isAdmin;
                }
            }
            return RedirectToAction("Index", "Home");
        }
        public ActionResult Logout()
        {
            Session["loggedUser"] = null;
            return RedirectToAction("Index", "Home");
        }

    }
}