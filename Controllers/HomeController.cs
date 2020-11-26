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
            return View();
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